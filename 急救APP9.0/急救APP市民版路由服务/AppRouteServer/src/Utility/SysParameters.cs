using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ZIT.AppRouteServer.Utility
{
    public class SysParameters
    {
        static SysParameters()
        {
            DBType = "Mysql";
            APIErrorReSendInterval = 5;
            SharkHandsInterval = 5;
            try
            {
                AppAPIUsername = ConfigurationManager.AppSettings["AppAPIUsername"];
            }
            catch
            {
                AppAPIUsername = "app_user";
            }

            try
            {
                AppAPIPassword = ConfigurationManager.AppSettings["AppAPIPassword"];
            }
            catch
            {
                AppAPIPassword = "app_password";
            }

            try
            {
                DBConnectString = ConfigurationManager.AppSettings["DBConnectString"];
            }
            catch
            {
                DBConnectString = "Data Source=ORCL;User ID=appinfo;Password=appinfo;";
            }

            try
            {
                CallInServerPort = short.Parse(ConfigurationManager.AppSettings["CallInServerPort"]);
            }
            catch
            {
                CallInServerPort = 6001;
            }

            try
            {
                CarLocationServerPort = short.Parse(ConfigurationManager.AppSettings["CarLocationServerPort"]);
            }
            catch
            {
                CarLocationServerPort = 6002;
            }

            try
            {
                OtherMsgServerPort = short.Parse(ConfigurationManager.AppSettings["OtherMsgServerPort"]);
            }
            catch
            {
                OtherMsgServerPort = 6003;
            }
        }

        /// <summary>
        /// 数据库类型 Oracle; Mysql
        /// </summary>
        public static string DBType;

        /// <summary>
        /// 调用appserver api 异常时，重发消息时间间隔，单位：秒
        /// </summary>
        public static int APIErrorReSendInterval;
        /// <summary>
        /// 与各子系统握手时间间隔，单位：秒
        /// </summary>
        public static int SharkHandsInterval;
        /// <summary>
        /// AppRouteSever web service user name
        /// </summary>
        public static string AppAPIUsername;
        /// <summary>
        /// AppRouteSever web service password
        /// </summary>
        public static string AppAPIPassword;
        /// <summary>
        /// APP电话呼入通道监听端口
        /// </summary>
        public static short CallInServerPort;
        /// <summary>
        /// 车辆轨迹信息通道监听端口
        /// </summary>
        public static short CarLocationServerPort;
        /// <summary>
        /// 其他信息通道监听端口
        /// </summary>
        public static short OtherMsgServerPort;
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string DBConnectString;
    }
}
