using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZIT.AppClient.Controller
{
    /// <summary>
    /// Des消息类型
    /// </summary>
    public enum DesMsgType { Handshake, LoginResponse, LogoffResponse, DispatchCarNotice, VehicleStatusResponse, VehiclePointResponse };

    //Handshake, LoginResponse, LogoffResponse, DispatchCarNotice, VehicleStatusResponse ,VehiclePointResponse




    /// <summary>
    /// Des接收的消息类
    /// </summary>
    public class DesMsg
    {
        //public string _xzbm;
        public DesMsgType _type;
        public object _data;
        public DateTime _dt;
        public string _strData;

        public DesMsg(DesMsgType type, object obj, string strData)
        {
            //_xzbm = xzbm;
            _type = type;
            _data = obj;
            _strData = strData;
            _dt = DateTime.Now;
        }
    }
}
