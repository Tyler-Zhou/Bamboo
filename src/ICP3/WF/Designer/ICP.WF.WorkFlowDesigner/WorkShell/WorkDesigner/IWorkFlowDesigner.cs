using System;
using System.Collections.Generic;
using System.Workflow.ComponentModel.Design;
using System.ComponentModel.Design;
using System.Drawing;

namespace ICP.WF.WorkFlowDesigner
{
    public interface IWorkFlowDesigner : IBasePart
    {
        /// <summary>
        /// 选择的活动控件发生改变
        /// </summary>
        event SelectedChangedEventHandler SelectionChanged;

        event ActiveDesignerChangedEventHandler ActiveDesignerChanged;

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="commandname"></param>
        void ExecuteAction(CommandID commandname);
        /// <summary>
        /// 加载指定工作流文件
        /// </summary>
        /// <param name="xoml"></param>
        void LoadWorkflow(string xoml);
        /// <summary>
        /// 显示默认工作流
        /// </summary>
        void ShowDefaultWorkflow();
        /// <summary>
        /// 保存工作流文件
        /// </summary>
        /// <param name="path"></param>
        void Save(string filePath);

        /// <summary>
        /// 开始布局
        /// </summary>
        void StartLayout();

        /// <summary>
        /// 结束布局
        /// </summary>
        void EndLayout();

        /// <summary>
        /// 刷新布局
        /// </summary>
         void ReLayout();

         /// <summary>
         /// 验证数据
         /// </summary>
         /// <returns></returns>
         List<string> GetErrorList();


        #region 属性
        /// <summary>
        /// 流程名称
        /// </summary>
        string WorkflowName{get;set;}
        /// <summary>
        /// 保存路径
        /// </summary>
        string WorkflowSaveAs{get;set;}
        /// <summary>
        /// 流程类型
        /// </summary>
        WorkflowTypes WorkflowCreationType { get; set; }
        /// <summary>
        /// 流程设计控件
        /// </summary>
        WorkflowView WorkFlowView { get; }
        /// <summary>
        /// 模板流程路径
        /// </summary>
        string TemplateXoml { get; set; }
        /// <summary>
        /// 获得Size
        /// </summary>
        Size GetSize { get; }
        #endregion

    }


    public class ActiveDesignerChangedEventArgs : EventArgs
    {
        public IDesignerHost DesignerHost { get; set; }

        public ActiveDesignerChangedEventArgs(IDesignerHost designerHost)
        {
            this.DesignerHost = designerHost;
        }
    }

    public delegate void ActiveDesignerChangedEventHandler(object sender, ActiveDesignerChangedEventArgs e);

}
