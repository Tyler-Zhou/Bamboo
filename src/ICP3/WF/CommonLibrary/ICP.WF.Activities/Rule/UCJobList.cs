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
using ICP.WF.ServiceInterface;
using DevExpress.XtraTreeList.Nodes;
using ICP.WF.ServiceInterface.DataObject;
using ICP.Framework.CommonLibrary;

namespace ICP.WF.Activities
{
    public partial class UCJobList : XtraUserControl
	{
		public UCJobList()
		{
			InitializeComponent();
        }

        #region 服务

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
        /// 左边数据列表
        /// </summary>
        List<WFJobList> LeftJobList = new List<WFJobList>();
        /// <summary>
        /// 右边数据列表
        /// </summary>
        List<WFJobList> RightJobList = new List<WFJobList>();
        /// <summary>
        /// 对应的树节点
        /// </summary>
        public TreeListNode treeNode
        {
            get;
            set;
        }
        #endregion

        #region 按钮方法
        /// <summary>
        /// 移动到右边
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToRight_Click(object sender, EventArgs e)
        {
            LeftToRight();
        }
        /// <summary>
        /// 将选中移动到左边
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToLeft_Click(object sender, EventArgs e)
        {
            RightToLeftList();
        }
        /// <summary>
        /// 将右边全部移动到左边
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllToLeft_Click(object sender, EventArgs e)
        {
            RightAllToLeft();
        }

        #endregion

        #region 方法
        /// <summary>
        /// 绑定左边列表
        /// </summary>
        public void BindLeftList()
        {
            LeftJobList = new List<WFJobList>();

            if (wService == null)
            {
                return;
            }
            List<JobList> syJobList = wService.GetJobList(null,null,true,0);

            foreach (JobList jobList in syJobList)
            {
                WFJobList wfJob = new WFJobList();
                wfJob.ID = jobList.ID;
                wfJob.WFID = jobList.ID.ToString();
                wfJob.Code = jobList.Code;
                wfJob.CName = jobList.CName;
                wfJob.EName = jobList.EName;

                LeftJobList.Add(wfJob);
            }
            //表单中的数据
            if (FormDataList != null && FormDataList.Count > 0)
            {
                foreach (string str in FormDataList.Keys)
                {
                    WFJobList wfJob = new WFJobList();
                    wfJob.ID = Guid.NewGuid();
                    wfJob.WFID = str;
                    wfJob.Code = str;
                    wfJob.CName = FormDataList[str];
                    wfJob.EName = FormDataList[str];

                    LeftJobList.Insert(0, wfJob);
                }
            }

            this.listJobLeft.DataSource = LeftJobList;
            this.listJobLeft.ValueMember = "WFID";
            this.listJobLeft.DisplayMember = LocalData.IsEnglish?"EName":"CName";

        }
        /// <summary>
        /// 绑定右边列表数据
        /// </summary>
        public void BindRightList(List<string> strList)
        {
            RightJobList = new List<WFJobList>();

            if (wService == null)
            {
                return;
            }
            foreach (string str in strList)
            {
                JobList job = null;
                if (DataTypeHelper.IsGuid(str))
                {
                    job=wService.GetJobInfo(str);
                }

                if (job == null)
                {
                    WFJobList wfjob= LeftJobList.Find(delegate(WFJobList item) { return item.WFID == str; });

                    if (wfjob != null)
                    {
                        job = wfjob;
                    }
                    else
                    {
                        job = new JobList();
                        job.ID = Guid.NewGuid();
                    }
                }

                WFJobList wfJobList = new WFJobList();

                wfJobList.ID = job.ID;
                wfJobList.WFID = job.ID.ToString();
                wfJobList.Code = job.Code;
                wfJobList.EName = job.EName;
                wfJobList.CName = job.CName;

                RightJobList.Add(wfJobList);
            }

            JobsBindingSource.DataSource = RightJobList;

            this.listJobRight.ValueMember = "WFID";
            this.listJobRight.DisplayMember = LocalData.IsEnglish ? "EName" : "CName";

            JobsBindingSource.ListChanged -= new ListChangedEventHandler(JobsBindingSource_ListChanged);
            JobsBindingSource.ListChanged += new ListChangedEventHandler(JobsBindingSource_ListChanged);

        }


        /// <summary>
        /// 从左边移到右边
        /// </summary>
        public void LeftToRight()
        {
            if (listJobLeft.SelectedValue == null)
            {
                return;
            }
            WFJobList job = this.listJobLeft.SelectedItem as WFJobList;
            if (job == null)
            {
                return;
            }
            if (RightJobList.Contains(job))
            {
                return;
            }
            RightJobList.Add(job);

            JobsBindingSource.ResetBindings(false);

            RefreshButton();
        }
        /// <summary>
        /// 从右边移到右边
        /// </summary>
        public void RightToLeftList()
        {
            if (listJobRight.SelectedValue == null)
            {
                return;
            }
            WFJobList job = this.listJobRight.SelectedItem as WFJobList;
            if (job == null)
            {
                return;  
            }
            if (!RightJobList.Contains(job))
            {
                return;
            }
            RightJobList.Remove(job);

            JobsBindingSource.ResetBindings(false);

            RefreshButton();
        }
        /// <summary>
        /// 将右边的全部移到右边
        /// </summary>
        public void RightAllToLeft()
        {
            if (listJobRight.ItemCount == 0)
            {
                return;
            }
            
            RightJobList.Clear();

            JobsBindingSource.ResetBindings(false);

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

            if (listJobRight.ItemCount == 0)
            {
                this.btnToLeft.Enabled = false;
                this.btnAllToLeft.Enabled = false;
            }
            if (listJobLeft.ItemCount == 0)
            {
                this.btnToRight.Enabled = false;
            }
        }
        #endregion

        #region Right List事件
        /// <summary>
        /// 双击移动 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listJobRight_DoubleClick(object sender, EventArgs e)
        {
            RightToLeftList();
        }
        #endregion

        #region Left List事件
        /// <summary>
        /// 双击移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listJobLeft_DoubleClick(object sender, EventArgs e)
        {
            LeftToRight();
        }
        #endregion

        #region 输入的职位发生改变时
        /// <summary>
        /// 输入的职位发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtJobSearch_EditValueChanged(object sender, EventArgs e)
        {
            string strjob = this.txtJobSearch.Text;
            List<WFJobList> data = LeftJobList;
            if (!string.IsNullOrEmpty(strjob))
            {
                if (LocalData.IsEnglish)
                {
                    data = LeftJobList.FindAll(delegate(WFJobList job)
                    {
                        return job.EName.ToLower().Contains(strjob.ToLower());
                    });
                }
                else
                {
                    data = LeftJobList.FindAll(delegate(WFJobList job)
                    {
                        return job.CName.ToLower().Contains(strjob.ToLower());
                    });
                }
            }

            listJobLeft.DataSource = data;

        }
        #endregion


        #region 窗体事件
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
        public void InitControl()
        {
            // BindLeftList();

            //BindRightList();
        }

        #endregion

        #region 将数据绑定到节点树上
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
                foreach (WFJobList job in RightJobList)
                {
                    data.RightExpress.Add(job.ID.ToString());
                }

                treeNode.SetValue("ComRule", data);
            }
            
        }
        /// <summary>
        /// 数据库发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JobsBindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            SetRule();
        }

        #endregion

    }
}
