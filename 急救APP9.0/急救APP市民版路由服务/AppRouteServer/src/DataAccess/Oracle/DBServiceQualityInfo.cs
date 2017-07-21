using System;
using System.Data;
using System.Text;
using System.Data.OracleClient;
using ZIT.AppRouteServer.DataAccess;//Please add references
using ZIT.AppRouteServer.Model;
using ZIT.AppRouteServer.Utility;

namespace ZIT.AppRouteServer.DataAccess.Oracle
{
	/// <summary>
	/// 数据访问类:ServiceQualityInfo
	/// </summary>
    class DBServiceQualityInfo : IDBServiceQualityInfo
	{
		public DBServiceQualityInfo()
		{}
		#region  BasicMethod

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ZIT.AppRouteServer.Model.ServiceQualityInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ServiceQualityInfo(");
            strSql.Append("LSH,CASEID,CCCC,CLID,TIMETAKEN,MONEY,KM,ADDTIME)");
			strSql.Append(" values (");
            strSql.Append(":LSH,:CASEID,:CCCC,:CLID,:TIMETAKEN,:MONEY,:KM,:ADDTIME)");
			OracleParameter[] parameters = {
					new OracleParameter(":LSH",  DBOpHelper.GetString(model.LSH)),
					new OracleParameter(":CASEID",  DBOpHelper.GetString(model.CASEID)),
					new OracleParameter(":CCCC", DBOpHelper.GetNumber( model.CCCC)),
					new OracleParameter(":CLID", DBOpHelper.GetString( model.CLID)),
					new OracleParameter(":TIMETAKEN",  DBOpHelper.GetNumber(model.TIMETAKEN)),
					new OracleParameter(":MONEY",DBOpHelper.GetNumber( model.MONEY)),
					new OracleParameter(":KM", DBOpHelper.GetNumber( model.KM)),
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
		/// 更新一条数据
		/// </summary>
		public bool Update(ZIT.AppRouteServer.Model.ServiceQualityInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ServiceQualityInfo set ");
			strSql.Append("LSH=:LSH,");
			strSql.Append("CASEID=:CASEID,");
			strSql.Append("CCCC=:CCCC,");
			strSql.Append("CLID=:CLID,");
			strSql.Append("TIMETAKEN=:TIMETAKEN,");
			strSql.Append("MONEY=:MONEY,");
			strSql.Append("KM=:KM");
			strSql.Append(" where ");
			OracleParameter[] parameters = {
					new OracleParameter(":LSH", OracleType.VarChar,19),
					new OracleParameter(":CASEID", OracleType.VarChar,50),
					new OracleParameter(":CCCC", OracleType.Number,2),
					new OracleParameter(":CLID", OracleType.VarChar,10),
					new OracleParameter(":TIMETAKEN", OracleType.Number,4),
					new OracleParameter(":MONEY", OracleType.Number,4),
					new OracleParameter(":KM", OracleType.Number,4)};
			parameters[0].Value = model.LSH;
			parameters[1].Value = model.CASEID;
			parameters[2].Value = model.CCCC;
			parameters[3].Value = model.CLID;
			parameters[4].Value = model.TIMETAKEN;
			parameters[5].Value = model.MONEY;
			parameters[6].Value = model.KM;

			int rows=DbHelperOra.ExecuteSql("",strSql.ToString(),parameters);
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
		/// 删除一条数据
		/// </summary>
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ServiceQualityInfo ");
			strSql.Append(" where ");
			OracleParameter[] parameters = {
			};

			int rows=DbHelperOra.ExecuteSql("",strSql.ToString(),parameters);
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
		/// 得到一个对象实体
		/// </summary>
		public ZIT.AppRouteServer.Model.ServiceQualityInfo GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select LSH,CASEID,CCCC,CLID,TIMETAKEN,MONEY,KM from ServiceQualityInfo ");
			strSql.Append(" where ");
			OracleParameter[] parameters = {
			};

			ZIT.AppRouteServer.Model.ServiceQualityInfo model=new ZIT.AppRouteServer.Model.ServiceQualityInfo();
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
		public ZIT.AppRouteServer.Model.ServiceQualityInfo DataRowToModel(DataRow row)
		{
			ZIT.AppRouteServer.Model.ServiceQualityInfo model=new ZIT.AppRouteServer.Model.ServiceQualityInfo();
			if (row != null)
			{
				if(row["LSH"]!=null)
				{
					model.LSH=row["LSH"].ToString();
				}
				if(row["CASEID"]!=null)
				{
					model.CASEID=row["CASEID"].ToString();
				}
				if(row["CCCC"]!=null && row["CCCC"].ToString()!="")
				{
					model.CCCC=int.Parse(row["CCCC"].ToString());
				}
				if(row["CLID"]!=null)
				{
					model.CLID=row["CLID"].ToString();
				}
				if(row["TIMETAKEN"]!=null && row["TIMETAKEN"].ToString()!="")
				{
                    model.TIMETAKEN = int.Parse(row["TIMETAKEN"].ToString());
				}
				if(row["MONEY"]!=null && row["MONEY"].ToString()!="")
				{
                    model.MONEY = int.Parse(row["MONEY"].ToString());
				}
				if(row["KM"]!=null && row["KM"].ToString()!="")
				{
                    model.KM = int.Parse(row["KM"].ToString());
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
			strSql.Append("select LSH,CASEID,CCCC,CLID,TIMETAKEN,MONEY,KM ");
			strSql.Append(" FROM ServiceQualityInfo ");
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
			strSql.Append("select count(1) FROM ServiceQualityInfo ");
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
			strSql.Append(")AS Row, T.*  from ServiceQualityInfo T ");
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
			parameters[0].Value = "ServiceQualityInfo";
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

