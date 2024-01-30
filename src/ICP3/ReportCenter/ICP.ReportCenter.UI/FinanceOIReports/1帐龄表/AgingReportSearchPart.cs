using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.ReportCenter.UI.Common.Helper;
using System.Xml;
using ICP.Framework.ClientComponents;

namespace ICP.ReportCenter.UI.FinanceOIReports
{
    /// <summary>
    /// 海运进口账龄表
    /// </summary>
    [ToolboxItem(false)]
    public partial class AgingReportSearchPart : ReportBaseSearchPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public ICP.FAM.ServiceInterface.IFinanceReportService FinanceReportService
        {
            get
            {
                return ServiceClient.GetService<ICP.FAM.ServiceInterface.IFinanceReportService>();
            }
        }

        #endregion

        #region  init

        public AgingReportSearchPart()
        {
            InitializeComponent();
            chkcmbCompany.SplitText = "&";
            Disposed += delegate
            {
                OnSearched = null;
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

            dteEndingDate.DateTime = Utility.GetEndDate(DateTime.Now);
            dteFrom.DateTime = DateTime.Now.Date;
            dteTo.DateTime = Utility.GetEndDate(DateTime.Now);
            rdoSummary.CheckedChanged += new EventHandler(Style_CheckedChanged);
            rdoDetail.CheckedChanged += new EventHandler(Style_CheckedChanged);
            rdoCA_Detail.CheckedChanged += new EventHandler(Style_CheckedChanged);
        }

        void Style_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSummary.Checked || rdoDetail.Checked)
            {
                panelDate.Enabled = true;
                panelETD.Enabled = false;
            }
            else
            {
                panelDate.Enabled = false;
                panelETD.Enabled = true;
            }
        }


        #endregion

        #region 属性


        /// <summary>
        /// 用 & 分割的报表类型
        /// </summary>
        public string BillTypesString
        {
            get
            {
                StringBuilder strBuilder = new StringBuilder();
                if (chkAR.Checked)
                {
                    strBuilder = strBuilder.Append(chkAR.Text);
                }
                if (chkAP.Checked)
                {
                    if(strBuilder.Length>0)
                        strBuilder = strBuilder.Append("& ");
                    strBuilder = strBuilder.Append(chkAP.Text);
                }
                if (chkDRCR.Checked)
                {
                    if (strBuilder.Length > 0)
                        strBuilder = strBuilder.Append("& ");
                    strBuilder = strBuilder.Append(chkDRCR.Text);
                }
                return strBuilder.ToString();
            }
        }

        List<BillType> BillTypes
        {
            get
            {
                List<BillType> billTypes = new List<BillType>();
                if (chkAP.Checked) billTypes.Add(BillType.AP);
                if (chkAR.Checked) billTypes.Add(BillType.AR);
                if (chkDRCR.Checked) billTypes.Add(BillType.DC);
                return billTypes;
            }
        }

        string XMLCondition
        {
            get
            {
                System.IO.StringWriter str = new System.IO.StringWriter();
                XmlTextWriter writer = new XmlTextWriter(str);
                writer.Formatting = Formatting.Indented;

                writer.WriteStartDocument();
                writer.WriteComment(" Search Condition XML");

                writer.WriteStartElement("root");

                writer.WriteStartElement("EndingDate");
                writer.WriteValue(dteEndingDate.DateTime.ToShortDateString());
                writer.WriteEndElement();

                writer.WriteStartElement("CompanyIds");
                writer.WriteValue(chkcmbCompany.CompanyIDs.ToArray().Join4Report());
                writer.WriteEndElement();

                writer.WriteStartElement("BillTypes");
                writer.WriteValue(BillTypes.ToArray().Join4Report());
                writer.WriteEndElement();

                writer.WriteStartElement("OperationTypes");
                writer.WriteValue(chkcmbOperaionType.SelectedOperationTypes.ToArray().Join4Report());
                writer.WriteEndElement();

                writer.WriteStartElement("SearchType");
                writer.WriteValue(rdoFinanceDate.Checked ? (short)0 : (short)1);
                writer.WriteEndElement();

                writer.WriteStartElement("CustomerId");
                writer.WriteValue(txtCustomer.Tag == null ? "" : txtCustomer.Tag.ToString());
                writer.WriteEndElement();

                writer.WriteStartElement("TermType");
                writer.WriteValue((rdoGroupTerm.SelectedIndex + 1));
                writer.WriteEndElement();

                writer.WriteStartElement("InsuredOption");
                writer.WriteValue((rdoGroupInsured.SelectedIndex + 1));
                writer.WriteEndElement();

                writer.WriteStartElement("AgingDateState");
                writer.WriteValue(rdoAgingDateState.SelectedIndex);
                writer.WriteEndElement();

                writer.WriteStartElement("FromDate");
                writer.WriteValue(dteFrom.DateTime.ToShortDateString());
                writer.WriteEndElement();

                writer.WriteStartElement("ToDate");
                writer.WriteValue(dteTo.DateTime.ToShortDateString());
                writer.WriteEndElement();

                writer.WriteStartElement("Currency");
                writer.WriteValue("All".Equals(cmbCurrency.Text)?"":cmbCurrency.Text);
                writer.WriteEndElement();

                writer.WriteStartElement("OnlyOverPaid");
                writer.WriteValue(chkOverPaid.Checked);
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
            btnSearch.Enabled = false;
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
                btnSearch.Enabled = true;
            }
        }

        #endregion

        #region ISearchPart 成员
        //SearchParameter _SearchParameter;
        public override object GetData()
        {
            #region 验证必输项
            if (chkcmbCompany.EditValue == null || chkcmbCompany.EditValue.Count == 0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Company type must choose one." : "请至少选择一个公司.");
                return null;
            }
            if (chkcmbOperaionType.EditValue == null || chkcmbOperaionType.EditValue.Count == 0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "OperaionType type must choose one." : "请至少选择一种业务类型.");
                return null;
            }

           
            if (BillTypes.Count == 0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "BillType type must choose one." : "请至少选择一种帐单类型.");
                return null;
            }
            #endregion

            #region Bulid SearchParameter


          //  _SearchParameter = new SearchParameter
          //{
          //    BeginDate = DateTime.Parse(dteFrom.DateTime.ToShortDateString()),
          //    EndDate = DateTime.Parse(dteTo.DateTime.ToShortDateString()),
          //    BillTypes = BillTypes,
          //    CompanyIDs = chkcmbCompany.CompanyIDs,
          //    OperationTypes = chkcmbOperaionType.SelectedOperationTypes,
          //    Currency = cmbCurrency.SelectedCurrencyName ,
          //    CustomerID = txtCustomer.Tag == null ? Guid.Empty : new Guid(txtCustomer.Tag.ToString()) ,
          //    EndingDate = dteEndingDate.DateTime ,
          //    OnlyOverPaid = chkOverPaid.Checked ,
          //    SearchType = rdoFinanceDate.Checked ? (short)0 : (short)1
          //};

            #endregion

            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("CompanyName", chkcmbCompany.EditText));
            paramList.Add(new ReportParameter("ClosingDate", dteEndingDate.DateTime.ToShortDateString()));
            paramList.Add(new ReportParameter("ReportType", BillTypesString));
            paramList.Add(new ReportParameter("Condition", XMLCondition));
            paramList.Add(new ReportParameter("IsEnglish", LocalData.IsEnglish?"1":"0"));

            try
            {


                AgingDateState agingDateSate = (AgingDateState)rdoAgingDateState.SelectedIndex;
                TermType termtype = (TermType)rdoGroupTerm.SelectedIndex + 1;

                if (rdoSummary.Checked)//Total
                {
                    #region Total
                    ReportData rd = new ReportData
                    {
                        Parameters = paramList,
                        ServiceReportPath = ReportPathConstants.AccountingReportByImport,
                        ReportName = "RPT_AgingReport_Summary"
                    };
                    return rd;
                    #endregion
                }
                else if (rdoDetail.Checked)//Detail
                {
                    #region Detail
                    paramList.Add(new ReportParameter("CustomerId", txtCustomer.Tag == null ? "" : txtCustomer.Tag.ToString()));
                    ReportData rd = new ReportData
                    {
                        Parameters = paramList,
                        ServiceReportPath = ReportPathConstants.AccountingReportByImport,
                        ReportName = "RPT_AgingReport_Detail"
                    };
                    
                    return rd;
                    #endregion
                }
                else if (rdoCA_Detail.Checked)//CA_Detail
                {
                    #region CA_Detail

                    paramList.Add(new ReportParameter("Currency", "All".Equals(cmbCurrency.Text) ? "" : cmbCurrency.Text));
                    paramList.Add(new ReportParameter("FromETD", dteFrom.DateTime.ToShortDateString()));
                    paramList.Add(new ReportParameter("ToETD", dteTo.DateTime.ToShortDateString()));

                    ReportData rd = new ReportData
                    {
                        Parameters = paramList,
                        ServiceReportPath = ReportPathConstants.AccountingReportByImport,
                        ReportName = "RPT_AgingReport_CADetail"
                    };

                    return rd;

                    #endregion
                }

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

        //public override ReportData GetDrillthroughData(string reportEmbeddedResource, IList<ReportParameter> parameters)
        //{
        //    if (_SearchParameter == null) return null;

        //    try
        //    {
        //        if (reportEmbeddedResource.Contains("AgingReport_Total_Detail.rdlc"))
        //        {
        //            AgingDateState agingDateSate = (AgingDateState)rdoAgingDateState.SelectedIndex;
        //            TermType termtype = (TermType)rdoGroupTerm.SelectedIndex + 1;

        //            List<AgingReportDetailData> DetailDataList = FinanceReportService.GetAgingReportForDetail
        //                (_SearchParameter.EndingDate
        //              , _SearchParameter.CompanyIDs.ToArray()
        //              , _SearchParameter.BillTypes.ToArray()
        //              , _SearchParameter.OperationTypes.ToArray()
        //              , new Guid(parameters[0].Values[0].ToString())
        //              , parameters[5].Values[0].ToString()
        //              , _SearchParameter.SearchType
        //              , _SearchParameter.OnlyOverPaid
        //              , agingDateSate
        //              , termtype
        //              );

        //            ReportData rd = new ReportData();
        //            List<ReportDataSource> ds = new List<ReportDataSource>();
        //            ds.Add(new ReportDataSource("AgingReportTotal_DetailData", DetailDataList));
        //            rd.DataSource = ds;
        //            return rd;

        //        }
        //        else
        //        {
        //            List<AgingReportFeeData> FeeDetailDataList = FinanceReportService.GetAgingReportForFeeDetail
        //                (new Guid(parameters[0].Values[0].ToString()));


        //            ReportData rd = new ReportData();
        //            List<ReportDataSource> ds = new List<ReportDataSource>();
        //            ds.Add(new ReportDataSource("BillFeeData", FeeDetailDataList));
        //            rd.DataSource = ds;
        //            return rd;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LocalCommonServices.ErrorTrace.SetErrorInfo(this, (LocalData.IsEnglish ? "Get Report Data Failed." : "获取报表数据失败.") + ex.ToString());
        //        return null;
        //    }
        //}

        #endregion

        class SearchParameter
        {
            public DateTime BeginDate { get; set; }
            public DateTime EndDate { get; set; }

            public DateTime EndingDate { get; set; }
            public List<Guid> CompanyIDs { get; set; }
            public List<BillType> BillTypes { get; set; }
            public List<OperationType> OperationTypes { get; set; }
            public Guid? CustomerID { get; set; }
            public string Currency { get; set; }
            public short SearchType { get; set; }
            public bool OnlyOverPaid { get; set; }
        }
    }
}
