using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using ZIT.AppRouteServer.Model;
using ZIT.AppRouteServer.DataAccess;
using ZIT.AppRouteServer.Controller.CommChannel;
using ZIT.Communication.Comm.Communication.Messages;
using ZIT.AppRouteServer.AppServerAPI;
using ZIT.AppRouteServer.Utility;
using ZIT.LOG;

namespace ZIT.AppRouteServer.Controller.DataAnalysis
{
    public class CallInfo
    {
        private Thread td;

        private IDBAppCallInfo AppCall;

        private AppServer Appserver;

        public CallInfo()
        {
            td = new Thread(new ThreadStart(Todo));
            AppCall = DataAccess.DataAccess.GetDBAppCallInfo();
            Appserver = new AppServer();
        }

        public void Start()
        {
            td.Start();
        }

        /// <summary>
        ///  操作线程
        /// </summary>
        private void Todo()
        {
            while (true)
            {
                try
                {
                    List<AppCallInfo> list = AppCall.GetNewCallInfo();
                    foreach (var item in list)
                    {
                        HandleAppCallInfo(item);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog("程序异常!", ex);
                }
                Thread.Sleep(1 * 1000);
            }
        }

        private void HandleAppCallInfo(AppCallInfo aci)
        {
            try
            {
                IDBRouter router = DataAccess.DataAccess.GetDBRouter();
                string strUnitCode = router.GetUnitCodeByRegistCity(aci.PROVINCE, aci.CITY, aci.AREA);
                if (string.IsNullOrEmpty(strUnitCode))
                {
                    //send error msg to app server
                    HandleCallError hce = new HandleCallError();
                    hce.CASEID = aci.CASEID;
                    hce.ERRORCODE = "1";
                    hce.ERRORMSG = "未注册APP急救服务";
                    hce.ERRORTIME = DateTime.Now;
                    Appserver.SendCallError(hce);
                }
                else
                {
                    ServerClient client = CoreService.GetInstance().CallInServer.GetServerClientByUnitCode(strUnitCode);
                    if (client != null)
                    {
                        //send msg to app client
                        string msg = CreateCallInfoJsonByModel(aci);
                        client.SendMessage(new ScsTextMessage(msg));
                    }
                    else
                    {
                        //send error msg to app server
                        HandleCallError hce = new HandleCallError();
                        hce.CASEID = aci.CASEID;
                        hce.ERRORCODE = "2";
                        hce.ERRORMSG = "未找到服务通讯";
                        hce.ERRORTIME = DateTime.Now;
                        Appserver.SendCallError(hce);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("程序异常!", ex);
            }
        }

        private string CreateCallInfoJsonByModel(AppCallInfo aci)
        {
            Hashtable htCallInfo = new Hashtable();

            htCallInfo.Add("messageName", "CallInfo");
            htCallInfo.Add("phone", aci.PHONE);
            htCallInfo.Add("name", aci.NAME);
            htCallInfo.Add("sex", aci.SEX);
            htCallInfo.Add("brithday", aci.BRITHDAY);
            htCallInfo.Add("height", aci.HEIGHT);
            htCallInfo.Add("weight", aci.WEIGHT);
            htCallInfo.Add("identityCard", aci.IDENTITYCARD);
            htCallInfo.Add("jd", aci.JD);
            htCallInfo.Add("wd", aci.WD);
            htCallInfo.Add("address", aci.ADDRESS);
            htCallInfo.Add("medicalHistory", aci.MEDICALHISTORY);
            htCallInfo.Add("contactWay1", aci.CONTACTWAY1);
            htCallInfo.Add("contactWay2", aci.CONTACTWAY2);
            htCallInfo.Add("contactWay3", aci.CONTACTWAY3);
            htCallInfo.Add("medicalInsuranceCard", aci.MEDICALINSURANCECARD);
            htCallInfo.Add("caseId", aci.CASEID);
            htCallInfo.Add("province", aci.PROVINCE);
            htCallInfo.Add("city", aci.CITY);
            htCallInfo.Add("area", aci.AREA);
            htCallInfo.Add("callTime", string.Format("{0:yyyy-MM-dd HH:mm:ss}",aci.CALLTIME));
            htCallInfo.Add("isSelf", aci.ISSELF);

            return JSON.Encode(htCallInfo);
        }


        public void Stop()
        {
            td.Abort();
        }

    }
}
