using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Microsoft.Reporting.WinForms;
using System.IO;
using System.Net;
using System.Security.Principal;

namespace LongWin.DataWarehouseReport.WinUI
{
    /// <summary>
    /// 报表显示，需设置reportName和ParamList的值，然后调用Display即可，也可重写该控件
    /// </summary>
    public partial class ReportViewBase : UserControl
    {
        private const string WELCOME = "请输入你要检索的有效查询条件，并点击 ＂查询＂ 开始显示报表";
        private const string READING_DATA = "数据读取中，请稍等..........";

        private string _reportName;
        /// <summary>
        /// 报表的名称，不加后缀名

        /// </summary>
        public string ReportName
        {
            set { this._reportName = value; }
        }
        private List<ReportParameter> _paramList;
        /// <summary>
        /// 报表的参数列表

        /// </summary>
        public List<ReportParameter> ParamList
        {
            set { this._paramList = value; }
        }
        
        

        public ReportViewBase()
        {
            InitializeComponent();
            SetReportServerInfo();

         
        }

        


        public void DisplayLocalReport(string reportName, List<ReportDataSource> dataSouce, IEnumerable<ReportParameter> reportParam)
        {
            this.reportViewer.Reset();
            this.reportViewer.ProcessingMode = ProcessingMode.Local;
            this.reportViewer.LocalReport.ReportEmbeddedResource = reportName;
            this.reportViewer.LocalReport.DataSources.Clear();

            this.reportViewer.LocalReport.DataSources.Clear();

            foreach (ReportDataSource data in dataSouce)
            {
                this.reportViewer.LocalReport.DataSources.Add(data);
            }            

            if (reportParam != null)
            {
                this.reportViewer.LocalReport.SetParameters(reportParam);
            }

            

            this.reportViewer.RefreshReport();
        }


        //string Id;
        /// <summary>
        /// 显示报表，必须首先设置reportName和ParamList的值
        /// </summary>
        public void DisplayData()
        {
            //如果没有设置参数的值就返回
            if (this._reportName == "" || this._paramList == null) return;
            while(this.reportViewer.ServerReport.IsDrillthroughReport)
            {
                this.reportViewer.PerformBack();
            }

            SetReportServerInfo();


            //this.reportViewer.ServerReport.ReportServerCredentials.SetFormsCredentials(null, "ReportUser", "longwin", null);
            this.reportViewer.ServerReport.ReportPath = "/LongWin.DataWarehouseReport.Report/" + this._reportName;
            this.reportViewer.ServerReport.SetParameters(this._paramList);
            //默认开始时为打印模式，但是打印模式无法展开隐藏的文本，所以暂时屏蔽

            //this.reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            //this.reportViewer.ZoomMode = ZoomMode.Percent;
            //this.reportViewer.ZoomPercent = 100;
           

            this.reportViewer.RefreshReport();

            this.Cursor = Cursors.Default;
        }

        public void DisplayData(string reportFolder)
        {
            if (this.reportViewer.ServerReport.IsDrillthroughReport)
            {
                this.reportViewer.PerformBack();
            }
            SetReportServerInfo();

            //如果没有设置参数的值就返回
            if (this._reportName == "" || this._paramList == null) return;
            while (this.reportViewer.ServerReport.IsDrillthroughReport)
            {
                this.reportViewer.PerformBack();
            }

            //this.reportViewer.ServerReport.ReportServerCredentials.SetFormsCredentials(null, "ReportUser", "longwin", null);
            this.reportViewer.ServerReport.ReportPath = reportFolder + this._reportName;
            this.reportViewer.ServerReport.SetParameters(this._paramList);
            //默认开始时为打印模式，但是打印模式无法展开隐藏的文本，所以暂时屏蔽

            //this.reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            //this.reportViewer.ZoomMode = ZoomMode.Percent;
            //this.reportViewer.ZoomPercent = 100;
            
            this.reportViewer.RefreshReport();

            this.Cursor = Cursors.Default;
        }

        private void SetReportServerInfo()
        {
            // this.reportViewer.ServerReport.ReportServerUrl = new Uri("http://rpt.cityocean.com/ReportServer");
            //this.reportViewer.ServerReport.ReportServerUrl = new Uri("http://192.168.99.32/ReportServer");
          
            this.reportViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            this.reportViewer.ServerReport.ReportServerUrl = new Uri(Utility.ReportServerInfos.ReportUrl);
            System.Net.NetworkCredential myCred = new System.Net.NetworkCredential(Utility.ReportServerInfos.ReportUser, Utility.ReportServerInfos.ReportUserPSW, null);
            //MessageBox.Show(Utility.ReportServerInfos.ReportUser + '/' + Utility.ReportServerInfos.ReportUserPSW);
            this.reportViewer.ServerReport.ReportServerCredentials.NetworkCredentials = myCred;
        }

        

        private void reportViewer_Load(object sender, EventArgs e)
        {

        }

        //#region 按钮事件
        //public event EventHandler OpenSearch;
        //private void tsbOpenSearch_Click(object sender, EventArgs e)
        //{
        //    if (this.OpenSearch != null)
        //    {
        //        this.OpenSearch(sender, e);
        //    }
        //}

        //public event EventHandler CloseForm;
        //private void tsbExit_Click(object sender, EventArgs e)
        //{
        //    if (this.CloseForm != null)
        //    {
        //        this.CloseForm(sender, e);
        //    }
        //}

        //private void tsbStop_Click(object sender, EventArgs e)
        //{
        //    this.reportViewer.CancelRendering(0);
        //}

        //private void tsbRefresh_Click(object sender, EventArgs e)
        //{
        //    this.SetEnableFalse();
        //    this.Refresh();

        //    this.reportViewer.RefreshReport();
        //    this.SetEnableTrue();
        //}

        //#region 转到
        //private void tsbFirstPage_Click(object sender, EventArgs e)
        //{
        //    this.reportViewer.CurrentPage = 1;

        //    this.tsbFirstPage.Enabled = false;
        //    this.tsbPrepage.Enabled = false;

        //    if (this.reportViewer.ServerReport.GetTotalPages() == 1)
        //    {
        //        this.tsbNextPage.Enabled = false;
        //        this.tsbLastPage.Enabled = false;
        //        this.tsbGoto.Enabled = false;
        //    }
        //    else
        //    {
        //        this.tsbNextPage.Enabled = true;
        //        this.tsbLastPage.Enabled = true;
        //        this.tsbGoto.Enabled = true;
        //    }
        //}

        //private void tsbPrepage_Click(object sender, EventArgs e)
        //{
        //    if (this.reportViewer.CurrentPage > 1)
        //    {
        //        this.reportViewer.CurrentPage = this.reportViewer.CurrentPage - 1;
        //    }

        //    if (this.reportViewer.CurrentPage == 1)
        //    {
        //        this.tsbFirstPage.Enabled = false;
        //        this.tsbPrepage.Enabled = false;
        //    }
        //    else
        //    {
        //        this.tsbFirstPage.Enabled = true;
        //        this.tsbPrepage.Enabled = true;
        //    }
        //    if (this.reportViewer.ServerReport.GetTotalPages() == 1)
        //    {
        //        this.tsbNextPage.Enabled = false;
        //        this.tsbLastPage.Enabled = false;
        //        this.tsbGoto.Enabled = false;
        //    }
        //    else
        //    {
        //        this.tsbNextPage.Enabled = true;
        //        this.tsbLastPage.Enabled = true;
        //        this.tsbGoto.Enabled = true;
        //    }

        //}

        //private void tsbNextPage_Click(object sender, EventArgs e)
        //{
        //    if (this.reportViewer.CurrentPage < this.reportViewer.ServerReport.GetTotalPages() - 1)
        //    {
        //        this.reportViewer.CurrentPage = this.reportViewer.CurrentPage + 1;
        //    }
        //    if (this.reportViewer.CurrentPage == 1)
        //    {
        //        this.tsbFirstPage.Enabled = false;
        //        this.tsbPrepage.Enabled = false;
        //    }
        //    else
        //    {
        //        this.tsbFirstPage.Enabled = true;
        //        this.tsbPrepage.Enabled = true;
        //    }
        //    if (this.reportViewer.ServerReport.GetTotalPages() == this.reportViewer.CurrentPage)
        //    {
        //        this.tsbNextPage.Enabled = false;
        //        this.tsbLastPage.Enabled = false;
        //    }
        //    else
        //    {
        //        this.tsbNextPage.Enabled = true;
        //        this.tsbLastPage.Enabled = true;
        //    }
        //    if (this.reportViewer.ServerReport.GetTotalPages() == 1)
        //    {
        //        this.tsbGoto.Enabled = false;
        //    }
        //    else
        //    {
        //        this.tsbGoto.Enabled = true;
        //    }

        //}

        //private void tsbLastPage_Click(object sender, EventArgs e)
        //{
        //    this.reportViewer.CurrentPage = this.reportViewer.ServerReport.GetTotalPages();
        //    this.tsbLastPage.Enabled = false;
        //    this.tsbNextPage.Enabled = false;
        //    if (this.reportViewer.CurrentPage == 1)
        //    {
        //        this.tsbFirstPage.Enabled = false;
        //        this.tsbPrepage.Enabled = false;
        //        this.tsbGoto.Enabled = false;
        //    }
        //    else
        //    {
        //        this.tsbFirstPage.Enabled = true;
        //        this.tsbPrepage.Enabled = true;
        //        this.tsbGoto.Enabled = true;
        //    }
        //}

        //private void tsbGoto_Click(object sender, EventArgs e)
        //{
        //    int pageIndex = 1;
        //    try { pageIndex = System.Convert.ToInt32(this.tsTxtGo.Text.Trim()); }
        //    catch
        //    {
        //        this.tsTxtGo.Text = "1";
        //    }
        //    this.reportViewer.CurrentPage = pageIndex;
        //}
        //#endregion

        //private void tsbGoBack_Click(object sender, EventArgs e)
        //{
        //    if (this.reportViewer.ServerReport.IsDrillthroughReport)
        //    {
        //        this.reportViewer.PerformBack();
        //    }
        //    else { this.reportViewer.Enabled = false; }
        //}
        //private void tsbPrint_Click(object sender, EventArgs e)
        //{
        //    this.reportViewer.PrintDialog();
        //}

        //#region 缩放
        //private void tsbZoom500_Click(object sender, EventArgs e)
        //{
        //    this.reportViewer.ZoomMode = ZoomMode.Percent;
        //    this.reportViewer.ZoomPercent = 500;
        //}

        //private void tsbZoom400_Click(object sender, EventArgs e)
        //{
        //    this.reportViewer.ZoomMode = ZoomMode.Percent;
        //    this.reportViewer.ZoomPercent = 400;
        //}

        //private void tsbZoom300_Click(object sender, EventArgs e)
        //{
        //    this.reportViewer.ZoomMode = ZoomMode.Percent;
        //    this.reportViewer.ZoomPercent = 300;
        //}

        //private void tsbZoom200_Click(object sender, EventArgs e)
        //{
        //    this.reportViewer.ZoomMode = ZoomMode.Percent;
        //    this.reportViewer.ZoomPercent = 200;
        //}

        //private void tsb150_Click(object sender, EventArgs e)
        //{
        //    this.reportViewer.ZoomMode = ZoomMode.Percent;
        //    this.reportViewer.ZoomPercent = 150;
        //}

        //private void tsbZoom100_Click(object sender, EventArgs e)
        //{
        //    this.reportViewer.ZoomMode = ZoomMode.Percent;
        //    this.reportViewer.ZoomPercent = 100;
        //}

        //private void tsbZoom75_Click(object sender, EventArgs e)
        //{
        //    this.reportViewer.ZoomMode = ZoomMode.Percent;
        //    this.reportViewer.ZoomPercent = 75;
        //}

        //private void tsbZoom50_Click(object sender, EventArgs e)
        //{
        //    this.reportViewer.ZoomMode = ZoomMode.Percent;
        //    this.reportViewer.ZoomPercent = 50;
        //}

        //private void tsbZoom25_Click(object sender, EventArgs e)
        //{
        //    this.reportViewer.ZoomMode = ZoomMode.Percent;
        //    this.reportViewer.ZoomPercent = 25;
        //}

        //private void tsbZoomWholePage_Click(object sender, EventArgs e)
        //{
        //    this.reportViewer.ZoomMode = ZoomMode.FullPage;
        //}

        //private void tsbZoomWholeWith_Click(object sender, EventArgs e)
        //{
        //    this.reportViewer.ZoomMode = ZoomMode.PageWidth;
        //}
        //#endregion

        //#region 查询

        //private void tsbSearch_Click(object sender, EventArgs e)
        //{
        //    if (this.tsbTxtFind.Text != string.Empty)
        //    {
        //        if (this.reportViewer.Find(this.tsbSearch.Text.Trim(), this.reportViewer.CurrentPage) == 0)
        //        {
        //            MessageBox.Show("对不起，没有找到您需要的内容！");
        //        }
        //        else
        //        {
        //            this.tsbFindNext.Enabled = true;
        //        }
        //    }
        //}

        //private void tsbFindNext_Click(object sender, EventArgs e)
        //{
        //    if (this.tsbTxtFind.Text != string.Empty)
        //    {
        //        this.reportViewer.FindNext();
        //    }
        //}
        //#endregion

        //#region 导出
        //SaveFileDialog filedia;
        //private void tsbSavePdf_Click(object sender, EventArgs e)
        //{
        //    filedia = new SaveFileDialog();
        //    filedia.AddExtension = true;
        //    filedia.FileName = this._reportName + ".pdf";
            
        //    filedia.Filter = "Adobe Reader(*.pdf)|*.pdf|Excel文件(*.xls)|*.xls";

        //    if (filedia.ShowDialog() == DialogResult.OK)
        //    {
        //        string filePath = filedia.FileName;


        //        this.Export(this.reportViewer.ServerReport, "PDF", filePath);
        //    }
        //}

        //private void tsbSaveXls_Click(object sender, EventArgs e)
        //{
        //    filedia = new SaveFileDialog();
        //    filedia.AddExtension = true;
        //    filedia.FileName = this._reportName + ".xls";
        //    filedia.Filter = "Excel文件(*.xls)|*.xls|Adobe Reader(*.pdf)|*.pdf";

        //    if (filedia.ShowDialog() == DialogResult.OK)
        //    {
        //        string filePath = filedia.FileName;


        //        this.Export(this.reportViewer.ServerReport, "Excel", filePath);
        //    }
        //}


        ////导出
        //private void Export(ServerReport report, string Format, string filePath)
        //{
        //    //string deviceInfo =
        //    //  "<DeviceInfo>" +
        //    //  "  <OutputFormat>" + Format + "</OutputFormat>" +
        //    //  "  <PageWidth>8.5in</PageWidth>" +
        //    //  "  <PageHeight>11in</PageHeight>" +
        //    //  "  <MarginTop>0.25in</MarginTop>" +
        //    //  "  <MarginLeft>0.25in</MarginLeft>" +
        //    //  "  <MarginRight>0.25in</MarginRight>" +
        //    //  "  <MarginBottom>0.25in</MarginBottom>" +
        //    //  "</DeviceInfo>";


        //    Microsoft.Reporting.WinForms.Warning[] Warnings;
        //    string[] strStreamIds;
        //    string strMimeType;
        //    string strEncoding;
        //    string strFileNameExtension;

        //    byte[] bytes = report.Render(Format, null, out strMimeType, out strEncoding, out strFileNameExtension, out strStreamIds, out Warnings);


        //    using (System.IO.FileStream fs = new FileStream(filePath, FileMode.Create))
        //    {
        //        fs.Write(bytes, 0, bytes.Length);
        //    }

        //    if (MessageBox.Show("报表打印： \r\n    成功导出文件！" + filePath + "\r\n    要现在打开文件" + filePath + "吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //    {
        //        System.Diagnostics.Process.Start(filePath);
        //    }

        //}
        //#endregion

        //#endregion

        //#region 自定义方法

        //public void SetEnableFalse()
        //{
        //    this.tsbFirstPage.Enabled = false;
        //    this.tsbPrepage.Enabled = false;
        //    this.tsbNextPage.Enabled = false;
        //    this.tsbLastPage.Enabled = false;
        //    this.tsbGoto.Enabled = false;
        //    this.tsbGoBack.Enabled = false;
        //    this.tsbRefresh.Enabled = false;
        //    this.tsbSave.Enabled = false;
        //    this.tsbFindNext.Enabled = false;
        //    this.tsbSearch.Enabled = false;
        //    this.tsbZoom.Enabled = false;
        //    this.tsbExit.Enabled = false;
        //    this.tsbOpenSearch.Enabled = false;
        //    this.tsTxtGo.Enabled = false;
        //    this.tsbTxtFind.Enabled = false;
        //    this.tsbPrint.Enabled = false;

        //    this.tsbStop.Enabled = true;
        //}

        //public void SetEnableTrue()
        //{
        //    //如果总页数只有一页

        //    if (this.reportViewer.ServerReport.GetTotalPages() > 1)
        //    {
        //        this.tsbFirstPage.Enabled = true;
        //        this.tsbPrepage.Enabled = true;
        //        this.tsbNextPage.Enabled = true;
        //        this.tsbLastPage.Enabled = true;
        //        this.tsbGoto.Enabled = true;
        //    }
        //    else 
        //    {
        //        this.tsbFirstPage.Enabled = false;
        //        this.tsbPrepage.Enabled = false;
        //        this.tsbNextPage.Enabled = false;
        //        this.tsbLastPage.Enabled = false;
        //        this.tsbGoto.Enabled = false;
        //    }
        //    if (this.reportViewer.ServerReport.IsDrillthroughReport)
        //    {
        //        this.tsbGoBack.Enabled = true;
        //    }
        //    else
        //    {
        //        this.tsbGoBack.Enabled = false;
        //    }
        //    this.tsbRefresh.Enabled = true;
        //    this.tsbSave.Enabled = true;
        //    this.tsbSearch.Enabled = true;
        //    this.tsbZoom.Enabled = true;
        //    this.tsbExit.Enabled = true;
        //    this.tsbOpenSearch.Enabled = true;
            
        //    this.tsbTxtFind.Enabled = true;
        //    this.tsbPrint.Enabled = true;

        //    this.tsbStop.Enabled = false;
        //}
        //#endregion


    }
}
