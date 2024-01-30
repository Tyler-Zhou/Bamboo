using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Sys.ServiceInterface;

using ICP.Sys.ServiceInterface.DataObjects;
using ICP.WF.ServiceInterface.DataObject;
using ICP.Framework.CommonLibrary.Client;
using ICP.WF.ServiceInterface;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary;

namespace ICP.WF.Activities
{
    public partial class UCOrganization : XtraUserControl
	{
        public UCOrganization()
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

        #region 自定义变量

        /// <summary>
        /// 表单数据
        /// </summary>
        public Dictionary<string,string> FormDataList
        {
            get;
            set;
        }

        /// <summary>
        /// 对应的树节点
        /// </summary>
        public TreeListNode treeNode
        {
            get;
            set;
        }
        List<OrganizationList> RightOrgList = new List<OrganizationList>();
        #endregion

        #region 初始化控件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                InitControl();
            }

        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            InitComBox();
        }
        /// <summary>
        /// 初始化下拉框
        /// </summary>
        private void InitComBox()
        {
            if (!LocalData.IsEnglish)
            {
                this.icmbOP.Properties.Items.Add(new ImageComboBoxItem(WWFConstants.CLike, WWFConstants.ELike));
                this.icmbOP.Properties.Items.Add(new ImageComboBoxItem(WWFConstants.CNotLike, WWFConstants.ENotLike));
            }
            else
            {
                this.icmbOP.Properties.Items.Add(new ImageComboBoxItem(WWFConstants.ELike, WWFConstants.ELike));
                this.icmbOP.Properties.Items.Add(new ImageComboBoxItem(WWFConstants.ENotLike, WWFConstants.ENotLike));
            }
            //this.cmbFilter.SelectedIndex = 0;
            this.icmbOP.SelectedIndex = 0;
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
        /// 将选中移到左边
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToLeft_Click(object sender, EventArgs e)
        {
            RightToLeftList();
        }
        /// <summary>
        /// 全部移动到左边
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllToLeft_Click(object sender, EventArgs e)
        {
            RightAllToLeft();
        }
        #endregion

        #region Left List 事件
        /// <summary>
        /// 双击选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeDptLeft_DoubleClick(object sender, EventArgs e)
        {
            LeftToRight();
        }
        /// <summary>
        /// 当前行的颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeDptLeft_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            this.treeDptLeft.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.treeDptLeft.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;

        }
        #endregion

        #region Right List 事件
        /// <summary>
        /// 双击选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeDptRight_DoubleClick(object sender, EventArgs e)
        {
            RightToLeftList();
        }
        /// <summary>
        /// 当前行的颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeDptRight_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            this.treeDptRight.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.treeDptRight.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
        }

        #endregion


        #region 方法
        /// <summary>
        /// 绑定左边列表
        /// </summary>
        public void BindLeftTree()
        {

            if (wService == null)
            {
                return;
            }
            List<OrganizationList> orgList = wService.GetOrganizationList(null, null, true, 0);

            this.treeDptLeft.KeyFieldName = "ID";
            this.treeDptLeft.ParentFieldName = "ParentID";
            tcLeftOrgName.FieldName = LocalData.IsEnglish ? "EShortName" : "CShortName";
            this.treeDptLeft.DataSource = orgList;

            foreach(KeyValuePair<string,string> keys in FormDataList)
            {
                //this.cmbFilter.Properties.Items.Add(new ImageComboBoxItem(keys.Value, keys.Key));
                this.cmbFilter.Properties.Items.Add(new ImageComboBoxItem(keys.Value));
            }
        }
        public void SetCmbFilterValue(string key)
        {
            this.cmbFilter.EditValue = key;
        }

        /// <summary>
        /// 绑定右边列表数据
        /// </summary>
        public void BindRightTree(List<string> strList)
        {
            foreach (string str in strList)
            {
                OrganizationList org = null;

                if (DataTypeHelper.IsGuid(str))
                {
                    org = wService.GetOrganizationInfo(str);
                }
                if (org == null)
                {
                    org = new OrganizationList();
                    org.ID = Guid.NewGuid();  
                }

                RightOrgList.Add(org);
            }

            OrgsBindingSource.DataSource = RightOrgList;


            this.treeDptRight.KeyFieldName = "ID";
            this.treeDptRight.ParentFieldName = "ParentID";
            tcRightOrgName.FieldName = LocalData.IsEnglish ? "EShortName" : "CShortName";
            tcRightOrgName.Caption = tcLeftOrgName.Caption = LocalData.IsEnglish ? "Name" : "名称";

            treeDptRight.ExpandAll();
        }


        /// <summary>
        /// 从左边移到右边
        /// </summary>
        public void LeftToRight()
        {
            if (treeDptLeft.FocusedNode == null)
            {
                return;
            }
            OrganizationList data = treeDptLeft.GetDataRecordByNode(this.treeDptLeft.FocusedNode) as OrganizationList;
            if (data == null)
            {
                return;
            }

            if (RightOrgList.Contains(data))
            {
                return;
            }
            else
            {
                RightOrgList.Add(data);
            }

            treeDptRight.RefreshDataSource();

            SetRule();
            treeDptRight.ExpandAll();
            RefreshButton();
        }
        /// <summary>
        /// 从右边移到左边
        /// </summary>
        public void RightToLeftList()
        {

            if (treeDptRight.FocusedNode == null)
            {
                return;
            }
            OrganizationList data = treeDptRight.GetDataRecordByNode(this.treeDptRight.FocusedNode) as OrganizationList;
            if (data == null)
            {
                return;
            }
            if (RightOrgList.Contains(data))
            {
                RightOrgList.Remove(data);
            }
            treeDptRight.RefreshDataSource();
            SetRule();
            RefreshButton();
        }
        /// <summary>
        /// 将右边的全部移到右边
        /// </summary>
        public void RightAllToLeft()
        {
            if (treeDptRight.Nodes.Count == 0)
            {
                return;
            }

            RightOrgList.Clear();
            treeDptRight.RefreshDataSource();
            SetRule();
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

            if (treeDptRight.Nodes.Count == 0)
            {
                this.btnToLeft.Enabled = false;
                this.btnAllToLeft.Enabled = false;
            }
            if (treeDptLeft.Nodes.Count == 0)
            {
                this.btnToRight.Enabled = false;
            }
        }
        #endregion

        #region 下拉框方法
        /// <summary>
        /// 选择的值发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageComboBoxEdit1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (GetFormExpressionText != null)
            {
                GetFormExpressionText(cmbFilter.Text);
            }

        }

        private void icmbOP_SelectedValueChanged(object sender, EventArgs e)
        {
            if (GetOperatorText != null)
            {
                if (icmbOP.EditValue != null)
                    GetOperatorText(icmbOP.EditValue.ToString());
                else
                    GetOperatorText(string.Empty);
            }
        }
        /// <summary>
        /// 获得表单属性的值
        /// </summary>.
        [Description("获得值文本框中的Text")]
        public event FormExpressionTextChang GetFormExpressionText;

        /// <summary>
        /// 获得"表达式"下拉框中的值
        /// </summary>.
        [Description("条件发生改变时，获得条件")]
        public event OperatorTextChang GetOperatorText;

        #endregion

        /// <summary>
        /// 设置节点对应的数据
        /// </summary>
        public void SetRule()
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
                foreach (OrganizationList list in RightOrgList)
                {
                    data.RightExpress.Add(list.ID.ToString());
                }

                treeNode.SetValue("ComRule", data);
            }
        }


        #region 委托

        public delegate void OperatorTextChang(string text);
        public delegate void FormExpressionTextChang(string text);

        #endregion
    }
}
