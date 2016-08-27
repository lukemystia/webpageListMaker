using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Runtime.Serialization;
using webpageListerServerApp.Models;

namespace webpageListerServerApp
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			InitializeComponent();
		}

		/// <summary>
		/// ソケット・リスナー
		/// </summary>
		private TcpListener myListener;

		/// <summary>
		/// クライアント送受信
		/// </summary>
		private ClientTcpIp[] myClient = new ClientTcpIp[4];

		/// <summary>
		/// フォームロード時のソケット接続処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form1_Load(object sender, EventArgs e)
		{
			// IPアドレス＆ポート番号設定
			var myPort = 8888;
			IPAddress myIp = Dns.GetHostEntry("localhost").AddressList[0];
			var myEndPoint = new IPEndPoint(myIp, myPort);

			// リスナー開始
			myListener = new TcpListener(myEndPoint);
			myListener.Start();

			// クライアント接続待ち開始
			Thread myServerThread = new Thread(new ThreadStart(ServerThread));
			myServerThread.Start();
		}

		/// <summary>
		/// フォームクローズ時のソケット切断処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			// リスナー終了
			myListener.Stop();

			// クライアント切断
			for (int i = 0; i <= myClient.GetLength(0) - 1; i++)
			{
				if (myClient[i] == null) continue;

				if (myClient[i].objSck.Connected)
				{
					// ソケットクローズ
					myClient[i].objStm.Close();
					myClient[i].objSck.Close();
				}
			}
		}


		/// <summary>
		/// クライアント接続待ちスレッド
		/// </summary>
		private void ServerThread()
		{
			try
			{
				var intNo = 0;
				while (true)
				{
					// ソケット接続待ち
					TcpClient myTcpClient = myListener.AcceptTcpClient();

					// クライアントから接続有り
					for (intNo = 0; intNo <= myClient.GetLength(0) - 1; intNo++)
					{
						if (myClient[intNo] == null) break;
						else if (!myClient[intNo].objSck.Connected) break;
					}

					if (intNo < myClient.GetLength(0))
					{
						// クライアント送受信オブジェクト生成
						myClient[intNo] = new ClientTcpIp();
						myClient[intNo].intNo = intNo + 1;
						myClient[intNo].objSck = myTcpClient;
						myClient[intNo].objStm = myTcpClient.GetStream();

						// クライアントとの送受信開始
						var myClientThread = new Thread(new ThreadStart(myClient[intNo].ReadWrite));
						myClientThread.Start();
					}
					else
					{
						// 接続拒否
						myTcpClient.Close();
					}
				}
			}
			catch (Exception ex)
			{

			}
		}
	}
}
