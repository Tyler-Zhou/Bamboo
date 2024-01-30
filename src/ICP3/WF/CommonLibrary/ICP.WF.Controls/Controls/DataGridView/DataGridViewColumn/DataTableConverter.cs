using System;
using System.Collections;
using System.Data;
using System.Reflection;
using ICP.WF.ServiceInterface;

namespace ICP.WF.Controls
{
    public static class DataTableConverter
    {
        /// <summary>
        /// 将datasource转换成DataTable，datasource的类型必须是DataTable,DataSet,DataView,实现IList接口的数据源中的一种
        /// 如果datasource是DataView将根据DataView中现有的行创建一个新的DataTable
        /// ThrowExceptions:
        /// ArgumentNullException
        /// ApplicationException
        /// </summary>
        /// <param name="datasource"></param>
        /// <returns></returns>
        public static DataTable ConvertToDataTable(object datasource)
        {
            if (datasource == null)
            {
                throw new ArgumentNullException("datasource");
            }
            DataTable dt = null;
            if (datasource is DataTable)
            {
                dt = (DataTable)datasource;
            }
            else if (datasource is DataSet)
            {
                dt = ((DataSet)datasource).Tables[0];
            }
            else if (datasource is DataView)
            {
                dt = ((DataView)datasource).ToTable();
            }
            else if (datasource is IList)
            {
                IList lst = (IList)datasource;
                return lst.ToTable();
            }
            else
            {
                throw new ApplicationException(Utility.GetString("NotSupportedDataSource", "该数据源不能转换成DataTable,只有DataTable,DataSet,DataView才能转换成DataTable"));
            }
            return dt;
        }
    }
}
