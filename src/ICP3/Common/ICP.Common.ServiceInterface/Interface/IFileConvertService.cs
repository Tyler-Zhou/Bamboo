using System;
using System.Collections.Generic;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.Common.ServiceInterface
{  
    /// <summary>
    /// 文件转换服务接口
    /// </summary>
    [ServiceContract]
    [ICPServiceHost]
   public interface IFileConvertService
    {    
        /// <summary>
        /// 将邮件文件转换为PDF
        /// </summary>
        /// <param name="mailFile">邮件文件路径</param>
        /// <returns>转换后产生的文件所在路径</returns>
        [OperationContract]
        string ConvertMail2PDF(string mailFile);
        /// <summary>
        /// 合并多个PDF文件产生单个PDF文件
        /// </summary>
        /// <param name="fileList">PDF文件列表</param>
        /// <returns>转换后产生的文件所在路径</returns>
        [OperationContract]
        String MergePDFFiles(List<string> fileList);
        /// <summary>
        /// 将图片转换为PDF文件
        /// </summary>
        /// <param name="imageFilePath">图片文件路径</param>
        /// <returns>转换后产生的文件所在路径</returns>
        [OperationContract]
        string ConvertImage2PDF(String imageFilePath);
        /// <summary>
        /// 将图片转换为PDF文件
        /// </summary>
        /// <param name="txtFilePath">文本文件路径</param>
        /// <returns>转换后产生的文件所在路径</returns>
        [OperationContract]
        String ConvertText2PDF(String txtFilePath);
        /// <summary>
        /// 将Word文件转换为PDF
        /// </summary>
        /// <param name="wordFilePath">word文件路径</param>
        /// <returns>转换后产生的文件所在路径</returns>
        [OperationContract]
        String ConvertWord2PDF(String wordFilePath);
        /// <summary>
        /// 将PPT转换为PDF文件
        /// </summary>
        /// <param name="pptFilePath">ppt文档所在路径</param>
        /// <returns>转换后产生的文件所在路径</returns>
        [OperationContract]
        String ConvertPPT2PDF(String pptFilePath);
        /// <summary>
        /// 将Excel文件转换为PDF文件
        /// </summary>
        /// <param name="excelFilePath">excel文件所在路径</param>
        /// <returns>转换后产生的文件所在路径</returns>
        [OperationContract]
        String ConvertExcel2PDF(String excelFilePath);
        /// <summary>
        /// 将html文件转换为PDF文件
        /// </summary>
        /// <param name="htmlFilePath">html文件所在路径</param>
        /// <returns>转换后产生的文件所在路径</returns>
        [OperationContract]
        string ConvertHtml2PDF(string htmlFilePath);


    }
}
