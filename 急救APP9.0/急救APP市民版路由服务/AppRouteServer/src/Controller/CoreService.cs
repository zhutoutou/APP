using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ZIT.AppRouteServer.Controller.CommChannel;
using ZIT.AppRouteServer.Controller.DataAnalysis;
using ZIT.AppRouteServer.Utility;
using ZIT.AppRouteServer.Model;
using ZIT.LOG;

namespace ZIT.AppRouteServer.Controller
{
    /// <summary>
    /// 核心服务类
    /// </summary>
    public class CoreService
    {
        /// <summary>
        /// 数据库连接状态变化事件
        /// </summary>
        public event EventHandler<StatusEventArgs> DBConnectStatusChanged;

        /// <summary>
        /// 与CallInServer连接客户端改变事件
        /// </summary>
        public event EventHandler<UnitsEventArgs> CallInServerConnectedClientChanged;

        /// <summary>
        /// 与CarLocationServer连接客户端改变事件
        /// </summary>
        public event EventHandler<UnitsEventArgs> CarLocationServerConnectedClientChanged;

        /// <summary>
        /// 与OtherMsgServer连接客户端改变事件
        /// </summary>
        public event EventHandler<UnitsEventArgs> OtherMsgServerConnectedClientChanged;

        /// <summary>
        /// 
        /// </summary>
        internal RouteServer CallInServer;

        internal RouteServer CarLocationServer;

        internal RouteServer OtherMsgServer;

        internal CallInfo CI;

        internal QualityComment QC;

        internal ConnectTest CT;

        private CoreService()
        {
            CallInServer = new RouteServer();
            CarLocationServer = new RouteServer();
            OtherMsgServer = new RouteServer();
            CI = new CallInfo();
            QC = new QualityComment();
            CT = new ConnectTest();
        }


        /// <summary>
        /// 获取当前类实例
        /// </summary>
        /// <returns></returns>
        public static CoreService GetInstance()
        {
            if (null == instance)
            {
                instance = new CoreService();
            }
            return instance;
        }

        /// <summary>
        /// 确保Service只有一个实例。
        /// </summary>
        private static CoreService instance = null;

        public void Start()
        {

            //启动CallInServer
            try
            {
                CallInServer.ServerPort = SysParameters.CallInServerPort;
                CallInServer.ServerConnectedClientChanged += CallInServerConnectedClient_Changed;
                CallInServer.Start();
                LogHelper.WriteLog("CallInServer已启动");
            }
            catch (Exception ex)
            {
                LOG.LogHelper.WriteLog("", ex);
            }

            //启动CarLocationServer
            try
            {
                CarLocationServer.ServerPort = SysParameters.CarLocationServerPort;
                CarLocationServer.ServerConnectedClientChanged += CarLocationServerConnectedClient_Changed;
                CarLocationServer.Start();
                LogHelper.WriteLog("CarLocationServer已启动");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }

            //启动OtherMsgServer
            try
            {
                OtherMsgServer.ServerPort = SysParameters.OtherMsgServerPort;
                OtherMsgServer.ServerConnectedClientChanged += OtherMsgServerConnectedClient_Changed;
                OtherMsgServer.Start();
                LogHelper.WriteLog("OtherMsgServer已启动");
            }
            catch (Exception ex)
            {
                LOG.LogHelper.WriteLog("", ex);
            }

            //启动数据库连接检测线程
            CT.ConnectionStatusChanged += DBConnect_StatusChanged;
            CT.Start();
            LogHelper.WriteLog("数据库连接检测线程已启动");

            //启动CallInfo数据库析服务
            CI.Start();
            LogHelper.WriteLog("CallInfo数据分析服务已启动");

            //启动QualityComment数据分析服务
            QC.Start();
            LogHelper.WriteLog("QualityComment数据分析服务已启动");
        }

        public void Stop()
        {
            CallInServer.Stop();
            LogHelper.WriteLog("CallInServer已关闭");
            CarLocationServer.Stop();
            LogHelper.WriteLog("CarLocationServer已关闭");
            OtherMsgServer.Stop();
            LogHelper.WriteLog("OtherMsgServer已关闭");
            CT.Stop();
            LogHelper.WriteLog("CallInfo数据库连接检测线程已关闭");
            CI.Stop();
            LogHelper.WriteLog("CallInfo数据分析服务已关闭");
            QC.Stop();
            LogHelper.WriteLog("QualityComment数据分析服务已关闭");
        }

        private void DBConnect_StatusChanged(object sender, StatusEventArgs e)
        {
            OnDBConnectStatusChanged(e.Status);
        }

        private void CallInServerConnectedClient_Changed(object sender, UnitsEventArgs e)
        {
            OnCallInServerConnectedClientChanged(e.Units);
        }

        private void CarLocationServerConnectedClient_Changed(object sender, UnitsEventArgs e)
        {
            OnCarLocationServerConnectedClientChanged(e.Units);
        }

        private void OtherMsgServerConnectedClient_Changed(object sender, UnitsEventArgs e)
        {
            OnOtherMsgServerConnectedClientChanged(e.Units);
        }

        /// <summary>
        /// Raises CallInServerConnectedClientChanged event.
        /// </summary>
        /// <param name="message">Received message</param>
        protected virtual void OnCallInServerConnectedClientChanged(List<OnlineUnit> units)
        {
            var handler = CallInServerConnectedClientChanged;
            if (handler != null)
            {
                handler(this, new UnitsEventArgs(units));
            }
        }

        /// <summary>
        /// Raises CarLocationServerConnectedClientChanged event.
        /// </summary>
        /// <param name="message">Received message</param>
        protected virtual void OnCarLocationServerConnectedClientChanged(List<OnlineUnit> units)
        {
            var handler = CarLocationServerConnectedClientChanged;
            if (handler != null)
            {
                handler(this, new UnitsEventArgs(units));
            }
        }

        /// <summary>
        /// Raises OtherMsgServerConnectedClientChanged event.
        /// </summary>
        /// <param name="message">Received message</param>
        protected virtual void OnOtherMsgServerConnectedClientChanged(List<OnlineUnit> units)
        {
            var handler = OtherMsgServerConnectedClientChanged;
            if (handler != null)
            {
                handler(this, new UnitsEventArgs(units));
            }
        }

        /// <summary>
        /// Raises DBConnectStatusChanged event.
        /// </summary>
        /// <param name="message">Received message</param>
        protected virtual void OnDBConnectStatusChanged(NetStatus status)
        {
            var handler = DBConnectStatusChanged;
            if (handler != null)
            {
                handler(this, new StatusEventArgs(status));
            }
        }
    }
}
