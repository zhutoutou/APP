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
    /// 数据访问类:CarState
    /// </summary>
    class DBCarState : IDBCarState
    {
        public DBCarState()
        { }
        #region  BasicMethod



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(CarState model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CarState(");
            strSql.Append("LSH,CASEID,CCCC,CLID,SJ,ZT,TASK_TERMINATION_REASON,ADDTIME)");
            strSql.Append(" values (");
            strSql.Append(":LSH,:CASEID,:CCCC,:CLID,:SJ,:ZT,:TASK_TERMINATION_REASON,:ADDTIME)");
            OracleParameter[] parameters = {
					new OracleParameter(":LSH", DBOpHelper.GetString(model.LSH)),
					new OracleParameter(":CASEID", DBOpHelper.GetString(model.CASEID)),
					new OracleParameter(":CCCC", DBOpHelper.GetNumber(model.CCCC)),
					new OracleParameter(":CLID", DBOpHelper.GetString(model.CLID)),
					new OracleParameter(":SJ", DBOpHelper.GetDateTime(model.SJ)),
					new OracleParameter(":ZT", DBOpHelper.GetString(model.ZT)),
					new OracleParameter(":TASK_TERMINATION_REASON", DBOpHelper.GetString(model.TASK_TERMINATION_REASON)),
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
        public ZIT.AppRouteServer.Model.CarState GetModel()
        {
            //该表无主键信息，请自定义主键/条件字段
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,LSH,CASEID,CCCC,CLID,SJ,ZT,TASK_TERMINATION_REASON from CarState ");
            strSql.Append(" where ");
            OracleParameter[] parameters = {
			};

            ZIT.AppRouteServer.Model.CarState model = new ZIT.AppRouteServer.Model.CarState();
            DataSet ds = DbHelperOra.Query("", strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
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
        public ZIT.AppRouteServer.Model.CarState DataRowToModel(DataRow row)
        {
            ZIT.AppRouteServer.Model.CarState model = new ZIT.AppRouteServer.Model.CarState();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["LSH"] != null)
                {
                    model.LSH = row["LSH"].ToString();
                }
                if (row["CASEID"] != null)
                {
                    model.CASEID = row["CASEID"].ToString();
                }
                if (row["CCCC"] != null && row["CCCC"].ToString() != "")
                {
                    model.CCCC = int.Parse(row["CCCC"].ToString());
                }
                if (row["CLID"] != null)
                {
                    model.CLID = row["CLID"].ToString();
                }
                if (row["SJ"] != null && row["SJ"].ToString() != "")
                {
                    model.SJ = DateTime.Parse(row["SJ"].ToString());
                }
                if (row["ZT"] != null)
                {
                    model.ZT = row["ZT"].ToString();
                }
                if (row["TASK_TERMINATION_REASON"] != null)
                {
                    model.TASK_TERMINATION_REASON = row["TASK_TERMINATION_REASON"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,LSH,CASEID,CCCC,CLID,SJ,ZT,TASK_TERMINATION_REASON ");
            strSql.Append(" FROM CarState ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperOra.Query("", strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM CarState ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperOra.GetSingle("", strSql.ToString());
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T. desc");
            }
            strSql.Append(")AS Row, T.*  from CarState T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperOra.Query("", strSql.ToString());
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
            parameters[0].Value = "CarState";
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

