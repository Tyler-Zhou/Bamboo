using System.Collections.Generic;
using ICP.DataCache.ServiceInterface1;
using System.Data.Common;
using System.Data.SqlServerCe;
using System.Reflection;
using System;
using System.IO;
using ICP.Framework.CommonLibrary.Client;
namespace ICP.DataOperation.SqlCE1
{
    public class SqlCEOperation : BaseDataOperation
    {
        public SqlCEOperation()
        {
            Init();
        }
        public override void Init()
        {
            string rootDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LocalData.UserInfo.LoginName);

            Init(System.IO.Path.Combine(rootDirectory, "ICP35DataCache.sdf"));
        }
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
      
    
    }
}
