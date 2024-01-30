using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using System.Xml;

namespace ICP.ReportCenter.UI.FCMReports
{
    [ToolboxItem(false)]
    public partial class JobInfoForCargoTrackingSearchPart : ReportBaseSearchPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region  init

        public JobInfoForCargoTrackingSearchPart()
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
            rdoOperationOrgial.CheckedChanged += new EventHandler(rdoDepartment_CheckedChanged);
            rdoOperationDepartment.CheckedChanged += new EventHandler(rdoDepartment_CheckedChanged);
          
        }

        void rdoDepartment_CheckedChanged(object sender, EventArgs e)
        {
            bool showDepartment = true;
            if (rdoOperationOrgial.Checked)
            {
                showDepartment = false;
            }
            this.treeBoxSalesDep.ShowDepartment = showDepartment;
            this.treeBoxSalesDep.AddCompanyItems();
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
                writer.WriteValue(rdoOperationOrgial.Checked ?"0":"1");
                writer.WriteEndElement();

                writer.WriteStartElement("StructNodeId");
                writer.WriteValue(treeBoxSalesDep.GetAllEditValue.ToSplitString(","));
                writer.WriteEndElement();

                writer.WriteStartElement("ETD_Beginning_Date");
                writer.WriteValue( operationDatePart1.FromDate.ToString("yyyy-MM-dd"));
                writer.WriteEndElement();

                writer.WriteStartElement("ETD_Ending_Date");
                writer.WriteValue(operationDatePart1.ToDate.ToString("yyyy-MM-dd"));
                writer.WriteEndElement();

                writer.WriteStartElement("SalesSet");
                writer.WriteValue(this.txtSales.Tag.TagToSplitString(","));
                writer.WriteEndElement();

                writer.WriteStartElement("CarrierSet");
                writer.WriteValue(this.txtCarrier.Tag.TagToSplitString(","));
                writer.WriteEndElement();

                writer.WriteStartElement("ShippingLineSet");
                writer.WriteValue(this.chkcmbShipLine.EditValue.ObjectsToSplitString(","));
                writer.WriteEndElement();

                writer.WriteStartElement("LoadPortSet");
                writer.WriteValue(this.stxtPOL.Tag.TagToSplitString(","));
                writer.WriteEndElement();

                writer.WriteStartElement("DiscPortSet");
                writer.WriteValue(this.stxtPOD.Tag.TagToSplitString(","));
                writer.WriteEndElement();

                //只有一种时间类型 ,0业务时间
                writer.WriteStartElement("DateType");
                writer.WriteValue("0");
                writer.WriteEndElement();


                writer.WriteStartElement("SoDateToETD");
                writer.WriteValue(this.seSODateToETD.Value.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("ETDToETA");
                writer.WriteValue(this.seETDToETA .Value.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("LoadingToETD");
                writer.WriteValue(this.seETDToLoadShip.Value.ToString());
                writer.WriteEndElement();


                writer.WriteStartElement("SCNO");
                writer.WriteValue(this.txtContractNo.Text.Trim());
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
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (OnSearched != null)
                    OnSearched(this, GetData());

            }
            finally
            {
                this.btnSearch.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region ISearchPart 成员

        public override object GetData()
        {
            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("Condition", XMLCondition));

            ReportData rd = new ReportData { Parameters = paramList, ReportName = "RPT_GetJobInfoForCargoTracking" };
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
