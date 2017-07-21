using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ZIT.AppRouteServer.Model;
using ZIT.AppRouteServer.Utility;
using ZIT.AppRouteServer.DataAccess;
using ZIT.AppRouteServer.AppServerAPI.CallErrorManagerService;
using ZIT.LOG;

namespace ZIT.AppRouteServer.AppServerAPI
{
    public class APICallError
    {
        public void HandleCallError(HandleCallError hce)
        {
            try
            {
                //Save Database
                IDBHandleCallError db = DataAccess.DataAccess.GetDBHandleCallError();
                db.Add(hce);

                CallErrorManagerDelegateClient client = new CallErrorManagerDelegateClient();
                client.addCallErrorCompleted += new EventHandler<addCallErrorCompletedEventArgs>(Service_Completed);
                CredentialSoapHeader header = new CredentialSoapHeader();
                header.Username = SysParameters.AppAPIUsername;
                header.Password = SysParameters.AppAPIPassword;
                string strJson = ConvertModelToJson(hce);

                WebserviceUserState UserState = new WebserviceUserState();
                UserState.TIMES = 1;
                UserState.MESSAGE = strJson;

                client.addCallErrorAsync(header, strJson, UserState);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
        }

        private string ConvertModelToJson(HandleCallError hce)
        {
            string strJson = "";
            Hashtable ht = new Hashtable();

            ht.Add("caseId", hce.CASEID);
            ht.Add("errorCode", hce.ERRORCODE);
            ht.Add("errorMsg", hce.ERRORMSG);
            ht.Add("errorTime", string.Format("{0:yyyy-MM-dd HH:mm:ss}", hce.ERRORTIME));

            strJson = JSON.Encode(ht);
            return strJson;
        }

        private void Service_Completed(object sender, addCallErrorCompletedEventArgs e)
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
                            ReSendCallError(UserState);
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
        private void ReSendCallError(WebserviceUserState UserState)
        {
            try
            {
                Thread.Sleep(SysParameters.APIErrorReSendInterval * 1000);

                string strJson = UserState.MESSAGE;

                CallErrorManagerDelegateClient client = new CallErrorManagerDelegateClient();
                client.addCallErrorCompleted += new EventHandler<addCallErrorCompletedEventArgs>(Service_Completed);
                CredentialSoapHeader header = new CredentialSoapHeader();
                header.Username = SysParameters.AppAPIUsername;
                header.Password = SysParameters.AppAPIPassword;

                client.addCallErrorAsync(header, strJson, UserState);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
        }
    }
}
