﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZIT.AppRouteServer.Model;

namespace ZIT.AppRouteServer.DataAccess
{
    public interface IDBSendCarInfo
    {
        /// <summary>
		/// 增加一条数据
		/// </summary>
        bool Add(SendCarInfo model);
    }
}
