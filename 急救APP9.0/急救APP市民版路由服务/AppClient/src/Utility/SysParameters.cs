using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ZIT.AppClient.Utility
{
    public class SysParameters
    {
        static SysParameters()
        {
            SharkHandsInterval = 5;

            AppRouteServerIP = ConfigurationManager.AppSettings["AppRouteServerIP"];
            CallInServerPort = short.Parse(ConfigurationManager.AppSettings["CallInServerPort"]);
            CarLocationServerPort = short.Parse(ConfigurationManager.AppSettings["CarLocationServerPort"]);
            OtherMsgServerPort = short.Parse(ConfigurationManager.AppSettings["OtherMsgServerPort"]);

            BServerIP = ConfigurationManager.AppSettings["BServerIP"];
            BServerPort = short.Parse(ConfigurationManager.AppSettings["BServerPort"]);
            BLocalPort = short.Parse(ConfigurationManager.AppSettings["BLocalPort"]);

            GLocalPort = short.Parse(ConfigurationManager.AppSettings["GLocalPort"]);

            UnitCode = ConfigurationManager.AppSettings["UnitCode"];
            LocalUnitCode = ConfigurationManager.AppSettings["LocalUnitCode"];

            ApplyVersion = ConfigurationManager.AppSettings["ApplyVersion"];
        }

        /// <summary>
        /// 与各服务器握手时间间隔，单位：秒
        /// </summary>
        public static int SharkHandsInterval;// = 5
        /// <summary>
        /// APP路由服务器IP地址
        /// </summary>
        public static string AppRouteServerIP;// = "192.168.0.8";
        /// <summary>
        /// APP电话呼入通道端口
        /// </summary>
        public static short CallInServerPort;// = 6001;
        /// <summary>
        /// 车辆轨迹信息通道端口
        /// </summary>
        public static short CarLocationServerPort;// = 6002;
        /// <summary>
        /// 其他信息传输通道端口
        /// </summary>
        public static short OtherMsgServerPort;// = 6003;
        /// <summary>
        /// 120业务服务器IP地址
        /// </summary>
        public static string BServerIP;// = "192.168.0.254";
        /// <summary>
        /// 120业务服务器监听端口
        /// </summary>
        public static short BServerPort;// = 1003;
        /// <summary>
        /// 与120业务服务器连接的本地端口
        /// </summary>
        public static short BLocalPort;// = 7777;
        /// <summary>
        /// GPS业务服务器消息广播监听端口
        /// </summary>
        public static short GLocalPort;// = 2000;
        /// <summary>
        /// 单位行政编码
        /// </summary>
        public static string UnitCode;// = "522121";
        /// <summary>
        /// 本地单位编号
        /// </summary>
        public static string LocalUnitCode;// = "000000";
        /// <summary>
        /// 应用版本
        /// </summary>
        public static string ApplyVersion;// = "new";
    }
}
