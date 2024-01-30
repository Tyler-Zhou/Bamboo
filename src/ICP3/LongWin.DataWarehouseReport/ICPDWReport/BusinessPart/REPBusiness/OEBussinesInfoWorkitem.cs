using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.WinForms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using LongWin.DataWarehouseReport.WinUI.BusinessPart.REPBusiness.SearchPart;
using System.Xml.Linq;
using Microsoft.Reporting.WinForms;

using LongWin.CommonData.ServiceInterface;
using LongWin.CommonData.ServiceInterface.DataObjects;
using LongWin.Framework.Client;
using Agilelabs.Framework.Service;

namespace LongWin.DataWarehouseReport.WinUI
{
    public class OEBussinesInfoWorkitem : WorkItem
    {
        #region

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IInitializeService _InitializeService { get; set; }

        [ServiceDependency]
        public IDataFindClientService _dataFinder { get; set; }

        [ServiceDependency]
        public IDataFinderFactory _finderFactory { get; set; }

        [ServiceDependency]
        public IDataFindClientService dfcService { get; set; }

        ReportMainSpace reportMainWorkSpace;
        OEBussinesInfo_SearchPart _SearchPart;
        ReportViewBase _ReportView;

        #endregion

        #region

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            this.Show();
        }

        internal void Show()
        {

            IWorkspace workspace = this.Workitem.Workspaces["MainWorkspace"];

            if (this.reportMainWorkSpace == null)
            {
                this.reportMainWorkSpace = this.Items.AddNew<ReportMainSpace>((new Guid()).ToString());
                this.reportMainWorkSpace.Disposed += new EventHandler(reportMainWorkSpace_Disposed);
                this.reportMainWorkSpace.Dock = DockStyle.Fill;
                SmartPartInfo info = new SmartPartInfo();
                info.Title = Utility.IsEnglish ? "OEBussinesInfo Report" : "出口业务信息报表";

                workspace.Show(this.reportMainWorkSpace, info);
                //内容显示面板
                if (this._ReportView == null)
                {
                    this._ReportView = this.Items.AddNew<ReportViewBase>();
                    this._ReportView.Dock = DockStyle.Fill;
                }
                this.reportMainWorkSpace.ListSpace.Show(this._ReportView);

                //查询面板
                if (this._SearchPart == null)
                {
                    this._SearchPart = new OEBussinesInfo_SearchPart(this, _dataFinder, _finderFactory, dfcService);
                    this.SmartParts.Add(_SearchPart);
                    this._SearchPart.Dock = DockStyle.Fill;

                }
                this.reportMainWorkSpace.SearchSpace.Show(this._SearchPart);

            }
            else { workspace.Activate(this.reportMainWorkSpace); }
        }

        void reportMainWorkSpace_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        #endregion

        [CommandHandler("Search")]
        public void Search(object sender, EventArgs e)
        {
            OEBussinesInfoForReport oebiData = _SearchPart.GetData();

            #region
            XElement xeRoot = new XElement("Root");
            xeRoot.Add(new XElement("CustomerIDs", oebiData.CustomerIDs));
            xeRoot.Add(new XElement("SalesID", oebiData.SalesID));
            xeRoot.Add(new XElement("DateFrom", oebiData.DateFrom.ToShortDateString()));
            xeRoot.Add(new XElement("DateTo", oebiData.DateTo.ToShortDateString()));
            xeRoot.Add(new XElement("GroupBy", oebiData.GroupByType));

            #endregion

            this._ReportView.ReportName = "RPT_GetOEHblListForOI";

            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("Condition", xeRoot.ToString()));

            this._ReportView.ParamList = paramList;
            this._ReportView.DisplayData();
        }
    }
}
