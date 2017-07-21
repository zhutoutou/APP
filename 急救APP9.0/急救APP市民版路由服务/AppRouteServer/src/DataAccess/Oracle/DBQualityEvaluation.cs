using System;
using System.Data;
using System.Text;
using System.Data.OracleClient;
using System.Collections.Generic;
using System.Collections;
using ZIT.AppRouteServer.DataAccess;//Please add references
using ZIT.AppRouteServer.Model;
using ZIT.AppRouteServer.Utility;


namespace ZIT.AppRouteServer.DataAccess.Oracle
{
	/// <summary>
	/// 数据访问类:QualityEvaluation
	/// </summary>
    class DBQualityEvaluation : IDBQualityEvaluation
	{
		public DBQualityEvaluation()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
        public bool Add(QualityEvaluation model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into QualityEvaluation(");
            strSql.Append("LSH,QUALITYCOMMENT,REASON,CASEID,COMMENTTIME,ADDTIME)");
            strSql.Append(" values (");
            strSql.Append(":LSH,:QUALITYCOMMENT,:REASON,:CASEID,:COMMENTTIME,:ADDTIME)");
            OracleParameter[] parameters = {
					new OracleParameter(":LSH", DBOpHelper.GetString(model.LSH)),
					new OracleParameter(":QUALITYCOMMENT",  DBOpHelper.GetString(model.QUALITYCOMMENT)),
					new OracleParameter(":REASON",  DBOpHelper.GetString(model.REASON)),
					new OracleParameter(":CASEID",  DBOpHelper.GetString(model.CASEID)),
					new OracleParameter(":COMMENTTIME",  DBOpHelper.GetDateTime(model.COMMENTTIME)),
                    new OracleParameter(":ADDTIME",  DBOpHelper.GetDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")))};

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
        /// 根据CASEID 获取 单位编号
        /// </summary>
        /// <param name="CaseId"></param>
        /// <returns></returns>
        public string GetUnitCodeByCaseId(string CaseId)
        {
            string UnitCode = "";
            DBAppCallInfo dba = new DBAppCallInfo();
            DBRouter router = new DBRouter();
            AppCallInfo aci = dba.GetAppCallInfoByCaseId(CaseId);
            if (aci != null)
            {
                UnitCode = router.GetUnitCodeByRegistCity(aci.PROVINCE, aci.CITY, aci.AREA);
            }
            return UnitCode;
        }

        /// <summary>
        /// 获取最新的质量评价信息
        /// </summary>
        /// <returns></returns>
        public List<QualityEvaluation> GetNewQualityEvaluation()
        {
            List<QualityEvaluation> list = new List<QualityEvaluation>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select LSH,QUALITYCOMMENT,REASON,CASEID,COMMENTTIME ");
            strSql.Append("  from (select * from  QualityEvaluation where (sysdate-COMMENTTIME)*24*60*60<3600 and READFLAG=0 order by COMMENTTIME)   where rownum<=100 ");
            OracleParameter[] parameters = {
			};

            DataSet ds = DbHelperOra.Query(SysParameters.DBConnectString, strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ArrayList arrayList = new ArrayList();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    QualityEvaluation model = DataRowToModel(ds.Tables[0].Rows[i]);
                    list.Add(model);
                    arrayList.Add(string.Format("update QualityEvaluation set READFLAG=1,READTIME=sysdate where CASEID='{0}'", model.CASEID));
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
		public ZIT.AppRouteServer.Model.QualityEvaluation GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select LSH,QUALITYCOMMENT,REASON,CASEID,COMMENTTIME from QualityEvaluation ");
			strSql.Append(" where ");
			OracleParameter[] parameters = {
			};

			ZIT.AppRouteServer.Model.QualityEvaluation model=new ZIT.AppRouteServer.Model.QualityEvaluation();
			DataSet ds=DbHelperOra.Query("",strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ZIT.AppRouteServer.Model.QualityEvaluation DataRowToModel(DataRow row)
		{
			ZIT.AppRouteServer.Model.QualityEvaluation model=new ZIT.AppRouteServer.Model.QualityEvaluation();
			if (row != null)
			{
				if(row["LSH"]!=null)
				{
					model.LSH=row["LSH"].ToString();
				}
				if(row["QUALITYCOMMENT"]!=null)
				{
					model.QUALITYCOMMENT=row["QUALITYCOMMENT"].ToString();
				}
				if(row["REASON"]!=null)
				{
					model.REASON=row["REASON"].ToString();
				}
				if(row["CASEID"]!=null)
				{
					model.CASEID=row["CASEID"].ToString();
				}
				if(row["COMMENTTIME"]!=null && row["COMMENTTIME"].ToString()!="")
				{
					model.COMMENTTIME=DateTime.Parse(row["COMMENTTIME"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select LSH,QUALITYCOMMENT,REASON,CASEID,COMMENTTIME ");
			strSql.Append(" FROM QualityEvaluation ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperOra.Query("",strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM QualityEvaluation ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperOra.GetSingle("",strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T. desc");
			}
			strSql.Append(")AS Row, T.*  from QualityEvaluation T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperOra.Query("",strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			OracleParameter[] parameters = {
					new OracleParameter(":tblName", OracleType.VarChar, 255),
					new OracleParameter(":fldName", OracleType.VarChar, 255),
					new OracleParameter(":PageSize", OracleType.Number),
					new OracleParameter(":PageIndex", OracleType.Number),
					new OracleParameter(":IsReCount", OracleType.Clob),
					new OracleParameter(":OrderType", OracleType.Clob),
					new OracleParameter(":strWhere", OracleType.VarChar,1000),
					};
			parameters[0].Value = "QualityEvaluation";
			parameters[1].Value = "";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperOra.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

