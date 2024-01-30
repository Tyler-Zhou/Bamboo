#region Comment

/*
 * 
 * FileName:    FormException.cs
 * CreatedOn:   2014/5/21 星期三 16:19:06
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->异常信息显示
 * History：
 * 
 * 
 * 
 * 
 */

#endregion


using System;
using System.Text;

namespace ICP.Document
{
    /// <summary>
    /// 异常信息显示
    /// </summary>
    public partial class FormException : FormBase
    {
        #region 成员变量
        /// <summary>
        /// 异常信息
        /// </summary>
        private static Exception _Error; 
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="exType">异常类型</param>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        public FormException(ExceptionType exType, Exception ex, string backStr)
        {
            InitializeComponent();
            btnClose.Click += new EventHandler(btnClose_Click);
            btnShowDetail.Click += new EventHandler(btnShowDetail_Click);
            this.Height = 125;
            _Error = ex;
            txtErrorInfo.Text = GetExceptionMsg(exType, ex, backStr);
            LogUtility.WriteLog("System Exception", txtErrorInfo.Text);
            this.Disposed += (sender, e) =>
            {
                txtErrorInfo.Text = "";
                _Error = null;
                btnClose.Click -= new EventHandler(btnClose_Click);
                btnShowDetail.Click -= new EventHandler(btnShowDetail_Click);
            };
        } 
        #endregion

        #region 窗体事件
        /// <summary>
        /// 显示异常明细
        /// </summary>
        void btnShowDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Height == 125)
                {
                    this.Height = 350;
                    this.btnShowDetail.Text = "Detail ↑";
                }
                else
                {
                    this.Height = 125;
                    this.btnShowDetail.Text = "Detail ↓";
                }
            }
            catch
            {
                return;
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        #endregion

        #region ShowException
        /// <summary>
        /// 显示异常
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        public static void ShowException(Exception Ex)
        {
            ShowException(ExceptionType.ThreadEx, Ex, "");
        }
        /// <summary>
        /// 显示异常
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        public static void ShowException(Exception Ex, string backStr)
        {
            ShowException(ExceptionType.ThreadEx, Ex, backStr);
        }
        /// <summary>
        /// 显示异常
        /// </summary>
        /// <param name="ExceptionType">异常类型</param>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        public static void ShowException(ExceptionType exType, Exception Ex, string backStr)
        {
            FormException frmException = new FormException(exType, Ex, backStr);
            frmException.ShowDialog();
        } 
        #endregion

        #region 生成自定义异常消息
        /// <summary>
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ExType">异常类型</param>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        public static string GetExceptionMsg(ExceptionType exType,Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("【Exception Time】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                switch (exType)
                {
                    case ExceptionType.ThreadEx:
                        sb.AppendLine("【Exception Type】：" + ex.GetType().Name);
                        sb.AppendLine("【Exception Message】：" + ex.Message);
                        sb.AppendLine("【Exception StackTrace】：" + ex.StackTrace);
                        break;
                    case ExceptionType.UnhandledEx:
                        sb.AppendLine("【Application UnhandledException】：" + ex.Message);
                        sb.AppendLine("【Stack Information】：" + ex.StackTrace);
                        break;
                }
            }
            else
            {
                switch (exType)
                {
                    case ExceptionType.ThreadEx:
                        sb.AppendLine("【Thread Exception】：" + backStr);
                        break;
                    case ExceptionType.UnhandledEx:
                        sb.AppendLine("【Unhandled Exception】：" + backStr);
                        break;
                }
                
            }
            return sb.ToString();
        }
        #endregion
    }
}
