using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data.OracleClient;
using ZIT.AppRouteServer.DataAccess;
using ZIT.AppRouteServer.Model;
using ZIT.AppRouteServer.Utility;

namespace ZIT.AppRouteServer.DataAccess.Oracle
{
	/// <summary>
    /// 数据访问类:DBAppCallInfo
	/// </summary>
    class DBAppCallInfo : IDBAppCallInfo
	{
		public DBAppCallInfo()
		{}
		#region  BasicMethod

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(AppCallInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AppCallInfo(");
			strSql.Append("CASEID,PHONE,PROVINCE,CITY,AREA,CALLTIME,ISSELF,NAME,SEX,BRITHDAY,HEIGHT,WEIGHT,IDENTITYCARD,JD,WD,ADDRESS,MEDICALHISTORY,CONTACTWAY1,CONTACTWAY2,CONTACTWAY3,MEDICALINSURANCECARD,ADDTIME)");
			strSql.Append(" values (");
			strSql.Append(":CASEID,:PHONE,:PROVINCE,:CITY,:AREA,:CALLTIME,:ISSELF,:NAME,:SEX,:BRITHDAY,:HEIGHT,:WEIGHT,:IDENTITYCARD,:JD,:WD,:ADDRESS,:MEDICALHISTORY,:CONTACTWAY1,:CONTACTWAY2,:CONTACTWAY3,:MEDICALINSURANCECARD,:ADDTIME)");
            OracleParameter[] parameters = {
					new OracleParameter(":CASEID", DBOpHelper.GetString(model.CASEID)),
					new OracleParameter(":PHONE", DBOpHelper.GetString(model.PHONE)),
					new OracleParameter(":PROVINCE", DBOpHelper.GetString(model.PROVINCE)),
					new OracleParameter(":CITY", DBOpHelper.GetString(model.CITY)),
					new OracleParameter(":AREA", DBOpHelper.GetString(model.AREA)),
					new OracleParameter(":CALLTIME", DBOpHelper.GetDateTime(model.CALLTIME.ToString())),
					new OracleParameter(":ISSELF", DBOpHelper.GetNumber(model.ISSELF.ToString())),
					new OracleParameter(":NAME",DBOpHelper.GetString(model.NAME)),
					new OracleParameter(":SEX", DBOpHelper.GetNumber(model.SEX.ToString())),
					new OracleParameter(":BRITHDAY", DBOpHelper.GetString(model.BRITHDAY)),
					new OracleParameter(":HEIGHT", DBOpHelper.GetNumber(model.HEIGHT.ToString())),
					new OracleParameter(":WEIGHT", DBOpHelper.GetNumber(model.WEIGHT.ToString())),
					new OracleParameter(":IDENTITYCARD", DBOpHelper.GetString(model.IDENTITYCARD)),
					new OracleParameter(":JD", DBOpHelper.GetString(model.JD)),
					new OracleParameter(":WD",DBOpHelper.GetString(model.WD)),
					new OracleParameter(":ADDRESS",DBOpHelper.GetString(model.ADDRESS)),
					new OracleParameter(":MEDICALHISTORY", DBOpHelper.GetString(model.MEDICALHISTORY)),
					new OracleParameter(":CONTACTWAY1", DBOpHelper.GetString(model.CONTACTWAY1)),
					new OracleParameter(":CONTACTWAY2", DBOpHelper.GetString(model.CONTACTWAY2)),
					new OracleParameter(":CONTACTWAY3", DBOpHelper.GetString(model.CONTACTWAY3)),
					new OracleParameter(":MEDICALINSURANCECARD",DBOpHelper.GetString(model.MEDICALINSURANCECARD)),
					new OracleParameter(":ADDTIME", DBOpHelper.GetDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")))};

            int rows = DbHelperOra.ExecuteSql(SysParameters.DBConnectString, strSql.ToString(), parameters);
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
            strSql.Append("select CASEID,PHONE,PROVINCE,CITY,AREA,CALLTIME,ISSELF,NAME,SEX,BRITHDAY,HEIGHT,WEIGHT,IDENTITYCARD,JD,WD,ADDRESS,MEDICALHISTORY,CONTACTWAY1,CONTACTWAY2,CONTACTWAY3,MEDICALINSURANCECARD,ADDTIME ");
            strSql.Append("  from (select * from  AppCallInfo where (sysdate-CALLTIME)*24*60*60<300 and READFLAG=0 order by CALLTIME)   where rownum<=100 ");
            OracleParameter[] parameters = {
			};

            DataSet ds = DbHelperOra.Query(SysParameters.DBConnectString, strSql.ToString(), parameters);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                ArrayList arrayList = new ArrayList();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    AppCallInfo model = DataRowToModel(ds.Tables[0].Rows[i]);
                    list.Add(model);
                    arrayList.Add(string.Format("update AppCallInfo set READFLAG=1,READTIME=sysdate where CASEID='{0}'", model.CASEID));
                }
                //更新读取标志
                ExcuteSqlList(arrayList);
            }
            return list;
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
            OracleParameter[] parameters = {
			};
            DataSet ds = DbHelperOra.Query(SysParameters.DBConnectString, strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model = DataRowToModel(ds.Tables[0].Rows[0]);
            }
            return model;
        }

        /// <summary>
        /// 批量执行SQL语句
        /// </summary>
        /// <param name="ast"></param>
        private void ExcuteSqlList(ArrayList ast)
        {
            if (ast != null && ast.Count > 0)
            {
                DbHelperOra.ExecuteSqlTran(SysParameters.DBConnectString, ast);
            }
        }


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public AppCallInfo DataRowToModel(DataRow row)
		{
			AppCallInfo model=new AppCallInfo();
			if (row != null)
			{
				if(row["CASEID"]!=null)
				{
					model.CASEID=row["CASEID"].ToString();
				}
				if(row["PHONE"]!=null)
				{
					model.PHONE=row["PHONE"].ToString();
				}
				if(row["PROVINCE"]!=null)
				{
					model.PROVINCE=row["PROVINCE"].ToString();
				}
				if(row["CITY"]!=null)
				{
					model.CITY=row["CITY"].ToString();
				}
				if(row["AREA"]!=null)
				{
					model.AREA=row["AREA"].ToString();
				}
				if(row["CALLTIME"]!=null && row["CALLTIME"].ToString()!="")
				{
					model.CALLTIME=DateTime.Parse(row["CALLTIME"].ToString());
				}
				if(row["ISSELF"]!=null && row["ISSELF"].ToString()!="")
				{
                    model.ISSELF = int.Parse(row["ISSELF"].ToString());
				}
				if(row["NAME"]!=null)
				{
					model.NAME=row["NAME"].ToString();
				}
				if(row["SEX"]!=null && row["SEX"].ToString()!="")
				{
                    model.SEX = int.Parse(row["SEX"].ToString());
				}
				if(row["BRITHDAY"]!=null)
				{
					model.BRITHDAY=row["BRITHDAY"].ToString();
				}
				if(row["HEIGHT"]!=null && row["HEIGHT"].ToString()!="")
				{
                    model.HEIGHT = int.Parse(row["HEIGHT"].ToString());
				}
				if(row["WEIGHT"]!=null && row["WEIGHT"].ToString()!="")
				{
                    model.WEIGHT = decimal.Parse(row["WEIGHT"].ToString());
				}
				if(row["IDENTITYCARD"]!=null)
				{
					model.IDENTITYCARD=row["IDENTITYCARD"].ToString();
				}
				if(row["JD"]!=null)
				{
					model.JD=row["JD"].ToString();
				}
				if(row["WD"]!=null)
				{
					model.WD=row["WD"].ToString();
				}
				if(row["ADDRESS"]!=null)
				{
					model.ADDRESS=row["ADDRESS"].ToString();
				}
				if(row["MEDICALHISTORY"]!=null)
				{
					model.MEDICALHISTORY=row["MEDICALHISTORY"].ToString();
				}
				if(row["CONTACTWAY1"]!=null)
				{
					model.CONTACTWAY1=row["CONTACTWAY1"].ToString();
				}
				if(row["CONTACTWAY2"]!=null)
				{
					model.CONTACTWAY2=row["CONTACTWAY2"].ToString();
				}
				if(row["CONTACTWAY3"]!=null)
				{
					model.CONTACTWAY3=row["CONTACTWAY3"].ToString();
				}
				if(row["MEDICALINSURANCECARD"]!=null)
				{
					model.MEDICALINSURANCECARD=row["MEDICALINSURANCECARD"].ToString();
				}
				if(row["ADDTIME"]!=null && row["ADDTIME"].ToString()!="")
				{
					model.ADDTIME=DateTime.Parse(row["ADDTIME"].ToString());
				}
			}
			return model;
		}

		#endregion  BasicMethod
	}
}

