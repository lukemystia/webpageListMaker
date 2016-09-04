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
		/// フォームロード時のソケット接続処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form1_Load(object sender, EventArgs e)
		{
			try
			{
				// サーバーソケット初期化
				var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				var ip = IPAddress.Parse("127.0.0.1");
				var ipEndPoint = new IPEndPoint(ip, 8888);

				server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
				server.Bind(ipEndPoint);
				server.Listen(5);


				// 要求待ち
				while (true)
				{
					Socket client = server.Accept();

					var response = new Response(client);
					response.Start();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
