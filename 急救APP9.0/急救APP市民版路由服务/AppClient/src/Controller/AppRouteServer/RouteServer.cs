using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using ZIT.Communication.Comm.Client;
using ZIT.Communication.Comm.Client.Tcp;
using ZIT.Communication.Comm.Communication;
using ZIT.Communication.Comm.Communication.EndPoints.Tcp;
using ZIT.Communication.Comm.Communication.Messages;
using ZIT.LOG;
using ZIT.AppClient.Utility;
using ZIT.AppClient.Model;

namespace ZIT.AppClient.Controller.AppRouteServer
{
    internal class RouteServer
    {
        /// <summary>
        /// 与 Route Server 连接状态改变事件
        /// </summary>
        public event EventHandler<StatusEventArgs> ConnectionStatusChanged;

        /// <summary>
        /// 连接Route Server客户端类
        /// </summary>
        private IScsClient tcpClient;

        /// <summary>
        /// Route server ip
        /// </summary>
        public string ServerIP;

        /// <summary>
        /// Route server port
        /// </summary>
        public short ServerPort;

        /// <summary>
        /// 消息处理类
        /// </summary>
        private RouteServerMsgHandler MsgHandler;

        /// <summary>
        /// 客户端重连类
        /// </summary>
        private ClientReConnecter crc;


        /// <summary>
        /// 构造函数
        /// </summary>
        public RouteServer()
        {
            MsgHandler = new RouteServerMsgHandler(this);
        }

        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            tcpClient = ScsClientFactory.CreateClient(new ScsTcpEndPoint(ServerIP, ServerPort));
            crc = new ClientReConnecter(tcpClient);
            tcpClient.Disconnected += new EventHandler(RouteServer_Disconnected);
            tcpClient.Connected += new EventHandler(RouteServer_Connected);
            tcpClient.ConnectTimeout = 30;
            tcpClient.MessageReceived += new EventHandler<MessageEventArgs>(RouteServer_MessageReceived);
            tcpClient.MessageSent += new EventHandler<MessageEventArgs>(RouteServer_MessageSent);
            tcpClient.Connect();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
        }


        /// <summary>
        /// 与Route server 断开连接处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RouteServer_Disconnected(object sender, EventArgs e)
        {
            var handler = ConnectionStatusChanged;
            if (handler != null)
            {
                handler(this, new StatusEventArgs(NetStatus.DisConnected));
            }
        }

        /// <summary>
        /// 已连接 Route server 处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RouteServer_Connected(object sender, EventArgs e)
        {
            var handler = ConnectionStatusChanged;
            if (handler != null)
            {
                handler(this, new StatusEventArgs(NetStatus.Connected));
            }
            MsgHandler.SendLoginServerMsg();
        }

        /// <summary>
        /// 已登录 Route server 处理方法
        /// </summary>
        public void OnRouteServerLogin()
        {
            var handler = ConnectionStatusChanged;
            if (handler != null)
            {
                handler(this, new StatusEventArgs(NetStatus.Login));
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RouteServer_MessageSent(object sender, MessageEventArgs e)
        {
            try
            {
                if (e.Message is ScsPingMessage)
                {
                    return;
                }
                LogHelper.WriteSevNetMsgLog("Sent message to RouteServer:" + e.Message.ToString());
            }
            catch
            { }
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RouteServer_MessageReceived(object sender, MessageEventArgs e)
        {
            try
            {
                MsgHandler.HandleMsg((ScsTextMessage)e.Message);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
        }

  
        /// <summary>
        /// 发给消息给Route Server
        /// </summary>
        /// <param name="strMsg"></param>
        public void SendMessage(ScsTextMessage Message)
        {
            try
            {
                if (null != tcpClient)
                {
                    tcpClient.SendMessage(Message);
                    LogHelper.WriteSevNetMsgLog("Send route server message" + Message.Text);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("", ex);
            }
        }

        /// <summary>
        /// 发送派车信息给route server
        /// </summary>
        /// <param name="sci"></param>
        public void SendCarInfo(SendCarInfo sci)
        {
            Hashtable ht = GetSendCarInfoHashtableByModel(sci);
            ScsTextMessage message = new ScsTextMessage(JSON.Encode(ht));
            SendMessage(message);
        }

        /// <summary>
        /// 发送车辆状态信息给route server
        /// </summary>
        /// <param name="sci"></param>
        public void SendCarStep(CarState cs)
        {
            Hashtable ht = GetCarStepHashtableByModel(cs);
            ScsTextMessage message = new ScsTextMessage(JSON.Encode(ht));
            SendMessage(message);
        }
        /// <summary>
        /// 发送车辆轨迹信息给route server
        /// </summary>
        /// <param name="cl"></param>
        public void SendCarLocation(CarLocation cl)
        {
            Hashtable ht = GetCarLocationHashtableByModel(cl);
            ScsTextMessage message = new ScsTextMessage(JSON.Encode(ht));
            SendMessage(message);
        }

        /// <summary>
        /// 发送服务质量信息给route server
        /// </summary>
        /// <param name="cl"></param>
        public void SendQualityInfo(ServiceQualityInfo sqi)
        {
            Hashtable ht = GetQualityInfoHashtableByModel(sqi);
            ScsTextMessage message = new ScsTextMessage(JSON.Encode(ht));
            SendMessage(message);
        }

        public Hashtable GetSendCarInfoHashtableByModel(SendCarInfo sci)
        {

            /*lsh;	String	流水号	Varchar2(19) not null	19	必填，主键，120生成
            clid	String	车俩ID	Varchar2(10) not null	10	必填
            cccc	Integer	出车车次	Number(2)	2	必填，默认为弟1次
            cph	String	车牌号	Varchar2 (20)	20	建议必填
            ccsj	Date  	出车时间	Date		必填
            ssdw	String	车俩所属单位名称	Varchar2 (50)	50	
            driverPhone	String	司机电话	Varchar2(15)	15	
            driverName	String	司机名称	Varchar2(10)	10	
            doctorPhone	String	医生电话	Varchar2(15)	15	
            doctorName	String	医生名称	Varchar2(10)	10	
            caseId	String	CaseID	Varchar2(50)	50	必填由APP提供
            */
            Hashtable ht = new Hashtable();
            ht.Add("messageName", "SendCarInfo");
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

            return ht;
        }

        public Hashtable GetCarStepHashtableByModel(CarState cs)
        {
            /*
             *  lsh;	String	流水号	Varchar2(19) not null	19	必填，主键，120生成
                caseId	String	CaseID	Varchar2(20)	20	由APP提供
                cccc	Integer	出车车次	Number(2)	2	必填
                clid	String	车俩ID	Varchar2(10) not null	10	必填
                sj	Date  	状态数据时间	Date		必填
                zt	String	状态	Varchar2(20)	20	必填,出车默认不需发送，主要有
                1.到达现场，2.病人上车，3.送达医院，4.任务完成。
                5.任务中止
                reason	String	任务中止异常原因描述	Varchar2(100)	100	1.用户来电取消，2车辆故障，3.病人已康复，4.车道人走，5.拒绝治疗，6.病人已死亡
             */
            Hashtable ht = new Hashtable();
            ht.Add("messageName", "CarStep");
            ht.Add("caseId", cs.CASEID);
            ht.Add("cccc", cs.CCCC);
            ht.Add("clid", cs.CLID);
            ht.Add("lsh", cs.LSH);
            ht.Add("sj", string.Format("{0:yyyy-MM-dd HH:mm:ss}", cs.SJ));
            ht.Add("zt", cs.ZT);
            ht.Add("reason", cs.TASK_TERMINATION_REASON);

            return ht;
        }

        public Hashtable GetCarLocationHashtableByModel(CarLocation cl)
        {
            /*
             * lsh;	String	流水号	Varchar2(19) not null	19	必填，主键，120生成
                caseId	String	CaseID	Varchar2(50)	50	必填，由APP提供
                cccc	Integer	出车车次	Number(2)	2	必填
                clid	String	车俩ID	Varchar2(10) not null	10	必填
                sj	Date  	坐标数据时间	Date		
                jd	String	经度	Varchar2(30)	30	
                wd	String	纬度	Varchar2(30)	30	
                sd	Double	速度	Number(8,3)	8	单位：KM/H
                fx	String	方向	Varchar2(20)	20	
             */
            Hashtable ht = new Hashtable();
            ht.Add("messageName", "CarLocation");
            ht.Add("caseId", cl.CASEID);
            ht.Add("cccc", cl.CCCC);
            ht.Add("clid", cl.CLID);
            ht.Add("fx", cl.FX);
            ht.Add("jd", cl.JD);
            ht.Add("wd", cl.WD);
            ht.Add("lsh", cl.LSH);
            ht.Add("sj", string.Format("{0:yyyy-MM-dd HH:mm:ss}", cl.SJ));
            ht.Add("sd", cl.SD);
            return ht;
        }

        public Hashtable GetQualityInfoHashtableByModel(ServiceQualityInfo sqi)
        {
            /*
            lsh;	String	服务流水号	Varchar2(50)，not null	50	必填,主键，接口服务端自动生成，并返回客户端
            caseId	String	CaseID	Varchar2(50)	50	必填,由APP提供
            cccc	Integer	出车车次	Number(2)	2	必填
            clid	String	车俩ID	Varchar2(10) not null	10	必填
            money	Double	服务费用	Number(8,3)	8	
            km	Double	公里数	Number(8,3)	8	
             */
            Hashtable ht = new Hashtable();
            ht.Add("messageName", "ServiceQualityInfo");
            ht.Add("caseId", sqi.CASEID);
            ht.Add("cccc", sqi.CCCC);
            ht.Add("clid", sqi.CLID);
            ht.Add("km", sqi.KM);
            ht.Add("lsh", sqi.LSH);
            ht.Add("money", sqi.MONEY);
            return ht;
        }
    }
}
