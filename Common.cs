//    HL株式会社社用管理システム
//
//機能：   12共通クラス
//作成者:　丁
//作成日： 2019/11/20
//現在バージョン：0.0.0.1
//最後更新日：2019/11/20
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace HL_塾管理
{
    internal class Common
    {
        public SqlConnection sqlCon = null;

        public Common()
        {
            DataBaseConnect();

            if (sqlCon == null)
            {
                throw new Exception("データベースの接続に失敗しました。設定ファイルapp.configを確認してください。");
            }
        }

        /// <summary>
        /// DB接続0519
        /// </summary>
        public void DataBaseConnect()
        {
            try
            {
                //設定ファイルから　ＤＢ接続文字列を取得する Todo取得失敗の場合　エラーログ
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                sqlCon = new SqlConnection(connectionString);

                //DBに接続する
                sqlCon.Open();
            }
            catch (Exception e)
            {
                //ログ出力
                MessageBox.Show("データベースの接続に失敗しました。設定ファイルapp.configを確認してください。");
                throw e;
            }
        }

        #region Sqlコマンド実行  検索系　トラザックションいらない

        /// <summary>
        /// Sqlコマンド実行
        /// </summary>
        /// <param name="sqlCmd"></param>
        /// <param name="paramList"></param>
        /// <returns>件数</returns>
        public int ExecuteIntSqlCmd(SqlConnection conn, string sqlCmd, params object[] paramList)
        {
            int result = 0;

            //Transaction
            SqlTransaction traction = conn.BeginTransaction();

            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlCmd;

                //Return Int型
                result = cmd.ExecuteNonQuery();

                traction.Commit();
            }
            catch (Exception ex)
            {
                //失敗の場合　ロールバック
                traction.Rollback();

                //エラーログ出力　ToDo
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (null != conn)
                {
                    //DB接続を閉じる
                    conn.Close();
                }
            }

            return result;
        }

        /// <summary>
        ///  Sqlコマンド実行
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="sqlCmd"></param>
        /// <returns>DataTable</returns>
        public DataTable ExecuteDataTableSqlCmd(SqlConnection conn, SqlCommand sqlCmd)
        {
            DataTable dt = new DataTable();

            try
            {
                if (sqlCmd.Parameters.Count > 0)
                {
                    foreach (SqlParameter param in sqlCmd.Parameters)
                    {
                        sqlCmd.CommandText = sqlCmd.CommandText.Replace(param.ParameterName, "'" + param.Value + "'");
                    }
                }

                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd.CommandText, conn);

                sqlDa.Fill(dt);
            }
            catch (Exception ex)
            {
                //エラーログ出力　ToDo
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (null != conn)
                {
                    //DB接続を閉じる
                    conn.Close();
                }
            }

            return dt;
        }

        #endregion Sqlコマンド実行  検索系　トラザックションいらない
    }

    public static class CommonConst
    {
        public const string ConnectionStringInValid = "データベースの接続に失敗しました。設定ファイルapp.configを確認してください。";
    }

    public sealed class DataContext
    {
        // TOD: Update Connection string section name
        private static string _connectionString = ConfigurationManager.ConnectionStrings["LocalConnectionString"].ConnectionString;
        private static DataContext _instance;

        public static DataContext Instance => _instance ?? (_instance = new DataContext());

        /// <summary>
        /// Constructor
        /// </summary>
        private DataContext()
        {
        }

        /// <summary>
        /// DB接続0519
        /// </summary>
        public void DataBaseConnect()
        {
            //try
            //{
            //    //設定ファイルから　ＤＢ接続文字列を取得する Todo取得失敗の場合　エラーログ


            //    _sqlConnection = new SqlConnection(connectionString);

            //    //DBに接続する
            //    _sqlConnection.Open();
            //}
            //catch (Exception e)
            //{
            //    //ログ出力
            //    MessageBox.Show(CommonConst.ConnectionStringInValid);
            //    throw e;
            //}
        }

        #region Sqlコマンド実行  検索系　トラザックションいらない

        /// <summary>
        /// Sqlコマンド実行
        /// </summary>
        /// <param name="sqlCmd"></param>
        /// <param name="paramList"></param>
        /// <returns>件数</returns>
        public int ExecuteIntSqlCmd(SqlConnection conn, string sqlCmd, params object[] paramList)
        {
            int result = 0;

            //Transaction
            SqlTransaction traction = conn.BeginTransaction();

            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlCmd;

                //Return Int型
                result = cmd.ExecuteNonQuery();

                traction.Commit();
            }
            catch (Exception ex)
            {
                //失敗の場合　ロールバック
                traction.Rollback();

                //エラーログ出力　ToDo
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //DB接続を閉じる
                conn?.Close();
            }

            return result;
        }

        /// <summary>
        ///  Sqlコマンド実行
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="sqlCmd"></param>
        /// <returns>DataTable</returns>
        public DataTable ExecuteDataTableSqlCmd(SqlConnection conn, SqlCommand sqlCmd)
        {
            DataTable dt = new DataTable();

            try
            {
                if (sqlCmd.Parameters.Count > 0)
                {
                    foreach (SqlParameter param in sqlCmd.Parameters)
                    {
                        sqlCmd.CommandText = sqlCmd.CommandText.Replace(param.ParameterName, "'" + param.Value + "'");
                    }
                }

                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd.CommandText, conn);

                sqlDa.Fill(dt);
            }
            catch (Exception ex)
            {
                //エラーログ出力　ToDo
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //DB接続を閉じる
                conn?.Close();
            }

            return dt;
        }

        #endregion Sqlコマンド実行  検索系　トラザックションいらない

        /// <summary>
        /// Execute query and return DataTable--------修正未完全　Linh 20200601
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataTable ExecuteQuery(string query)
        {
            // TODO: Handle ContextSwitchDeadlock
            DataTable result = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open connection
                    connection.Open();

                    // Execute query
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    dataAdapter.Fill(result);

                    // Close connection
                    connection?.Close();
                }
            }
            catch (InvalidOperationException i)
            {
                // Handle exception
                MessageBox.Show(CommonConst.ConnectionStringInValid + i.Message);
            }
            catch (Exception ex)
            {
                // Handle exception
                throw ex;
            }
            return result;
        }


        /// <summary>
        /// Execute query and return SqlDataReader
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public SqlDataReader ReaderExecuteQuery(string query)
        {
            SqlDataReader result = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // Open connection

                    connection.Open();

                    // Execute query
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    result = sqlCommand.ExecuteReader();

                    // Close connection
                    connection?.Close();
                }
            }
            catch (InvalidOperationException i)
            {
                // Handle exception
                MessageBox.Show(CommonConst.ConnectionStringInValid + i.Message);
            }
            catch (Exception ex)
            {
                // Handle exception
                throw ex;
            }

            return result;
        }
    }
}