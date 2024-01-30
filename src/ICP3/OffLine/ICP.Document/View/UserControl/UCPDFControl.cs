#region Comment

/*
 * 
 * FileName:    UCPDFControl.cs
 * CreatedOn:   2014/5/19 星期一 16:39:21
 * CreatedBy:   taylor
 * 
 * Description：
 *      ->PDF预览控件
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using System;

namespace ICP.Document
{
    public partial class UCPDFControl : ViewBase,IVPDFConvert
    {
        /// <summary>
        /// 转换文件
        /// </summary>
        public event EventHandler<PDFConvertEventArgs> ConvertFile;

        #region 构造函数
        public UCPDFControl()
        {
            InitializeComponent();
        }

        protected override object CreatePresenter()
        {
            return new PPDFConvert(this);
        } 
        #endregion

        /// <summary>
        /// 加载文件
        /// </summary>
        /// <param name="paramFilePath">文件路径</param>
        public void LoadFile(string paramFilePath)
        {
            this.ConvertFile(this, new PDFConvertEventArgs() { FilePath = paramFilePath });
        }
        /// <summary>
        /// 控件加载文件
        /// </summary>
        /// <param name="paramFilePath">文件路径</param>
        public void ControlLoadFile(string paramFilePath)
        {
            this.axAcroPDF1.LoadFile(paramFilePath);
        }

        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {
            this.axAcroPDF1.Print();
        }
        
    }
}
