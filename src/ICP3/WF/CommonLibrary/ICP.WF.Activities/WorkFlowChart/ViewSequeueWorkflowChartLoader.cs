using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using ICP.WF.ServiceInterface;
using ICP.WF.ServiceInterface.DataObject;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 查看流程完成情况的顺序图
    /// </summary>
    internal class ViewSequeueWorkflowChartLoader : WorkflowChartLoader
    {
        #region 本地变量
        private Guid _workFlowId = Guid.Empty;
        private IWorkflowService _workflowService;
        #endregion

        #region 构造函数

        internal ViewSequeueWorkflowChartLoader()
        {
        }


        internal ViewSequeueWorkflowChartLoader(IWorkflowService workflowService, Guid workFlowId)
        {
            _workflowService = workflowService;
            _workFlowId = workFlowId;
        }

        #endregion

        #region 外部接口

        //public void Flush(IWorkFlowService workflowService, Guid workFlowId)
        //{
        //    this.Initialize();
         
        //    //_workflowService = workflowService;
        //    //_workFlowId = workFlowId;

        //    //InitWorkFlowView();

        //    //this.LoaderHost.Reload();
        //}

        #endregion

        #region 重载方法

        public override void InitWorkFlowView()
        {
            if (_workFlowId != Guid.Empty && _workflowService != null)
            {
                LoadWorkFlowActivitys(_workFlowId);
            }
        }

        #endregion

        #region 本地方法

        /// <summary>
        /// 是否可以取消
        /// </summary>
        public bool isCanceled
        {
            get;
            set;
        }

        /*从指定流程中加载一个流程完成信息的流程图*/
        private void LoadWorkFlowActivitys(Guid workFlowId)
        {
            IDesignerHost designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));
            isCanceled = false;
            FlowChartInfo info=_workflowService.GetFlowChartInfo(workFlowId);
            List<FlowChartNode> items = null;
            if (info != null)
            {
                items = info.Nodes;
                //是否可以进行取消操作
                if (items.Count <= 1)
                {
                    //只有一个任务明细，可以进行取消
                    isCanceled = true;
                }
                else if (items.Count == 2)
                {
                    //有两个任务明细，而且第二个的执行人为NULL，可以进行取消
                    if (string.IsNullOrEmpty(items[1].ExcutorName))
                    {
                        isCanceled = true;
                    }
                    else
                    {
                        isCanceled = false;
                    }
                }
                else
                {
                    isCanceled = false;
                }

            }
            if (items != null && items.Count > 0)
            {

                ViewSequenceActivity sa = new ViewSequenceActivity();
                sa.Name = info.WorkflowTitle;
                sa.WorkflowState = info.WorkflowSate;
                sa.Title = info.WorkflowNo + "-" + SR.GetString("FlowChart", "流程图");
                sa.WorkInfo = info;
                string excutor = string.Empty;
                foreach (FlowChartNode item in items)
                {
                    string tit = item.Name;
                    int index = tit.IndexOf("(");
                    if (index > 0)
                    {
                        tit = tit.Substring(0, index);
                    }
                    if (tit.Length > 20)
                    {
                        tit = tit.Substring(0, 20);
                    }

                    ViewActivity a = new ViewActivity();
                    a.Name = item.Name+item.Id.ToString();
                    a.WorkItem = item;
                    a.Title = tit + Environment.NewLine + (string.IsNullOrEmpty(item.ExcutorName) ? "" : ("[" + item.ExcutorName + "]"));
                    a.TipMessage = item.ToString(SR.IsEnglish);
                    sa.Activities.Add(a);

                    excutor = item.ExcutorName;
                }
                if (sa.Activities.Count > 0)
                {
                    if (sa.WorkflowState == WorkflowState.Cancel)
                    {
                        ViewActivity a = new ViewActivity();
                        a.Name = "Cancel"+Guid.NewGuid().ToString();
                        a.WorkItem = new FlowChartNode();
                        a.Title = SR.GetString("Cancel", "取消");
                        a.TipMessage = SR.GetString("Cancel", "取消");
                        sa.Activities.Add(a);
                    }
                    else if (sa.WorkflowState == WorkflowState.Return)
                    {
                        ViewActivity a = new ViewActivity();
                        a.Name = "NotThrough"+Guid.NewGuid().ToString();
                        a.WorkItem = new FlowChartNode();
                        a.Title = SR.GetString("NotThrough", "审批没通过") + Environment.NewLine + (string.IsNullOrEmpty(excutor) ? "" : ("[" + excutor + "]"));
                        a.TipMessage = SR.GetString("NotThrough", "审批没通过");
                        sa.Activities.Add(a);
                    }
                }

                if (sa != null && designerHost != null)
                {
                    AddObjectGraphToDesignerHost(designerHost, sa);
                }

                designerHost.Activate();
            }
        }
        #endregion
    }
}
