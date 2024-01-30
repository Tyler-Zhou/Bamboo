using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace ICP.DataCache.ServiceInterface
{
    /// <summary>
    /// 数据库操作接口
    /// </summary>
    public interface IDatabaseOperation
    {
        String ConnectionString { get; set; }

        /// <summary>
        /// 数据查询
        /// </summary>
        /// <param name="sql">查询SQL语句</param>
        /// <returns>数据表</returns>
        DataTable GetDataTable(String sql);
        /// <summary>
        /// 获取整表数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        DataTable GetData(String tableName);

        DataTable GetDataTable(String sql, List<DbParameter> parameters);
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql">需执行的SQL语句</param>
        /// <returns>受影响的行数</returns>
        int ExecuteNonQuery(String sql);
        /// <summary>
        /// 执行带参数SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteNonQuery(String sql, List<DbParameter> parameters);
        /// <summary>
        /// 执行SQL语句返回单个值
        /// </summary>
        /// <param name="sql">需执行的SQL语句</param>
        /// <returns>返回值;如果语句执行无返回值，则返回空字符串</returns>
        String ExecuteScalar(String sql);
        String ExecuteScalar(String sql, List<DbParameter> parameters);
        /// <summary>
        /// 更新表数据
        /// </summary>
        /// <param name="tableName">需更新的表名</param>
        /// <param name="data">列名和目标值键值对</param>
        /// <param name="where">查询条件</param>
        /// <returns>成功返回true,失败返回false</returns>
        bool Update(String tableName, Dictionary<String, String> data, String where);
        /// <summary>
        ///  删除表数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="where">过滤行数据SQL</param>
        /// <returns>删除成功返回true,失败返回false</returns>
        bool Delete(String tableName, String where);
        /// <summary>
        /// 指定列名插入表数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="data">列名列值键值对</param>
        /// <returns>成功返回true,失败返回false</returns>
        bool Insert(String tableName, Dictionary<String, String> data);
        bool BulkInsert(String tableName, List<String> fieldNames, DataTable source);
        /// <summary>
        /// 清空当前数据库中的所有数据
        /// </summary>
        /// <returns>成功返回true,失败返回false</returns>
        bool ClearDB();
        /// <summary>
        /// 清空表数据
        /// </summary>
        /// <param name="table">表名</param>
        /// <returns>成功返回true,失败返回false</returns>
        bool ClearTable(String table);
        /// <summary>
        /// 开始一个事务,必须以EndTransaction结束
        /// </summary>
        /// <returns></returns>
        void BeginTransaction();

        /// <summary>
        /// 
        /// </summary>
        void RollBackTransaction();
        /// <summary>
        /// 
        /// </summary>
        void CommitTransaction();
        /// <summary>
        /// 结束一个事务
        /// </summary>
        void EndTransaction();

    }
}
