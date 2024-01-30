using System;
using System.Collections.Generic;
using System.ComponentModel;

using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using System.Xml.Linq;

namespace ICP.ReportCenter.UI.FCMReports
{
    [ToolboxItem(false)]
    public partial class AgentForOperatorSearchPart : ReportBaseSearchPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region  init

        public AgentForOperatorSearchPart()
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
            Utility.BulidComboboxItem<ReportDateType>(cmbDateType, 0);
            Utility.BulidComboboxItem<ReportSalesType>(cmbSalesType, 6);
        }


        #endregion

        #region 属性

        public string XMLCondition
        {
            get
            {

                XElement xeRoot = new XElement("Root");
                xeRoot.Add(new XElement("CustomerIDs", this.txtCustomer.Tag.TagToSplitString(",")));
                xeRoot.Add(new XElement("GoodsType", cmbSalesType.SelectedIndex.ToString()));
                xeRoot.Add(new XElement("SalesIDs", this.txtSales.Tag.TagToSplitString(",")));
                xeRoot.Add(new XElement("DateFrom", operationDatePart1.FromDate.ToShortDateString()));
                xeRoot.Add(new XElement("DateTo", operationDatePart1.ToDate.ToShortDateString()));
                xeRoot.Add(new XElement("IsETD", cmbDateType.SelectedIndex == 0 ? "false" : "true"));
                xeRoot.Add(new XElement("UserId", LocalData.UserInfo.LoginID));
                return xeRoot.ToString();
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
            paramList.Add(new ReportParameter("Condition", XMLCondition));

            ReportData rd = new ReportData { Parameters = paramList, ReportName = "Rpt_AgentForOperator" };
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
