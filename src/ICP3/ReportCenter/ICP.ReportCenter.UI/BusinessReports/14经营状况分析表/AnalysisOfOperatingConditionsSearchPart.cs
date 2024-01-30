using System;
using System.Collections.Generic;
using System.ComponentModel;

using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using ICP.Framework.CommonLibrary.Helper;
using System.Xml;
using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.ReportCenter.UI.BusinessReports
{
    [ToolboxItem(false)]
    public partial class AnalysisOfOperatingConditionsSearchPart : ReportBaseSearchPart
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

        public AnalysisOfOperatingConditionsSearchPart()
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
            List<EnumHelper.ListItem<OrderByOPType>> orderByOPType = EnumHelper.GetEnumValues<OrderByOPType>(LocalData.IsEnglish);
            cmbOrderByOP.Properties.BeginUpdate();
            foreach (var item in orderByOPType)
            {
                cmbOrderByOP.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbOrderByOP.Properties.EndUpdate();
            cmbOrderByOP.SelectedIndex = 0;

            List<EnumHelper.ListItem<ConditionsReportType>> types = EnumHelper.GetEnumValues<ConditionsReportType>(LocalData.IsEnglish);
            cmbReportType.Properties.BeginUpdate();
            foreach (var item in types)
            {
                cmbReportType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbReportType.Properties.EndUpdate();
            cmbReportType.SelectedIndex = 0;
            cmbReportType.SelectedIndexChanged += new EventHandler(cmbReportType_SelectedIndexChanged);

            cmbGroupBy.Properties.Items.Clear();
            cmbGroupBy.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "Cost Item" : "业务员"));
            cmbGroupBy.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "Department" : "组织结构"));
            cmbGroupBy.SelectedIndex = 1; 
        }

        void cmbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConditionsReportType reportType = this.ReportType;
            #region 刷新分组方式 单箱自由选择,业务利润只能按组织结构分组,其它的只能按业务员分组
            if (reportType == ConditionsReportType.PorfitForT)
            {
                cmbGroupBy.SelectedIndex = 0;
                cmbGroupBy.Enabled = true;
            }
            else
            {
                cmbGroupBy.Enabled = false;
                if (reportType == ConditionsReportType.BusinessPorfit) cmbGroupBy.SelectedIndex = 1;
                else cmbGroupBy.SelectedIndex = 0;
            }
            #endregion

            #region 刷新排序 选业务利润时隐藏排序控件
            if (reportType == ConditionsReportType.BusinessPorfit)
            {
                cmbOrderByOP.Visible = seOrderByTop.Visible = labOrderBy.Visible = false;
            }
            else
            {
                cmbOrderByOP.Visible = seOrderByTop.Visible = labOrderBy.Visible = true;
                if (reportType == ConditionsReportType.PorfitForT)
                    labOrderBy.Text = LocalData.IsEnglish ? "TEU" : "箱量";
                else if (reportType == ConditionsReportType.BusinessManagementCost)
                    labOrderBy.Text = LocalData.IsEnglish ? "Cost" : "成本";
                else if (reportType == ConditionsReportType.BusinessDeficit)
                    labOrderBy.Text = LocalData.IsEnglish ? "Deficit" : "亏损";
            }

            #endregion
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
                writer.WriteValue(string.Empty);
                writer.WriteEndElement();

                writer.WriteStartElement("LineID");
                writer.WriteValue(this.chkcmbShipLine.EditValue.ToSplitString(","));
                writer.WriteEndElement();

                writer.WriteStartElement("CostItemID");
                writer.WriteValue(string.Empty);
                writer.WriteEndElement();

                writer.WriteStartElement("Number");
                writer.WriteValue(this.seOrderByTop.Value.ToString("F0"));
                writer.WriteEndElement();


                if (this.cmbReportType.SelectedIndex == 3 )
                {
                    writer.WriteStartElement("OrderBy");
                    writer.WriteValue("3");
                    writer.WriteEndElement();
                }
                else
                {
                    writer.WriteStartElement("OrderBy");
                    writer.WriteValue("0");
                    writer.WriteEndElement();
                }

                writer.WriteStartElement("OrderByType");
                writer.WriteValue(this.cmbOrderByOP.SelectedIndex.ToString());
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

        public ConditionsReportType ReportType
        {
            get
            {
                ConditionsReportType conditionsReportType = (ConditionsReportType)Enum.Parse(typeof(ConditionsReportType), cmbReportType.EditValue.ToString());
                return conditionsReportType;
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

            ConditionsReportType conditionsReportType = (ConditionsReportType)Enum.Parse(typeof(ConditionsReportType), cmbReportType.EditValue.ToString());
            string reportName = string.Empty;

            if (conditionsReportType == ConditionsReportType.BusinessPorfit)
                reportName = "RPT_AnalysisOfOperatingConditions";
            else if (conditionsReportType == ConditionsReportType.PorfitForT)
                reportName = "rpt_ProfitPerTForAnalysisOfOperating";
            else
            {
                paramList.Add(new ReportParameter("AnalysisType", ((short)conditionsReportType).ToString()));
                reportName = "rpt_LossAndCommissionForAnalysisOfOperating";
            }

            ReportData rd = new ReportData { Parameters = paramList, ReportName = reportName };
            return rd;
        }
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion

        /// <summary>
        /// 报表类型 0 - BusinessPorfit, 1 -PorfitForT,2 -BusinessManagementCost,3 -BusinessDeficit
        /// </summary>
        public enum ConditionsReportType
        {
            [MemberDescription("业务利润-运营成本分析", "Business Porfit")]
            BusinessPorfit =0,
            [MemberDescription("单箱利润分析", "Porfit For T")]
            PorfitForT =1,
            [MemberDescription("业务管理成本分析", "Business Management Cost")]
            BusinessManagementCost=2,
            [MemberDescription("业务亏损分析", "Business Deficit")]
            BusinessDeficit =3
        }

        /// <summary>
        /// 排序类型
        /// </summary>
        private enum OrderByOPType
        {
            [MemberDescription("最多的", "Max")]
            Max =0,
            [MemberDescription("最少的", "Min")]
            Min =1,
        }
    }
}
