
//-----------------------------------------------------------------------
// <copyright file="HostSurface.cs" company="ICP">
//     Copyright (c) ICP. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.WorkFlowDesigner
{
    using System;
    using System.ComponentModel.Design;


    /// <summary>
    /// 提供用于设计组件的用户界面。
    /// </summary>
	public class ICPFlowDesignSurface : DesignSurface
    {
        
        #region  构造函数

        public ICPFlowDesignSurface() 
            : base()
		{
            this.ServiceContainer.AddService(
                typeof(IMenuCommandService),
                new WorkflowMenuCommandService(this));
		}
        public ICPFlowDesignSurface(IServiceProvider parentProvider) 
            : base(parentProvider)
		{
            this.ServiceContainer.AddService(
                typeof(IMenuCommandService),
                new WorkflowMenuCommandService(this));
        }


  
        #endregion

        private WorkflowLoader loader;
        public void Init(WorkflowLoader designerLoader)
        {
            loader = designerLoader;
            this.BeginLoad(designerLoader);
        }

        public WorkflowLoader DesignerLoader
        {
            get
            {
                return loader;
            }
        }

    }


    public class SelectedEventArgs : EventArgs
    {
        public object Object { get; set; }

        public SelectedEventArgs(object arg)
        {
            this.Object = arg;
        }
    }

    public delegate void SelectedChangedEventHandler(object sender, SelectedEventArgs e);


    public class ExceptionEventArgs : EventArgs
    {
        public Exception Exception { get; set; }

        public ExceptionEventArgs(Exception exception)
        {
            this.Exception = exception;
        }
    }

    public delegate void ExceptionEventHandler(object sender, ExceptionEventArgs e);

}
