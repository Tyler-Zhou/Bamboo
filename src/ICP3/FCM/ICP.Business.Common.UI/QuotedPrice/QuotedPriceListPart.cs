using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.MailCenter.ServiceInterface;
using ICP.MailCenter.UI;
using ICP.Message.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.Client;
using ICP.Common.ServiceInterface.DataObjects;
using Utility = ICP.Framework.ClientComponents.Controls.Utility;
using EnumDocumentType = ICP.FileSystem.ServiceInterface.DocumentType;

namespace ICP.Business.Common.UI.QuotedPrice
{
    /// <summary>
    /// 报价列表
    /// </summary>
    [ToolboxItem(false)]
    public partial class QuotedPriceListPart : BaseListPart
    {
        #region Fields & Service & Property & Delegate
        #region Fields
        #endregion

        #region Service
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem RootWorkItem { get; set; }
        /// <summary>
        /// FCM公用服务
        /// </summary>
        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        /// <summary>
        /// 文件转换服务
        /// </summary>
        public IFileConvertService FileConvertService
        {
            get
            {
                return ServiceClient.GetClientService<IFileConvertService>();
            }
        }

       /// <summary>
       /// 邮件模板服务
       /// </summary>
        public IMailCenterTemplateService MailCenterTemplateService
        {
            get
            {
                ClientHelper.EnsureEmailCenterAppStarted();

                return new MailCenterTemplateService();
            }
        }
        /// <summary>
        /// 报表服务
        /// </summary>
        public IReportViewService ReportViewService
        {
            get { return ServiceClient.GetClientService<IReportViewService>(); }
        }
        /// <summary>
        /// 客户服务
        /// </summary>
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }
        #endregion

        #region Property
        /// <summary>
        /// 当前行
        /// </summary>
        public override object Current
        {
            get { return bsList.Current; }
        }
        /// <summary>
        /// 报价选择行
        /// </summary>
        protected QuotedPriceOrderList CurrentRow
        {
            get { return Current as QuotedPriceOrderList; }
        }
        /// <summary>
        /// 
        /// </summary>
        public EditPartShowCriteria ShowCriteria
        {
            get
            {
                return new EditPartShowCriteria
                {
                    OperationID = CurrentRow.ID,
                    OperationNo = CurrentRow.No,
                    BillNo = CurrentRow.ID
                };
            }
        }
        /// <summary>
        /// List DataSource
        /// </summary>
        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                bsList.DataSource = value;
                bsList.ResetBindings(false);
                if (CurrentChanged != null) CurrentChanged(this, Current);
            }
        }
        #endregion

        #region Delegate
        /// <summary>
        /// 行选项改变
        /// </summary>
        public override event CurrentChangedHandler CurrentChanged;
        /// <summary>
        /// 行改变前
        /// </summary>
        public override event CancelEventHandler CurrentChanging; 
        #endregion
        #endregion

        #region Init
        /// <summary>
        /// 报价列表
        /// </summary>
        public QuotedPriceListPart()
        {
            InitializeComponent();
            InitMessage();
            RegisterEvent();
            Disposed += (sender, arg) =>
            {
                UnRegisterEvent();
                bsList.Clear();
            };
        }
        
        #endregion

        #region Control Event

        /// <summary>
        /// 行选项改变
        /// </summary>
        void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) 
                CurrentChanged(this, Current);
        }
        /// <summary>
        /// 行样式设置
        /// </summary>
        void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            QuotedPriceOrderList list = gvMain.GetRow(e.RowHandle) as QuotedPriceOrderList;
            if (list == null) return;

            if (list.IsValid == false)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Disabled);
            }
            else if (string.IsNullOrEmpty(list.ConfirmedName))
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.NewLine);
            }
        }
        /// <summary>
        /// 换行前
        /// </summary>
        void gvMain_BeforeLeaveRow(object sender, RowAllowEventArgs e)
        {
            if (CurrentChanging != null)
            {
                CancelEventArgs ce = new CancelEventArgs();
                CurrentChanging(this, ce);
                e.Allow = !ce.Cancel;
            }
        }
        /// <summary>
        /// 双击编辑
        /// </summary>
        private void gvMain_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && CurrentRow != null)
                RootWorkItem.Commands[QPCommandConstants.Command_EditData].Execute();
        }
        /// <summary>
        /// 绘制行号
        /// </summary>
        private void gvMain_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }

        #endregion

        #region Workitem Command
        /// <summary>
        /// 添加报价
        /// </summary>
        [CommandHandler(QPCommandConstants.Command_AddData)]
        public void Command_AddData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                var title = LocalData.IsEnglish ? "New QP Orders" : "新增报价单";
                PartLoader.ShowEditPart<QuotedPriceEditPart>(RootWorkItem, null, EditMode.New, null, title, EditPartSaved, title);
            }
        }

        /// <summary>
        /// 编辑报价
        /// </summary>
        [CommandHandler(QPCommandConstants.Command_EditData)]
        public void Command_EditData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {

                string title = ((LocalData.IsEnglish) ? "Edit QP Order" : "编辑报价单") + CommonUIUtility.GetSerialNumber(ShowCriteria.OperationNo);
                PartLoader.ShowEditPart<QuotedPriceEditPart>(RootWorkItem, ShowCriteria, EditMode.Edit, null, title, EditPartSaved, ShowCriteria.OperationID.ToString());
            }
        }

        /// <summary>
        /// 删除报价
        /// </summary>
        [CommandHandler(QPCommandConstants.Command_DeleteData)]
        public void Command_DeleteData(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;
            if (Utility.GuidIsNullOrEmpty(CurrentRow.ID))
                return;
            try
            {
                //是否有效
                if (!CurrentRow.IsValid)
                {
                    MessageBoxService.ShowWarning(NativeLanguageService.GetText(this, "QPOrderUnValid"));
                    return;
                }
                //提示是否删除
                if (PartLoader.EnquireIsDeleteCurrentData() == false) return;
                SingleResult result = FCMCommonService.RemoveQuotedPriceOrderInfo(CurrentRow.ID, !CurrentRow.IsValid, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
                //更新当前数据
                QuotedPriceOrderList currentRow = CurrentRow;
                currentRow.IsValid = !currentRow.IsValid;
                currentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                bsList.ResetCurrentItem();

                if (CurrentChanged != null) CurrentChanged(this, Current);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "DeleteSuccessfully"));
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
        }

        /// <summary>
        /// 复制报价单
        /// </summary>
        [CommandHandler(QPCommandConstants.Command_CopyData)]
        public void Command_CopyData(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;
            if (Utility.GuidIsNullOrEmpty(CurrentRow.ID))
                return;
            try
            {
                string title = ((LocalData.IsEnglish) ? "Copy QP Order" : "复制报价单");
                PartLoader.ShowEditPart<QuotedPriceEditPart>(RootWorkItem, ShowCriteria, EditMode.Copy, null, title, EditPartSaved,ShowCriteria.OperationID.ToString());
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
        }

        /// <summary>
        /// 打印报价单
        /// </summary>
        [CommandHandler(QPCommandConstants.Command_PrintPrice)]
        public void Command_PrintPrice(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;
            if (Utility.GuidIsNullOrEmpty(CurrentRow.ID))
                return;
            try
            {
                //是否有效
                if (!CurrentRow.IsValid)
                {
                    MessageBoxService.ShowWarning(NativeLanguageService.GetText(this, "QPOrderUnValid"));
                    return;
                }
                string title = (LocalData.IsEnglish ? "Print Quoted Price Order" : "打印报价单") + ("-" + CurrentRow.No);
                QPOrderReportData reportData = FCMCommonService.GetQPOrderReportData(CurrentRow.ID);
                Dictionary<string, object> reportSource = new Dictionary<string, object>
                {
                    {"ReportSource", reportData},
                    {"RatesListSource", reportData.RatesList},
                    {"SurChargesSource", reportData.SurCharges},
                    {CommonConstants.DocumentTypeKey,EnumDocumentType.QuotedPrice},
                    {CommonConstants.DocumentNameKey, CurrentRow.No}
                };
                CustomerInfo _customerInfo = CustomerService.GetCustomerInfo(CurrentRow.CustomerID.Value);
                if (_customerInfo != null && !string.IsNullOrEmpty(_customerInfo.Fax))
                {
                    reportSource.Add(CommonConstants.CustomerFaxAddressKey, _customerInfo.Fax);
                }
                if (_customerInfo != null && !string.IsNullOrEmpty(_customerInfo.EMail))
                {
                    reportSource.Add(CommonConstants.CustomerEmailAddressKey, _customerInfo.EMail);
                }
                string fileName=Application.StartupPath + "\\Reports\\QuotedPrice\\";
                fileName += "QP_Order.frx";
                IReportViewer viewer = ReportViewService.ShowReportViewer(title, RootWorkItem.Workspaces[ClientConstants.MainWorkspace]);
                Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message
                {
                    UserProperties = new MessageUserPropertiesObject
                    {
                        OperationType = OperationType.QuotedPrice,
                        OperationId = CurrentRow.ID,
                        FormType = FormType.QuotedPrice,
                        FormId = CurrentRow.ID
                    }
                };

                viewer.BindData(fileName, reportSource, null, message);
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
        }

        /// <summary>
        /// 刷新报价单列表
        /// </summary>
        [CommandHandler(QPCommandConstants.Command_RefreshData)]
        public void Command_RefreshData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    List<QuotedPriceOrderList> list = DataSource as List<QuotedPriceOrderList>;
                    if (list == null || list.Count == 0) return;

                    List<Guid> ids = (from item in list where !item.ID.IsNullOrEmpty() select item.ID).ToList();

                    List<QuotedPriceOrderList> newList = FCMCommonService.GetQuotedPriceListByIds(ids.ToArray());
                    DataSource = newList;
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
            }
        }
        #endregion

        #region Custom Method
        /// <summary>
        /// 初始化提示信息
        /// </summary>
        private void InitMessage()
        {
            RegisterMessage("QPOrderUnValid", LocalData.IsEnglish ? "Quoted Price Un Valid" : "报价单无效,禁止操作!");
            RegisterMessage("QPOrderSure", LocalData.IsEnglish ? "Sure Quoted Price" : "是否确认报价?");
            RegisterMessage("QPOrderUnSure", LocalData.IsEnglish ? "Cancel Sure Quoted Price" : "是否取消确认报价?");
            RegisterMessage("SureSuccessfully", LocalData.IsEnglish ? "Sure Quoted Price Successfully" : "报价单确认成功!");
            RegisterMessage("UnSureSuccessfully", LocalData.IsEnglish ? "UnSure Quoted Price Successfully" : "报价单取消确认成功!");
            
        }
        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvent()
        {
            bsList.PositionChanged += bsList_PositionChanged;
            gvMain.BeforeLeaveRow+=gvMain_BeforeLeaveRow;
            gvMain.RowStyle += gvMain_RowStyle;
            gvMain.RowCellClick += gvMain_RowCellClick;
            gvMain.CustomDrawRowIndicator += gvMain_CustomDrawRowIndicator;
        }
        /// <summary>
        /// 移除事件
        /// </summary>
        private void UnRegisterEvent()
        {
            bsList.PositionChanged -= bsList_PositionChanged;
            gvMain.BeforeLeaveRow -= gvMain_BeforeLeaveRow;
            gvMain.RowStyle -= gvMain_RowStyle;
            gvMain.RowCellClick -= gvMain_RowCellClick;
            gvMain.CustomDrawRowIndicator -= gvMain_CustomDrawRowIndicator;
        }
        /// <summary>
        /// 设置列宽度
        /// </summary>
        void SetColumnsWidth()
        {
            gvMain.BestFitColumns();
        }
        /// <summary>
        /// 编辑面板保存后
        /// </summary>
        /// <param name="prams"></param>
        private void EditPartSaved(object[] prams)
        {
            if (this.IsDisposed || this.Parent.IsDisposed)
                return;
            if (prams == null || prams.Length == 0) return;

            QuotedPriceOrderList data = prams[0] as QuotedPriceOrderList;
            List<QuotedPriceOrderList> source = this.DataSource as List<QuotedPriceOrderList>;
            if (source == null || source.Count == 0)
            {
                bsList.Add(data);
                bsList.ResetBindings(false);
            }
            else
            {
                QuotedPriceOrderList tager = source.Find(delegate(QuotedPriceOrderList item) { return item.ID == data.ID; });
                if (tager == null)
                {
                    bsList.Insert(0, data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    Utility.CopyToValue(data, tager, typeof(QuotedPriceOrderList));
                    bsList.ResetItem(bsList.IndexOf(tager));
                }

            }
            if (CurrentChanged != null) CurrentChanged(this, CurrentRow);


            SetColumnsWidth();
        }
        #endregion
    }
}
