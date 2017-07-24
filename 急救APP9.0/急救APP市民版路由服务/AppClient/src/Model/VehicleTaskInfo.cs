using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZIT.AppClient.Model
{
    public class VehicleTaskInfo
    {
        /// <summary>
        /// case id
        /// </summary>
        public string CaseId
        {
            get;
            set;
        }
        /// <summary>
        /// 出车车次
        /// </summary>
        public int? CCCC
        {
            get;
            set;
        }
        /// <summary>
        /// 流水号
        /// </summary>
        public string LSH
        {
            get;
            set;
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime
        {
            get;
            set;
        }
    }
}
