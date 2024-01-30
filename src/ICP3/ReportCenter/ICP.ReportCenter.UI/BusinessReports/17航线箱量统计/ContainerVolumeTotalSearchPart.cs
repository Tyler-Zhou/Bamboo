using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using System.Xml;
using ICP.FRM.ServiceInterface;
using ICP.ReportCenter.ServiceInterface.DataObjects;
using DevExpress.XtraEditors.Controls;
using ICP.ReportCenter.ServiceInterface;
using ICP.Sys.ServiceInterface;

namespace ICP.ReportCenter.UI.BusinessReports
{
    [ToolboxItem(false)]
    public partial class ContainerVolumeTotalSearchPart : ReportBaseSearchPart
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

        public IBusinessInfoService BusinessInfoService
        {
            get
            {
                return ServiceClient.GetService<IBusinessInfoService>();
            }
        }

        public IReportCenterService ReportCenterService
        {
            get
            {
                return ServiceClient.GetService<IReportCenterService>();
            }
        }

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        #endregion

        #region 私有字段
        /// <summary>
        /// 周日期对象列表
        /// </summary>
        List<DateWeekly> dateWeeklyList = new List<DateWeekly>();
        #endregion

        #region  init

        public ContainerVolumeTotalSearchPart()
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
                rdoCustomizing.CheckedChanged += delegate
            {
                dateFrom.Enabled = dateTo.Enabled = rdoCustomizing.Checked;
            };

                rdoDefault.CheckedChanged += delegate
                {
                    cmbWeekDate.Enabled = rdoDefault.Checked;
                };
            }
        }

        private void InitControls()
        {
            //获取服务器的时间，不能用本机的时间
            DateTime dtStart = new DateTime(2011, 01, 01);
            DateTime dtEnd = BusinessInfoService.GetServerDate();

            dateWeeklyList = new List<DateWeekly>();

            int i = 1;
            for (DateTime dt = dtStart; dt <= dtEnd; dt = dt.AddDays(0))
            {
                DateWeekly item = new DateWeekly();
                item.StartDate = dt.Date;
                item.Index = i++;
                item.Year = dt.Year;
                item.Weekly = GetWeekOfYear(dt);

                //结束日期为dt的所在周的最后一天
                DateTime startWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d")));
                item.EndDate = startWeek.AddDays(6);

                dt = dt.AddDays(((item.EndDate - item.StartDate).Days + 1));

                dateWeeklyList.Add(item);
            }

            dateWeeklyList = (from d in dateWeeklyList orderby d.Index descending select d).ToList();

            foreach (DateWeekly item in dateWeeklyList)
            {
                this.cmbWeekDate.Properties.Items.Add(new ImageComboBoxItem(item.ItemName, item.Index.ToString()));
            }

            this.cmbWeekDate.SelectedIndex = 0;


            SearchBoxAdapter.RegisterMultipleSearchBox(DataFindClientService, txtSales, SystemFinderConstants.UserFinder);

        }


        /// <summary>
        /// 获得指定日期在当年中所处的周数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private int GetWeekOfYear(DateTime date)
        {
            int firstWeekend = 7 - Convert.ToInt32(DateTime.Parse(date.Year + "-1-1").DayOfWeek);

            int currentDay = date.DayOfYear;

            return Convert.ToInt32(Math.Ceiling((currentDay - firstWeekend) / 7.0)) + 1;
        }


        /// <summary>
        /// 获得当前选择的日期
        /// </summary>
        /// <returns></returns>
        private DateWeekly GetSeletctWeekly()
        {
            if (cmbWeekDate.EditValue == null)
            {
                return null;
            }
            int index = Convert.ToInt32(cmbWeekDate.EditValue);

            DateWeekly currencyItem = (from d in dateWeeklyList where d.Index == index select d).SingleOrDefault();
            if (currencyItem == null)
            {
                return null;
            }
            return currencyItem;
        }

        #endregion

        #region event
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (OnSearched != null)
            {
                using (new CursorHelper())
                {
                    OnSearched(this, GetData());
                }
            }
        }

        #endregion

        #region ISearchPart 成员

        public override object GetData()
        {
            List<Guid> customerIDList = new List<Guid>();
            if (this.txtCustomer.Tag != null)
            {
                customerIDList = this.txtCustomer.Tag as List<Guid>;
            }

            List<Guid> shippinglines = new List<Guid>();
            if (this.chkcmbShipLine.EditValue != null)
            {
                shippinglines = this.chkcmbShipLine.EditValue as List<Guid>;
            }

            List<Guid> salesids = new List<Guid>();
            if (this.txtSales.Tag != null)
            {
                salesids = this.txtSales.Tag as List<Guid>;
            }

            List<Guid> companys = new List<Guid>();

            if (this.trCompany.EditValue != null)
            {
                companys = this.trCompany.CompanyIDs;
            }

            DateTime from;
            DateTime to;
            if (rdoDefault.Checked)
            {
                DateWeekly date = GetSeletctWeekly();
                from = date.StartDate;
                to = date.EndDate;
            }
            else
            {
                from = dateFrom.DateTime.Date;
                to = dateTo.DateTime.Date;
            }

            List<ReportContainerVolumeForShipperLine> containervolumelist = new List<ReportContainerVolumeForShipperLine>();
            containervolumelist = ReportCenterService.GetContainerVolumeForShipperLine(from, to, customerIDList.ToArray(), shippinglines.ToArray(),salesids.ToArray(),companys.ToArray());

            if (containervolumelist == null)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "total search 0 data." : "总共查询到 0条数据.");
                containervolumelist = new List<ReportContainerVolumeForShipperLine>();
            }

            List<ReportParameter> paramList = new List<ReportParameter>();

            paramList.Add(new ReportParameter("ETDRange", from.ToString("yyyy-MM-dd") + " 至 " + to.ToString("yyyy-MM-dd")));
            paramList.Add(new ReportParameter("IsGroupByCustomer", (rdoGroup.SelectedIndex+1).ToString()));

            ReportData rd = new ReportData();
            rd.IsLocalReport = true;
            rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.ShipperLineContainerVolumeReport.rdlc";           
            List<ReportDataSource> ds = new List<ReportDataSource>();
            ds.Add(new ReportDataSource("ShipperLineContainerVolumeReport", containervolumelist));
            rd.Parameters = paramList;
            rd.DataSource = ds;
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
