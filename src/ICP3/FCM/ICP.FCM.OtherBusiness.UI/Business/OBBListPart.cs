#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/3/14 星期三 19:18:18
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using DevExpress.XtraEditors;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.UI.Common.Parts;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.UI.Common;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI.Commands;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ICP.FCM.OtherBusiness.UI.Business
{
    /// <summary>
    /// 其他业务-列表
    /// </summary>
    public class OBBListPart:OBListPart
    {
        #region 字段 & 属性 & 事件
        /// <summary>
        /// 
        /// </summary>
        public override string CommandEditData
        {
            get { return OBBCommandConstants.Command_EditData; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override string CommandShowSearch
        {
            get { return OBBCommandConstants.Command_ShowSearch; }
        }
        #endregion

        #region 构造函数

        /// <summary>
        /// 其他业务-列表
        /// </summary>
        public OBBListPart()
        {
            
        }
        #endregion

        #region CommandHandler
        #region 新增
        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [CommandHandler(OBBCommandConstants.Command_AddData)]
        public void Command_AddData(object sender, EventArgs args)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                PartLoader.ShowEditPart<OBBaseEditPart>(Workitem, null, LocalData.IsEnglish ? "Add Business" : "新增业务信息", EditPartSaved);
            }
        }
        #endregion

        #region 复制
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OBBCommandConstants.Command_CopyData)]
        public void Command_CopyData(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            using (new CursorHelper(Cursors.WaitCursor))
            {

                CurrentRow.EditMode = EditMode.Copy;
                Workitem.State["AddType"] = AddType;
                PartLoader.ShowEditPart<OBBaseEditPart>(Workitem, CurrentRow, LocalData.IsEnglish ? "Edit Business" : "编辑业务信息", EditPartSaved,
                       OBBCommandConstants.Command_EditData + CurrentRow.ID.ToString());
            }
        }

        #endregion

        #region 编辑
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OBBCommandConstants.Command_EditData)]
        public void Command_EditData(object sender, EventArgs e)
        {

            if (CurrentRow == null || CurrentRow.IsNew) return;
            using (new CursorHelper(Cursors.WaitCursor))
            {
                CurrentRow.EditMode = EditMode.Edit;
                PartLoader.ShowEditPart<OBBaseEditPart>(Workitem, CurrentRow, LocalData.IsEnglish ? "Edit Business" : "编辑业务信息", EditPartSaved,
                        OBBCommandConstants.Command_EditData + CurrentRow.ID.ToString());
                //Workitem.State["AddType"] = addType;
                //if (addType == AddType.OtherBusiness)
                //{

                //}
                //else
                //{
                //    PartLoader.ShowEditPart<OBOrderBaseEditPart>(Workitem, CurrentRow, LocalData.IsEnglish ? "Edit Business" : "编辑业务信息", EditPartSaved,
                //       OBBCommandConstants.Command_EditData + CurrentRow.ID.ToString());
                //}
            }
        }

        #endregion

        #region 刷新
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OBBCommandConstants.Command_RefreshData)]
        public void Command_RefreshData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    List<OtherBusinessList> blList = DataSource as List<OtherBusinessList>;
                    if (blList == null || blList.Count == 0) return;//无数据则返回
                    List<Guid> ids = new List<Guid>();
                    List<Guid> companyids = new List<Guid>();
                    foreach (var item in blList)
                    {
                        ids.Add(item.ID);
                        companyids.Add(item.CompanyID);
                    }

                    List<OtherBusinessList> list = OtherBusinessService.GetOtherBusinessListById(ids.ToArray(), companyids.ToArray());
                    DataSource = list;
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
            }
        }

        #endregion

        #region 作废
        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OBBCommandConstants.Command_CancelData)]
        public void Command_CancelData(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            try
            {

                bool isCancel = CurrentRow.IsValid;

                string message = string.Empty;
                if (isCancel)
                    message = LocalData.IsEnglish ? "Srue Cancel Current Booking?" : "你真的要取消这笔业务吗?";
                else
                    message = LocalData.IsEnglish ? "Srue Available Current Booking?" : "你真的要恢复这笔业务吗?";


                DialogResult dialogResult = XtraMessageBox.Show(message,
                                                    LocalData.IsEnglish ? "Tip" : "提示",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    SingleResult result = OtherBusinessService.CancelOtherBusiness(CurrentRow.ID, CurrentRow.CompanyID, isCancel, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);

                    OtherBusinessList currentRow = CurrentRow;
                    currentRow.IsValid = !isCancel;
                    currentRow.ID = result.GetValue<Guid>("ID");
                    currentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    bsOBList.ResetCurrentItem();

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Changed  State Successfully" : "更改状态成功");
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Changed  State Failed" : "更改状态失败") + ex.Message);
            }
        }
        #endregion

        #region 账单
        /// <summary>
        /// 账单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OBBCommandConstants.Command_Bill)]
        public void Command_Bill(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OperationCommonInfo operationCommonInfo = FCMCommonService.GetOperationCommonInfo(CurrentRow.ID, OperationType.Other);
                if (operationCommonInfo != null)
                {
                    FinanceClientService.ShowBillList(operationCommonInfo, ClientConstants.MainWorkspace);
                }
                else
                {
                    MessageBoxService.ShowInfo(LocalData.IsEnglish ? @"No found" : @"无对应的账单");
                }
            }
        }
        #endregion

        #region 核销单
        /// <summary>
        /// 核销单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        [CommandHandler(OBBCommandConstants.Command_VerifiSheet)]
        public void Command_VerifiSheet(object sender, EventArgs e)
        {

            if (CurrentRow == null || CurrentRow.IsNew)
            {
                return;
            }
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string no = CurrentRow.NO.Length <= 4 ? CurrentRow.NO : CurrentRow.NO.Substring(CurrentRow.NO.Length - 4, 4);
                string title = LocalData.IsEnglish ? "Verifi.Sheet" : "核销单" + ("-" + no);
                object[] data = new object[2];
                data[0] = CurrentRow.ID;
                data[1] = CurrentRow.NO;
                PartLoader.ShowEditPart<VerifiSheetEditPart>(Workitem,
                    data,
                    null,
                    title,
                    null,
                    OBBCommandConstants.Command_VerifiSheet + CurrentRow.ID.ToString());
            }
        }

        #endregion

        #region 提货通知书
        /// <summary>
        /// 提货通知书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OBBCommandConstants.Command_PickUp)]
        public void Command_PickUp(object sender, EventArgs e)
        {

            if (CurrentRow == null)
            {
                return;
            }
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string title = LocalData.IsEnglish ? "ReleaseNotify" : "提货通知书";
                ExportUIHelper.ShowTruckEdit(CurrentRow.ID, CurrentRow,
                        Workitem, OtherBusinessService,
                       Utility.GetLineNo(CurrentRow));
            }
        }
        #endregion

        #region 打印
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OBBCommandConstants.Command_OpContact)]
        public void Command_PrintOrder(object sender, EventArgs e)
        {

            if (CurrentRow == null) return;
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OtherBusinessPrintHelper.PrintOBOrder(CurrentRow.ID, CurrentRow.CompanyID);

            }
        }

        #region 利润表
        /// <summary>
        /// 利润表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(OBBCommandConstants.Command_Profit)]
        public void Command_Profit(object sender, EventArgs e)
        {

            if (CurrentRow == null) return;
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OtherBusinessPrintHelper.PrintOEBookingProfit(CurrentRow);
            }
        }
        #endregion

        #endregion 
        #endregion

        #region 方法
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prams"></param>
        void EditPartSaved(object[] prams)
        {
            if (prams == null || prams.Length == 0) return;

            OtherBusinessInfo data = prams[0] as OtherBusinessInfo;
            List<OtherBusinessList> source = DataSource as List<OtherBusinessList>;
            if (source == null || source.Count == 0)
            {
                bsOBList.Add(data);
                bsOBList.ResetBindings(false);
            }
            else
            {
                OtherBusinessList tager = source.Find(item => item.ID == data.ID);
                if (tager == null)
                {
                    bsOBList.Insert(0, data);
                    bsOBList.ResetBindings(false);
                }
                else
                {
                    Utility.CopyToValue(data, tager, typeof(OtherBusinessList));
                    bsOBList.ResetItem(bsOBList.IndexOf(tager));
                }

            }
            GridCurrentChanged(this, CurrentRow);


            SetColumnsWidth();
        }
        void SetColumnsWidth()
        {
            gvMain.BestFitColumns();
        }
        #endregion
    }
}
