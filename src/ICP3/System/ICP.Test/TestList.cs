using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface;
using DevExpress.XtraTreeList.Nodes;
using System.Threading;
using ICP.Framework.CommonLibrary;

namespace ICP.Test
{
    public partial class TestList : DevExpress.XtraEditors.XtraUserControl, IAutoTestServiceInterface
    {
        public TestList()
        {
            InitializeComponent();
        }

        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public ISystemService SystemService
        {
            get
            {
                return ServiceClient.GetService<ISystemService>();
            }
        }

   
        #endregion

        #region 私有

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                BindTreeData();
            }
        }

        public void BindTreeData()
        {
            DataSet ds = SystemService.GetDataSet("select * From sm.TestMethods where IsValid=1");
            List<TestMethodList> list = (from d in ds.Tables[0].AsEnumerable()
                                       select new TestMethodList {
                                           ID = d.Field<Guid>("ID"),
                                           Type = d.Field<int>("Type"),
                                           MethodName = d.Field<string>("MethodName"),
                                           MethodDescription = d.Field<string>("MethodDescription"),
                                           ParentID = d.Field<Guid>("ParentID")
                                       }).ToList();

            this.treeList1.DataSource = list;
        }

        private void barStart_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void treeList1_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            SetCheckedChildNodes(e.Node, e.Node.CheckState);
            SetCheckedParentNodes(e.Node, e.Node.CheckState);
        }
        /*如果选择父节点，同时也选择所有子节点*/
        private void SetCheckedChildNodes(TreeListNode node, CheckState check)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].CheckState = check;
                SetCheckedChildNodes(node.Nodes[i], check);
            }
        }
        /*如果所有的子节点都选择了，则父节点也选择*/
        private void SetCheckedParentNodes(TreeListNode node, CheckState check)
        {
            if (node.ParentNode != null)
            {
                bool b = false;
                CheckState state = CheckState.Checked;
                for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                {
                    state = (CheckState)node.ParentNode.Nodes[i].CheckState;
                    if (!check.Equals(state))
                    {
                        b = !b;
                        break;
                    }
                }

                node.ParentNode.CheckState = b ? CheckState.Indeterminate : check;
                SetCheckedParentNodes(node.ParentNode, check);
            }
        }

        private void checkDate_CheckedChanged(object sender, EventArgs e)
        {
            this.txtSecond.Properties.ReadOnly = !this.checkDate.Checked;
        }
        #endregion

        #region 刷新
        private void barRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BindTreeData();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        #endregion

        #region 测试
        #region 开始测试
        private void btnStartTest_Click(object sender, EventArgs e)
        {
            this.txtMessagge.Text = string.Empty;
            this.txtWarning.Text = string.Empty;
            this.txtError.Text = string.Empty;

            ICPAutoTest.IsAutoTestMode = true;
            if (checkDate.Checked)
            {
                ICPAutoTest.Second = DataTypeHelper.GetInt(this.txtSecond.Text,0);
            }
            else
            {
                ICPAutoTest.Second = 0;
            }

            isFinish = false;
            this.btnStartTest.Enabled = false;
            this.btnStop.Enabled = true;

            ///得到要测试的模块与方法列表
            GetTestMethodList();
            if (ModuleList.Count == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("没有选择任何模块");
            }

            TestCase();

        }
        private bool isFinish = false;
        private int count = 0;
        private List<string> ModuleList = new List<string>();
        /// <summary>
        /// 得到测试功能列表
        /// </summary>
        private void GetTestMethodList()
        {
            ICPAutoTest.TestMethodList = new List<string>();
            ModuleList = new List<string>();
            count = 0;

            List<TreeListNode> allNodes = new List<TreeListNode>();
            for (int i = 0; i < treeList1.AllNodesCount; i++)
            {
                TreeListNode item = treeList1.FindNodeByID(i);
                if (item != null) allNodes.Add(item);
            }

            foreach (TreeListNode item in allNodes)
            {
                if (item.Checked)
                {
                    int type = DataTypeHelper.GetInt(item.GetValue("Type"),0);
                    if (type == 2 || type == 3)
                    {
                        string methodName = DataTypeHelper.GetString(item.GetValue("MethodName"), string.Empty);
                        if (string.IsNullOrEmpty(methodName))
                        {
                            continue;
                        }
                          if (type == 2)
                          {
                              if (!ModuleList.Contains(methodName))
                              {
                                  ModuleList.Add(methodName);
                              }                              
                          }
                          if (type == 3)
                          {
                              if (!ICPAutoTest.TestMethodList.Contains(methodName))
                              {
                                  ICPAutoTest.TestMethodList.Add(methodName);
                              }
                          }
                    }
                }
            }

        }
        /// <summary>
        /// 测试样例
        /// </summary>
        private void TestCase()
        {
            if (ModuleList.Count == 0 || isFinish)
            {
                return;
            }
           
            string moduleName =DataTypeHelper.GetString(ModuleList[count],string.Empty);
            if (string.IsNullOrEmpty(moduleName))
            {
                return;
            }
            Workitem.Commands[moduleName].Execute();
            if (count == ModuleList.Count())
            {
                FinishTest();
            }
            else
            {
                count++;
            }
        }
        /// <summary>
        /// 完成测试
        /// </summary>
        private void FinishTest()
        {
            isFinish = true;
            this.txtMessagge.Text = this.txtMessagge.Text + System.Environment.NewLine + DateTime.Now.ToString() + "测试完成！";
        }

        #endregion

        #endregion

        #region 新增
        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        #endregion

        #region 重载
        /// <summary>
        /// 设置消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        public void AutoTestSetMessage(string message)
        {
            if (string.IsNullOrEmpty(this.txtMessagge.Text))
            {
                this.txtMessagge.Text = message;
            }
            else
            {
                this.txtMessagge.Text = this.txtMessagge.Text + System.Environment.NewLine + message;
            }

        }
        /// <summary>
        /// 设置错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        public void AutoTestSetError(string message)
        {
            if (string.IsNullOrEmpty(this.txtError.Text))
            {
                this.txtError.Text = message;
            }
            else
            {
                this.txtError.Text = this.txtError.Text + System.Environment.NewLine + message;
            }
        }
        /// <summary>
        /// 设置预警
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        public void AutoTestSetWarning(string message)
        {
            if (string.IsNullOrEmpty(this.txtWarning.Text))
            {
                this.txtWarning.Text = message;
            }
            else
            {
                this.txtWarning.Text = this.txtWarning.Text + System.Environment.NewLine + message;
            }
        }
        /// <summary>
        /// 执行下一个案例
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        public void AutoTestNextCase()
        {
            TestCase();
        }
        #endregion

        #region 停止
        private void btnStop_Click(object sender, EventArgs e)
        {
            isFinish = true;
            this.btnStartTest.Enabled = true;
            this.btnStop.Enabled = false;
        }
        #endregion

    }


 


}
