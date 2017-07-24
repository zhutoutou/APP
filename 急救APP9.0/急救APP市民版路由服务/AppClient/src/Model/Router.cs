using System;
namespace ZIT.AppClient.Model
{
	/// <summary>
	/// 路由信息表
	/// </summary>
	[Serializable]
	public partial class Router
	{
		public Router()
		{}
		#region Model
		private int _id;
		private string _xzbm;
		private string _province;
		private string _city;
		private string _area;
		private int _isvalid;
		private DateTime? _addtime;
		private string _operator;
		/// <summary>
		/// 主键，自增1
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 行政编码
		/// </summary>
		public string XZBM
		{
			set{ _xzbm=value;}
			get{return _xzbm;}
		}
		/// <summary>
		/// 省
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
		/// 县（区）
		/// </summary>
		public string AREA
		{
			set{ _area=value;}
			get{return _area;}
		}
		/// <summary>
		/// 是否有效  0：无效  1：有效  默认1
		/// </summary>
		public int ISVALID
		{
			set{ _isvalid=value;}
			get{return _isvalid;}
		}
		/// <summary>
		/// 数据添加时间
		/// </summary>
		public DateTime? ADDTIME
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		/// <summary>
		/// 数据操作人
		/// </summary>
		public string OPERATOR
		{
			set{ _operator=value;}
			get{return _operator;}
		}
		#endregion Model

	}
}

