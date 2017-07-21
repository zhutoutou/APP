using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using ZIT.AppRouteServer.Model;
using ZIT.AppRouteServer.Utility;
using ZIT.AppRouteServer.DataAccess;

namespace ZIT.AppRouteServer.WebServiceController
{
    public class QualityComment
    {
        public string HandleAddQualityComment(string qualityComment)
        {
            Hashtable htQualityComment = (Hashtable)JSON.Decode(qualityComment);
            Hashtable htReturn = new Hashtable();

            QualityEvaluation qe;
            string strErrInfo;

            if (VerifyItems(out strErrInfo, out qe, htQualityComment))
            {
                // save info into database
                IDBQualityEvaluation dbqe = DataAccess.DataAccess.GetDBQualityEvaluation();
                if (dbqe.Add(qe))
                {
                    htReturn.Add("msg", "保存成功");
                    htReturn.Add("success", "true");
                }
                else
                {
                    htReturn.Add("msg", "数据库存入错误");
                    htReturn.Add("success", "false");
                }
            }
            else
            {
                htReturn.Add("msg", strErrInfo);
                htReturn.Add("success", "false");
            }

            return JSON.Encode(htReturn);

        }

        /// <summary>
        /// 验证客户端提交的信息内容
        /// </summary>
        /// <param name="ErrorInfo"></param>
        /// <param name="htQualityComment"></param>
        /// <returns></returns>
        public bool VerifyItems(out string ErrorInfo, out QualityEvaluation qe, Hashtable htQualityComment)
        {
            ErrorInfo = "";
            qe = new QualityEvaluation();

            // lsh;	String	服务流水号	Varchar2(50)，not null	50	主键，120自动生成，并发送给APP的
            if (htQualityComment["lsh"] == null || htQualityComment["lsh"].ToString() == "")
            {
                ErrorInfo = "服务流水号不能为空";
                return false;
            }
            else
            {
                qe.LSH = htQualityComment["lsh"].ToString();
            }

            // qualityComment	String	服务质量评价	Varchar2(8)	8	0：非常满意
            // 1：满意
            // 2：不满意
            if (htQualityComment["qualityComment"] == null || htQualityComment["qualityComment"].ToString() == "")
            {
                ErrorInfo = "服务质量评价不能为空";
                return false;
            }
            else
            {
                qe.QUALITYCOMMENT = htQualityComment["qualityComment"].ToString();
            }

            //caseId	String	CaseID	Varchar2(20)	20	由APP提供
            if (htQualityComment["caseId"] == null || htQualityComment["caseId"].ToString() == "")
            {
                ErrorInfo = "caseId不能为空";
                return false;
            }
            else
            {
                qe.CASEID = htQualityComment["caseId"].ToString();
            }

            //commentTime	Date  	评价时间	Date
            if (htQualityComment["commentTime"] == null || htQualityComment["commentTime"].ToString() == "")
            {
                ErrorInfo = "评价时间不能为空";
                return false;
            }
            else
            {
                qe.COMMENTTIME = DateTime.Parse(htQualityComment["commentTime"].ToString());
            }

            //reason	String	原因	Varchar2 (400)	400	不满意时，必填项，满意时可不填写
            if (htQualityComment["reason"] != null)
            {
                qe.REASON = htQualityComment["reason"].ToString();
            }

            return true;
        }
    }
}
