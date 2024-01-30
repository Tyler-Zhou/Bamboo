using ICP.Framework.CommonLibrary.Common;
using System;

namespace ICP.EDI.ServiceInterface
{
    #region EDI预览值
    /// <summary>
    /// EDI预览值
    /// </summary>
    [Serializable]
    public class EDIPreviewValue
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 箱号
        /// </summary>
        public string ContainerNo { get; set; }

        /// <summary>
        /// 节点
        /// </summary>
        public string Node { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public string Sourse { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }
    #endregion
    /// <summary>
    /// 获取上级节点数据
    /// </summary>
    [Serializable]
    public class EDINode
    {
        /// <summary>
        /// 提单编号
        /// </summary>
        [GuidRequired(CMessage = "提单编号", EMessage = "BL No")]
        public string No { get; set; }

        /// <summary>
        /// 大船编号
        /// </summary>
        [GuidRequired(CMessage = "船编号", EMessage = "UNCode")]
        public string UNCode { get; set; }

        /// <summary>
        /// 船名
        /// </summary>
        [GuidRequired(CMessage = "船名", EMessage = "Vessel")]
        public string Vessel { get; set; }

        /// <summary>
        /// 航次
        /// </summary>
        [GuidRequired(CMessage = "航次", EMessage = "Voyage")]
        public string Voyage { get; set; }

        /// <summary>
        /// ETD
        /// </summary>
        [GuidRequired(CMessage = "离港日", EMessage = "ETD")]
        public string ETD { get; set; }

        /// <summary>
        /// 收货地点编号
        /// </summary>
        [GuidRequired(CMessage = "收货地编号", EMessage = "PlaceOfReceipt Code")]
        public string ReceiptCode { get; set; }

        /// <summary>
        /// 收货地点
        /// </summary>
        [GuidRequired(CMessage = "收货地", EMessage = "PlaceOfReceipt")]
        public string Receipt { get; set; }

        /// <summary>
        /// 装货港编号
        /// </summary>
        [GuidRequired(CMessage = "装货港编号", EMessage = "POL Code")]
        public string LoadPortCode { get; set; }

        /// <summary>
        /// 装货港
        /// </summary>
        [GuidRequired(CMessage = "装货港", EMessage = "POL")]
        public string LoadPort { get; set; }

        /// <summary>
        /// 卸货港编号
        /// </summary>
        [GuidRequired(CMessage = "卸货港编号", EMessage = "POD Code")]
        public string DischargePortCode { get; set; }

        /// <summary>
        /// 卸货港
        /// </summary>
        [GuidRequired(CMessage = "卸货港", EMessage = "POD")]
        public string DischargePort { get; set; }

        ///</summary>
        /// 交货地编号
        /// </summary>
        [GuidRequired(CMessage = "交货地编号", EMessage = "PlaceOfDelivery Code")]
        public string DeliveryPortCode { get; set; }

        /// <summary>
        /// 交货地
        /// </summary>
        [GuidRequired(CMessage = "交货地", EMessage = "PlaceOfDelivery")]
        public string DeliveryPort { get; set; }

        /// <summary>
        /// 订舱号
        /// </summary>
        [GuidRequired(CMessage = "订舱号", EMessage = "BookingNO")]
        public string BookingNO { get; set; }

        /// <summary>
        /// 付款类型
        /// </summary>
        [GuidRequired(CMessage = "付款类型", EMessage = "PaymentTerm")]
        public string PaymentTerm { get; set; }

        /// <summary>
        /// HSCode
        /// </summary>
        [GuidRequired(CMessage = "HSCode", EMessage = "HSCode")]
        public string HSCode { get; set; }

        /// <summary>
        /// 放单类型
        /// </summary>
        [GuidRequired(CMessage = "放单类型", EMessage = "ReleaseType")]
        public string ReleaseType { get; set; }

        /// <summary>
        /// 合约号
        /// </summary>
        [GuidRequired(CMessage = "合约号", EMessage = "SCNO")]
        public string SCNO { get; set; }

        /// <summary>
        /// 发货人名称
        /// </summary>
        [GuidRequired(CMessage = "发货人名称", EMessage = "Shipper Name")]
        public string ShipperName { get; set; }

        /// <summary>
        /// 发货人地址
        /// </summary>
        [GuidRequired(CMessage = "发货人地址", EMessage = "Shipper Address")]
        public string ShipperAddress { get; set; }

        /// <summary>
        /// 发货人电话
        /// </summary>
        [GuidRequired(CMessage = "发货人电话", EMessage = "Shipper Tel")]
        public string ShipperTel { get; set; }

        /// <summary>
        /// 发货人传真
        /// </summary>
        [GuidRequired(CMessage = "发货人传真", EMessage = "Shipper Fax")]
        public string ShipperFax { get; set; }

        /// <summary>
        /// 收货人名称
        /// </summary>
        [GuidRequired(CMessage = "收货人名称", EMessage = "Consignee Name")]
        public string ConsigneeName { get; set; }

        /// <summary>
        /// 收货人地址
        /// </summary>
        [GuidRequired(CMessage = "收货人地址", EMessage = "Consignee Address")]
        public string ConsigneeAddress { get; set; }

        /// <summary>
        /// 收货人电话
        /// </summary>
        [GuidRequired(CMessage = "收货人电话", EMessage = "Consignee Tel")]
        public string ConsigneeTel { get; set; }

        /// <summary>
        /// 收货人传真
        /// </summary>
        [GuidRequired(CMessage = "收货人传真", EMessage = "Consignee Fax")]
        public string ConsigneeFax { get; set; }

        /// <summary>
        /// 通知人名称
        /// </summary>
        [GuidRequired(CMessage = "通知人名称", EMessage = "Notify Name")]
        public string NotifyName { get; set; }

        /// <summary>
        /// 通知人地址
        /// </summary>
        [GuidRequired(CMessage = "通知人地址", EMessage = "Notify Address")]
        public string NotifyAddress { get; set; }

        /// <summary>
        /// 通知人电话
        /// </summary>
        [GuidRequired(CMessage = "通知人电话", EMessage = "Notify Tel")]
        public string NotifyTel { get; set; }

        /// <summary>
        /// 通知人传真
        /// </summary>
        [GuidRequired(CMessage = "通知人传真", EMessage = "Notify Fax")]
        public string NotifyFax { get; set; }

        /// <summary>
        /// 件数
        /// </summary>
        [GuidRequired(CMessage = "件数", EMessage = "Quantity")]
        public string Qty { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        [GuidRequired(CMessage = "重量", EMessage = "Weight")]
        public string Weight { get; set; }

        /// <summary>
        /// 体积
        /// </summary>
        [GuidRequired(CMessage = "体积", EMessage = "Volume")]
        public string Volume { get; set; }

        /// <summary>
        /// 品名
        /// </summary>
        [GuidRequired(CMessage = "品名", EMessage = "GoodsDescription")]
        public string CargoDESC { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [GuidRequired(CMessage = "备注", EMessage = "Remarks")]
        public string Remarks { get; set; }

        /// <summary>
        /// 运输条款
        /// </summary>
        [GuidRequired(CMessage = "运输条款", EMessage = "TransportClause")]
        public string TransportClauseName { get; set; }

        /// <summary>
        /// 唛头
        /// </summary>
        [GuidRequired(CMessage = "唛头", EMessage = "Marks")]
        public string Marks { get; set; }

        /// <summary>
        /// 箱型
        /// </summary>
        [GuidRequired(CMessage = "箱型", EMessage = "Container")]
        public string Container { get; set; }

        /// <summary>
        /// 箱类
        /// </summary>
        [GuidRequired(CMessage = "箱类", EMessage = "CagoType")]
        public string CagoType { get; set; }

        /// <summary>
        /// 摄氏度
        /// </summary>
        [GuidRequired(CMessage = "摄氏度", EMessage = "Centigrade")]
        public string Centigrade { get; set; }

        /// <summary>
        /// 危险品类型
        /// </summary>
        [GuidRequired(CMessage = "危险品类型", EMessage = "Dangerous Class")]
        public string DangerousClass { get; set; }

        /// <summary>
        /// 危险品编号
        /// </summary>
        [GuidRequired(CMessage = "危险品编号", EMessage = "Dangerous NO")]
        public string DangerousNo { get; set; }
    }

    /// <summary>
    /// 获取EDI箱信息
    /// </summary>
    [Serializable]
    public class EDIChildNode
    {
        /// <summary>
        /// 提单编号
        /// </summary>
        [GuidRequired(CMessage = "提单编号", EMessage = "BL No")]
        public string No { get; set; }

        /// <summary>
        /// 箱号
        /// </summary>
        [GuidRequired(CMessage = "箱号", EMessage = "ContainerNo")]
        public string ContainerNo { get; set; }

        /// <summary>
        /// 封条号
        /// </summary>
        [GuidRequired(CMessage = "封条号", EMessage = "SealNo")]
        public string SealNo { get; set; }

        /// <summary>
        /// 件数
        /// </summary>
        [GuidRequired(CMessage = "件数", EMessage = "Quantity")]
        public string Qty { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        [GuidRequired(CMessage = "重量", EMessage = "Weight")]
        public string Weight { get; set; }

        /// <summary>
        /// 体积
        /// </summary>
        [GuidRequired(CMessage = "体积", EMessage = "Volume")]
        public string Volume { get; set; }

        /// <summary>
        /// 箱型
        /// </summary>
        [GuidRequired(CMessage = "箱型", EMessage = "Container Type")]
        public string ContainerType { get; set; }

    }
}
