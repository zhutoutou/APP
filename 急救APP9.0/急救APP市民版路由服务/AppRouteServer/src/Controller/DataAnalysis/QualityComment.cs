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
    public class QualityComment
    {
         private Thread td;

        private IDBQualityEvaluation QE;

        public QualityComment()
        {
            td = new Thread(new ThreadStart(Todo));
            QE = DataAccess.DataAccess.GetDBQualityEvaluation();
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
                    List<QualityEvaluation> list = QE.GetNewQualityEvaluation();
                    foreach (var item in list)
                    {
                        HandleQualityEvaluation(item);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog("程序异常!", ex);
                }
                Thread.Sleep(5 * 1000);
            }
        }

        private void HandleQualityEvaluation(QualityEvaluation qe)
        {
            try
            {
                IDBRouter router = DataAccess.DataAccess.GetDBRouter();
                string strUnitCode = QE.GetUnitCodeByCaseId(qe.CASEID);
                if (string.IsNullOrEmpty(strUnitCode))
                {
                    //send error msg to app server
                    Exception ex = new Exception("未找到对于的单位编码");
                    LogHelper.WriteLog("运行错误!", ex);
                }
                else
                {
                    ServerClient client = CoreService.GetInstance().CallInServer.GetServerClientByUnitCode(strUnitCode);
                    if (client != null)
                    {
                        //send msg to app client
                        string msg = CreateQualityEvaluationJsonByModel(qe);
                        client.SendMessage(new ScsTextMessage(msg));
                    }
                    else
                    {
                        Exception ex = new Exception("未找到该地区的服务通讯");
                        LogHelper.WriteLog("运行错误!", ex);
                     
                    }

                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("程序异常!", ex);
            }
        }

        private string CreateQualityEvaluationJsonByModel(QualityEvaluation qe)
        {
            Hashtable htQualityComment = new Hashtable();

            htQualityComment.Add("messageName", "QualityComment");
            htQualityComment.Add("lsh", qe.LSH);
            htQualityComment.Add("qualityComment", qe.QUALITYCOMMENT);
            htQualityComment.Add("reason", qe.REASON);
            htQualityComment.Add("caseId", qe.CASEID);
            htQualityComment.Add("commentTime", string.Format("{0:yyyy-MM-dd HH:mm:ss}", qe.COMMENTTIME));

            return JSON.Encode(htQualityComment);
        }


        public void Stop()
        {
            td.Abort();
        }
    }
}
