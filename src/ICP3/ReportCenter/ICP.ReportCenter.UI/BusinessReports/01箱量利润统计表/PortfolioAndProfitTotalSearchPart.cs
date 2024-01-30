using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using System.Xml;
using DevExpress.XtraEditors;
using ICP.Framework.ClientComponents;

namespace ICP.ReportCenter.UI.BusinessReports
{
    [ToolboxItem(false)]
    public partial class PortfolioAndProfitTotalSearchPart : ReportBaseSearchPart
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

        public PortfolioAndProfitTotalSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
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
            if (!DesignMode)
            {
                InitControls();

            }
        }

        private void InitControls()
        {
            ReportCenterHelper.BulidReportTypeAndGroups(this.reportOperationTypePart1, this.chkcmbGroupBy, false, true);
            chkcmbGroupBy.Items[0].CheckState = CheckState.Checked;
            chkcmbGroupBy.Items[1].CheckState = CheckState.Checked;
            chkcmbGroupBy.RefreshText();
            reportOperationTypePart1.EditValueChanged += delegate
            {
                List<Guid> checkIds = reportOperationTypePart1.CheckedItems;
                if (checkIds == null || checkIds.Count == 0)
                {
                    navOther.Visible = true;
                }
                else
                {
                    var tager = ReportCenterHelper.ReportOperationTypes.Find(r => r.HasShipLine == false && checkIds.Contains(r.ID));
                    if (tager == null) navOther.Visible = true;
                    else navOther.Visible = false;
                }
            };

            checkLCL.CheckedChanged += CheckLCL_CheckedChanged;
            Utility.BulidComboboxItem<ReportDeficitType>(cmbDeficit, 0);
            Utility.BulidComboboxItem<ReportSalesType>(cmbSalesType, 6);
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
                writer.WriteValue(operationDatePart1.FromDate.ToString("yyyy-MM-dd"));
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


                writer.WriteStartElement("ConsignerSet");
                writer.WriteValue(this.txtCustomer.Tag.TagToSplitString(","));
                writer.WriteEndElement();


                writer.WriteStartElement("ShipAgentSet");
                writer.WriteValue(this.txtAgentOfCarrier.Tag.TagToSplitString(","));
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

                writer.WriteStartElement("LoadPortSet");
                writer.WriteValue(this.stxtPOL.Tag.TagToSplitString(","));
                writer.WriteEndElement();

                writer.WriteStartElement("DiscPortSet");
                writer.WriteValue(this.stxtPOD.Tag.TagToSplitString(","));
                writer.WriteEndElement();

                writer.WriteStartElement("DestPortSet");
                writer.WriteValue(this.stxtPlaceOfDelivery.Tag.TagToSplitString(","));
                writer.WriteEndElement();

                writer.WriteStartElement("ProfitType");
                writer.WriteValue(this.cmbDeficit.SelectedIndex);
                writer.WriteEndElement();


                writer.WriteStartElement("GroupString");
                writer.WriteValue(this.chkcmbGroupBy.EditText);
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

                writer.WriteStartElement("TermType");
                writer.WriteValue(rdoGroupTerm.SelectedIndex + 1);
                writer.WriteEndElement();

                writer.WriteStartElement("BookingParty");
                writer.WriteValue(this.txtBookingParty.Tag.TagToSplitString(","));
                writer.WriteEndElement();
                //是否只显示自拼电商业务
                writer.WriteStartElement("IsOnlyShowLCL");
                writer.WriteValue(checkLCL.Checked ? "1" : "0");
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
            if (OnSearched != null)
            {
                using (new CursorHelper())
                {
                    OnSearched(this, GetData());
                }
            }
        }
        private void CheckLCL_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckEdit checkControl = sender as CheckEdit;
                reportOperationTypePart1.Enabled = !checkControl.Checked;

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }
        #endregion

        #region ISearchPart 成员

        public override object GetData()
        {
            DateTime from = DateTime.Parse(this.operationDatePart1.FromDate.ToShortDateString());
            DateTime to = DateTime.Parse(this.operationDatePart1.ToDate.ToShortDateString());
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

            if (chkcmbGroupBy.CheckCount > 3)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "最多可以选择三种分组方式." : "最多可以选择三种分组方式.");
                return null;
            }

            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("ETD_Beginning_Date", operationDatePart1.FromDate.ToString("yyyy-MM-dd")));
            paramList.Add(new ReportParameter("ETD_Ending_Date", operationDatePart1.ToDate.ToString("yyyy-MM-dd")));
            paramList.Add(new ReportParameter("DateType", "0"));//只有一种时间类型 ,0业务时间

            paramList.Add(new ReportParameter("GroupString", chkcmbGroupBy.EditText));
            paramList.Add(new ReportParameter("GroupCount", chkcmbGroupBy.CheckCount.ToString()));
            paramList.Add(new ReportParameter("Group3", string.Empty));
            paramList.Add(new ReportParameter("JobPlace", this.ucBusinessOrganizationSelect.JobPlace));
            paramList.Add(new ReportParameter("CompanyName", ReportCenterHelper.DefaultCompany.EShortName));//默认公司的英文名字
            paramList.Add(new ReportParameter("CompanyAddress", ReportCenterHelper.DefaultCompany.EAddress));
            paramList.Add(new ReportParameter("TelOrFax", ReportCenterHelper.DefaultCompanyTelAndFax));
            paramList.Add(new ReportParameter("ConditionRemark", (LocalData.IsEnglish ? "部门" : "Department") + "  : " + this.ucBusinessOrganizationSelect.EditText));
            paramList.Add(new ReportParameter("IsEnglish", LocalData.IsEnglish ? "1" : "0"));
            paramList.Add(new ReportParameter("Condition", this.XMLCondition));
            paramList.Add(new ReportParameter("JobTypeBit", ReportCenterHelper.GetJobTypeBitByOperationTypeControl(reportOperationTypePart1)));

            ReportData rd = new ReportData { Parameters = paramList, ServiceReportPath = ReportPathConstants.BusinessReports, ReportName = "RPT_ALLPortfolioAndProfitForGeneral_Total" };
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
