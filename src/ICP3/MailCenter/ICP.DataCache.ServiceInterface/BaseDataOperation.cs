using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;

namespace ICP.DataCache.ServiceInterface
{
    public abstract class BaseDataOperation : IDataOperation
    {
        public IDatabaseOperation DataBaseOperation { get; set; }
        #region ICacheOperation 成员

        public abstract void Init();

        public abstract void Init(String dbConnectionString);

        public abstract void Init(Dictionary<String, String> config);
        public abstract DbParameter GetParameter();
        public abstract DbParameter GetParameter(String name, object value);
        public abstract DbParameter GetParameter(string name, SqlDbType dbType);

        public DataTable GetAllTableData(String tableName)
        {
            CheckInit();
            return DataBaseOperation.GetData(tableName);
        }

        public DataTable Get(String sql)
        {
            CheckInit();
            return DataBaseOperation.GetDataTable(sql);
        }
        public DataTable Get(String sql, DbParameter parameter)
        {
            return Get(sql, new List<DbParameter>() { parameter });
        }
        public DataTable Get(String sql, List<DbParameter> parameters)
        {
            CheckInit();
            return DataBaseOperation.GetDataTable(sql, parameters);
        }

        public int ExecuteNonQuery(String sql)
        {
            CheckInit();
            return DataBaseOperation.ExecuteNonQuery(sql);
        }

        public int ExecuteNonQuery(String sql, List<DbParameter> parameters)
        {
            CheckInit();
            return DataBaseOperation.ExecuteNonQuery(sql, parameters);
        }

        public String ExecuteScalar(String sql)
        {
            CheckInit();
            return DataBaseOperation.ExecuteScalar(sql);

        }
        public String ExecuteScalar(String sql, List<DbParameter> parameters)
        {
            CheckInit();
            return DataBaseOperation.ExecuteScalar(sql, parameters);
        }
        public bool Delete(String tableName, String where)
        {
            CheckInit();
            return DataBaseOperation.Delete(tableName, where);
        }

        public bool ClearDB()
        {
            CheckInit();
            return DataBaseOperation.ClearDB();
        }

        public bool ClearTable(String table)
        {
            CheckInit();
            return DataBaseOperation.ClearTable(table);
        }
        private void CheckInit()
        {
            if (DataBaseOperation == null)
            {
                //throw new NullReferenceException("请先调用Init方法以初始化缓存环境");
                Init();
            }
        }
        public bool BulkInsert(String tableName, List<String> fieldNames, DataTable source)
        {
            CheckInit();
            return DataBaseOperation.BulkInsert(tableName, fieldNames, source);
        }
        public bool Insert(String tableName, Dictionary<String, String> data)
        {
            CheckInit();
            return DataBaseOperation.Insert(tableName, data);
        }
        public void BeginTransaction()
        {
            DataBaseOperation.BeginTransaction();
        }

        public void EndTransaction()
        {
            DataBaseOperation.EndTransaction();
        }

        /// <summary>
        /// 
        /// </summary>
        public void RollBackTransaction()
        {
            DataBaseOperation.RollBackTransaction();
        }

        /// <summary>
        /// 
        /// </summary>
        public void CommitTransaction()
        {
            DataBaseOperation.CommitTransaction();
        }
        #endregion
    }
}
