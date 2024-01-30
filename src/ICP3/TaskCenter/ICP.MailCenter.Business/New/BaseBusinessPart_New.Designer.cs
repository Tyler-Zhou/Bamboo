using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ICP.Common.Business.ServiceInterface
{
    public partial class BaseBusinessPart_New
    {
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barTool = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.baseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.baseBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager
            // 
            this.barManager.AllowCustomization = false;
            this.barManager.AllowMoveBarOnToolbar = false;
            this.barManager.AllowQuickCustomization = false;
            this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barTool});
            this.barManager.DockControls.Add(this.barDockControlTop);
            this.barManager.DockControls.Add(this.barDockControlBottom);
            this.barManager.DockControls.Add(this.barDockControlLeft);
            this.barManager.DockControls.Add(this.barDockControlRight);
            this.barManager.Form = this;
            this.barManager.MainMenu = this.barTool;
            this.barManager.MaxItemId = 0;
            // 
            // barTool
            // 
            this.barTool.BarName = "Main menu";
            this.barTool.DockCol = 0;
            this.barTool.DockRow = 0;
            this.barTool.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barTool.FloatLocation = new System.Drawing.Point(503, 145);
            this.barTool.FloatSize = new System.Drawing.Size(46, 24);
            this.barTool.OptionsBar.MultiLine = true;
            this.barTool.OptionsBar.UseWholeRow = true;
            this.barTool.Text = "Main menu";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(505, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 214);
            this.barDockControlBottom.Size = new System.Drawing.Size(505, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 190);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(505, 24);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 190);
            // 
            // BaseBusinessPart_New
            // 
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "BaseBusinessPart_New";
            this.Size = new System.Drawing.Size(505, 214);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.baseBindingSource)).EndInit();
            this.ResumeLayout(false);

        }
        private System.ComponentModel.IContainer components;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        protected DevExpress.XtraBars.Bar barTool;
        protected DevExpress.XtraBars.BarManager barManager;
        public System.Windows.Forms.BindingSource baseBindingSource;
    }
}