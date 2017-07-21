using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ZIT.AppRouteServer.Model;
using ZIT.AppRouteServer.Utility;
using ZIT.AppRouteServer.DataAccess;
using ZIT.AppRouteServer.AppServerAPI.CarLocationManagerService;
using ZIT.LOG;

namespace ZIT.AppRouteServer.AppServerAPI
{
    class APICarLocation
    {
        public void HandleCarLocation(CarLocation cl)
        {
            try
            {
                //Save Database
               // DBCarLocation db = new DBCarLocation();
                //20160802 修改人：朱星汉 修改内容：速度字符串过长
                cl.SD = Math.Round((Decimal)cl.SD, 2);
                IDBCarLocation db = DataAccess.DataAccess.GetDBCarLocation();
                db.Add(cl);
                
                CarLocationManagerDelegateClient client = new CarLocationManagerDelegateClient();
                client.addCarLocationCompleted += new EventHandler<addCarLocationCompletedEventArgs>(Service_Completed);
                CredentialSoapHeader header = new CredentialSoapHeader();
                header.Username = SysParameters.AppAPIUsername;
                header.Password = SysParameters.AppAPIPassword;
                string strJson = ConvertModelToJson(cl);

                WebserviceUserState UserState = new WebserviceUserState();
                UserState.TIMES = 1;
                UserState.MESSAGE = strJson;

                client.addCarLocationAsync(header, strJson, UserState);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
        }

        private string ConvertModelToJson(CarLocation cl)
        {
            string strJson = "";
            Hashtable ht = new Hashtable();

            ht.Add("caseId", cl.CASEID);
            ht.Add("cccc", cl.CCCC);
            ht.Add("clid", cl.CLID);
            ht.Add("fx", cl.FX);
            ht.Add("jd", cl.JD);
            ht.Add("wd", cl.WD);
            ht.Add("lsh", cl.LSH);
            ht.Add("sj", string.Format("{0:yyyy-MM-dd HH:mm:ss}", cl.SJ));
            ht.Add("sd", cl.SD);

            strJson = JSON.Encode(ht);
            return strJson;
        }

        private void Service_Completed(object sender, addCarLocationCompletedEventArgs e)
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
                        if (UserState.TIMES < 2)
                        {
                            UserState.TIMES++;
                            ReSendCarLocation(UserState);
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
        private void ReSendCarLocation(WebserviceUserState UserState)
        {
            try
            {
                Thread.Sleep(SysParameters.APIErrorReSendInterval * 1000);

                string strJson = UserState.MESSAGE;

                CarLocationManagerDelegateClient client = new CarLocationManagerDelegateClient();
                client.addCarLocationCompleted += new EventHandler<addCarLocationCompletedEventArgs>(Service_Completed);
                CredentialSoapHeader header = new CredentialSoapHeader();
                header.Username = SysParameters.AppAPIUsername;
                header.Password = SysParameters.AppAPIPassword;

                client.addCarLocationAsync(header, strJson, UserState);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
        }
    }
}
