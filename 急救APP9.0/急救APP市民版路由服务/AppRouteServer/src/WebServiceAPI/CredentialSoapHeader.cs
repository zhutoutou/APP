using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;

namespace ZIT.AppRouteServer.WebServiceAPI
{
    /// <summary>  
    /// 用于身份验证的类  
    /// </summary>  
    public class CredentialSoapHeader : SoapHeader
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }  
}