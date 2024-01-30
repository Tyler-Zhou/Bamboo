using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace ICP.DataCache.ServiceInterface1
{ 
    /// <summary>
    ///Sqlite3缓存操作接口
    /// 使用时需首先调用Init方法初始化
    /// </summary>
  public  interface IDataOperation
    {
      IDatabaseOperation DataBaseOperation { get; set; }
      /// <summary>
      /// 初始化
      /// 默认连接到test数据库
      /// </summary>
      void Init();
      /// <summary>
      /// 初始化
      /// </summary>
      /// <param name="connectionOpts">连接参数</param>
      void Init(Dictionary<String, String> connectionOpts);
      /// <summary>
      /// 初始化
      /// </summary>
      /// <param name="dbConnectionString">连接字符串</param>
      void Init(String dbConnectionString);
      /// <summary>
      /// 返回整表数据
      /// </summary>
      /// <param name="tableName"></param>
      /// <returns></returns>
      DataTable GetAllTableData(String tableName);
      /// <summary>
      /// 查询数据
      /// </summary>
      /// <param name="sql"></param>
      /// <returns></returns>
      DataTable Get(String sql);
      /// <summary>
      /// 查询数据
      /// </summary>
      /// <param name="sql"></param>
      /// <param name="parameters"></param>
      /// <returns></returns>
      DataTable Get(String sql, List<DbParameter> parameters);
      /// <summary>
      /// 查询数据
      /// </summary>
      /// <param name="sql"></param>
      /// <param name="parameter"></param>
      /// <returns></returns>
      DataTable Get(String sql, DbParameter parameter);
      /// <summary>
      /// 执行Sql
      /// </summary>
      /// <param name="sql"></param>
      /// <returns>受影响的行数</returns>
      int ExecuteNonQuery(String sql);
      /// <summary>
      ///  执行Sql
      /// </summary>
      /// <param name="sql"></param>
      /// <param name="parameters">参数</param>
      /// <returns>受影响的行数</returns>
      int ExecuteNonQuery(String sql, List<DbParameter> parameters);
      /// <summary>
      /// 执行Sql
      /// </summary>
      /// <param name="sql"></param>
      /// <returns>返回第一行第一列的值</returns>
      String ExecuteScalar(String sql);
      /// <summary>
      /// 执行Sql
      /// </summary>
      /// <param name="sql"></param>
      /// <param name="parameters"></param>
      /// <returns>返回第一行第一列的值</returns>
      String ExecuteScalar(String sql, List<DbParameter> parameters);

      /// <summary>
      /// 删除表数据
      /// </summary>
      /// <param name="tableName"></param>
      /// <param name="where"></param>
      /// <returns></returns>
      bool Delete(String tableName, String where);
      /// <summary>
      /// 清空整个数据库数据
      /// </summary>
      /// <returns></returns>
      bool ClearDB();
      /// <summary>
      /// 清空整表数据
      /// </summary>
      /// <param name="tableName"></param>
      /// <returns></returns>
      bool ClearTable(String tableName);
      /// <summary>
      /// 批量插入数据
      /// </summary>
      /// <param name="tableName">表名</param>
      /// <param name="fieldNames">需插入数据的字段名</param>
      /// <param name="source">数据表，表中的数据必须和给出的列名的顺序和数量一致</param>
      /// <returns></returns>
      bool BulkInsert(String tableName, List<String> fieldNames, DataTable source);
      bool Insert(String tableName, Dictionary<String, String> data);

      DbParameter GetParameter();
      DbParameter GetParameter(String name, object value);
      DbTransaction BeginTransaction();
    }
}
