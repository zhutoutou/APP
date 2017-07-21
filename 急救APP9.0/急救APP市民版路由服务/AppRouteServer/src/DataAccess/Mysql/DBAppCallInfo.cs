using System;
using System.Collections;
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
    class DBAppCallInfo : IDBAppCallInfo
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(AppCallInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into appcallinfo(");
            strSql.Append("CASEID,PHONE,PROVINCE,CITY,AREA,CALLTIME,ISSELF,NAME,SEX,BRITHDAY,HEIGHT,WEIGHT,IDENTITYCARD,JD,WD,ADDRESS,MEDICALHISTORY,CONTACTWAY1,CONTACTWAY2,CONTACTWAY3,MEDICALINSURANCECARD,ADDTIME)");
            strSql.Append(" values (");
            strSql.Append("@CASEID,@PHONE,@PROVINCE,@CITY,@AREA,@CALLTIME,@ISSELF,@NAME,@SEX,@BRITHDAY,@HEIGHT,@WEIGHT,@IDENTITYCARD,@JD,@WD,@ADDRESS,@MEDICALHISTORY,@CONTACTWAY1,@CONTACTWAY2,@CONTACTWAY3,@MEDICALINSURANCECARD,@ADDTIME)");
            MySqlParameter[] parameters = {        
            	    new MySqlParameter("@CASEID", DBOpHelper.GetString(model.CASEID)),
					new MySqlParameter("@PHONE", DBOpHelper.GetString(model.PHONE)),
					new MySqlParameter("@PROVINCE", DBOpHelper.GetString(model.PROVINCE)),
					new MySqlParameter("@CITY", DBOpHelper.GetString(model.CITY)),
					new MySqlParameter("@AREA", DBOpHelper.GetString(model.AREA)),
					new MySqlParameter("@CALLTIME", DBOpHelper.GetDateTime(model.CALLTIME.ToString())),
					new MySqlParameter("@ISSELF", DBOpHelper.GetNumber(model.ISSELF.ToString())),
					new MySqlParameter("@NAME",DBOpHelper.GetString(model.NAME)),
					new MySqlParameter("@SEX", DBOpHelper.GetNumber(model.SEX.ToString())),
					new MySqlParameter("@BRITHDAY", DBOpHelper.GetString(model.BRITHDAY)),
					new MySqlParameter("@HEIGHT", DBOpHelper.GetNumber(model.HEIGHT.ToString())),
					new MySqlParameter("@WEIGHT", DBOpHelper.GetNumber(model.WEIGHT.ToString())),
					new MySqlParameter("@IDENTITYCARD", DBOpHelper.GetString(model.IDENTITYCARD)),
					new MySqlParameter("@JD", DBOpHelper.GetString(model.JD)),
					new MySqlParameter("@WD",DBOpHelper.GetString(model.WD)),
					new MySqlParameter("@ADDRESS",DBOpHelper.GetString(model.ADDRESS)),
					new MySqlParameter("@MEDICALHISTORY", DBOpHelper.GetString(model.MEDICALHISTORY)),
					new MySqlParameter("@CONTACTWAY1", DBOpHelper.GetString(model.CONTACTWAY1)),
					new MySqlParameter("@CONTACTWAY2", DBOpHelper.GetString(model.CONTACTWAY2)),
					new MySqlParameter("@CONTACTWAY3", DBOpHelper.GetString(model.CONTACTWAY3)),
					new MySqlParameter("@MEDICALINSURANCECARD",DBOpHelper.GetString(model.MEDICALINSURANCECARD)),
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

        /// <summary>
        /// 获取最新的呼救信息
        /// </summary>
        /// <returns></returns>
        public List<AppCallInfo> GetNewCallInfo()
        {
            List<AppCallInfo> list = new List<AppCallInfo>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from AppCallInfo where  TIMESTAMPDIFF(SECOND,CALLTIME,now()) <300  and READFLAG=0 order by CALLTIME limit 100 ");
            MySqlParameter[] parameters = {
			};

            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                List<string> arrayList = new List<string>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    AppCallInfo model = DataRowToModel(ds.Tables[0].Rows[i]);
                    list.Add(model);
                    arrayList.Add(string.Format("update AppCallInfo set READFLAG=1,READTIME=now() where CASEID='{0}'", model.CASEID));
                }
                //更新读取标志
                ExcuteSqlList(arrayList);
            }
            return list;
        }

        /// <summary>
        /// 批量执行SQL语句
        /// </summary>
        /// <param name="ast"></param>
        private void ExcuteSqlList(List<string> ast)
        {
            if (ast != null && ast.Count > 0)
            {
                DbHelperMySQL.ExecuteSqlTran(ast);
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        private AppCallInfo DataRowToModel(DataRow row)
        {
            AppCallInfo model = new AppCallInfo();
            if (row != null)
            {
                if (row["CASEID"] != null)
                {
                    model.CASEID = row["CASEID"].ToString();
                }
                if (row["PHONE"] != null)
                {
                    model.PHONE = row["PHONE"].ToString();
                }
                if (row["PROVINCE"] != null)
                {
                    model.PROVINCE = row["PROVINCE"].ToString();
                }
                if (row["CITY"] != null)
                {
                    model.CITY = row["CITY"].ToString();
                }
                if (row["AREA"] != null)
                {
                    model.AREA = row["AREA"].ToString();
                }
                if (row["CALLTIME"] != null && row["CALLTIME"].ToString() != "")
                {
                    model.CALLTIME = DateTime.Parse(row["CALLTIME"].ToString());
                }
                if (row["ISSELF"] != null && row["ISSELF"].ToString() != "")
                {
                    model.ISSELF = int.Parse(row["ISSELF"].ToString());
                }
                if (row["NAME"] != null)
                {
                    model.NAME = row["NAME"].ToString();
                }
                if (row["SEX"] != null && row["SEX"].ToString() != "")
                {
                    model.SEX = int.Parse(row["SEX"].ToString());
                }
                if (row["BRITHDAY"] != null)
                {
                    model.BRITHDAY = row["BRITHDAY"].ToString();
                }
                if (row["HEIGHT"] != null && row["HEIGHT"].ToString() != "")
                {
                    model.HEIGHT = int.Parse(row["HEIGHT"].ToString());
                }
                if (row["WEIGHT"] != null && row["WEIGHT"].ToString() != "")
                {
                    model.WEIGHT = decimal.Parse(row["WEIGHT"].ToString());
                }
                if (row["IDENTITYCARD"] != null)
                {
                    model.IDENTITYCARD = row["IDENTITYCARD"].ToString();
                }
                if (row["JD"] != null)
                {
                    model.JD = row["JD"].ToString();
                }
                if (row["WD"] != null)
                {
                    model.WD = row["WD"].ToString();
                }
                if (row["ADDRESS"] != null)
                {
                    model.ADDRESS = row["ADDRESS"].ToString();
                }
                if (row["MEDICALHISTORY"] != null)
                {
                    model.MEDICALHISTORY = row["MEDICALHISTORY"].ToString();
                }
                if (row["CONTACTWAY1"] != null)
                {
                    model.CONTACTWAY1 = row["CONTACTWAY1"].ToString();
                }
                if (row["CONTACTWAY2"] != null)
                {
                    model.CONTACTWAY2 = row["CONTACTWAY2"].ToString();
                }
                if (row["CONTACTWAY3"] != null)
                {
                    model.CONTACTWAY3 = row["CONTACTWAY3"].ToString();
                }
                if (row["MEDICALINSURANCECARD"] != null)
                {
                    model.MEDICALINSURANCECARD = row["MEDICALINSURANCECARD"].ToString();
                }
                if (row["ADDTIME"] != null && row["ADDTIME"].ToString() != "")
                {
                    model.ADDTIME = DateTime.Parse(row["ADDTIME"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获取最新的呼救信息
        /// </summary>
        /// <returns></returns>
        public AppCallInfo GetAppCallInfoByCaseId(string strCaseId)
        {
            AppCallInfo model = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CASEID,PHONE,PROVINCE,CITY,AREA,CALLTIME,ISSELF,NAME,SEX,BRITHDAY,HEIGHT,WEIGHT,IDENTITYCARD,JD,WD,ADDRESS,MEDICALHISTORY,CONTACTWAY1,CONTACTWAY2,CONTACTWAY3,MEDICALINSURANCECARD,ADDTIME ");
            strSql.Append("  from AppCallInfo where CASEID = '" + strCaseId + "'");
            MySqlParameter[] parameters = {
			};
            DataSet ds = DbHelperMySQL.Query( strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model = DataRowToModel(ds.Tables[0].Rows[0]);
            }
            return model;
        }
    }
}
