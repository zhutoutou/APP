using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZIT.AppRouteServer.Model;

namespace ZIT.AppRouteServer.DataAccess
{
    public interface IDBAppCallInfo
    {
        /// <summary>
		/// 增加一条数据
		/// </summary>
        bool Add(AppCallInfo model);

        /// <summary>
        /// 获取最新的呼救信息
        /// </summary>
        /// <returns></returns>
        List<AppCallInfo> GetNewCallInfo();

        /// <summary>
        /// 获取最新的呼救信息
        /// </summary>
        /// <returns></returns>
        AppCallInfo GetAppCallInfoByCaseId(string strCaseId);

    }
}
