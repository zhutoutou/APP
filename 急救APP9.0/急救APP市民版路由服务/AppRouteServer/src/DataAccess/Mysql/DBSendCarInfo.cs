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
    class DBSendCarInfo : IDBSendCarInfo
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SendCarInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sendcarinfo(");
            strSql.Append("CASEID,LSH,CCCC,CLID,CPH,CCSJ,SSDW,DRIVERPHONE,DRIVERNAME,DOCTORPHONE,DOCTORNAME,ADDTIME)");
            strSql.Append(" values (");
            strSql.Append("@CASEID,@LSH,@CCCC,@CLID,@CPH,@CCSJ,@SSDW,@DRIVERPHONE,@DRIVERNAME,@DOCTORPHONE,@DOCTORNAME,@ADDTIME)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@CASEID", DBOpHelper.GetString(model.CASEID)),
					new MySqlParameter("@LSH", DBOpHelper.GetString(model.LSH)),
					new MySqlParameter("@CCCC",DBOpHelper.GetNumber(model.CCCC)),
					new MySqlParameter("@CLID",DBOpHelper.GetString(model.CLID)),
					new MySqlParameter("@CPH",DBOpHelper.GetString(model.CPH)),
					new MySqlParameter("@CCSJ", DBOpHelper.GetDateTime(model.CCSJ)),
					new MySqlParameter("@SSDW", DBOpHelper.GetString(model.SSDW)),
					new MySqlParameter("@DRIVERPHONE", DBOpHelper.GetString(model.DRIVERPHONE)),
					new MySqlParameter("@DRIVERNAME", DBOpHelper.GetString(model.DRIVERNAME)),
					new MySqlParameter("@DOCTORPHONE", DBOpHelper.GetString(model.DOCTORPHONE)),
					new MySqlParameter("@DOCTORNAME", DBOpHelper.GetString(model.DOCTORNAME)),
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
