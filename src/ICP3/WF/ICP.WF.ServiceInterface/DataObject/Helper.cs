using System;
using System.Data;
using System.Reflection;
using System.Linq;
using System.Text.RegularExpressions;

namespace ICP.WF.ServiceInterface
{
    public static class ExtendList
    {
        /// <summary>
        /// 把指定的列表类型对象,转换为DataTable
        /// </summary>
        /// <param name="list"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DataTable ToTable(this System.Collections.IEnumerable list, Type type)
        {
            System.Reflection.PropertyInfo[] ps = type.GetProperties();
            DataTable dt = new DataTable();
            foreach (System.Reflection.PropertyInfo p in ps)
            {
                if (p.PropertyType.IsArray) continue;
                if (p.PropertyType.Name == "String" || p.PropertyType.Name == "Guid" || p.PropertyType.Name.Contains("Int"))
                {
                    System.Data.DataColumn dc = new DataColumn();
                    dc.DataType = p.PropertyType;
                    dc.ColumnName = p.Name;
                    dt.Columns.Add(dc);
                }
            }

            foreach (object v in list)
            {
                DataRow dr = dt.NewRow();
                foreach (DataColumn c in dt.Columns)
                {
                    dr[c.ColumnName] = ps.FirstOrDefault(delegate(System.Reflection.PropertyInfo p) { return p.Name == c.ColumnName; }).GetValue(v, null);
                }

                dt.Rows.Add(dr);
            }

            dt.AcceptChanges();

            return dt;
        }


        /// <summary>
        /// 把一个列表转换为DataTable
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public static DataTable ToTable(this System.Collections.IList lst)
        {
            if (lst == null || lst.Count == 0) return null;

            DataTable table = new DataTable();
            PropertyInfo[] props = lst[0].GetType().GetProperties();
            foreach (PropertyInfo p in props)
            {
                if (p.PropertyType.Name == "String" || p.PropertyType.Name == "Guid" || p.PropertyType.Name.Contains("Int"))
                {
                    DataColumn col = new DataColumn(p.Name.ToUpper(), p.PropertyType);
                    table.Columns.Add(col);
                }
            }

            if (table.Columns.Contains("ID"))
            {
                table.PrimaryKey = new DataColumn[] { table.Columns["ID"] };
            }

            table.BeginLoadData();
            foreach (object item in lst)
            {
                DataRow dr = table.NewRow();
                foreach (PropertyInfo p in props)
                {
                    if (table.Columns.Contains(p.Name))
                    {
                        dr[p.Name] = p.GetValue(item, null);
                    }
                }
                table.Rows.Add(dr);
            }
            table.EndLoadData();
            table.AcceptChanges();
            return table;
        }



        public static string ToCustomString(this System.Collections.IEnumerable list,string title)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (string.IsNullOrEmpty(title) == false)
            {
                sb.Append(title);
                sb.Append(System.Environment.NewLine);
            }
            int i = 0;
            foreach (string s in list)
            {
                i++;
                sb.Append(i.ToString());
                sb.Append(":");
                sb.Append(s);
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

    }


}
