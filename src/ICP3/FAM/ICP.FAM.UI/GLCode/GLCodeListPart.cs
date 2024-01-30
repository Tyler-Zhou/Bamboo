using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Common;
using ICP.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.FAM.UI.GLCode;
using ICP.Framework.ClientComponents.Controls;

namespace ICP.FAM.UI
{
    public partial class GLCodeListPart : BaseListPart
    {
        public GLCodeListPart()
        {
            InitializeComponent();
            Disposed += delegate {
                Selected = null;
                CurrentChanged = null;
                lwTreeGridControl1.DataSource = null;
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

        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }


        #endregion

        #region 窗体事件
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
                InitMessage();
                lwTreeGridControl1.IndicatorWidth = 30;
            }

        }

        private void InitMessage()
        {
            RegisterMessage("1305230001", LocalData.IsEnglish ? "Are you sure to invalidate the selected time?" : "确认要作废该数据");
            RegisterMessage("1305230002", LocalData.IsEnglish ? "Are you sure to resume the selected time?" : "确认要激活该数据");
        }
        /// <summary>
        /// 初始化控件 
        /// </summary>
        private void InitControls()
        {
            //科目类型
            List<EnumHelper.ListItem<GLCodeType>> glCodeType = EnumHelper.GetEnumValues<GLCodeType>(LocalData.IsEnglish);
            foreach (var item in glCodeType)
            {
                if (item.Value != GLCodeType.Unknown)
                {
                    cmbGLCodeType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                }
            }
            //帐页格式
            List<EnumHelper.ListItem<GLCodeLedgerStyle>> ledgerStyle = EnumHelper.GetEnumValues<GLCodeLedgerStyle>(LocalData.IsEnglish);
            foreach (var item in ledgerStyle)
            {
                if (item.Value != GLCodeLedgerStyle.Unknown)
                {
                    cmbLedgerStyle.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                }
            }
            //余额方向
            List<EnumHelper.ListItem<GLCodeProperty>> glCodeProperty = EnumHelper.GetEnumValues<GLCodeProperty>(LocalData.IsEnglish);
            foreach (var item in glCodeProperty)
            {
                if (item.Value != GLCodeProperty.Unknown)
                {
                    cmbGLCodeProperty.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                }
            }

            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID,true);
            if (configureInfo != null)
            {
                ConfigureInfo = configureInfo;
                SolutionID = configureInfo.SolutionID;
            }  
        }
        /// <summary>
        /// 设置行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lwTreeGridControl1_CustomDrawNodeIndicator(object sender, CustomDrawNodeIndicatorEventArgs e)
        {
            if (e.IsNodeIndicator == false || e.ObjectArgs == null) return;

            IndicatorObjectInfoArgs args = e.ObjectArgs as IndicatorObjectInfoArgs;
            if (args != null)
            {
                int rowNum = lwTreeGridControl1.GetVisibleIndexByNode(e.Node) + 1;
                args.DisplayText = rowNum.ToString();
            }
        }
        #endregion

        #region 属性
        private Guid solutionID;
        /// <summary>
        /// 解决方案ID
        /// </summary>
        public Guid SolutionID
        {
            get { return solutionID; }
            set { solutionID = value; }
        }

        /// <summary>
        /// 公司ID集合
        /// </summary>
        public List<Guid> CompanyIDs
        {
            get;
            set;
        }

        protected virtual bool IsFinder { get { return false; } }

        public override event SelectedHandler Selected;

        #endregion

        #region  事件处理
        /// <summary>
        /// 选择项发生改变
        /// </summary>
        public override event CurrentChangedHandler CurrentChanged;
        #endregion

        #region 数据源
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
                BindDataList(value);
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="value"></param>
        private void BindDataList(object value)
        {
            List<SolutionGLCodeList> list = value as List<SolutionGLCodeList>;
            if (list == null)
            {
                list = new List<SolutionGLCodeList>();
            }

            list = (from d in list where d.Code != "0" select d).ToList();

            if (LocalData.IsEnglish)
            {
                colName.FieldName = "EName";
            }

            bsList.DataSource = list;
            bsList.ResetBindings(false);

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "total search " + list.Count.ToString() + " data." : "总共查询到 "
                            + list.Count.ToString() + " 条数据.");

            if (CurrentChanged != null)
            {
                CurrentChanged(this, CurrentRow);
            }

            lwTreeGridControl1.IndicatorWidth = 35;

            lwTreeGridControl1.ExpandAll();
        }

        public override object Current
        {
            get
            {
                return bsList.Current;
            }
        }

        /// <summary>
        /// 当前行数据
        /// </summary>
        protected SolutionGLCodeList CurrentRow
        {
            get
            {
                return bsList.Current as SolutionGLCodeList;
            }
        }
        private ConfigureInfo ConfigureInfo
        {
            get;
            set;
        }
        #endregion

        #region 按钮方法 

        /// <summary>
        /// 编辑界面保存数据
        /// </summary>
        /// <param name="prams"></param>
        private void EditPartSaved(object[] prams)
        {
            if (prams == null || prams.Length == 0) return;

            SolutionGLCodeList data = prams[0] as SolutionGLCodeList;

            List<SolutionGLCodeList> source = DataSource as List<SolutionGLCodeList>;
            if (source == null || source.Count == 0)
            {
                bsList.Add(data);
                bsList.ResetBindings(false);
            }
            else
            {
                SolutionGLCodeList tager = source.Find(delegate(SolutionGLCodeList item) { return item.ID == data.ID; });
                if (tager == null)
                {
                    bsList.Add(data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    FAMUtility.CopyToValue(data, tager, typeof(SolutionGLCodeList));

                    bsList.ResetItem(bsList.IndexOf(tager));
                }
            } 
           // this.lwTreeGridControl1.ExpandAll();

            if (CurrentChanged != null)
            {
                CurrentChanged(this, CurrentRow);
            }

        }

        #region 所属公司
        /// <summary>
        ///所属公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(GLCodeCommandConstants.Command_GLCompany)]
        public void Command_GLCompany(object sender, EventArgs e)
        {
            GLCompanyEdit setCom = Workitem.Items.AddNew<GLCompanyEdit>();
            setCom.myGLID = CurrentRow.ID;
            setCom.Code = CurrentRow.Code;
            string title = LocalData.IsEnglish ? "Set Company" : "设置所属公司";
            PartLoader.ShowDialog(setCom, title);
            Workitem.Items.Remove(setCom);
        }
        #endregion

        #region 新增
        /// <summary>
        ///新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(GLCodeCommandConstants.Command_GLCodeAdd)]
        public void Command_GLCodeAdd(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                SolutionGLCodeList glCode = new SolutionGLCodeList();
                glCode.SolutionID = SolutionID;
                glCode.CreateByID = LocalData.UserInfo.LoginID;
                glCode.GLCodeType = GLCodeType.ASSETS;
                glCode.GLCodeProperty = GLCodeProperty.Debit;
                glCode.LedgerStyle = GLCodeLedgerStyle.AMOUNT;
                if (CurrentRow != null)
                {
                    FAMUtility.CopyToValue(CurrentRow,glCode, typeof(SolutionGLCodeList));

                    glCode.ParentID = CurrentRow.ID;
                    glCode.ParentName = LocalData.IsEnglish ? CurrentRow.EName:CurrentRow.CName;

                    glCode.ID = Guid.Empty;
                    glCode.Code = string.Empty;
                    glCode.EName = string.Empty;
                    glCode.CName = string.Empty;
                    glCode.Description = string.Empty;
                    glCode.CreateByID = LocalData.UserInfo.LoginID;
                    glCode.UpdateDate = null;
                }

                GLCodeEditPart addPart = Workitem.Items.AddNew<GLCodeEditPart>();
                addPart.DataSource = glCode;
                addPart.ConfigureInfo = ConfigureInfo;

                IWorkspace mainWorkspace = Workitem.Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Add GLCode" : "新增会计科目";
                mainWorkspace.Show(addPart, smartPartInfo);

               addPart.Saved+=new SavedHandler(EditPartSaved);
            }
        }

   

        #endregion

        #region 编辑
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(GLCodeCommandConstants.Command_GLCodeEdit)]
        public void Command_GLCodeEdit(object sender, EventArgs e)
        {
            EditData();
        }
        private void lwTreeGridControl1_DoubleClick(object sender, EventArgs e)
        {
            DoubleClick(sender,e);
        }

        public virtual void DoubleClick(object sender, EventArgs e)
        {
            EditData();
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        private void EditData()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow != null)
                {
                    GLCodeEditPart addPart = Workitem.Items.AddNew<GLCodeEditPart>();

                    SolutionGLCodeList glCode = new SolutionGLCodeList();
                    FAMUtility.CopyToValue(CurrentRow, glCode, typeof(SolutionGLCodeList));

                    addPart.DataSource = glCode;
                    addPart.ConfigureInfo = ConfigureInfo;
                    addPart.CompanyIDs = CompanyIDs;

                    IWorkspace mainWorkspace = Workitem.Workspaces[ClientConstants.MainWorkspace];
                    SmartPartInfo smartPartInfo = new SmartPartInfo();
                    smartPartInfo.Title = LocalData.IsEnglish ? "Edit GLCode" : "编辑会计科目";
                    mainWorkspace.Show(addPart, smartPartInfo);

                    addPart.Saved += new SavedHandler(EditPartSaved);
                }
            }
        }

        #endregion

        #region 作废/激活
        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(GLCodeCommandConstants.Command_GLCodeCancel)]
        public void Command_GLCodeCancel(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            try
            {

                string message = string.Empty;
                if (CurrentRow.IsValid)
                    message = NativeLanguageService.GetText(this, "1305230001");
                else
                    message = NativeLanguageService.GetText(this, "1305230002");

                if (FAMUtility.ShowResultMessage(message))
                {
                    SingleResultData result = ConfigureService.ChangeSolutionGLCodeState(
                            CurrentRow.ID,
                           !CurrentRow.IsValid,
                            LocalData.UserInfo.LoginID,
                            CurrentRow.UpdateDate);

                    CurrentRow.IsValid = !CurrentRow.IsValid;
                    CurrentRow.UpdateDate = result.UpdateDate;
                    bsList.ResetCurrentItem();

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Changed GLCode State Successfully" : "更改会计科目状态成功");
                    if (CurrentChanged != null)
                    {
                        CurrentChanged(this, CurrentRow);
                    }
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Changed GLCode State Failed" : "更改会计科目状态失败") + ex.Message);
            }
        }

        #endregion

        [CommandHandler(GLCodeCommandConstants.Command_GLCodeTo)]
        public void Command_GLCodeTo(object sender, EventArgs e)
        {
            lwTreeGridControl1.ExportToXls("会计科目.xls");
        }


        private void lwTreeGridControl1_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
            {
            if (e.Node == null)
            {
                return;
            }

            SolutionGLCodeList data = lwTreeGridControl1.GetDataRecordByNode(e.Node) as SolutionGLCodeList;

            if (data == null) return;

            if (data.IsValid == false)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Disabled);
            }


        }


        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
        }
        #endregion

 

 
    }
}
