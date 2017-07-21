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
    class DBQualityEvaluation : IDBQualityEvaluation
    {
         /// <summary>
         /// 增加一条数据
         /// </summary>
         public bool Add(QualityEvaluation model)
         {
             StringBuilder strSql = new StringBuilder();
             strSql.Append("insert into qualityevaluation(");
             strSql.Append("LSH,QUALITYCOMMENT,REASON,CASEID,COMMENTTIME,ADDTIME)");
             strSql.Append(" values (");
             strSql.Append("@LSH,@QUALITYCOMMENT,@REASON,@CASEID,@COMMENTTIME,@ADDTIME)");
             MySqlParameter[] parameters = {
                new MySqlParameter("@LSH", DBOpHelper.GetString(model.LSH)),
                new MySqlParameter("@QUALITYCOMMENT",  DBOpHelper.GetString(model.QUALITYCOMMENT)),
                new MySqlParameter("@REASON",  DBOpHelper.GetString(model.REASON)),
                new MySqlParameter("@CASEID",  DBOpHelper.GetString(model.CASEID)),
                new MySqlParameter("@COMMENTTIME",  DBOpHelper.GetDateTime(model.COMMENTTIME)),
                new MySqlParameter("@ADDTIME",  DBOpHelper.GetDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")))};

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
        /// 获取最新的质量评价信息
        /// </summary>
        /// <returns></returns>
        public List<QualityEvaluation> GetNewQualityEvaluation()
        {
            List<QualityEvaluation> list = new List<QualityEvaluation>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from QualityEvaluation where  TIMESTAMPDIFF(SECOND,COMMENTTIME,now()) <3600  and READFLAG=0 order by COMMENTTIME limit 100 ");
            MySqlParameter[] parameters = {
			};

            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                List<string> arrayList = new List<string>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    QualityEvaluation model = DataRowToModel(ds.Tables[0].Rows[i]);
                    list.Add(model);
                    arrayList.Add(string.Format("update QualityEvaluation set READFLAG=1,READTIME=now() where CASEID='{0}'", model.CASEID));
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
        public QualityEvaluation DataRowToModel(DataRow row)
        {
            QualityEvaluation model = new QualityEvaluation();
            if (row != null)
            {
                if (row["LSH"] != null)
                {
                    model.LSH = row["LSH"].ToString();
                }
                if (row["QUALITYCOMMENT"] != null)
                {
                    model.QUALITYCOMMENT = row["QUALITYCOMMENT"].ToString();
                }
                if (row["REASON"] != null)
                {
                    model.REASON = row["REASON"].ToString();
                }
                if (row["CASEID"] != null)
                {
                    model.CASEID = row["CASEID"].ToString();
                }
                if (row["COMMENTTIME"] != null && row["COMMENTTIME"].ToString() != "")
                {
                    model.COMMENTTIME = DateTime.Parse(row["COMMENTTIME"].ToString());
                }
            }
            return model;
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
    }

}
