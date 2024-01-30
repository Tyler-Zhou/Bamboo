using System.ServiceModel;

namespace ICP.FilePreviewServiceLibrary
{
  public class FilePreviewServiceClient:ClientBase<IFilePreviewService>,IFilePreviewService
    {
      public FilePreviewServiceClient()
          : base(FilePreviewHelper.GetBinding(), new EndpointAddress(string.Format("{0}/{1}", FilePreviewHelper.GetServiceBaseAddress(), "FilePreviewService")))
      {
       
       
      }
        #region IFilePreviewService 成员

        public void Preview(string filePath, System.Drawing.Point location, System.Drawing.Size size, bool isAutoHide)
        {
            base.Channel.Preview(filePath, location, size, isAutoHide);
        }

        public void Hide()
        {
            base.Channel.Hide();
        }
        public void Exit()
        {
            base.Channel.Exit();
        }
        public void Print()
        {
            base.Channel.Print();
        }
        #endregion
    }
}
