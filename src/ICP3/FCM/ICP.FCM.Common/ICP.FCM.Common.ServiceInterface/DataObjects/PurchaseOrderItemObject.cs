using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    /// <summary>
    /// 采购单明细
    /// </summary>
    [Serializable]
    public class PurchaseOrderItem : BaseDataObject
    {
        #region 是否新增
        /// <summary>
        /// 是否新增
        /// </summary>
        public override bool IsNew { get { return _ID == 0; } }
        #endregion

        #region 唯一键
        private int _ID;
        /// <summary>
        /// 唯一键
        /// </summary>
        public int ID
        {
            get
            {
                return _ID;
            }

            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    OnPropertyChanged("ID", value);
                }
            }
        }
        #endregion

        #region 提单ID
        private Guid _BillOfLadingID;
        /// <summary>
        /// 提单ID
        /// </summary>
        public Guid BillOfLadingID
        {
            get
            {
                return _BillOfLadingID;
            }

            set
            {
                if (_BillOfLadingID != value)
                {
                    _BillOfLadingID = value;
                    OnPropertyChanged("BillOfLadingID", value);
                }
            }
        }
        #endregion

        #region 提单号
        private string _BillOfLadingNO;
        /// <summary>
        /// 提单号
        /// </summary>
        public string BillOfLadingNO
        {
            get
            {
                return _BillOfLadingNO;
            }

            set
            {
                if (_BillOfLadingNO != value)
                {
                    _BillOfLadingNO = value;
                    OnPropertyChanged("BillOfLadingNO", value);
                }
            }
        }
        #endregion

        #region 箱ID
        private Guid _ContainerID;
        /// <summary>
        /// 箱ID
        /// </summary>
        public Guid ContainerID
        {
            get
            {
                return _ContainerID;
            }

            set
            {
                if (_ContainerID != value)
                {
                    _ContainerID = value;
                    OnPropertyChanged("ContainerID", value);
                }
            }
        }
        #endregion

        #region 箱号
        private string _ContainerNO;
        /// <summary>
        /// 箱号
        /// </summary>
        public string ContainerNO
        {
            get
            {
                return _ContainerNO;
            }

            set
            {
                if (_ContainerNO != value)
                {
                    _ContainerNO = value;
                    OnPropertyChanged("ContainerNO", value);
                }
            }
        }
        #endregion

        #region 采购单ID
        private int _PurchaseOrderID;
        /// <summary>
        /// 采购单ID
        /// </summary>
        public int PurchaseOrderID
        {
            get
            {
                return _PurchaseOrderID;
            }

            set
            {
                if (_PurchaseOrderID != value)
                {
                    _PurchaseOrderID = value;
                    OnPropertyChanged("PurchaseOrderID", value);
                }
            }
        }
        #endregion

        #region PO#
        private string _PurchaseOrderNO;
        /// <summary>
        /// PO#
        /// </summary>
        public string PurchaseOrderNO
        {
            get
            {
                return _PurchaseOrderNO;
            }

            set
            {
                if (_PurchaseOrderNO != value)
                {
                    _PurchaseOrderNO = value;
                    OnPropertyChanged("PurchaseOrderNO", value);
                }
            }
        }
        #endregion

        #region 产品名称
        private string _ProductName;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName
        {
            get
            {
                return _ProductName;
            }

            set
            {
                if (_ProductName != value)
                {
                    _ProductName = value;
                    OnPropertyChanged("ProductName", value);
                }
            }
        }
        #endregion

        #region 库存量单位(SKU)
        private string _StockKeepingUnit;
        /// <summary>
        /// 库存量单位(SKU)
        /// </summary>
        public string StockKeepingUnit
        {
            get
            {
                return _StockKeepingUnit;
            }

            set
            {
                if (_StockKeepingUnit != value)
                {
                    _StockKeepingUnit = value;
                    OnPropertyChanged("StockKeepingUnit", value);
                }
            }
        }
        #endregion

        #region 制造商零件编号(MPN)
        private string _ManufacturerPartNumber;
        /// <summary>
        /// 制造商零件编号(MPN)
        /// </summary>
        public string ManufacturerPartNumber
        {
            get
            {
                return _ManufacturerPartNumber;
            }

            set
            {
                if (_ManufacturerPartNumber != value)
                {
                    _ManufacturerPartNumber = value;
                    OnPropertyChanged("ManufacturerPartNumber", value);
                }
            }
        }
        #endregion

        #region 纸箱
        private int _CartonCount;
        /// <summary>
        /// 纸箱
        /// </summary>
        public int CartonCount
        {
            get
            {
                return _CartonCount;
            }
            set
            {
                if (_CartonCount != value)
                {
                    _CartonCount = value;
                    OnPropertyChanged("CartonCount", value);
                }
            }
        }
        #endregion

        #region 件数
        private decimal _Quantity;
        /// <summary>
        /// 件数
        /// </summary>
        public decimal Quantity
        {
            get
            {
                return _Quantity;
            }

            set
            {
                if (_Quantity != value)
                {
                    _Quantity = value;
                    OnPropertyChanged("Quantity", value);
                }
            }
        }
        #endregion

        #region 单元成本价
        private decimal _UnitCostPrice;
        /// <summary>
        /// 单元成本价
        /// </summary>
        public decimal UnitCostPrice
        {
            get
            {
                return _UnitCostPrice;
            }

            set
            {
                if (_UnitCostPrice != value)
                {
                    _UnitCostPrice = value;
                    OnPropertyChanged("UnitCost", value);
                }
            }
        }
        #endregion

        #region 重量
        private decimal _Weight;
        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight
        {
            get
            {
                return _Weight;
            }

            set
            {
                if (_Weight != value)
                {
                    _Weight = value;
                    OnPropertyChanged("Weight", value);
                }
            }
        }
        #endregion

        #region 体积
        private decimal _Volume;
        /// <summary>
        /// 体积
        /// </summary>
        public decimal Volume
        {
            get
            {
                return _Volume;
            }

            set
            {
                if (_Volume != value)
                {
                    _Volume = value;
                    OnPropertyChanged("Volume", value);
                }
            }
        }
        #endregion
    }
}
