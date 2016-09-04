using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webpageListerServerApp.Models;
using webpageListerServerApp.Entity;
using LukeLibrary;

namespace webpageListerServerApp.Bizlogic
{
	/// <summary>
	/// chrome拡張の命令で処理分岐
	/// </summary>
	public class ProcessBranch
	{
		private WebPageData wpd;

		public ProcessBranch(WebPageData wpd)
		{
			this.wpd = wpd;
		}

		/// <summary>
		/// コマンドによって処理を分ける
		/// </summary>
		/// <returns>レスポンス文字列</returns>
		public string Logic()
		{
			try
			{
				switch (wpd.Command)
				{
					case "add":
						{
							var logic = new DbEntity();
							logic.Ins(wpd);

							return "0";
						}
					case "get":
						{
							var logic = new DbEntity();
							// 取得
							var getData = logic.Get(wpd);
							// 移動
							logic.Tohistory();
							// 消去
							logic.Del();

							return getData.SaveInJson<List<WebPageData>>();
						}
				}

				throw new Exception();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return "error";
			}
		}
	}
}
