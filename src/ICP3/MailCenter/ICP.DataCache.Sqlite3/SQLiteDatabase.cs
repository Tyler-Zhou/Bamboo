using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Data.Common;
using System.Linq;
using ICP.DataCache.ServiceInterface;
namespace ICP.DataOperation.Sqlite3
{
    /// <summary>
    /// Sqlite3操作辅助类
    /// </summary>
    public sealed class SQLiteDatabase : IDatabaseOperation
    {
        public String ConnectionString { get; set; }
        private SQLiteConnection connection;
        /// <summary>
        /// 默认构造函数
        /// 默认数据库为test
        /// </summary>
        public SQLiteDatabase()
        {
            ConnectionString = @"Data Source=test";
        }
        public DbTransaction BeginTransaction()
        {
            OpenConnection();
            return connection.BeginTransaction();
        }

        public void EndTransaction(DbTransaction trans)
        {
            throw new NotImplementedException();
        }

        private SQLiteCommand _myCommand;
        public SQLiteCommand myCommand
        {
            get
            {
                if (myCommand == null)
                    _myCommand = new SQLiteCommand(connection);
                return _myCommand;
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="inputFile">数据库文件</param>
        public SQLiteDatabase(String inputFile)
        {
            ConnectionString = String.Format("Data Source={0}", inputFile);
            OpenConnection();
        }
        private void OpenConnection()
        {
            try
            {
                connection = new SQLiteConnection(ConnectionString);
                connection.Open();
            }
            catch (SQLiteException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionOpts">连接参数键值对</param>
        public SQLiteDatabase(Dictionary<String, String> connectionOpts)
        {
            String str = "";
            foreach (KeyValuePair<String, String> row in connectionOpts)
            {
                str += String.Format("{0}={1}; ", row.Key, row.Value);
            }
            str = str.Trim().Substring(0, str.Length - 1);
            ConnectionString = str;
            OpenConnection();
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
        /// 获取整表数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetData(String tableName)
        {
            String queryString = "select * from " + tableName;
            return GetDataTable(queryString);
        }
        public DataTable GetDataTable(String sql, List<DbParameter> parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                //SQLiteConnection cnn = new SQLiteConnection(ConnectionString);
                //cnn.Open();
                //SQLiteCommand mycommand = new SQLiteCommand(connection);
                myCommand.CommandText = sql;
                if (parameters != null && parameters.Count > 0)
                {
                    myCommand.Parameters.AddRange(parameters.ToArray());
                }
                SQLiteDataReader reader = myCommand.ExecuteReader();

                dt.Load(reader);
                reader.Close();
                reader.Dispose();
                myCommand.Parameters.Clear();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return dt;
        }

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
            //SQLiteConnection cnn = new SQLiteConnection(ConnectionString);
            //cnn.Open();            
            myCommand.CommandText = sql;
            if (parameters != null && parameters.Count > 0)
            {
                myCommand.Parameters.AddRange(parameters.ToArray());
            }
            int rowsUpdated = myCommand.ExecuteNonQuery();
            myCommand.Parameters.Clear();
            //cnn.Close();
            return rowsUpdated;
        }

        /// <summary>
        /// 执行SQL语句返回单个值
        /// </summary>
        /// <param name="sql">需执行的SQL语句</param>
        /// <returns>返回值;如果语句执行无返回值，则返回空字符串</returns>
        public String ExecuteScalar(String sql)
        {
            return ExecuteScalar(sql, null);
        }
        public String ExecuteScalar(String sql, List<DbParameter> parameters)
        {
            //SQLiteConnection cnn = new SQLiteConnection(ConnectionString);
            //cnn.Open();          
            myCommand.CommandText = sql;
            if (parameters != null && parameters.Count > 0)
            {
                myCommand.Parameters.AddRange(parameters.ToArray());
            }

            object value = myCommand.ExecuteScalar();
            myCommand.Parameters.Clear();
            // cnn.Close();
            if (value != null)
            {
                return value.ToString();
            }
            return String.Empty;
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
                foreach (KeyValuePair<String, String> val in data)
                {
                    vals += String.Format(" {0} = '{1}',", val.Key.ToString(), val.Value.ToString());
                }
                vals = vals.Substring(0, vals.Length - 1);
            }
            try
            {
                this.ExecuteNonQuery(String.Format("update {0} set {1} where {2};", tableName, vals, where));
            }
            catch
            {
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
                this.ExecuteNonQuery(String.Format("delete from {0} where {1};", tableName, where));
            }
            catch (Exception)
            {
                returnCode = false;
            }
            return returnCode;
        }

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

                this.ExecuteNonQuery(String.Format("insert into {0}({1}) values({2});", tableName, columns, values));
            }
            catch (Exception)
            {

                returnCode = false;
            }
            return returnCode;
        }
        public bool BulkInsert(String tableName, List<String> fieldNames, DataTable source)
        {
            if (source == null || source.Rows.Count <= 0)
                return true;
            //SQLiteConnection cnn = new SQLiteConnection(ConnectionString);


            // cnn.Open();
            using (DbTransaction dbTrans = connection.BeginTransaction())
            {
                myCommand.CommandText = "INSERT INTO @tableName(@fieldNames) VALUES(@value)";
                List<SQLiteParameter> parameters = new List<SQLiteParameter>();
                SQLiteParameter parameterTableName = new SQLiteParameter("@tableName", tableName);
                SQLiteParameter parameterFieldNames = new SQLiteParameter("@fieldNames", fieldNames.Aggregate((i, j) => i + "," + j));
                SQLiteParameter parameterValue = new SQLiteParameter("@value");
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
                    myCommand.ExecuteNonQuery();
                }
                dbTrans.Commit();
                myCommand.Parameters.Clear();
            }
            return true;

        }


        /// <summary>
        /// 清空当前数据库中的所有数据
        /// </summary>
        /// <returns>成功返回true,失败返回false</returns>
        public bool ClearDB()
        {
            DataTable tables;
            try
            {
                tables = this.GetDataTable("select NAME from SQLITE_MASTER where type='table' order by NAME;");
                foreach (DataRow table in tables.Rows)
                {
                    this.ClearTable(table["NAME"].ToString());
                }
                return true;
            }
            catch
            {
                return false;
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
                this.ExecuteNonQuery(String.Format("delete from {0};", table));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

