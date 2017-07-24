using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZIT.AppClient.Model
{
    //{"ID":"003"}
    public class Handshake
    {
        public string ID;
    }

    //{"ID":"003","Pwd":"123456"，"Type":"WX","Name":"微信服务入口"}
    public class Login
    {
        public string ID;
        public string Pwd;
        public string Type;
        public string Name;
    }
    
    //{"ID":"003","Result":"1"} 1:成功 -1:id错 -2:密码错 -3：类型错
    public class LoginResponse
    {
        public string ID;
        public int Result;
    }

    //{"ID":"003","Pwd":"123456"}
    public class Logoff
    {
        public string ID;
        public string Pwd;
    }

    //{"ID":"003","Result":"1"} 1:成功 -1:id错 -2:密码错
    public class LogoffResponse
    {
        public string ID;
        public int Result;
    }
}
