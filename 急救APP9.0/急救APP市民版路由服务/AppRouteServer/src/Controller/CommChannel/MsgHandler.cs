using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using ZIT.AppRouteServer.Model;
using ZIT.AppRouteServer.Utility;
using ZIT.AppRouteServer.DataAccess;
using ZIT.Communication.Comm.Communication.Messages;
using ZIT.AppRouteServer.AppServerAPI;
using ZIT.LOG;

namespace ZIT.AppRouteServer.Controller.CommChannel
{
    public class MsgHandler
    {
        private ServerClient Client;

        private AppServer Appserver;

        public MsgHandler(ServerClient client)
        {
            Client = client;
            Appserver = new AppServer();
        }

        public void Message_Handler(object sender, MessageEventArgs Args)
        {
            try
            {
                ScsTextMessage Message = (ScsTextMessage)Args.Message;
                LogHelper.WriteNetMsgLog("Recieved message Unitcode is " + Client.UnitCode + " Port is " + Client.Routeserver.ServerPort.ToString() + ":" + Message.Text);
                Hashtable htMessage = (Hashtable)JSON.Decode(Message.Text);

                switch (htMessage["messageName"].ToString())
                {
                    case "LoginServer":
                        HandleLoginServer(htMessage);
                        break;
                    case "SendCarInfo":
                        HandleSendCarInfo(htMessage);
                        break;
                    case "CarStep":
                        HandleCarStep(htMessage);
                        break;
                    case "CarLocation":
                        HandleCarLocation(htMessage);
                        break;
                    case "ServiceQualityInfo":
                        HandleServiceQualityInfo(htMessage);
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
        /// 登陆处理
        /// </summary>
        /// <param name="recStr"></param>
        /// <returns></returns>
        private void HandleLoginServer(Hashtable htMessage)
        {
            try
            {
                Hashtable htReturn = new Hashtable();
                htReturn.Add("messageName", "LoginServerResp");
                string strUnitCode = htMessage["unitCode"].ToString();
                string strUnitName;
                if (!string.IsNullOrEmpty(strUnitCode))
                {
                    IDBRouter route = DataAccess.DataAccess.GetDBRouter();
                    //strUnitName = route.GetUnitNameByUnitCode(strUnitCode);
                    strUnitName = "1213";
                    if(!string.IsNullOrEmpty(strUnitName))
                    {
                        Client.UnitCode = strUnitCode;
                        Client.UnitName = strUnitName;
                        Client.Status = NetStatus.Login;
                        htReturn.Add("loginResult", "1");
                        htReturn.Add("failtureReason", "");
                    }
                    else
                    {
                        htReturn.Add("loginResult", "0");
                        htReturn.Add("failtureReason", "该行政编码未注册！");
                    }
                }
                else
                {
                    htReturn.Add("loginResult", "0");
                    htReturn.Add("failtureReason", "行政编码为空！");
                }
                string strReturnMsg = JSON.Encode(htReturn);
                Client.SendMessage(new ScsTextMessage(strReturnMsg));
                Client.Routeserver.OnServerConnectedClientChanged();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
        }

        private void HandleSendCarInfo(Hashtable htMessage)
        {
            try
            {
                SendCarInfo sci = GetSendCarInfoModelByHashtable(htMessage);
                Appserver.SendCarInfo(sci);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
        }

        private SendCarInfo GetSendCarInfoModelByHashtable(Hashtable htMessage)
        {
            SendCarInfo sci = new SendCarInfo();
            if (htMessage["caseId"] != null)
            {
                sci.CASEID = htMessage["caseId"].ToString();
            }
            if (htMessage["cccc"] != null && htMessage["cccc"].ToString()!="")
            {
                sci.CCCC = int.Parse(htMessage["cccc"].ToString());
            }

            if (htMessage["ccsj"] != null && htMessage["ccsj"].ToString() != "")
            {
                sci.CCSJ = DateTime.Parse(htMessage["ccsj"].ToString());
            }
            if (htMessage["clid"] != null)
            {
                sci.CLID = htMessage["clid"].ToString();
            }
            if (htMessage["cph"] != null)
            {
                sci.CPH = htMessage["cph"].ToString();
            }
            if (htMessage["doctorName"] != null)
            {
                sci.DOCTORNAME = htMessage["doctorName"].ToString();
            }
            if (htMessage["doctorPhone"] != null)
            {
                sci.DOCTORPHONE = htMessage["doctorPhone"].ToString();
            }
            if (htMessage["driverName"] != null)
            {
                sci.DRIVERNAME = htMessage["driverName"].ToString();
            }
            if (htMessage["driverPhone"] != null)
            {
                sci.DRIVERPHONE = htMessage["driverPhone"].ToString();
            }
            if (htMessage["lsh"] != null)
            {
                sci.LSH = htMessage["lsh"].ToString();
            }
            if (htMessage["ssdw"] != null)
            {
                sci.SSDW = htMessage["ssdw"].ToString();
            }
            return sci;
        }

        private void HandleCarStep(Hashtable htMessage)
        {
            try
            {
                CarState cs = GetCarStateModelByHashtable(htMessage);
                Appserver.SendCarStep(cs);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
        }

        private CarState GetCarStateModelByHashtable(Hashtable htMessage)
        {
            CarState sci = new CarState();
            if (htMessage["caseId"] != null)
            {
                sci.CASEID = htMessage["caseId"].ToString();
            }
            if (htMessage["cccc"] != null && htMessage["cccc"].ToString() != "")
            {
                sci.CCCC = int.Parse(htMessage["cccc"].ToString());
            }
            if (htMessage["clid"] != null)
            {
                sci.CLID = htMessage["clid"].ToString();
            }
            if (htMessage["lsh"] != null)
            {
                sci.LSH = htMessage["lsh"].ToString();
            }
            if (htMessage["sj"] != null && htMessage["sj"].ToString() != "")
            {
                sci.SJ = DateTime.Parse(htMessage["sj"].ToString());
            }
            if (htMessage["zt"] != null)
            {
                sci.ZT = htMessage["zt"].ToString();
            }
            if (htMessage["reason"] != null)
            {
                sci.TASK_TERMINATION_REASON = htMessage["reason"].ToString();
            }
            return sci;
        }

        private void HandleCarLocation(Hashtable htMessage)
        {
            CarLocation cl = GetCarLocationModelByHashtable(htMessage);
            Appserver.SendCarLocation(cl);
        }

        private CarLocation GetCarLocationModelByHashtable(Hashtable htMessage)
        {
            CarLocation cl = new CarLocation();
            if (htMessage["caseId"] != null)
            {
                cl.CASEID = htMessage["caseId"].ToString();
            }
            if (htMessage["cccc"] != null && htMessage["cccc"].ToString() != "")
            {
                cl.CCCC = int.Parse(htMessage["cccc"].ToString());
            }
            if (htMessage["clid"] != null)
            {
                cl.CLID = htMessage["clid"].ToString();
            }
            if (htMessage["fx"] != null)
            {
                cl.FX = htMessage["fx"].ToString();
            }
            if (htMessage["jd"] != null)
            {
                cl.JD = htMessage["jd"].ToString();
            }
            if (htMessage["lsh"] != null)
            {
                cl.LSH = htMessage["lsh"].ToString();
            }
            if (htMessage["sd"] != null && htMessage["sd"].ToString() != "")
            {
                cl.SD = decimal.Parse(htMessage["sd"].ToString());
            }
            if (htMessage["sj"] != null && htMessage["sj"].ToString() != "")
            {
                cl.SJ = DateTime.Parse(htMessage["sj"].ToString());
            }
            if (htMessage["wd"] != null)
            {
                cl.WD = htMessage["wd"].ToString();
            }
            return cl;
        }


        private void HandleServiceQualityInfo(Hashtable htMessage)
        {
            ServiceQualityInfo sqi = GetServiceQualityInfoModelByHashtable(htMessage);
            Appserver.SendQualityInfo(sqi);
        }

        private ServiceQualityInfo GetServiceQualityInfoModelByHashtable(Hashtable htMessage)
        {
            ServiceQualityInfo cl = new ServiceQualityInfo();
            cl.CASEID = htMessage["caseId"].ToString();


            return cl;
        }

        
    }
}

