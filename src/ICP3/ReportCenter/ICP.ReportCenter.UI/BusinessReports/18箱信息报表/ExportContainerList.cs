using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.ReportCenter.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Xml;

namespace ICP.ReportCenter.UI.BusinessReports
{
    /// <summary>
    /// 出口箱信息查询面板
    /// </summary>
    public partial class ExportContainerList : ReportBaseSearchPart
    {
        #region Service
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 基础数据查询服务
        /// </summary>
        ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }
        /// <summary>
        /// 报表中心帮助类
        /// </summary>
        ReportCenterHelper ReportCenterHelper
        {
            get
            {
                return ClientHelper.Get<ReportCenterHelper, ReportCenterHelper>();
            }
        }
        /// <summary>
        /// 查询条件XML格式化
        /// </summary>
        string XMLCondition
        {
            get
            {
                System.IO.StringWriter str = new System.IO.StringWriter();
                XmlTextWriter writer = new XmlTextWriter(str);
                writer.Formatting = Formatting.Indented;

                writer.WriteStartDocument();
                writer.WriteComment(" 查询条件XML");

                writer.WriteStartElement("root");

                writer.WriteStartElement("DateSearchType");
                writer.WriteValue(cmbDateType.SelectedIndex+1);
                writer.WriteEndElement();

                writer.WriteStartElement("ETD_Beginning_Date");
                writer.WriteValue(DatePart.FromDate.ToString("yyyy-MM-dd"));
                writer.WriteEndElement();

                writer.WriteStartElement("ETD_Ending_Date");
                writer.WriteValue(DatePart.ToDate.ToString("yyyy-MM-dd"));
                writer.WriteEndElement();

                writer.WriteStartElement("CompanyIDs");
                writer.WriteValue(treeBoxSalesDep.GetAllEditValue.ToSplitString(","));
                writer.WriteEndElement();

                writer.WriteStartElement("ShippingLineIDs");
                writer.WriteValue(chkcmbShipLine.EditValue.ToSplitString(","));
                writer.WriteEndElement();

                string deliveryTypeName = cmbDeliveryTypeName.Text;
                writer.WriteStartElement("DeliveryTypeName");
                writer.WriteValue(("全部".Equals(deliveryTypeName) || "All".Equals(deliveryTypeName))?"": deliveryTypeName);
                writer.WriteEndElement();

                writer.WriteStartElement("GroupString");
                writer.WriteValue(chkcmbGroupBy.EditText);
                writer.WriteEndElement();

                writer.WriteStartElement("IsEnglish");
                writer.WriteValue(LocalData.IsEnglish ? "1" : "0");
                writer.WriteEndElement();

                writer.WriteEndElement();

                writer.WriteEndDocument();

                writer.Close();

                return str.ToString();
            }
        }

        #endregion
        /// <summary>
        /// 重写查询事件变量
        /// </summary>
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;
        /// <summary>
        /// 出口箱信息查询面板
        /// </summary>
        public ExportContainerList()
        {
            InitializeComponent();
            InitControls();
            Disposed += delegate
            {
                OnSearched = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }
        /// <summary>
        /// 初始化控件值
        /// </summary>
        private void InitControls()
        {
            List<DataDictionaryList> transportClauseList =  TransportFoundationService.GetDataDictionaryList(string.Empty, string.Empty, DataDictionaryType.TransportClause, true, 0);
            cmbDeliveryTypeName.Properties.BeginUpdate();
            cmbDeliveryTypeName.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "All" : "全部", ""));
            foreach (DataDictionaryList item in transportClauseList)
            {
                cmbDeliveryTypeName.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Code, item.ID));
            }
            cmbDeliveryTypeName.Properties.EndUpdate();
            cmbDeliveryTypeName.SelectedIndex = 0;
            Utility.BulidComboboxItem<ECLDateSearchType>(cmbDateType, 0);
            Utility.BuildGroupBy_ExportContainerList(chkcmbGroupBy);
        }
        /// <summary>
        /// 查询点击事件
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
            try
            {
                if (OnSearched != null)
                {
                    using (new CursorHelper())
                    {
                        OnSearched(this, GetData());
                    }
                }

            }
            finally
            {
                btnSearch.Enabled = true;
            }
        }

        #region ISearchPart 成员
        /// <summary>
        /// 重写查询面板获取数据弗雷方法
        /// </summary>
        /// <returns>本地或远程报表数据对象</returns>
        public override object GetData()
        {
            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("DateSearchType", "" + (cmbDateType.SelectedIndex+1)));
            paramList.Add(new ReportParameter("ETD_Beginning_Date", DatePart.FromDate.ToString("yyyy-MM-dd")));
            paramList.Add(new ReportParameter("ETD_Ending_Date", DatePart.ToDate.ToString("yyyy-MM-dd")));
            paramList.Add(new ReportParameter("CompanyName", ReportCenterHelper.DefaultCompany.EShortName));//默认公司的英文名字
            paramList.Add(new ReportParameter("CompanyAddress", ReportCenterHelper.DefaultCompany.EAddress));
            paramList.Add(new ReportParameter("TelOrFax", ReportCenterHelper.DefaultCompanyTelAndFax));
            paramList.Add(new ReportParameter("GroupString", chkcmbGroupBy.EditText));
            paramList.Add(new ReportParameter("JobPlace", LocalData.IsEnglish ? "Job Place:" : "业务发生地:" + treeBoxSalesDep.EditText));
            paramList.Add(new ReportParameter("Condition", this.XMLCondition));
            paramList.Add(new ReportParameter("IsEnglish", LocalData.IsEnglish ? "1" : "0"));

            ReportData rd = new ReportData { Parameters = paramList, ServiceReportPath = ReportPathConstants.BusinessReports, ReportName = "RPT_ExportContainerInfo" };
            return rd;
        }
        /// <summary>
        /// 查询
        /// </summary>
        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion

    }
}
