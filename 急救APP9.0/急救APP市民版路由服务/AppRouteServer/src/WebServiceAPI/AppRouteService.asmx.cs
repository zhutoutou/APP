using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using ZIT.AppRouteServer.WebServiceController;

namespace ZIT.AppRouteServer.WebServiceAPI
{
    /// <summary>
    /// AppRouteService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://www.zitsoft.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class AppRouteService : WebServiceBase
    {
        private CallInfo Call;

        private QualityComment Quality;

        public AppRouteService()
        {
            Call = new CallInfo();
            Quality = new QualityComment();
        }

        [WebMethod(Description = "Add call info")]
        [SoapHeader("Credentials")]
        public string addCallInfo(string callInfoJson)
        {
            base.VerifyCredentials();
            return Call.HandleAddCallInfo(callInfoJson);
        }

        [WebMethod(Description = "Add quality comment")]
        [SoapHeader("Credentials")]
        public string addQualityComment(string qualityCommentJson)
        {
            base.VerifyCredentials();
            return Quality.HandleAddQualityComment(qualityCommentJson);
        }
    }
}
