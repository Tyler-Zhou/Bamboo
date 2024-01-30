using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Sys.UI.WorkSpaceView
{
    public partial class WorkSpaceOPViewEdit : BaseEditPart
    {
        public WorkSpaceOPViewEdit()
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
        public Guid WorkSpaceID { get; set; }
        public OperationViewList OperationViewList { get; set; }
        #endregion

        #region 绑定
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
                SetData();
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
        private void SetData()
        {
            this.cmbOperationType.EditValue = OperationViewList.OperationType;
            this.txtCode.Text = OperationViewList.Code;
            this.txtCName.Text = OperationViewList.CName;
            this.txtEName.Text = OperationViewList.EName;
            this.txtSelectedColumn.Text = OperationViewList.SelectedColumn;
            this.txtBaseCriteria.Text = OperationViewList.BaseCriteria;
            this.txtToolTipCN.Text = OperationViewList.TooltiopCN;
            this.txtToolTipEN.Text = OperationViewList.TooltiopEN;
        }
        #endregion

        #region 保存
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
            {
                return;
            }
            GetData();
            try
            {
                SingleResultData result = PermissionService.SaveOperationViewInfo(
                                        WorkSpaceID,
                                        OperationViewList.ID,
                                        OperationViewList.OperationType,
                                        OperationViewList.Code,
                                        OperationViewList.CName,
                                        OperationViewList.EName,
                                        OperationViewList.TooltiopCN,
                                        OperationViewList.TooltiopEN,
                                        OperationViewList.SelectedColumn,
                                        OperationViewList.BaseCriteria,
                                        LocalData.UserInfo.LoginID,
                                        OperationViewList.UpdateDate);

                OperationViewList.ID = result.ID;
                OperationViewList.UpdateDate = result.UpdateDate;

                if (Saved != null)
                {
                    Saved(OperationViewList);
                }
                this.FindForm().Close();
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
            }

        }

        public override event SavedHandler Saved;

        private bool ValidateData()
        {
            if (this.txtCode.Text == string.Empty)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("代码不能为空");
                this.txtCode.Focus();
                return false;
            }
            if (this.txtCName.Text == string.Empty)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("中文名称不能为空");
                this.txtCName.Focus();
                return false;
            }
            if (this.txtEName.Text == string.Empty)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("英文名称不能为空");
                this.txtEName.Focus();
                return false;
            }
            if (this.cmbOperationType.EditValue==null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("业务类型不能为空");
                this.cmbOperationType.Focus();
                return false;
            }
            return true;
     
        }
        private void GetData()
        {
            OperationViewList.OperationType = (OperationType)this.cmbOperationType.EditValue;
            OperationViewList.Code = this.txtCode.Text;
            OperationViewList.CName = this.txtCName.Text;
            OperationViewList.EName = this.txtEName.Text;
            OperationViewList.SelectedColumn = this.txtSelectedColumn.Text;
            OperationViewList.BaseCriteria = this.txtBaseCriteria.Text;
            OperationViewList.TooltiopCN = this.txtToolTipCN.Text;
            OperationViewList.TooltiopEN = this.txtToolTipEN.Text;
        }
        #endregion

        #region 窗体
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
        #endregion

    
    }
}
