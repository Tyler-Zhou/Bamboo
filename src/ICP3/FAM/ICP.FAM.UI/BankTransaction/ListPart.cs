using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.Controls;
using ICP.FAM.UI.WriteOff;
using ICP.FAM.ServiceInterface.CompositeObjects;
using ICP.Common.ServiceInterface.Client;
using System.Drawing;

namespace ICP.FAM.UI.BankTransaction
{
    /// <summary>
    /// 
    /// </summary>
    [ToolboxItem(false)]
    public partial class ListPart : BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 
        /// </summary>
        IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        /// <summary>
        /// 报表服务
        /// </summary>
        public IReportViewService ReportViewService
        {
            get { return ServiceClient.GetClientService<IReportViewService>(); }
        }
        #endregion

        #region 成员
        /// <summary>
        /// 
        /// </summary>
        public override event CurrentChangedHandler CurrentChanged;
        /// <summary>
        /// 
        /// </summary>
        public override event CancelEventHandler CurrentChanging;
        #endregion

        #region 初始化

        public ListPart()
        {
            InitializeComponent();
            Disposed += delegate {
                CurrentChanged = null;
                CurrentChanging = null;
                KeyDown = null;
                gcMain.DataSource = null;
                bsList.PositionChanged -= bsList_PositionChanged;
                bsList.DataSource = null;
                bsList.Dispose();
                
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
                InitMeesage();
            }

        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            FAMUtility.ShowGridRowNo(gvMain);
        }

        /// <summary>
        /// 初始化消息内容
        /// </summary>
        private void InitMeesage()
        {
        }
        #endregion

        #region event

        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null)
            {
                CurrentChanged(this, Current);
            }
        }

        private void gvMain_BeforeLeaveRow(object sender, RowAllowEventArgs e)
        {

            if (CurrentChanging != null)
            {
                CancelEventArgs cancel = new CancelEventArgs();
                CurrentChanging(sender, cancel);

                if (cancel.Cancel)
                {
                    e.Allow = false;
                }
            }
        }

        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            BankTransactionInfo list = gvMain.GetRow(e.RowHandle) as BankTransactionInfo;
            if (list == null) return;
            if(!list.BusinessNO.IsNullOrEmpty())
            {
                e.Appearance.ForeColor = Color.Blue;
            }
        }
        #endregion

        #region 命令
        /// <summary>
        /// 销账
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BankTransactionConstants.Commond_WriteOff)]
        public void Commond_WriteOff(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow != null)
                {
                    try
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        
                        dic.Add("WriteOffType", WriteOffType.Single);
                        dic.Add("CompanyID", CurrentRow.CompanyID);
                        dic.Add("PayCurrencyID", "DEB5F402-B6C0-4491-B247-B75C3EDA7976");
                        dic.Add("BankTransactionInfo", CurrentRow);
                        string title = string.Empty;
                        if (CurrentRow.DebitCreditFlag == "C")
                        {
                            title = LocalData.IsEnglish ? "Receivables" : "收账";
                            dic.Add("FeeWay", FeeWay.AR);
                        }
                        else
                        {
                            title = LocalData.IsEnglish ? "Payment" : "付账";
                            dic.Add("FeeWay", FeeWay.AP);
                        }
                        PartLoader.ShowEditPart<WriteOffEditPart>(Workitem.Items.AddNew<WorkItem>(), null, dic, title, null, string.Empty);
                    }
                    catch (Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                    }
                }
            }
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="items"></param>
        public override void Refresh(object items)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
            }
        }
        /// <summary>
        /// 打印
        /// </summary>
        [CommandHandler(BankTransactionConstants.Command_Print)]
        public void Commond_Print(object sender, EventArgs e)
        {
            if (CurrentRow != null)
            {
                try
                {
                    string title = (LocalData.IsEnglish ? "Print Bank Transaction" : "打印银行流水");
                    List<BankTransactionReportData> reportData = FinanceService.ReportDataForBankTransaction(new BankTransactionReportSearchParameter() { BankTransactionIDs = new Guid[]{ CurrentRow.ID } });
                    Dictionary<string, object> reportSource = new Dictionary<string, object>
                {
                    {"ReportSource", reportData},
                };
                    string fileName = Application.StartupPath + "\\Reports\\FAM\\";
                    fileName += "BankTransaction.frx";
                    IReportViewer viewer = ReportViewService.ShowReportViewer(title,Workitem.RootWorkItem.Workspaces[ClientConstants.MainWorkspace]);
                    viewer.BindData(fileName, reportSource, null, null);
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                }
            }
        }
       
        [CommandHandler(BankTransactionConstants.Commond_View )]
        public void Commond_View(object sender, EventArgs e)
        {
        }

        


        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        protected BankTransactionInfo CurrentRow
        {
            get { return Current as BankTransactionInfo; }
        }

        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {

                List<BankTransactionInfo> list = value as List<BankTransactionInfo>;

                bsList.DataSource = list;

                bsList.ResetBindings(false);
                if (CurrentChanged != null)
                {
                    CurrentChanged(this, Current);
                }
                gvMain.BestFitColumns();


                string message = string.Empty;

                if (list.Count > 0)
                {
                    if (LocalData.IsEnglish)
                    {
                        message = string.Format("{0} record found", list.Count);
                    }
                    else
                    {
                        message = string.Format("查询到 {0} 条记录", list.Count);
                    }
                }
                else
                {
                    message = LocalData.IsEnglish ? "Nothing found!" : "没有查询到任何结果。";
                }

                if (list.Count.ToString().Length == 1)
                {
                    gvMain.IndicatorWidth = 30;
                }
                else
                {
                    gvMain.IndicatorWidth = list.Count.ToString().Length * 17;
                }
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), message);
            }
        }

        public override void RemoveItem(int index)
        {
            bsList.RemoveAt(index);
        }

        public override void RemoveItem(object item)
        {
            bsList.Remove(item);
        }

        #endregion

        #region 热键

        public new event KeyEventHandler KeyDown;

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {          
            if (KeyDown != null
                && e.KeyCode == Keys.F5
                && gvMain.FocusedColumn != null
                && gvMain.FocusedValue != null)
            {
                string text = gvMain.GetFocusedDisplayText();
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add(gvMain.FocusedColumn.FieldName, text);
                KeyDown(keyValue, null);
            }
            if (e.KeyCode == Keys.F6 && CurrentRow != null)
            {
                Workitem.Commands[BankTransactionConstants.Command_ShowSearch].Execute();
            }
        }

        #endregion

    }
}
