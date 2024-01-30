using System;
using System.Drawing;
using System.ServiceModel;

namespace ICP.FilePreviewServiceLibrary
{   
    /// <summary>
    /// 文档预览服务接口
    /// </summary>
    [ServiceContract]
   public interface IFilePreviewService
    {   
        /// <summary>
        /// 预览文档
        /// </summary>
        /// <param name="filePath">文档的绝对路径</param>
        /// <param name="location">预览窗口左上角位置</param>
        /// <param name="size">预览窗口大小</param>
        /// <param name="isAutoHide">预览窗口是否定时隐藏</param>
        [OperationContract]
       void Preview(String filePath, Point location, Size size,bool isAutoHide);
        /// <summary>
        /// 隐藏预览窗口
        /// </summary>
        [OperationContract]
        void Hide();
        /// <summary>
        /// 退出预览程序
        /// </summary>
        [OperationContract]
        void Exit();
        /// <summary>
        /// 打印
        /// </summary>
        [OperationContract]
        void Print();
    }
}
