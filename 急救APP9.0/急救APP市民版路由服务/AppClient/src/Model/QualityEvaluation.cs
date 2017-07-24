using System;
namespace ZIT.AppClient.Model
{
	/// <summary>
	/// 服务质量评价表
	/// </summary>
	[Serializable]
	public partial class QualityEvaluation
	{
		public QualityEvaluation()
		{}
		#region Model
		private string _lsh;
		private string _qualitycomment;
		private string _reason;
		private string _caseid;
		private DateTime? _commenttime;
		/// <summary>
		/// 服务流水号
		/// </summary>
		public string LSH
		{
			set{ _lsh=value;}
			get{return _lsh;}
		}
		/// <summary>
		/// 服务质量评价  0：非常满意
		/// 1：满意
		/// 2：不满意
        /// 经沟通，不使用五星评价制
		/// </summary>
		public string QUALITYCOMMENT
		{
			set{ _qualitycomment=value;}
			get{return _qualitycomment;}
		}
		/// <summary>
		/// 原因   不满意时，必填项，满意时可不填写
		/// </summary>
		public string REASON
		{
			set{ _reason=value;}
			get{return _reason;}
		}
		/// <summary>
		/// 由APP提供
		/// </summary>
		public string CASEID
		{
			set{ _caseid=value;}
			get{return _caseid;}
		}
		/// <summary>
		/// 评价时间
		/// </summary>
		public DateTime? COMMENTTIME
		{
			set{ _commenttime=value;}
			get{return _commenttime;}
		}
		#endregion Model

	}
}

