using System;
using System.Collections.Generic;

namespace NB.CLP
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class BillInfo
    {
        /// <summary>
        /// 1.提单号（必填）
        /// </summary>
        public string billNo { get; set; }
        /// <summary>
        /// 2.船名（必填）
        /// </summary>
        public string vesselName { get; set; }
        /// <summary>
        /// 3.航次（必填）
        /// </summary>
        public string voyageNo { get; set; }
        /// <summary>
        /// 4.航线代码
        /// </summary>
        public string lineCode { get; set; }
        /// <summary>
        /// 5.装货港名称（必填）
        /// </summary>
        public string loadPort { get; set; }
        /// <summary>
        /// 6.装货港代码
        /// </summary>
        public string loadPortCode { get; set; }
        /// <summary>
        /// 7.目的港
        /// </summary>
        public string deliveryPlace { get; set; }
        /// <summary>
        /// 8.目的港代码
        /// </summary>
        public string deliveryPlaceCode { get; set; }
        /// <summary>
        /// 9.卸货港（必填）
        /// </summary>
        public string destinationPort { get; set; }
        /// <summary>
        /// 10.卸货港代码
        /// </summary>
        public string destinationPortCode { get; set; }
        /// <summary>
        /// 11.交货地点
        /// </summary>
        public string receiptPlace { get; set; }
        /// <summary>
        /// 12.中转港代码（必填）
        /// </summary>
        public string transitPort { get; set; }
        /// <summary>
        /// 13.结算章
        /// </summary>
        public string clearingCode { get; set; }
        /// <summary>
        /// 14.船公司代码（必填）
        /// </summary>
        public string carrierCode { get; set; }
        /// <summary>
        /// 15.船公司
        /// </summary>
        public string carrier { get; set; }
        /// <summary>
        /// 16.箱子经营人代码
        /// </summary>
        public string ctnOperatorCode { get; set; }
        /// <summary>
        /// 17.发送方货代联系人
        /// </summary>
        public string forwarderContacts { get; set; }
        /// <summary>
        /// 18.发送方货代联系方式
        /// </summary>
        public string forwarderTel { get; set; }
        /// <summary>
        /// 19.发送方货代名称
        /// </summary>
        public string forwarderName { get; set; }
        /// <summary>
        /// 20.发送方货代邮件地址（操作人邮箱）
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 21.托运编号
        /// </summary>
        public string consignmentNo { get; set; }
        /// <summary>
        /// 22.箱型（必填）
        /// </summary>
        public string ctnType { get; set; }
        /// <summary>
        /// 23.装箱单编号（企业内部唯一编号）（必填）
        /// </summary>
        public string costcoNo { get; set; }
        /// <summary>
        /// 24.一代企业名称（必填）
        /// </summary>
        public string firstForwarderName { get; set; }
        /// <summary>
        /// 25.客户邮箱地址
        /// </summary>
        public string contactEmail { get; set; }
        /// <summary>
        /// 26.数据来源（1-HY，2-云海，3-99路，4-其他）
        /// </summary>
        public string dataSource { get; set; }
        /// <summary>
        /// 27.关联货代（企业中文名称）
        /// </summary>
        public string bookingOrgName { get; set; }
        /// <summary>
        /// 28.货代方客户名称代码
        /// </summary>
        public string clientCode { get; set; }
        /// <summary>
        /// 29.承运人类型（只针对泛洋发送邮件需求）
        /// </summary>
        public string carrierType { get; set; }
        /// <summary>
        /// 30.货物类型（只针对泛洋发送邮件需求）
        /// </summary>
        public string cargoType { get; set; }
        /// <summary>
        /// 31.装箱地点
        /// </summary>
        public string place { get; set; }
        /// <summary>
        /// 32.装箱要求																			
        /// </summary>
        public string ctnLoadRequire { get; set; }
        /// <summary>
        /// 33.装箱时间（YYYY-MM-DD HH:MM:SS）																			
        /// </summary>
        public string ctnLoadTime { get; set; }
        /// <summary>
        /// 34.装箱联系人																			
        /// </summary>
        public string ctnLoadContact { get; set; }
        /// <summary>
        /// 35.装箱联系人电话																			
        /// </summary>
        public string ctnLoadTel { get; set; }
        /// <summary>
        /// 36.装箱备注																			
        /// </summary>
        public string ctnRemark { get; set; }
        /// <summary>
        /// 37.对接客服名称																			
        /// </summary>
        public string customerName { get; set; }
        /// <summary>
        /// 38.对接客服电话																			
        /// </summary>
        public string customerTel { get; set; }
        /// <summary>
        /// 39.对接客服邮箱																			
        /// </summary>
        public string customerEmail { get; set; }
        /// <summary>
        /// 40.单证名称																			
        /// </summary>
        public string documentName { get; set; }
        /// <summary>
        /// 41.单证电话																			
        /// </summary>
        public string documentTel { get; set; }
        /// <summary>
        /// 42.单证邮箱																			
        /// </summary>
        public string documentEmail { get; set; }
        /// <summary>
        /// 43.截关时间																			
        /// </summary>
        public string closingDate { get; set; }
        /// <summary>
        /// 44.截单时间																			
        /// </summary>
        public string cutOffTime { get; set; }
        /// <summary>
        /// 45.是否退关（Y-是，N-不是）																			
        /// </summary>
        public string isShutOut { get; set; }
        /// <summary>
        /// 46.提箱堆场																			
        /// </summary>
        public string pickYardName { get; set; }
        /// <summary>
        /// 47.进港码头																			
        /// </summary>
        public string dockName { get; set; }
        /// <summary>
        /// 48.UN代码																			
        /// </summary>
        public string unCode { get; set; }
        /// <summary>
        /// 49.内外贸标志（N-内贸 W-外贸）																			
        /// </summary>
        public string tradeFlag { get; set; }
        /// <summary>
        /// 50.车队名称																			
        /// </summary>
        public string fleetName { get; set; }
        /// <summary>
        /// 51.合约号（申洋需求添加）																			
        /// </summary>
        public string contractNo { get; set; }
        /// <summary>
        /// 52.合约客户（申洋需求添加）																			
        /// </summary>
        public string contractCustomer { get; set; }
        /// <summary>
        /// 53.预计开航日期（YYYY-MM-DD HH:MM:SS）																			
        /// </summary>
        public string etd { get; set; }
        /// <summary>
        /// 54.订舱代理代码																			
        /// </summary>
        public string bookingOrgCode { get; set; }
        /// <summary>
        /// 55.业务对象代码																			
        /// </summary>
        public string busClientCode { get; set; }
        /// <summary>
        /// 56.码头放行截止时间（YYYY-MM-DD HH:MM:SS）																			
        /// </summary>
        public string yardClosingTime { get; set; }
        /// <summary>
        /// 57.ENS截止时间（YYYY-MM-DD HH:MM:SS）																			
        /// </summary>
        public string ensClosingTime { get; set; }
        /// <summary>
        /// 58.AMS截止时间（YYYY-MM-DD HH:MM:SS）																			
        /// </summary>
        public string amsClosingTime { get; set; }
        /// <summary>
        /// 59.作业号																			
        /// </summary>
        public string jobNo { get; set; }
        /// <summary>
        /// 60.发送类型（00-发送至无纸化箱单平台 10-发送至易港通 20-发送至海恒蓝）																			
        /// </summary>
        public string sendType { get; set; }
        /// <summary>
        /// 货物信息（必填）							
        /// </summary>
        public List<CargoInfo> cargoList { get; set; }
    }

    /// <summary>
    /// 货物信息
    /// </summary>
    [Serializable]
    public class CargoInfo
    {
        /// <summary>
        /// 1.分提单号（必填）																			
        /// </summary>
        public string otherBillNo { get; set; }
        /// <summary>
        /// 2.包装类型																			
        /// </summary>
        public string packageType { get; set; }
        /// <summary>
        /// 3.包装类型代码																			
        /// </summary>
        public string packageTypeCode { get; set; }
        /// <summary>
        /// 4.件数（必填）																			
        /// </summary>
        public int qty { get; set; }
        /// <summary>
        /// 5.毛重（必填）																			
        /// </summary>
        public decimal grossWeight { get; set; }
        /// <summary>
        /// 6.体积（必填）																			
        /// </summary>
        public decimal volume { get; set; }
        /// <summary>
        /// 7.货物名称																			
        /// </summary>
        public string cargoName { get; set; }
        /// <summary>
        /// 8.唛头																			
        /// </summary>
        public string remark { get; set; }
    }
}
