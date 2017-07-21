
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
        /// ��120ҵ�����������״̬�ı��¼�
        /// </summary>
        public event EventHandler<StatusEventArgs> ConnectionStatusChanged;

        public bool blConnected;

        public bool blLogined;

        public DateTime dtLastRecieveMsgTime;

        /// <summary>
        /// DES��Ϣ����
        /// </summary>
        public static ConcurrentQueue<DesMsg> _revBSSMsg;

        /// <summary>
        /// ��Ϣ���л�����
        /// </summary>
        private Mutex MsgMutex = new Mutex();
        /// <summary>
        /// ��Ϣ������
        /// </summary>
        private BServerMsgTCPHandler MsgHandler;

        /// <summary>
        /// 120ҵ�������IP��ַ
        /// </summary>
        public string strRemoteIP;
        /// <summary>
        /// 120ҵ�������IP�˿�
        /// </summary>
        public short nRemotePort;
        /// <summary>
        /// ���ض˿�
        /// </summary>
        public short nLocalPort;

        /// <summary>
        /// TCP�ͻ���
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
        /// ����
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
        /// ����Client����
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
                LOG.LogHelper.WriteLog("���ӷ������쳣", ex);
            }
        }

        public void Stop()
        {
            SendLoginoffMsg();
        }

        /// <summary>
        /// RecvMessage�¼�����������Ϣ�������_revBSSMsg
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        private void tsh_RecvMessage(object sender, NetEventArgs e)
        {
            //<LoginMsg>
            // <RoutID>3201</RoutID>
            // <TLX>PDEP</TLX>
            // <TName>����ʡ����������</TName>
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
        /// SendMessage�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region
        private void tsh_SendMessage(object sender, NetEventArgs e)
        {
            //<LoginMsg>
            // <RoutID>3201</RoutID>
            // <TLX>PDEP</TLX>
            // <TName>����ʡ����������</TName>
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
        /// Connected�¼�
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
        /// DisConnected�¼�
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
        /// ������ӳ�ʱ�߳�
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
        /// ��ҵ������������߳�
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
                    ///����������Ϣ
                    if (tch._client !=null )
                    {
                        SendHandshakeMsg();
                    }
                    else
                    {
                        LOG.LogHelper.WriteLog("���ַ���ʧ��");
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
        /// ҵ����Ϣ�������
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
                            LOG.LogHelper.WriteLog(string.Format("��Ϣ[No:{0}]����60sδ������������!", data._type.ToString(), data._strData));
                            continue;
                        }
                        switch (data._type)
                        {
                            //ͨ�ò���
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



        #region // ��Ϣ����ģ��
        //����������Ϣ
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

        //�����¼����
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
                        LOG.LogHelper.WriteLog("��¼�ɹ�.");
                    }
                    else
                    {
                        string strResult = "";
                        switch (lr.Result)
                        {
                            case -1:
                                strResult = "ID����";
                                break;
                            case -2:
                                strResult = "�������";
                                break;
                            case -3:
                                strResult = "���ʹ���";
                                break;
                            default:
                                strResult = "δ֪�Ĵ�������-" + lr.Result;
                                break;
                        }
                        LOG.LogHelper.WriteLog("��¼ʧ�ܣ�" + strResult);
                    }
                }
            }
            catch (Exception ex)
            {
                LOG.LogHelper.WriteLog("", ex);
            }
        }

        //����ǳ�����
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
                        LOG.LogHelper.WriteLog("�ǳ��ɹ�.");
                    }
                    else
                    {
                        string strResult = "";
                        switch (lfr.Result)
                        {
                            case -1:
                                strResult = "ID����";
                                break;
                            case -2:
                                strResult = "�������";
                                break;
                            default:
                                strResult = "δ֪�Ĵ�������-" + lfr.Result;
                                break;
                        }
                        LOG.LogHelper.WriteLog("��¼ʧ�ܣ�" + strResult);
                    }
                }
            }
            catch (Exception ex)
            {
                LOG.LogHelper.WriteLog("", ex);
            }
        }

        //�����ɳ���Ϣ
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

        //�������ڵ���Ϣ
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



        //�������ڵ���Ϣ
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


        #region //���¼�����˳���Ϣ
        //����������Ϣ
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
                LOG.LogHelper.WriteLog("���͵�¼��Ϣʧ��", ex);
            }
        }
        //���͵�¼��Ϣ
        private void SendLoginMsg()
        {
            try
            {
                Login lg = new Login();
                lg.ID = ZJM;
                lg.Pwd = "123456";
                lg.Type = "APP";
                lg.Name = "APP��������";
                string strMsg = JSON.ToJson(lg);
                tch.SendToServer(1001, CommandType.joson, strMsg);
            }
            catch (Exception ex)
            {
                LOG.LogHelper.WriteLog("���͵�¼��Ϣʧ��", ex);
            }
        }

        //�����˳���Ϣ
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
                LOG.LogHelper.WriteLog("���͵�¼��Ϣʧ��", ex);
            }
        }

        #endregion


        #region //����120ҵ����Ϣ
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
                LOG.LogHelper.WriteLog(string.Format("����APPת����Ϣ���ͳɹ�����Ϣ����{0}",strMsg));
            }
            catch (Exception ex)
            {
                LOG.LogHelper.WriteLog("����APPת����Ϣ����ʧ��", ex);
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
                LOG.LogHelper.WriteLog(string.Format("����APP������Ϣ���ͳɹ�����Ϣ����{0}", strMsg));
            }
            catch (Exception ex)
            {
                LOG.LogHelper.WriteLog("����APP������Ϣ����ʧ��", ex);
            }
        }
        #endregion

        //JSON��ʽ�ĵ���
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