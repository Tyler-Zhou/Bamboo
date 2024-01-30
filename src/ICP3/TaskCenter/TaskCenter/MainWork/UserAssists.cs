/******
 * 描述:协助同事UI
 * 创建者:王乐俊
 * 创建时间:2015-03-20
 * ******/
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.ClientComponents;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Server;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.TaskCenter.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ICP.TaskCenter.UI
{
    /// <summary>
    /// 协助同事UI
    /// </summary>
    public partial class UserAssists : XtraForm
    {
        #region Service & Property

        /// <summary>
        /// 数据操作
        /// </summary>
        IOperationViewService OperationViewService
        {
            get
            {
                return ServiceClient.GetService<IOperationViewService>();
            }
        }
        /// <summary>
        /// 用户服务
        /// </summary>
        IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        /// <summary>
        /// 父窗口
        /// </summary>
        public ViewListSmartPart ViewListSmartPart { get; set; }

        /// <summary>
        /// 选中的数据行
        /// </summary>
        UserAssistsType FocusedDataRow
        {
            get { return UserAssistsData.Current as UserAssistsType; }
        }
        /// <summary>
        /// 协助类型实体类
        /// </summary>
        UserAssistsType UserAssistsType { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public UserAssists()
        {
            InitializeComponent();
        } 
        #endregion

        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserAssists_Load(object sender, EventArgs e)
        {
            tabControlUpdate.Enabled = false;
            ClearControls();
            #region  下拉框数据绑定
            var company = LocalData.UserInfo.UserOrganizationList.FirstOrDefault(
                 n => n.IsDefault && n.Type == LocalOrganizationType.Company && n.IsDefault == true);
            if (company != null)
            {
                ComboBoxUser.Properties.BeginUpdate();
                ComboBoxUser.Properties.Items.Clear();
                ComboBoxAssister.Properties.BeginUpdate();
                ComboBoxAssister.Properties.Items.Clear();
                ComboBoxUser.Properties.Items.Insert(0, new ImageComboBoxItem(null, null));
                ComboBoxAssister.Properties.Items.Insert(0, new ImageComboBoxItem(null, null));
                List<UserList> users = UserService.GetUnderlingUserList(null, null, new string[] { "订舱", "客服", "文件" }, true);
                foreach (var item in users)
                {
                    // 用户默认的公司必须和下拉框绑定数据的公司是一致的
                    if (item.OrganizationName.Contains(company.EShortName))
                    {
                        ComboBoxUser.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
                        ComboBoxAssister.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
                    }
                }
                ComboBoxUser.Properties.EndUpdate();
                ComboBoxAssister.Properties.EndUpdate();
            }
            #endregion

            #region 绑定数据源
            UserAssistsData.DataSource = OperationViewService.GetUserAssistsList(LocalData.UserInfo.LoginID,
                DateTime.Now);
            #endregion

            if (LocalData.IsEnglish)
            {
                tabPage1.Text = "Editor";
                labelControlAssign.Text = "Assign";
                labelControltoassist.Text = "to assist";
                labelControlwork.Text = "'s work";
                labelControlDuration.Text = "Duration";
                labelControlto.Text = "to";
                gridColumnStaff.Caption = "Staff";
                gridColumnFromDate.Caption = "From Date";
                gridColumnToDate.Caption = "To Date";
                simpleButtonDelte.Text = "Delete";
                simpleButtonInsert.Text = "New";
                simpleButtonSave.Text = "Save";
            }
        }
        
        /// <summary>
        /// 新增按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonInsert_Click(object sender, EventArgs e)
        {
            tabControlUpdate.Enabled = true;
            ClearControls();
            UserAssistsType = new UserAssistsType() { Operation = "INSERT" };
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonSave_Click(object sender, EventArgs e)
        {
            if (Judge()) return;
            if (UserAssistsType.Operation == "INSERT")
            {
                UserAssistsType.Id = Guid.NewGuid();
            }
            else
            {
                UserAssistsType.Id = FocusedDataRow.Id;
            }
            UserAssistsType.CreateBy = LocalData.UserInfo.LoginID;
            UserAssistsType.AssisterId = new Guid(ComboBoxAssister.EditValue.ToString());
            UserAssistsType.UserId = new Guid(ComboBoxUser.EditValue.ToString());
            UserAssistsType.FromDate = DateTime.Parse(dateEditFromDate.Text);
            UserAssistsType.ToDate = DateTime.Parse(dateEditToDate.Text);
            if (OperationViewService.UserAssistsSave(UserAssistsType) > 0)
            {
                ViewListSmartPart.RefreshNodeInfo();
                Close();
            }
        }
        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButtonDelte_Click(object sender, EventArgs e)
        {
            if (FocusedDataRow == null)
                return;
            if (XtraMessageBox.Show((LocalData.IsEnglish ? "Do you really want to delete the currently selected record?" : "你真的要删除当前选择记录吗？")
                               , LocalData.IsEnglish ? "Tip" : "提示"
                               , MessageBoxButtons.YesNo
                               , MessageBoxIcon.Question) == DialogResult.Yes)
            {
                UserAssistsType = new UserAssistsType
                {
                    Operation = "DELETE",
                    Id = FocusedDataRow.Id,
                    CreateBy = FocusedDataRow.CreateBy,
                    AssisterId = FocusedDataRow.AssisterId,
                    UserId = FocusedDataRow.UserId,
                    FromDate = FocusedDataRow.FromDate,
                    ToDate = FocusedDataRow.ToDate
                };
                if (OperationViewService.UserAssistsSave(UserAssistsType) > 0)
                {
                    UserAssistsData.RemoveCurrent();
                    UserAssistsData.ResetBindings(false);
                    ViewListSmartPart.RefreshNodeInfo();
                    Close();
                }
            }

        }

        /// <summary>
        /// 行双击变成编辑模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Clicks == 2 && e.Button == MouseButtons.Left)
            {
                tabControlUpdate.Enabled = true;
                UserAssistsType = new UserAssistsType { Operation = "UPDATE" };
                ComboBoxAssister.EditValue = FocusedDataRow.AssisterId;
                ComboBoxUser.EditValue = FocusedDataRow.UserId;
                dateEditFromDate.EditValue = FocusedDataRow.FromDate;
                dateEditToDate.EditValue = FocusedDataRow.ToDate;
            }
        }

        /// <summary>
        /// 清空控件绑定值
        /// </summary>
        public void ClearControls()
        {
            if (ComboBoxAssister.EditValue != null && ComboBoxUser.EditValue != null
                && dateEditFromDate.EditValue != null && dateEditToDate.EditValue != null)
            {
                if (!string.IsNullOrEmpty(ComboBoxAssister.EditValue.ToString()))
                {
                    ComboBoxAssister.EditValue = string.Empty;
                }
                if (!string.IsNullOrEmpty(ComboBoxUser.EditValue.ToString()))
                {
                    ComboBoxUser.EditValue = string.Empty;
                }

                if (!string.IsNullOrEmpty(dateEditFromDate.EditValue.ToString()))
                {
                    dateEditFromDate.EditValue = string.Empty;
                }

                if (!string.IsNullOrEmpty(dateEditToDate.EditValue.ToString()))
                {
                    dateEditToDate.EditValue = string.Empty;
                }
            }
        }
        /// <summary>
        /// 保存前控件值判断
        /// </summary>
        /// <returns></returns>
        public bool Judge()
        {
            bool flg = false;
            if (ComboBoxAssister.EditValue == null || ComboBoxUser.EditValue == null)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "The staff is required." : "必须输入项.");
                flg = true;
            }
            else if (dateEditFromDate.EditValue == null || dateEditToDate.EditValue == null)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "The staff is required." : "必须输入项.");
                flg = true;
            }
            else if (DateTime.Parse(dateEditFromDate.Text) > DateTime.Parse(dateEditToDate.Text))
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish
                    ? "Start time must be less than the end time."
                    : "开始时间要小于结束时间.");
                flg = true;
            }
            else if (DateTime.Parse(dateEditFromDate.Text) == DateTime.Parse(dateEditToDate.Text))
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish
                  ? "Start time or equal to the end of time."
                  : "开始时间要不等于结束时间.");
                flg = true;
            }
            else if (ComboBoxAssister.EditValue != null && ComboBoxUser.EditValue != null)
            {
                if (ComboBoxAssister.EditValue.Equals(ComboBoxUser.EditValue))
                {
                    MessageBoxService.ShowInfo(LocalData.IsEnglish
                        ? "The staff could not be the same person."
                        : "两者不能够选择相同的人员.");
                    flg = true;
                }
            }
            return flg;
        }


    }
}
