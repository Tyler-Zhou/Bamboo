using ICP.DataCache.ServiceInterface;
using ICP.DataSynchronization.ServiceInterface;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading;

namespace ICP.DataOperation.SqlCE
{
    /// <summary>
    /// SQL CE Database Operation
    /// </summary>
    public class SqlCEDatabase : IDatabaseOperation
    {
        #region Member & Variables
        /// <summary>
        /// 
        /// </summary>
        public String ConnectionString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private SqlCeConnection _Connection;

        /// <summary>
        /// 
        /// </summary>
        private SqlCeCommand _Command;

        /// <summary>
        /// 
        /// </summary>
        private object objDbAccess = new object();
        /// <summary>
        /// 是否在事务中
        /// </summary>
        private bool _IsRunTransaction = false;
        #endregion

        #region Constructor
        /// <summary>
        /// 默认构造函数
        /// 默认数据库为ICP35DataCache.sdf
        /// </summary>
        public SqlCEDatabase()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbFileName">数据库文件</param>
        public SqlCEDatabase(String dbFileName)
        {
            try
            {
                ConnectionString = String.Format(DataSynchronizationUtility.ConnectionStringTemplate, dbFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionOpts">连接参数键值对</param>
        public SqlCEDatabase(Dictionary<String, String> connectionOpts)
        {
            try
            {
                String str = connectionOpts.Aggregate("", (current, row) => current + String.Format("{0}={1}; ", row.Key, row.Value));
                ConnectionString = str;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        #endregion

        #region Connection
        /// <summary>
        /// 
        /// </summary>
        public void OpenConnection()
        {
            if (_Connection == null)
            {
                _Connection = new SqlCeConnection(ConnectionString);
                _Command = _Connection.CreateCommand();
                _Command.Connection = _Connection;
                _Command.CommandType = CommandType.Text;
                
            }
            if(_Connection.State!=ConnectionState.Open)
                _Connection.Open();
        }

        private void CloseConnection()
        {
            if (_IsRunTransaction)
                return;
            if (_Connection != null && _Connection.State != ConnectionState.Closed)
            {
                _Connection.Close();
            }
        }
        #endregion

        #region Transaction
        /// <summary>
        /// 
        /// </summary>
        public void BeginTransaction()
        {
            OpenConnection();
            _Command.Transaction = _Connection.BeginTransaction();
            _IsRunTransaction = true;
        }
        /// <summary>
        /// 
        /// </summary>
        public void RollBackTransaction()
        {
            _Command.Transaction.Rollback();
        }
        /// <summary>
        /// 
        /// </summary>
        public void CommitTransaction()
        {
            _Command.Transaction.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        public void EndTransaction()
        {
            _IsRunTransaction = false;
            CloseConnection();
        }

        #endregion

        #region GetDataTable
        /// <summary>
        /// 获取整表数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetData(String tableName)
        {
            String queryString = "SELECT * FROM " + tableName;
            return GetDataTable(queryString);
        }
        /// <summary>
        /// 数据查询
        /// </summary>
        /// <param name="sql">查询SQL语句</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTable(String sql)
        {
            return GetDataTable(sql, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable GetDataTable(String sql, List<DbParameter> parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                _Command.CommandText = sql;
                if (parameters != null && parameters.Count > 0)
                {
                    _Command.Parameters.AddRange(parameters.ToArray());
                }
                using (SqlCeDataReader reader = _Command.ExecuteReader())
                {
                    dt.Load(reader);
                }
                _Command.Parameters.Clear();
            }
            catch (SqlCeException ex)
            {
                if (ex.NativeError == DataSynchronizationUtility.DataBaseCorruptCode)
                {
                    RepairDataBase("SQL CE GetDataTable", sql, CommonHelper.BuildExceptionString(ex));
                }
                    // 操作超时的错误信息
                else if (ex.Message.Contains("SQL Server Compact"))
                {
                    RepairDataBase("SQL CE GetDataTable", sql, CommonHelper.BuildExceptionString(ex));
                }
                    //SqlCeParameter  存在的错误信息
                else if (ex.Message.Contains("The SqlCeParameter with"))
                {
                    RepairDataBase("SQL CE GetDataTable", sql, CommonHelper.BuildExceptionString(ex));
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(sql + "\r\n" + CommonHelper.BuildExceptionString(ex));
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dt;
        }
        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql">需执行的SQL语句</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(String sql)
        {
            return ExecuteNonQuery(sql, null);
        }
        /// <summary>
        /// 执行带参数SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(String sql, List<DbParameter> parameters)
        {
            try
            {
                
                Monitor.Enter(objDbAccess);
                OpenConnection();
                _Command.CommandText = sql;
                if (parameters != null && parameters.Count > 0)
                {
                    _Command.Parameters.Clear();
                    _Command.Parameters.AddRange(parameters.ToArray());
                }
                int rowsUpdated = _Command.ExecuteNonQuery();
                _Command.Parameters.Clear();
                return rowsUpdated;
            }
            catch (SqlCeException ex)
            {
                if (ex.NativeError == DataSynchronizationUtility.DataBaseCorruptCode)
                {
                    RepairDataBase("SQL CE ExecuteNonQuery", sql, CommonHelper.BuildExceptionString(ex));
                    return ExecuteNonQuery(sql, parameters);
                }
                // 操作超时的错误信息
                else if (ex.Message.Contains("SQL Server Compact"))
                {
                    RepairDataBase("SQL CE ExecuteNonQuery", sql, CommonHelper.BuildExceptionString(ex));
                    return ExecuteNonQuery(sql, parameters);
                }
                //SqlCeParameter  存在的错误信息
                else if (ex.Message.Contains("The SqlCeParameter with"))
                {
                    RepairDataBase("SQL CE ExecuteNonQuery", sql, CommonHelper.BuildExceptionString(ex));
                    return ExecuteNonQuery(sql, parameters);
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(sql + "\r\n" + CommonHelper.BuildExceptionString(ex));
                throw ex;
            }
            finally
            {
                CloseConnection();
                Monitor.Exit(objDbAccess);
            }
        }

        /// <summary>
        /// 更新表数据
        /// </summary>
        /// <param name="tableName">需更新的表名</param>
        /// <param name="data">列名和目标值键值对</param>
        /// <param name="where">查询条件</param>
        /// <returns>成功返回true,失败返回false</returns>
        public bool Update(String tableName, Dictionary<String, String> data, String where)
        {
            String vals = "";
            Boolean returnCode = true;
            if (data != null && data.Count >= 1)
            {
                vals = data.Aggregate(vals, (current, val) => current + String.Format(" {0} = '{1}',", val.Key.ToString(), val.Value.ToString()));
                vals = vals.Substring(0, vals.Length - 1);
            }
            try
            {
                ExecuteNonQuery(String.Format("update {0} set {1} where {2};", tableName, vals, where));
            }
            catch
            {
                returnCode = false;
            }
            return returnCode;
        }
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// 执行SQL语句返回单个值
        /// </summary>
        /// <param name="sql">需执行的SQL语句</param>
        /// <returns>返回值;如果语句执行无返回值，则返回空字符串</returns>
        public String ExecuteScalar(String sql)
        {
            return ExecuteScalar(sql, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public String ExecuteScalar(String sql, List<DbParameter> parameters)
        {
            try
            {
                OpenConnection();
                _Command.CommandText = sql;
                if (parameters != null && parameters.Count > 0)
                {
                    _Command.Parameters.Clear();
                    _Command.Parameters.AddRange(parameters.ToArray());
                }

                object value = _Command.ExecuteScalar();
                _Command.Parameters.Clear();
                if (value != null)
                {
                    return value.ToString();
                }
                return String.Empty;
            }
            catch (SqlCeException ex)
            {
                if (ex.NativeError == DataSynchronizationUtility.DataBaseCorruptCode)
                {
                    RepairDataBase("SQL CE ExecuteScalar", sql, CommonHelper.BuildExceptionString(ex));
                    //DataSynchronizationUtility.RepairDataBase(ConnectionString);
                    return ExecuteScalar(sql, parameters);
                }
                    // 操作超时的错误信息
                else if (ex.Message.Contains("SQL Server Compact"))
                {
                    RepairDataBase("SQL CE ExecuteScalar", sql, CommonHelper.BuildExceptionString(ex));
                    //DataSynchronizationUtility.RepairDataBase(ConnectionString);
                    return ExecuteScalar(sql, parameters);
                }
                    //SqlCeParameter  存在的错误信息
                else if (ex.Message.Contains("The SqlCeParameter with"))
                {
                    RepairDataBase("SQL CE ExecuteScalar", sql, CommonHelper.BuildExceptionString(ex));
                    //DataSynchronizationUtility.RepairDataBase(ConnectionString);
                    return ExecuteScalar(sql, parameters);
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(sql + "\r\n" + CommonHelper.BuildExceptionString(ex));
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }
        #endregion

        #region Other
        /// <summary>
        /// 指定列名插入表数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="data">列名列值键值对</param>
        /// <returns>成功返回true,失败返回false</returns>
        public bool Insert(String tableName, Dictionary<String, String> data)
        {
            String columns = "";
            String values = "";
            Boolean returnCode = true;
            foreach (KeyValuePair<String, String> val in data)
            {
                columns += String.Format(" {0},", val.Key.ToString());
                values += String.Format(" '{0}',", val.Value);
            }
            columns = columns.Substring(0, columns.Length - 1);
            values = values.Substring(0, values.Length - 1);
            try
            {
                ExecuteNonQuery(String.Format("INSERT INTO {0}({1}) VALUES({2});", tableName, columns, values));
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog(CommonHelper.BuildExceptionString(ex));
                returnCode = false;
            }
            return returnCode;
        }

        /// <summary>
        ///  删除表数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="where">过滤行数据SQL</param>
        /// <returns>删除成功返回true,失败返回false</returns>
        public bool Delete(String tableName, String where)
        {
            Boolean returnCode = true;
            try
            {
                ExecuteNonQuery(String.Format("delete from {0} where {1};", tableName, where));
            }
            catch (Exception)
            {
                returnCode = false;
            }
            return returnCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldNames"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public bool BulkInsert(String tableName, List<String> fieldNames, DataTable source)
        {
            if (source == null || source.Rows.Count <= 0)
                return true;
            SqlCeConnection conn = null;
            DbTransaction dbTrans = null;
            try
            {
                Monitor.Enter(objDbAccess);
                conn = new SqlCeConnection(ConnectionString);
                conn.Open();
                //OpenConnection();
                dbTrans = conn.BeginTransaction();
                SqlCeCommand cmd = new SqlCeCommand();
                cmd.CommandText = "INSERT INTO @tableName(@fieldNames) VALUES(@value)";
                cmd.Connection = conn;
                List<SqlCeParameter> parameters = new List<SqlCeParameter>();
                SqlCeParameter parameterTableName = new SqlCeParameter("@tableName", tableName);
                SqlCeParameter parameterFieldNames = new SqlCeParameter("@fieldNames",
                                                                        fieldNames.Aggregate(
                                                                            (i, j) => i + "," + j));
                SqlCeParameter parameterValue = new SqlCeParameter("@value", null);
                parameters.Add(parameterTableName);
                parameters.Add(parameterFieldNames);
                String values = "";
                foreach (DataRow row in source.Rows)
                {
                    foreach (DataColumn column in source.Columns)
                    {
                        values += String.Format(" '{0}',", row[column]);
                    }
                    parameterValue.Value = values;
                    cmd.ExecuteNonQuery();

                }
                dbTrans.Commit();
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                dbTrans.Rollback();
                throw ex;
            }
            finally
            {
                EndTransaction();
                if (conn != null)
                    conn.Close();
                Monitor.Exit(objDbAccess);
            }
            return true;
        }

        /// <summary>
        /// 清空当前数据库中的所有数据
        /// </summary>
        /// <returns>成功返回true,失败返回false</returns>
        public bool ClearDB()
        {
            lock (objDbAccess)
            {
                DataTable tables;
                try
                {
                    tables = GetDataTable("select NAME from SQLITE_MASTER where type='table' order by NAME;");
                    foreach (DataRow table in tables.Rows)
                    {
                        ClearTable(table["NAME"].ToString());
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 清空表数据
        /// </summary>
        /// <param name="table">表名</param>
        /// <returns>成功返回true,失败返回false</returns>
        public bool ClearTable(String table)
        {
            try
            {
                ExecuteNonQuery(String.Format(" DELETE FROM {0};", table));
                return true;
            }
            catch
            {
                return false;
            }
        } 
        #endregion

        #region Repair DataBase
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MethodName"></param>
        /// <param name="executeSql"></param>
        /// <param name="exceptionString"></param>
        private void RepairDataBase(string MethodName, string executeSql, string exceptionString)
        {
            Logger.Log.Error(string.Format(
                "Method:{0}\r\nExecute Sql:{1}\r\nException:{2}", MethodName, executeSql, exceptionString));
            DataSynchronizationUtility.RepairDataBase(ConnectionString);
        } 
        #endregion

        #region Comment Code
        //public DbTransaction BeginTransaction()
        //{
        //    try
        //    {
        //        SqlCeConnection conn = new SqlCeConnection(ConnectionString);
        //        conn.Open();
        //        return conn.BeginTransaction();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
        //        throw ex;
        //    }
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="trans"></param>
        //public void EndTransaction(DbTransaction trans)
        //{
        //    if (trans != null)
        //    {
        //        if (trans.Connection.State != ConnectionState.Closed)
        //            trans.Connection.Close();
        //        trans.Dispose();
        //        trans = null;
        //    }
        //}

        //private SqlCeCommand _myCommand;
        //public SqlCeCommand mycommand
        //{
        //    get
        //    {
        //        if (_myCommand != null)
        //            return _myCommand;
        //        else
        //        {
        //            _myCommand = new SqlCeCommand();
        //            return _myCommand;
        //        }
        //    }
        //}
        //        /// <summary>
        //        /// 数据查询
        //        /// </summary>
        //        /// <param name="sql">查询SQL语句</param>
        //        /// <returns>数据表</returns>
        //        public DataTable GetDataTable(String sql)
        //        {
        //            return GetDataTable(sql, null);
        //        }
        //        /// <summary>
        //        /// 获取整表数据
        //        /// </summary>
        //        /// <param name="tableName"></param>
        //        /// <returns></returns>
        //        public DataTable GetData(String tableName)
        //        {
        //            String queryString = "select * from " + tableName;
        //            return GetDataTable(queryString);
        //        }
        //        public DataTable GetDataTable(String sql, List<DbParameter> parameters)
        //        {
        //            DataTable dt = new DataTable();
        //            SqlCeConnection cnn = null;
        //            try
        //            {
        //                cnn = new SqlCeConnection(ConnectionString);
        //                cnn.Open();
        //                SqlCeCommand cmd = new SqlCeCommand();
        //                cmd.Connection = cnn;
        //                cmd.CommandText = sql;

        //                if (parameters != null && parameters.Count > 0)
        //                    cmd.Parameters.AddRange(parameters.ToArray());

        //                using (SqlCeDataReader reader = cmd.ExecuteReader())
        //                {
        //                    dt.Load(reader);
        //                }
        //                cmd.Parameters.Clear();
        //            }
        //            catch (SqlCeException ex)
        //            {
        //                if (ex.NativeError == DataSynchronizationUtility.DataBaseCorruptCode)
        //                {
        //                    RepairDataBase("SQL CE GetDataTable", sql, CommonHelper.BuildExceptionString(ex));
        //                    //DataSynchronizationUtility.RepairDataBase(ConnectionString);
        //                }
        //                // 操作超时的错误信息
        //                else if (ex.Message.Contains("SQL Server Compact"))
        //                {
        //                    RepairDataBase("SQL CE GetDataTable", sql, CommonHelper.BuildExceptionString(ex));
        //                    //DataSynchronizationUtility.RepairDataBase(ConnectionString);
        //                }
        //                //SqlCeParameter  存在的错误信息
        //                else if (ex.Message.Contains("The SqlCeParameter with"))
        //                {
        //                    RepairDataBase("SQL CE GetDataTable", sql, CommonHelper.BuildExceptionString(ex));
        //                    //DataSynchronizationUtility.RepairDataBase(ConnectionString);
        //                }
        //                else
        //                {
        //                    throw ex;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Logger.Log.Error(sql + "\r\n" + CommonHelper.BuildExceptionString(ex));
        //                throw ex;
        //            }
        //            finally
        //            {
        //                if (cnn != null) cnn.Close();
        //            }
        //            return dt;
        //        }

        //        /// <summary>
        //        /// 执行SQL语句
        //        /// </summary>
        //        /// <param name="sql">需执行的SQL语句</param>
        //        /// <returns>受影响的行数</returns>
        //        public int ExecuteNonQuery(String sql)
        //        {
        //            return ExecuteNonQuery(sql, null);
        //        }
        //        /// <summary>
        //        /// 执行带参数SQL语句
        //        /// </summary>
        //        /// <param name="sql"></param>
        //        /// <param name="parameters"></param>
        //        /// <returns></returns>
        //        public int ExecuteNonQuery(String sql, List<DbParameter> parameters)
        //        {
        //            int rowsUpdated = -1;
        //            SqlCeConnection cnn = null;
        //            try
        //            {
        //                Monitor.Enter(objDbAccess);
        //                cnn = new SqlCeConnection(ConnectionString);
        //                cnn.Open();
        //                SqlCeCommand cmd = new SqlCeCommand();
        //                cmd.Connection = cnn;
        //                cmd.CommandText = sql;
        //                if (parameters != null && parameters.Count > 0)
        //                {
        ////                     System.Diagnostics.Debug.Assert(cmd.Parameters.Count == 0);
        //                    cmd.Parameters.Clear();
        //                    cmd.Parameters.AddRange(parameters.ToArray());
        //                }
        //                rowsUpdated = cmd.ExecuteNonQuery();
        //                cmd.Parameters.Clear();
        //                return rowsUpdated;
        //            }
        //            catch (SqlCeException ex)
        //            {
        //                if (ex.NativeError == DataSynchronizationUtility.DataBaseCorruptCode)
        //                {
        //                    RepairDataBase("SQL CE ExecuteNonQuery", sql, CommonHelper.BuildExceptionString(ex));
        //                    //DataSynchronizationUtility.RepairDataBase(ConnectionString);
        //                    return ExecuteNonQuery(sql, parameters);
        //                }
        //                // 操作超时的错误信息
        //                else if (ex.Message.Contains("SQL Server Compact"))
        //                {
        //                    RepairDataBase("SQL CE ExecuteNonQuery", sql, CommonHelper.BuildExceptionString(ex));
        //                    //DataSynchronizationUtility.RepairDataBase(ConnectionString);
        //                    return ExecuteNonQuery(sql, parameters);
        //                }
        //                //SqlCeParameter  存在的错误信息
        //                else if (ex.Message.Contains("The SqlCeParameter with"))
        //                {
        //                    RepairDataBase("SQL CE ExecuteNonQuery", sql, CommonHelper.BuildExceptionString(ex));
        //                    //DataSynchronizationUtility.RepairDataBase(ConnectionString);
        //                    return ExecuteNonQuery(sql, parameters);
        //                }
        //                else
        //                {
        //                    throw ex;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Logger.Log.Error(sql + "\r\n" + CommonHelper.BuildExceptionString(ex));
        //                throw ex;
        //            }
        //            finally
        //            {
        //                if(cnn != null) cnn.Close();
        //                Monitor.Exit(objDbAccess);
        //            }
        //        }

        //        /// <summary>
        //        /// 执行SQL语句返回单个值
        //        /// </summary>
        //        /// <param name="sql">需执行的SQL语句</param>
        //        /// <returns>返回值;如果语句执行无返回值，则返回空字符串</returns>
        //        public String ExecuteScalar(String sql)
        //        {
        //            return ExecuteScalar(sql, null);
        //        }
        //        public String ExecuteScalar(String sql, List<DbParameter> parameters)
        //        {
        //            SqlCeConnection conn = null;
        //            try
        //            {
        //                conn = new SqlCeConnection(ConnectionString);
        //                conn.Open();
        //                //OpenConnection();
        //                SqlCeCommand cmd = new SqlCeCommand();
        //                cmd.Connection = conn;
        //                cmd.CommandText = sql;
        //                if (parameters != null && parameters.Count > 0)
        //                {
        //                    cmd.Parameters.Clear();
        //                    cmd.Parameters.AddRange(parameters.ToArray());
        //                }

        //                object value = cmd.ExecuteScalar();
        //                cmd.Parameters.Clear();
        //                if (value != null)
        //                {
        //                    return value.ToString();
        //                }
        //                return String.Empty;
        //            }
        //            catch (SqlCeException ex)
        //            {
        //                if (ex.NativeError == DataSynchronizationUtility.DataBaseCorruptCode)
        //                {
        //                    RepairDataBase("SQL CE ExecuteScalar", sql, CommonHelper.BuildExceptionString(ex));
        //                    //DataSynchronizationUtility.RepairDataBase(ConnectionString);
        //                    return ExecuteScalar(sql, parameters);
        //                }
        //                // 操作超时的错误信息
        //                else if (ex.Message.Contains("SQL Server Compact"))
        //                {
        //                    RepairDataBase("SQL CE ExecuteScalar", sql, CommonHelper.BuildExceptionString(ex));
        //                    //DataSynchronizationUtility.RepairDataBase(ConnectionString);
        //                    return ExecuteScalar(sql, parameters);
        //                }
        //                //SqlCeParameter  存在的错误信息
        //                else if (ex.Message.Contains("The SqlCeParameter with"))
        //                {
        //                    RepairDataBase("SQL CE ExecuteScalar", sql, CommonHelper.BuildExceptionString(ex));
        //                    //DataSynchronizationUtility.RepairDataBase(ConnectionString);
        //                    return ExecuteScalar(sql, parameters);
        //                }
        //                else
        //                {
        //                    throw ex;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Logger.Log.Error(sql+"\r\n"+CommonHelper.BuildExceptionString(ex));
        //                throw ex;
        //            }
        //            finally
        //            {
        //                if(conn != null) conn.Close();
        //            }
        //        }

        //        /// <summary>
        //        /// 更新表数据
        //        /// </summary>
        //        /// <param name="tableName">需更新的表名</param>
        //        /// <param name="data">列名和目标值键值对</param>
        //        /// <param name="where">查询条件</param>
        //        /// <returns>成功返回true,失败返回false</returns>
        //        public bool Update(String tableName, Dictionary<String, String> data, String where)
        //        {
        //            String vals = "";
        //            Boolean returnCode = true;
        //            if (data != null && data.Count >= 1)
        //            {
        //                foreach (KeyValuePair<String, String> val in data)
        //                {
        //                    vals += String.Format(" {0} = '{1}',", val.Key.ToString(), val.Value.ToString());
        //                }
        //                vals = vals.Substring(0, vals.Length - 1);
        //            }
        //            try
        //            {
        //                ExecuteNonQuery(String.Format("update {0} set {1} where {2};", tableName, vals, where));
        //            }
        //            catch
        //            {
        //                returnCode = false;
        //            }
        //            return returnCode;
        //        } 
        #endregion
    }
}
