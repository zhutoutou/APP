using System;
namespace ZIT.AppClient.Model
{
	/// <summary>
	/// 服务质量信息
	/// </summary>
	[Serializable]
	public partial class ServiceQualityInfo
	{
		public ServiceQualityInfo()
		{}
		#region Model
		private string _lsh;
		private string _caseid;
		private int? _cccc;
		private string _clid;
		private int? _timetaken;
		private int? _money;
		private int? _km;
		/// <summary>
		/// 流水号
		/// </summary>
		public string LSH
		{
			set{ _lsh=value;}
			get{return _lsh;}
		}
		/// <summary>
		/// CaseID
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
		/// 服务时长  多少分钟，由APP自行计算，此参数取消
		/// </summary>
		public int? TIMETAKEN
		{
			set{ _timetaken=value;}
			get{return _timetaken;}
		}
		/// <summary>
		/// 服务费用   单位：元
		/// </summary>
		public int? MONEY
		{
			set{ _money=value;}
			get{return _money;}
		}
		/// <summary>
		/// 公里数  单位：公里
		/// </summary>
		public int? KM
		{
			set{ _km=value;}
			get{return _km;}
		}
		#endregion Model

	}
}

