using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Service;
using ICP.Common.ServiceInterface;

namespace ICP.FAM.UI.VerificationSheet
{
    [ToolboxItem(false)]
    public partial class VerificationSheetListPart : BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IVerificationSheetService VerificationSheetService
        {
            get
            {
                return ServiceClient.GetService<IVerificationSheetService>();
            }
        }

        #endregion

        #region 初始化

        public VerificationSheetListPart()
        {
            InitializeComponent();
           
            Disposed += delegate {
                gcMain.DataSource = null;
                bsList.PositionChanged -= bsList_PositionChanged;
                bsList.DataSource = null;
                bsList.Dispose();
                KeyDown = null;
                CurrentChanged = null;
                CurrentChanging = null;
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
            RegisterMessage("RemoveSuccessfully", LocalData.IsEnglish ? "Remove Successfully!" : "删除成功!");
            RegisterMessage("ComfirmDelete", LocalData.IsEnglish ? "Are you sure to delete?" : "确认删除当前数据?");
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
            Common.ServiceInterface.DataObjects.VerificationSheet list = gvMain.GetRow(e.RowHandle) as Common.ServiceInterface.DataObjects.VerificationSheet;
            if (list == null) return;

            //if (list.IsValid == false)
            //{
            //    ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
            //}

        }
        #endregion

        #region 命令

        [CommandHandler(VerificationCommandConstants.Command_Add)]
        public void Commond_Add(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Common.ServiceInterface.DataObjects.VerificationSheet sheet = new Common.ServiceInterface.DataObjects.VerificationSheet();
                sheet.CreateByID = LocalData.UserInfo.LoginID;
                sheet.CreateByName = LocalData.UserInfo.LoginName;
                sheet.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
                //telex.CompanyId = LocalData.UserInfo.DefaultCompanyID;
                gvMain.ClearSorting();
                bsList.Add(sheet);
                gvMain.MoveLast();
                sheet.BeginEdit();
            }
        }

        [CommandHandler(VerificationCommandConstants.Command_Delete)]
        public void Command_Delete(object sender, EventArgs e)
        {
            //gvMain.CloseEditor();
            if (bsList.Current == null) return;

            #region 询问
            DialogResult result = XtraMessageBox.Show(
                                             NativeLanguageService.GetText(this, "ComfirmDelete"),
                                              LocalData.IsEnglish ? "Tip" : "Tip",
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            #endregion  

            try
            {
                var sheet = bsList.Current as Common.ServiceInterface.DataObjects.VerificationSheet;
                if (sheet != null)
                {
                    if (sheet.ID != null && sheet.ID != Guid.Empty)
                    {
                       VerificationSheetService.RemoveVerificationSheet(sheet.ID, LocalData.UserInfo.LoginID, sheet.UpdateDate);
                    }

                    bsList.RemoveCurrent();
                    //bsDataSource.MoveFirst();
                    //bsList.ResetBindings(false);
                    //this.treeMain.BestFitColumns();               
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "RemoveSuccessfully"));

                }
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
        }

        public override void Refresh(object items)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (items == null)
                {
                    return;
                }

                object[] paras = (object[])items;

                Common.ServiceInterface.DataObjects.VerificationSheet data = paras[0] as Common.ServiceInterface.DataObjects.VerificationSheet;

                if (data == null)
                {
                    return;
                }

                List<Common.ServiceInterface.DataObjects.VerificationSheet> source = bsList.DataSource as List<Common.ServiceInterface.DataObjects.VerificationSheet>;


                if (source == null || source.Count == 0)
                {
                    bsList.DataSource = new List<Common.ServiceInterface.DataObjects.VerificationSheet>();
                    bsList.Insert(0, data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    Common.ServiceInterface.DataObjects.VerificationSheet tager = source.Find(delegate(Common.ServiceInterface.DataObjects.VerificationSheet item) { return item.ID == data.ID; });
                    if (tager == null)
                    {
                        bsList.Insert(0, data);
                        bsList.ResetBindings(false);
                    }
                    else
                    {
                        FAMUtility.CopyToValue(data, tager, typeof(Common.ServiceInterface.DataObjects.VerificationSheet));
                        bsList.ResetItem(bsList.IndexOf(tager));
                    }
                }
            }
        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        protected Common.ServiceInterface.DataObjects.VerificationSheet CurrentRow
        {
            get { return Current as Common.ServiceInterface.DataObjects.VerificationSheet; }
        }

        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                List<Common.ServiceInterface.DataObjects.VerificationSheet> list = value as List<Common.ServiceInterface.DataObjects.VerificationSheet>;
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

        public override event CurrentChangedHandler CurrentChanged;

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
                Workitem.Commands[VerificationCommandConstants.Command_ShowSearch].Execute();
            }

        }

        #endregion
    }
}
