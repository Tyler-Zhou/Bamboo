using System;
using System.Data.SqlServerCe;

namespace ICP.DataSynchronization.ServiceInterface
{  
    /// <summary>
    /// Sqlserver ce数据库实体类
    /// </summary>
    public class CEDatabase
    {
        String dbName;
        SqlCeConnection connection;

        public String Name
        {
            get { return dbName; }
            set { dbName = value; }
        }
        String dbLocation;

        public String Location
        {
            get { return dbLocation; }
            set { dbLocation = value; }
        }
        CEDatabaseCreationMode creationMode;

        public CEDatabaseCreationMode CreationMode
        {
            get { return creationMode; }
            set { creationMode = value; }
        }

        public SqlCeConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    connection = new SqlCeConnection("Data Source=\"" + dbLocation + "\"");
                }
                return connection;
            }
        }
    }
}
