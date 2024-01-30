using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.ReportCenter.UI.Common.Helper;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using System.Xml;
using DevExpress.XtraEditors;
using ICP.Framework.ClientComponents;

namespace ICP.ReportCenter.UI.BusinessReports
{
    [ToolboxItem(false)]
    public partial class ProfitRatiosSummary : ReportBaseSearchPart
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

        public ProfitRatiosSummary()
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
            if (!DesignMode)
            {
                InitControls();
            }
        }

        private void InitControls()
        {
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


                writer.WriteStartElement("Beginning_Date");
                writer.WriteValue( operationDatePart1.FromDate.ToString("yyyy-MM-dd"));
                writer.WriteEndElement();

                writer.WriteStartElement("Ending_Date");
                writer.WriteValue(operationDatePart1.ToDate.ToString("yyyy-MM-dd"));
                writer.WriteEndElement();

                writer.WriteStartElement("Carrier");
                writer.WriteValue("" + txtCarrier.EditValue);
                writer.WriteEndElement();

                writer.WriteStartElement("ContractNo");
                writer.WriteValue("" + txtcontractNo.EditValue);
                writer.WriteEndElement();

                writer.WriteStartElement("RatioBys");
                writer.WriteValue("" + txtRatioBy.Tag.TagToSplitString(","));
                writer.WriteEndElement();

                writer.WriteStartElement("CompanyIDs");
                writer.WriteValue(chkcmbCompany.CompanyIDs.ToArray().Join4Report());
                writer.WriteEndElement();

                writer.WriteStartElement("ShipLineIDs");
                writer.WriteValue(chkcmbShipLine.ItemIDs.ToArray().Join4Report());
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
        #endregion

        #region ISearchPart 成员

        public override object GetData()
        {
            DateTime from=this.operationDatePart1.FromDate;
            DateTime to = this.operationDatePart1.ToDate;

            List<ReportParameter> paramList = new List<ReportParameter>
            {
                new ReportParameter("Beginning_Date", operationDatePart1.FromDate.ToString("yyyy-MM-dd")),
                new ReportParameter("Ending_Date", operationDatePart1.ToDate.ToString("yyyy-MM-dd")),
                new ReportParameter("CompanyName", ReportCenterHelper.DefaultCompany.EShortName),//默认公司的英文名字
                new ReportParameter("CompanyAddress", ReportCenterHelper.DefaultCompany.EAddress),
                new ReportParameter("TelOrFax", ReportCenterHelper.DefaultCompanyTelAndFax),
                new ReportParameter("Condition", this.XMLCondition),
                new ReportParameter("IsEnglish", LocalData.IsEnglish ? "1" : "0"),
            };

            ReportData rd = new ReportData { Parameters = paramList, ServiceReportPath = ReportPathConstants.BusinessReports, ReportName = "RPT_ProfitRatios_Summary" };
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
