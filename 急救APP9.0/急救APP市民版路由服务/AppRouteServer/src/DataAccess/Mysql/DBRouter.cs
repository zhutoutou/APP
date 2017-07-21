using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZIT.AppRouteServer.DataAccess;
using ZIT.AppRouteServer.Model;
using ZIT.AppRouteServer.Utility;
using MySql.Data.MySqlClient;

namespace ZIT.AppRouteServer.DataAccess.Mysql
{
    class DBRouter : IDBRouter
    {
        /// <summary>
        /// 根据注册的城市名获取单位行政编码
        /// </summary>
        /// <param name="province"></param>
        /// <param name="city"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        public string GetUnitCodeByRegistCity(string province, string city, string area)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select XZBM FROM Router ");
            strSql.Append(" where PROVINCE ='" + province + "' AND CITY='" + city + "' AND AREA='" + area + "' AND ISVALID=1 ");

            object obj = DbHelperMySQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }

        /// <summary>
        /// 根据单位行政编码获取单位名称
        /// </summary>
        /// <param name="UnitCode"></param>
        /// <returns></returns>
        public string GetUnitNameByUnitCode(string UnitCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UNITNAME FROM Router ");
            strSql.Append(" where XZBM ='" + UnitCode + "' AND ISVALID =1");

            object obj = DbHelperMySQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }
    }
}
