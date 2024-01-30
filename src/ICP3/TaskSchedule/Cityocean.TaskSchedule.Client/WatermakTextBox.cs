#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/4/27 16:08:47
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Cityocean.TaskSchedule.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class WatermakTextBox : TextBox
    {
        private const int WM_PAINT = 0xF;
        private string _emptyTextTip;
        private Color _emptyTextTipColor = Color.DarkGray;
        /// <summary>
        /// 
        /// </summary>
        [DefaultValue("")]
        public string EmptyTextTip
        {
            get { return _emptyTextTip; }
            set
            {
                _emptyTextTip = value;
                base.Invalidate();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(typeof(Color), "DarkGray")]
        public Color EmptyTextTipColor
        {
            get { return _emptyTextTipColor; }
            set
            {
                _emptyTextTipColor = value;
                base.Invalidate();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_PAINT)
            {
                WmPaint(ref m);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        private void WmPaint(ref Message m)
        {
            Rectangle rectangle = new Rectangle(0, 0, Width, Height);
            using (Graphics graphics = Graphics.FromHwnd(base.Handle))
            {
                if (Text.Length == 0
                   && !string.IsNullOrEmpty(_emptyTextTip)
                   && !Focused)
                {
                    TextFormatFlags format =
                         TextFormatFlags.EndEllipsis |
                         TextFormatFlags.VerticalCenter;
                    if (RightToLeft == RightToLeft.Yes)
                    {
                        format |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
                    }
                    TextRenderer.DrawText(
                        graphics,
                        _emptyTextTip,
                        Font,
                        base.ClientRectangle,
                        _emptyTextTipColor,
                          format);
                }
            }
        }
    }
}
