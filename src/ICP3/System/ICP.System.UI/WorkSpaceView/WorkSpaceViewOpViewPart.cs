using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using DevExpress.XtraGrid.Columns;
using ICP.Framework.ClientComponents.Controls;

namespace ICP.Sys.UI.WorkSpaceView
{
    public partial class WorkSpaceViewOpViewPart : BaseListPart
    {
        public WorkSpaceViewOpViewPart()
        {
            InitializeComponent();
        }

        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IPermissionService PermissionService
        {
            get
            {
                return ServiceClient.GetService<IPermissionService>();
            }
        }

        #endregion

        #region 属性
        public WorkSpaceList MainList
        {
            get;
            set;
        }
        public Guid MainID { get; set; }
        public DateTime? MainUpdateDate { get; set; }
        public OperationViewList CurrentRow
        {
            get
            {
                return bsList.Current as OperationViewList;
            }
        }
        public List<OperationViewList> DataList
        {
            get
            {
                return bsList.DataSource as List<OperationViewList>;
            }
        }
        #endregion

        #region 初始化
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
                Utility.ShowGridRowNo(this.gvDetails);
            }
        }
        private void InitControls()
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OperationType>> operationTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OperationType>(LocalData.IsEnglish);
            cmbOperationType.Properties.BeginUpdate();
            cmbOperationType.Properties.Items.Clear();
            foreach (var item in operationTypes)
            {
                if (item.Value != OperationType.Unknown)
                {
                    cmbOperationType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
                }
            }
            cmbOperationType.Properties.EndUpdate();
        }
        #endregion

        #region 绑定
        public void BindData( )
        {
            this.txtCode.Text = MainList.Code;
            this.txtCName.Text = MainList.CName;
            this.txtEName.Text = MainList.EName;

            MainID = MainList.ID;
            MainUpdateDate = MainList.UpdateDate;

            List<OperationViewList> list = PermissionService.GetOperationViewList(MainList.ID);
            bsList.DataSource = list;
            bsList.ResetBindings(false);

        }
        #endregion

        #region Grid事件
        private void gvDetails_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            OperationViewList list = gvDetails.GetRow(e.RowHandle) as OperationViewList;
            if (list == null) return;

            Font font = e.Appearance.Font;

            if (list.IsCheck)
            {
                e.Appearance.ForeColor = Color.Red;
            }
            else
            {
                e.Appearance.ForeColor = Color.Black; 
            }
         
        }
        #endregion

        #region WorkSpace
        private void barNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MainID = Guid.NewGuid();
            MainUpdateDate = null;

            this.txtCode.Text = string.Empty;
            this.txtCName.Text = string.Empty;
            this.txtEName.Text = string.Empty;
        }
        public event SavedHandler Saved;
        private void barSaves_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           SingleResultData result= PermissionService.SaveWorkSpaceInfo(MainID,
                                                this.txtCode.Text,
                                                this.txtCName.Text,
                                                this.txtEName.Text,
                                                LocalData.UserInfo.LoginID,
                                                MainUpdateDate);
          

           if (Saved != null)
           {
               WorkSpaceList item = new WorkSpaceList { ID = result.ID,
                                                        Code=this.txtCode.Text,
                                                        CName=this.txtCName.Text,
                                                        EName=this.txtEName.Text,
                                                        CreateByName=LocalData.UserInfo.LoginName,
                                                        UpdateDate = result.UpdateDate};
               Saved(item);
           }

           LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(),"保存成功!");
        }
        #endregion

        #region OperationView
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                WorkSpaceOPViewEdit opPart = Workitem.Items.AddNew<WorkSpaceOPViewEdit>();
                opPart.OperationViewList = new OperationViewList();
                opPart.WorkSpaceID = MainID;
                string title = "New OperationView";
                opPart.Saved += delegate(object[] prams)
                {
                    EditSaved(prams);
                };

                PartLoader.ShowDialog(opPart, title);
            }
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditOperationViewPart();
        }
        private void gvDetails_DoubleClick(object sender, EventArgs e)
        {
            EditOperationViewPart();
        }

        private void EditOperationViewPart()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;

                WorkSpaceOPViewEdit opPart = Workitem.Items.AddNew<WorkSpaceOPViewEdit>();
                opPart.OperationViewList = CurrentRow;
                opPart.WorkSpaceID = MainID;
                string title = "Edit OperationView";
                opPart.Saved += delegate(object[] prams)
                {
                    EditSaved(prams);
                };

                PartLoader.ShowDialog(opPart, title);
            }
        }
        private void EditSaved(object[] prams)
        {
            OperationViewList data = prams[0] as OperationViewList;

            if (DataList == null || DataList.Count == 0)
            {
                this.bsList.Add(data);
                bsList.ResetBindings(false);
            }
            else
            {
                OperationViewList tager = DataList.Find(delegate(OperationViewList item) { return item.ID == data.ID; });
                if (tager == null)
                {
                    bsList.Insert(0, data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    Utility.CopyToValue(data, tager, typeof(OperationViewList));
                    bsList.ResetItem(bsList.IndexOf(tager));
                }

            }
            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "保存成功!");
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.gvDetails.Focus();
            List<Guid> opIDs = new List<Guid>();
            List<Int32> indexList = new List<int>();

            foreach (OperationViewList item in DataList)
            {
                if (item.IsCheck)
                {
                    opIDs.Add(item.ID);
                    indexList.Add(DataTypeHelper.GetInt(item.ShowIndex,1));
                }
            }

            PermissionService.SaveWorkSpace2OperationViewList(MainID,
                                                            opIDs.ToArray(),
                                                            indexList.ToArray(),
                                                            LocalData.UserInfo.LoginID);

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(),"保存成功!");
        }

        private void gvDetails_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if(e.Column==this.colIsCheck)
            { 
            OperationViewList item = gvDetails.GetRow(e.RowHandle) as OperationViewList;
            if (item != null)
            {
                item.IsCheck = !item.IsCheck;
                bsList.ResetCurrentItem();
            }
            }
        }

        #endregion

       
      


    }
}
