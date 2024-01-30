using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace ICP.MailCenter.UI
{
    /// <summary>
    /// 扩展的文本框
    /// </summary>
    public partial class GrowTextBox : TextBox
    {
        #region 构造函数
        public GrowTextBox()
        {
            InitializeComponent();
            this.Multiline = true;
        }

        #endregion

        #region 属性

        [Browsable(false)]
        public int FullHeight
        {
            get;
            set;
        }
        #endregion

        #region 根据文本框的值开重新绘制高度
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            ResizeLabel();
        }

        private bool mGrowing;
        private void ResizeLabel()
        {
            if (mGrowing) return;
            mGrowing = true;
            Size sz = new Size(this.Width, 250);
            sz = TextRenderer.MeasureText(this.Text, this.Font, sz, TextFormatFlags.WordBreak);
            FullHeight = this.Height = sz.Height + 5;
            mGrowing = false;
        }

        #endregion
    }
}
