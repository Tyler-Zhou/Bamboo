#region Comment

/*
 * 
 * FileName:    FormFadeBase.cs
 * CreatedOn:   2014/5/20 星期二 9:45:29
 * CreatedBy:   taylor
 * 
 * Description：
 *      ->预览窗体显示、隐藏淡入淡出效果
 * History：
 * 
 * 
 * 
 * 
 */

#endregion


using System.Windows.Forms;
using System;
namespace ICP.Document
{
    public partial class FormFadeBase : FormBase
    {
        #region 成员字段
        /// <summary>
        /// 探测计时器
        /// </summary>
        private Timer DetectTimer = new Timer();
        /// <summary>
        /// 
        /// </summary>
        private bool IsInBound = false;
        /// <summary>
        /// 鼠标 改变
        /// </summary>
        public Action<Boolean> MouseBoundChanged;

        #endregion

        public FormFadeBase()
        {
            InitializeComponent();
            this.VisibleChanged += new EventHandler(FormFadeBase_VisibleChanged);
            this.Disposed += (sender, e) =>
            {
                this.VisibleChanged -= new EventHandler(FormFadeBase_VisibleChanged);
            };
        }
        /// <summary>
        /// 窗体可见改变：可见启用计时器
        /// </summary>
        void FormFadeBase_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (!this.DetectTimer.Enabled)
                {
                    this.DetectTimer.Enabled = true;
                    this.DetectTimer.Start();
                }
            }
            else
            {
                this.DetectTimer.Stop();
                this.DetectTimer.Enabled = false;
            }
        }

        void DetectTimer_Tick(object sender, EventArgs e)
        {
            bool inBound = this.Bounds.Contains(Cursor.Position);
            if (inBound != IsInBound)
            {
                IsInBound = inBound;
                if (this.MouseBoundChanged != null)
                {
                    MouseBoundChanged(inBound);
                }
            }
        }
        #region Form Overrides
        /// <summary>
        /// 重写加载方法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {

            DetectTimer.Enabled = true;
            DetectTimer.Interval = 300;
            DetectTimer.Tick += new EventHandler(DetectTimer_Tick);
            base.OnLoad(e);
        }
        #endregion
    }
}
