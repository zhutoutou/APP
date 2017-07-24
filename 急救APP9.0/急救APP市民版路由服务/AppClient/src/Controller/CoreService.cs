using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ZIT.AppClient.Utility;
using ZIT.AppClient.Model;
using ZIT.LOG;
using ZIT.AppClient.Controller.AppRouteServer;
using ZIT.AppClient.Controller.BusinessServer;
using ZIT.AppClient.Controller.GpsServer;

namespace ZIT.AppClient.Controller
{
    /// <summary>
    /// 核心服务类
    /// </summary>
    public class CoreService
    {
        /// <summary>
        /// 确保CoreService只有一个实例。
        /// </summary>
        private static CoreService instance = null;

        /// <summary>
        /// 与120业务服务器连接状态改变事件
        /// </summary>
        public event EventHandler<StatusEventArgs> BServerConnectionStatusChanged;

        /// <summary>
        /// 与CallInServer连接状态改变事件
        /// </summary>
        public event EventHandler<StatusEventArgs> CallInServerConnectionStatusChanged;

        /// <summary>
        /// 与CarLocationServer连接状态改变事件
        /// </summary>
        public event EventHandler<StatusEventArgs> CarLocationServerConnectionStatusChanged;

        /// <summary>
        /// 与OtherMsgServer连接状态改变事件
        /// </summary>
        public event EventHandler<StatusEventArgs> OtherMsgServerConnectionStatusChanged;

        internal RouteServer CallInServer;
        internal RouteServer CarLocationServer;
        internal RouteServer OtherMsgServer;
        internal BServer bs;
        internal GServer gs;
        public BServerClient bsTcp;

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
        /// 构造函数
        /// </summary>
        private CoreService()
        {
            CallInServer = new RouteServer();
            CallInServer.ServerIP = SysParameters.AppRouteServerIP;
            CallInServer.ServerPort = SysParameters.CallInServerPort;
   
            CarLocationServer = new RouteServer();
            CarLocationServer.ServerIP = SysParameters.AppRouteServerIP;
            CarLocationServer.ServerPort = SysParameters.CarLocationServerPort;

            OtherMsgServer = new RouteServer();
            OtherMsgServer.ServerIP = SysParameters.AppRouteServerIP;
            OtherMsgServer.ServerPort = SysParameters.OtherMsgServerPort;

            if (SysParameters.ApplyVersion == "OLD")
            {
                bs = new BServer();
                bs.strRemoteIP = SysParameters.BServerIP;
                bs.nRemotePort = SysParameters.BServerPort;
                bs.nLocalPort = SysParameters.BLocalPort;

                gs = new GServer();
                gs.nLocalPort = SysParameters.GLocalPort;
            }
            else if (SysParameters.ApplyVersion == "NEW")
            {
                bsTcp = new BServerClient();
                bsTcp.strRemoteIP = SysParameters.BServerIP;
                bsTcp.nRemotePort = SysParameters.BServerPort;
                bsTcp.nLocalPort = SysParameters.BLocalPort;

            }


        }

        /// <summary>
        /// 开始数据交换服务
        /// </summary>
        public void StartService()
        {
            try
            {
                //连接CallInServer
                CallInServer.ConnectionStatusChanged += CallInServer_StatusChanged;
                CallInServer.Start();
            }
            catch (Exception ex) { LOG.LogHelper.WriteLog("", ex); }
            try
            {
                //连接OtherMsgServer
                OtherMsgServer.ConnectionStatusChanged += OtherMsgServer_StatusChanged;
                OtherMsgServer.Start();
            }
            catch (Exception ex) { LOG.LogHelper.WriteLog("", ex); }
            try
            {
                //连接CarLocationServer
                CarLocationServer.ConnectionStatusChanged += CarLocationServer_StatusChanged;
                CarLocationServer.Start();
            }
            catch (Exception ex) { LOG.LogHelper.WriteLog("", ex); }
            try
            {
                if (bs != null)
                {
                    //UDP连接120业务服务器
                    bs.ConnectionStatusChanged += BusnessServer_StatusChanged;
                    bs.Start();
                }
            }
            catch (Exception ex) { LOG.LogHelper.WriteLog("UDP业务服务器启动失败", ex); }
            try
            {
                if (bsTcp != null)
                {
                    //TCP连接120业务服务器
                    bsTcp.ConnectionStatusChanged += BusnessServer_StatusChanged;
                    bsTcp.Start();
                }
            }
            catch (Exception ex) { LOG.LogHelper.WriteLog("TCP业务服务器启动失败", ex); }
            try
            {
                if (gs != null)
                {
                    //监听GPS业务器的广播车辆轨迹数据
                    gs.Start();
                }
            }
            catch (Exception ex) { LOG.LogHelper.WriteLog("", ex); }

        }

        /// <summary>
        /// 停止数据交换服务
        /// </summary>
        public void StopService()
        {
            CallInServer.Stop();
            CarLocationServer.Stop();
            OtherMsgServer.Stop();
            if (bs != null)
            {
                bs.Stop();
            }
            if (bsTcp != null)
            {
                bsTcp.Stop();
            }
            if (gs != null)
            {
                gs.Stop();
            }
        }

        private void BusnessServer_StatusChanged(object sender, StatusEventArgs e)
        {
            OnBServerConnectionStatusChanged(e.Status);
        }


        private void CallInServer_StatusChanged(object sender, StatusEventArgs e)
        {
            OnCallInServerConnectionStatusChanged(e.Status);
        }

        private void CarLocationServer_StatusChanged(object sender, StatusEventArgs e)
        {
            OnCarLocationServerConnectionStatusChanged(e.Status);
        }

        private void OtherMsgServer_StatusChanged(object sender, StatusEventArgs e)
        {
            OnOtherMsgServerConnectionStatusChanged(e.Status);
        }


        /// <summary>
        /// Raises BServerConnectionStatusChanged event.
        /// </summary>
        /// <param name="message">Received message</param>
        protected virtual void OnBServerConnectionStatusChanged(NetStatus status)
        {
            var handler = BServerConnectionStatusChanged;
            if (handler != null)
            {
                handler(this, new StatusEventArgs(status));
            }
        }

        /// <summary>
        /// Raises CallInServerConnectionStatusChanged event.
        /// </summary>
        /// <param name="message">Received message</param>
        protected virtual void OnCallInServerConnectionStatusChanged(NetStatus status)
        {
            var handler = CallInServerConnectionStatusChanged;
            if (handler != null)
            {
                handler(this, new StatusEventArgs(status));
            }
        }

        /// <summary>
        /// Raises CarLocationServerConnectionStatusChanged event.
        /// </summary>
        /// <param name="message">Received message</param>
        protected virtual void OnCarLocationServerConnectionStatusChanged(NetStatus status)
        {
            var handler = CarLocationServerConnectionStatusChanged;
            if (handler != null)
            {
                handler(this, new StatusEventArgs(status));
            }
        }


        /// <summary>
        /// Raises OtherMsgServerConnectionStatusChanged event.
        /// </summary>
        /// <param name="message">Received message</param>
        protected virtual void OnOtherMsgServerConnectionStatusChanged(NetStatus status)
        {
            var handler = OtherMsgServerConnectionStatusChanged;
            if (handler != null)
            {
                handler(this, new StatusEventArgs(status));
            }
        }

    }
}
