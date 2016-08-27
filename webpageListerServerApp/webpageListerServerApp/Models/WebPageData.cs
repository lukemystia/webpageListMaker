using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace webpageListerServerApp.Models
{
	/// <summary>
	/// chrome拡張機能から送られてくる情報格納用クラス
	/// </summary>
	[DataContract]
	public class WebPageData
	{
		/// <summary>
		/// 操作コマンド
		/// </summary>
		[DataMember(Name = "cmd")]
		public string Command { get; set; }

		/// <summary>
		/// webページ名
		/// </summary>
		[DataMember(Name = "title")]
		public string Title { get; set; }

		/// <summary>
		/// webページURL
		/// </summary>
		[DataMember(Name = "url")]
		public string URL { get; set; }

		/// <summary>
		/// 現在時刻
		/// </summary>
		public DateTime nowtime { get; set; }
	}
}
