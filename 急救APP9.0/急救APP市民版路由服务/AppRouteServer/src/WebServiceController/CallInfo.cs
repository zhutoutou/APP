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
    public class CallInfo
    {
        /// <summary>
        /// 处理Call Info
        /// </summary>
        /// <param name="callInfo"></param>
        /// <returns></returns>
        public string HandleAddCallInfo(string callInfo)
        {
            Hashtable htCallInfo = (Hashtable)JSON.Decode(callInfo);
            Hashtable htReturn = new Hashtable();

            AppCallInfo aci;
            string strErrInfo;

            if (VerifyItems(out strErrInfo, out aci, htCallInfo))
            {
                // save info into database
                IDBAppCallInfo dbaci = DataAccess.DataAccess.GetDBAppCallInfo();
                if (dbaci.Add(aci))
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
        /// <param name="htCallInfo"></param>
        /// <returns></returns>
        public bool VerifyItems(out string ErrorInfo, out AppCallInfo aci, Hashtable htCallIn)
        {
            ErrorInfo = ""; 
            aci = new AppCallInfo();
            // phone	String	主叫号码	Varchar2(20)	20	必填
            if (htCallIn["phone"] == null || htCallIn["phone"].ToString() == "")
            {
                ErrorInfo = "主机号码不能为空";
                return false;
            }
            else
            {
                aci.PHONE = htCallIn["phone"].ToString();
            }

            // caseId	String	CaseID	Varchar2(50)	50	必填，由APP提供，GUID
            if (htCallIn["caseId"] == null || htCallIn["caseId"].ToString() == "")
            {
                ErrorInfo = "caseId不能为空";
                return false;
            }
            else
            {
                aci.CASEID = htCallIn["caseId"].ToString();
            }

            //province	String	省（直辖市）	Varchar2(30)	30	必填
            if (htCallIn["province"] == null || htCallIn["province"].ToString() == "")
            {
                ErrorInfo = "省（直辖市）不能为空";
                return false;
            }
            else
            {
                aci.PROVINCE = htCallIn["province"].ToString();
            }

            //city	String	市	Varchar2(30)	30	必填
            if (htCallIn["city"] == null || htCallIn["city"].ToString() == "")
            {
                ErrorInfo = "市不能为空";
                return false;
            }
            else
            {
                aci.CITY = htCallIn["city"].ToString();
            }

            //area	String	区（县）	Varchar2(30)	30	必填
            if (htCallIn["area"] == null || htCallIn["area"].ToString() == "")
            {
                ErrorInfo = "区（县）不能为空";
                return false;
            }
            else
            {
                aci.AREA = htCallIn["area"].ToString();
            }

            //isSelf	String	是否本人	Number(1)	1	必填，1：自己，2：           
            if (htCallIn["isSelf"] == null || htCallIn["isSelf"].ToString() == "")
            {
                ErrorInfo = "是否本人不能为空";
                return false;
            }
            else
            {
                aci.ISSELF = int.Parse(htCallIn["isSelf"].ToString());
            }

            //callTime	Date  	呼叫时间	Date		必填，呼叫时间   
            if (htCallIn["callTime"] == null || htCallIn["callTime"].ToString() == "")
            {
                ErrorInfo = "呼叫时间不能为空";
                return false;
            }
            else
            {
                aci.CALLTIME = DateTime.Parse(htCallIn["callTime"].ToString());
            }

            //name	String	姓名	Varchar2(50)	50	可以是本人，可以是亲友，由呼叫人指定，路人时候为空
            if (htCallIn["name"] != null)
            {
                aci.NAME = htCallIn["name"].ToString();
            }


            //sex	String	性别	Number(1)	1	可以是本人，可以是亲友，由呼叫人指定，路人时候为空【0：未知 1：男 2：女】
            if (htCallIn["sex"] == null || htCallIn["sex"].ToString() == "")
            {
                aci.SEX = 0;
            }
            else
            {
                aci.SEX = int.Parse(htCallIn["sex"].ToString());
            }

            //brithday	String	出生日期【年月日】	Varchar2 (8)	8	可以是本人，可以是亲友，由呼叫人指定，路人时候为空
            if (htCallIn["brithday"] != null)
            {
                aci.BRITHDAY = htCallIn["brithday"].ToString();
            }

            //height	Integer	身高	Number(4)		厘米，可以是本人，可以是亲友，由呼叫人指定，路人时候为空
            if (!(htCallIn["height"] == null || htCallIn["height"].ToString() == ""))
            {
                aci.HEIGHT = int.Parse(htCallIn["height"].ToString());
            }

            //weight	Integer	体重	Number(4)		千克，可以是本人，可以是亲友，由呼叫人指定，路人时候为空
            if (!(htCallIn["weight"] == null || htCallIn["weight"].ToString() == ""))
            {
                aci.WEIGHT = decimal.Parse(htCallIn["weight"].ToString());
            }

            //identityCard	String	身份证号码	Varchar2(50)	50	可以是本人，可以是亲友，由呼叫人指定，路人时候为空
            if (htCallIn["identityCard"] != null)
            {
                aci.IDENTITYCARD = htCallIn["identityCard"].ToString();
            }

            //jd	String	经度	Varchar2(30)	30	报警电话的经度
            if (htCallIn["jd"] != null)
            {
                aci.JD = htCallIn["jd"].ToString();
            }

            //wd	String	纬度	Varchar2(30)	30	报警电话的维度
            if (htCallIn["wd"] != null)
            {
                aci.WD = htCallIn["wd"].ToString();
            }

            //address	String	呼叫人地址	Varchar2(400)	400	报警电话的呼叫地址
            if (htCallIn["address"] != null)
            {
                aci.ADDRESS = htCallIn["address"].ToString();
            }

            //medicalHistory	String	既往病史	Varchar2(500)	500	可以是本人，可以是亲友，由呼叫人指定，路人时候为空
            if (htCallIn["medicalHistory"] != null)
            {
                aci.MEDICALHISTORY = htCallIn["medicalHistory"].ToString();
            }

            //contactWay1	String	联系方式1	Varchar2(20)	20	
            if (htCallIn["contactWay1"] != null)
            {
                aci.CONTACTWAY1 = htCallIn["contactWay1"].ToString();
            }

            //contactWay2	String	联系方式2	Varchar2(20)	20	
            if (htCallIn["contactWay2"] != null)
            {
                aci.CONTACTWAY2 = htCallIn["contactWay2"].ToString();
            }

            //contactWay3	String	联系方式3	Varchar2(20)	20	
            if (htCallIn["contactWay3"] != null)
            {
                aci.CONTACTWAY3 = htCallIn["contactWay3"].ToString();
            }

            //medicalInsuranceCard	String	医保卡号	Varchar2(50)	50	
            if (htCallIn["medicalInsuranceCard"] != null)
            {
                aci.MEDICALINSURANCECARD = htCallIn["medicalInsuranceCard"].ToString();
            }

            return true;
        }
    }
}
