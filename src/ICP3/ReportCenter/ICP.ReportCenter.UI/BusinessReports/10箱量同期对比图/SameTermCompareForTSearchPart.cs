using System;
using System.Collections.Generic;
using System.ComponentModel;

using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using System.Xml;
using ICP.Framework.ClientComponents;

namespace ICP.ReportCenter.UI.BusinessReports
{
    [ToolboxItem(false)]
    public partial class SameTermCompareForTSearchPart : ReportBaseSearchPart
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

        public SameTermCompareForTSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.OnSearched = null;
                reportOperationTypePart1.EditValueChanged -= this.OnReportOperationTypeEditValueChanged;
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
            ReportCenterHelper.BulidReportTypeAndGroups(this.reportOperationTypePart1, null, true, true);
            reportOperationTypePart1.EditValueChanged += this.OnReportOperationTypeEditValueChanged;
      

            Utility.BulidComboboxItem<ReportSalesType>(cmbSalesType, 6);

         

        }
   

        private void OnReportOperationTypeEditValueChanged(object sender,EventArgs e)
        {
            List<Guid> checkIds = reportOperationTypePart1.CheckedItems;
            if (checkIds == null || checkIds.Count == 0)
            {
                txtAgent.Enabled = chkcmbShipLine.Enabled = txtCarrier.Enabled = true;
            }
            else
            {
                var tager = ReportCenterHelper.ReportOperationTypes.Find(r => r.HasShipLine == false && checkIds.Contains(r.ID));
                if (tager == null) txtAgent.Enabled = chkcmbShipLine.Enabled = txtCarrier.Enabled = true;
                else txtAgent.Enabled = chkcmbShipLine.Enabled = txtCarrier.Enabled = false;
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

                writer.WriteStartElement("SalesType");
                writer.WriteValue(cmbSalesType.SelectedIndex);
                writer.WriteEndElement();

                writer.WriteStartElement("SalesSet");
                writer.WriteValue(this.txtSales.Tag.TagToSplitString(","));
                writer.WriteEndElement();


                writer.WriteStartElement("CarrierSet");
                writer.WriteValue(this.txtCarrier.Tag.TagToSplitString(","));
                writer.WriteEndElement();

                writer.WriteStartElement("ShippingLineSet");
                writer.WriteValue(this.chkcmbShipLine.EditValue.ToSplitString(","));
                writer.WriteEndElement();

                writer.WriteStartElement("JobType");
                writer.WriteValue(this.reportOperationTypePart1.EditValue);
                writer.WriteEndElement();

                writer.WriteStartElement("AgentSet");
                writer.WriteValue(this.txtAgent.Tag.TagToSplitString(","));
                writer.WriteEndElement();

                writer.WriteStartElement("DateType");
                writer.WriteValue("0");
                writer.WriteEndElement();

                writer.WriteStartElement("SCNO");
                writer.WriteValue(this.txtContractNo.Text.Trim());
                writer.WriteEndElement();

                writer.WriteStartElement("GroupByField");
                writer.WriteValue(cmbGroupBy.SelectedIndex.ToString());
                writer.WriteEndElement();


                writer.WriteStartElement("Month");
                writer.WriteValue(this.operationDatePart1.MonthString);
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

            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("ETD_Beginning_Date", operationDatePart1.FromDate.ToString("yyyy-MM-dd")));
            paramList.Add(new ReportParameter("ETD_Ending_Date", operationDatePart1.ToDate.ToString("yyyy-MM-dd")));
            paramList.Add(new ReportParameter("Months", this.operationDatePart1.MonthString));   
            paramList.Add(new ReportParameter("IsEnglish", LocalData.IsEnglish ? "1" : "0"));
            paramList.Add(new ReportParameter("Condition", this.XMLCondition));

            ReportData rd = new ReportData { Parameters = paramList, ReportName = "RPT_ALLGetSameTermCompareForT" };
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
