using System;
using System.Collections.Generic;
using System.ComponentModel;

using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using System.Xml;

namespace ICP.ReportCenter.UI.BusinessReports
{
    [ToolboxItem(false)]
    public partial class CostFor_TotalSearchPart : ReportBaseSearchPart
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

        public CostFor_TotalSearchPart()
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
            cmbGroupBy.Properties.Items.Clear();
            cmbGroupBy.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "Cost Item" : "费用项目"));
            cmbGroupBy.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "Department" : "组织结构"));
            cmbGroupBy.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "Fee Date" : "费用期间"));
            cmbGroupBy.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "Employee" : "员工"));
            cmbGroupBy.SelectedIndex = 1; 
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

                writer.WriteStartElement("StructNodeId");
                writer.WriteValue(treeDepartment.EditValue.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("Beginning_Date");
                writer.WriteValue(operationDatePart1.FromDate.ToString("yyyy-MM-dd"));
                writer.WriteEndElement();

                writer.WriteStartElement("Ending_Date");
                writer.WriteValue(operationDatePart1.ToDate.ToString("yyyy-MM-dd"));
                writer.WriteEndElement();

                writer.WriteStartElement("UserID");
                writer.WriteValue(this.txtSales.Tag.TagToSplitString(","));
                writer.WriteEndElement();


                writer.WriteStartElement("GroupByTotalType");
                writer.WriteValue(cmbGroupBy.SelectedIndex.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("CostItemID");
                writer.WriteValue((treeCostItem.EditValue == ReportCenterHelper.TopCostItem.ID ? Guid.Empty : treeCostItem.EditValue).ToString());
                writer.WriteEndElement();


                writer.WriteStartElement("IsTopLevel");
                writer.WriteValue(1);
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

            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("Beginning_Date", operationDatePart1.FromDate.ToString("yyyy-MM-dd")));
            paramList.Add(new ReportParameter("Ending_Date", operationDatePart1.ToDate.ToString("yyyy-MM-dd")));
            paramList.Add(new ReportParameter("StructureNodeName", treeDepartment.Text));
            paramList.Add(new ReportParameter("IsEnglish", LocalData.IsEnglish ? "1" : "0"));
            paramList.Add(new ReportParameter("Condition", this.XMLCondition));

            //this._paramList.Add(new ReportParameter("StructureNodeName", this._SearchPart.StructureNodeName));


            ReportData rd = new ReportData { Parameters = paramList, ReportName = "RPT_ALLCostFor_Total" };
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
