using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZIT.AppRouteServer.Model;

namespace ZIT.AppRouteServer.DataAccess
{
    public interface IDBRouter
    {
        /// <summary>
        /// 根据注册的城市名获取单位行政编码
        /// </summary>
        /// <param name="province"></param>
        /// <param name="city"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        string GetUnitCodeByRegistCity(string province, string city, string area);

        /// <summary>
        /// 根据单位行政编码获取单位名称
        /// </summary>
        /// <param name="UnitCode"></param>
        /// <returns></returns>
        string GetUnitNameByUnitCode(string UnitCode);





    }
}
