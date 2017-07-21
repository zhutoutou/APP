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
	/// 数据访问类:SendCarInfo
	/// </summary>
    class DBSendCarInfo : IDBSendCarInfo
	{
		public DBSendCarInfo()
		{}
		#region  BasicMethod

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(SendCarInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SendCarInfo(");
            strSql.Append("CASEID,LSH,CCCC,CLID,CPH,CCSJ,SSDW,DRIVERPHONE,DRIVERNAME,DOCTORPHONE,DOCTORNAME,ADDTIME)");
			strSql.Append(" values (");
            strSql.Append(":CASEID,:LSH,:CCCC,:CLID,:CPH,:CCSJ,:SSDW,:DRIVERPHONE,:DRIVERNAME,:DOCTORPHONE,:DOCTORNAME,:ADDTIME)");
			OracleParameter[] parameters = {
					new OracleParameter(":CASEID", DBOpHelper.GetString(model.CASEID)),
					new OracleParameter(":LSH", DBOpHelper.GetString(model.LSH)),
					new OracleParameter(":CCCC",DBOpHelper.GetNumber(model.CCCC)),
					new OracleParameter(":CLID",DBOpHelper.GetString(model.CLID)),
					new OracleParameter(":CPH",DBOpHelper.GetString(model.CPH)),
					new OracleParameter(":CCSJ", DBOpHelper.GetDateTime(model.CCSJ)),
					new OracleParameter(":SSDW", DBOpHelper.GetString(model.SSDW)),
					new OracleParameter(":DRIVERPHONE", DBOpHelper.GetString(model.DRIVERPHONE)),
					new OracleParameter(":DRIVERNAME", DBOpHelper.GetString(model.DRIVERNAME)),
					new OracleParameter(":DOCTORPHONE", DBOpHelper.GetString(model.DOCTORPHONE)),
					new OracleParameter(":DOCTORNAME", DBOpHelper.GetString(model.DOCTORNAME)),
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
		/// 得到一个对象实体
		/// </summary>
		public ZIT.AppRouteServer.Model.SendCarInfo GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,CASEID,LSH,CCCC,CLID,CPH,CCSJ,SSDW,DRIVERPHONE,DRIVERNAME,DOCTORPHONE,DOCTORNAME from SendCarInfo ");
			strSql.Append(" where ");
			OracleParameter[] parameters = {
			};

			ZIT.AppRouteServer.Model.SendCarInfo model=new ZIT.AppRouteServer.Model.SendCarInfo();
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
		public ZIT.AppRouteServer.Model.SendCarInfo DataRowToModel(DataRow row)
		{
			ZIT.AppRouteServer.Model.SendCarInfo model=new ZIT.AppRouteServer.Model.SendCarInfo();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["CASEID"]!=null)
				{
					model.CASEID=row["CASEID"].ToString();
				}
				if(row["LSH"]!=null)
				{
					model.LSH=row["LSH"].ToString();
				}
				if(row["CCCC"]!=null && row["CCCC"].ToString()!="")
				{
					model.CCCC=int.Parse(row["CCCC"].ToString());
				}
				if(row["CLID"]!=null)
				{
					model.CLID=row["CLID"].ToString();
				}
				if(row["CPH"]!=null)
				{
					model.CPH=row["CPH"].ToString();
				}
				if(row["CCSJ"]!=null && row["CCSJ"].ToString()!="")
				{
					model.CCSJ=DateTime.Parse(row["CCSJ"].ToString());
				}
				if(row["SSDW"]!=null)
				{
					model.SSDW=row["SSDW"].ToString();
				}
				if(row["DRIVERPHONE"]!=null)
				{
					model.DRIVERPHONE=row["DRIVERPHONE"].ToString();
				}
				if(row["DRIVERNAME"]!=null)
				{
					model.DRIVERNAME=row["DRIVERNAME"].ToString();
				}
				if(row["DOCTORPHONE"]!=null)
				{
					model.DOCTORPHONE=row["DOCTORPHONE"].ToString();
				}
				if(row["DOCTORNAME"]!=null)
				{
					model.DOCTORNAME=row["DOCTORNAME"].ToString();
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
			strSql.Append("select ID,CASEID,LSH,CCCC,CLID,CPH,CCSJ,SSDW,DRIVERPHONE,DRIVERNAME,DOCTORPHONE,DOCTORNAME ");
			strSql.Append(" FROM SendCarInfo ");
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
			strSql.Append("select count(1) FROM SendCarInfo ");
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
			strSql.Append(")AS Row, T.*  from SendCarInfo T ");
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
			parameters[0].Value = "SendCarInfo";
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

