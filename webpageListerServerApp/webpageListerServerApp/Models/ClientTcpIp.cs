using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LukeLibrary;
using webpageListerServerApp.Bizlogic;
using webpageListerServerApp.Models;

namespace webpageListerServerApp.Models
{
	// クライアント送受信クラス
	public class ClientTcpIp
	{
		public int intNo;
		public TcpClient objSck;
		public NetworkStream objStm;

		/// <summary>
		/// エンコード utf-8
		/// </summary>
		private static readonly Encoding UTF8 = Encoding.GetEncoding("utf-8");

		/// <summary>
		/// クライアント送受信
		/// </summary>
		public void ReadWrite()
		{
			try
			{
				while (true)
				{
					// ソケット受信
					var rdat = new Byte[1024];
					int ldat = objStm.Read(rdat, 0, rdat.GetLength(0));

					if (ldat > 0)
					{
						// クライアントからの受信データ有り
						// 送信データ作成
						var sdat = new Byte[ldat];
						Array.Copy(rdat, sdat, ldat);
						var msg = UTF8.GetString(sdat);

						// json部分を探す
						var jsonIdx = msg.IndexOf("json=");

						if (jsonIdx >= 0)
						{
							// json取り出し
							var json = msg.Substring(jsonIdx + "json=".Length);
							// jsonデシリアライズ
							var temp = json.LoadInJson<WebPageData>();
							temp.nowtime = DateTime.Now;

							// cmdによって処理する
							var logic = new ProcessBranch(temp);
							logic.logic();
						}

						sdat = UTF8.GetBytes(msg);
						// ソケット送信
						objStm.Write(sdat, 0, sdat.GetLength(0));
					}
					else
					{
						// ソケット切断有り
						// ソケットクローズ
						objStm.Close();
						objSck.Close();
						return;
					}
				}
			}
			catch (Exception ex)
			{

			}
		}
	}
}
