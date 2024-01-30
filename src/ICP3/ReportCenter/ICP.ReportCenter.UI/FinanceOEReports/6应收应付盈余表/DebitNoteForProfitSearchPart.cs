using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using System.Xml;

namespace ICP.ReportCenter.UI.FinanceOEReports
{
    [ToolboxItem(false)]
    public partial class DebitNoteForProfitSearchPart : ReportBaseSearchPart
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

        IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }


        #endregion

        #region  init

        public DebitNoteForProfitSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.OnSearched = null;
                if (this.chargingCodeFinder != null)
                {
                    this.chargingCodeFinder.Dispose();
                    this.chargingCodeFinder = null;
                }
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
                SearchRegister();
            }
        }

        private void InitControls()
        {
            ReportCenterHelper.BulidReportTypeAndGroups(this.reportOperationTypePart1,null,false,false);
            Utility.BulidComboboxItem<ReportYesNoType>(cmbIsAttach, 0);

            rdoOperationOrgial.CheckedChanged += new EventHandler(rdoDepartment_CheckedChanged);
            rdoOperationDepartment.CheckedChanged += new EventHandler(rdoDepartment_CheckedChanged);
            rdoDepartment_CheckedChanged(null, null);
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

        void SearchRegister()
        {

            #region ChargeCode
            chargingCodeFinder = SearchBoxAdapter.RegisterChargeCodeMultipleSearchBox(DataFindClientService, txtChargingCode, CommonFinderConstants.ChargingCodeFinder, GetSolutionChargingCodeSearchCondition);
            #endregion
        }
        IDisposable chargingCodeFinder;
        Guid _SolutionID
        {
            get
            {
                Guid id = Guid.Empty;
                ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(Utility.UserDefaultCompanyID);
                if (configureInfo != null)
                {
                    id = configureInfo.SolutionID;
                }
                return id;
            }
        }
        SearchConditionCollection GetSolutionChargingCodeSearchCondition()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("SolutionID", _SolutionID, false);
            return conditions;
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

                writer.WriteStartElement("ETD_Beginning_Date");
                writer.WriteValue(operationDatePart1.FromDate.ToString("yyyy-MM-dd"));
                writer.WriteEndElement();

                writer.WriteStartElement("ETD_Ending_Date");
                writer.WriteValue(operationDatePart1.ToDate.ToString("yyyy-MM-dd"));
                writer.WriteEndElement();

                writer.WriteStartElement("SalesSet");
                writer.WriteValue(this.txtSales.Tag.TagToSplitString(","));
                writer.WriteEndElement();


                writer.WriteStartElement("ShipperSet");
                writer.WriteValue(this.txtBillTo.Tag.TagToSplitString(","));
                writer.WriteEndElement();


                writer.WriteStartElement("FeecodeSet");
                writer.WriteValue(this.txtChargingCode.Tag.TagToSplitString(","));
                writer.WriteEndElement();


                writer.WriteStartElement("JobType");
                writer.WriteValue(this.reportOperationTypePart1.EditValue);
                writer.WriteEndElement();

                writer.WriteStartElement("IsEnglish");
                writer.WriteValue(LocalData.IsEnglish ? "1" : "0");
                writer.WriteEndElement();

                writer.WriteStartElement("IsChangeData");
                writer.WriteValue(cmbIsAttach.SelectedIndex.ToString());
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
            paramList.Add(new ReportParameter("StructType", rdoOperationOrgial.Checked ? "0" : "1"));
            paramList.Add(new ReportParameter("StructNodeId", treeBoxSalesDep.GetAllEditValue.ToSplitString(",")));
            paramList.Add(new ReportParameter("ETD_Beginning_Date", operationDatePart1.FromDate.ToString("yyyy-MM-dd")));
            paramList.Add(new ReportParameter("ETD_Ending_Date", operationDatePart1.ToDate.ToString("yyyy-MM-dd")));
            paramList.Add(new ReportParameter("SalesSet",txtSales.Tag.TagToSplitString(",")));
            paramList.Add(new ReportParameter("ShipperSet", txtBillTo.Tag.TagToSplitString(",")));
            paramList.Add(new ReportParameter("FeecodeSet", txtChargingCode.Tag.TagToSplitString(",")));
            paramList.Add(new ReportParameter("JobType", this.reportOperationTypePart1.EditValue));
            paramList.Add(new ReportParameter("CompanyName", ReportCenterHelper.DefaultCompany.EShortName));//默认公司的英文名字
            paramList.Add(new ReportParameter("CompanyAddress", ReportCenterHelper.DefaultCompany.EAddress));
            paramList.Add(new ReportParameter("TelOrFax", ReportCenterHelper.DefaultCompanyTelAndFax));
            paramList.Add(new ReportParameter("JobPlace", this.JobPlace));
            paramList.Add(new ReportParameter("GroupCount", "2"));
            paramList.Add(new ReportParameter("GroupFields", ""));
            paramList.Add(new ReportParameter("ConditionRemark", (LocalData.IsEnglish ? "部门" : "Department") + "  : " + treeBoxSalesDep.EditText));
            paramList.Add(new ReportParameter("IsEnglish", LocalData.IsEnglish ? "1" : "0"));
            paramList.Add(new ReportParameter("Condition", this.XMLCondition));

            ReportData rd = new ReportData { Parameters = paramList ,ReportName ="RPT_DebitnoteForProfit_Detail" };
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
