using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Common.ServiceInterface;
using System.Data;
using System.Collections;
using ICP.Framework.CommonLibrary.Client;
using System.Reflection;

namespace ICP.Common.UI
{  
    /// <summary>
    /// 客户端基础数据服务实现类
    /// <remarks>相比对应的服务端服务，增加本地缓存</remarks>
    /// </summary>
   public class ClientBaseDataService:IClientBaseDataService
    {  
       private static Hashtable htMemoryCache = new Hashtable(50);
       private object syncObj = new object();
       private IBaseDataService BaseDataService
       {
           get
           {
               return ServiceClient.GetService<IBaseDataService>();
           }
       }
        public DataTable GetBaseData(BaseDataType dataType)
        {  
            
            if (htMemoryCache.ContainsKey(dataType))
            {
                return htMemoryCache[dataType] as DataTable;
            }
            else
            {  
               
                DataTable dt = BaseDataService.GetBaseData(dataType);
                htMemoryCache[dataType] = dt;
                return dt;

            }

        }
             /// <summary>
       /// 更新指定键值的缓存数据
       /// 数据必须至少包含Id属性
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="dataType"></param>
       /// <param name="deleteIds">需删除的基础数据Id列表</param>
       /// <param name="items"></param>
        public void Update(BaseDataType dataType, List<BaseDataInfo> items, List<Guid> deleteIds)
        {   
               DataTable dtLocal = GetLocalCache(dataType);
            //删除
               dtLocal = DeleteRows(dtLocal,deleteIds);
            if (items == null || items.Count() <= 0)
            {
                return;
            }
            DataTable dt = new DataTable();

            //缓存数据还不存在，则完全新增插入
            if (dtLocal == null)
            {
                htMemoryCache[dataType] = Convert(items);
            }
            //缓存数据存在，则除了新增插入，还需处理更新
            else
            {
                DataTable mergeTable = GetMergeTable(dtLocal, items);
                htMemoryCache[dataType] = mergeTable;
            }
            
        }

        private DataTable DeleteRows(DataTable dtLocal,List<Guid> deleteIds)
        {
            if (deleteIds != null && deleteIds.Count > 0)
            {
                if (dtLocal != null)
                {
                    string inClause = GetInClause(deleteIds);
                    List<DataRow> deletingRows = dtLocal.Select(string.Format("ID in ({0})", inClause)).ToList();
                    foreach (DataRow row in deletingRows)
                    {
                        dtLocal.Rows.Remove(row);
                    }
                }
            }
            return dtLocal;
        }

        private string GetInClause(List<Guid> deleteIds)
        {
            if (deleteIds.Count == 1)
            {
                return string.Format("'{0}'", deleteIds[0].ToString());
            }
            else
            {
                StringBuilder builder = new StringBuilder(100);
                foreach (var item in deleteIds)
                {
                    builder.AppendFormat("'{0}',", item.ToString());
                }
                return builder.ToString().Substring(0, builder.Length - 1);
            }
       

        }
        private List<PropertyInfo> GetTargetProperties(List<string> propertyNames)
        {
            List<PropertyInfo> properties = typeof(BaseDataInfo).GetProperties().ToList();
            List<PropertyInfo> targetProperties = (from item in properties where propertyNames.Contains(item.Name) select item).ToList();
            return targetProperties;
        }
        private DataTable GetMergeTable(DataTable dtLocal,List<BaseDataInfo> items)
        {
            List<string> columnNames = GetColumnNames(dtLocal);

            List<PropertyInfo> targetProperties = GetTargetProperties(columnNames);
            PropertyInfo IdProperty = typeof(BaseDataInfo).GetProperty("ID");
            foreach (var data in items)
            {
                DataRow row = null;
                DataRow[] targetRows = dtLocal.Select(string.Format("ID='{0}'", IdProperty.GetValue(data, null).ToString()));
                if (targetRows != null && targetRows.Length > 0)
                {
                    row = targetRows[0];
                }
                else
                {
                    row = dtLocal.NewRow();
                }
                foreach (var propertyInfo in targetProperties)
                {
                    row[propertyInfo.Name] = propertyInfo.GetValue(data, null);
                }
                dtLocal.Rows.Add(row);
            }
            return dtLocal;
        }

        private List<string> GetColumnNames(DataTable dtLocal)
        {
            List<string> columnNames = new List<string>();
            for (int i = 0; i < dtLocal.Columns.Count; i++)
            {
                columnNames.Add(dtLocal.Columns[i].ColumnName);
            }
            return columnNames;
        }
       /// <summary>
       /// 更新实体默认需要缓存的属性名称
       /// </summary>
        private List<string> targetPropertyNames = new List<string> { "ID", "ParentID", "EName", "CName", "Code" };

        private DataTable Convert(List<BaseDataInfo> items)
        {

            List<PropertyInfo> targetProperties = GetTargetProperties(targetPropertyNames);
            DataTable dt = new DataTable();
            AddColumns(dt);
            foreach (var data in items)
            {
                DataRow row = dt.NewRow();
                foreach (var propertyInfo in targetProperties)
                {
                    row[propertyInfo.Name] = propertyInfo.GetValue(data, null);
                }
                dt.Rows.Add(row);
            }
            return dt;
           

        }

        private void AddColumns(DataTable dt)
        {
            foreach (string propertyName in targetPropertyNames)
            {  
                DataColumn column=new DataColumn(propertyName);
                if (propertyName.Contains("ID"))
                {
                    column.DataType = typeof(System.Guid);
                }
                else
                {
                    column.DataType = typeof(string);
                }
                dt.Columns.Add(column);
            }
        }
        
        private DataTable GetLocalCache(BaseDataType dataType)
        {
            if (htMemoryCache.ContainsKey(dataType))
            {
                return htMemoryCache[dataType] as DataTable;
            }
            else
            {
                return null;
            }
        }
         

    
    }
}
