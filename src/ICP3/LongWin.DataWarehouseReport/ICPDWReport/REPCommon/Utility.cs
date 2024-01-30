using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LongWin.CommonData.ServiceInterface.DataObjects;
using LongWin.CommonData.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using LongWin.DataWarehouseReport.ServiceInterface;

namespace LongWin.DataWarehouseReport.WinUI
{
    public class Utility
    {
        public static void CopyData(object source, object dest)
        {
            System.Reflection.PropertyInfo[] fields = source.GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo property in fields)
            {
                object value = property.GetValue(source, null);

                System.Reflection.PropertyInfo destProperty = dest.GetType().GetProperty(property.Name);
                if (destProperty != null)
                {
                    destProperty.SetValue(dest, value, null);
                }
            }
        }

        public static void SetGoodsSalesTypes(System.Windows.Forms.ComboBox cmbSalesTypes)
        {           
            string[] results;
            if (IsEnglish)
            {
                results = new string[] {    "NVOCC",
                                            "SALES",
                                            "AGENT",
                                            "COMPANY",
                                            "FOREIGN SALES",
                                            "OVERSEA AGENT",
                                            "ALL"};
            }
            else
            {
                results = new string[] { "同行货",
                                        "自揽货",
                                        "指定货",
                                        "公司货",
                                        "外地自揽货",
                                        "海外指定货",
                                        "全部"};
            }

            cmbSalesTypes.Items.Clear();
            cmbSalesTypes.Items.AddRange(results);
            cmbSalesTypes.SelectedIndex = 6;
        }


        /// <summary>
        /// 获取英文名
        /// </summary>
        /// <param name="title"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public  static string GetValueString(string title, string defaultValue)
        {
            try
            {
                if (LongWin.Framework.ClientComponents.ControlBase.LocalCulture.Contains("en") == true)
                {
                    if (LongWin.DataWarehouseReport.WinUI.DWReportResources.ResourceManager.GetString(title) != string.Empty
                        && LongWin.DataWarehouseReport.WinUI.DWReportResources.ResourceManager.GetString(title) != null )
                    {
                        return LongWin.DataWarehouseReport.WinUI.DWReportResources.ResourceManager.GetString(title);
                    }
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

    

        /// <summary>
        /// 获取业务类型的集合

        /// </summary>
        /// <returns></returns>
        public static DataSet GetConveyanceType()
        {
            DataTable dtConveyanceType = null;
            dtConveyanceType = new DataTable();
            dtConveyanceType.Columns.Add("code", typeof(string));
            dtConveyanceType.Columns.Add("name", typeof(string));

            dtConveyanceType.Rows.Add(new object[] { "YWLX01", "整箱出口" });
            dtConveyanceType.Rows.Add(new object[] { "YWLX02", "整箱进口" });
            dtConveyanceType.Rows.Add(new object[] { "YWLX03", "拼箱出口" });
            dtConveyanceType.Rows.Add(new object[] { "YWLX04", "拼箱进口" });
            dtConveyanceType.Rows.Add(new object[] { "YWLX06", "空运出口" });
            dtConveyanceType.Rows.Add(new object[] { "YWLX07", "空运进口" });
            dtConveyanceType.Rows.Add(new object[] { "YWLX05", "散杂货" });
            dtConveyanceType.Rows.Add(new object[] { "YWLX08", "其他业务" });

            DataSet ds = new DataSet();
            ds.Tables.Add(dtConveyanceType);
            return ds;
        }

        /// <summary>
        /// 获取业务类型的集合

        /// </summary>
        /// <returns></returns>
        public static DataSet GetShppingLine(List<ShippingLineListData> list)
        {
            DataTable dtShippingLine = null;

            dtShippingLine = new DataTable();
            dtShippingLine.Columns.Add("Id", typeof(System.Guid));
            dtShippingLine.Columns.Add("Cname", typeof(string));
            dtShippingLine.Columns.Add("Ename", typeof(string));
            dtShippingLine.Columns.Add("Code", typeof(string));

            foreach (ShippingLineListData line in list)
            {
                DataRow row = dtShippingLine.NewRow();
                row["Id"] = line.Id;
                row["Cname"] = line.CName;
                row["Ename"] = line.EName;
                row["Code"] = line.Code;
                dtShippingLine.Rows.Add(row);
            }

            DataSet ds = new DataSet();
            ds.Tables.Add(dtShippingLine);
            return ds;
        }


        /// <summary>
        /// 判断当前环境是否在英文环境下
        /// </summary>
        public static bool IsEnglish
        {
            get
            {
                return LongWin.Framework.ClientComponents.ControlBase.LocalCulture.Contains("en");
            }
        }

        public static ReportServerInfo ReportServerInfos ;

        public static DataSet GetCostItem(List<CostItemData> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(System.Guid));
            dt.Columns.Add("ParentID", typeof(System.Guid));
            dt.Columns.Add("CName", typeof(string));
            dt.Columns.Add("EName", typeof(string));

            foreach (CostItemData costItem in list)
            {
                DataRow row = dt.NewRow();
                row["Id"] = costItem.Id;
                if (costItem.ParentID == null)
                    row["ParentID"] = Guid.Empty;
                else
                    row["ParentID"] = costItem.ParentID;
                row["CName"] = costItem.CName;
                row["EName"] = costItem.EName;
                dt.Rows.Add(row);
            }

            DataRow rowParent = dt.NewRow();
            rowParent["Id"] = Guid.Empty;
            rowParent["ParentID"] = System.DBNull.Value;
            rowParent["CName"] = "全部";
            rowParent["EName"] = "ALL";
            dt.Rows.Add(rowParent);

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            return ds;
        }
    }

}
