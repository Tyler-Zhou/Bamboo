using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlServerCe;
using System.IO;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.DataOperation.SqlCE
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlCEOperation : BaseDataOperation
    {

        /// <summary>
        /// 
        /// </summary>
        public SqlCEOperation()
        {
            Init();
        }
        /// <summary>
        /// 
        /// </summary>
        public override void Init()
        {
            string rootDirectory = string.Empty;
            if (LocalData.IsplugIn)
            {
                rootDirectory = Path.Combine(LocalData.MainPath, LocalData.UserInfo.LoginName.Trim());
            }
            else
            {
                rootDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LocalData.UserInfo.LoginName.Trim());
            }

            Init(Path.Combine(rootDirectory, "ICP35DataCache.sdf"));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbFileName"></param>
        public override void Init(String dbFileName)
        {
            DataBaseOperation = new SqlCEDatabase(dbFileName);
        }

        public override void Init(Dictionary<String, String> config)
        {
            DataBaseOperation = new SqlCEDatabase(config);
        }
        public override DbParameter GetParameter()
        {
            return new SqlCeParameter();
        }
        public override DbParameter GetParameter(String name, object value)
        {
            return new SqlCeParameter(name, value);

        }
        public override DbParameter GetParameter(string name, SqlDbType dbType)
        {
            return new SqlCeParameter(name, dbType);
        }


    }
}
