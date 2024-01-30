
//-----------------------------------------------------------------------
// <copyright file="ShellPropertyControl.cs" company="ICP">
//     Copyright (c) ICP. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.WorkFlowDesigner
{
    using System.ComponentModel.Design;
    using ICP.Framework.ClientComponents.Controls;
using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// 属性面版
    /// </summary>
    public partial class WorkProperty : LWXtraPropertyGrid, IWorkProperty
    {
        public WorkProperty()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this._currentDesignerHost = null;
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
           
        }
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public object SelectedObject
        {
            get
            {
                return this.PropertyGrid.SelectedObject;
            }
            set
            {
                this.PropertyGrid.SelectedObject = value;
            }
        }

        IDesignerHost _currentDesignerHost;
        public IDesignerHost CurrentDesignerHost 
        {
            get
            {
                return _currentDesignerHost;
            }
            set
            {
                _currentDesignerHost = value;
                this.PropertyGrid.ServiceProvider = value;
            }
        }

        protected override object GetService(System.Type service)
        {
            if (_currentDesignerHost != null)
            {
                return _currentDesignerHost.GetService(service);
            }
            else
            {
                return base.GetService(service);
            }
        }
    }
}
