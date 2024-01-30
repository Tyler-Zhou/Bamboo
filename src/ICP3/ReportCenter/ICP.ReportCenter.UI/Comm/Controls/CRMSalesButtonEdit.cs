using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface.DataObjects;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;

namespace ICP.ReportCenter.UI.Comm.Controls
{  
    /// <summary>
    /// CRM业务员选择框
    /// </summary>
    public class CRMSalesButtonEdit : DevExpress.XtraEditors.ButtonEdit
    {   
        /// <summary>
        /// 
        /// </summary>
        private WorkItem WorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }
       private List<UserList> UserDataList;
        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (!LocalData.IsDesignMode)
            {
                UserDataList = Utility.GetSubordinateUserList();
                if (UserDataList == null || UserDataList.Count == 0 || UserDataList.Count == 1)
                {
                    this.Enabled = false;
                    List<Guid> idList = new List<Guid>();
                    idList.Add(LocalData.UserInfo.LoginID);
                    this.Tag = idList;
                    this.Text = LocalData.UserInfo.UserName;
                }
                else
                {
                    this.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(CRMSalesButtonEdit_ButtonClick);
                    this.KeyDown += new KeyEventHandler(CRMSalesButtonEdit_KeyDown);
                    this.KeyPress += new KeyPressEventHandler(CRMSalesButtonEdit_KeyPress);
                    this.Disposed += delegate
                    {
                        this.ButtonClick -= new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(CRMSalesButtonEdit_ButtonClick);
                        this.KeyDown -= new KeyEventHandler(CRMSalesButtonEdit_KeyDown);
                        this.KeyPress -= new KeyPressEventHandler(CRMSalesButtonEdit_KeyPress);
                    };
                }
            }
        }

        void CRMSalesButtonEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        void CRMSalesButtonEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ShowSelectUser();
            }
        }

        void CRMSalesButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ShowSelectUser();
        }
        /// <summary>
        /// 查询用户
        /// </summary>
        private void ShowSelectUser()
        {
            SelectUserListPart selectUser = this.WorkItem.Items.AddNew<SelectUserListPart>();
            string title = LocalData.IsEnglish ? "Select User" : "选择用户";

            selectUser.DataList = UserDataList;
            selectUser.SelectIDList = this.Tag;

            if (ICP.Framework.ClientComponents.Controls.PartLoader.ShowDialog(selectUser, title) == DialogResult.OK)
            {
                this.Tag = selectUser.UserIDList;
                this.Text = selectUser.UserNames;
            }
        }
    }
}
