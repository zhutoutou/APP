using System;
namespace ZIT.AppClient.Model
{
	/// <summary>
	/// 车辆状态表
	/// </summary>
	[Serializable]
	public partial class CarState
	{
		public CarState()
		{}
		#region Model
		private int _id;
		private string _lsh;
		private string _caseid;
        private int? _cccc;
		private string _clid;
		private DateTime? _sj;
		private string _zt;
		private string _task_termination_reason;
		/// <summary>
		/// 主键 自增1
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 流水号
		/// </summary>
		public string LSH
		{
			set{ _lsh=value;}
			get{return _lsh;}
		}
		/// <summary>
		/// CaseID 由APP提供
		/// </summary>
		public string CASEID
		{
			set{ _caseid=value;}
			get{return _caseid;}
		}
		/// <summary>
		/// 出车车次
		/// </summary>
        public int? CCCC
		{
			set{ _cccc=value;}
			get{return _cccc;}
		}
		/// <summary>
		/// 车俩ID
		/// </summary>
		public string CLID
		{
			set{ _clid=value;}
			get{return _clid;}
		}
		/// <summary>
		/// 状态数据时间
		/// </summary>
		public DateTime? SJ
		{
			set{ _sj=value;}
			get{return _sj;}
		}
		/// <summary>
		/// 状态  出车默认不需发送，主要有
        /// 1.到达现场，2.病人上车，3.送达医院，4.任务完成。
        /// 5.任务中止
		/// </summary>
		public string ZT
		{
			set{ _zt=value;}
			get{return _zt;}
		}
		/// <summary>
		/// 任务中止异常原因描述  1.用户来电取消，2车辆故障，3.病人已康复，4.车道人走，5.拒绝治疗，6.病人已死亡
		/// </summary>
		public string TASK_TERMINATION_REASON
		{
			set{ _task_termination_reason=value;}
			get{return _task_termination_reason;}
		}
		#endregion Model

	}
}

