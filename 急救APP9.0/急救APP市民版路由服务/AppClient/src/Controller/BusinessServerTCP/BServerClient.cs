
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using NetHandler;
using NetHandler.Client;
using System.Threading;
using System.Text;
using System.Text.RegularExpressions;
using ZIT.AppClient.Utility;
using ZIT.AppClient.Model;
using System.Collections.Concurrent;
using ZIT.Communication.Comm.Communication.Messages;
using ZIT.LOG;

namespace ZIT.AppClient.Controller.BusinessServer
{
    public class BServerClient
    {
        public string ZJM = "004";
        /// <summary>
        /// 与120业务服务器连接状态改变事件
        /// </summary>
        public event EventHandler<StatusEventArgs> ConnectionStatusChanged;

        public bool blConnected;

        public bool blLogined;

        public DateTime dtLastRecieveMsgTime;

        /// <summary>
        /// DES消息队列
        /// </summary>
        public static ConcurrentQueue<DesMsg> _revBSSMsg;

        /// <summary>
        /// 消息队列互斥量
        /// </summary>
        private Mutex MsgMutex = new Mutex();
        /// <summary>
        /// 消息处理类
        /// </summary>
        private BServerMsgTCPHandler MsgHandler;

        /// <summary>
        /// 120业务服务器IP地址
        /// </summary>
        public string strRemoteIP;
        /// <summary>
        /// 120业务服务器IP端口
        /// </summary>
        public short nRemotePort;
        /// <summary>
        /// 本地端口
        /// </summary>
        public short nLocalPort;

        /// <summary>
        /// TCP客户端
        /// </summary>
        private TcpClientHandler tch;
        
        public BServerClient()
        {
            dtLastRecieveMsgTime = DateTime.MinValue;
            blConnected = false;
            blLogined = false;
            dtLastRecieveMsgTime = DateTime.Now; 
        }

        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            _revBSSMsg = new ConcurrentQueue<DesMsg>();
            //connect 120BSS
            StartConnectServer();
            // handle recieved message
            ThreadPool.QueueUserWorkItem(new WaitCallback(DealMsgQueue_Thread));
            //check business server connect status
            //ThreadPool.QueueUserWorkItem(new WaitCallback(CheckConnectedStatus_Thread), SysParameters.SharkHandsInterval);
            //shake handle with bisniess server
            ThreadPool.QueueUserWorkItem(new WaitCallback(SharkHands_Thread), SysParameters.SharkHandsInterval);

        }

        /// <summary>
        /// 启动Client连接
        /// </summary>
        public void StartConnectServer()
        {

            IPEndPoint _remote=new IPEndPoint(IPAddress.Parse(strRemoteIP),nRemotePort);
            IPEndPoint _local=new IPEndPoint(IPAddress.Any,nLocalPort);
            NetTransCondiction ntc=new NetTransCondiction();
            ntc.IsPackHead =true;
            ntc.IsShakeHand =true;

            tch = new TcpClientHandler(_local,_remote,ntc);
            tch.Connected += new System.EventHandler<NetEventArgs>(tsh_Connected);
            tch.DisConnected += new System.EventHandler<NetEventArgs>(tsh_DisConnected);
            tch.RecvMessage += new System.EventHandler<NetEventArgs>(tsh_RecvMessage);
            tch.SendMessage += new System.EventHandler<NetEventArgs>(tsh_SendMessage);
            tch._isPermitReconnect = true;
            try
            {
                tch.Connect();
            }
            catch (Exception ex)
            {
                LOG.LogHelper.WriteLog("连接服务器异常", ex);
            }
        }

        public void Stop()
        {
            SendLoginoffMsg();
        }

        /// <summary>
        /// RecvMessage事件，并存入消息处理队列_revBSSMsg
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        private void tsh_RecvMessage(object sender, NetEventArgs e)
        {
            //<LoginMsg>
            // <RoutID>3201</RoutID>
            // <TLX>PDEP</TLX>
            // <TName>江苏省交换服务器</TName>
            //</LoginMsg>			
            try
            {
                int msgno = e.msgEntity.msgH.msgID;
                string body = Encoding.Default.GetString(e.msgEntity.msgB);
                dtLastRecieveMsgTime = DateTime.Now;
                LogHelper.WriteSevNetMsgLog(body);
                switch (msgno)
                {
                    case 1000:  //Handshake
                        Handshake hk = new Handshake();
                        hk = (Handshake)JSON.JsonToObject(body,hk);
                        if (hk != null)

                            _revBSSMsg.Enqueue(new DesMsg(DesMsgType.Handshake, hk, body));
                        break;
                    case 1002:  //LoginReponse
                        LoginResponse lr = new LoginResponse();
                        lr = (LoginResponse)JSON.JsonToObject(body,lr);
                        if (lr != null)
                            _revBSSMsg.Enqueue(new DesMsg(DesMsgType.LoginResponse, lr, body));
                        break;
                    case 1004:  //LoginoffReponse
                        LogoffResponse lfr = new LogoffResponse();
                        lfr = (LogoffResponse)JSON.JsonToObject(body,lfr);
                        if (lfr != null)
                            _revBSSMsg.Enqueue(new DesMsg(DesMsgType.LogoffResponse, lfr, body));
                        break;
                    case 7001:  //DispatchCarNotice
                        DispatchCarNotice pc = new DispatchCarNotice();
                        pc=(DispatchCarNotice)JSON.JsonToObject(body,pc);
                        if (pc != null)
                            _revBSSMsg.Enqueue(new DesMsg(DesMsgType.DispatchCarNotice, pc, body));
                        break;
                    case 7002: //VehicleStatusResponse 
                        VehicleStatusResponse vs = new VehicleStatusResponse();
                        vs = (VehicleStatusResponse)JSON.JsonToObject(body, vs);
                        if (vs != null)
                            _revBSSMsg.Enqueue(new DesMsg(DesMsgType.VehicleStatusResponse, vs, body));
                        break;
                    case 7004: //VehiclePointResponse
                        VehiclePointResponse vp = new VehiclePointResponse();
                        vp = (VehiclePointResponse)JSON.JsonToObject(body, vp);
                        if (vp != null)
                            _revBSSMsg.Enqueue(new DesMsg(DesMsgType.VehiclePointResponse, vp, body));
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LOG.LogHelper.WriteLog("", ex);
            }

        }





        #endregion
        /// <summary>
        /// SendMessage事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        private void tsh_SendMessage(object sender, NetEventArgs e)
        {
            //<LoginMsg>
            // <RoutID>3201</RoutID>
            // <TLX>PDEP</TLX>
            // <TName>江苏省交换服务器</TName>
            //</LoginMsg>			
            try
            {
                LogHelper.WriteBssNetMsgLog("Sent message to BssServer:" + e.Message.ToString());
            }
            catch (Exception ex)
            {
                LOG.LogHelper.WriteLog("", ex);
            }

        }
        #endregion
        /// <summary>
        /// Connected事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsh_Connected(object sender, NetEventArgs e)
        {
            if (!blConnected)
            {
                blConnected =true;
                //raise connect event
                OnConnectionStatusChanged(NetStatus.Connected);
            }
        }

        /// <summary>
        /// DisConnected事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsh_DisConnected(object sender, NetEventArgs e)
        {
            if (blConnected)
            {
                blConnected = false;
                blLogined = false;
                //raise disconnect event
                OnConnectionStatusChanged(NetStatus.DisConnected);
            }
        }

        private void OnConnectionStatusChanged(NetStatus status)
        {
            var handler = ConnectionStatusChanged;
            if (handler != null)
            {
                handler(this, new StatusEventArgs(status));
            }
        }

        /// <summary>
        /// 检测连接超时线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckConnectedStatus_Thread(object e)
        {
            int SharkHandsTime = int.Parse(e.ToString());
            int CheckConnectedInterval = 3;
            while (true)
            {
                Thread.Sleep(CheckConnectedInterval * 1000);
                try
                {
                    if (DateTime.Now.Subtract(dtLastRecieveMsgTime) > new TimeSpan(0, 0, 2 * SharkHandsTime + 1))
                    {
                        if (blConnected)
                        {
                            blConnected = false;
                            blLogined = false;
                            //raise disconnect event
                            OnConnectionStatusChanged(NetStatus.DisConnected);
                        }
                    }
                    else
                    {
                        if (!blConnected)
                        {
                            blConnected = true;
                            //raise connect event
                            OnConnectionStatusChanged(NetStatus.Connected);
                        }
                    }
                }
                catch(Exception ex)
                {
                    LOG.LogHelper.WriteLog("", ex);
                }
            }
        }
        
        /// <summary>
        /// 与业务服务器握手线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SharkHands_Thread(object e)
        {
            int intSharkHandsInterval = int.Parse(e.ToString());
            while (true)
            {
                
                try
                {
                    ///发送握手消息
                    if (tch._client !=null )
                    {
                        SendHandshakeMsg();
                    }
                    else
                    {
                        LOG.LogHelper.WriteLog("握手发送失败");
                    }
                }
                catch (Exception ex)
                {
                    LOG.LogHelper.WriteLog("Error occurred:", ex);
                }
                finally
                {
                    Thread.Sleep(intSharkHandsInterval * 1000);
                }
            }

        }

        /// <summary>
        /// 业务消息处理进程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DealMsgQueue_Thread(object e)
        {

            while (true)
            {
                if (_revBSSMsg.Count > 0)
                {
                    try
                    {
                        DesMsg data = null;
                        bool blnSuccess = _revBSSMsg.TryDequeue(out data);

                        LOG.LogHelper.WriteBssNetMsgLog("Recieve BServer message:" + data._strData);
                        TimeSpan ts = DateTime.Now - data._dt;
                        Hashtable hs;
                        if (ts.TotalSeconds > 60000)
                        {
                            LOG.LogHelper.WriteLog(string.Format("消息[No:{0}]超过60s未处理，将被舍弃!", data._type.ToString(), data._strData));
                            continue;
                        }
                        switch (data._type)
                        {
                            //通用部分
                            case DesMsgType.Handshake:
                                DealHandShake(data._data);
                                break;
                            case DesMsgType.LoginResponse:
                                DealLoginResponse(data._data);
                                break;
                            case DesMsgType.LogoffResponse:
                                DealLogoffResponse(data._data);
                                break;      
                            case DesMsgType.DispatchCarNotice:
                                DealDispatchCarNotice(data._data);
                                break;
                            case DesMsgType.VehicleStatusResponse:
                                DealVehicleStatusResponse(data._data);
                                break;
                            case DesMsgType.VehiclePointResponse:
                                DealVehiclePointResponse(data._data);
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        LOG.LogHelper.WriteLog("", ex);
                    }
                }
                else
                    Thread.Sleep(1000);
            }

        }



        #region // 消息处理模块
        //处理握手消息
        private void DealHandShake(object obj)
        {
            try
            {
                dtLastRecieveMsgTime = DateTime.Now;
                blConnected = true;
                if (!blLogined)
                {
                    SendLoginMsg();
                }
            }
            catch (Exception ex)
            {
                LOG.LogHelper.WriteLog("", ex);
            }
        }

        //处理登录反馈
        private void DealLoginResponse(object obj)
        {
            try
            {
                LoginResponse lr = (LoginResponse)obj;
                if (lr.ID == ZJM)
                {
                    if (lr.Result == 1)
                    {
                        blLogined = true;
                        OnConnectionStatusChanged(NetStatus.Login);
                        LOG.LogHelper.WriteLog("登录成功.");
                    }
                    else
                    {
                        string strResult = "";
                        switch (lr.Result)
                        {
                            case -1:
                                strResult = "ID错误";
                                break;
                            case -2:
                                strResult = "密码错误";
                                break;
                            case -3:
                                strResult = "类型错误";
                                break;
                            default:
                                strResult = "未知的错误类型-" + lr.Result;
                                break;
                        }
                        LOG.LogHelper.WriteLog("登录失败：" + strResult);
                    }
                }
            }
            catch (Exception ex)
            {
                LOG.LogHelper.WriteLog("", ex);
            }
        }

        //处理登出反馈
        private void DealLogoffResponse(object obj)
        {
            try
            {
                LogoffResponse lfr = (LogoffResponse)obj;
                if (lfr.ID == ZJM)
                {
                    if (lfr.Result == 1)
                    {
                        blLogined = true;
                        OnConnectionStatusChanged(NetStatus.Login);
                        LOG.LogHelper.WriteLog("登出成功.");
                    }
                    else
                    {
                        string strResult = "";
                        switch (lfr.Result)
                        {
                            case -1:
                                strResult = "ID错误";
                                break;
                            case -2:
                                strResult = "密码错误";
                                break;
                            default:
                                strResult = "未知的错误类型-" + lfr.Result;
                                break;
                        }
                        LOG.LogHelper.WriteLog("登录失败：" + strResult);
                    }
                }
            }
            catch (Exception ex)
            {
                LOG.LogHelper.WriteLog("", ex);
            }
        }

        //处理派车消息
        private void DealDispatchCarNotice(object obj)
        {
            try
            {
                DispatchCarNotice pc= (DispatchCarNotice)obj;
                SendCarInfo sc = getScFromPc(pc);

                CoreService.GetInstance().OtherMsgServer.SendCarInfo(sc);
            }
            catch (Exception ex)
            {
                LOG.LogHelper.WriteLog("", ex);
            }
        }

        private SendCarInfo getScFromPc(DispatchCarNotice pc)
        {
            SendCarInfo sc = new SendCarInfo();
            try
            {   int i;
                DateTime dt;
                
                sc.CASEID = pc.caseid;
                if (int.TryParse(pc.cccc,out i)) sc.CCCC = int.Parse(pc.cccc);
                if (DateTime.TryParse(pc.ccsj, out dt)) sc.CCSJ = DateTime.Parse(pc.ccsj);
                sc.CLID = pc.clid;
                sc.CPH = pc.cph;
                sc.DOCTORNAME = pc.doctorname;
                sc.DOCTORPHONE = pc.doctorphone;
                sc.DRIVERNAME = pc.drivername;
                sc.DRIVERPHONE = pc.driverphone;
                sc.LSH = pc.lsh;
                sc.SSDW = pc.ssdw;
            }
            catch(Exception ex)
            {
                LOG.LogHelper.WriteLog("", ex);
            }
            return sc;
        }

        //处理车辆节点消息
        private void DealVehicleStatusResponse(object obj)
        {
            try
            {
                VehicleStatusResponse vs = (VehicleStatusResponse)obj;
                CarState cs = getCsFromVs(vs);
                CoreService.GetInstance().OtherMsgServer.SendCarStep(cs);
                
            }
            catch (Exception ex)
            {
                LOG.LogHelper.WriteLog("", ex);
            }
        }

        private CarState getCsFromVs(VehicleStatusResponse vs)
        {
            CarState cs = new CarState();
            try
            {
                int i;
                DateTime dt;

                cs.CASEID = vs.caseid;
                if (int.TryParse(vs.cccc, out i)) cs.CCCC = int.Parse(vs.cccc);
                if (DateTime.TryParse(vs.sj, out dt)) cs.SJ = DateTime.Parse(vs.sj);
                cs.CLID = vs.clid;
                cs.TASK_TERMINATION_REASON = vs.reason;
                cs.ZT = vs.zt;
                cs.LSH = vs.lsh;

            }
            catch (Exception ex)
            {
                LOG.LogHelper.WriteLog("", ex);
            }
            return cs;
        }



        //处理车辆节点消息
        private void DealVehiclePointResponse(object obj)
        {
            try
            {
                VehiclePointResponse vp = (VehiclePointResponse)obj;
                CarLocation cl = getClFromVp(vp);
                CoreService.GetInstance().OtherMsgServer.SendCarLocation(cl);

            }
            catch (Exception ex)
            {
                LOG.LogHelper.WriteLog("", ex);
            }
        }

        private CarLocation getClFromVp(VehiclePointResponse vp)
        {
            CarLocation cl = new CarLocation();
            try
            {
                Decimal i;
                int j;
                DateTime dt;
                cl.CASEID = vp.caseid;
                if (int.TryParse(vp.cc, out j)) cl.CCCC = int.Parse(vp.cc);
                if (DateTime.TryParse(vp.sj, out dt)) cl.SJ = DateTime.Parse(vp.sj);
                cl.CLID = vp.id;
                cl.FX= vp.fx;
                cl.JD = vp.jd;
                cl.LSH = vp.lsh;
                if (Decimal.TryParse(vp.sd, out i)) cl.SD = Decimal.Parse(vp.sd);
                cl.WD = vp.wd;
                

            }
            catch (Exception ex)
            {
                LOG.LogHelper.WriteLog("", ex);
            }
            return cl;
        }
        private string getIntFromFloat(string str)
        {
            try
            {
                float f;
                bool bln = float.TryParse(str, out f);
                if (bln == true)
                {
                    return ((int)f).ToString();
                }
                else
                {
                    return str;
                }
            }
            catch (Exception ex)
            {
                return str;
            }
        }

        #endregion


        #region //与登录握手退出消息
        //发送握手消息
        private void SendHandshakeMsg()
        {
            try
            {
                Handshake hk = new Handshake();
                hk.ID = ZJM;
                string strMsg = JSON.ToJson(hk);
                tch.SendToServer(1000, CommandType.joson, strMsg);
            }
            catch (Exception ex)
            {
                LOG.LogHelper.WriteLog("发送登录消息失败", ex);
            }
        }
        //发送登录消息
        private void SendLoginMsg()
        {
            try
            {
                Login lg = new Login();
                lg.ID = ZJM;
                lg.Pwd = "123456";
                lg.Type = "APP";
                lg.Name = "APP报警接入";
                string strMsg = JSON.ToJson(lg);
                tch.SendToServer(1001, CommandType.joson, strMsg);
            }
            catch (Exception ex)
            {
                LOG.LogHelper.WriteLog("发送登录消息失败", ex);
            }
        }

        //发送退出消息
        private void SendLoginoffMsg()
        {
            try
            {
                Logoff lgf = new Logoff();
                lgf.ID = ZJM;
                lgf.Pwd = "123456";
                string strMsg = JSON.ToJson(lgf);
                tch.SendToServer(1003, CommandType.joson, strMsg);
            }
            catch (Exception ex)
            {
                LOG.LogHelper.WriteLog("发送登录消息失败", ex);
            }
        }

        #endregion


        #region //发送120业务消息
        public void SendAppCallInMessage(AppCallInfo aci)
        {
            try
            {
                receiveAPPSheet res = new receiveAPPSheet();
                res.address = aci.ADDRESS;
                res.areacode = SysParameters.LocalUnitCode;
                res.brithday = aci.BRITHDAY;
                res.calltime = aci.CALLTIME.ToString();
                res.caseid = aci.CASEID;
                res.contactway1 = aci.CONTACTWAY1;
                res.contactway2 = aci.CONTACTWAY2;
                res.contactway3 = aci.CONTACTWAY3;
                res.height = aci.HEIGHT.ToString();
                res.identitycard = aci.IDENTITYCARD;
                res.isself = aci.ISSELF.ToString();
                res.jd = aci.JD;
                res.medicalhistory = aci.MEDICALHISTORY;
                res.medicalinsurancecard = aci.MEDICALINSURANCECARD;
                res.name = aci.NAME;
                res.phone = aci.PHONE;
                res.sex = aci.SEX.ToString();
                res.wd = aci.WD;
                res.weight = aci.WEIGHT.ToString();
                res.area = aci.AREA.ToString();
                res.city = aci.CITY.ToString();
                res.province=aci.PROVINCE.ToString();
                string strMsg = JSON.ToJson(res);
                tch.SendToServer(7000, CommandType.joson, strMsg);
                LOG.LogHelper.WriteLog(string.Format("发送APP转单消息发送成功，消息内容{0}",strMsg));
            }
            catch (Exception ex)
            {
                LOG.LogHelper.WriteLog("发送APP转单消息发送失败", ex);
            }
        }

        public void SendQualityEvaluationMessage(QualityEvaluation qe)
        {
            try
            {
                QualityRespnose qr = new QualityRespnose();
                qr.caseid = qe.CASEID;
                qr.commenttime = qe.COMMENTTIME.ToString();
                qr.lsh = qe.LSH;
                qr.qualitycomment = qe.QUALITYCOMMENT;
                qr.reason = qe.REASON;
                string strMsg = JSON.ToJson(qr);
                tch.SendToServer(7003, CommandType.joson, strMsg);
                LOG.LogHelper.WriteLog(string.Format("发送APP评价消息发送成功，消息内容{0}", strMsg));
            }
            catch (Exception ex)
            {
                LOG.LogHelper.WriteLog("发送APP评价消息发送失败", ex);
            }
        }
        #endregion

        //JSON格式的调整
        private string GetCorrectStyle(string body)
        {
            try
            {
                body = body.Replace("ambulancetelcode", "ambulanceTelCode");
                body = body.Replace("platenum", "plateNum");
            }
            catch (Exception ex)
            {
                LOG.LogHelper.WriteLog("",ex);
            }
            return body;
        }


    }//end PDesSvc
}//end namespace Bound