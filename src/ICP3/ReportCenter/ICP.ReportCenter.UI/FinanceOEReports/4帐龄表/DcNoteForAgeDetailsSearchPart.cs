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
using ICP.FAM.ServiceInterface.DataObjects.Report;

namespace ICP.ReportCenter.UI.FinanceOEReports._4帐龄表
{
    public partial class DcNoteForAgeDetailsSearchPart : ReportBaseSearchPart
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
        public ICP.FAM.ServiceInterface.IFinanceReportService FinanceReportService
        {
            get
            {
                return ServiceClient.GetService<ICP.FAM.ServiceInterface.IFinanceReportService>();
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
        public DcNoteForAgeDetailsSearchPart()
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
            dteTo.DateTime = Utility.GetEndDate(DateTime.Now);
            Utility.BulidComboboxItem<ReportGroupby>(cmbGroupBy, 0);
            Utility.BulidComboboxItem<ReportViewType>(cmbViewType, 2);
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
                else if (this.rdoOperationOrgial.Checked)
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

                writer.WriteStartElement("ViewType");
                writer.WriteValue(this.cmbViewType.SelectedIndex.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("IsEnglish");
                writer.WriteValue(LocalData.IsEnglish ? "1" : "0");
                writer.WriteEndElement();

                writer.WriteStartElement("GroupBy");
                writer.WriteValue(this.cmbGroupBy.Text);
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
                    using (new CursorHelper(Cursors.WaitCursor))
                    {
                        int therd = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm(this.ParentForm,"数据正在查询，请稍候...");
                        OnSearched(this, GetData());
                        ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(therd);
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
        SearchParameter _SearchParameter;
        public override object GetData()
        {
            if (string.IsNullOrEmpty(this.cmbGroupBy.Text))
            {
                MessageBox.Show(LocalData.IsEnglish?"":"","提示", MessageBoxButtons.OK);
                return null;
            }

            //List<ReportParameter> paramList = new List<ReportParameter>();
            //paramList.Add(new ReportParameter("Peried", dteTo.DateTime.ToShortDateString()));
            //paramList.Add(new ReportParameter("Condition", this.XMLCondition));
            //ReportData rd = new ReportData { Parameters = paramList, ReportName = "Rpt_DebitnoteAge_TotalDetial" };
            //return rd;

            _SearchParameter = new SearchParameter
            {
                StructType = (byte)(rdoOperationOrgial.Checked ? 0 : 1),
                StructNodeId = treeBoxSalesDep.EditValue.ToSplitString(","),
                ETD_Ending_Date = dteTo.DateTime,
                ViewType = (byte)this.cmbViewType.SelectedIndex,
                IsEnglish = LocalData.IsEnglish ? "1" : "0",
                GroupBy = this.cmbGroupBy.Text,
            };

            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("StructType", _SearchParameter.StructType.ToString()));
            paramList.Add(new ReportParameter("StructNodeId", _SearchParameter.StructNodeId));
            paramList.Add(new ReportParameter("ETD_Ending_Date", _SearchParameter.ETD_Ending_Date.ToShortDateString()));
            paramList.Add(new ReportParameter("ViewType", _SearchParameter.ViewType.ToString()));
            paramList.Add(new ReportParameter("IsEnglish", _SearchParameter.IsEnglish));
            paramList.Add(new ReportParameter("GroupBy", _SearchParameter.GroupBy));

            try
            {

                List<DebitnoteAgeSumData> lists = FinanceReportService.GetDebitnoteAgeReportData
                      (_SearchParameter.StructType
                      , _SearchParameter.StructNodeId
                      , _SearchParameter.ETD_Ending_Date
                      , _SearchParameter.ViewType
                      , _SearchParameter.IsEnglish
                      , _SearchParameter.GroupBy
                      );

                ReportData rd = new ReportData();
                rd.IsLocalReport = true;
                rd.CustomerID = LocalData.UserInfo.DefaultCompanyID;
                if (cmbGroupBy.Text == "业务发生地")
                {
                    rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.Rpt_DebitnoteAge_TotalDetial.rdlc";
                }
                else
                {
                    rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.Rpt_DebitnoteAge_TotalDetial1.rdlc";
                }
                rd.Parameters = paramList;
                List<ReportDataSource> ds = new List<ReportDataSource>();
                ds.Add(new ReportDataSource("ICP_ReportCenter_ServiceInterface_DataObjects_DebitnoteAgeSumData", lists));
                rd.DataSource = ds;
                return rd;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this, (LocalData.IsEnglish ? "Get Report Data Failed." : "获取报表数据失败.") + ex.ToString());
                return null;
            }

            return null;
         
        }
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion


        class SearchParameter
        {
            public byte StructType { get; set; }
            public string StructNodeId { get; set; }

            public DateTime ETD_Ending_Date { get; set; }
            public byte ViewType { get; set; }
            public string IsEnglish { get; set; }
            public string GroupBy { get; set; }
        }
    }
}
