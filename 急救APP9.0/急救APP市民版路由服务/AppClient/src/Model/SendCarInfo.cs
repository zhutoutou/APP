using System;
namespace ZIT.AppClient.Model
{
	/// <summary>
	/// 出车信息表
	/// </summary>
	[Serializable]
	public partial class SendCarInfo
	{
		public SendCarInfo()
		{}
		#region Model
		private int? _id;
		private string _caseid;
		private string _lsh;
        private int? _cccc;
		private string _clid;
		private string _cph;
		private DateTime? _ccsj;
		private string _ssdw;
		private string _driverphone;
		private string _drivername;
		private string _doctorphone;
		private string _doctorname;
		/// <summary>
		/// 主键 自增1
		/// </summary>
		public int? ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 必填由APP提供
		/// </summary>
		public string CASEID
		{
			set{ _caseid=value;}
			get{return _caseid;}
		}
		/// <summary>
		/// 流水号 必填
		/// </summary>
		public string LSH
		{
			set{ _lsh=value;}
			get{return _lsh;}
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
		/// 车牌号
		/// </summary>
		public string CPH
		{
			set{ _cph=value;}
			get{return _cph;}
		}
		/// <summary>
		/// 出车时间
		/// </summary>
		public DateTime? CCSJ
		{
			set{ _ccsj=value;}
			get{return _ccsj;}
		}
		/// <summary>
		/// 车俩所属单位名称
		/// </summary>
		public string SSDW
		{
			set{ _ssdw=value;}
			get{return _ssdw;}
		}
		/// <summary>
		/// 司机电话
		/// </summary>
		public string DRIVERPHONE
		{
			set{ _driverphone=value;}
			get{return _driverphone;}
		}
		/// <summary>
		/// 司机名称
		/// </summary>
		public string DRIVERNAME
		{
			set{ _drivername=value;}
			get{return _drivername;}
		}
		/// <summary>
		/// 医生电话
		/// </summary>
		public string DOCTORPHONE
		{
			set{ _doctorphone=value;}
			get{return _doctorphone;}
		}
		/// <summary>
		/// 医生名称
		/// </summary>
		public string DOCTORNAME
		{
			set{ _doctorname=value;}
			get{return _doctorname;}
		}
		#endregion Model

	}
}

