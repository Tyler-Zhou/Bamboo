using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.OA.ServiceInterface.DataObjects;
using ICP.OA.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using ICP.Framework.CommonLibrary;

namespace ICP.OA.UI.Bulletin
{
    [ToolboxItem(false)]
    public partial class BulletinSearchPart : BaseSearchPart
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public IBulletinService BulletinService
        {
            get
            {
                return ServiceClient.GetService<IBulletinService>();
            }
        }

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        public BulletinSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
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

            Utility.SearchPartKeyEnterToSearch(new List<Control> 
            {
                txtSubject,txtBody ,txtDepartment 
            }, this.btnSearch);


            List<UserList> userList = UserService.GetUserListByList(null, null, null, null, null, null, true, true, 0);

            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add("CName", "Name");
            col.Add("Code", "Code");
            cmbPublisher.InitSource<UserList>(userList, col, "EName", "ID");

            //cmbPublisher.InitSource<UserList>(userList, col, LocalData.IsEnglish ? "EName" : "CName", "Code");
            this.cmbPublisher.ShowSelectedValue(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName);

            this.radioGroup1.SelectedIndex = 3;
            this.dteFrom.DateTime = DateTime.Now.AddMonths(-3);
            this.dteTo.DateTime = DateTime.Now;
        }

        #region event
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (OnSearched != null)
                OnSearched(this, GetData());
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            txtBody.Text = txtDepartment.Text = txtSubject.Text = string.Empty;
            radioGroup1.SelectedIndex = 0;
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == (int)DateType.Specify)
            {
                dteFrom.Enabled = dteTo.Enabled = true;
                dteFrom.DateTime = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).AddDays(-7);
                dteTo.DateTime = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).Date;
            }
            else
                dteFrom.Enabled = dteTo.Enabled = false;
        }
        enum DateType
        {
            Unknown,
            LastMonth,
            ThisMonth,
            Specify
        }

        #endregion

        #region ISearchPart 成员

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? From
        {
            get
            {
                if (radioGroup1.SelectedIndex == (int)DateType.Unknown)
                {
                    return null;
                }
                else if (radioGroup1.SelectedIndex == (int)DateType.LastMonth)
                {
                    DateTime dt = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).AddMonths(-1);
                    return new DateTime(dt.Year, dt.Month, 1);
                }
                else if (radioGroup1.SelectedIndex == (int)DateType.ThisMonth)
                {
                    return new DateTime(DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).Year, DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).Month, 1);
                }
                else
                {
                    return dteFrom.DateTime.Date;
                }
            }
        }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? To
        {
            get
            {
                if (radioGroup1.SelectedIndex == (int)DateType.Unknown)
                {
                    return null;
                }
                else if (radioGroup1.SelectedIndex == (int)DateType.LastMonth)
                {
                    DateTime dt = Utility.GetEndDate(new DateTime(DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).Year, DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).Month, 1));//本月月头
                    return dt.AddDays(-1);
                }
                else if (radioGroup1.SelectedIndex == (int)DateType.ThisMonth)
                {
                    DateTime dt = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).AddMonths(1);//下月月头
                    dt = Utility.GetEndDate(new DateTime(dt.Year, dt.Month, 1));
                    return dt.AddDays(-1);
                }
                else
                {
                    return Utility.GetEndDate(dteTo.DateTime);
                }
            }
        }

        public override object GetData()
        {
            try
            {
                Guid? userID = null;
                if(this.cmbPublisher.EditValue!=null)
                {
                    userID=DataTypeHelper.GetGuid(this.cmbPublisher.EditValue,Guid.Empty);
                }

                List<BulletinData> list = BulletinService.GetBulletins(userID
                                    ,txtSubject.Text.Trim()
                                    , txtBody.Text.Trim()
                                    , txtDepartment.Text.Trim()
                                    , From
                                    , To
                                    , 100);
                return list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); return null; }

        }
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            this.btnSearch.PerformClick();
        }

        #endregion

    }
}
