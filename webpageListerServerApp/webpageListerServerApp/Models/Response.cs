using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LukeLibrary;
using webpageListerServerApp.Bizlogic;

namespace webpageListerServerApp.Models
{
	public class Response
	{
		private Socket mClient;

		public Response(Socket client)
		{
			mClient = client;
		}

		/// <summary>
		/// 応答開始
		/// </summary>
		public void Start()
		{
			try
			{
				var thread = new Thread(Run);
				thread.Start();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 応答実行
		/// </summary>
		public void Run()
		{
			try
			{
				var message = GetClientMessage();

				// json部分を探す
				var jsonIdx = message.IndexOf("json=");
				if (jsonIdx < 0) return;

				// jsonデシリアライズ
				var jsonData = message.Substring(jsonIdx + "json=".Length).LoadInJson<WebPageData>();
				jsonData.nowtime = DateTime.Now;

				// DB操作
				var logic = new ProcessBranch(jsonData);
				message = logic.Logic();

				// レスポンス
				SendResponse(message);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// メッセージ取得
		/// </summary>
		/// <returns></returns>
		private string GetClientMessage()
		{
			try
			{
				var buffer = new byte[4096];
				var recvLen = mClient.Receive(buffer);

				if (recvLen > 0)
				{
					return Encoding.UTF8.GetString(buffer, 0, recvLen);
				}
				else
				{
					return "";
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// レスポンス
		/// </summary>
		/// <param name="message"></param>
		private void SendResponse(string message)
		{
			try
			{
				var buffer = Encoding.UTF8.GetBytes(message);
				var contentLen = buffer.GetLength(0);

				// HTTPヘッダー生成
				var httpHeader = String.Format(
					"HTTP/1.1 200 OK\n" +
					"Content-type: text/html; charset=UTF-8\n" +
					"Content-length: {0}\n" +
					"\n",
					contentLen);

				var httpHeaderBuffer = new byte[4096];
				httpHeaderBuffer = Encoding.UTF8.GetBytes(httpHeader);

				// 応答内容送信
				mClient.Send(httpHeaderBuffer);
				mClient.Send(buffer);

				mClient.Close();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
