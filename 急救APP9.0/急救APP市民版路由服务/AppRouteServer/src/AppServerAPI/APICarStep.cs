using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ZIT.AppRouteServer.Model;
using ZIT.AppRouteServer.Utility;
using ZIT.AppRouteServer.DataAccess;
using ZIT.AppRouteServer.AppServerAPI.CarStepManagerService;
using ZIT.LOG;

namespace ZIT.AppRouteServer.AppServerAPI
{
    class APICarStep
    {
        public void HandleCarStep(CarState cs)
        {
            try
            {
                //Save Database
                IDBCarState db = DataAccess.DataAccess.GetDBCarState();
                db.Add(cs);

                CarStepManagerDelegateClient client = new CarStepManagerDelegateClient();
                client.addCarStepCompleted += new EventHandler<addCarStepCompletedEventArgs>(Service_Completed);
                CredentialSoapHeader header = new CredentialSoapHeader();
                header.Username = SysParameters.AppAPIUsername;
                header.Password = SysParameters.AppAPIPassword;
                string strJson = ConvertModelToJson(cs);
                
                WebserviceUserState UserState = new WebserviceUserState();
                UserState.TIMES = 1;
                UserState.MESSAGE = strJson;

                //{"caseId":"a07aa295-e5f0-41bb-858d-277b1e3479ff","lsh":"2016061300000000005","sj":"2016-06-13 17:19:41","zt":"病人上车","reason":"","clid":"101","cccc":2}
                client.addCarStepAsync(header, strJson, UserState);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
        }

        private string ConvertModelToJson(CarState cs)
        {
            string strJson = "";
            Hashtable ht = new Hashtable();
            ht.Add("caseId", cs.CASEID);
            ht.Add("cccc", cs.CCCC);
            ht.Add("clid", cs.CLID);
            ht.Add("lsh", cs.LSH);
            ht.Add("sj", string.Format("{0:yyyy-MM-dd HH:mm:ss}", cs.SJ));
            ht.Add("zt", cs.ZT);
            ht.Add("reason", cs.TASK_TERMINATION_REASON);

            strJson = JSON.Encode(ht);
            return strJson;
        }


        private void Service_Completed(object sender, addCarStepCompletedEventArgs e)
        {
            try
            {
                if (e.UserState != null)
                {
                    WebserviceUserState UserState = (WebserviceUserState)e.UserState;

                    if (e.Error != null)
                    {
                        string strErrInfo = string.Format("Message:{0}\n Send Times:{1}\n", UserState.MESSAGE, UserState.TIMES.ToString());
                        LogHelper.WriteLog("调用 AppServer API 异常：", new Exception(strErrInfo));
                        LogHelper.WriteLog("调用 AppServer API 异常：", e.Error);
                        if (UserState.TIMES < 3)
                        {
                            UserState.TIMES++;
                            ReSendCarStep(UserState);
                        }
                    }
                    else if (e.Result != null)
                    {
                        LogHelper.WriteLog(e.Result);
                    }
                }
                else
                {
                    throw new Exception("e.UserState 为空！");
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
        }

        /// <summary>
        /// 重发消息
        /// </summary>
        /// <param name="UserState"></param>
        private void ReSendCarStep(WebserviceUserState UserState)
        {
            try
            {
                Thread.Sleep(SysParameters.APIErrorReSendInterval * 1000);

                string strJson = UserState.MESSAGE;

                CarStepManagerDelegateClient client = new CarStepManagerDelegateClient();
                client.addCarStepCompleted += new EventHandler<addCarStepCompletedEventArgs>(Service_Completed);
                CredentialSoapHeader header = new CredentialSoapHeader();
                header.Username = SysParameters.AppAPIUsername;
                header.Password = SysParameters.AppAPIPassword;

                client.addCarStepAsync(header, strJson, UserState);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
        }
    }
}
