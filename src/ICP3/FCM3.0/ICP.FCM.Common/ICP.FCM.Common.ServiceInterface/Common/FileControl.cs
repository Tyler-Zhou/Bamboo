using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ICP.FCM.Common.ServiceInterface.Common
{
    /**/
    /// <summary>
    /// 文件控制类
    /// </summary>
    public class FileControl
    {
        public FileControl()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /**/
        /// <summary>
        /// 判断是否存在指定文件
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="FileName"></param>
        public void IsCreateFile(string Path)
        {
            if (Directory.Exists(Path))
            { }
            else
            {
                FileInfo CreateFile = new FileInfo(Path); //创建文件 
                if (!CreateFile.Exists)
                {
                    FileStream FS = CreateFile.Create();
                    FS.Close();
                }
            }
        }
        /**/
        /// <summary> 
        /// 在根目录下创建文件夹 
        /// </summary> 
        /// <param name="FolderPath">要创建的文件路径</param> 
        public void CreateFolder(string FolderPathName)
        {
            if (FolderPathName.Trim().Length > 0)
            {
                try
                {
                    if (!Directory.Exists(FolderPathName))
                    {
                        Directory.CreateDirectory(FolderPathName);
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        /**/
        /// <summary> 
        /// 删除一个文件夹下面的字文件夹和文件 
        /// </summary> 
        /// <param name="FolderPathName"></param> 
        public void DeleteChildFolder(string FolderPathName)
        {
            if (FolderPathName.Trim().Length > 0)
            {
                try
                {
                    if (Directory.Exists(FolderPathName))
                    {
                        Directory.Delete(FolderPathName, true);
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        /**/
        /// <summary> 
        /// 删除一个文件 
        /// </summary> 
        /// <param name="FilePathName"></param> 
        public void DeleteFile(string FilePathName)
        {
            try
            {
                FileInfo DeleFile = new FileInfo(FilePathName);
                DeleFile.Delete();
            }
            catch
            {
            }
        }
        /// <summary>
        /// 建立一个文件
        /// </summary>
        /// <param name="FilePathName"></param>
        public void CreateFile(string FilePathName)
        {
            try
            {
                //创建文件夹 
                //string[] strPath = FilePathName.Split('/');
                //CreateFolder(FilePathName.Replace("/" + strPath[strPath.Length - 1].ToString(), "")); //创建文件夹 
                FileInfo CreateFile = new FileInfo(FilePathName); //创建文件 
                if (!CreateFile.Exists)
                {
                    FileStream FS = CreateFile.Create();
                    FS.Close();
                }

            }
            catch
            {
            }
        }
        /**/
        /// <summary> 
        /// 删除整个文件夹及其字文件夹和文件 
        /// </summary> 
        /// <param name="FolderPathName"></param> 
        public void DeleParentFolder(string FolderPathName)
        {
            try
            {
                DirectoryInfo DelFolder = new DirectoryInfo(FolderPathName);
                if (DelFolder.Exists)
                {
                    DelFolder.Delete();
                }
            }
            catch
            {
            }
        }
        /**/
        /// <summary> 
        /// 在文件里追加内容 
        /// </summary> 
        /// <param name="FilePathName"></param> 
        public void ReWriteReadinnerText(string FilePathName, string WriteWord)
        {
            try
            {
                //建立文件夹和文件 
                //CreateFolder(FilePathName); 
                CreateFile(FilePathName);
                //得到原来文件的内容 
                FileStream FileRead = new FileStream(FilePathName, FileMode.Open, FileAccess.ReadWrite);
                StreamReader FileReadWord = new StreamReader(FileRead, System.Text.Encoding.Default);
                string OldString = FileReadWord.ReadToEnd().ToString();
                OldString = OldString + WriteWord;
                //把新的内容重新写入 
                StreamWriter FileWrite = new StreamWriter(FileRead, System.Text.Encoding.Default);
                FileWrite.Write(WriteWord);
                //关闭 
                FileWrite.Close();
                FileReadWord.Close();
                FileRead.Close();
            }
            catch
            {
                // throw; 
            }
        }


        /// <summary> 
        /// 在文件里追加内容 
        /// </summary> 
        /// <param name="FilePathName"></param> 
        public void WriteReadinnerText(string FilePathName, string WriteWord)
        {
            try
            {
                //建立文件夹和文件 
                //CreateFolder(FilePathName); 
                CreateFile(FilePathName);
                //得到原来文件的内容 
                FileStream FileRead = new FileStream(FilePathName, FileMode.Open, FileAccess.ReadWrite);
                StreamReader FileReadWord = new StreamReader(FileRead, System.Text.Encoding.Default);

                //把新的内容重新写入 
                StreamWriter FileWrite = new StreamWriter(FileRead, System.Text.Encoding.Default);

                FileWrite.Write(WriteWord);
                //关闭 
                FileWrite.Close();
                FileReadWord.Close();
                FileRead.Close();
            }
            catch
            {
                // throw; 
            }
        }

        /**/
        /// <summary> 
        /// 读取文件里内容 
        /// </summary> 
        /// <param name="FilePathName"></param> 
        public string ReaderFileData(string FilePathName)
        {
            try
            {

                FileStream FileRead = new FileStream(FilePathName, FileMode.Open, FileAccess.Read);
                StreamReader FileReadWord = new StreamReader(FileRead, System.Text.Encoding.Default);
                string TxtString = FileReadWord.ReadToEnd().ToString();
                //关闭 
                FileReadWord.Close();
                FileRead.Close();
                return TxtString;
            }
            catch
            {
                throw;
            }
        }


        public byte[] ReadFully(string filePathName)
        {

            // 初始化一个32k的缓存
            using (FileStream stream = new FileStream(filePathName, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[32768];
                using (MemoryStream ms = new MemoryStream())
                { //返回结果后会自动回收调用该对象的Dispose方法释放内存

                    // 不停的读取
                    while (true)
                    {

                        int read = stream.Read(buffer, 0, buffer.Length);

                        // 直到读取完最后的3M数据就可以返回结果了

                        if (read <= 0)

                            return ms.ToArray();

                        ms.Write(buffer, 0, read);

                    }
                }
                stream.Close();
            }
        }

        /// <summary> 
        /// 读取文件夹的文件 
        /// </summary> 
        /// <param name="FilePathName"></param> 
        /// <returns></returns> 
        public DirectoryInfo checkValidSessionPath(string FilePathName)
        {
            try
            {
                DirectoryInfo MainDir = new DirectoryInfo(FilePathName);
                return MainDir;

            }
            catch
            {
                throw;
            }
        }
    }
}
