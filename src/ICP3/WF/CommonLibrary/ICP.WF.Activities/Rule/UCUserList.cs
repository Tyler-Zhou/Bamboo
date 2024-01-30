using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.WF.ServiceInterface.DataObject;
using ICP.WF.ServiceInterface;
using DevExpress.XtraTreeList.Nodes;
using ICP.Framework.CommonLibrary;

namespace ICP.WF.Activities
{
    public partial class UCUserList : XtraUserControl
	{
		public UCUserList()
		{
			InitializeComponent();
		}

        #region 注入服务
        //服务容器提供者
        public IServiceProvider serviceProvider
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流扩展服务
        /// </summary>
        public IWorkFlowExtendService wService
        {
            get
            {
                return (IWorkFlowExtendService)serviceProvider.GetService(typeof(IWorkFlowExtendService));
            }
        }
        #endregion

        #region 字段
        /// <summary>
        /// 左边List数据源
        /// </summary>
        List<WFUserList> leftUserList = new List<WFUserList>();
        /// <summary>
        /// 右边数据源
        /// </summary>
        List<WFUserList> rightUserList = new List<WFUserList>();
        /// <summary>
        /// 对应的树节点
        /// </summary>
        public TreeListNode treeNode
        {
            get;
            set;
        }

        /// <summary>
        /// 表单数据
        /// </summary>
        public Dictionary<string,string> FormDataList
        {
            get;
            set;
        }

        WFUserList list1;
        WFUserList list2;
        #endregion

        #region 文件框输入发生改变
        /// <summary>
        /// 文件框的值发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserSearch_EditValueChanged(object sender, EventArgs e)
        {
            string userCode = txtUserSearch.Text.Trim();
            List<WFUserList> datas = leftUserList;
            if (!string.IsNullOrEmpty(userCode))
            {
                if (LocalData.IsEnglish)
                {
                    datas = leftUserList.FindAll(delegate(WFUserList data) { return data.EName.ToLower().Contains(userCode.ToLower()); });
                }
                else
                {
                    datas = leftUserList.FindAll(delegate(WFUserList data) { return data.CName.ToLower().Contains(userCode.ToLower()); });
                }
            }

            listUserLeft.DataSource = datas;
        }

        #endregion

        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCUserList_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                this.txtUserSearch.Width = gbxUser.Width - 20;
                InitControl();
            }
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            // BindLeftList();

            // BindRightList();
        }
        #endregion

        #region 按钮方法
        /// <summary>
        /// 移到右边
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToRight_Click(object sender, EventArgs e)
        {
            LeftToRight();
        }
        /// <summary>
        /// 将右边选中项移到左边
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToLeft_Click(object sender, EventArgs e)
        {
            RightToLeftList();
        }
        /// <summary>
        /// 将右边所有项移到左边
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllToLeft_Click(object sender, EventArgs e)
        {
            RightAllToLeft();
        }

        #endregion

        #region LEFT List事件
        /// <summary>
        /// 双击移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listUserLeft_DoubleClick(object sender, EventArgs e)
        {
            LeftToRight();
        }
        #endregion

        #region RIGHT List 事件
        /// <summary>
        /// 双击移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listUserRight_DoubleClick(object sender, EventArgs e)
        {
            RightToLeftList();
        }

        #endregion

        #region 方法
        /// <summary>
        /// 绑定左边列表
        /// </summary>
        public void BindLeftList()
        {
            leftUserList = new List<WFUserList>();

            if (wService == null)
            {
                return;
            }

            List<UserList> userList = wService.GetUserList(null,null,null,null,null,null,true,0);


            foreach (UserList item in userList)
            {
                WFUserList wfuser = new WFUserList();
                wfuser.ID = item.ID;
                wfuser.WFID = item.ID.ToString();
                wfuser.Code = item.Code;
                wfuser.CName = item.CName;
                wfuser.EName = item.EName;

                leftUserList.Add(wfuser);
            }

            list1 = new WFUserList();
            list1.ID = Guid.NewGuid();
            list1.WFID = WWFConstants.ProposerNameCode;
            list1.Code = WWFConstants.ProposerNameCode;
            list1.CName = WWFConstants.ProposerName_C;
            list1.EName = WWFConstants.ProposerName_E;

            list2 = new WFUserList();
            list2.ID = Guid.NewGuid();
            list2.WFID = WWFConstants.ProposerIDCode;
            list2.Code = WWFConstants.ProposerIDCode;
            list2.CName = WWFConstants.ProposerID_C;
            list2.EName = WWFConstants.ProposerID_E;

            leftUserList.Insert(0, list1);
            leftUserList.Insert(0, list2);

            //表单中的数据
            if (FormDataList != null && FormDataList.Count > 0)
            {
                foreach (string str in FormDataList.Keys)
                {
                    WFUserList wfuser = new WFUserList();
                    wfuser.ID = Guid.NewGuid();
                    wfuser.WFID = str;
                    wfuser.Code = str;
                    wfuser.CName = FormDataList[str];
                    wfuser.EName = FormDataList[str];

                    leftUserList.Insert(0,wfuser);
                }
            }

            listUserLeft.DataSource = leftUserList;
            listUserLeft.ValueMember = "WFID";
            listUserLeft.DisplayMember = LocalData.IsEnglish?"EName":"CName";

        }
        /// <summary>
        /// 绑定右边列表数据
        /// </summary>
        public void BindRightList(List<string> strList)
        {

            rightUserList = new List<WFUserList>();
            if (wService == null)
            {
                return;
            }

            foreach (string str in strList)
            {
                UserList user=null;
                if (DataTypeHelper.IsGuid(str))
                {
                    user=wService.GetUserInfo(str);
                }

                if (user == null)
                {
                    WFUserList wfuser = leftUserList.Find(delegate(WFUserList item) { return item.WFID == str; });

                    if (wfuser != null)
                    {
                        user = wfuser;
                    }
                    else
                    {
                        user = new UserList();
                        user.ID = Guid.NewGuid();
                    }
                }

                WFUserList wfuserList = new WFUserList();

                wfuserList.ID = user.ID;
                wfuserList.WFID = user.ID.ToString();
                wfuserList.Code = user.Code;
                wfuserList.CName = user.CName;
                wfuserList.EName = user.EName;


                rightUserList.Add(wfuserList);
            }

            UsersBindingSource.DataSource = rightUserList;
            listUserRight.ValueMember = "WFID";
            listUserRight.DisplayMember = LocalData.IsEnglish ? "EName" : "CName";

            UsersBindingSource.ListChanged -= new ListChangedEventHandler(UsersBindingSource_ListChanged);
            UsersBindingSource.ListChanged+=new ListChangedEventHandler(UsersBindingSource_ListChanged);
        }


        /// <summary>
        /// 从左边移到右边
        /// </summary>
        public void LeftToRight()
        {
            if (listUserLeft.SelectedValue == null)
            {
                return;
            }
            WFUserList user = this.listUserLeft.SelectedItem as WFUserList;
            if (user == null)
            {
                return;
            }
            if (rightUserList.Contains(user))
            {
                return;
            }

            rightUserList.Add(user);

            UsersBindingSource.ResetBindings(false);

            RefreshButton();
        }
        /// <summary>
        /// 从右边移到右边
        /// </summary>
        public void RightToLeftList()
        {
            if (listUserRight.SelectedValue == null)
            {
                return;
            }
            WFUserList user = this.listUserRight.SelectedItem as WFUserList;
            if (user == null)
            {
                return;
            }
            if (!rightUserList.Contains(user))
            {
                return;
            }

            rightUserList.Remove(user);

            UsersBindingSource.ResetBindings(false);

            RefreshButton();
        }
        /// <summary>
        /// 将右边的全部移到右边
        /// </summary>
        public void RightAllToLeft()
        {
            if (listUserRight.ItemCount == 0)
            {
                return;
            }

            rightUserList.Clear();

            UsersBindingSource.ResetBindings(false);

            RefreshButton();
        }
        /// <summary>
        /// 刷新按钮 
        /// </summary>
        public void RefreshButton()
        {
            this.btnToRight.Enabled = true;
            this.btnToLeft.Enabled = true;           
            this.btnAllToLeft.Enabled = true;

            if (listUserRight.ItemCount == 0)
            {
                this.btnToLeft.Enabled = false;
                this.btnAllToLeft.Enabled = false;
            }
            if (listUserLeft.ItemCount == 0)
            {
                this.btnToRight.Enabled = false ;
            }
        }
        #endregion

        #region 关联Tree数据
        /// <summary>
        /// 返回树节点的数据
        /// </summary>
        public void SetNodeData( )
        {
            CommonRule data = null;
            if (treeNode.TreeList == null)
            {
                return;
            }
            RuleList rule = treeNode.TreeList.GetDataRecordByNode(treeNode) as RuleList;
            if (rule != null)
            {
                data = rule.ComRule;
            }
            
            if (data != null)
            {
                data.RightExpress.Clear();
                foreach (WFUserList user in rightUserList)
                {
                    data.RightExpress.Add(user.WFID);
                }

                treeNode.SetValue("ComRule", data);
            }
            
        }
        /// <summary>
        /// 数据源发生改变时，绑定树数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void UsersBindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            SetNodeData();
        }

        #endregion
    }
}
