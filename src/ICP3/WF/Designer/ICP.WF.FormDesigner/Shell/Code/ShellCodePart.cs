
//-----------------------------------------------------------------------
// <copyright file="ShellCodePart.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.FormDesigner
{
    using DevExpress.XtraEditors;
    using System.ComponentModel.Design;

    /// <summary>
    /// 代码预览界面
    /// </summary>
    public partial class ShellCodePart:XtraUserControl , ICodePart
    {
        public ShellCodePart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.OnException = null;
                this.CurrentDesignerHost = null;
            };
        }

        /// <summary>
        /// 显示代码
        /// </summary>
        public string Code
        {
            get
            {
                return txtCode.Text;
            }
            set
            {
                txtCode.Text = value;
            }
        }

        public event ExceptionEventHandler OnException;

        public IDesignerHost CurrentDesignerHost { get; set; }
    }
}
