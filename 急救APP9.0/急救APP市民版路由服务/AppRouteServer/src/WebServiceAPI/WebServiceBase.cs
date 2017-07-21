using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;

namespace ZIT.AppRouteServer.WebServiceAPI
{

    /// <summary>  
    /// WebService基类
    /// </summary>  
    public class WebServiceBase : System.Web.Services.WebService
    {
        private const string username = "120_user";
        private const string password = "120_password";

        private CredentialSoapHeader credentials;
        public CredentialSoapHeader Credentials
        {
            get
            {
                if (this.credentials == null)
                    this.credentials = new CredentialSoapHeader();
                return credentials;
            }
            set
            {
                credentials = value;
            }
        }
        protected void VerifyCredentials()
        {
            if (this.Credentials == null || string.IsNullOrEmpty(this.Credentials.Username) || string.IsNullOrEmpty(this.Credentials.Password))
            {
                throw new SoapException("请输入用户名或密码.", SoapException.ClientFaultCode, "用户登录");
            }
            bool result = (this.Credentials.Username == username && this.Credentials.Password == password);
            if (result == false)
            {
                throw new SoapException("用户名或密码错误.", SoapException.ClientFaultCode, "用户登录");
            }
        }
    }
}