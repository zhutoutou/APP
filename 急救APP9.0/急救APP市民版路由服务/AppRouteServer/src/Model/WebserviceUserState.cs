using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZIT.AppRouteServer.Model
{

    public class WebserviceUserState
    {
        private int _times;
        private string _message;

        /// <summary>
        /// 发送次数
        /// </summary>
        public int TIMES
        {
            set { _times = value; }
            get { return _times; }
        }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string MESSAGE
        {
            set { _message = value; }
            get { return _message; }
        }
    }
}
