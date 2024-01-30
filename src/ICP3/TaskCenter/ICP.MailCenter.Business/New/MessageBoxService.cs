using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
namespace ICP.Operation.Common.ServiceInterface
{
   public class MessageBoxService:IMessageBoxService
    {
       string defaultCaption = "ICP";
        #region IMessageBoxService 成员

        public void ShowInfo(string text)
        {
            ShowInfo(text, defaultCaption);
        }

        public void ShowInfo(string text, string caption)
        {
            ShowInfo(text, caption, MessageBoxButtons.OK);
        }

        public DialogResult ShowInfo(string text, string caption, MessageBoxButtons buttons)
        {
            return Show(text, caption, buttons, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        public DialogResult ShowWarning(string text)
        {
            return ShowWarning(text, defaultCaption);
        }

        public DialogResult ShowWarning(string text, string caption)
        {
            return ShowWarning(text, caption, MessageBoxButtons.OK);
        }

        public DialogResult ShowWarning(string text, string caption, MessageBoxButtons buttons)
        {
            return ShowWarning(text, caption, buttons, MessageBoxDefaultButton.Button1);
        }

        public DialogResult ShowWarning(string text, string caption, MessageBoxButtons buttons, MessageBoxDefaultButton defaultButton)
        {
            return Show(text, caption, buttons, MessageBoxIcon.Warning, defaultButton);
        }

        public DialogResult ShowQuestion(string text)
        {
            return ShowQuestion(text, defaultCaption);
        }

        public DialogResult ShowQuestion(string text, string caption)
        {
            return ShowQuestion(text, caption, MessageBoxButtons.OKCancel);
        }

        public DialogResult ShowQuestion(string text, string caption, MessageBoxButtons buttons)
        {
            return ShowQuestion(text, caption, buttons, MessageBoxDefaultButton.Button1);
        }

        public DialogResult ShowQuestion(string text, string caption, MessageBoxButtons buttons, MessageBoxDefaultButton defaultButton)
        {
            return Show(text, caption, buttons, MessageBoxIcon.Question, defaultButton);
        }

        public void ShowError(string text)
        {
            ShowError(text, defaultCaption);
        }

        public void ShowError(string text, string caption)
        {
             ShowError(text, caption, MessageBoxButtons.OK);
        }

        public DialogResult ShowError(string text, string caption, MessageBoxButtons buttons)
        {
            return Show(text, caption, buttons, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
        }

        public DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            return XtraMessageBox.Show(text, caption, buttons, icon, defaultButton);
        }

        #endregion
    }
}
