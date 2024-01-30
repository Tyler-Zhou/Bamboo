using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Office.Interop.Excel;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;
using ICP.FRM.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.ClientComponents.Controls;
using Application = Microsoft.Office.Interop.Excel.Application;
using DataTable = System.Data.DataTable;

namespace ICP.FRM.UI.OceanPrice
{
    /// <summary>
    /// 合约列表面板
    /// </summary>
    [ToolboxItem(false)]
    public partial class OPContractListPart : BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IOceanPriceService OceanPriceService
        {
            get
            {
                return ServiceClient.GetService<IOceanPriceService>();
            }
        }

        public OceanPriceUIDataHelper OceanPriceUIDataHelper
        {
            get
            {
                return ClientHelper.Get<OceanPriceUIDataHelper, OceanPriceUIDataHelper>();
            }
        }

        #endregion

        #region 属性
        /// <summary>
        /// 当前选择的行
        /// </summary>
        List<OceanList> SelectRows
        {
            get
            {
                int[] indexs = gvMain.GetSelectedRows();
                if (indexs == null || indexs.Length == 0) return null;

                List<OceanList> list = new List<OceanList>();
                foreach (var item in indexs)
                {
                    OceanList tager = gvMain.GetRow(item) as OceanList;
                    if (tager != null) list.Add(tager);
                }
                return list;
            }
        }
        #endregion

        #region Init
        /// <summary>
        /// 合约列表面板
        /// </summary>
        public OPContractListPart()
        {
            InitializeComponent();
            Disposed += delegate {
                CurrentChanged = null;
                CurrentChanging = null;
                gcMain.DataSource = null;
                bsList.PositionChanged -= bsMainList_PositionChanged;
                bsList.DataSource = null;
                bsList.Dispose();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        private void SetCnText()
        {
            colContractName.Caption = "合约名";
            colShippingLineName.Caption = "航线";
            colContractNo.Caption = "合约号";
            colCarrierName.Caption = "船东";
            colConsigneeNames .Caption ="收货人";
            colNotifyNames.Caption = "通知人";
            colPaymentTermName.Caption = "运输条款";
            colCurrencyName.Caption = "币种";
            colContractType.Caption = "类型";
            colFromDate.Caption = "开始日期";
            colToDate.Caption = "结束日期";
            colState .Caption = "状态";
            colPublisherName.Caption = "发布人";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitMessage();
                Utility.ShowGridRowNo(gvMain);
            }
        }
        private void InitMessage()
        {
            RegisterMessage("PleaSeselectTheDataToCompareTwo", "Please select the data to compare two");

            RegisterMessage("DeleteSuccessfully", "Delete Successfully");
            RegisterMessage("SaveSuccessfully", "Save Successfully");

            RegisterMessage("CopySuccessfully", "A new contract has been created successfully. Please re-fill in the Contract NO && Duration.");

            RegisterMessage("UpdateSelectedContract", "Are you sure you want to {0} the selected contract?");
            RegisterMessage("Publish", "publish");
            RegisterMessage("Pause", "pause");
            RegisterMessage("Invalidate", "invalidate");
            RegisterMessage("Resume", "resume");
        }

 

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        protected OceanList CurrentRow
        {
            get { return Current as OceanList; }
            set
            {
                OceanList currentRow = CurrentRow;
                currentRow = value;
            }
        }

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

        public override void Refresh(object items)
        {
            List<OceanList> list = DataSource as List<OceanList>;
            if (list == null) return;
            List<OceanList> newLists = items as List<OceanList>;

            foreach (var item in newLists)
            {
                OceanList tager = list.Find(delegate(OceanList jItem) { return item.ID == jItem.ID; });
                if (tager == null) continue;

                Utility.CopyToValue(item,tager, typeof(OceanList));
            }
            bsList.ResetBindings(false);
        }

        public override void RemoveItem(int index)
        {
            bsList.RemoveAt(index);
        }

        public override void RemoveItem(object item)
        {
            bsList.Remove(item);
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        public override event CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;

        #endregion

        #region GridView Event

        public void bsMainList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        private void gvMain_BeforeLeaveRow(object sender, RowAllowEventArgs e)
        {
            if (CurrentChanging != null)
            {
                CancelEventArgs ce = new CancelEventArgs();
                CurrentChanging(this, ce);
                e.Allow = !ce.Cancel;
            }
        }

        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            OceanList list = gvMain.GetRow(e.RowHandle) as OceanList;
            if (list == null) return;

            if (list.State == OceanState.Invalidated|| list.State == OceanState.Expired )
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Disabled);
            else if (list.State == OceanState.Draft )
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.NewLine);
        }


        #endregion

        #region Workitem Common

        [CommandHandler(OPCommonConstants.Command_AddData)]
        public void Command_AddData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {

                bsList.PositionChanged -= new EventHandler(bsMainList_PositionChanged);
                OceanInfo newData = new OceanInfo();
                newData.State = OceanState.Draft;
                newData.ContractType = ContractType.BOTH;
                newData.RateType = RateType.Contract;

                newData.CreateByID = LocalData.UserInfo.LoginID;
                newData.CreateByName = LocalData.UserInfo.LoginName;
                newData.FromDate = newData.ToDate = newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
                newData.PaymentTermID = Guid.Empty;
                newData.Permission = OceanPermission.Manager;
                newData.PublisherID = LocalData.UserInfo.LoginID;
                newData.PublisherName = LocalData.UserInfo.LoginName;
                newData.CurrencyID = OceanPriceUIDataHelper.USDCurrency.ID;
                newData.CurrencyName = OceanPriceUIDataHelper.USDCurrency.Code;

                newData.UpdateDate = null;
                SetNormalRateUnitValue(newData);
                newData.CancelEdit();
                newData.EndEdit();
                newData.BeginEdit();
                bsList.Insert(0, newData);

                if (gvMain.FocusedRowHandle >= 0)
                {
                    int rowhandle = gvMain.GetRowHandle(0);
                    gvMain.FocusedRowHandle = rowhandle;
                    gvMain.ClearSelection();
                    gvMain.SelectCell(rowhandle, colContractName);
                }
                if (CurrentChanged != null) CurrentChanged(this, newData);

                bsList.PositionChanged += new EventHandler(bsMainList_PositionChanged);
            }
        }

        void SetNormalRateUnitValue(OceanInfo data)
        {
            if (data.OceanUnits != null && data.OceanUnits.Count > 0) return;
            data.OceanUnits = new List<OceanUnitList>();

            OceanUnitList unit1 = new OceanUnitList();
            unit1.UnitID = new Guid("C4D1AFB9-94E2-42FD-921E-34EF2D332553");
            unit1.UnitName = "20GP";
            data.OceanUnits.Add(unit1);

            OceanUnitList unit2 = new OceanUnitList();
            unit2.UnitID = new Guid("9C0619F2-9A75-4DCA-BEE5-606FB5BBAB60");
            unit2.UnitName = "40GP";
            data.OceanUnits.Add(unit2);

            OceanUnitList unit3 = new OceanUnitList();
            unit3.UnitID = new Guid("3C7C3AB0-D245-41DE-8881-D845A08A1E9A");
            unit3.UnitName = "40HQ";
            data.OceanUnits.Add(unit3); 

            OceanUnitList unit4 = new OceanUnitList();
            unit4.UnitID = new Guid("F6FC92E2-2FE5-4A0B-B622-D0141235BE2A");
            unit4.UnitName = "45HQ";
            data.OceanUnits.Add(unit4);

            //OceanUnitList unit5 = new OceanUnitList();
            //unit5.UnitID = new Guid("86E11CF3-2D46-E011-A69C-001321CC6D9F");
            //unit5.UnitName = "20NOR";
            //data.OceanUnits.Add(unit5);

            OceanUnitList unit6 = new OceanUnitList();
            unit6.UnitID = new Guid("63DBF0F7-4D29-446B-A9DE-9DADB1A36260");
            unit6.UnitName = "40NOR";
            data.OceanUnits.Add(unit6);
        }
        /// <summary>
        /// 复制合约
        /// </summary>
        [CommandHandler(OPCommonConstants.Command_CopyData)]
        public void Command_CopyData(object sender, EventArgs e)
        {
            Workitem.Commands[OPCommonConstants.Command_FirstTabFocused].Execute();
            using (new CursorHelper(Cursors.WaitCursor))
            {
                int theradID = 0;
                if (CurrentRow == null || Utility.GuidIsNullOrEmpty(CurrentRow.ID)) return;

                bsList.PositionChanged -= new EventHandler(bsMainList_PositionChanged);

                try
                {
                    theradID=LoadingServce.ShowLoadingForm();

                    OceanInfo newData =OceanPriceService.CopyOceanInfo(CurrentRow.ID, LocalData.UserInfo.LoginID);//= opService.GetOceanInfo(newID);

                    newData.CancelEdit();
                    newData.EndEdit();
                    newData.BeginEdit();
                    bsList.Insert(0, newData);
                    gvMain.CancelSelection();
                    gvMain.FocusedRowHandle = 0;
                    gvMain.SelectCell(0, colContractName);
                    if (CurrentChanged != null) CurrentChanged(this, newData);


                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "CopySuccessfully"));
                    LoadingServce.CloseLoadingForm(theradID);
                }
                catch (Exception ex)
                {
                    LoadingServce.CloseLoadingForm(theradID);
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                }

                bsList.PositionChanged += new EventHandler(bsMainList_PositionChanged);
            }
        }
        /// <summary>
        /// 删除合约
        /// </summary>
        [CommandHandler(OPCommonConstants.Command_DeleteData)]
        public void Command_DeleteData(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            try
            {
                if (Utility.EnquireIsDeleteCurrentData() == false) return;

                if (Utility.GuidIsNullOrEmpty(CurrentRow.ID) == false)
                    OceanPriceService.RemoveOceanInfo(CurrentRow.ID, LocalData.UserInfo.LoginID,CurrentRow.UpdateDate);

                bsList.RemoveCurrent();
                if (CurrentChanged != null) CurrentChanged(this, Current);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "DeleteSuccessfully"));
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
        }

        /// <summary>
        /// 使合约生效/过期
        /// </summary>
        [CommandHandler(OPCommonConstants.Command_InvalidateResumeData )]
        public void Command_InvalidateResumeData(object sender, EventArgs e)
        {
            if (CurrentRow == null || Utility.GuidIsNullOrEmpty(CurrentRow.ID)) return;

            try
            {
                OceanState rstate = (CurrentRow.State != OceanState.Invalidated) ? OceanState.Invalidated : OceanState.Draft;


                string message = string.Empty;
                if (rstate == OceanState.Invalidated)
                    message = string.Format(NativeLanguageService.GetText(this, "UpdateSelectedContract"), NativeLanguageService.GetText(this, "Invalidate"));
                else
                    message = string.Format(NativeLanguageService.GetText(this, "UpdateSelectedContract"), NativeLanguageService.GetText(this, "Resume"));

                if (Utility.EnquireYesOrNo(message) == false) return;

                SingleResultData result = OceanPriceService.ChangeOceanState(CurrentRow.ID, rstate, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
                CurrentRow.State = rstate;
                CurrentRow.PublishDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
                CurrentRow.PublisherName = LocalData.UserInfo.LoginName;
                CurrentRow.UpdateDate = result.UpdateDate;
                Refresh(new List<OceanList> { CurrentRow });
                bsMainList_PositionChanged(null, null);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), "Set Successfully");
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
        }

        /// <summary>
        /// 比较合约
        /// </summary>
        [CommandHandler(OPCommonConstants.Command_Compare)]
        public void Command_Compare(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (SelectRows == null || SelectRows.Count != 2)
                {
                    Utility.ShowMessage(NativeLanguageService.GetText(this, "PleaSeselectTheDataToCompareTwo"));
                    return;
                }

                ComparisionList Comparision = Workitem.Items.AddNew<ComparisionList>();
                Comparision.Contract1ID = SelectRows[0].ID;
                Comparision.Contract2ID = SelectRows[1].ID;
                Comparision.Width = Screen.PrimaryScreen.Bounds.Width - 10;

                Comparision.Contract1Title = SelectRows[0].ContractName + ": " + SelectRows[0].FromDate.ToShortDateString() + " TO " + SelectRows[0].ToDate.ToShortDateString();
                Comparision.Contract2Title = SelectRows[1].ContractName + ": " + SelectRows[1].FromDate.ToShortDateString() + " TO " + SelectRows[1].ToDate.ToShortDateString();


                PartLoader.ShowDialog(Comparision, "Comparision");
            }
        }
        #region RefreshData

        [CommandHandler(OPCommonConstants.Command_RefreshData)]
        public void Command_RefreshData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    List<OceanList> list = DataSource as List<OceanList>;
                    if (list == null || list.Count == 0) return;

                    List<Guid> ids = new List<Guid>();
                    foreach (var item in list)
                    {
                        if (item.ID.IsNullOrEmpty()) continue;

                        ids.Add(item.ID);
                    }

                    List<OceanList> newList = OceanPriceService.GetOceanListByIds(ids.ToArray(), LocalData.UserInfo.LoginID);
                    DataSource = newList;
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
            }
        }
        #endregion


        #endregion

        #region 导出到Excel
        [CommandHandler(OPCommonConstants.Command_ExportToExcel)]
        public void Command_ExportToExcel(object sender, EventArgs e)
        {
            if (CurrentRow == null || Utility.GuidIsNullOrEmpty(CurrentRow.ID)) return;
            int theradID = 0;
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                   

                    SaveFileDialog saveFile = new SaveFileDialog();
                    saveFile.FileName = CurrentRow.ContractNo;
                    saveFile.Filter = "xls files(*.xls)|*.xls";
                    saveFile.RestoreDirectory = true;
                    saveFile.FilterIndex = 2;
                    if (saveFile.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    string fileName = saveFile.FileName.ToString();

                    theradID=LoadingServce.ShowLoadingForm("Exporting......");


                    OceanRateToExcel list = OceanPriceService.ExportOceanRateToExcel(CurrentRow.ID, false);

                    OceanRateExportToExcel toExcelForm = Workitem.Items.AddNew<OceanRateExportToExcel>();
                    toExcelForm.BindData(list.DataList, list.UnitList);

                    //PartLoader.ShowDialog(toExcelForm,"");

                    toExcelForm.ExportToExcel(fileName);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                }
                finally
                {
                    LoadingServce.CloseLoadingForm(theradID);

                }
            }
        }


        public static void DataTableToExcel(DataTable dt, string fileName)
        { 
            Application xlApp = new Application();
            if (xlApp == null)
            {
                XtraMessageBox.Show("无法启动Excel，可能您未安装Excel");
                return;
            }
            Workbook workbook = xlApp.Workbooks.Add(true);
            Worksheet worksheet = (Worksheet)workbook.Worksheets[1];
            Range range;


            // string oldCaption = dt.CaptionText;
            // 列索引，行索引，总列数，总行数                   
            int colIndex = 0;
            int RowIndex = 0;
            int colCount = dt.Columns.Count;
            int RowCount = dt.Rows.Count;

            // *****************获取数据*********************
            // 创建缓存数据
            object[,] objData = new object[RowCount + 1, colCount];
            // 获取列标题
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                objData[RowIndex, colIndex++] = dt.Columns[i].Caption; // dgv.Columns[i].HeaderText;
            }

            // 获取具体数据
            for (RowIndex = 1; RowIndex < RowCount; RowIndex++)
            {
                for (colIndex = 0; colIndex < colCount; colIndex++)
                {
                    objData[RowIndex, colIndex] = dt.Rows[RowIndex - 1][colIndex];
                }
            }

            //********************* 写入Excel*******************
            range = worksheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[RowCount, colCount]);
            range.Value2 = objData;
            System.Windows.Forms.Application.DoEvents();

            //*******************设置输出格式******************************

            //设置顶部説明   合并的单元格
            range = worksheet.get_Range(xlApp.Cells[1, 1], xlApp.Cells[1, colCount]);
            range.MergeCells = true;
            range.RowHeight = 38;
            range.Font.Bold = true;
            range.Font.Size = 14;
            range.Font.ColorIndex = 10;//字体颜色
            xlApp.ActiveCell.FormulaR1C1 = "导入记录查询结果";

            //特殊数字格式
            range = worksheet.get_Range(xlApp.Cells[2, colCount], xlApp.Cells[RowCount, colCount]);

            xlApp.Cells.HorizontalAlignment = Constants.xlCenter;
            range = worksheet.get_Range(xlApp.Cells[2, 1], xlApp.Cells[2, colCount]);

            range.Font.Bold = true;
            range.RowHeight = 20;

            if (fileName != "")
            {
                try
                {
                    workbook.Saved = true;
                    workbook.SaveCopyAs(fileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                    return;
                }
            }

            xlApp.Quit();
            GC.Collect();//强行销毁  
        }

        #endregion

        internal void RefreshUpdate()
        {
            if (CurrentRow != null)
            {
                CurrentRow = OceanPriceService.GetOceanListByIds(new Guid[] { CurrentRow.ID }, LocalData.UserInfo.LoginID)[0];
                if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
            }
        }
    }
}
