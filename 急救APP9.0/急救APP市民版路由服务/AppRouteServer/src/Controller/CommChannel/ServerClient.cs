using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using ZIT.Communication.Comm.Communication;
using ZIT.Communication.Comm.Communication.Messages;
using ZIT.Communication.Comm.Server;
using ZIT.LOG;
using ZIT.AppRouteServer.Utility;

namespace ZIT.AppRouteServer.Controller.CommChannel
{
    public class ServerClient
    {
        /// <summary>
        /// IP地址
        /// </summary>
        public string strClientIP;
        /// <summary>
        /// 端口
        /// </summary>
        public string strClientPort;
        /// <summary>
        /// 客户端ID
        /// </summary>
        public long intClientID;
        /// <summary>
        /// 单位行政编号
        /// </summary>
        public string UnitCode;
        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName;
        /// <summary>
        /// 连接时间
        /// </summary>
        public DateTime dtClientConnTime;
        /// <summary>
        /// 网络状态
        /// </summary>
        public NetStatus Status;

        /// <summary>
        /// 服务器
        /// </summary>
        public RouteServer Routeserver;

        private MsgHandler handler;

        public ServerClient(RouteServer server)
        {
            Status = NetStatus.DisConnected;
            dtClientConnTime = DateTime.MinValue;
            UnitCode = "";
            UnitName = "";
            Routeserver = server;
            handler = new MsgHandler(this);
        }

        public void MessageReceived(object sender, MessageEventArgs e)
        {
            try
            {
                handler.Message_Handler(sender, e);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
        }

        public void SendMessage(ScsTextMessage message)
        {
            try
            {
                Routeserver.ScsServer.Clients[this.intClientID].SendMessage(message);
                LogHelper.WriteNetMsgLog("Send message Unitcode is " + UnitCode + " Port is " + Routeserver.ServerPort.ToString() + ":" + message.Text);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
        }
    }
}
