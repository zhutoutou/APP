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

namespace  ZIT.AppClient.Controller.GpsServer
{
    class GServer
    {
        /// <summary>
        /// 与GPS业务服务器连接状态改变事件
        /// </summary>
        public event EventHandler<StatusEventArgs> ConnectionStatusChanged;


        private bool blConnected;

        /// <summary>
        /// 接收消息缓冲区
        /// </summary>
        private string strRecvMsg;

        /// <summary>
        /// 与GPS业务服务器通信
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
        /// 本地端口
        /// </summary>
        public short nLocalPort;


        private DateTime dtLastRecieveMsgTime;

        /// <summary>
        /// 消息处理类
        /// </summary>
        private GServerMsgHandler MsgHandler;


        /// <summary>
        /// 构造函数
        /// </summary>
        public GServer()
        {
            dtLastRecieveMsgTime = DateTime.MinValue;
            strRecvMsg = "";
            blConnected = false;
            MsgHandler = new GServerMsgHandler();
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
                    LogHelper.WriteLog("", ex);
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
                        int StartIndex = strRecvMsg.IndexOf("(");
                        int EndIndex = strRecvMsg.IndexOf(")");

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
