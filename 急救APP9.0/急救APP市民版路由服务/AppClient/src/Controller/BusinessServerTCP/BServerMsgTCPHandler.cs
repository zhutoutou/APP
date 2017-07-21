using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ZIT.LOG;
using ZIT.AppClient.Model;
using System.Collections;

namespace ZIT.AppClient.Controller.BusinessServer
    {
    /// <summary>
    /// 省交换消息处理类
    /// </summary>
    public class BServerMsgTCPHandler
    {


    }

    public class DesMsgArgs : EventArgs
    {
        //public string _msgName;
        public DesMsgType _desType;
        public string _data;

        public DesMsgArgs(DesMsgType desType, string data)
        {
            _desType = desType;
            _data = data;
        }

    }



}
