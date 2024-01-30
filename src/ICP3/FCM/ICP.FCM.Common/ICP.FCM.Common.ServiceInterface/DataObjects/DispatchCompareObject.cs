using System;
using System.Collections.Generic;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.Common.ServiceInterface
{
    //joe 2013-06-14 init
    /// <summary>
    /// 分文件比较类。
    /// </summary>
    [Serializable]
    public partial class DispatchCompareObject : BaseDataObject
    {
        /// <summary>
        ///港前业务订单信息
        /// </summary>

        /// <summary>
        /// 海出订单信息
        /// </summary>
        public OceanBusinessInfo OEBookingInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 海出主提单信息
        /// </summary>
        public OceanBusinessMBLList OEMBLInfo
        {
            get;
            set;
        }

         /// <summary>
        /// 海出分提单信息列表
        /// </summary>
        public List<OceanBusinessHBLList> OEHBLList
        {
            get;
            set;
        }

        /// <summary>
        /// 海出集装箱信息列表
        /// </summary>
        public List<OIBusinessContainerList> OEContainerList
        {
            get;
            set;
        }
      

        
        /// <summary>
        /// 港后业务订单信息
        /// </summary>
        public OceanBusinessInfo OIOceanBusinessInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 港前业务号
        /// </summary>
        public Guid OEOperationID
        {
            get;
            set;
        }
        /// <summary>
        /// 港后业务号
        /// </summary>
        public Guid OIOperationID;



  
        /// <summary>
        /// 港前业务账单信息列表
        /// </summary>
        public List<BillInfo> OEBillInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 港后业务账单信息列表
        /// </summary>
        public List<BillInfo> OIBillInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        

    }

    /// <summary>
    /// 费用比较信息
    /// </summary>
    [Serializable]
    public class Fee : BaseDataObject
    {

       /// <summary>
       /// 费用ID
       /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 账单ID
        /// </summary>
        public string BillID { get; set; }

        /// <summary>
        /// 费用类型1为应收，2为应付
        /// </summary>
        public int Way { get; set; }

        /// <summary>
        /// 费用代码
        /// </summary>
        public string ChargeCode { get; set; }

        /// <summary>
        /// 费用名称
        /// </summary>
        public string ChargeName { get; set; }

        /// <summary>
        /// 费用是否代理
        /// </summary>
        public int IsAgent { get; set; }

        /// <summary>
        /// 上次费用总和
        /// </summary>
        public string OldSumMoney { get; set; }

        /// <summary>
        /// 上次费用说明
        /// </summary>
        public string OldRemark { get; set; }

        /// <summary>
        /// 本次费用总和
        /// </summary>
        public string NewSumMoney { get; set; }

        /// <summary>
        /// 本次费用说明
        /// </summary>
        public string NewRemark { get; set; }

        /// <summary>
        /// 修改状态0:Same,1:Add,2:Modify,3:Delete,4:SelfAdd
        /// </summary>
        public int UpdateState { get; set; }
        /// <summary>
        /// 当前引用费用ID
        /// </summary>
        public Guid? RefFeeID { get; set; }
        /// <summary>
        /// 分发费用ID
        /// </summary>
        public Guid? DispatchFeeID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }


    /// <summary>
    /// 提单比较信息  [AMSNo],[ISFNo],[ShipperID] , [ShipperDescription],[ReceiveOBLDate] 
    /// </summary>
    [Serializable]
    public class HBLInfo : BaseDataObject
    {
        /// <summary>
        /// 原提单号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 原ISFNo号
        /// </summary>
        public string OldISFNo { get; set; }
        /// <summary>
        /// 原AMSNo号
        /// </summary>
        public string OldAMSNo { get; set; }
        /// <summary>
        /// 原收货人
        /// </summary>
        public string OldShipperID { get; set; }

        /// <summary>
        /// 原收到正本日
        /// </summary>
        public string OldReceiveOBLDate { get; set; }

        /// <summary>
        /// 原数量
        /// </summary>
        public string OldQuantity { get; set; }
        /// <summary>
        /// 原重量
        /// </summary>
        public string OldWeight { get; set; }

        /// <summary>
        /// 原体积
        /// </summary>
        public string OldMeasurement { get; set; }

        /// <summary>
        /// 原货物信息
        /// </summary>
        public string OldDescriptionOfGood { get; set; }

        /// <summary>
        /// 新ISFNo号
        /// </summary>
        public string NewISFNo { get; set; }
        /// <summary>
        /// 新AMSNo号
        /// </summary>
        public string NewAMSNo { get; set; }
        /// <summary>
        /// 新收货人
        /// </summary>
        public string NewShipperID { get; set; }

        /// <summary>
        /// 新收到正本日
        /// </summary>
        public string NewReceiveOBLDate { get; set; }

        /// <summary>
        /// 新数量
        /// </summary>
        public string NewQuantity { get; set; }
        /// <summary>
        /// 新重量
        /// </summary>
        public string NewWeight { get; set; }

        /// <summary>
        /// 新体积
        /// </summary>
        public string NewMeasurement { get; set; }

        /// <summary>
        /// 新货物信息
        /// </summary>
        public string NewDescriptionOfGood { get; set; }

    }

    /// <summary>
    /// 集装箱比较信息[No], [TypeID], [SealNo], [Quantity] ,[Weight],[Measurement]  IsPartOf  
    /// </summary>
    [Serializable]
    public class ContainerInfo : BaseDataObject
    {


      //,[Weight]
      //,[Measurement]
        /// <summary>
        /// 集装箱号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        ///  原集装箱类型
        /// </summary>
        public string OldContainerType { get; set; }


        /// <summary>
        ///  新集装箱类型
        /// </summary>
        public string NewContainerType { get; set; }


        /// <summary>
        /// 原提单号
        /// </summary>
        public string HBLNo { get; set; }
        

        /// <summary>
        /// 原封条号
        /// </summary>
        public string OldSealNo { get; set; }
        /// <summary>
        /// 原地点
        /// </summary>
        public string OldLocation { get; set; }

        /// <summary>
        /// 原PickUpNo
        /// </summary>
        public string OldPickUpNo { get; set; }

        /// <summary>
        /// 原监管仓日
        /// </summary>
        public string OldGODate { get; set; }
        /// <summary>
        /// 原可提货日
        /// </summary>
        public string OldAvailableDate { get; set; }

        /// <summary>
        /// 原免堆日
        /// </summary>
        public string OldLastFreeDate { get; set; }

        /// <summary>
        /// 原分单
        /// </summary>
        public string OldIsPartOf { get; set; }

        /// <summary>
        /// 原数量
        /// </summary>
        public string OldQuantity { get; set; }
        /// <summary>
        /// 原重量
        /// </summary>
        public string OldWeight { get; set; }

        /// <summary>
        /// 原体积
        /// </summary>
        public string OldMeasurement { get; set; }



        /// <summary>
        /// 新封条号
        /// </summary>
        public string NewSealNo { get; set; }
        /// <summary>
        /// 新地点
        /// </summary>
        public string NewLocation { get; set; }

        /// <summary>
        /// 新PickUpNo
        /// </summary>
        public string NewPickUpNo { get; set; }

        /// <summary>
        /// 新监管仓日
        /// </summary>
        public string NewGODate { get; set; }
        /// <summary>
        /// 新可提货日
        /// </summary>
        public string NewAvailableDate { get; set; }

        /// <summary>
        /// 新免堆日
        /// </summary>
        public string NewLastFreeDate { get; set; }

        /// <summary>
        /// 新分单
        /// </summary>
        public string NewIsPartOf { get; set; }

        /// <summary>
        /// 新数量
        /// </summary>
        public string NewQuantity { get; set; }
        /// <summary>
        ///新重量
        /// </summary>
        public string NewWeight { get; set; }

        /// <summary>
        /// 新体积
        /// </summary>
        public string NewMeasurement { get; set; }


       

    }
    /// <summary>
    /// 账单比较信息
    /// </summary>
    [Serializable]
    public class Bill : BaseDataObject
    {


        private List<Fee> lstfees = new List<Fee>();


        /// <summary>
        /// Bill号
        /// </summary>
        public string ID
        {
            get;
            set;
        }
        /// <summary>
        /// 业务号
        /// </summary>
        public string BusinessNO
        {
            get;
            set;
        }

        /// <summary>
        /// 海出账单号
        /// </summary>
        public string OEBillNO { get; set; }

        /// <summary>
        /// 海出费用币种
        /// </summary>
        public string OECurrencyName { get; set; }

        /// <summary>
        /// 海出账单总金额
        /// </summary>
        public string OEAmount { get; set; }

        /// <summary>
        /// 海进账单号
        /// </summary>
        public string OIBillNO { get; set; }

        /// <summary>
        /// 海进费用币种
        /// </summary>
        public string OICurrencyName { get; set; }

        /// <summary>
        /// 海出账单总金额
        /// </summary>
        public string OIAmount { get; set; }

        /// <summary>
        /// 费用列表
        /// </summary>
        public List<Fee> Fees
        {
            get { return lstfees; }
            set { lstfees = value; }
        }


        /// <summary>
        /// 修改状态0:Same,1:Add,2:Modify,3:Delete,4:SelfAdd
        /// </summary>
        public int UpdateState{get;set;}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }

    /// <summary>
    /// 比较分发业务信息修改字段
    /// </summary>
    [Serializable]
    public class BusinessUpdateField :BaseDataObject
    {

        /// <summary>
        /// 业务号
        /// </summary>
        public string BusinessNO
        {
            get;
            set;
        }
        /// <summary>
        /// 字段对应数据库的列名
        /// </summary>
        public string FieldDBName { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        ///  字段旧值
        /// </summary>
        public string OldValue { get; set; }


        /// <summary>
        ///  字段新值
        /// </summary>
        public string NewValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return ((BusinessUpdateField)obj).BusinessNO==BusinessNO;
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        public int IsUpdate { get; set; }

        private List<HBLInfo> hbls = new List<HBLInfo>();

        private List<ContainerInfo> containers = new List<ContainerInfo>();

        /// <summary>
        /// 提单信息比较
        /// </summary>
        public List<HBLInfo> HBLs
        {
            get { return hbls; }
            set { hbls = value; }
        }

        /// <summary>
        /// 集装箱信息比较
        /// </summary>
        public List<ContainerInfo> Containers
        {
            get { return containers; }
            set { containers = value; }
        }
    }

    /// <summary>
    /// 简单的业务信息（ID和NO）
    /// </summary>
    [Serializable]
    public class SimpleBusinnessInfo
    {


        /// <summary>
        /// 海进业务ID
        /// </summary>
        public Guid OIBusinessID
        {
            get;
            set;
        }
        /// <summary>
        /// 海进业务号
        /// </summary>
        public string OIBusinessNO
        {
            get;
            set;
        }
        /// <summary>
        /// 海出业务ID
        /// </summary>
        public Guid OEBusinessID
        {
            get;
            set;
        }
        /// <summary>
        /// 海出业务号
        /// </summary>
        public string OEBusinessNO
        {
            get;
            set;
        }

        /// <summary>
        /// 分发修订时间
        /// </summary>
        public DateTime DispatchDate
        {
            get;
            set;
        }

        /// <summary>
        /// 分发修订人
        /// </summary>
        public string DispatchUserName
        {
            get;
            set;
        }
        /// <summary>
        /// Remark
        /// </summary>
        public string Remark
        {
            get;
            set;
        }
        /// <summary>
        /// 签收时间
        /// </summary>
        public DateTime? AcceptDate
        {
            get;
            set;
        }

        /// <summary>
        /// 签收人
        /// </summary>
        public string AcceptUserName
        {
            get;
            set;
        }

        /// <summary>
        /// 业务类型
        /// </summary>
        public OperationType Type
        {
            get;
            set;
        }

        
    }

    /// <summary>
    /// 业务的代理，文件员，客服信息   joe
    /// </summary>
        [Serializable]
    public partial class BusinessAgentAndCustomInfoObject
    {
        /// <summary>
        /// 代理
        /// </summary>
        public string AgentName
        {
            get;
            set;
        }
        /// <summary>
        ///文件员
        /// </summary>
        public string FilerName
        {
            get;
            set;
        }
        /// <summary>
        /// 客服
        /// </summary>
        public string CustomerName
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 分发签收文档比较类型
    /// </summary>
    public enum DipatchCompareType
    {
       /// <summary>
       /// 海进签收
       /// </summary>
         OEAccept=0,

         /// <summary>
         /// 海出签收
         /// </summary>
         OIAccept=1,

         /// <summary>
         /// 海进签收历史
         /// </summary>
         OEAcceptHestory=2,

         /// <summary>
         /// 海出签收历史
         /// </summary>
         OIAcceptHestory = 3

    }

    /// <summary>
    /// 修改状态
    /// </summary>
    public enum UpdateState
    {

        /// <summary>
        /// 没有修改
        /// </summary>
        Same =0,

        /// <summary>
        /// 添加
        /// </summary>
        Add=1,

        /// <summary>
        /// 修改
        /// </summary>
        Modify=2,

        /// <summary>
        /// 删除
        /// </summary>
        Delete=3,

        /// <summary>
        /// 自己添加
        /// </summary>
        SelfAdd=4


    }


    /// <summary>
    /// 签收前后利润比较对象
    /// </summary>
    [Serializable]
    public class ProfitCompare : BaseDataObject
    {
        /// <summary>
        ///签收前利润
        /// </summary>
        public decimal OldProfit
        {
            get;
            set;
        }
        /// <summary>
        ///签收后利润
        /// </summary>
        public decimal NewProfit
        {
            get;
            set;
        } 
    }

    /// <summary>
    /// 分发文件对象
    /// </summary>
    [Serializable]
    public class DispatchFile : BaseDataObject
    {
        /// <summary>
        ///文件id
        /// </summary>
        public Guid OperationFileID
        {
            get;
            set;
        }
       
        /// <summary>
        ///业务id
        /// </summary>
        public Guid OperationID
        {
            get;
            set;
        }

        /// <summary>
        /// 业务类型
        /// </summary>
        public byte OperationType
        {
            get;
            set;
        }

        /// <summary>
        /// 形式
        /// </summary>
        public byte FormType
        {
            get;
            set;
        }

        /// <summary>
        /// 文件类型
        /// </summary>
        public byte DocumentType
        {
            get;
            set;
        }

        /// <summary>
        /// 文件类型名称
        /// </summary>
        public string DocumentTypeName
        {
            get;
            set;
        }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CreateByID
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreateByName
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            get;
            set;
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get;
            set;
        }

        /// <summary>
        /// 文件来源
        /// </summary>
        public byte FileSource
        {
            get;
            set;
        }
    }


}
