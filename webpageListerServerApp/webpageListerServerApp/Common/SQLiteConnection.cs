using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace webpageListerServerApp.Common
{
	/// <summary>
	/// SQLite接続
	/// </summary>
	public class SQLiteConnect
	{
		/// <summary>
		/// コネクション
		/// </summary>
		public readonly SQLiteConnection conn;

		/// <summary>
		/// コンストラクタ
		/// 接続する。開放を行うこと。
		/// </summary>
		public SQLiteConnect()
		{
			try
			{
				string dbConnectionStr = "Data Source=webPageList.db";
				conn = new SQLiteConnection(dbConnectionStr);

				conn.Open();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}

	/// <summary>
	/// パラメータセット
	/// </summary>
	public static class CreateSQLiteParameter
	{
		/// <summary>
		/// パラメータセット
		/// </summary>
		/// <param name="pName">パラメータ名</param>
		/// <param name="pDt">タイプ</param>
		/// <param name="pValue">値</param>
		/// <returns></returns>
		public static SQLiteParameter SetParam(string pName, DbType pDt, object pValue)
		{
			var param = new SQLiteParameter();

			param.ParameterName = pName;
			param.DbType = pDt;
			param.Value = pValue;

			return param;
		}
	}

}
