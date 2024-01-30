using System;
using System.Data.SqlServerCe;
using System.IO;
using System.IO.Compression;

namespace ICP.DataSynchronization.ServiceInterface
{
    public class DataSynchronizationUtility
    {

        /// <summary>
        /// SDF文件损坏后，异常处理捕捉NativeError  Code
        /// </summary>
        public static int DataBaseCorruptCode = 25017;

        /// <summary>
        /// 同步域名
        /// </summary>
        public static String ScopeName { get { return "ICP35Document"; } }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public static String ClientDatabaseName{ get{ return CacheDataBaseName+".sdf"; } }

        /// <summary>
        /// 缓存数据库名称
        /// </summary>
        public static String CacheDataBaseName { get { return "ICP35DataCache"; } }

        public static string ConnectionStringTemplate = "Data Source={0};Max Database Size=4000;Max Buffer Size =1100;Persist Security Info=False;SSCE:Default Lock Timeout=10000";
        /// <summary>
        /// 数据同步表名数组
        /// </summary>
        public static String[] SyncAdapterTables = new String[] { "sm.Languages", "fcm.OperationContactDomainCaches", "fcm.OperationContactCache", "fcm.OperationViewOECache", "fcm.OperationMessages" };
        public static string OEOperationViewTableName = "fcm.OperationViewOECache";
        public static string OperationContactDomainCacheTableName = "fcm.OperationContactDomainCaches";
        public static string OperationMessageTableName = "fcm.OperationMessages";
        public static string OperationContactTableName = "fcm.OperationContactCache";
        /// <summary>
        /// 数据同步表对应的主键列名
        /// </summary>
        public static String[] SyncAdapterTablePrimaryKeys = new String[] { "ID" };
        /// <summary>
        /// 数据同步中已被删除行的删除标记维持时间
        /// </summary>
        public static int TombstoneAgingInHours = 10;

        /// <summary>
        /// 修复数据库SDF文件
        /// </summary>
        /// <param name="connectionString"></param>
        public static void RepairDataBase(string connectionString)
        {
            SqlCeEngine engine = new SqlCeEngine(connectionString);
            engine.Repair(connectionString,
                false == engine.Verify() ? RepairOption.RecoverAllPossibleRows : RepairOption.RecoverAllOrFail);
            engine.Dispose();
        }
        /// <summary>
        /// 压缩流
        /// </summary>
        /// <param name="sourceStream"></param>
        /// <returns></returns>
        public static Stream DecompressStream(Stream sourceStream)
        {
            MemoryStream responseStream = new MemoryStream();
            using (GZipStream compressedStream = new GZipStream(responseStream, CompressionMode.Compress, true))
            {


                byte[] buffer = new byte[sourceStream.Length];
                int checkCounter = sourceStream.Read(buffer, 0, buffer.Length);
                if (checkCounter != buffer.Length) throw new ApplicationException();

                compressedStream.Write(buffer, 0, buffer.Length);

            }
            responseStream.Position = 0;
            return responseStream;

        }

        /// <summary>
        /// 解压缩流
        /// </summary>
        /// <param name="sourceStream"></param>
        /// <returns></returns>
        public static Stream UncompressStream(Stream sourceStream)
        {
            MemoryStream destinationStream = new MemoryStream();
            byte[] quartetBuffer = new byte[4];
            int position = (int)sourceStream.Length - 4;
            sourceStream.Position = position;
            sourceStream.Read(quartetBuffer, 0, 4);
            sourceStream.Position = 0;
            int checkLength = BitConverter.ToInt32(quartetBuffer, 0);
            byte[] buffer = new byte[checkLength + 1000];
            using (GZipStream decompressedStream = new GZipStream(sourceStream, CompressionMode.Decompress, true))
            {
                int total = 0;
                for (int offset = 0; ; )
                {
                    int bytesRead = decompressedStream.Read(buffer, offset, 1000);
                    if (bytesRead == 0) break;
                    offset += bytesRead;
                    total += bytesRead;
                }

                destinationStream.Write(buffer, 0, total);


            }
            destinationStream.Position = 0;
            return destinationStream;

        }


    }
}
