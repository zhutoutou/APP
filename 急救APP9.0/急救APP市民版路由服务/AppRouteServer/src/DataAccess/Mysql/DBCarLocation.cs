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
    class DBCarLocation : IDBCarLocation
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(CarLocation model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into carlocation(");
            strSql.Append("LSH,CASEID,CCCC,CLID,SJ,JD,WD,SD,FX,ADDTIME)");
            strSql.Append(" values (");
            strSql.Append("@LSH,@CASEID,@CCCC,@CLID,@SJ,@JD,@WD,@SD,@FX,@ADDTIME)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@LSH", DBOpHelper.GetString(model.LSH)),
					new MySqlParameter("@CASEID", DBOpHelper.GetString(model.CASEID)),
					new MySqlParameter("@CCCC", DBOpHelper.GetNumber(model.CCCC)),
					new MySqlParameter("@CLID", DBOpHelper.GetString(model.CLID)),
					new MySqlParameter("@SJ", DBOpHelper.GetDateTime(model.SJ)),
					new MySqlParameter("@JD", DBOpHelper.GetString(model.JD)),
					new MySqlParameter("@WD", DBOpHelper.GetString(model.WD)),
					new MySqlParameter("@SD", DBOpHelper.GetNumber(model.SD)),
					new MySqlParameter("@FX", DBOpHelper.GetString(model.FX)),
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
