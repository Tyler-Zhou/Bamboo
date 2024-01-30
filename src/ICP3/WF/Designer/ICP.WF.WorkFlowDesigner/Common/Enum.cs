using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.WF.WorkFlowDesigner
{
    #region 工作流类型

    /// <summary>
    /// 工作流类型
    /// </summary>
    public enum WorkflowTypes
    {
        /// <summary>
        /// 顺序工作流
        /// </summary>
        DefaultSequenceWorkflow = 0,
        /// <summary>
        /// 状态机
        /// </summary>
        StateMachineWorkflowActivity
    }

    #endregion
}
