using System;

namespace ICP.WF.ServiceInterface.DataObject
{
    ///// <summary>
    ///// 数据源模板数据

    ///// </summary>
    //[Serializable]
    //public class DataSourceTemplateItem
    //{
    //    /// <summary>
    //    /// ID
    //    /// </summary>
    //    public Guid Id { get; set; }

    //    /// <summary>
    //    /// 名称
    //    /// </summary>
    //    public string Name { get; set; }

    //    /// <summary>
    //    /// XML数据
    //    /// </summary>
    //    public System.Data.DataSet TemplateFileContent { get; set; }

    //    /// <summary>
    //    /// 版本 
    //    /// </summary>
    //    public string Version { get; set; }


    //    public List<DataSourceItem> GetDataSourceItemList()
    //    {
    //        if (TemplateFileContent==null)
    //        {
    //            return new List<DataSourceItem>();
    //        }
    //        else
    //        {
    //            List<DataSourceItem> items = LoadDataFromDataSet(TemplateFileContent);
    //            return items;
    //        }
    //    }


    //    private  List<DataSourceItem> LoadDataFromDataSet(System.Data.DataSet ds)
    //    {
    //        if (ds == null) return new List<DataSourceItem>();

    //        List<DataSourceItem> items = (from col in ds.Tables[0].Columns.OfType<DataColumn>()
    //                                      select new DataSourceItem
    //                                      {
    //                                          ColumnName = col.ColumnName,
    //                                          MaxLength = col.MaxLength,
    //                                          Description = col.Caption,
    //                                          MustInput = !col.AllowDBNull,
    //                                          DataType =col.DataType.ToString().Substring(col.DataType.ToString().IndexOf(".") + 1)
    //                                      }).ToList();
    //        return items;

    //    }


    //}

    /// <summary>
    /// 工作流模板数据
    /// </summary>
    [Serializable]
    public class WorkFlowTemplateItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// XML数据
        /// </summary>
        public string TemplateFile { get; set; }

        /// <summary>
        /// 版本 
        /// </summary>
        public string Version { get; set; }

    }


    /// <summary>
    /// 工作流模板数据
    /// </summary>
    [Serializable]
    public class FormTemplateItem
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// XML数据
        /// </summary>
        public string TemplateFile { get; set; }

    }


    //public static class ArrayListExtend
    //{
    //    public static string ToErrorMessage(this IList arrs)
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        int i = 1;
    //        foreach (object item in arrs)
    //        {

    //            sb.Append("Error");
    //            sb.Append(i.ToString());
    //            sb.Append(":");
    //            sb.Append(item.ToString());
    //            i++;
    //        }

    //        return sb.ToString();
    //    }
    //}
}
