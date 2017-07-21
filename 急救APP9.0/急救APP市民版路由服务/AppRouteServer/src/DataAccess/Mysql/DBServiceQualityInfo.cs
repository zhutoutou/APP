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
    class DBServiceQualityInfo : IDBServiceQualityInfo
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(ServiceQualityInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into servicequalityinfo(");
            strSql.Append("LSH,CASEID,CCCC,CLID,TIMETAKEN,MONEY,KM,ADDTIME)");
            strSql.Append(" values (");
            strSql.Append("@LSH,@CASEID,@CCCC,@CLID,@TIMETAKEN,@MONEY,@KM,@ADDTIME)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@LSH",  DBOpHelper.GetString(model.LSH)),
					new MySqlParameter("@CASEID",  DBOpHelper.GetString(model.CASEID)),
					new MySqlParameter("@CCCC", DBOpHelper.GetNumber( model.CCCC)),
					new MySqlParameter("@CLID", DBOpHelper.GetString( model.CLID)),
					new MySqlParameter("@TIMETAKEN",  DBOpHelper.GetNumber(model.TIMETAKEN)),
					new MySqlParameter("@MONEY",DBOpHelper.GetNumber( model.MONEY)),
					new MySqlParameter("@KM", DBOpHelper.GetNumber( model.KM)),
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
