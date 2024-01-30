using System;
using System.Collections.Generic;
using System.IO;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
namespace ICP.DataCache.ServiceInterface.File
{
    public abstract class BaseFileConverter : IFileConverter
    {
        public IFileConvertService FileConvertService
        {
            get
            {
               return ServiceClient.GetService<IFileConvertService>();
            }
        }
        String tempPath = IOHelper.HtmlTempPath;
        public BaseFileConverter()
        {
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }
        }
        #region IFileConverter 成员
        /// <summary>
        /// 文件转换后的存放路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns>文件转换后的存放路径。注意：BaseFileConverter返回空</returns>
        public virtual void Convert(String path)
        {
            if (!System.IO.File.Exists(path))
                throw new FileNotFoundException(String.Format("file:{0} doesn't exists.", path));
            FileOriginalName = Path.GetFileNameWithoutExtension(path);
            FileNewPath = Path.Combine(TempPath, FileOriginalName + ".pdf");
        }


        protected void CopyFile(String path)
        {
            String destPath = Path.Combine(TempPath, Path.GetFileName(path));
            System.IO.File.Copy(path, destPath, true);
            FileNewPath = destPath;
        }


        #endregion

        #region IFileConverter 成员

        public abstract FileType FileType { get; }


        public String TempPath
        {
            get
            {
                return tempPath;
            }

        }
        private String fileOriginalName;
        public String FileOriginalName
        {
            get { return fileOriginalName; }
            set { fileOriginalName = value; }
        }
        private String fileNewPath;
        public String FileNewPath
        {
            get { return fileNewPath; }
            set { fileNewPath = value; }
        }
        public abstract List<String> FileExtensions
        {
            get;
        }
        #endregion

   
    }
}
