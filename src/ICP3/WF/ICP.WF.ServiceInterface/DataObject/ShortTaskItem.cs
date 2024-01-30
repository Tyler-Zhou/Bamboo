

namespace ICP.WF.ServiceInterface.DataObject
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [Serializable]
    public class FlowChartInfo
    {
        public FlowChartInfo()
        {
            this.Nodes = new List<FlowChartNode>();
        }
        /// <summary>
        /// 流程标题
        /// </summary>
        public string WorkflowTitle { get; set; }

        /// <summary>
        /// 流程ID
        /// </summary>
        public Guid WorkflowID { get; set; }

        public string WorkflowNo { get; set; }

        /// <summary>
        /// 流程状态
        /// </summary>
        public WorkflowState WorkflowSate { get; set; }

        public List<FlowChartNode> Nodes { get; set; }
    }

    /// <summary>
    /// 用于流程图显示
    /// </summary>
    [Serializable]
    public class FlowChartNode
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 当前任务状态
        /// </summary>
        public WorkItemState State { get; set; }

        /// <summary>
        ///如果任务已经签收- 当前正在执行人
        /// </summary>
        public string ExcutorName { get; set; }

        /// <summary>
        /// 如果任务还在代办中 -有权限的负责人列表
        /// </summary>
        public List<string> Principals { get; set; }


        /// <summary>
        /// 任务创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 任务签收时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 任务完成时间
        /// </summary>
        public DateTime? FinishedTime { get; set; }

        /// <summary>
        /// 是否主申请表单
        /// </summary>
        public bool IsMainWorkItem { get; set; }

        /// <summary>
        /// 重载ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("State:");
            sb.Append(Enum.GetName(typeof(WorkItemState), this.State));
            sb.Append(Environment.NewLine);

            if (string.IsNullOrEmpty(this.ExcutorName) == false)
            {
                sb.Append("Executor:");
                sb.Append(this.ExcutorName);
                sb.Append(Environment.NewLine);
            }
            else
            {
                StringBuilder s = new StringBuilder();
                foreach (string p in this.Principals)
                {
                    if (s.Length > 0)
                    {
                        s.Append(",");
                    }

                    s.Append(p);
                }

                if (s.Length > 0)
                {
                    sb.Append("Next Executor:");
                    sb.Append(s.ToString());
                    sb.Append(Environment.NewLine);
                }
            }

            sb.Append("Create Time:");
            sb.Append(this.CreateTime.ToString());
            sb.Append(Environment.NewLine);

            if (this.StartTime != null)
            {
                sb.Append("Sign Time:");
                sb.Append(this.StartTime.Value.ToString());
                sb.Append(Environment.NewLine);
            }

            if (this.FinishedTime != null)
            {
                sb.Append("Finished Time:");
                sb.Append(this.FinishedTime.Value.ToString());
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }


        /// <summary>
        /// 重载ToString
        /// </summary>
        /// <returns></returns>
        public string ToString(bool isEnglish)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(isEnglish? "State:":"状态:");
            sb.Append(Enum.GetName(typeof(WorkItemState), this.State));
            sb.Append(Environment.NewLine);

            if (string.IsNullOrEmpty(ExcutorName) == false)
            {
                sb.Append(isEnglish ? "Executor:" : "执行人:");
                sb.Append(this.ExcutorName);
                sb.Append(Environment.NewLine);
            }
            else
            {
                StringBuilder s = new StringBuilder();
                foreach (string p in this.Principals)
                {
                    if (s.Length > 0) s.Append(",");

                    s.Append(p);
                }

                if (s.Length > 0)
                {
                    sb.Append(isEnglish ? "Next Executor:" : "代办人:");
                    sb.Append(s.ToString());
                    sb.Append(Environment.NewLine);
                }
            }

            sb.Append(isEnglish ? "Create Time:" : "创建时间:");
            sb.Append(this.CreateTime.ToString());
            sb.Append(Environment.NewLine);

            //if (this.StartTime != null)
            //{
            //    sb.Append(isEnglish ? "Sign Time:" : "签收时间:");
            //    sb.Append(this.StartTime.Value.ToString());
            //    sb.Append(Environment.NewLine);
            //}

            if (this.FinishedTime != null)
            {
                sb.Append(isEnglish ? "Finished Time:" : "完成时间:");
                sb.Append(this.FinishedTime.Value.ToString());
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }


    public class WorkItemEventArgs : EventArgs
    {
        public WorkItemEventArgs(FlowChartInfo info)
        {
            this.WorkInfo = info;
        }

        public FlowChartInfo WorkInfo { get; set; }
    }
}
