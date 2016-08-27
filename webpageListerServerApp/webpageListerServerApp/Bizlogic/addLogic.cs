using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webpageListerServerApp.Models;
using webpageListerServerApp.Common;
using System.Data.SQLite;
using System.Data;
using static webpageListerServerApp.Common.CreateSQLiteParameter;

namespace webpageListerServerApp.Bizlogic
{
	public class addLogic
	{
		/// <summary>
		/// DBインサート
		/// </summary>
		public void ins(WebPageData wpd)
		{
			try
			{
				var dbConnection = new SQLiteConnect();

				using (dbConnection.conn)
				using (var transaction = dbConnection.conn.BeginTransaction())
				{
					var cmd = dbConnection.conn.CreateCommand();

					cmd.CommandText = "REPLACE INTO pages (title, url, date) VALUES (@TITLE, @URL, @DATE)";

					cmd.Parameters.Add(setParam("@TITLE", DbType.String, wpd.Title));
					cmd.Parameters.Add(setParam("@URL", DbType.String, wpd.URL));
					cmd.Parameters.Add(setParam("@DATE", DbType.String, wpd.nowtime.ToString()));

					cmd.ExecuteNonQuery();

					transaction.Commit();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}





	}
}
