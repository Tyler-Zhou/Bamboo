using ICP.Framework.CommonLibrary.Attributes;
using System.IO;
using System.ServiceModel;

namespace ICPwcfInterface
{
    [ServiceContract]
    [StreamTransportService]
    public interface Interface
    {
        //测试方法
        [OperationContract]
        double Test(double a, double b);

        //客户端发送上传文件到服务端
        [OperationContract(Action = "UploadFile")]
        void ClintTransferFileToService(FileTransferMessage UploadFileInfo);//文件传输

        //客户端从服务端下载文件
        [OperationContract(Action = "DownloadFile")]
        FileTransferMessage ServiceTransferFileToClint(FileTransferMessage DownloadFileInfo);//文件传输

        //合并文件
        [OperationContract(Action = "MergeFile")]
        string MergeFile(string[] FileName);

    }
    [MessageContract]
    public class FileTransferMessage
    {
        [MessageHeader(MustUnderstand = true)]
        public string FilePath;//文件路径(包含文件名称)

        [MessageHeader(MustUnderstand = true)]
        public long WholeFileSize;//文件尺寸

        [MessageHeader(MustUnderstand = true)]
        public bool IsFirstTime_NeedCreateFile;//第一次传输时需要创建文件

        [MessageHeader(MustUnderstand = true)]
        public string Exception;//异常信息

        [MessageBodyMember(Order = 1)]
        public Stream DisivionFileStream;//文件分割传输流
    }
}
