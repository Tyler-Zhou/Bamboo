using System;
using System.Drawing;
using System.ServiceModel;
using ICP.FilePreviewServiceLibrary;
using System.Windows.Forms;
namespace ICP.FilePreviewService
{
    [ServiceBehavior(IncludeExceptionDetailInFaults=true,InstanceContextMode=InstanceContextMode.Single)]
   public class FilePreviewService:IFilePreviewService
    {
        #region IICP.FilePreviewService 成员

        public void Preview(string filePath, Point location, Size size,bool isAutoHide)
        {
            try
            {   
                
                frmFilePreview.Current.Preview(filePath, location, size,isAutoHide);
            }
            catch (Exception ex)
            {
                string errorString = Framework.CommonLibrary.Common.CommonHelper.BuildExceptionString(ex);
                Framework.CommonLibrary.LogHelper.SaveLog(errorString);
                MessageBox.Show(ex.Message);
            }
        }

        public void Hide()
        {
            frmFilePreview.Current.Hide();
        }
        public void Exit()
        {
            Application.Exit();
        }

        public void Print()
        {
            frmFilePreview.Current.Print();
        }
        #endregion
    }
}
