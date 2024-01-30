using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIManagement;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.OceanExport.UI.BL
{
    public partial class DispatchFileLogShow : BasePart
    {
        public DispatchFileLogShow()
        {
            InitializeComponent();
        }

        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                //List<DispathLogData> datasours = OceanExportService.GetDispatchFileLogForUser(null,null,LocalData.UserInfo.LoginID);
                //gcMain.DataSource = datasours;
                //gvDetails.RefreshData();
                //DateStart.DateTime = DateTime.Now;
                //DateEnd.DateTime = DateTime.Now;
            }
        }

        private void setControlText()
        {
            if (LocalData.IsEnglish)
            {
                ckbIsTransTo.Text = "Dispatched";
                ckbState.Text = "Accepted";
                labDateStrat.Text = "DispatchDate";
                labTo.Text = "To";
                btnSearch.Text = "Search";
                btnAll.Text = "All";
                btnReDispatch.Text = "ReDispatch";
                colCreateDate.Caption = "DispatchDate";
                colIsTransTo.Caption = "Dispatched";
                colOperationNo.Caption = "BusinessNo";
                colState.Caption = "Accepted";
            }
            else
            {
                ckbIsTransTo.Text = "分发到港后";
                ckbState.Text = "已经签收";
                labDateStrat.Text = "分发时间";
                labTo.Text = "至";
                btnSearch.Text = "查询";
                btnAll.Text = "全部";
                btnReDispatch.Text = "重新分发";
                colCreateDate.Caption = "分发时间";
                colIsTransTo.Caption = "是否分发到港后";
                colOperationNo.Caption = "业务号";
                colState.Caption = "是否签收";
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //bool IsTransTo = ckbIsTransTo.Checked;
            //byte state = (byte)(ckbState.Checked ? 2 : 1);
            //List<DispathLogData> datasours = OceanExportService.GetDispatchFileLogForUser(IsTransTo, state, LocalData.UserInfo.LoginID);
            //gcMain.DataSource = datasours;
            //gvDetails.RefreshData();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            //List<DispathLogData> datasours = OceanExportService.GetDispatchFileLogForUser(null, null, LocalData.UserInfo.LoginID);
            //gcMain.DataSource = datasours;
            //gvDetails.RefreshData();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (gvDetails.FocusedRowHandle < 0)
            {
                string message = LocalData.IsEnglish ? "Please select a line of distribution records！" : "请先选择一行分发记录！";
                MessageBox.Show(message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //DispathLogData current = (DispathLogData)gvDetails.GetFocusedRow();

            //if (current.IsTransTo || (int)current.State == 2)
            //{
            //    string message = LocalData.IsEnglish ? "The current distribution record does not need to be reDispath！" : "当前分发记录不需要重新发送！";
            //    MessageBox.Show(message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}


        }
    }
}
