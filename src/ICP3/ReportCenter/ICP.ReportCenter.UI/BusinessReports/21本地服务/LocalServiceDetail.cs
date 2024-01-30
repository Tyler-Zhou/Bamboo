using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using System.Xml;
using ICP.Framework.ClientComponents;

namespace ICP.ReportCenter.UI.BusinessReports
{
    [ToolboxItem(false)]
    public partial class LocalServiceDetail : ReportBaseSearchPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public ReportCenterHelper ReportCenterHelper
        {
            get
            {
                return ClientHelper.Get<ReportCenterHelper, ReportCenterHelper>();
            }
        }

        #endregion

        #region  init

        public LocalServiceDetail()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.OnSearched = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
       
        }

        private void InitControls()
        {
            ReportCenterHelper.BulidReportTypeAndGroups2(this.reportOperationTypePart1, this.chkcmbGroupBy);
            if (chkcmbGroupBy.Items.Count>0)
            {
                chkcmbGroupBy.Items[0].CheckState = CheckState.Checked;
                chkcmbGroupBy.Items[1].CheckState = CheckState.Checked;
                chkcmbGroupBy.RefreshText();
            }
        }

        #endregion

        #region 属性

       

        public string XMLCondition
        {
            get
            {
                System.IO.StringWriter str = new System.IO.StringWriter();
                XmlTextWriter writer = new XmlTextWriter(str);
                writer.Formatting = Formatting.Indented;

                writer.WriteStartDocument();
                writer.WriteComment(" 查询条件XML");

                writer.WriteStartElement("root");

                writer.WriteStartElement("StructType");
                writer.WriteValue(this.ucBusinessOrganizationSelect.OrganizationType.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("StructNodeId");
                writer.WriteValue(this.ucBusinessOrganizationSelect.SelectedOrganizationString);
                writer.WriteEndElement();

                writer.WriteStartElement("ETD_Beginning_Date");
                writer.WriteValue( operationDatePart1.FromDate.ToString("yyyy-MM-dd"));
                writer.WriteEndElement();

                writer.WriteStartElement("ETD_Ending_Date");
                writer.WriteValue(operationDatePart1.ToDate.ToString("yyyy-MM-dd"));
                writer.WriteEndElement();

                writer.WriteStartElement("SupplierSet");
                writer.WriteValue(this.txtSupplier.Tag.TagToSplitString(",") );
                writer.WriteEndElement();

                writer.WriteStartElement("JobType");
                writer.WriteValue(this.reportOperationTypePart1.EditValue);
                writer.WriteEndElement();

                writer.WriteStartElement("GroupString");
                writer.WriteValue(this.chkcmbGroupBy.EditText);
                writer.WriteEndElement();

                writer.WriteStartElement("DateType");
                writer.WriteValue("0");
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

        #region event
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.btnSearch.Enabled = false;
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
                this.btnSearch.Enabled = true;
            }
        }

        #endregion

        #region ISearchPart 成员

        public override object GetData()
        {
            DateTime from=this.operationDatePart1.FromDate;
            DateTime to = this.operationDatePart1.ToDate;
            if (reportOperationTypePart1.CheckedItems == null || reportOperationTypePart1.CheckedItems.Count == 0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Operation type must choose one." : "请至少选择一种业务类型.");
                return null;
            }
            if (chkcmbGroupBy.CheckCount == 0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "GroupBy must choose one." : "请至少选择一种分组方式.");
                return null;
            }

            if (chkcmbGroupBy.CheckCount > 2)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "最多可以选择二种分组方式." : "最多可以选择二种分组方式.");
                return null;
            }
            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("ETD_Beginning_Date", operationDatePart1.FromDate.ToString("yyyy-MM-dd")));
            paramList.Add(new ReportParameter("ETD_Ending_Date", operationDatePart1.ToDate.ToString("yyyy-MM-dd")));
            paramList.Add(new ReportParameter("DateType", "0"));//只有一种时间类型 ,0业务时间

            paramList.Add(new ReportParameter("CompanyName", ReportCenterHelper.DefaultCompany.EShortName));//默认公司的英文名字
            paramList.Add(new ReportParameter("CompanyAddress", ReportCenterHelper.DefaultCompany.EAddress));
            paramList.Add(new ReportParameter("TelOrFax", ReportCenterHelper.DefaultCompanyTelAndFax));
            paramList.Add(new ReportParameter("ConditionRemark", (LocalData.IsEnglish ? "部门" : "Department") + "  : " + this.ucBusinessOrganizationSelect.EditText));
            paramList.Add(new ReportParameter("JobPlace", this.ucBusinessOrganizationSelect.JobPlace));
            paramList.Add(new ReportParameter("GroupCount", chkcmbGroupBy.CheckCount.ToString()));
            paramList.Add(new ReportParameter("Condition", this.XMLCondition));
            paramList.Add(new ReportParameter("GroupString", chkcmbGroupBy.EditText));
            paramList.Add(new ReportParameter("IsEnglish", LocalData.IsEnglish ? "1" : "0"));
            paramList.Add(new ReportParameter("JobTypeBit", ReportCenterHelper.GetJobTypeBitByOperationTypeControl(reportOperationTypePart1)));

            ReportData rd = new ReportData { Parameters = paramList, ServiceReportPath = ReportPathConstants.BusinessReports, ReportName = "RPT_LocalService_Detail" };
            return rd;

        }
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion
    }
}
