using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZIT.AppRouteServer.Model;

namespace ZIT.AppRouteServer.DataAccess
{

    public interface IDBQualityEvaluation
    {
        /// <summary>
		/// 增加一条数据
		/// </summary>
        bool Add(QualityEvaluation model);

        /// <summary>
        /// 获取最新的质量评价信息
        /// </summary>
        /// <returns></returns>
        List<QualityEvaluation> GetNewQualityEvaluation();

        /// <summary>
        /// 根据CASEID获取单位编号
        /// </summary>
        /// <param name="CaseId"></param>
        /// <returns></returns>
        string GetUnitCodeByCaseId(string CaseId);
    }
}
