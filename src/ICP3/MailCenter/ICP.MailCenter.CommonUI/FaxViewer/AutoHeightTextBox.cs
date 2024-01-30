using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ICP.MailCenter.CommonUI
{   

    /// <summary>
    /// 自动伸缩文本框
    /// </summary>
   public class AutoHeightTextBox:TextBox
    {  
       /// <summary>
       /// 构造函数
       /// </summary>
       public AutoHeightTextBox()
       {
           this.MinimumSize = new System.Drawing.Size(0, this.MinHeight);
           this.TextChanged += new EventHandler(AutoHeightTextBox_TextChanged);
           //this.SizeChanged += new EventHandler(AutoHeightTextBox_SizeChanged);
       }

       void AutoHeightTextBox_SizeChanged(object sender, EventArgs e)
       {
           AutoHeight();
       }

       void AutoHeightTextBox_TextChanged(object sender, EventArgs e)
       {
           AutoHeight();
       }
       [DllImport("user32.dll")]
       public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
       private int maxHeight = 35;
       private int minHeight = 21;
       /// <summary>
       /// 最小高度
       /// </summary>
       public int MinHeight
       {
           get { return this.minHeight; }
           set { this.minHeight = value; }
       }
       /// <summary>
       /// 最大高度
       /// </summary>
       public int MaxHeight
       {
           get { return maxHeight; }
           set {this.maxHeight=value;}
       }
       /// <summary>
       /// 自动调节高度
       /// </summary>
       public void AutoHeight()
       {
           const int EM_GETLINECOUNT = 186;
           Int32 lineCount = SendMessage(this.Handle, EM_GETLINECOUNT, IntPtr.Zero, IntPtr.Zero).ToInt32();
           int height = (lineCount * this.Font.Height);
           if (height > 35)
           {
               this.Height = 35;
               this.ScrollBars = ScrollBars.Vertical;
           }
           else
           {
               this.Height = height;
               this.ScrollBars = ScrollBars.None;
           }
       }
    }
}
