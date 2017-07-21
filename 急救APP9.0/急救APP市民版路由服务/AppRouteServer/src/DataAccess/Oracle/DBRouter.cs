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
	/// 数据访问类:Router
	/// </summary>
    class DBRouter : IDBRouter
	{
		public DBRouter()
		{}
		#region  BasicMethod

        /// <summary>
        /// 根据注册的城市名获取单位行政编码
        /// </summary>
        /// <param name="province"></param>
        /// <param name="city"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        public string GetUnitCodeByRegistCity(string province, string city, string area)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select XZBM FROM Router ");
            strSql.Append(" where PROVINCE ='" + province + "' AND CITY='" + city + "' AND AREA='" + area + "' AND ISVALID=1 ");

            object obj = DbHelperOra.GetSingle(SysParameters.DBConnectString, strSql.ToString());
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }

        /// <summary>
        /// 根据单位行政编码获取单位名称
        /// </summary>
        /// <param name="UnitCode"></param>
        /// <returns></returns>
        public string GetUnitNameByUnitCode(string UnitCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UNITNAME FROM Router ");
            strSql.Append(" where XZBM ='" + UnitCode + "' AND ISVALID =1");

            object obj = DbHelperOra.GetSingle(SysParameters.DBConnectString, strSql.ToString());
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Router model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Router(");
			strSql.Append("ID,XZBM,PROVINCE,CITY,AREA,ISVALID,ADDTIME,OPERATOR)");
			strSql.Append(" values (");
			strSql.Append(":ID,:XZBM,:PROVINCE,:CITY,:AREA,:ISVALID,:ADDTIME,:OPERATOR)");
			OracleParameter[] parameters = {
					new OracleParameter(":ID", OracleType.Number,4),
					new OracleParameter(":XZBM", OracleType.NVarChar),
					new OracleParameter(":PROVINCE", OracleType.NVarChar),
					new OracleParameter(":CITY", OracleType.NVarChar),
					new OracleParameter(":AREA", OracleType.NVarChar),
					new OracleParameter(":ISVALID", OracleType.Number,4),
					new OracleParameter(":ADDTIME", OracleType.DateTime),
					new OracleParameter(":OPERATOR", OracleType.NVarChar)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.XZBM;
			parameters[2].Value = model.PROVINCE;
			parameters[3].Value = model.CITY;
			parameters[4].Value = model.AREA;
			parameters[5].Value = model.ISVALID;
			parameters[6].Value = model.ADDTIME;
			parameters[7].Value = model.OPERATOR;

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
		/// 更新一条数据
		/// </summary>
		public bool Update(ZIT.AppRouteServer.Model.Router model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Router set ");
			strSql.Append("ID=:ID,");
			strSql.Append("XZBM=:XZBM,");
			strSql.Append("PROVINCE=:PROVINCE,");
			strSql.Append("CITY=:CITY,");
			strSql.Append("AREA=:AREA,");
			strSql.Append("ISVALID=:ISVALID,");
			strSql.Append("ADDTIME=:ADDTIME,");
			strSql.Append("OPERATOR=:OPERATOR");
			strSql.Append(" where ");
			OracleParameter[] parameters = {
					new OracleParameter(":ID", OracleType.Number,4),
					new OracleParameter(":XZBM", OracleType.NVarChar),
					new OracleParameter(":PROVINCE", OracleType.NVarChar),
					new OracleParameter(":CITY", OracleType.NVarChar),
					new OracleParameter(":AREA", OracleType.NVarChar),
					new OracleParameter(":ISVALID", OracleType.Number,4),
					new OracleParameter(":ADDTIME", OracleType.DateTime),
					new OracleParameter(":OPERATOR", OracleType.NVarChar)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.XZBM;
			parameters[2].Value = model.PROVINCE;
			parameters[3].Value = model.CITY;
			parameters[4].Value = model.AREA;
			parameters[5].Value = model.ISVALID;
			parameters[6].Value = model.ADDTIME;
			parameters[7].Value = model.OPERATOR;

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
			strSql.Append("delete from Router ");
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
		public ZIT.AppRouteServer.Model.Router GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,XZBM,PROVINCE,CITY,AREA,ISVALID,ADDTIME,OPERATOR from Router ");
			strSql.Append(" where ");
			OracleParameter[] parameters = {
			};

			ZIT.AppRouteServer.Model.Router model=new ZIT.AppRouteServer.Model.Router();
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
		public ZIT.AppRouteServer.Model.Router DataRowToModel(DataRow row)
		{
			ZIT.AppRouteServer.Model.Router model=new ZIT.AppRouteServer.Model.Router();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["XZBM"]!=null)
				{
					model.XZBM=row["XZBM"].ToString();
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
				if(row["ISVALID"]!=null && row["ISVALID"].ToString()!="")
				{
					model.ISVALID=int.Parse(row["ISVALID"].ToString());
				}
				if(row["ADDTIME"]!=null && row["ADDTIME"].ToString()!="")
				{
					model.ADDTIME=DateTime.Parse(row["ADDTIME"].ToString());
				}
				if(row["OPERATOR"]!=null)
				{
					model.OPERATOR=row["OPERATOR"].ToString();
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
			strSql.Append("select ID,XZBM,PROVINCE,CITY,AREA,ISVALID,ADDTIME,OPERATOR ");
			strSql.Append(" FROM Router ");
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
			strSql.Append("select count(1) FROM Router ");
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
			strSql.Append(")AS Row, T.*  from Router T ");
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
			parameters[0].Value = "Router";
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

