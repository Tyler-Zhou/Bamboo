using System;
using System.Collections.Generic;
using System.ComponentModel;

using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using System.Xml;

namespace ICP.ReportCenter.UI.CRMReports
{
    [ToolboxItem(false)]
    public partial class CustomerStateSearchPart : ReportBaseSearchPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        #endregion

        #region  init

        public CustomerStateSearchPart()
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
                writer.WriteValue("1900-01-01");
                writer.WriteEndElement();

                writer.WriteStartElement("Ending_Date");
                writer.WriteValue(DateTime.Now.ToString("yyyy-MM-dd"));
                writer.WriteEndElement();

                writer.WriteStartElement("SalesSet");
                writer.WriteValue(this.txtSales.Tag.TagToSplitString(","));
                writer.WriteEndElement();

                writer.WriteStartElement("CustomerSet");
                writer.WriteValue(this.txtCustomer.Tag.TagToSplitString(","));
                writer.WriteEndElement();


                writer.WriteStartElement("IsEnglish");
                writer.WriteValue(LocalData.IsEnglish ? "1" : "0");
                writer.WriteEndElement();


                writer.WriteStartElement("IsDirectClient");
                writer.WriteValue(this.cmbCustomerType.SelectedIndex.ToString());
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
            paramList.Add(new ReportParameter("CompanyName", treeDepartment.Text));
            paramList.Add(new ReportParameter("Condition", this.XMLCondition));

            ReportData rd = new ReportData { 
                Parameters = paramList
                ,ServiceReportPath = ReportPathConstants.ReportCenter
                ,
                ReportName = "Report_CRM_GetCustomerStateTotal" 
            };
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
