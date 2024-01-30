using System;

namespace ICP.DataCache.FileSystem
{
    public class FileUtility
    {
        static FileUtility()
        {
            if (System.IO.Directory.Exists(FileStoreRootPath))
                System.IO.Directory.CreateDirectory(FileStoreRootPath);
        }
        public static String FileStoreRootPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileStore");

       
    }
   
}
