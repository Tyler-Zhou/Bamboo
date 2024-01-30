using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FAM.ServiceInterface.DataObjects
{
    /// <summary>
    /// 账单报表数据
    /// </summary>
    [Serializable]
    public class BatchBillReportData
    {
        #region CompanyDsc
        private string _companydsc;

        /// <summary>
        /// 我公司的名称地址电话传真拼装串
        /// </summary>
        public string CompanyDsc
        {
            get { return _companydsc; }
            set
            {
                _companydsc = value;
                _companydsc = _companydsc.Replace("^", Environment.NewLine);
            }
        } 
        #endregion

        #region CustomerDsc
        private string _customerdsc;
        /// <summary>
        /// CustomerDsc
        /// </summary>
        public string CustomerDsc
        {
            get { return _customerdsc; }
            set
            {
                _customerdsc = value;
                _customerdsc = _customerdsc.Replace("^", Environment.NewLine);
            }
        }  
        #endregion

        /// <summary>
        /// AccountDate
        /// </summary>
        public string AccountDate { get; set; }
        /// <summary>
        /// DueDate
        /// </summary>
        public string DueDate { get; set; }
        /// <summary>
        /// Trem
        /// </summary>
        public int Trem { get; set; }
        /// <summary>
        /// 应收总金额 主币种
        /// </summary>
        public string TotalAmount { get; set; }
        /// <summary>
        /// 当前用户
        /// </summary>
        public string CurrentUser{ get; set; }
        #region 费用列表
        /// <summary>
        /// 费用列表
        /// </summary>
        public List<BillManifestInfo> ManifestList
        {
            get;
            set;
        }
        #endregion
    }
    /// <summary>
    /// 费用
    /// </summary>
    [Serializable]
    public class BillManifestInfo
    {
        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNo
        {
            get;
            set;
        }
        /// <summary>
        /// 柜号
        /// </summary>
        public string ContainerNo
        {
            get;
            set;
        }
        /// <summary>
        /// 费用
        /// </summary>
        public string DescriptionOfCharges
        {
            get;
            set;
        }
        /// <summary>
        /// 箱数量
        /// </summary>
        public decimal CntQty
        {
            get;
            set;
        }
        /// <summary>
        /// 费用方向
        /// </summary>
        public FeeWay ChargeWay { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal ChargePrice
        {
            get;
            set;
        }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal ChargeRate { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal ChargeAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 总金额 主币种
        /// </summary>
        public decimal TotalAmoint
        {
            get
            {
                return ChargeRate*ChargeAmount;
            }
        }
        /// <summary>
        /// 币种
        /// </summary>
        public string CurrencyName
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
    }
}
