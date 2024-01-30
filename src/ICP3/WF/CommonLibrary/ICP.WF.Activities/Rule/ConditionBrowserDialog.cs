using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Workflow.ComponentModel;
using ICP.WF.ServiceInterface;
using ICP.WF.ServiceInterface.DataObject;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using DevExpress.XtraBars;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 规则维护界面
    /// </summary>
    public partial class ConditionBrowserDialog : XtraForm
    {
        #region 本地变量

        /*该流程得规则集*/
        private ICPRuleSet ruleExpressionRuleSet = new ICPRuleSet();

        //当前维护的条件树 
        private ICPCondition condition = new ICPCondition();

        //服务容器提供者
        private IServiceProvider serviceProvider;

        //设置条件的活动
        private Activity _activity;

        //条件名
        private string _conditionName;

        private ITypeDescriptorContext _typeDescriptorContext;

        /// <summary>
        /// 注入WorkItem
        /// </summary>
        [Microsoft.Practices.ObjectBuilder.Dependency]
        public Microsoft.Practices.CompositeUI.WorkItem workItem { get; set; }

        private List<RuleList> treeData = new List<RuleList>();

        #endregion

        #region 初始化与释放

        public ConditionBrowserDialog()
        {
            InitializeComponent();

            if (DesignMode == false)
            {
                this.Load += new EventHandler(WFRuleSetBrowserDialog_Load);
            }
        }


        public ConditionBrowserDialog(ITypeDescriptorContext typeDescriptorContext, Activity activity, string name)
        {
            InitializeComponent();

            if (!DesignMode)
            {
                this.Load += new EventHandler(WFRuleSetBrowserDialog_Load);

                this.serviceProvider = activity.Site;
                this._conditionName = name;
                this._activity = activity;
                this._typeDescriptorContext = typeDescriptorContext;
                Activity root = WFHelpers.GetRootActivity(this._activity);
                this.ruleExpressionRuleSet = RuleHelper.LoadRulesDT(serviceProvider, root);
                if (this.ruleExpressionRuleSet != null && this.ruleExpressionRuleSet.Conditions != null && string.IsNullOrEmpty(_conditionName) == false)
                {
                    this.condition = ruleExpressionRuleSet.Conditions.Find(delegate(ICPCondition con)
                    {
                        if (string.IsNullOrEmpty(con.ConditionName) == false)
                        {
                            return con.ConditionName.Equals(_conditionName);
                        }
                        else
                        {
                            return false;
                        }
                    });
                    if (this.condition == null)
                    {
                        this.condition = new ICPCondition();
                        //如果没名，则新建规则名
                        this.condition.ConditionName = CreateNewName();
                    }
                }

                this.ucDepartment.serviceProvider = serviceProvider;
                this.ucOrganization.serviceProvider = serviceProvider;
                this.ucUserList.serviceProvider = serviceProvider;
                this.ucJobList.serviceProvider = serviceProvider;
              
            }
        }

     



        #endregion

        #region 菜单事件

        /*确定*/
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!SaveRule())
                {
                    return;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(SR.GetString("Savefailly", "保存失败") + ex.Message);
            }
        }

        /*取消*/
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

        #region 本地方法

        /*保存规则到规则文件中*/
        private bool SaveRule()
        {
            try
            {

                CommonRule rule = null;

                if (treeViewRule.Nodes.Count > 0)
                {
                    valueisNull = false;
                    //如果设置的有规则，则生成规则
                    BuildExpress(treeViewRule.Nodes[0], ref rule);
                    if (valueisNull)
                    {
                        return false;
                    }
                }
                this.condition.Rule = rule;
                if (string.IsNullOrEmpty(_conditionName))
                {
                    //如果没名，则新建规则名
                    this.condition.ConditionName = CreateNewName();
                }

                //如果不存在规则集，则生成一个新的规则集
                if (ruleExpressionRuleSet == null)
                {
                    ruleExpressionRuleSet = new ICPRuleSet();
                }

                //删掉原有规则
                if (ruleExpressionRuleSet != null && string.IsNullOrEmpty(this.condition.ConditionName) == false)
                {
                    ruleExpressionRuleSet.Conditions.RemoveAll(delegate(ICPCondition lw)
                    {
                        if (string.IsNullOrEmpty(lw.ConditionName)) return false;
                        else return lw.ConditionName.Equals(this.condition.ConditionName);
                    });
                }

                //如果有规则，则放到规则集中 
                if (rule != null)
                {
                    ruleExpressionRuleSet.Conditions.Add(this.condition);
                }
                else
                {
                    this.condition = null;
                }

                Activity root = WFHelpers.GetRootActivity(this._activity);
                if (root != null && this.ruleExpressionRuleSet != null)
                {
                    root.SetValue(DefaultSequenceActivity.ICPRuleDefinitionsProperty, ruleExpressionRuleSet);
                }

                //保存到规则文件
                RuleHelper.FlushRules(serviceProvider, root);
                return true;
            }
            catch (Exception exception)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(exception.Message);
                return false;
                throw exception;
            }
        }

        /*根据规则条件，欻建条件树*/
        private void BuildRuleTree(int parentID, CommonRule condition)
        {
            if (condition is AndRule)
            {
                AndRule rules = condition as AndRule;
                int pnID = BuildTreeNode(parentID, NodeType.And.ToString(), condition);
                foreach (CommonRule r in rules.Express)
                {
                    BuildRuleTree(pnID, r);
                }
            }
            else if (condition is OrRule)
            {
                OrRule rules = condition as OrRule;
                int pnID = BuildTreeNode(parentID, NodeType.Or.ToString(), condition);
                foreach (CommonRule r in rules.Express)
                {
                    BuildRuleTree(pnID, r);
                }
            }
            else if (condition is UserRule)
            {
                BuildTreeNode(parentID, NodeType.User.ToString(), condition);
            }
            else if (condition is DepartmentRule)
            {
                BuildTreeNode(parentID, NodeType.Department.ToString(), condition);
            }
            else if (condition is OrganizationRule)
            {
                BuildTreeNode(parentID, NodeType.Organization.ToString(), condition);
            }
            else if (condition is RoleRule)
            {
                BuildTreeNode(parentID, NodeType.Job.ToString(), condition);
            }
            else if (condition is FormExpressionRule)
            {
                BuildTreeNode(parentID, NodeType.FormExpression.ToString(), condition);
            }
        }

        /// <summary>
        /// 根据规则条件，欻建树节点
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="nodeText"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        private int BuildTreeNode(int parentID, string nodeText, CommonRule rule)
        {
            RuleList list = new RuleList();
            list.ID = treeData.Count + 1;
            list.Text = nodeText;
            list.ComRule = rule;
            list.ParentID = parentID;

            treeData.Add(list);

            treeViewRule.RefreshDataSource();

            return list.ID;
        }
        Dictionary<string, string> departmentstrings = new Dictionary<string,string>();
        Dictionary<string, string> userstrings = new Dictionary<string, string>();
        Dictionary<string, string> rolestrings = new Dictionary<string, string>();

        /// <summary>
        /// 获取主表单属性列表
        /// </summary>
        /// <returns></returns>
        private List<string> GetMainPropertyList()
        {
            List<string> ps = new List<string>();
            userstrings.Clear();
            departmentstrings.Clear();
            rolestrings.Clear();

            //初始化参数值的数据源
            DefaultSequenceActivity parentActivity = WFHelpers.GetRootActivity(_activity) as DefaultSequenceActivity;
            if (parentActivity != null)
            {
                Activity[] acs = WFHelpers.GetAllNestedActivities(parentActivity);
                foreach (Activity a in acs)
                {
                    if (a is ApplicationActivity)
                    {
                        string tname = GetTaskNameFromActivity(a);
                        DataSet ds = GetDataSetFromActivity(a);
                        if (ds != null)
                        {
                            foreach (DataTable dt in ds.Tables)
                            {
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    if (dc.DataType == typeof(string)
                                        || dc.DataType == typeof(Guid)
                                        || dc.DataType == typeof(Guid?)
                                        || dc.DataType == typeof(DateTime)
                                        || dc.DataType == typeof(DateTime?)
                                        || dc.DataType == typeof(short)
                                        || dc.DataType == typeof(short?)
                                        || dc.DataType == typeof(int)
                                        || dc.DataType == typeof(int?)
                                        || dc.DataType == typeof(Boolean?)
                                        || dc.DataType == typeof(Boolean)
                                        )
                                    {
                                        string tval = tname + "->" + dc.Caption;
                                        string tcal = tname + "->" + dc.ColumnName;
                                        if (string.IsNullOrEmpty(tval) == false && ps.Contains(tval) == false)
                                        {
                                            ps.Add(tval);
                                        }
                                        if (!string.IsNullOrEmpty(dc.ColumnName))
                                        {
                                            string columnName=dc.ColumnName.ToUpper();
                                            if (columnName.EndsWith(WWFConstants.UserID.ToUpper()) || columnName.EndsWith(WWFConstants.UserName.ToUpper()))
                                            {
                                                userstrings.Add(tcal,tval);
                                            }
                                            if (columnName.EndsWith(WWFConstants.DepartmentID.ToUpper()) || columnName.EndsWith(WWFConstants.DepartmentName.ToUpper()))
                                            {
                                                departmentstrings.Add(tcal, tval);
                                            }
                                            if (columnName.EndsWith(WWFConstants.JobID.ToUpper()) || columnName.EndsWith(WWFConstants.JobName.ToUpper()))
                                            {
                                                rolestrings.Add(tcal,tval);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return ps;
        }


        /// <summary>
        /// 获取活动的任务名称
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        private string GetTaskNameFromActivity(Activity activity)
        {
            if (activity is ApplicationActivity)
            {
                ApplicationActivity aa = activity as ApplicationActivity;
                if (aa != null)
                {
                    if (SR.IsEnglish)
                    {
                        return aa.EName;
                    }
                    else
                    {
                        return aa.CName;
                    }
                }
            }
            else if (activity is ApproveActivity)
            {
                ApproveActivity aa = activity as ApproveActivity;
                if (aa != null)
                {
                    if (SR.IsEnglish)
                    {
                        return aa.EName;
                    }
                    else
                    {
                        return aa.CName;
                    }

                }
            }

            return string.Empty;
        }



        /// <summary>
        /// 获取活动对应表单的数据源
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        private DataSet GetDataSetFromActivity(Activity activity)
        {
            DataSet ds = null;
            FormProfileInfo info = null;

            if (activity is ApplicationActivity)
            {
                ApplicationActivity aa = activity as ApplicationActivity;
                if (aa != null)
                {
                    if (aa.FormFile == null)
                    {
                        return null;
                    }
                    IWorkFlowConfigService wService = (IWorkFlowConfigService)((IServiceProvider)_typeDescriptorContext).GetService(typeof(IWorkFlowConfigService));

                    if (wService != null)
                    {
                        info = wService.GetFormProfileInfo(aa.FormFile.ToString(), LocalData.IsEnglish);
                        if (info != null)
                        {
                            ds = info.Data;
                        }
                    }



                }
            }
            else if (activity is ApproveActivity)
            {
                ApproveActivity aa = activity as ApproveActivity;
                if (aa != null)
                {
                    if (aa.FormFile == null || string.IsNullOrEmpty(aa.FormFile.ToString()))
                    {
                        return null;
                    }
                    IWorkFlowConfigService wService = (IWorkFlowConfigService)((IServiceProvider)_typeDescriptorContext.Container).GetService(typeof(IWorkFlowConfigService));
                    if (wService != null)
                    {
                        info = wService.GetFormProfileInfo(aa.FormFile.ToString(), LocalData.IsEnglish);
                        if (info != null)
                        {
                            ds = info.Data;
                        }
                    }

                }
            }

            return ds;
        }



        /// <summary>
        /// 刷新工具按的可用性
        /// </summary>
        private void RefreshToolBars()
        {
            string nodeText = string.Empty;

            if(treeViewRule.Nodes.Count == 0|| treeViewRule.FocusedNode == null|| treeViewRule.FocusedNode.GetValue("Text")==null)
            {
                 pnlMain.Enabled = false;
            }
            else
            {
                nodeText=treeViewRule.FocusedNode.GetValue("Text").ToString().ToLower();
            }

            if (string.IsNullOrEmpty(nodeText) || nodeText.Equals("and") || nodeText.Equals("or"))
            {
                pnlMain.Enabled = false;
            }
            else
            {
                pnlMain.Enabled = true;
            }

            if (treeViewRule.FocusedNode != null && (
               nodeText.Equals(NodeType.User.ToString().ToLower())
            || nodeText.Equals(NodeType.Department.ToString().ToLower())
            || nodeText.Equals(NodeType.Organization.ToString().ToLower())
            || nodeText.Equals(NodeType.Job.ToString().ToLower())
            ||nodeText.Equals(NodeType.FormExpression.ToString().ToLower())
            ))
            {
                barAddOr.Enabled = false;
                barNewAnd.Enabled = false;
                barNewNotRule.Enabled = false;
                barNewUserRule.Enabled = false;
                barNewJobRule.Enabled = false;
                barNewDepartRule.Enabled = false;
                btnNewOrganizationRule.Enabled = false;
                barNewFormExpression.Enabled = false;
            }
            else
            {
                barAddOr.Enabled = true;
                barNewAnd.Enabled = true;
                barNewNotRule.Enabled = true;
                barNewUserRule.Enabled = true;
                barNewJobRule.Enabled = true;
                barNewDepartRule.Enabled = true;
                btnNewOrganizationRule.Enabled = true;
                barNewFormExpression.Enabled = true;
            }

            barDelete.Enabled = treeViewRule.FocusedNode != null;

        }

        /// <summary>
        /// 根据条件，切换界面显示
        /// </summary>
        /// <param name="rule"></param>
        private void ChangeUIView(CommonRule rule)
        {
            if (rule == null || rule is AndRule || rule is OrRule)
            {
                this.ucDepartment.Visible = false;
                this.ucOrganization.Visible = false;
                this.ucFormRule.Visible = false;
                this.ucJobList.Visible = false;
                this.ucUserList.Visible = false;

            }
            else if (rule is DepartmentRule)
            {
                this.ucDepartment.Visible = true;
                this.ucOrganization.Visible = false;
                this.ucFormRule.Visible = false;
                this.ucJobList.Visible = false;
                this.ucUserList.Visible = false;

            }
            else if (rule is OrganizationRule)
            {
                this.ucOrganization.Visible = true;
                this.ucDepartment.Visible = false;
                this.ucFormRule.Visible = false;
                this.ucJobList.Visible = false;
                this.ucUserList.Visible = false;

            }
            else if (rule is UserRule)
            {
                this.ucUserList.Visible = true;

                this.ucDepartment.Visible = false;
                this.ucOrganization.Visible = false;
                this.ucFormRule.Visible = false;
                this.ucJobList.Visible = false;

                //if (listUserLeft.Items.Count > 0)
                //{
                //    listUserLeft.SelectedItem = listUserLeft.Items[0];
                //}
            }
            else if (rule is RoleRule)
            {
                this.ucJobList.Visible = true;

                this.ucUserList.Visible = false;
                this.ucDepartment.Visible = false;
                this.ucOrganization.Visible = false;
                this.ucFormRule.Visible = false;

                //if (listRoleLeft.Items.Count > 0)
                //{
                //    listRoleLeft.SelectedItem = listRoleLeft.Items[0];
                //}
            }
            else if (rule is FormExpressionRule)
            {
                this.ucFormRule.Visible = true;

                this.ucUserList.Visible = false;
                this.ucDepartment.Visible = false;
                this.ucOrganization.Visible = false;
                this.ucJobList.Visible = false;
            }
        }
        bool valueisNull=false;
        /// <summary>
        /// 根据树节点，生成一个规则条件树
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="parentRule"></param>
        private void BuildExpress(TreeListNode parentNode, ref CommonRule parentRule)
        {
            RuleList ruleList = null;
            CommonRule rule = null;

            if (valueisNull)
            {
                return ;
            }
            if (parentNode.GetValue("Text").ToString() == NodeType.And.ToString())
            {
                rule = new AndRule();
                foreach (TreeListNode n in parentNode.Nodes)
                {
                    BuildExpress(n, ref rule);
                }
            }
            else if (parentNode.GetValue("Text").ToString() == NodeType.Or.ToString())
            {
                rule = new OrRule();
                foreach (TreeListNode n in parentNode.Nodes)
                {
                    BuildExpress(n, ref rule);
                }
            }
            else if (parentNode.GetValue("Text").ToString() == NodeType.User.ToString())
            {
                ruleList = treeViewRule.GetDataRecordByNode(parentNode) as RuleList;
                if (ruleList != null)
                {
                    rule = ruleList.ComRule as UserRule;
                    if (rule!=null && rule.RightExpress.Count == 0)
                    {
                        this.treeViewRule.FocusedNode = parentNode;
                        valueisNull = true;
                        DevExpress.XtraEditors.XtraMessageBox.Show(SR.GetString("NoSelectUser","请选择用户"));
                        return ;
                    }
                }
            }
            else if (parentNode.GetValue("Text").ToString() == NodeType.Job.ToString())
            {
                ruleList = treeViewRule.GetDataRecordByNode(parentNode) as RuleList;
                if (ruleList != null)
                {
                    rule = ruleList.ComRule as RoleRule;
                    if (rule != null && rule.RightExpress.Count == 0)
                    {
                        this.treeViewRule.FocusedNode = parentNode;
                        valueisNull = true;
                        DevExpress.XtraEditors.XtraMessageBox.Show(SR.GetString("NoSelectJob", "请选择职位"));

                        return ;
                    }
                }
            }
            else if (parentNode.GetValue("Text").ToString() == NodeType.Department.ToString())
            {
                 ruleList = treeViewRule.GetDataRecordByNode(parentNode) as RuleList;
                 if (ruleList != null)
                 {
                     rule = ruleList.ComRule as DepartmentRule;
                     if (rule != null && rule.RightExpress.Count == 0)
                     {
                         this.treeViewRule.FocusedNode = parentNode;
                         valueisNull = true;
                         DevExpress.XtraEditors.XtraMessageBox.Show(SR.GetString("NoSelectDepartment", "请选择部门"));

                         return ;
                     }
                 }
            }
            else if (parentNode.GetValue("Text").ToString() == NodeType.Organization.ToString())
            {
                ruleList = treeViewRule.GetDataRecordByNode(parentNode) as RuleList;
                if (ruleList != null)
                {
                    rule = ruleList.ComRule as OrganizationRule;
                    if (rule != null && rule.RightExpress.Count == 0)
                    {
                        this.treeViewRule.FocusedNode = parentNode;
                        valueisNull = true;
                        DevExpress.XtraEditors.XtraMessageBox.Show(SR.GetString("NoSelectOrganization", "请选择组织结构"));

                        return;
                    }
                }
            }
            else if (parentNode.GetValue("Text").ToString() == NodeType.FormExpression.ToString())
            {
                ruleList = treeViewRule.GetDataRecordByNode(parentNode) as RuleList;
                if (ruleList != null)
                {
                    rule = ruleList.ComRule as FormExpressionRule;
                    if (rule != null && string.IsNullOrEmpty(rule.LeftExpress) && rule.Operator!="is null")
                    {
                        this.treeViewRule.FocusedNode = parentNode;
                        valueisNull = true;
                        DevExpress.XtraEditors.XtraMessageBox.Show(SR.GetString("NoSelectFormExpression", "请选择表单属性"));

                        return ;
                    }

                    if (rule != null && string.IsNullOrEmpty(rule.Operator))
                    {
                        this.treeViewRule.FocusedNode = parentNode;
                        valueisNull = true;
                        DevExpress.XtraEditors.XtraMessageBox.Show(SR.GetString("NoSelectFormOperator", "请选择表单操作符"));
                        return ;
                    }
                    if (rule != null && (rule .RightExpress.Count==0|| string.IsNullOrEmpty(rule.RightExpress[0])))
                    {
                        this.treeViewRule.FocusedNode = parentNode;
                        valueisNull = true;
                        DevExpress.XtraEditors.XtraMessageBox.Show(SR.GetString("NoSelectFormValue", "请选择表单值"));

                        return ;
                    }
               
                }

             
            }
            if (parentRule != null && (parentRule is AndRule || parentRule is OrRule))
            {
                parentRule.Express.Add(rule);
            }
            else
            {
                parentRule = rule;
            }

        }

        /*创建条件名*/
        private string CreateNewName()
        {
            string newConditionName = _activity.QualifiedName;
            return newConditionName;
        }


        #endregion

        #region 公共方法

        /// <summary>
        /// 返回当前的条件名
        /// </summary>
        public string SelectedName
        {
            get
            {
                if (condition != null) return condition.ConditionName;
                else return string.Empty;
            }
        }
        #endregion

        #region 自定义变量


        #endregion

        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void WFRuleSetBrowserDialog_Load(object sender, EventArgs e)
        {
            treeData = new List<RuleList>();

            this.treeViewRule.DataSource = treeData;
            this.treeViewRule.KeyFieldName = "ID";
            this.treeViewRule.ParentFieldName = "ParentID";

            if (condition != null)
            {
                BuildRuleTree(0, condition.Rule);
            }

            if (this.treeViewRule.Nodes.Count > 0)
            {
                this.treeViewRule.FocusedNode = this.treeViewRule.Nodes[0];
            }
            else
            {
                ChangeUIView(null);
            }


            List<string> strList = GetMainPropertyList();

            this.ucFormRule.BindComBox(strList);

            this.ucDepartment.Visible = false;
            this.ucOrganization.Visible = false;
            this.ucFormRule.Visible = false;
            this.ucJobList.Visible = false;
            this.ucUserList.Visible = false;



            this.ucDepartment.FormDataList = this.departmentstrings;
            this.ucDepartment.BindLeftTree();

            this.ucOrganization.FormDataList = this.departmentstrings;
            this.ucOrganization.BindLeftTree();


            this.ucJobList.FormDataList = this.rolestrings;
            this.ucJobList.BindLeftList();

            this.ucUserList.FormDataList = this.userstrings;
            this.ucUserList.BindLeftList();

            RefreshToolBars();

            treeViewRule.ExpandAll();

            if (_activity is SendEMailActivity || _activity is SendMessageActivity)
            {
                barNewFormExpression.Visibility = BarItemVisibility.Never;
            }

            if (treeViewRule.Nodes.Count > 0)
            {
                treeViewRule.FocusedNode = treeViewRule.Nodes[0];
            }

        }

       
        #endregion

        #region 按钮方法
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barNewAnd_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddNode(NodeType.And);
        }
        /// <summary>
        /// OR
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAddOr_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddNode(NodeType.Or);
        }
        /// <summary>
        /// Not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barNewNotRule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddNode(NodeType.Not);
        }
        /// <summary>
        /// User
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barNewUserRule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddNode(NodeType.User);
        }
        /// <summary>
        /// Job
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barNewJobRule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddNode(NodeType.Job);
        }
        /// <summary>
        /// Depart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barNewDepartRule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddNode(NodeType.Department);
        }

        /// <summary>
        /// Organization
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewOrganizationRule_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddNode(NodeType.Organization);
        }

        /// <summary>
        /// FormExpression
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barNewFormExpression_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddNode(NodeType.FormExpression);
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeViewRule.FocusedNode != null)
            {
                DeleteNode(treeViewRule.FocusedNode);
            }
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        private void DeleteNode(TreeListNode node)
        {
            if (node.ParentNode != null)
            {
                node.ParentNode.Nodes.Remove(node);
            }
            else
            {
                node.TreeList.Nodes.Remove(node);
            }
        }
        #endregion

        #region 新增节点
        /// <summary>
        /// 新增一个节点
        /// </summary>
        /// <param name="type"></param>
        private void AddNode(NodeType type)
        {
            CommonRule oldRule = null;
            TreeListNode parentNode = null;
            RuleList rule = new RuleList();

            if (this.treeViewRule.FocusedNode != null)
            {
                parentNode = this.treeViewRule.FocusedNode;
                rule = treeViewRule.GetDataRecordByNode(this.treeViewRule.FocusedNode) as RuleList;
                if(rule!=null)
                {
                    oldRule = rule.ComRule;
                }
            }

            CommonRule newRule = null;
            if (type==NodeType.And)
            {
                newRule = new AndRule();
            }
            else if (type == NodeType.Or )
            {
                newRule = new OrRule();
            }
            else if (type == NodeType.Not)
            {
                return;
                //暂没做
            }
            else if (type == NodeType.User)
            {
                newRule = new UserRule();
                newRule.LeftExpress = "id";
                newRule.Operator = "like";
            }
            else if (type == NodeType.Department)
            {
                newRule = new DepartmentRule();
                newRule.LeftExpress = "id";
                if (ucDepartment.cmbFilter.EditValue != null)
                {
                    newRule.Operator = ucDepartment.cmbFilter.EditValue.ToString();
                }
            }
            else if (type == NodeType.Organization)
            {
                newRule = new OrganizationRule();
                if (ucOrganization.icmbOP.EditValue != null)
                {
                    newRule.Operator = ucOrganization.icmbOP.EditValue.ToString();
                }
                
            }
            else if (type == NodeType.Job)
            {
                newRule = new RoleRule();
                newRule.LeftExpress = "id";
                newRule.Operator = "equal";
            }
            else if (type == NodeType.FormExpression)
            {

                newRule = new FormExpressionRule();
                newRule.LeftExpress = ucFormRule.cmbFormExpression.Text;
                newRule.Operator = ucFormRule.cmbOperator.Text;
                newRule.RightExpress.Add(ucFormRule.txtValue.Text);
            }

            if (oldRule != null && (oldRule is AndRule || oldRule is OrRule))
            {
                oldRule.Express.Add(newRule);
            }

            RuleList newList = new RuleList();
            newList.Text = type.ToString();
            newList.ComRule = newRule;
            newList.ID = treeData.Count + 1;


            if (rule != null)
            {
                newList.ParentID = rule.ID;
            }
            else
            {
                newList.ParentID = 0;
            }

            treeData.Add(newList);

            treeViewRule.RefreshDataSource();

            TreeListNode node = treeViewRule.FindNodeByFieldValue("ID", newList.ID);
            if (node != null)
            {
                treeViewRule.FocusedNode = node;
            }

            ///设置工具栏的可用性
            RefreshToolBars();
        }

        #endregion

        #region Tree事件
        /// <summary>
        /// 选择的节点发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewRule_AfterFocusNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (this.treeViewRule.FocusedNode==null)
            {
                this.ucDepartment.OrgsBindingSource.Clear();
                this.ucOrganization.OrgsBindingSource.Clear();
                this.ucJobList.JobsBindingSource.Clear();
                this.ucUserList.UsersBindingSource.Clear();

                RefreshToolBars();

                return;
            }

            CommonRule data = null;
            TreeListNode parentNode = null;
            RuleList rule = new RuleList();

            if (this.treeViewRule.FocusedNode != null)
            {
                parentNode = this.treeViewRule.FocusedNode;
                rule = treeViewRule.GetDataRecordByNode(this.treeViewRule.FocusedNode) as RuleList;
                if (rule != null)
                {
                    data = rule.ComRule;
                }
            }
            else
            {
                return;
            }

            if (data != null)
            {
                if (data is DepartmentRule)
                {
                    ucDepartment.treeNode = this.treeViewRule.FocusedNode;
                    ucDepartment.BindRightTree(data.RightExpress);
                    ucDepartment.cmbFilter.EditValue = data.Operator;

                }
                if (data is OrganizationRule)
                {
                    ucOrganization.treeNode = this.treeViewRule.FocusedNode;

                    ucOrganization.BindRightTree(data.RightExpress);
                    ucOrganization.SetCmbFilterValue(data.LeftExpress);                 
                    ucOrganization.icmbOP.EditValue = data.Operator;
                }
                else if (data is FormExpressionRule)
                {
                    ucDepartment.treeNode = this.treeViewRule.FocusedNode;
                    this.ucFormRule.cmbOperator.Text = data.Operator;
                    this.ucFormRule.txtValue.Text = (data.RightExpress == null || data.RightExpress.Count == 0) ? string.Empty : data.RightExpress[0];


                }
                else if (data is UserRule)
                {
                    this.ucUserList.treeNode = this.treeViewRule.FocusedNode;
                    this.ucUserList.BindRightList(data.RightExpress);
                }
                else if (data is RoleRule)
                {
                    this.ucJobList.treeNode = this.treeViewRule.FocusedNode;
                    this.ucJobList.BindRightList(data.RightExpress);
                }
            }
            else
            {
                this.ucDepartment.OrgsBindingSource.Clear();
                this.ucOrganization.OrgsBindingSource.Clear();
                this.ucJobList.JobsBindingSource.Clear();
                this.ucUserList.UsersBindingSource.Clear(); 

            }

            ChangeUIView(data);

            RefreshToolBars();
        }
        /// <summary>
        /// 选中节点的颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewRule_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            this.treeViewRule.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.treeViewRule.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;

        }
        #endregion

        #region 用户控件的操作符发生改变时
        /// <summary>
        /// 部门的操作符发生改变
        /// </summary>
        /// <param name="text"></param>
        private void ucDepartment_GetRelationsText(string text)
        {
            if (this.treeViewRule.FocusedNode != null)
            {
                RuleList rule = treeViewRule.GetDataRecordByNode(this.treeViewRule.FocusedNode) as RuleList;
                CommonRule data=null;
                if (rule != null)
                {
                    data = rule.ComRule;
                    if (data != null&& data is DepartmentRule)
                    {
                        rule.ComRule.Operator = text;
                    }
                }
            }
        }

        /// <summary>
        /// 组织结构操作符发送改变
        /// </summary>
        /// <param name="text"></param>
        private void ucOrganization_GetOperatorText(string text)
        {
            if (this.treeViewRule.FocusedNode != null)
            {
                RuleList rule = treeViewRule.GetDataRecordByNode(this.treeViewRule.FocusedNode) as RuleList;
                CommonRule data = null;
                if (rule != null)
                {
                    data = rule.ComRule;
                    if (data != null && data is OrganizationRule)
                    {
                        rule.ComRule.Operator = text;
                    }
                }
            }
        }

        void ucOrganization_GetFormExpressionText(string text)
        {
            if (this.treeViewRule.FocusedNode != null)
            {
                RuleList rule = treeViewRule.GetDataRecordByNode(this.treeViewRule.FocusedNode) as RuleList;
                CommonRule data = null;
                if (rule != null)
                {
                    data = rule.ComRule;
                    if (data != null && data is OrganizationRule)
                    {
                        rule.ComRule.LeftExpress = text;
                    }
                }
            }
        }

        /// <summary>
        /// 表达操作表达式
        /// </summary>
        /// <param name="text"></param>
        private void ucFormRule_GetOperatorText(string text)
        {
            if (this.treeViewRule.FocusedNode != null)
            {
                RuleList rule = treeViewRule.GetDataRecordByNode(this.treeViewRule.FocusedNode) as RuleList;
                CommonRule data = null;
                if (rule != null)
                {
                    data = rule.ComRule;
                    if (data != null && data is FormExpressionRule)
                    {
                        rule.ComRule.Operator = text;
                    }
                }
            }
        }
        /// <summary>
        /// 表单属性
        /// </summary>
        /// <param name="text"></param>
        private void ucFormRule_GetFormExpressionText(string text)
        {
            if (this.treeViewRule.FocusedNode != null)
            {
                RuleList rule = treeViewRule.GetDataRecordByNode(this.treeViewRule.FocusedNode) as RuleList;
                CommonRule data = null;
                if (rule != null)
                {
                    data = rule.ComRule;
                    if (data != null && data is FormExpressionRule)
                    {
                        rule.ComRule.LeftExpress = text;
                    }
                }
            }
        }
        /// <summary>
        /// 表单值
        /// </summary>
        /// <param name="text"></param>
        private void ucFormRule_GetValueText(string text)
        {
            if (this.treeViewRule.FocusedNode != null)
            {
                RuleList rule = treeViewRule.GetDataRecordByNode(this.treeViewRule.FocusedNode) as RuleList;
                CommonRule data = null;
                if (rule != null)
                {
                    data = rule.ComRule;
                    if (data != null && data is FormExpressionRule)
                    {
                        rule.ComRule.RightExpress.Clear();
                        rule.ComRule.RightExpress.Add(text);
                    }
                }
            }
        }
        #endregion

    }

    public class RuleList
    {
        /// <summary>
        /// 节点代码
        /// </summary>
        public int ID
        {
            get;
            set;
        }
        /// <summary>
        /// 父节点
        /// </summary>
        public int ParentID
        {
            set;
            get;
        }
        /// <summary>
        /// 节点显示值
        /// </summary>
        public string Text
        {
            get;
            set;
        }
        /// <summary>
        /// 节点类型属性
        /// </summary>
        public CommonRule ComRule
        {
            get;
            set;
        }

    }
}
