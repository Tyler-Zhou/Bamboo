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
    public partial class UCDepartment : XtraUserControl
	{
		public UCDepartment()
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


        public List<WFOrganizationList> RightOrgList = new List<WFOrganizationList>();
        public List<WFOrganizationList> LeftOrgList = new List<WFOrganizationList>();

        /// <summary>
        /// 对应的树节点
        /// </summary>
        public TreeListNode treeNode
        {
            get;
            set;
        }

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
            InitTree();
        }
        /// <summary>
        /// 初始化下拉框
        /// </summary>
        private void InitComBox()
        {
            if (!LocalData.IsEnglish)
            {
                this.cmbFilter.Properties.Items.Add(new ImageComboBoxItem(WWFConstants.DepartmentEqual_C, WWFConstants.DepartmentEqual));
                this.cmbFilter.Properties.Items.Add(new ImageComboBoxItem(WWFConstants.DepartmentLike_C, WWFConstants.DepartmentLike));

                this.cmbFilter.Properties.Items.Add(new ImageComboBoxItem(WWFConstants.CompanyEqual_C, WWFConstants.CompanyEqual));
                this.cmbFilter.Properties.Items.Add(new ImageComboBoxItem(WWFConstants.CompanyLike_C, WWFConstants.CompanyLike));

                this.cmbFilter.Properties.Items.Add(new ImageComboBoxItem(WWFConstants.SectionEqual_C, WWFConstants.SectionEqual));
                this.cmbFilter.Properties.Items.Add(new ImageComboBoxItem(WWFConstants.SectionLike_C, WWFConstants.SectionLike));
            }
            else
            {
                this.cmbFilter.Properties.Items.Add(new ImageComboBoxItem(WWFConstants.DepartmentEqual_E, WWFConstants.DepartmentEqual));
                this.cmbFilter.Properties.Items.Add(new ImageComboBoxItem(WWFConstants.DepartmentLike_E, WWFConstants.DepartmentLike));

                this.cmbFilter.Properties.Items.Add(new ImageComboBoxItem(WWFConstants.CompanyEqual_E, WWFConstants.CompanyEqual));
                this.cmbFilter.Properties.Items.Add(new ImageComboBoxItem(WWFConstants.CompanyLike_E, WWFConstants.CompanyLike));

                this.cmbFilter.Properties.Items.Add(new ImageComboBoxItem(WWFConstants.SectionEqual_E, WWFConstants.SectionEqual));
                this.cmbFilter.Properties.Items.Add(new ImageComboBoxItem(WWFConstants.SectionLike_E, WWFConstants.SectionLike));

            }
            this.cmbFilter.SelectedIndex = 0;
        }
        /// <summary>
        /// 初始化列表树
        /// </summary>
        private void InitTree()
        {

            //BindLeftTree();

           // BindRightTree();

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
            RightOrgList = new List<WFOrganizationList>();

            if (wService == null)
            {
                return;
            }
            List<OrganizationList> orgList = wService.GetOrganizationList(null, null, true, 0);

            WFOrganizationList list1 = new WFOrganizationList();
            list1.ID = Guid.NewGuid();
            list1.WFID = WWFConstants.ProposerDepartmentIDCode;
            list1.Code = WWFConstants.ProposerDepartmentIDCode;
            list1.CShortName = WWFConstants.ProposerDepartmentName_C;
            list1.EShortName = WWFConstants.ProposerDepartmentName_E;
            list1.ParentID = null;
            list1.WFParentID = null;

         

            WFOrganizationList list2 = new WFOrganizationList();
            list2.ID = Guid.NewGuid();
            list2.WFID = WWFConstants.ProposerCompanyIDCode;
            list2.Code = WWFConstants.ProposerCompanyIDCode;
            list2.CShortName = WWFConstants.ProposerCompanyName_C;
            list2.EShortName = WWFConstants.ProposerCompanyName_E;
            list2.ParentID = null;
            list2.WFParentID = null;


            LeftOrgList.Insert(0, list1);
            LeftOrgList.Insert(0, list2);


            if (FormDataList == null)
            {
                FormDataList =new Dictionary<string, string>();
            }
            if (FormDataList != null && FormDataList.Count > 0)
            {
                //表单中的部门数据
                foreach (string str in FormDataList.Keys)
                {
                    WFOrganizationList org = new WFOrganizationList();
                    org.ID = Guid.NewGuid();
                    org.WFID = str;
                    org.Code = str;
                    org.CShortName = FormDataList[str];
                    org.EShortName = FormDataList[str];
                    org.ParentID = null;
                    org.WFParentID = null;

                    LeftOrgList.Insert(0, org);
                }
            }

            foreach (OrganizationList item in orgList)
            {
                WFOrganizationList org = new WFOrganizationList();
                org.ID = item.ID;
                org.WFID = item.ID.ToString();
                org.Code = item.Code;
                org.CShortName = item.CShortName;
                org.EShortName = item.EShortName;
                org.ParentID = item.ParentID;
                org.WFParentID = item.ParentID==null?null:item.ParentID.ToString();

                LeftOrgList.Add(org);
            }

            this.treeDptLeft.KeyFieldName = "WFID";
            this.treeDptLeft.ParentFieldName = "WFParentID";
            tcLeftOrgName.FieldName = LocalData.IsEnglish ? "EShortName" : "CShortName";
            this.treeDptLeft.DataSource = LeftOrgList;

        }
        /// <summary>
        /// 绑定右边列表数据
        /// </summary>
        public void BindRightTree(List<string> strList)
        {
            RightOrgList.Clear();
            foreach(string str in strList)
            {
                OrganizationList org=null;

                if (DataTypeHelper.IsGuid(str))
                {
                    org = wService.GetOrganizationInfo(str);
                }
                if (org == null)
                {
                    WFOrganizationList wforg = LeftOrgList.Find(delegate(WFOrganizationList item) { return item.WFID == str; });

                    if (wforg != null)
                    {
                        org = wforg;
                    }
                    else
                    {
                        org = new OrganizationList();
                        org.ID = Guid.NewGuid();
                    }
                }

                WFOrganizationList wfItem = new WFOrganizationList();
                wfItem.ID = org.ID;
                wfItem.WFID = org.ID.ToString();
                wfItem.Code = org.Code;
                wfItem.CShortName = org.CShortName;
                wfItem.EShortName = org.EShortName;
                wfItem.ParentID = org.ParentID;
                wfItem.WFParentID =org.ParentID==null?null:org.ParentID.ToString();

                RightOrgList.Add(wfItem);

            }

            OrgsBindingSource.DataSource=RightOrgList;


            this.treeDptRight.KeyFieldName = "WFID";
            this.treeDptRight.ParentFieldName = "WFParentID";
            tcRightOrgName.FieldName = LocalData.IsEnglish ? "EShortName" : "CShortName";
            tcRightOrgName.Caption=tcLeftOrgName.Caption = LocalData.IsEnglish ? "Name" : "名称";

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
            WFOrganizationList data = treeDptLeft.GetDataRecordByNode(this.treeDptLeft.FocusedNode) as WFOrganizationList;
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
            WFOrganizationList data = treeDptRight.GetDataRecordByNode(this.treeDptRight.FocusedNode) as WFOrganizationList;
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
            //需要定义委托，将下拉框中的值传到主界面的树控件中去。
            if (GetRelationsText != null)
            {
                if (cmbFilter.EditValue != null)
                {
                    GetRelationsText(cmbFilter.EditValue.ToString());
                }
                else
                {
                    GetRelationsText(string.Empty);
                }
            }

        }
        /// <summary>
        /// 获得下拉框中的值
        /// </summary>.
        [Description("条件发生改变时，获得条件")]
        public event RelationsTextChang GetRelationsText;
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
                foreach (WFOrganizationList list in RightOrgList)
                {
                    data.RightExpress.Add(list.WFID);
                }

                treeNode.SetValue("ComRule", data);
            }
        }

   



    }
    public delegate void RelationsTextChang(string text);

}
