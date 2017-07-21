using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using ZIT.AppRouteServer.Utility;

namespace ZIT.AppRouteServer.DataAccess
{
    public class DataAccess
    {
        /// <summary>
        /// 定义根程序集
        /// </summary>
        private static readonly string strAssemblyName = "DataAccess";
        private static readonly string strNameSpaceName = "ZIT.AppRouteServer.DataAccess";
        /// <summary>
        /// 读取数据库类型
        /// </summary>
        private static readonly string db = SysParameters.DBType; 
        /// <summary>
        /// 数据访问类:AppCallInfo
        /// </summary>
        /// <returns></returns>
        public static IDBAppCallInfo GetDBAppCallInfo()
        {
            string strClassName = strNameSpaceName + "." + db + ".DBAppCallInfo";
            return (IDBAppCallInfo)Assembly.Load(strAssemblyName).CreateInstance(strClassName);
        }

        /// <summary>
        /// 数据访问类:CarLocation
        /// </summary>
        /// <returns></returns>
        public static IDBCarLocation GetDBCarLocation()
        {
            string strClassName = strNameSpaceName + "." + db + ".DBCarLocation";
            return (IDBCarLocation)Assembly.Load(strAssemblyName).CreateInstance(strClassName);
        }

        /// <summary>
        /// 数据访问类:DBCarState
        /// </summary>
        /// <returns></returns>
        public static IDBCarState GetDBCarState()
        {
            string strClassName = strNameSpaceName + "." + db + ".DBCarState";
            return (IDBCarState)Assembly.Load(strAssemblyName).CreateInstance(strClassName);
        }

        /// <summary>
        /// 数据访问类:DBConnTest
        /// </summary>
        /// <returns></returns>
        public static IDBConnTest GetDBConnTest()
        {
            string strClassName = strNameSpaceName + "." + db + ".DBConnTest";
            return (IDBConnTest)Assembly.Load(strAssemblyName).CreateInstance(strClassName);
        }

        /// <summary>
        /// 数据访问类:DBHandleCallError
        /// </summary>
        /// <returns></returns>
        public static IDBHandleCallError GetDBHandleCallError()
        {
            string strClassName = strNameSpaceName + "." + db + ".DBHandleCallError";
            return (IDBHandleCallError)Assembly.Load(strAssemblyName).CreateInstance(strClassName);

        }


        /// <summary>
        /// 数据访问类:DBQualityEvaluation
        /// </summary>
        /// <returns></returns>
        public static IDBQualityEvaluation GetDBQualityEvaluation()
        {
            string strClassName = strNameSpaceName + "." + db + ".DBQualityEvaluation";
            return (IDBQualityEvaluation)Assembly.Load(strAssemblyName).CreateInstance(strClassName);
        }

        /// <summary>
        /// 数据访问类:DBRouter
        /// </summary>
        /// <returns></returns>
        public static IDBRouter GetDBRouter()
        {
            string strClassName = strNameSpaceName + "." + db + ".DBRouter";
            return (IDBRouter)Assembly.Load(strAssemblyName).CreateInstance(strClassName);
        }

        /// <summary>
        /// 数据访问类:DBSendCarInfo
        /// </summary>
        /// <returns></returns>
        public static IDBSendCarInfo GetDBSendCarInfo()
        {
            string strClassName = strNameSpaceName + "." + db + ".DBSendCarInfo";
            return (IDBSendCarInfo)Assembly.Load(strAssemblyName).CreateInstance(strClassName);
        }

        /// <summary>
        /// 数据访问类:DBServiceQualityInfo
        /// </summary>
        /// <returns></returns>
        public static IDBServiceQualityInfo GetDBServiceQualityInfo()
        {
            string strClassName = strNameSpaceName + "." + db + ".DBServiceQualityInfo";
            return (IDBServiceQualityInfo)Assembly.Load(strAssemblyName).CreateInstance(strClassName);
        }
       

       
    }
}
