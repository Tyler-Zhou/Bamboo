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

using LongWin.CommonData.ServiceInterface.DataObjects;

using Microsoft.Reporting.WinForms;

namespace LongWin.DataWarehouseReport.WinUI
{
    /// <summary>
    /// 操作工作量统计
    /// </summary>
    public class AgentForOperatorWorkitem : WorkItem
    {
        #region

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [Microsoft.Practices.CompositeUI.ServiceDependency]
        public Agilelabs.Framework.Service.IInitializeService _InitializeService { get; set; }

        ReportMainSpace reportMainWorkSpace;
        AgentForOperator_SearchPart  _SearchPart;
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
                info.Title = Utility.IsEnglish ? "Operator Agent Report" : "操作指定货报表";

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
                    this._SearchPart = this.Items.AddNew<AgentForOperator_SearchPart>();
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
            AgentForOperatorForReport afoData = _SearchPart.GetData();

            #region 
            XElement xeRoot = new XElement("Root");
            xeRoot.Add(new XElement("CustomerIDs", afoData.CustomerIDs));
            xeRoot.Add(new XElement("GoodsType", afoData.GoodsType));
            xeRoot.Add(new XElement("SalesIDs", afoData.SalesIDs));
            xeRoot.Add(new XElement("DateFrom", afoData.DateFrom.ToShortDateString()));
            xeRoot.Add(new XElement("DateTo", afoData.DateTo.ToShortDateString()));
            xeRoot.Add(new XElement("IsETD", afoData.isEtd.ToString()));
            xeRoot.Add(new XElement("UserId", _InitializeService.GetUserInfo().UserId));

            #endregion

            this._ReportView.ReportName = "Rpt_AgentForOperator";

            List<ReportParameter> paramList = new List<ReportParameter>();
            paramList.Add(new ReportParameter("Condition", xeRoot.ToString()));

            this._ReportView.ParamList = paramList;
            this._ReportView.DisplayData();
        }
    }
}
