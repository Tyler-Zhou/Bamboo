#region Comment

/*
 * 
 * FileName:    SqlCEHelper.cs
 * CreatedOn:   2014/5/15 星期四 10:00:05
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->SqlCe数据库帮助类
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;

namespace ICP.Document
{
    /// <summary>
    /// SqlCe数据库帮助类
    /// </summary>
    public class SqlCeHelper
    {
        #region[字段]
        private string connectstring = @"Data Source=|DataDirectory|\Database1.sdf";
        SqlCeConnection connect = null;
        SqlCeCommand command = null;
        private Dictionary<string, string> whereFields;
        #endregion

        #region[属性]
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectString
        {
            get { return connectstring; }
            set { connectstring = value; }
        }
        #endregion

        #region[构造函数]
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DBPath">数据库路径</param>
        public SqlCeHelper(string DBPath)
        {
            this.ConnectString = "Data Source=" + DBPath;
            connect = new SqlCeConnection(ConnectString);
            whereFields = new Dictionary<string, string>();
        }       
        #endregion

        #region[私有函数]
        private void Open()
        {
            try
            {
                if (connect.State != System.Data.ConnectionState.Open)
                {
                    connect.Open();
                }

            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
        }

        private void Close()
        {
            try
            {
                if (connect.State != System.Data.ConnectionState.Closed)
                {
                    connect.Close();
                }

            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
        }
        #endregion

        #region [公用函数]
        /// <summary>
        /// 添加Where查询字段
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="fieldValue">字段值</param>
        public void AddWhereFields(string fieldName, string fieldValue)
        {
            whereFields.Add(fieldName, fieldValue);
        }

        /// <summary>
        /// 添加Where查询字段
        /// </summary>
        /// <param name="paramWhereField">Where 字段</param>
        public void SetWhereFields(Dictionary<string, string> paramWhereField)
        {
            whereFields = paramWhereField;
        }

        /// <summary>
        /// 测试连通性
        /// </summary>
        /// <returns></returns>
        public bool ConnectTest()
        {
            try
            {
                connect.Open();
            }
            catch
            {
                connect.Close();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 执行无返回的Sql语句，如插入，删除，更新,注意异常的抛出
        /// </summary>
        /// <param name="sqlstr">SQL语句</param>
        /// <returns>受影响的条数</returns>
        public int ExecuteNonQuery(string sqlstr)
        {
            try
            {
                Open();
                if (whereFields.Count > 0)
                    sqlstr += " WHERE 1=1 ";
                foreach (KeyValuePair<string, string> kvp in this.whereFields)
                {
                    sqlstr += " AND " + kvp.Key + "='" + kvp.Value + "'";
                }
                command = new SqlCeCommand(sqlstr, connect);

                int num = command.ExecuteNonQuery();
                command.Parameters.Clear();
                Close();
                whereFields.Clear();
                return num;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sqlstr">Sql</param>
        /// <returns>DataSet数据集</returns>
        public DataSet ReturnDataSetPagination(string pTableName, int pRowCount, int pPage)
        {
            DataSet ds = new DataSet();
            try
            {
                Open();
                string sqlstr = "SELECT TOP ("
                    + pRowCount + ") * FROM ["
                    + pTableName + "] WHERE [ID] NOT IN ( SELECT TOP ("
                    + pRowCount + ") [ID] FROM  ["
                    + pTableName + "] ORDER BY [ID]) ORDER BY [ID] ";
                SqlCeDataAdapter adapter = new SqlCeDataAdapter(sqlstr, connect);
                adapter.Fill(ds, "Obj");
                Close();
                whereFields.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sqlstr">Sql</param>
        /// <returns>DataSet数据集</returns>
        public DataSet ReturnDataSet(string sqlstr)
        {
            DataSet ds = new DataSet();
            try
            {
                Open();
                if (whereFields.Count > 0)
                    sqlstr += " WHERE 1=1 ";
                foreach (KeyValuePair<string, string> kvp in this.whereFields)
                {
                    sqlstr += " AND " + kvp.Key + "='" + kvp.Value + "'";
                }
                SqlCeDataAdapter adapter = new SqlCeDataAdapter(sqlstr, connect);
                adapter.Fill(ds, "Obj");
                Close();
                whereFields.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// 执行查询语句，返回DataTable。注意异常的抛出
        /// </summary>
        /// <param name="sqlstr">Sqk</param>
        /// <returns>DataTable数据表</returns>
        public DataTable ReturnDataTable(string sqlstr)
        {
            try
            {
                return ReturnDataSet(sqlstr).Tables[0];
            }
            catch (Exception ex)
            { throw ex; }
        }

        /// <summary>
        /// 执行查询语句，返回DataReader
        /// </summary>
        /// <param name="sqlstr">Sql</param>
        /// <returns>DataReader</returns>
        public SqlCeDataReader ReturnDataReader(string sqlstr)
        {

            try
            {
                Open();
                if (whereFields.Count > 0)
                    sqlstr += " WHERE 1=1 ";
                foreach (KeyValuePair<string, string> kvp in this.whereFields)
                {
                    sqlstr += " AND " + kvp.Key + "='" + kvp.Value + "'";
                }
                command = new SqlCeCommand(sqlstr, connect);
                SqlCeDataReader myReader = command.ExecuteReader();
                command.Parameters.Clear();
                Close();
                whereFields.Clear();
                return myReader;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="SQLStringList"></param>
        public void ExecuteSqlTran(ArrayList SQLStringList)
        {

            Open();
            command = new SqlCeCommand();
            command.Connection = connect;
            SqlCeTransaction tx = connect.BeginTransaction();
            command.Transaction = tx;
            try
            {
                for (int n = 0; n < SQLStringList.Count; n++)
                {
                    string strsql = SQLStringList[n].ToString();
                    if (strsql.Trim().Length > 1)
                    {
                        command.CommandText = strsql;
                        command.ExecuteNonQuery();
                    }
                }
                tx.Commit();
            }
            catch (Exception ex)
            {
                tx.Rollback();
                throw ex;
            }
        } 
        #endregion
    }
}
