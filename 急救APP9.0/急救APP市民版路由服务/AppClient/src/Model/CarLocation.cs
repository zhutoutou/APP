using System;
namespace ZIT.AppClient.Model
{
	/// <summary>
	/// 车辆轨迹表
	/// </summary>
	[Serializable]
	public partial class CarLocation
	{
		public CarLocation()
		{}
		#region Model
		private string _lsh;
		private string _caseid;
        private int? _cccc;
		private string _clid;
		private DateTime? _sj;
		private string _jd;
		private string _wd;
		private decimal? _sd;
		private string _fx;
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
		/// 坐标数据时间
		/// </summary>
		public DateTime? SJ
		{
			set{ _sj=value;}
			get{return _sj;}
		}
		/// <summary>
		/// 经度
		/// </summary>
		public string JD
		{
			set{ _jd=value;}
			get{return _jd;}
		}
		/// <summary>
		/// 纬度
		/// </summary>
		public string WD
		{
			set{ _wd=value;}
			get{return _wd;}
		}
		/// <summary>
		/// 速度   单位：KM/H
		/// </summary>
		public decimal? SD
		{
			set{ _sd=value;}
			get{return _sd;}
		}
		/// <summary>
		/// 方向   角度，垂直向上是0度，顺时针方向，角度增加
		/// </summary>
		public string FX
		{
			set{ _fx=value;}
			get{return _fx;}
		}
		#endregion Model

	}
}

