
//-----------------------------------------------------------------------
// <copyright file="ShellPropertyControl.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.FormDesigner
{
    using System.ComponentModel.Design;
    using ICP.Framework.ClientComponents.Controls;

    /// <summary>
    /// 属性面版
    /// </summary>
    public partial class ShellPropertyPart : LWXtraPropertyGrid, IPropertyPart
    {
        public ShellPropertyPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.OnException = null;
                this.CurrentDesignerHost = null;
            
            };
           
        }

        public event ExceptionEventHandler OnException;

        public IDesignerHost CurrentDesignerHost { get; set; }

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
       
    }
}
