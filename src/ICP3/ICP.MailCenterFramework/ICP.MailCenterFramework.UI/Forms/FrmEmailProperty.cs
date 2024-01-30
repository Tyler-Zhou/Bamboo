#region Comment

/*
 * 
 * FileName:    FrmMessageBox.cs
 * CreatedOn:   2014/9/4 15:50:25
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

using System;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Outlook;
using Exception = System.Exception;

namespace ICP.MailCenterFramework.UI
{
    public partial class FrmEmailProperty : Form
    {
        public FrmEmailProperty()
        {
            InitializeComponent();
            Load += FrmEmailProperty_Load;
            KeyDown += FrmEmailProperty_KeyDown;
            Disposed += (sender, args) =>
            {
                Load -= FrmEmailProperty_Load;
                KeyDown -= FrmEmailProperty_KeyDown;
            };
        }

        void FrmEmailProperty_Load(object sender, EventArgs e)
        {
            try
            {
                ShowMailProperty();
            }
            catch (Exception ex)
            {
            }
        }
        void FrmEmailProperty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
        private void ShowMailProperty()
        {
            StringBuilder strMesage = new StringBuilder();
            object tempObj = OutlookUtility.CurrentApplication.ActiveExplorer().Selection[1];
            MailItem mailItem = tempObj as MailItem;
            if (mailItem == null) return;

            txtSubject.Text = mailItem.Subject;
            strMesage.Append("General\r\n");
            strMesage.AppendFormat("EntryID              :  {0}\r\n", mailItem.EntryID);
            string createBy = string.Empty;
            string MessageID = OutlookUtility.SearchMessageID(tempObj, out createBy);
            strMesage.AppendFormat("MessageId            :  {0}\r\n", MessageID);
            strMesage.AppendFormat("Build MessageID      :  {0}\r\n", createBy);

            MAPIFolder currentFolder = mailItem.Parent as MAPIFolder;
            if (currentFolder != null)
            {
                strMesage.Append("\r\nFolder\r\n");
                strMesage.AppendFormat("Folder Name          :  {0}\r\n", currentFolder.Name);
                strMesage.AppendFormat("Folder EntryID       :  {0}\r\n", currentFolder.EntryID);
            }
            

            Message.ServiceInterface.Message messageObj = OutlookUtility.ConvertMailItemToMessageInfo(mailItem);
            if (messageObj != null && messageObj.UserProperties != null)
            {
                strMesage.Append("\r\nUserProperties\r\n");
                strMesage.AppendFormat("OperationId          :  {0}\r\n",
                    messageObj.UserProperties.OperationId);
                strMesage.AppendFormat("OperationNO          :  {0}\r\n",
                    messageObj.UserProperties.OperationNO);
                strMesage.AppendFormat("OperationRelationID  :  {0}\r\n",
                    messageObj.UserProperties.OperationRelationID);
                strMesage.AppendFormat("OperationRelationID  :  {0}\r\n",
                    messageObj.UserProperties.OperationType);
            }
            txtMessage.Text = strMesage.ToString();
        }
    }
}
