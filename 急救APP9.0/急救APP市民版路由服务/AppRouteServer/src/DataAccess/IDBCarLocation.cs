using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZIT.AppRouteServer.Model;

namespace ZIT.AppRouteServer.DataAccess
{
    public interface IDBCarLocation
    {
        /// <summary>
		/// 增加一条数据
		/// </summary>
        bool Add(CarLocation model);
    }
}
