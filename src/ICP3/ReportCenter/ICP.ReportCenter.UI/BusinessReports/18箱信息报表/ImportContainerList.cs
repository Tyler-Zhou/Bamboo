using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.ReportCenter.ServiceInterface;
using ICP.ReportCenter.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;

namespace ICP.ReportCenter.UI.BusinessReports
{
    public partial class ImportContainerList : ReportBaseSearchPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        IReportCenterService ReportCenterHelper
        {
            get
            {
                return ServiceClient.GetService<IReportCenterService>();
            }
        }

        #endregion
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;
        public ImportContainerList()
        {
            InitializeComponent();
            InitControls();
            Disposed += delegate
            {
                OnSearched = null;
                ccbVesselIDs.Enter -= ccbVesselIDs_Enter;
                ccbVoyageIDs.Enter -= ccbVoyageIDs_Enter;
                ccbContainerTypeIDs.Enter -= ccbContainerTypeIDs_Enter;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        

        private void InitControls()
        {
            ccbVesselIDs.Enter += ccbVesselIDs_Enter;
            ccbVoyageIDs.Enter += ccbVoyageIDs_Enter;
            ccbContainerTypeIDs.Enter += ccbContainerTypeIDs_Enter;
        }

        

        private void ccbVesselIDs_Enter(object sender, EventArgs e)
        {
            try
            {
                List<VesselList> dataList = TransportFoundationService.GetRecentVesselList(DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1));
                ccbVesselIDs.BeginUpdate();
                ccbVesselIDs.ClearItems();

                foreach (VesselList item in dataList)
                {
                    ccbVesselIDs.AddItem(item.ID, item.Name);
                }
                ccbVesselIDs.EndUpdate();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }

        private void ccbVoyageIDs_Enter(object sender, EventArgs e)
        {
            try
            {
                List<VoyageList> dataList = TransportFoundationService.GetRecentVoyageList(DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1));
                ccbVoyageIDs.BeginUpdate();
                ccbVoyageIDs.ClearItems();

                foreach (VoyageList item in dataList)
                {
                    ccbVoyageIDs.AddItem(item.ID, item.No);
                }
                ccbVoyageIDs.EndUpdate();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        private void ccbContainerTypeIDs_Enter(object sender, EventArgs e)
        {
            try
            {
                List<ContainerList> dataList = TransportFoundationService.GetContainerList(string.Empty,true, 0);
                ccbContainerTypeIDs.BeginUpdate();
                ccbContainerTypeIDs.ClearItems();

                foreach (ContainerList item in dataList)
                {
                    ccbContainerTypeIDs.AddItem(item.ID, item.Code);
                }
                ccbContainerTypeIDs.EndUpdate();
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

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

        #region ISearchPart 成员

        public override object GetData()
        {
            List<Guid> companyArr = treeBoxSalesDep.EditValue;
            List<Guid> cusList = txtCustomer.Tag as List<Guid>;
            string companyIDs;
            string freightLocationIDs;
            companyIDs = companyArr == null ? "" : companyArr.ToArray().Join();
            freightLocationIDs = cusList == null ? "" : cusList.ToArray().Join();


            //OIContainerReportData reportdata = ReportCenterHelper.GetOIContaierList(CompanyIDs , FreightLocationIDs , DatePart.FromDate , DatePart.ToDate);

            QueryCriteria4OIContainerVolumeTotal queryParameter = new QueryCriteria4OIContainerVolumeTotal
            {
                CompanyIDs = companyIDs,
                FreightLocationIDs = freightLocationIDs,
                VesselIDs = ccbVesselIDs.SelectValuesToGuid.Join(),
                VoyageIDs = ccbVoyageIDs.SelectValuesToGuid.Join(),
                ContainerTypeIDs = ccbContainerTypeIDs.SelectValuesToGuid.Join(),
                BeginTime = DatePart.FromDate,
                EndTime = DatePart.ToDate,
            };
            OIContainerReportData reportdata = ReportCenterHelper.GetOIContainerVolumeTotal(queryParameter);

            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("FromDate", DatePart.FromDate.ToShortDateString()));
            paramList.Add(new ReportParameter("ClosingDate", DatePart.ToDate.ToShortDateString()));
            paramList.Add(new ReportParameter("CompanyName", treeBoxSalesDep.EditText));
            paramList.Add(new ReportParameter("IsEnglish", LocalData.IsEnglish.ToString()));

            ReportData rd = new ReportData();
            rd.IsLocalReport = true;
            rd.ReportName = "ICP.ReportCenter.UI.Comm.LocalReports.ImportContainerList.rdlc";
            rd.Parameters = paramList;
            List<ReportDataSource> ds = new List<ReportDataSource>();
            ds.Add(new ReportDataSource("GLDataReport", reportdata.OIContaierList));
            ds.Add(new ReportDataSource("GLDataCountReport", reportdata.OIContainerTypeCount));
            rd.DataSource = ds;
            return rd;
        }

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion

    }
}
