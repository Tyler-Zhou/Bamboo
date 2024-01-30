using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using System;

namespace ICP.EDI.ServiceInterface.DataObjects
{

    #region EDI配置信息
    /// <summary>
    /// EDI配置信息
    /// </summary>
    [Serializable]
    public class EDIConfigItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 主题前缀
        /// </summary>
        public string SubjectPrefix { get; set; }
        /// <summary>
        /// EDI 模式
        /// </summary>
        public EDIMode EDIMode { get; set; }
        /// <summary>
        /// 存储过程
        /// </summary>
        public string StoredProcedure { get; set; }
        /// <summary>
        /// 数据格式
        /// </summary>
        public string DataFormat { get; set; }
        /// <summary>
        /// 规则文件
        /// </summary>
        public string RegularFile { get; set; }
        /// <summary>
        /// 插件类型
        /// </summary>
        public EDIPluginType PluginType { get; set; }
        /// <summary>
        /// 插件组件
        /// </summary>
        public string Component { get; set; }
        /// <summary>
        /// 文件格式
        /// </summary>
        public string FileFormat { get; set; }
        /// <summary>
        /// 上传方式
        /// </summary>
        public EDIUploadMode UploadMode { get; set; }
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string ServerAddress { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 磁盘路径
        /// </summary>
        public string DiskPath { get; set; }
        /// <summary>
        /// 接收地址
        /// </summary>
        public string ReceiveAddress { get; set; }
        /// <summary>
        /// FTP目录
        /// </summary>
        public string FTPPath { get; set; }
        /// <summary>
        /// 服务名
        /// </summary>
        public string ServiceConfigureKeyName { get; set; }
        /// <summary>
        /// 船东
        /// </summary>
        public string CarrierName { get; set; }
        /// <summary>
        /// 接收地址
        /// </summary>
        public string ToAddress { get; set; }
        /// <summary>
        /// 中海web地址
        /// </summary>
        public string CSCLWebURL { get; set; }
        /// <summary>
        /// 中海登录名
        /// </summary>
        public string CSCLLoginName { get; set; }
        /// <summary>
        /// 中海登录密码
        /// </summary>
        public string CSCLPassword { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        public string ComapnyAddress { get; set; }
    }
    #endregion



    #region 发送EDI参数
    /// <summary>
    /// 发送EDI参数
    /// </summary>
    [Serializable]
    public class EDISendOption
    {
        /// <summary>
        /// 项目名,例:HANJIN.Booking
        /// </summary>
        public string ServiceKey { get; set; }
        /// <summary>
        /// 发送类型，Cancel 1  , Replace 5 ，Original 9
        /// </summary>
        public EDIFlagType Flag { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 当前发送EDI内容
        /// </summary>
        public string CurrentContent { get; set; }
        /// <summary>
        /// 当前发送EDI内容(Byte[])
        /// </summary>
        public byte[] CurrentData { get; set; }
        /// <summary>
        /// 当前发送人
        /// </summary>
        public string CurrentSender { get; set; }
        /// <summary>
        /// 当前接收人
        /// </summary>
        public string CurrentReceiver { get; set; }
        /// <summary>
        /// 操作口岸公司id
        /// </summary>
        public Guid CompanyID { get; set; }
        /// <summary>
        /// 船东ID
        /// </summary>
        public Guid CarrierID { get; set; }
        /// <summary>
        /// 承运人ID
        /// </summary>
        public Guid AgentOfCarrierID { get; set; }
        /// <summary>
        /// 订舱人
        /// </summary>
        public Guid BookingPartyID { get; set; }
        /// <summary>
        /// 类型,例:Booking/SI/AMS……
        /// </summary>
        public EDIMode EdiMode { get; set; }
        /// <summary>
        /// 发送人id
        /// </summary>
        public Guid SendByID { get; set; }
        private string documentno;
        /// <summary>
        /// 文档编号
        /// </summary>
        public string DocumentNO
        {
            get
            {
                if (documentno.IsNullOrEmpty())
                    documentno = Subject;
                return documentno;
            }
            set
            {
                documentno = value;
            }
        }
        /// <summary>
        /// 主要表单ID集合
        /// </summary>
        public Guid[] IDs { get; set; }
        /// <summary>
        /// 主要表单编号集合
        /// </summary>
        public string[] NOs { get; set; }
        /// <summary>
        /// MBLID/HBLID
        /// </summary>
        public Guid[] FIDs { get; set; }
        /// <summary>
        /// 明细表单编号集合
        /// </summary>
        public string[] FNOs { get; set; }
        /// <summary>
        /// 船代
        /// </summary>
        public string Agent { get; set; }
        /// <summary>
        /// 船代名称
        /// </summary>
        public string AgentName { get; set; }
        /// <summary>
        /// 联系单位
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ContactTel { get; set; }
        /// <summary>
        /// 联系人Email
        /// </summary>
        public string ContactEmail { get; set; }
        /// <summary>
        /// 联系人Email
        /// </summary>
        public string VGMDate { get; set; }
        /// <summary>
        /// 联系人Email
        /// </summary>
        public string VGMRrmark { get; set; }
        /// <summary>
        /// 太平船务物品描述
        /// </summary>
        public string PILCommodity { get; set; }
        /// <summary>
        /// AMS条目类型（60=港口到港口61=内陆运输62=运输/出口63=立即再出口64=留在船上。）
        /// </summary>
        public AMSEntryType AMSEntryType { get; set; }
        /// <summary>
        /// ACI条目类型（23=转运到美国 24=进口 26=过境货）
        /// </summary>
        public ACIEntryType ACIEntryType { get; set; }
        /// <summary>
        /// 业务类型（出口，进口，空运，报关……）
        /// </summary>        
        public OperationType OperationType { get; set; }
        /// <summary>
        /// 发货人名称
        /// </summary>
        public string[] ShipperName { get; set; }
        /// <summary>
        /// 发货人名称(格式化)
        /// </summary>
        public string[] ShipperFormat { get; set; }
        /// <summary>
        /// 收货人名称(格式化)
        /// </summary>
        public string[] ConsigneeName { get; set; }
        /// <summary>
        /// 收货人名称(格式化)
        /// </summary>
        public string[] ConsigneeFormat { get; set; }
        /// <summary>
        /// 通知人名称
        /// </summary>
        public string[] NotifyName { get; set; }
        /// <summary>
        /// 通知人名称(格式化)
        /// </summary>
        public string[] NotifyFormat { get; set; }
        /// <summary>
        /// 其他信息(格式化)
        /// </summary>
        public string[] OtherFormat { get; set; }
        /// <summary>
        /// 货物信息(格式化)
        /// </summary>
        public string[] GoodinfoFormat { get; set; }
        /// <summary>
        /// 唛头(格式化)
        /// </summary>
        public string[] MarkFormat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int VersionNo { get; set; }
    }
    #endregion


    #region EDI服务配置
    ///// <summary>
    ///// EDI服务配置
    ///// </summary>
    //[Serializable]
    //public class ConfigItem
    //{
    //    /// <summary>
    //    /// 承运人
    //    /// </summary>
    //    public string Carrier { get; set; }


    //    /// <summary>
    //    /// 插件模块路径
    //    /// </summary>
    //    public string Assembly { get; set; }

    //    /// <summary>
    //    /// 邮箱配置项

    //    /// </summary>
    //    public EMailItem EMail { get; set; }

    //    /// <summary>
    //    /// FTP服务配置项
    //    /// </summary>
    //    public FTPItem Ftp { get; set; }

    //    /// <summary>
    //    /// 数据库配置项
    //    /// </summary>
    //    public DataBaseItem DataBase { get; set; }


    //    /// <summary>
    //    /// 发送EDI的文件格式(edi,txt,xml)
    //    /// </summary>
    //    public string EDIDocumentFormart { get; set; }



    //} 
    #endregion

    #region 数据库配置项
    ///// <summary>
    ///// 数据库配置项
    ///// </summary>
    //[Serializable]
    //public class DataBaseItem
    //{
    //    /// <summary>
    //    /// 输入数据集结构
    //    /// </summary>
    //    public string DataSchema { get; set; }

    //    /// <summary>
    //    /// 规则文件路径
    //    /// </summary>
    //    public string RuleFile { get; set; }

    //    /// <summary>
    //    /// 链接数据库
    //    /// </summary>
    //    public string ConnectString { get; set; }

    //    /// <summary>
    //    /// 储存过程()
    //    /// </summary>
    //    public string StoreProduce { get; set; }
    //} 
    #endregion

    #region 邮箱配置项
    ///// <summary>
    ///// 邮箱配置项
    ///// </summary>
    //[Serializable]
    //public class EMailItem
    //{
    //    /// <summary>
    //    /// 邮箱服务器地址
    //    /// </summary>
    //    public string ServerAddress { get; set; }

    //    /// <summary>
    //    /// 登陆邮箱服务器用户名
    //    /// </summary>
    //    public string UserName { get; set; }

    //    /// <summary>
    //    /// 陆邮箱服务器密码
    //    /// </summary>
    //    public string Password { get; set; }

    //    /// <summary>
    //    /// 船东接收EDI的邮箱 
    //    /// </summary>
    //    public string EMailAddress { get; set; }

    //} 
    #endregion

    #region FTP服务配置项
    ///// <summary>
    ///// FTP服务配置项
    ///// </summary>
    //[Serializable]
    //public class FTPItem
    //{
    //    /// <summary>
    //    /// FTP主机
    //    /// </summary>
    //    public string Host { get; set; }

    //    /// <summary>
    //    /// FTP路径
    //    /// </summary>
    //    public string Path { get; set; }

    //    /// <summary>
    //    /// FTP服务器用户名
    //    /// </summary>
    //    public string UserName { get; set; }

    //    /// <summary>
    //    /// FTP服务器密码
    //    /// </summary>
    //    public string Password { get; set; }
    //} 
    #endregion
}
