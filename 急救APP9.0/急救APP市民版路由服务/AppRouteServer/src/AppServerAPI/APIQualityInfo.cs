using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ZIT.AppRouteServer.Model;
using ZIT.AppRouteServer.Utility;
using ZIT.AppRouteServer.DataAccess;
using ZIT.AppRouteServer.AppServerAPI.QualityInfoManagerService;
using ZIT.LOG;

namespace ZIT.AppRouteServer.AppServerAPI
{
    class APIQualityInfo
    {
        public void HandleQualityInfo(ServiceQualityInfo sqi)
        {
            try
            {
                //Save Database
                // DBServiceQualityInfo db = new DBServiceQualityInfo();
                IDBServiceQualityInfo db = DataAccess.DataAccess.GetDBServiceQualityInfo();
                db.Add(sqi);

                QualityInfoManagerDelegateClient client = new QualityInfoManagerDelegateClient();
                client.addQualityInfoCompleted += new EventHandler<addQualityInfoCompletedEventArgs>(Service_Completed);
                CredentialSoapHeader header = new CredentialSoapHeader();
                header.Username = SysParameters.AppAPIUsername;
                header.Password = SysParameters.AppAPIPassword;
                string strJson = ConvertModelToJson(sqi);

                WebserviceUserState UserState = new WebserviceUserState();
                UserState.TIMES = 1;
                UserState.MESSAGE = strJson;

                client.addQualityInfoAsync(header, strJson, UserState);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
        }

        private string ConvertModelToJson(ServiceQualityInfo sqi)
        {
            string strJson = "";
            Hashtable ht = new Hashtable();

            ht.Add("caseId", sqi.CASEID);
            ht.Add("cccc", sqi.CCCC);
            ht.Add("clid", sqi.CLID);
            ht.Add("km", sqi.KM);
            ht.Add("lsh", sqi.LSH);
            ht.Add("money", sqi.MONEY);

            strJson = JSON.Encode(ht);
            return strJson;
        }


        private void Service_Completed(object sender, addQualityInfoCompletedEventArgs e)
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
                            ReSendQualityInfo(UserState);
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
        private void ReSendQualityInfo(WebserviceUserState UserState)
        {
            try
            {
                Thread.Sleep(SysParameters.APIErrorReSendInterval * 1000);

                string strJson = UserState.MESSAGE;

                QualityInfoManagerDelegateClient client = new QualityInfoManagerDelegateClient();
                client.addQualityInfoCompleted += new EventHandler<addQualityInfoCompletedEventArgs>(Service_Completed);
                CredentialSoapHeader header = new CredentialSoapHeader();
                header.Username = SysParameters.AppAPIUsername;
                header.Password = SysParameters.AppAPIPassword;
               
                client.addQualityInfoAsync(header, strJson, UserState);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
        }

      
    }
}
