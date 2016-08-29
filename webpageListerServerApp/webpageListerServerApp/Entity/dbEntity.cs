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

namespace webpageListerServerApp.Entity
{
	/// <summary>
	/// DB操作関連
	/// </summary>
	public class dbEntity
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


		/// <summary>
		/// historyテーブルに移す
		/// </summary>
		public void tohistory()
		{
			try
			{
				var dbConnection = new SQLiteConnect();

				// TODO : コネクションは削除の方とまとめること
				using (dbConnection.conn)
				using (var transaction = dbConnection.conn.BeginTransaction())
				{
					var cmd = dbConnection.conn.CreateCommand();

					var sql = new StringBuilder();
					sql.Append("REPLACE INTO history (move_date, title, url, date)");
					sql.Append("SELECT @NOWTIME,title, url, date ");
					sql.Append("FROM pages");

					cmd.CommandText = sql.ToString();

					cmd.Parameters.Add(setParam("@NOWTIME", DbType.String, DateTime.Now.ToString()));

					cmd.ExecuteNonQuery();

					transaction.Commit();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// 消去
		/// </summary>
		public void del()
		{
			try
			{
				var dbConnection = new SQLiteConnect();

				// TODO : コネクションはコピーの方とまとめること
				using (dbConnection.conn)
				using (var transaction = dbConnection.conn.BeginTransaction())
				{
					var cmd = dbConnection.conn.CreateCommand();

					cmd.CommandText = "DELETE FROM	pages";

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
