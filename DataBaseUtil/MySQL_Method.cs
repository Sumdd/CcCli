using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Common;
using MySql.Data.MySqlClient;
using Core_v1;

namespace DataBaseUtil {
    public class MySQL_Method {
        private static void PrepareSqlTextCommand(ref MySqlCommand cmd, string cmdText, MySqlParameter[] cmdParms, string m_sConnStr = null) {
            try {
                if (string.IsNullOrWhiteSpace(m_sConnStr))
                    cmd.Connection = new MySQLDBConnectionString().ConnectionString;
                else
                    cmd.Connection = new MySqlConnection(m_sConnStr);
                cmd.Connection.Open();
                cmd.CommandText = cmdText;
                cmd.CommandType = CommandType.Text;
                if(cmdParms != null) {
                    foreach(MySqlParameter parm in cmdParms)
                        cmd.Parameters.Add(parm);
                }
            } catch(Exception ex) {
                LogFile.Write(typeof(MySQL_Method), LOGLEVEL.ERROR, "查询数据库出错(" + cmdText + ")", ex);
            }
        }

        private static void PrepareProcedureCommand(ref MySqlCommand cmd, string cmdText, MySqlParameter[] cmdParms, string m_sConnStr = null) {
            if (string.IsNullOrWhiteSpace(m_sConnStr))
                cmd.Connection = new MySQLDBConnectionString().ConnectionString;
            else
                cmd.Connection = new MySqlConnection(m_sConnStr);
            cmd.CommandText = cmdText;
            cmd.CommandType = CommandType.StoredProcedure;
            if(cmdParms != null) {
                foreach(MySqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        /// <summary>
        /// execute sql and return the number of affected rows
        /// </summary>
        /// <param name="cmdText">cmdText</param>
        /// <param name="cmdParms">the number of affected rows</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string cmdText) {
            MySqlCommand cmd = new MySqlCommand();
            try {
                PrepareSqlTextCommand(ref cmd, cmdText, null);
                int val = cmd.ExecuteNonQuery();
                return val;
            } catch(Exception ex) {
                Log.Instance.Error($"[DataBaseUtil][MySQL_Method][ExecuteNonQuery][Exception][{ex.Message}]");
            } finally {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
                cmd.Dispose();
            }
            return 0;
        }

        /// <summary>
        /// execute sql with params and return the number of affected rows
        /// </summary>
        /// <param name="cmdText">cmdText</param>
        /// <param name="cmdParms">the number of affected rows</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string cmdText, MySqlParameter[] cmdParms, string m_sConnStr = null) {
            MySqlCommand cmd = new MySqlCommand();
            try {
                PrepareSqlTextCommand(ref cmd, cmdText, cmdParms, m_sConnStr);
                int val = cmd.ExecuteNonQuery();
                return val;
            } catch(Exception ex) {
                Log.Instance.Error($"[DataBaseUtil][MySQL_Method][ExecuteNonQuery][Exception][{ex.Message}]");
            } finally {
                cmd.Parameters.Clear();
                cmd.Connection.Close();
                cmd.Connection.Dispose();
                cmd.Dispose();
            }
            return 0;
        }

        /// <summary>
        ///  execute sql and return dataset
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string cmdText, string m_sConnStr = null) {
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataAdapter adpt = new MySqlDataAdapter();
            DataSet ds = new DataSet();
            try {
                PrepareSqlTextCommand(ref cmd, cmdText, null, m_sConnStr);
                adpt.SelectCommand = cmd;
                adpt.Fill(ds);
                return ds;
            } catch(Exception ex) {
                Log.Instance.Error($"[DataBaseUtil][MySQL_Method][ExecuteDataSet][Exception][{ex.Message}]");
            } finally {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
                cmd.Dispose();
                adpt.Dispose();
            }
            return ds;
        }

        /// <summary>
        ///  execute sql with params and return dataset
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string cmdText, MySqlParameter[] cmdParms) {
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataAdapter adpt = new MySqlDataAdapter();
            DataSet ds = new DataSet();
            try {
                PrepareSqlTextCommand(ref cmd, cmdText, cmdParms);
                adpt.SelectCommand = cmd;
                adpt.Fill(ds);
                return ds;
            } catch(Exception ex) {
                Log.Instance.Error($"[DataBaseUtil][MySQL_Method][ExecuteDataSet][Exception][{ex.Message}]");
            } finally {
                cmd.Parameters.Clear();
                cmd.Connection.Close();
                cmd.Connection.Dispose();
                cmd.Dispose();
                adpt.Dispose();
            }
            return ds;
        }

        /// <summary>
        /// execute sql with params and return datatable
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static DataTable BindTable(string cmdText, string m_sConnStr = null) {
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataAdapter adpt = new MySqlDataAdapter();
            DataSet ds = new DataSet();
            try {
                PrepareSqlTextCommand(ref cmd, cmdText, null, m_sConnStr);
                adpt.SelectCommand = cmd;
                adpt.Fill(ds);
                if(ds.Tables.Count > 0)
                    return ds.Tables[0];
                else
                    return new DataTable();
            } catch(Exception ex) {
                Log.Instance.Error($"[DataBaseUtil][MySQL_Method][BindTable][Exception][{cmdText}][{ex.Message}]");
                return new DataTable();
            } finally {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
                cmd.Dispose();
                adpt.Dispose();
            }
        }

        /// <summary>
        /// execute sql and return datatable
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static DataTable BindTable(string cmdText, MySqlParameter[] cmdParms) {
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataAdapter adpt = new MySqlDataAdapter();
            DataSet ds = new DataSet();
            try {
                PrepareSqlTextCommand(ref cmd, cmdText, cmdParms);
                adpt.SelectCommand = cmd;
                adpt.Fill(ds);
                return ds.Tables[0];
            } catch(Exception ex) {
                Log.Instance.Error($"[DataBaseUtil][MySQL_Method][BindTable][Exception][{ex.Message}]");
                return new DataTable();
            } finally {
                cmd.Parameters.Clear();
                cmd.Connection.Close();
                cmd.Connection.Dispose();
                cmd.Dispose();
                adpt.Dispose();
            }
        }

        /// <summary>
        /// execute sql and return DataReader
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static MySqlDataReader ExecuteDataReader(string cmdText) {
            MySqlCommand cmd = new MySqlCommand();
            try {
                PrepareSqlTextCommand(ref cmd, cmdText, null);
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            } catch(Exception ex) {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
                cmd.Dispose();
                Log.Instance.Error($"[DataBaseUtil][MySQL_Method][ExecuteDataReader][Exception][{ex.Message}]");
                throw;
            }
        }

        /// <summary>
        /// execute sql with params and return DataReader
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static MySqlDataReader ExecuteDataReader(string cmdText, MySqlParameter[] cmdParms) {
            MySqlCommand cmd = new MySqlCommand();
            try {
                PrepareSqlTextCommand(ref cmd, cmdText, cmdParms);
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            } catch(Exception ex) {
                cmd.Parameters.Clear();
                cmd.Connection.Close();
                cmd.Connection.Dispose();
                cmd.Dispose();
                Log.Instance.Error($"[DataBaseUtil][MySQL_Method][ExecuteDataReader][Exception][{ex.Message}]");
                throw;
            }
        }

        /// <summary>
        /// execute sql and return the frist row and frist column 
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string cmdText) {
            MySqlCommand cmd = new MySqlCommand();
            try {
                PrepareSqlTextCommand(ref cmd, cmdText, null);
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if(reader.HasRows && reader.Read())
                    return reader[0];
                else
                    return null;
            } catch(Exception ex) {
                Log.Instance.Error($"[DataBaseUtil][MySQL_Method][ExecuteScalar][Exception][{ex.Message}]");
                throw;
            } finally {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
                cmd.Dispose();
            }
        }

        /// <summary>
        /// execute sql with params and return the frist row and frist column 
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string cmdText, MySqlParameter[] cmdParms) {
            MySqlCommand cmd = new MySqlCommand();
            try {
                PrepareSqlTextCommand(ref cmd, cmdText, cmdParms);
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader[0];
            } catch(Exception ex) {
                Log.Instance.Error($"[DataBaseUtil][MySQL_Method][ExecuteScalar][Exception][{ex.Message}]");
                throw;
            } finally {
                cmd.Parameters.Clear();
                cmd.Connection.Close();
                cmd.Connection.Dispose();
                cmd.Dispose();
            }
        }

        /// <summary>
        ///  Execute Procedure and return dataset
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSetByProcedure(string cmdText) {
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataAdapter adpt = new MySqlDataAdapter();
            DataSet ds = new DataSet();
            try {
                PrepareProcedureCommand(ref cmd, cmdText, null);
                adpt.SelectCommand = cmd;
                adpt.Fill(ds);
                return ds;
            } catch(Exception ex) {
                Log.Instance.Error($"[DataBaseUtil][MySQL_Method][ExecuteDataSetByProcedure][Exception][{ex.Message}]");
            } finally {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
                cmd.Dispose();
                adpt.Dispose();
            }
            return ds;
        }

        /// <summary>
        ///  Execute Procedure with params and return dataset
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSetByProcedure(string cmdText, MySqlParameter[] cmdParms, string m_sConnStr = null) {
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataAdapter adpt = new MySqlDataAdapter();
            DataSet ds = new DataSet();
            try {
                PrepareProcedureCommand(ref cmd, cmdText, cmdParms, m_sConnStr);
                adpt.SelectCommand = cmd;
                adpt.Fill(ds);
                return ds;
            } catch(Exception ex) {
                Log.Instance.Error($"[DataBaseUtil][MySQL_Method][ExecuteDataSetByProcedure][Exception][{ex.Message}]");
            } finally {
                cmd.Parameters.Clear();
                cmd.Connection.Close();
                cmd.Connection.Dispose();
                cmd.Dispose();
                adpt.Dispose();
            }
            return ds;
        }

    }
}
