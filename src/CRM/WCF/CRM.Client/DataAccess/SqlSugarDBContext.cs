using SqlSugar;

namespace CRM.Client.DataAccess
{
    public class SqlSugarDBContext
    {
        public SqlSugarClient db;
        public SqlSugarDBContext()
        {
            if (db == null)
            {
                db = new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = "Data Source=.;Initial Catalog=CRM;User=sa;Password=DATABASE",//必填, 数据库连接字符串
                    DbType = DbType.SqlServer,         //必填, 数据库类型
                    IsAutoCloseConnection = true,       //默认false, 时候知道关闭数据库连接, 设置为true无需使用using或者Close操作
                    InitKeyType = InitKeyType.SystemTable    //默认SystemTable, 字段信息读取, 如：该属性是不是主键，是不是标识列等等信息
                });
            }
        }

    }
}
