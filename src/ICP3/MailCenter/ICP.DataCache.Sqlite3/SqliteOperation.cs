using System.Collections.Generic;
using ICP.DataCache.ServiceInterface;
using System.Data.Common;
using System.Data.SQLite;
using System;
namespace ICP.DataOperation.Sqlite3
{
    /// <summary>
    /// Sqlite3缓存操作实现类
    /// </summary>
    public sealed class SqliteOperation : BaseDataOperation
    {
        public override void Init()
        {
            Init("test");
        }
        public override void Init(String dbConnectionString)
        {
            DataBaseOperation = new SQLiteDatabase(dbConnectionString);
        }

        public override void Init(Dictionary<String, String> config)
        {
            DataBaseOperation = new SQLiteDatabase(config);
        }
        public override DbParameter GetParameter()
        {
            return new SQLiteParameter();
        }
        public override DbParameter GetParameter(String name, object value)
        {
            return new SQLiteParameter(name, value);
        }
        public override DbParameter GetParameter(string name, System.Data.SqlDbType dbType)
        {
            return new SQLiteParameter(name);
           
        }
       
    }
}
