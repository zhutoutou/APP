using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZIT.AppClient.Utility;
using ZIT.AppClient.Model;
using System.Text.RegularExpressions;
using ZIT.LOG;

namespace ZIT.AppClient.Controller.BusinessServer
{
    class BServerMsgHandler
    {
        public void HandleMsg(string strMsg)
        {
            try
            {
                string strMessageId = strMsg.Substring(1, 4);
                if (strMessageId != "3000")
                {
                    LogHelper.WriteBssNetMsgLog("Recieve BServer message:" + strMsg);
                }
                switch (strMessageId)
                {
                    case "5182":
                        //派车告知消息
                        Handle5182Message(strMsg);
                        break;
                    case "5183":
                        //车辆节点信息
                        Handle5183Message(strMsg);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
        }
        /// <summary>
        ///  派车告知消息
        /// </summary>
        /// <param name="strMsg"></param>
        private void Handle5182Message(string strMsg)
        {
            if (strMsg.Substring(0, 1) == "[" && strMsg.Substring(strMsg.Length - 3, 3) == "*#]")
            {
                SendCarInfo sci = GetSendCarInfoModelByMessage(strMsg);
                CoreService.GetInstance().OtherMsgServer.SendCarInfo(sci);
                CoreService.GetInstance().bs.DealVehicleMap(sci);
            }
        }

        /// <summary>
        /// 车辆节点信息
        /// </summary>
        /// <param name="strMsg"></param>
        private void Handle5183Message(string strMsg)
        {
            if (strMsg.Substring(0, 1) == "[" && strMsg.Substring(strMsg.Length - 3, 3) == "*#]")
            {
                CarState cs = GetCarStateModelByMessage(strMsg);
                CoreService.GetInstance().OtherMsgServer.SendCarStep(cs);
                CoreService.GetInstance().bs.DealVehicleMap(cs);
            }
        }

        private CarState GetCarStateModelByMessage(string strMsg)
        {
            //[5183LSH:2016061300000000005*#CaseID:a07aa295-e5f0-41bb-858d-277b1e3479ff*#CCCC:02*#CLID:101*#SJ:2016-06-13 17:19:41*#ZT:病人上车*#Reason:*#]
            CarState cs = new CarState();
            cs.CASEID = GetValueByKey(strMsg, "CaseID");
            if (GetValueByKey(strMsg, "CCCC") != "")
            {
                cs.CCCC = int.Parse(GetValueByKey(strMsg, "CCCC"));
            }
            cs.CLID =  GetValueByKey(strMsg, "CLID");
            cs.LSH =  GetValueByKey(strMsg, "LSH");
            if (GetValueByKey(strMsg, "SJ") != "")
            {
                cs.SJ = DateTime.Parse(GetValueByKey(strMsg, "SJ"));
            }
            cs.TASK_TERMINATION_REASON = GetValueByKey(strMsg, "Reason");
            cs.ZT = GetValueByKey(strMsg, "ZT");

            return cs;
        }


        private SendCarInfo GetSendCarInfoModelByMessage(string strMsg)
        {
            //[5182LSH:2016061300000000004*#CLID:101*#CCCC:01*#CPH:鄂K06772*#CCSJ:2016-06-13 10:38:11*#
            //SSDW:孝感市中心医院*#DriverPhone:*#DriverName:刘豫*#DoctorPhone:*#DoctorName:孙光辉*#
            //CaseID:d04221ed-e1c7-48dc-bf59-3a408b16ba70*#TH:0*#]
            SendCarInfo sci = new SendCarInfo();
            sci.CASEID = GetValueByKey(strMsg, "CaseID");
            if (GetValueByKey(strMsg, "CCCC") != "")
            {
                sci.CCCC = int.Parse(GetValueByKey(strMsg, "CCCC"));
            }
            sci.CLID = GetValueByKey(strMsg, "CLID");
            if (GetValueByKey(strMsg, "CCSJ") != "")
            {
                sci.CCSJ = DateTime.Parse(GetValueByKey(strMsg, "CCSJ"));
            }
            sci.CPH = GetValueByKey(strMsg, "CPH");
            sci.DOCTORNAME = GetValueByKey(strMsg, "DoctorName");
            sci.DOCTORPHONE = GetValueByKey(strMsg, "DoctorPhone");
            sci.DRIVERPHONE = GetValueByKey(strMsg, "DriverPhone");
            sci.DRIVERNAME = GetValueByKey(strMsg, "DriverName");
            sci.LSH = GetValueByKey(strMsg, "LSH");
            sci.SSDW = GetValueByKey(strMsg, "SSDW");
            return sci;
        }


        private string GetValueByKey(string message, string key)
        {
            string strReturn = "";

            Regex reg = new Regex(key + ":(.*?)\\*#");
            if (reg.IsMatch(message))
            {
                Match match = reg.Match(message);
                strReturn = match.Groups[1].Value.Trim();
            }
            return strReturn;
        }


    }

}
