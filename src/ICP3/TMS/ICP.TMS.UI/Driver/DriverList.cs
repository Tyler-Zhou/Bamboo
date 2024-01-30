using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.Service;
using ICP.TMS.ServiceInterface;

namespace ICP.TMS.UI
{
    /// <summary>
    /// 司机资料面板
    /// </summary>
    [ToolboxItem(false)]
    public partial class DriverList : BaseListPart
    {
        public DriverList()
        {
            InitializeComponent();
            this.Disposed += delegate {
                
                this.CurrentChanged = null;
                this.CurrentChanging = null;
                this.gcMain.DataSource = null;
                this.bsList.PositionChanged -= this.bsList_PositionChanged;
                this.gvMain.BeforeLeaveRow -= this.gvMain_BeforeLeaveRow;
                this.gvMain.KeyDown -= this.gvMain_KeyDown;
                this.gvMain.RowStyle -= this.gvMain_RowStyle;
                this.bsList.DataSource = null;
                this.bsList.Dispose();

                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            
            };
        }

        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 拖车服务
        /// </summary>
        public ITruckBookingService TruckBookingService
        {
            get
            {
                return ServiceClient.GetService<ITruckBookingService>();
            }
        }
        #endregion

        #region 私有字段
        /// <summary>
        /// 当前行数据
        /// </summary>
        private DriversDataList CurrentRow
        {
            get
            {
                return bsList.Current as DriversDataList;
            }
        }
        /// <summary>
        /// 数据列表
        /// </summary>
        private List<DriversDataList> DataList
        {
            get
            {
                return bsList.DataSource as List<DriversDataList>;
            }
        }
        #endregion

        #region 重写
        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                gvMain.BeginUpdate();

                List<DriversDataList> list = value as List<DriversDataList>;

                bsList.DataSource = list;

                bsList.ResetBindings(false);
                if (CurrentChanged != null)
                {
                    CurrentChanged(this, CurrentRow);
                }
                gvMain.BestFitColumns();
                gvMain.EndUpdate();

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
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), message);
            }
        }
        /// <summary>
        /// 行改变后
        /// </summary>
        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;
        /// <summary>
        /// 行改变时
        /// </summary>
        public override event CancelEventHandler CurrentChanging;
        public override void RemoveItem(int index)
        {
            bsList.RemoveAt(index);
        }

        public override void RemoveItem(object item)
        {
            bsList.Remove(item);
        }
        #endregion

        #region 初始化
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitMeesage();
                InitControls();
            }
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            Utility.ShowGridRowNo(this.gvMain);
        }
        /// <summary>
        /// 初始化消息内容
        /// </summary>
        private void InitMeesage()
        {
            this.RegisterMessage("1108190001", "确认要作废该数据");
            this.RegisterMessage("1108190002", "确认要激活该数据");
        }
        #endregion

        #region 按钮事件
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(DriverCommondConstants.Commond_Add)]
        public void Commond_Add(object sender, EventArgs e)
        {
            DriversDataList data = new DriversDataList();
            data.CreateBy = LocalData.UserInfo.LoginID;
            data.CreateByName = LocalData.UserInfo.LoginName;
            data.IsValid = true;
            data.ID = Guid.NewGuid();

            data.CreateDate = DateTime.Now;

            if (DataList == null || DataList.Count == 0)
            {
                this.bsList.DataSource = new List<DriversDataList>();
            }
            this.bsList.Add(data);

            this.gvMain.MoveLast();
        }
        /// <summary>
        /// 作废数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(DriverCommondConstants.Commond_Delete)]
        public void Command_Delete(object sender, EventArgs e)
        {
            if (this.CurrentRow != null)
            {
                string failureMessage = string.Empty;
                if (this.CurrentRow.IsValid)
                    failureMessage = LocalData.IsEnglish ? "Cancel Driver  Failed." : "作废司机资料失败.";
                else
                    failureMessage = LocalData.IsEnglish ? "Available Driver  Failed." : "激活司机资料失败.";

                try
                {
                    if (CurrentRow.IsValid)
                    {
                        if (!Utility.ShowResultMessage(NativeLanguageService.GetText(this, "1108190001")))
                        {
                            return;
                        }

                    }
                    else
                    {
                        if (!Utility.ShowResultMessage(NativeLanguageService.GetText(this, "1108190002")))
                        {
                            return;
                        }
                    }

                    SingleResult result = this.TruckBookingService.CancelDriver(this.CurrentRow.ID,
                        this.CurrentRow.IsValid,
                        LocalData.UserInfo.LoginID,
                        this.CurrentRow.UpdateDate,
                        LocalData.IsEnglish);

                    CurrentRow.IsValid = !CurrentRow.IsValid;
                    CurrentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                    bsList.ResetCurrentItem();

                    if (this.CurrentChanged != null)
                    {
                        CurrentChanged(this, CurrentRow);
                    }

                    if (!this.CurrentRow.IsValid)
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Cancel Driver Successfully." : "司机资料已经成功作废.");
                    else
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Available Driver Successfully." : "司机资料已经成功激活.");

                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), failureMessage + ex.Message);
                }
            }
        }
        #endregion

        #region Grid事件
        /// <summary>
        /// 选择行改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null)
            {
                CurrentChanged(this, CurrentRow);
            }
        }
        /// <summary>
        /// 设置行样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            DriversDataList list = gvMain.GetRow(e.RowHandle) as DriversDataList;
            if (list == null) return;

            if (list.IsValid == false)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
            }
        }
        /// <summary>
        /// 行改变时，验证数据是否已保存过
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            if (CurrentChanging != null)
            {
                CancelEventArgs cancel = new CancelEventArgs();
                this.CurrentChanging(sender, cancel);

                if (cancel.Cancel)
                {
                    e.Allow = false;
                }
            }
        }
        /// <summary>
        /// 热键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6 && CurrentRow != null)
            {
                Workitem.Commands[DriverCommondConstants.Command_ShowSearch].Execute();
            }
        }

        #endregion

        #region 保存后更新数据
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="prams"></param>
        public void RefreshData(object[] items)
        {
            if (items == null || items.Length == 0)
            {
                return;
            }

            DriversDataList data = items[0] as DriversDataList;
            if (data == null)
            {
                return;
            }

            if (DataList == null || DataList.Count == 0)
            {
                bsList.DataSource = new List<DriversDataList>();

                bsList.Insert(0, data);
                bsList.ResetBindings(false);
            }
            else
            {
                DriversDataList tager = DataList.Find(delegate(DriversDataList item) { return item.ID == data.ID; });
                if (tager == null)
                {
                    bsList.Insert(0, data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    Utility.CopyToValue(data, tager, typeof(DriversDataList));
                }
            }


            bsList.ResetBindings(false);

            if (CurrentChanged != null)
            {
                CurrentChanged(this, CurrentRow);
            }
        }
        #endregion





    }
}
