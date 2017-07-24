using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using ZIT.AppClient.Model;
using ZIT.AppClient.Utility;
using ZIT.LOG;

namespace  ZIT.AppClient.Controller.BusinessServer
{
    class BServer
    {
        /// <summary>
        /// 与120业务服务器连接状态改变事件
        /// </summary>
        public event EventHandler<StatusEventArgs> ConnectionStatusChanged;

        /// <summary>
        /// 车辆任务映射表 clid --> caseid,lsh,cccc
        /// </summary>
        public Dictionary<string, VehicleTaskInfo> VehicleTaskMap;

        public ReaderWriterLockSlim VehicleTaskMapLock;

        public Thread CheckVehicleTaskMapThread = null;
        
        private bool blConnected;

        /// <summary>
        /// 接收消息缓冲区
        /// </summary>
        private string strRecvMsg;

        /// <summary>
        /// 与120业务服务器通信
        /// </summary>
        public UdpClient udpClient;

        /// <summary>
        /// 消息队列互斥量
        /// </summary>
        private Mutex MsgMutex = new Mutex();
        /// <summary>
        /// 处理消息线程
        /// </summary>
        public Thread UdpThread = null;

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


        private DateTime dtLastRecieveMsgTime;

        /// <summary>
        /// 消息处理类
        /// </summary>
        private BServerMsgHandler MsgHandler;


        /// <summary>
        /// 构造函数
        /// </summary>
        public BServer()
        {
            dtLastRecieveMsgTime = DateTime.MinValue;
            strRecvMsg = "";
            blConnected = false;
            MsgHandler = new BServerMsgHandler();
            VehicleTaskMapLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion); ;
            VehicleTaskMap = new Dictionary<string, VehicleTaskInfo>();

            CheckVehicleTaskMapThread = new Thread(new ThreadStart(CheckVehicleTaskMapTimeOut_Thread));
        }


        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            try
            {
                StartLocalListener();

                // handle recieved message
                ThreadPool.QueueUserWorkItem(new WaitCallback(HandleRecvMsg_Thread));
                
                //shake handle with bisniess server
                ThreadPool.QueueUserWorkItem(new WaitCallback(SharkHands_Thread), SysParameters.SharkHandsInterval);

                //check business server connect status
                ThreadPool.QueueUserWorkItem(new WaitCallback(CheckConnectedStatus_Thread), SysParameters.SharkHandsInterval);

                CheckVehicleTaskMapThread.Start();
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            SendExitMessage();
        }

        /// <summary>
        /// 检测与业务服务器连接状态
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
                catch
                {
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
                    if (udpClient != null)
                    {
                        string str = "[3000DWBH:" + SysParameters.LocalUnitCode + "*#DWMC:*#ZJM:" + GetZJM() + "*#TLX:APP*#TH:0*#ZBY:*#ZT:1*#LSH:*#ZBBC:*#]";
                        Byte[] sendBytes = Encoding.ASCII.GetBytes(str);
                        udpClient.Send(sendBytes, sendBytes.Length, strRemoteIP, nRemotePort);
                    }
                    else
                    {
                        udpClient = null;
                        udpClient.Close();
                        udpClient = new UdpClient(nLocalPort);
                        UdpThread = new Thread(new ThreadStart(UdpReciveThread));
                        UdpThread.Start();
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog("Error occurred:",ex);
                }
                finally
                {
                    Thread.Sleep(intSharkHandsInterval * 1000);
                }
            }

        }
        /// <summary>
        /// 开启本地UDP监听
        /// </summary>
        public void StartLocalListener()
        {
            try
            {
                if (udpClient != null)
                {
                    UdpThread.Abort();
                    udpClient.Close();
                }
                udpClient = new UdpClient(nLocalPort);
                UdpThread = new Thread(new ThreadStart(UdpReciveThread));
                UdpThread.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 接收数据线程
        /// </summary>
        private void UdpReciveThread()
        {
            IPEndPoint remoteHost = null;
            while (udpClient != null && Thread.CurrentThread.ThreadState.Equals(System.Threading.ThreadState.Running))
            {
                try
                {
                    byte[] buf = udpClient.Receive(ref remoteHost);
                    string bufs = Encoding.GetEncoding(936).GetString(buf);
                    
                    dtLastRecieveMsgTime = DateTime.Now;
                    MsgMutex.WaitOne();
                    try
                    {
                        strRecvMsg += bufs.Trim();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        MsgMutex.ReleaseMutex();
                    }
                }
                catch (SocketException ex)
                {
                    if (ex.ErrorCode != 10054)
                    {
                        LogHelper.WriteLog("", ex);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog("",ex);
                }
            }
        }

        /// <summary>
        /// 处理数据线程
        /// </summary>
        private void HandleRecvMsg_Thread(object e)
        {
            while (true)
            {
                Thread.Sleep(100);
                if (strRecvMsg == null || strRecvMsg == "") continue;

                string strOneMsg = "";

                try
                {
                    MsgMutex.WaitOne();
                    try
                    {
                        int StartIndex = strRecvMsg.IndexOf("[");
                        int EndIndex = strRecvMsg.IndexOf("]");

                        if (StartIndex >= 0 && EndIndex >= 1)
                        {
                            if (EndIndex > StartIndex)
                            {
                                strOneMsg = strRecvMsg.Substring(StartIndex, EndIndex + 1 - StartIndex);
                                strRecvMsg = strRecvMsg.Substring(EndIndex + 1);
                            }
                            else
                            {
                                //去掉错误的消息内容
                                strRecvMsg = strRecvMsg.Substring(EndIndex + 1);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog("", ex);
                    }
                    finally
                    {
                        MsgMutex.ReleaseMutex(); 
                    }

                    if (strOneMsg != "")
                    {
                        MsgHandler.HandleMsg(strOneMsg);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog("", ex);
                }
            }
        }

        /// <summary>
        /// 获取主机名
        /// </summary>
        /// <returns></returns>
        private string GetZJM()
        {
            string hostName = "";
            try
            {
                hostName = Dns.GetHostName();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
            return hostName;
        }

        public void SendExitMessage()
        {
            ///发送握手消息
            if (udpClient != null)
            {
                string str = "[3000DWBH:" + SysParameters.LocalUnitCode + "*#DWMC:*#ZJM:" + GetZJM() + "*#TLX:APP*#TH:0*#ZBY:*#ZT:0*#LSH:*#ZBBC:*#]";
                Byte[] sendBytes = Encoding.ASCII.GetBytes(str);
                udpClient.Send(sendBytes, sendBytes.Length, strRemoteIP, nRemotePort);
            }
        }

        /// <summary>
        /// 给120业务服务器发送APP呼救信息
        /// </summary>
        /// <param name="aci"></param>
        public void SendAppCallInMessage(AppCallInfo aci)
        {
            try
            {
                string message = CreateAppCallInMsg(aci);
                SendMessage(message);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
        }

        private string CreateAppCallInMsg(AppCallInfo aci)
        {
            StringBuilder message = new StringBuilder();
            message.Append("[5180");
            message.Append("caseId:" + aci.CASEID + "*#");
            message.Append("phone:" + aci.PHONE + "*#");
            message.Append("name:" + aci.NAME + "*#");
            message.Append("sex:" + aci.SEX + "*#");
            message.Append("brithday:" + aci.BRITHDAY + "*#");
            message.Append("height:" + aci.HEIGHT.ToString() + "*#");
            message.Append("weight:" + aci.WEIGHT.ToString() + "*#");
            message.Append("identityCard:" + aci.IDENTITYCARD + "*#");
            message.Append("jd:" + aci.JD + "*#");
            message.Append("wd:" + aci.WD + "*#");
            message.Append("address:" + aci.ADDRESS + "*#");
            message.Append("medicalHistory:" + aci.MEDICALHISTORY + "*#");
            message.Append("contactWay1:" + aci.CONTACTWAY1 + "*#");
            message.Append("contactWay2:" + aci.CONTACTWAY2 + "*#");
            message.Append("contactWay3:" + aci.CONTACTWAY3 + "*#");
            message.Append("medicalInsuranceCard:" + aci.MEDICALINSURANCECARD + "*#");
            message.Append("province:" + aci.PROVINCE + "*#");
            message.Append("city:" + aci.CITY + "*#");
            message.Append("callTime:" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", aci.CALLTIME) + "*#");
            message.Append("isSelf:" + aci.ISSELF.ToString() + "*#");
            message.Append("]");
            return message.ToString() ;
        }

        /// <summary>
        /// 给120业务服务器发送服务质量评估信息
        /// </summary>
        /// <param name="aci"></param>
        public void SendQualityEvaluationMessage(QualityEvaluation qe)
        {
            try
            {
                string message = CreateQualityEvaluationMsg(qe);
                SendMessage(message);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
        }

        private string CreateQualityEvaluationMsg(QualityEvaluation qe)
        {
            StringBuilder message = new StringBuilder();
            message.Append("[5184");
            message.Append("lsh:" + qe.LSH + "*#");
            message.Append("caseId:" + qe.CASEID + "*#");
            message.Append("qualityComment:" + qe.QUALITYCOMMENT + "*#");
            message.Append("reason:" + qe.REASON + "*#");
            message.Append("commentTime:" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", qe.COMMENTTIME) + "*#");
            message.Append("]");
            return message.ToString();
        }

        /// <summary>
        /// 维护车辆任务映射表，处理新的出车消息
        /// </summary>
        /// <param name="sci"></param>
        public void DealVehicleMap(SendCarInfo sci)
        {
            if (sci.CLID != null && sci.CLID != "")
            {
                VehicleTaskInfo info = new VehicleTaskInfo();
                info.CaseId = sci.CASEID;
                info.CCCC = sci.CCCC;
                info.LSH = sci.LSH;
                info.AddTime = DateTime.Now;

                VehicleTaskMapLock.EnterWriteLock();
                try
                {
                    if (VehicleTaskMap.ContainsKey(sci.CLID))
                    {
                        VehicleTaskMap[sci.CLID] = info;
                    }
                    else
                    {
                        VehicleTaskMap.Add(sci.CLID, info);
                    }
                }
                catch { }
                finally
                { 
                    VehicleTaskMapLock.ExitWriteLock();
                }
            }
        }

        /// <summary>
        /// 维护车辆任务映射表，处理新的车辆状态变化消息
        /// </summary>
        /// <param name="cs"></param>
        public void DealVehicleMap(CarState cs)
        {
            if (cs.ZT != "" && cs.CLID != "")
            {
                //20160719 修改人：朱星汉 修改内容：定位信息发送至任务完成或者任务终止
                if (cs.ZT == "任务完成" || cs.ZT == "任务终止")
                //if (cs.ZT == "任务完成" || cs.ZT == "任务终止" ||  cs.ZT == "病人上车" || cs.ZT == "送达医院")
                {
                    VehicleTaskMapLock.EnterWriteLock();
                    try
                    {
                        if (VehicleTaskMap.ContainsKey(cs.CLID))
                        {
                            VehicleTaskMap.Remove(cs.CLID);
                        }
                    }
                    catch { }
                    finally
                    {
                        VehicleTaskMapLock.ExitWriteLock();
                    }
                }
            }

        }

        private void CheckVehicleTaskMapTimeOut_Thread()
        {
            while (true)
            {
                VehicleTaskMapLock.EnterWriteLock();
                try
                {
                    foreach (var item in VehicleTaskMap)
                    {
                        if (DateTime.Now.Subtract(item.Value.AddTime) >= new TimeSpan(3, 0, 0))
                        {
                            VehicleTaskMap.Remove(item.Key);
                        }
                    }
                }
                catch{}
                finally
                {
                    VehicleTaskMapLock.ExitWriteLock();
                }
                Thread.Sleep(5 * 60 * 1000);
            }
        }

        public bool GetVehicleTaskInfoByCLID(out VehicleTaskInfo info, string strCLID)
        {
            bool bReturn = false;

            info = new VehicleTaskInfo();
            VehicleTaskMapLock.EnterWriteLock();
            try
            {
                if (VehicleTaskMap.ContainsKey(strCLID))
                {
                    info.CaseId = VehicleTaskMap[strCLID].CaseId;
                    info.CCCC = VehicleTaskMap[strCLID].CCCC;
                    info.LSH = VehicleTaskMap[strCLID].LSH;
                    bReturn = true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
            finally
            {
                VehicleTaskMapLock.ExitWriteLock();
            }
            return bReturn;
        }

        /// <summary>
        /// 发给消息给120业务服务器
        /// </summary>
        /// <param name="strMsg"></param>
        public void SendMessage(string strMsg)
        {
            try
            {
                if (udpClient != null)
                {
                    if (strMsg != "")
                    {
                        Byte[] sendBytes = Encoding.GetEncoding(936).GetBytes(strMsg);
                        udpClient.Send(sendBytes, sendBytes.Length, strRemoteIP, nRemotePort);
                        LogHelper.WriteBssNetMsgLog("Sent message to BServer:" + strMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
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

    }
}
