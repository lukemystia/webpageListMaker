using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webpageListerServerApp.Models;
using webpageListerServerApp.Entity;

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
		public void logic()
		{
			
			switch (wpd.Command)
			{
				case "add":
					{
						var logic = new dbEntity();
						logic.ins(wpd);

						break;
					}
				case "get":

					break;

				case "del":

					break;
			}
		}
	}
}
