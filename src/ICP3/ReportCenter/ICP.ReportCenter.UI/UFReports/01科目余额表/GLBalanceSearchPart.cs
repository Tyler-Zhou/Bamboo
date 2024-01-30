using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Common.ServiceInterface;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using Microsoft.Reporting.WinForms;
using ICP.ReportCenter.ServiceInterface;
using ICP.ReportCenter.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents;

namespace ICP.ReportCenter.UI.UFReports
{
    [ToolboxItem(false)]
    public partial class GLBalanceSearchPart : ReportBaseSearchPart
    {
        #region Init
        public GLBalanceSearchPart()
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
        #endregion

        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        List<CurrencyList> currencyList = new List<CurrencyList>();
        public ReportCenterHelper ReportCenterHelper
        {
            get
            {
                return ClientHelper.Get<ReportCenterHelper, ReportCenterHelper>();
            }
        }

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        public IFinanceReportService FinanceReportService
        {
            get
            {
                return ServiceClient.GetService<IFinanceReportService>();
            }
        }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        #endregion

        #region Load
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
            currencyList = ConfigureService.GetCurrencyList(null, null, null, true, 0);
            //开始时间&结束时间
            this.operationDatePart1.SetLastMonth();

            //开始科目&结束科目
            SearchBoxAdapter.RegisterGLCodeSingleSearchBox(DataFindClientService, this.txtFromGLCode, true, this.chkcmbCompany.CompanyIDs);
            SearchBoxAdapter.RegisterGLCodeSingleSearchBox(DataFindClientService, this.txtToGLCode, true, this.chkcmbCompany.CompanyIDs);

            //科目类型
            List<EnumHelper.ListItem<GLCodeType>> types = EnumHelper.GetEnumValues<GLCodeType>(LocalData.IsEnglish);
            cmbGlCodeType.Properties.BeginUpdate();
            cmbGlCodeType.Properties.Items.Clear();
            foreach (var item in types)
            {
                if (item.Value == GLCodeType.Unknown)
                {
                    cmbGlCodeType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "All" : "全部", item.Value));
                }
                else
                {
                    cmbGlCodeType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
                }
            }
            cmbGlCodeType.Properties.EndUpdate();
            this.cmbGlCodeType.SelectedIndex = 0;
            //报表格式
            Utility.BulidGLCodeLedgerStyle(cmbReportFormat);
            this.cmbCurrencyList.OnFirstEnter += this.OncmbCurrencyListFirstEnter;
        }
        private void OncmbCurrencyListFirstEnter(object sender, EventArgs e)
        {


            this.cmbCurrencyList.Properties.BeginUpdate();
            this.cmbCurrencyList.Properties.Items.Clear();
            foreach (var item in currencyList)
            {
                this.cmbCurrencyList.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Code, item.ID));
            }
            this.cmbCurrencyList.Properties.Items.Insert(0, new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", null));
            this.cmbCurrencyList.Properties.EndUpdate();
        }
        #endregion

        #region 属性
        public string XMLCondition
        {
            get
            {
                return string.Empty;
                //System.IO.StringWriter str = new System.IO.StringWriter();
                //XmlTextWriter writer = new XmlTextWriter(str);
                //writer.Formatting = Formatting.Indented;

                //writer.WriteStartDocument();
                //writer.WriteComment(" 查询条件XML");

                //writer.WriteStartElement("root");

                //writer.WriteStartElement("StructType");
                //writer.WriteValue(this.ucBusinessOrganizationSelect.OrganizationType.ToString());
                //writer.WriteEndElement();

                //writer.WriteStartElement("StructNodeId");
                //writer.WriteValue(this.ucBusinessOrganizationSelect.SelectedOrganizationString);
                //writer.WriteEndElement();

                //writer.WriteStartElement("ETD_Beginning_Date");
                //writer.WriteValue(operationDatePart1.FromDate.ToString("yyyy-MM-dd"));
                //writer.WriteEndElement();

                //writer.WriteStartElement("ETD_Ending_Date");
                //writer.WriteValue(operationDatePart1.ToDate.ToString("yyyy-MM-dd"));
                //writer.WriteEndElement();

                //writer.WriteStartElement("SalesType");
                //writer.WriteValue(cmbSalesType.SelectedIndex);
                //writer.WriteEndElement();

                //writer.WriteStartElement("SalesSet");
                //writer.WriteValue(this.txtSales.Tag.TagToSplitString(","));
                //writer.WriteEndElement();


                //writer.WriteStartElement("ConsignerSet");
                //writer.WriteValue(this.txtCustomer.Tag.TagToSplitString(","));
                //writer.WriteEndElement();


                //writer.WriteStartElement("ShipAgentSet");
                //writer.WriteValue(this.txtAgentOfCarrier.Tag.TagToSplitString(","));
                //writer.WriteEndElement();

                //writer.WriteStartElement("CarrierSet");
                //writer.WriteValue(this.txtCarrier.Tag.TagToSplitString(","));
                //writer.WriteEndElement();

                //writer.WriteStartElement("ShippingLineSet");
                //writer.WriteValue(this.chkcmbShipLine.EditValue.ToSplitString(","));
                //writer.WriteEndElement();

                //writer.WriteStartElement("JobType");
                //writer.WriteValue(this.reportOperationTypePart1.EditValue);
                //writer.WriteEndElement();

                //writer.WriteStartElement("LoadPortSet");
                //writer.WriteValue(this.stxtPOL.Tag.TagToSplitString(","));
                //writer.WriteEndElement();

                //writer.WriteStartElement("DiscPortSet");
                //writer.WriteValue(this.stxtPOD.Tag.TagToSplitString(","));
                //writer.WriteEndElement();

                //writer.WriteStartElement("DestPortSet");
                //writer.WriteValue(this.stxtPlaceOfDelivery.Tag.TagToSplitString(","));
                //writer.WriteEndElement();

                //writer.WriteStartElement("ProfitType");
                //writer.WriteValue(this.cmbDeficit.SelectedIndex);
                //writer.WriteEndElement();


                //writer.WriteStartElement("GroupString");
                //writer.WriteValue(this.chkcmbGroupBy.EditText);
                //writer.WriteEndElement();

                //writer.WriteStartElement("AgentSet");
                //writer.WriteValue(this.txtAgent.Tag.TagToSplitString(","));
                //writer.WriteEndElement();

                //writer.WriteStartElement("DateType");
                //writer.WriteValue("0");
                //writer.WriteEndElement();

                //writer.WriteStartElement("SCNO");
                //writer.WriteValue(this.txtContractNo.Text.Trim());
                //writer.WriteEndElement();

                //writer.WriteStartElement("IsEnglish");
                //writer.WriteValue(LocalData.IsEnglish ? "1" : "0");
                //writer.WriteEndElement();

                //writer.WriteEndElement();

                //writer.WriteEndDocument();

                //writer.Close();

                //return str.ToString();
            }
        }
        string currencyName;
        public string CurrencyName
        {
            get
            {
                if (string.IsNullOrEmpty(currencyName))
                {
                    ConfigureInfo configure = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                    if (configure != null)
                    {
                        currencyName = configure.StandardCurrency;
                    }
                }
                return currencyName;
            }
        }
        #endregion

        #region 查询
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

        SearchParameter _SearchParameter = new SearchParameter();
        public override object GetData()
        {
            List<Guid> companyIDs = this.chkcmbCompany.CompanyIDs;
            if (companyIDs == null || companyIDs.Count == 0)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Company must choose one." : "请至少选择一个公司.");
                return null;
            }

            ConfigureInfo configure = new ConfigureInfo();
            string StandardCurrency = "";

            foreach (Guid companyID in companyIDs)
            {
                configure = ConfigureService.GetCompanyConfigureInfo(companyID, LocalData.IsEnglish);
                if (configure == null)
                {
                    continue;
                }

                if (string.IsNullOrEmpty(StandardCurrency))
                {
                    StandardCurrency = configure.StandardCurrency;
                }

                if (StandardCurrency.ToUpper() != configure.StandardCurrency.ToUpper())
                {
                    MessageBoxService.ShowWarning("所选公司本位币不统一不能一起查询！", "提醒", MessageBoxButtons.OK);
                    return null;
                }
            }

            string fromGLCode = null, toGLCode = null;
            Guid? mCurrencyID = null;
            if (this.txtFromGLCode.Tag != null && this.txtFromGLCode.Tag != DBNull.Value)
            {
                fromGLCode = this.txtFromGLCode.Tag.ToString();
            }
            if (this.txtToGLCode.Tag != null && this.txtToGLCode.Tag != DBNull.Value)
            {
                toGLCode = this.txtToGLCode.Tag.ToString();
            }

            try
            {
                _SearchParameter = new SearchParameter
                {
                    CompanyIDs = companyIDs,
                    FromDate = DateTime.Parse(this.operationDatePart1.FromDate.ToShortDateString()),
                    ToDate = DateTime.Parse(this.operationDatePart1.ToDate.ToShortDateString()),
                    FromGLCode = fromGLCode,
                    ToGLCode = toGLCode,
                    GlCodeType = (GLCodeType)this.cmbGlCodeType.EditValue,
                    FromGLLevel = Convert.ToInt32(this.numFromGLLevel.Value),
                    ToGLLevel = Convert.ToInt32(this.numToGLLevel.Value),
                    ShowEndLevel = this.chkShowEndLevel.Checked,
                    ShowCumulation = this.chkShowCumulation.Checked,
                    Format = (GLCodeLedgerStyle)cmbReportFormat.EditValue,
                    CurrencyID = cmbCurrencyList.Visible ? currencyList.Find(r => r.Code == cmbCurrencyList.Text).ID : mCurrencyID,

                };

                List<GLBalanceData> list = FinanceReportService.GetGLBalanceDataList(_SearchParameter.CompanyIDs.ToArray(),
                                                               _SearchParameter.FromGLCode,
                                                               _SearchParameter.ToGLCode,
                                                               _SearchParameter.GlCodeType,
                                                               _SearchParameter.FromGLLevel,
                                                               _SearchParameter.ToGLLevel,
                                                               _SearchParameter.ShowEndLevel,
                                                               _SearchParameter.FromDate,
                                                               _SearchParameter.ToDate,
                                                               _SearchParameter.ShowCumulation,
                                                               _SearchParameter.CurrencyID,
                                                               _SearchParameter.Format);

                List<ReportParameter> paramList = new List<ReportParameter>();

                //截取字符串
                string companystr = string.Empty;
                string[] newCompanyStr = chkcmbCompany.EditText.Split(',');
                List<string> mycompanys = new List<string>();
                if (newCompanyStr.Length > 0)
                {
                    foreach (string com in newCompanyStr)
                    {
                        if (com.IndexOf("办公室") > 0)
                        {
                            mycompanys.Add(com);
                        }
                        if (com.IndexOf("区") < 0 && com.IndexOf("东南亚") < 0)
                        {
                            mycompanys.Add(com);
                        }
                    }
                }
                if (mycompanys.Count > 0)
                {
                    foreach (string com in mycompanys)
                    {
                        companystr += com + ",";
                    }
                    companystr = companystr.Substring(0, companystr.Length - 1);
                }

                paramList.Add(new ReportParameter("FromDate", _SearchParameter.FromDate.ToShortDateString()));
                paramList.Add(new ReportParameter("ToDate", _SearchParameter.ToDate.ToShortDateString()));
                paramList.Add(new ReportParameter("CompanyName", companystr));
                paramList.Add(new ReportParameter("CurrencyName", cmbCurrencyList.Visible ? cmbCurrencyList.Text : StandardCurrency));

                ReportData rd = new ReportData();
                rd.IsLocalReport = true;
                if (_SearchParameter.Format == GLCodeLedgerStyle.AMOUNT)
                    rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.UFGLBalanceReport.rdlc";
                else if (_SearchParameter.Format == GLCodeLedgerStyle.OUTGOAMOUNT)
                    rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.UFFCGLBalanceReport.rdlc";
                rd.Parameters = paramList;
                List<ReportDataSource> ds = new List<ReportDataSource>();
                ds.Add(new ReportDataSource("GLDataReport", list));
                rd.DataSource = ds;
                return rd;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this, (LocalData.IsEnglish ? "Get Report Data Failed." : "获取报表数据失败.") + ex.ToString());
                return null;
            }
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }
        #endregion

        #region  子表
        public override ReportData GetDrillthroughData(string reportEmbeddedResource, IList<ReportParameter> parameters)
        {
            if (_SearchParameter == null) return null;

            if (reportEmbeddedResource.Contains("UFGLDetailReport"))
            {
                #region 进入科目明细表
                try
                {
                    //科目ID
                    if (parameters[4].Values[0] == null)
                    {
                        return null;
                    }
                    List<GLDetailData> data = FinanceReportService.GetFCGLDetailBalanceList(
                                            _SearchParameter.CompanyIDs.ToArray(),
                                            new Guid(parameters[4].Values[0].ToString()),
                                            _SearchParameter.FromDate,
                                            _SearchParameter.ToDate);



                    ReportData rd = new ReportData();
                    List<ReportDataSource> ds = new List<ReportDataSource>();
                    ds.Add(new ReportDataSource("GLDataReport", data));
                    rd.DataSource = ds;
                    return rd;
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this, (LocalData.IsEnglish ? "Get Report Data Failed." : "获取报表数据失败.") + ex.ToString());
                    return null;
                }
                #endregion
            }
            if (reportEmbeddedResource.Contains("UFFCGLDetailReport"))
            {
                #region 进入外币式科目明细表
                try
                {
                    //科目ID
                    if (parameters[4].Values[0] == null)
                    {
                        return null;
                    }
                    List<GLDetailData> data = FinanceReportService.GetFCGLDetailBalanceList(
                                            _SearchParameter.CompanyIDs.ToArray(),
                                            new Guid(parameters[4].Values[0].ToString()),
                                            _SearchParameter.FromDate,
                                            _SearchParameter.ToDate);



                    ReportData rd = new ReportData();
                    List<ReportDataSource> ds = new List<ReportDataSource>();
                    ds.Add(new ReportDataSource("GLDataReport", data));
                    rd.DataSource = ds;
                    return rd;
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this, (LocalData.IsEnglish ? "Get Report Data Failed." : "获取报表数据失败.") + ex.ToString());
                    return null;
                }
                #endregion
            }
            else if (reportEmbeddedResource.Contains("UFShowLedgerReport"))
            {
                #region 进入显示凭证明细
                Utility.ShoLedgerInfo(reportEmbeddedResource, parameters);

                return null;
                #endregion
            }
            return null;
        }
        #endregion

        #region  控件事件
        private void chkShowEndLevel_CheckedChanged(object sender, EventArgs e)
        {
            this.numFromGLLevel.Enabled = this.numToGLLevel.Enabled = this.chkShowCumulation.Checked;
        }
        #endregion


        class SearchParameter
        {
            public List<Guid> CompanyIDs { get; set; }
            public string FromGLCode { get; set; }
            public string ToGLCode { get; set; }
            public GLCodeType GlCodeType { get; set; }
            public int FromGLLevel { get; set; }
            public int ToGLLevel { get; set; }
            public bool ShowEndLevel { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public bool ShowCumulation { get; set; }
            public GLCodeLedgerStyle Format { get; set; }
            public Guid? CurrencyID { get; set; }
        }

        private void cmbReportFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbReportFormat.SelectedIndex == 1)
            {
                labelControl1.Visible = true;
                cmbCurrencyList.Visible = true;
            }
            else
            {
                labelControl1.Visible = false;
                cmbCurrencyList.Visible = false;
            }
        }
    }
}
