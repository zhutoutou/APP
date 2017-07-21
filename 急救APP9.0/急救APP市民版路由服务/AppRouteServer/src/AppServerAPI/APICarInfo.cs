using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ZIT.AppRouteServer.Model;
using ZIT.AppRouteServer.Utility;
using ZIT.AppRouteServer.DataAccess;
using ZIT.AppRouteServer.AppServerAPI.CarInfoManagerService;
using ZIT.LOG;

namespace ZIT.AppRouteServer.AppServerAPI
{
    public class APICarInfo
    {
        public void HandleCarInfo(SendCarInfo sci)
        {
            try
            {
                //Save Database
                //DBSendCarInfo db = new DBSendCarInfo();
                IDBSendCarInfo db = DataAccess.DataAccess.GetDBSendCarInfo();
                db.Add(sci);

                CarInfoManagerDelegateClient client = new CarInfoManagerDelegateClient();
                client.addCarInfoCompleted += new EventHandler<addCarInfoCompletedEventArgs>(Service_Completed);
                CredentialSoapHeader header = new CredentialSoapHeader();
                header.Username = SysParameters.AppAPIUsername;
                header.Password = SysParameters.AppAPIPassword;
                string strJson = ConvertModelToJson(sci);

                WebserviceUserState UserState = new WebserviceUserState();
                UserState.TIMES = 1;
                UserState.MESSAGE = strJson;

                //{"driverName":"刘豫","doctorPhone":"13871887869","doctorName":"孙光辉","ccsj":"2016-06-13 17:00:09","ssdw":"孝感市中心医院","driverPhone":"13871888610","caseId":"a07aa295-e5f0-41bb-858d-277b1e3479ff","lsh":"2016061300000000005","cph":"鄂K06772","clid":"101","cccc":2}
                client.addCarInfoAsync(header, strJson, UserState);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
        }

        private string ConvertModelToJson(SendCarInfo sci)
        {
            string strJson = "";
            Hashtable ht = new Hashtable();
            ht.Add("caseId", sci.CASEID);
            ht.Add("cccc", sci.CCCC);
            ht.Add("ccsj", string.Format("{0:yyyy-MM-dd HH:mm:ss}", sci.CCSJ));
            ht.Add("clid", sci.CLID);
            ht.Add("cph", sci.CPH);
            ht.Add("doctorName", sci.DOCTORNAME);
            ht.Add("doctorPhone", sci.DOCTORPHONE);
            ht.Add("driverName", sci.DRIVERNAME);
            ht.Add("driverPhone", sci.DRIVERPHONE);
            ht.Add("lsh", sci.LSH);
            ht.Add("ssdw", sci.SSDW);

            strJson = JSON.Encode(ht);
            return strJson;
        }

        private void Service_Completed(object sender, addCarInfoCompletedEventArgs e)
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
                            ReSendCarInfo(UserState);
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
        private void ReSendCarInfo(WebserviceUserState UserState)
        {
            try
            {
                Thread.Sleep(SysParameters.APIErrorReSendInterval * 1000);

                string strJson = UserState.MESSAGE;

                CarInfoManagerDelegateClient client = new CarInfoManagerDelegateClient();
                client.addCarInfoCompleted += new EventHandler<addCarInfoCompletedEventArgs>(Service_Completed);
                CredentialSoapHeader header = new CredentialSoapHeader();
                header.Username = SysParameters.AppAPIUsername;
                header.Password = SysParameters.AppAPIPassword;

                //{"driverName":"刘豫","doctorPhone":"13871887869","doctorName":"孙光辉","ccsj":"2016-06-13 17:00:09","ssdw":"孝感市中心医院","driverPhone":"13871888610","caseId":"a07aa295-e5f0-41bb-858d-277b1e3479ff","lsh":"2016061300000000005","cph":"鄂K06772","clid":"101","cccc":2}
                client.addCarInfoAsync(header, strJson, UserState);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
        }
    }
}
