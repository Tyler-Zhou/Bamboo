using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.ReportCenter.UI.Comm.Controls
{
    public partial class SelectUserListPart : BasePart
    {
        public SelectUserListPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.listBoxLeft.DataSource = null;
                this.listBoxRight.DataSource = null;
                this.dataList = null;
                this.dsList1.DataSource = null;
                this.dsList1.Dispose();
                this.dsList2.DataSource = null;
                this.dsList2.Dispose();
                
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            
            };
        }

        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region 属性
        private List<UserList> dataList;
        /// <summary>
        /// 原始数据源
        /// </summary>
        public List<UserList> DataList
        {
            get
            {
                if (dataList == null)
                {
                    dataList = new List<UserList>();
                }
                return dataList;
            }
            set
            {
                dataList = value;
            }
        }
        /// <summary>
        /// 右数据源
        /// </summary>
        public List<UserList> RightDataList
        {
            get
            {
                List<UserList> userList = dsList2.DataSource as List<UserList>;
                if (userList == null)
                {
                    userList = new List<UserList>();
                }
                return userList;
            }
            set
            {
                dsList2.DataSource = value;
                dsList2.ResetBindings(false);
            }

        }

        /// <summary>
        /// 左数据源
        /// </summary>
        public List<UserList> LeftDataList
        {
            get
            {
                List<UserList> userList = dsList1.DataSource as List<UserList>;
                if (userList == null)
                {
                    userList = new List<UserList>();
                }
                return userList;
            }
            set
            {
                dsList1.DataSource = value;
                dsList1.ResetBindings(false);
            }
        }

        /// <summary>
        /// 右边当前选中行
        /// </summary>
        public UserList RightCurrentUser
        {
            get
            {
                 return   dsList2.Current as UserList;
            }
        }

        /// <summary>
        /// 左边当前选中行
        /// </summary>
        public UserList LeftCurrentUser
        {
            get
            {
                return dsList1.Current as UserList;
            }
        }
        /// <summary>
        /// 用户ID集合
        /// </summary>
        public List<Guid> UserIDList
        {
            get;
            set;
        }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserNames
        {
            get;
            set;
        }

        /// <summary>
        /// 已经选择了的
        /// </summary>
        public object SelectIDList
        {
            get;
            set;
        }

        #endregion

        #region 初始化
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                this.listBoxLeft.ValueMember = "ID";
                this.listBoxLeft.DisplayMember = LocalData.IsEnglish ? "EName" : "CName";

                this.listBoxRight.ValueMember = "ID";
                this.listBoxRight.DisplayMember = LocalData.IsEnglish ? "EName" : "CName";

                this.LeftDataList = DataList;
                this.RightDataList = new List<UserList>();


                List<Guid> idList = SelectIDList as List<Guid>;
                if(idList!=null&&idList.Count>0)
                {

                        List<UserList> userList = (from d in DataList
                                                   where idList.Contains(d.ID)
                                                   select d).ToList();
                        this.RightDataList = userList;
                }

            }
        }
        #endregion

        #region 窗体事件
        /// <summary>
        /// 输入查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCode_EditValueChanged(object sender, EventArgs e)
        {
            string code = this.txtCode.Text.ToLower();

            if (string.IsNullOrEmpty(code))
            {
                LeftDataList = DataList;
            }
            else
            {
                List<UserList> userList = (from d in DataList
                                           where d.CName.ToLower().Contains(code)
                                               || d.CName.ToLower().Contains(code)
                                               || d.Code.ToLower().Contains(code)
                                               || d.EName.ToLower().Contains(code)
                                           select d).ToList();
                LeftDataList = userList;
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm().DialogResult = DialogResult.Cancel;
            this.FindForm().Close();
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (LeftCurrentUser == null)
            {
                return;
            }
            int i = (from d in RightDataList where d.ID == LeftCurrentUser.ID select d).Count();
            if (i == 0)
            {
                RightDataList.Add(LeftCurrentUser);
                dsList2.ResetBindings(false);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (LeftDataList == null || LeftDataList.Count == 0)
            {
                return;
            }
            foreach (UserList user in LeftDataList)
            {
                int i = (from d in RightDataList where d.ID == user.ID select d).Count();
                if (i == 0 && !RightDataList.Contains(user))
                {
                    RightDataList.Add(user);
                }
            }
            dsList2.ResetBindings(false);
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            dsList2.DataSource = new List<UserList>();
            dsList2.ResetBindings(false);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (RightCurrentUser == null)
            {
                return;
            }

            if (RightDataList.Contains(RightCurrentUser))
            {
                RightDataList.Remove(RightCurrentUser);
                dsList2.ResetBindings(false);
            }
        }

        #endregion

        #region OK

        private void btnOK_Click(object sender, EventArgs e)
        {
            UserIDList = new List<Guid>();
            UserNames = string.Empty;

            foreach (UserList user in RightDataList)
            {
                UserIDList.Add(user.ID);
                UserNames += LocalData.IsEnglish ? user.EName : user.CName;
                UserNames += ",";
            }

            if (!string.IsNullOrEmpty(UserNames))
            {
                UserNames = UserNames.Substring(0, UserNames.Length - 1);
            }

            this.FindForm().DialogResult = DialogResult.OK;
            this.FindForm().Close();

        }
        private void listBoxLeft_DoubleClick(object sender, EventArgs e)
        {
            simpleButton1.PerformClick();
        }
        private void listBoxRight_DoubleClick(object sender, EventArgs e)
        {
            simpleButton3.PerformClick();
        }
        #endregion

     




    }
}
