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
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary;

namespace ICP.Sys.UI.WorkSpaceView
{
    public partial class WorkSpaceViewUserPart : BaseListPart
    {
        public WorkSpaceViewUserPart()
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
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        #endregion

        #region 属性
        public Guid WorkSpaceID
        {
            get;
            set;
        }
        public UserList UserItem
        {
            get
            {
                return bsUsreList.Current as UserList;
            }
        }
        public UserList CurrentItem
        {
            get
            {
                return bsSelectList.Current as UserList;
            }
        }
        public List<UserList> SelectDataList
        {
            get
            {
                return bsSelectList.DataSource as List<UserList>;
            }
        }
        List<UserList> SelectedItem
        {
            get
            {
                int[] rowIndexs = gvList.GetSelectedRows();
                if (rowIndexs.Length == 0) return null;
                List<UserList> tagers = new List<UserList>();
                foreach (var item in rowIndexs)
                {
                    UserList dr = gvList.GetRow(item) as UserList;
                    if (dr != null) tagers.Add(dr);
                }
                return tagers;
            }
        }

        private List<UserList> DataList
        {
            get
            {
                return bsUsreList.DataSource as List<UserList>; 
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
                Utility.ShowGridRowNo(this.gvList);
            }
        }
        public void InitControls()
        {
             DataFindClientService.RegisterMiniFinder(stxtOrganization, SystemFinderConstants.OrganizationFinder, SearchFieldConstants.CodeName
                                  , LocalData.IsEnglish ? "EShortName" : "CShortName", "ID", new string[] { "ID", "EShortName", "CShortName" },
                        delegate(object inputSource, object[] resultData)
                        {
                            stxtOrganization.Text = LocalData.IsEnglish ? resultData[1].ToString() : resultData[2].ToString();
                            stxtOrganization.Tag = new Guid(resultData[0].ToString());
                        }, null);


             DataFindClientService.RegisterMiniFinder(stxtJob, SystemFinderConstants.JobFinder, SearchFieldConstants.CodeName
                                          , LocalData.IsEnglish ? "EName" : "CName", "ID", new string[] { "ID", "EName", "CName" },
                                delegate(object inputSource, object[] resultData)
                                {
                                    stxtJob.Text = LocalData.IsEnglish ? resultData[1].ToString() : resultData[2].ToString();
                                    stxtJob.Tag = new Guid(resultData[0].ToString());
                                }, null);

             this.txtCode.KeyDown += KeyEnterSearch;
             this.txtName.KeyDown += KeyEnterSearch;
        }

        void KeyEnterSearch(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchUserList();
            }
        }
        #endregion

        #region  绑定数据
        public void BindData()
        {
            List<UserList> list=PermissionService.GetWorkSpaceUserList(WorkSpaceID);
            bsSelectList.DataSource = list;
            bsSelectList.ResetBindings(false);
        }

        #endregion

        #region UserList
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchUserList();
        }
        private void SearchUserList()
        {
            List<UserList> userList = UserService.GetUserListByList(
                                               this.txtCode.Text,
                                               this.txtName.Text,
                                               null,
                                               DataTypeHelper.GetGuidNull(stxtJob.Tag),
                                               null,
                                               DataTypeHelper.GetGuidNull(stxtOrganization.Tag),
                                               null,
                                               true,
                                               0);

            bsUsreList.DataSource = userList;
            bsUsreList.ResetBindings(false);
        }
        /// <summary>
        /// 双击选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvList_DoubleClick(object sender, EventArgs e)
        {
            if (UserItem == null)
            {
                return;
            }
            int count = (from d in SelectDataList where d.ID==UserItem.ID  select d.ID).Count();// SelectDataList.Find(delegate(UserList item) { return item.ID == UserItem.ID; });
            if (count==0)
            {
                bsSelectList.Insert(0, UserItem);
                bsSelectList.ResetBindings(false);
            }
        }

        #endregion

        #region Select List
        /// <summary>
        /// 双击移除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteData();
        }
        private void gvDetails_DoubleClick(object sender, EventArgs e)
        {
            DeleteData();
        }
        private void DeleteData()
        {
            if (bsSelectList.Current != null)
            {
                bsSelectList.RemoveCurrent();
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<Guid> selectIDs = (from d in SelectDataList select d.ID).ToList();
            PermissionService.SaveWorkSpaceUserList(WorkSpaceID,selectIDs.ToArray(),LocalData.UserInfo.LoginID);

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(),"保存成功!");
        }
        #endregion

        #region 全部移动
        private void btnAll_Click(object sender, EventArgs e)
        {
            foreach (UserList UserItem in DataList)
            {
                int count = (from d in SelectDataList where d.ID == UserItem.ID select d.ID).Count();// SelectDataList.Find(delegate(UserList item) { return item.ID == UserItem.ID; });
                if (count == 0)
                {
                    bsSelectList.Insert(0, UserItem);
                    bsSelectList.ResetBindings(false);
                }
            }
        }
        #endregion


    }
}
