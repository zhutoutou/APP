using System;
namespace ZIT.AppClient.Model
{
	/// <summary>
	/// APP呼救信息错误表
	/// </summary>
	[Serializable]
	public partial class HandleCallError
	{
        public HandleCallError()
		{}
		#region Model
        private int? _id;
		private string _caseid;
		private string _errorcode;
		private string _errormsg;
		private DateTime? _errortime;
		/// <summary>
		/// 必填
		/// </summary>
		public int? ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 必填，由APP提供，GUID
		/// </summary>
		public string CASEID
		{
			set{ _caseid=value;}
			get{return _caseid;}
		}
		/// <summary>
		/// 错误码
		/// </summary>
		public string ERRORCODE
		{
			set{ _errorcode=value;}
			get{return _errorcode;}
		}
		/// <summary>
		/// 错误内容
		/// </summary>
		public string ERRORMSG
		{
			set{ _errormsg=value;}
			get{return _errormsg;}
		}
		/// <summary>
		/// 错误产生时间
		/// </summary>
		public DateTime? ERRORTIME
		{
			set{ _errortime=value;}
			get{return _errortime;}
		}
		#endregion Model

	}
}

