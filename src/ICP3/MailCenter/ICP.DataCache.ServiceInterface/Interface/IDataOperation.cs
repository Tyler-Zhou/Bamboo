using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.ServiceModel;

namespace ICP.DataCache.ServiceInterface
{
    /// <summary>
    ///Sqlite3缓存操作接口
    /// 使用时需首先调用Init方法初始化
    /// </summary>
    //[ICPServiceHost]
    //[ServiceContract]
    public interface IDataOperation
    {

        IDatabaseOperation DataBaseOperation { get; set; }
        /// <summary>
        /// 初始化
        /// 默认连接到test数据库
        /// </summary>
        ///[OperationContract]
        void Init();
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="connectionOpts">连接参数</param>
        [OperationContract(Name = "Init1")]
        void Init(Dictionary<String, String> connectionOpts);
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dbConnectionString">连接字符串</param>
        [OperationContract(Name = "Init2")]
        void Init(String dbConnectionString);
        /// <summary>
        /// 返回整表数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        ///[OperationContract]
        DataTable GetAllTableData(String tableName);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        ///[OperationContract]
        DataTable Get(String sql);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [OperationContract(Name = "Get1")]
        DataTable Get(String sql, List<DbParameter> parameters);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [OperationContract(Name = "Get2")]
        DataTable Get(String sql, DbParameter parameter);
        /// <summary>
        /// 执行Sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>受影响的行数</returns>
        ///[OperationContract]
        int ExecuteNonQuery(String sql);
        /// <summary>
        ///  执行Sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters">参数</param>
        /// <returns>受影响的行数</returns>
        [OperationContract(Name = "ExecuteNonQuery1")]
        int ExecuteNonQuery(String sql, List<DbParameter> parameters);
        /// <summary>
        /// 执行Sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>返回第一行第一列的值</returns>
        ///[OperationContract]
        String ExecuteScalar(String sql);
        /// <summary>
        /// 执行Sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns>返回第一行第一列的值</returns>
        [OperationContract(Name = "ExecuteScalar1")]
        String ExecuteScalar(String sql, List<DbParameter> parameters);

        /// <summary>
        /// 删除表数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        ///[OperationContract]
        bool Delete(String tableName, String where);
        /// <summary>
        /// 清空整个数据库数据
        /// </summary>
        /// <returns></returns>
        ///[OperationContract]
        bool ClearDB();
        /// <summary>
        /// 清空整表数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        ///[OperationContract]
        bool ClearTable(String tableName);
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fieldNames">需插入数据的字段名</param>
        /// <param name="source">数据表，表中的数据必须和给出的列名的顺序和数量一致</param>
        /// <returns></returns>
        ///[OperationContract]
        bool BulkInsert(String tableName, List<String> fieldNames, DataTable source);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        ///[OperationContract]
        bool Insert(String tableName, Dictionary<String, String> data);

        ///[OperationContract]
        DbParameter GetParameter();

        [OperationContract(Name = "GetParameter1")]
        DbParameter GetParameter(String name, object value);

        [OperationContract(Name = "GetParameter2")]
        DbParameter GetParameter(string name, SqlDbType dbType);


        #region Transaction
        /// <summary>
        /// 开始一个事务，以EndTransaction（）结束一个事务
        /// </summary>
        /// <returns></returns>
        ///[OperationContract]
        void BeginTransaction();
        /// <summary>
        /// 结束一个事务
        /// </summary>
        ///[OperationContract]
        void EndTransaction();

        /// <summary>
        /// 
        /// </summary>
        ///[OperationContract]
        void RollBackTransaction();
        /// <summary>
        /// 
        /// </summary>
        ///[OperationContract]
        void CommitTransaction();
        #endregion
    }
}
