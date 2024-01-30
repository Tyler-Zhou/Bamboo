
//-----------------------------------------------------------------------
// <copyright file="HostControl.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.FormDesigner
{
    using System;
    using System.ComponentModel.Design;
    using System.Windows.Forms;
    using DevExpress.XtraEditors;

    /// <summary>
	/// 宿主呈现控件
	/// </summary>
    public class LWHostControl : XtraUserControl
    {
        #region 本地变量 

        private System.ComponentModel.IContainer components = null;
		private LWFormDesignSurface _hostSurface;

        #endregion

        #region 初始化

        public LWHostControl(LWFormDesignSurface hostSurface)
		{
			InitializeComponent();
           
            this.AllowDrop = true;

			InitializeHost(hostSurface);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
                this._hostSurface = null;
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.SuspendLayout();
            // 
            // HostControl
            // 
            this.Name = "HostControl";
            this.Size = new System.Drawing.Size(389, 224);
            this.ResumeLayout(false);

		}
		#endregion

        #endregion

        #region 公共方法与公共属性
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostSurface"></param>
        internal void InitializeHost(LWFormDesignSurface hostSurface)
		{
			try
			{
                if (hostSurface == null)
                {
                    return;
                }
				_hostSurface = hostSurface;
               
				Control control = _hostSurface.View as Control;
                control.AllowDrop = true;
				control.Parent = this;
                control.BackColor = System.Drawing.Color.AliceBlue;
				control.Dock = DockStyle.Fill;
				control.Visible = true;
			}
			catch(Exception ex)
			{
                IOutputPart o = this.GetService(typeof(IOutputPart)) as IOutputPart;
                o.Info(ex.Message);
			}
		}

        /// <summary>
        /// 
        /// </summary>
		public LWFormDesignSurface HostSurface
		{
			get
			{
				return _hostSurface;
			}
		}

        /// <summary>
        /// 
        /// </summary>
		public IDesignerHost DesignerHost
		{
			get
			{
				return (IDesignerHost)_hostSurface.GetService(typeof(IDesignerHost));
			}
        }
        #endregion


       
    } 
}
