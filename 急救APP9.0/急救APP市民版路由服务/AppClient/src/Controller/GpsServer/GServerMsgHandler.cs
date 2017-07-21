using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZIT.AppClient.Utility;
using ZIT.LOG;
using ZIT.AppClient.Model;
using System.Text.RegularExpressions;

namespace ZIT.AppClient.Controller.GpsServer
{
    class GServerMsgHandler
    {
        public void HandleMsg(string strMsg)
        {
            string strMessageId = strMsg.Substring(1, 2);
            switch (strMessageId)
            {
                case "40":
                    //车辆轨迹消息
                    LogHelper.WriteSevNetMsgLog("Recieve GServer message:" + strMsg);
                    Handle40Message(strMsg);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        ///  车辆轨迹消息
        /// </summary>
        /// <param name="strMsg"></param>
        private void Handle40Message(string strMsg)
        {
            try
            {
                CarLocation cl = GetCarLocationByMessage(strMsg);
                VehicleTaskInfo info;
                if (CoreService.GetInstance().bs.GetVehicleTaskInfoByCLID(out info, cl.CLID))
                {
                    cl.CASEID = info.CaseId;
                    cl.CCCC = info.CCCC;
                    cl.LSH = info.LSH;
                    CoreService.GetInstance().CarLocationServer.SendCarLocation(cl);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
        }

        private CarLocation GetCarLocationByMessage(string strMsg)
        {
            //(40ID:101%SJ:2016-04-13 11:39:44%JD:118.77554321289063%WD:31.985252380371094%SD:0.0%FX:0.0%KS:%GZ:%TXZT:0%BJ:0%)
            CarLocation cl = new CarLocation();
           
            cl.CLID = GetValueByKey(strMsg, "ID");
            cl.FX = GetValueByKey(strMsg, "FX");
            cl.JD = GetValueByKey(strMsg, "JD");
            cl.WD = GetValueByKey(strMsg, "WD");
            if (GetValueByKey(strMsg, "SD") != "")
            {
                cl.SD = decimal.Parse( GetValueByKey(strMsg, "SD"));
            }
            if (GetValueByKey(strMsg, "SJ") != "")
            {
                cl.SJ =  DateTime.Parse(GetValueByKey(strMsg, "SJ"));
            }
            return cl;
        }

        private string GetValueByKey(string message, string key)
        {
            string strReturn = "";

            Regex reg = new Regex(key + ":(.*?)%");
            if (reg.IsMatch(message))
            {
                Match match = reg.Match(message);
                strReturn = match.Groups[1].Value.Trim();
            }
            return strReturn;
        }
    }
}
