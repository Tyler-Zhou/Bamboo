using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using ICP.Framework.ClientComponents.Controls;
using System.Xml;

namespace ICP.ReportCenter.UI.FinanceOEReports
{
    [ToolboxItem(false)]
    public partial class DcNoteForAgeSearchPart : ReportBaseSearchPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        public ReportCenterHelper ReportCenterHelper
        {
            get
            {
                return ClientHelper.Get<ReportCenterHelper, ReportCenterHelper>();
            }
        }

        #endregion

        #region  init

        public DcNoteForAgeSearchPart()
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
            ReportCenterHelper.BulidReportTypeAndGroups(this.reportOperationTypePart1,null,false,false);

            chkcmbGroupBy.AddItem(Guid.NewGuid(), LocalData.IsEnglish ? "Customer" : "客户");
            chkcmbGroupBy.AddItem(Guid.NewGuid(), LocalData.IsEnglish ? "Sales" : "业务员");
            chkcmbGroupBy.AddItem(Guid.NewGuid(), LocalData.IsEnglish ? "Company" : "业务发生地");
            chkcmbGroupBy.AddItem(Guid.NewGuid(), LocalData.IsEnglish ? "Sales Type" : "揽货方式");
            chkcmbGroupBy.Items[0].CheckState = CheckState.Checked;
            chkcmbGroupBy.Items[1].CheckState = CheckState.Checked;
            chkcmbGroupBy.RefreshText();

            dteTo.DateTime = Utility.GetEndDate(DateTime.Now);

            Utility.BulidComboboxItem<ReportViewType>(cmbViewType, 2);
            Utility.BulidComboboxItem<ReportSalesType>(cmbSalesType , 6);

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

        /// <summary>
        /// 描述信息
        /// </summary>
        public string JobPlace
        {
            get
            {
                StringBuilder strBuilder = new StringBuilder();
                if (this.rdoOperationDepartment.Checked)
                {
                    strBuilder.Append(rdoOperationDepartment.Text + "  : "); 
                }
                else if (this.rdoOperationOrgial .Checked)
                {
                    strBuilder.Append(rdoOperationOrgial.Text + "  : ");
                }
                strBuilder.Append(treeBoxSalesDep.EditText);
                return strBuilder.ToString();
            }
        }

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
                writer.WriteValue(rdoOperationOrgial.Checked ? "0" : "1");
                writer.WriteEndElement();

                writer.WriteStartElement("StructNodeId");
                writer.WriteValue(treeBoxSalesDep.EditValue.ToSplitString(","));
                writer.WriteEndElement();

                writer.WriteStartElement("ETD_Ending_Date");
                writer.WriteValue(dteTo.DateTime.ToString("yyyy-MM-dd"));
                writer.WriteEndElement();

                writer.WriteStartElement("SalesType");
                writer.WriteValue(cmbSalesType.SelectedIndex);
                writer.WriteEndElement();

                writer.WriteStartElement("SalesSet");
                writer.WriteValue(string.Empty);
                writer.WriteEndElement();

                writer.WriteStartElement("ShipperSet");
                writer.WriteValue(this.txtBillTo.Tag.TagToSplitString(","));
                writer.WriteEndElement();

                writer.WriteStartElement("ViewType");
                writer.WriteValue(this.cmbViewType.SelectedIndex.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("JobType");
                writer.WriteValue(this.reportOperationTypePart1.EditValue);
                writer.WriteEndElement();

                writer.WriteStartElement("IsBalance");
                writer.WriteValue(chkIsBalance.Checked ? "1":"0");
                writer.WriteEndElement();

                writer.WriteStartElement("Age");
                writer.WriteValue(9);
                writer.WriteEndElement();

                writer.WriteStartElement("GroupBy");
                writer.WriteValue(this.chkcmbGroupBy.EditText);
                writer.WriteEndElement();

                writer.WriteStartElement("GroupByID");
                writer.WriteValue(string.Empty);
                writer.WriteEndElement();

                writer.WriteStartElement("IsEnglish");
                writer.WriteValue(LocalData.IsEnglish ? "1" : "0");
                writer.WriteEndElement();


                writer.WriteStartElement("C1");
                writer.WriteValue(string.Empty);
                writer.WriteEndElement();


                writer.WriteStartElement("C2");
                writer.WriteValue(string.Empty);
                writer.WriteEndElement();

                writer.WriteStartElement("C3");
                writer.WriteValue(string.Empty);
                writer.WriteEndElement();


                writer.WriteStartElement("N1");
                writer.WriteValue(string.Empty);
                writer.WriteEndElement();

                writer.WriteStartElement("N2");
                writer.WriteValue(string.Empty);
                writer.WriteEndElement();

                writer.WriteStartElement("N3");
                writer.WriteValue(string.Empty);
                writer.WriteEndElement();

                writer.WriteStartElement("ConditionName");
                writer.WriteValue(string.Empty);
                writer.WriteEndElement();

                writer.WriteStartElement("Days");
                writer.WriteValue(txtDays.Text);
                writer.WriteEndElement();

                writer.WriteStartElement("Amount");
                writer.WriteValue(txtAmount.Text);
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
            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("Peried", dteTo.DateTime.ToShortDateString()));
            paramList.Add(new ReportParameter("Condition", this.XMLCondition));
            ReportData rd = new ReportData { Parameters = paramList, ReportName = "RPT_DebitnoteAge_Total1" };
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
