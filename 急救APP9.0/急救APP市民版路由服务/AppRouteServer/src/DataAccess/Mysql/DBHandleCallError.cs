using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZIT.AppRouteServer.DataAccess;
using ZIT.AppRouteServer.Model;
using ZIT.AppRouteServer.Utility;
using MySql.Data.MySqlClient;

namespace ZIT.AppRouteServer.DataAccess.Mysql
{
    class DBHandleCallError:IDBHandleCallError 
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(HandleCallError model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into handlecallerror(");
            strSql.Append("CASEID,ERRORCODE,ERRORMSG,ERRORTIME,ADDTIME)");
            strSql.Append(" values (");
            strSql.Append("@CASEID,@ERRORCODE,@ERRORMSG,@ERRORTIME,@ADDTIME)");
            MySqlParameter[] parameters = {
                new MySqlParameter("@CASEID", DBOpHelper.GetString(model.CASEID)),
                new MySqlParameter("@ERRORCODE", DBOpHelper.GetString(model.ERRORCODE)),
                new MySqlParameter("@ERRORMSG", DBOpHelper.GetString(model.ERRORMSG)),
                new MySqlParameter("@ERRORTIME", DBOpHelper.GetDateTime(model.ERRORTIME)),
                new MySqlParameter("@ADDTIME", DBOpHelper.GetDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")))};

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
