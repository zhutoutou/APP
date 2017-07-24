using System;
namespace ZIT.AppClient.Model
{
	/// <summary>
	/// APP呼入信息表
	/// </summary>
	[Serializable]
	public partial class AppCallInfo
	{
		public AppCallInfo()
		{}
		#region Model
		private string _caseid;
		private string _phone;
		private string _province;
		private string _city;
		private string _area;
		private DateTime? _calltime;
		private int _isself;
		private string _name;
        private int? _sex;
		private string _brithday;
        private int? _height;
        private decimal? _weight;
		private string _identitycard;
		private string _jd;
		private string _wd;
		private string _address;
		private string _medicalhistory;
		private string _contactway1;
		private string _contactway2;
		private string _contactway3;
		private string _medicalinsurancecard;
		private DateTime? _addtime;
		/// <summary>
		/// 主键，必填，由APP提供，GUID
		/// </summary>
		public string CASEID
		{
			set{ _caseid=value;}
			get{return _caseid;}
		}
		/// <summary>
		/// 主叫号码
		/// </summary>
		public string PHONE
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		/// <summary>
		/// 省（直辖市）
		/// </summary>
		public string PROVINCE
		{
			set{ _province=value;}
			get{return _province;}
		}
		/// <summary>
		/// 市
		/// </summary>
		public string CITY
		{
			set{ _city=value;}
			get{return _city;}
		}
		/// <summary>
		/// 区（县）
		/// </summary>
		public string AREA
		{
			set{ _area=value;}
			get{return _area;}
		}
		/// <summary>
		/// 必填，呼叫时间
		/// </summary>
		public DateTime? CALLTIME
		{
			set{ _calltime=value;}
			get{return _calltime;}
		}
		/// <summary>
		/// 必填，1：自己，2：他人
		/// </summary>
        public int ISSELF
		{
			set{ _isself=value;}
			get{return _isself;}
		}
		/// <summary>
		/// 姓名 可以是本人，可以是亲友，由呼叫人指定，路人时候为空
		/// </summary>
		public string NAME
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 可以是本人，可以是亲友，由呼叫人指定，路人时候为空【0：未知 1：男 2：女】
		/// </summary>
        public int? SEX
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 出生日期【年月日】
		/// </summary>
		public string BRITHDAY
		{
			set{ _brithday=value;}
			get{return _brithday;}
		}
		/// <summary>
		/// 厘米，可以是本人，可以是亲友，由呼叫人指定，路人时候为空
		/// </summary>
        public int? HEIGHT
		{
			set{ _height=value;}
			get{return _height;}
		}
		/// <summary>
		/// 千克，可以是本人，可以是亲友，由呼叫人指定，路人时候为空
		/// </summary>
        public decimal? WEIGHT
		{
			set{ _weight=value;}
			get{return _weight;}
		}
		/// <summary>
		/// 身份证号码 可以是本人，可以是亲友，由呼叫人指定，路人时候为空
		/// </summary>
		public string IDENTITYCARD
		{
			set{ _identitycard=value;}
			get{return _identitycard;}
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
		/// 呼叫人地址 报警电话的呼叫地址
		/// </summary>
		public string ADDRESS
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 既往病史
		/// </summary>
		public string MEDICALHISTORY
		{
			set{ _medicalhistory=value;}
			get{return _medicalhistory;}
		}
		/// <summary>
		/// 联系方式1
		/// </summary>
		public string CONTACTWAY1
		{
			set{ _contactway1=value;}
			get{return _contactway1;}
		}
		/// <summary>
		/// 联系方式2
		/// </summary>
		public string CONTACTWAY2
		{
			set{ _contactway2=value;}
			get{return _contactway2;}
		}
		/// <summary>
		/// 联系方式3
		/// </summary>
		public string CONTACTWAY3
		{
			set{ _contactway3=value;}
			get{return _contactway3;}
		}
		/// <summary>
		/// 医保卡号
		/// </summary>
		public string MEDICALINSURANCECARD
		{
			set{ _medicalinsurancecard=value;}
			get{return _medicalinsurancecard;}
		}
		/// <summary>
		/// 数据添加时间
		/// </summary>
		public DateTime? ADDTIME
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		#endregion Model

	}
}

