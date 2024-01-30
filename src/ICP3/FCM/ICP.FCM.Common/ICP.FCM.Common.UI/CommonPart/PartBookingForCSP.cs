using DevExpress.XtraBars;
using ICP.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.CompositeObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ICP.FCM.Common.UI.CommonPart
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PartBookingForCSP : BasePart, IChildPart
    {
        #region 成员
        /// <summary>
        /// 
        /// </summary>
        bool _isChanged = false;
        /// <summary>
        /// 
        /// </summary>
        public bool IsChanged
        {
            get
            {
                if (_isChanged == false)
                {
                    List<BookingDelegate> source = bindingSourceMain.DataSource as List<BookingDelegate>;
                    if (source != null)
                    {
                        foreach (var item in source)
                        {
                            if (item.IsDirty)
                            {
                                return true;
                            }
                        }
                    }
                }
                return _isChanged;
            }
        }
        /// <summary>
        /// 业务ID
        /// </summary>
        BusinessOperationContext Context { get; set; }
        /// <summary>
        /// 当前行
        /// </summary>
        BookingDelegate CurrentRow
        {
            get
            {
                if (bindingSourceMain.Current == null)
                    return null;
                else
                    return bindingSourceMain.Current as BookingDelegate;
            }
        }
        /// <summary>
        /// 选择行
        /// </summary>
        List<BookingDelegate> SelectRows
        {
            get
            {
                int[] indexs = gridViewMain.GetSelectedRows();
                if (indexs == null || indexs.Length == 0) return null;

                List<BookingDelegate> list = new List<BookingDelegate>();
                foreach (var item in indexs)
                {
                    BookingDelegate tager = gridViewMain.GetRow(item) as BookingDelegate;
                    if (tager != null) list.Add(tager);
                }
                return list;
            }
        }
        /// <summary>
        /// 选择行
        /// </summary>
        List<BookingDelegate> DataList
        {
            get
            {
                List<BookingDelegate> bookingDelegates = DataSource as List<BookingDelegate>;
                if (bookingDelegates == null)
                    bookingDelegates= new List<BookingDelegate>();
                return bookingDelegates;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public object DataSource
        {
            get { return bindingSourceMain.DataSource; }
        }
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler DataChanged;
        #endregion
        
        #region 服务
        /// <summary>
        /// 
        /// </summary>
        IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        /// <summary>
        /// FCM公用服务
        /// </summary>
        IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 
        /// </summary>
        public PartBookingForCSP()
        {
            InitializeComponent();
            Disposed += delegate
            {
                gridControlMain.DataSource = null;
                dxErrorProvider1.DataSource = null;
                bindingSourceMain.DataSource = null;
                bindingSourceMain.Dispose();
                DataChanged = null;
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="values"></param>
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
        }
        #endregion

        #region 窗体事件
        /// <summary>
        /// 
        /// </summary>
        void barAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            BookingDelegate newRow = new BookingDelegate()
            {
                ID = Guid.Empty,
                IsDefault = false,
                CreateByID = LocalData.UserInfo.LoginID,
                CreateByName = LocalData.UserInfo.LoginName,
                CreateDate = DateTime.Now,
            };
            bindingSourceMain.Add(newRow);
            bindingSourceMain.ResetBindings(false);
            _isChanged = true;
            if (DataChanged != null)
            {
                DataChanged(this, null);
            }
            gridViewMain.MoveLast();
        }
        /// <summary>
        /// 
        /// </summary>
        void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<BookingDelegate> list = SelectRows;
            if (list == null || list.Count == 0) return;

            if (!PartLoader.EnquireIsDeleteCurrentData())
            {
                return;
            }

            List<Guid> needRemoveIDs = new List<Guid>();
            List<DateTime?> needRemoveUpdateDate = new List<DateTime?>();

            foreach (var item in list)
            {
                if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(item.ID) == false)
                {
                    needRemoveIDs.Add(item.ID);
                    needRemoveUpdateDate.Add(item.UpdateDate);
                }
            }
            try
            {
                if (needRemoveIDs.Count != 0)
                {
                    //OceanExportService.RemoveOceanOrderFeeList(needRemoveIDs.ToArray(),_CompanyID, LocalData.UserInfo.LoginID, needRemoveUpdateDate.ToArray());
                }

                gridViewMain.DeleteSelectedRows();
                _isChanged = true;
                if (DataChanged != null) DataChanged(this, null);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm()
                    , (LocalData.IsEnglish ? "Delete Faily" : "删除失败.") + ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        void barSetDefault_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CurrentRow == null || gridViewMain.FocusedRowHandle < 0)
                return;
            for (int i = 0; i < DataList.Count; i++)
            {
                DataList[i].IsDefault = false;
            }
            CurrentRow.IsDefault = true;
            bindingSourceMain.EndEdit();
            bindingSourceMain.ResetBindings(false);
            if (DataChanged != null)
            {
                DataChanged(DataList, null);
            }
        }
        #endregion

        #region 方法
        private void InitControls()
        {
            DataFindClientService.RegisterGridColumnFinder(colCustomerName
                                                , CommonFinderConstants.CustoemrFinder
                                                , "CustomerID"
                                                , "CustomerName"
                                                , "ID"
                                                , LocalData.IsEnglish ? "EName" : "CName");

            RegisterRelativeEvents();
        }

        /// <summary>
        /// 注册各种联动的事件
        /// </summary>
        void RegisterRelativeEvents()
        {
            barAdd.ItemClick += barAdd_ItemClick;
            barDelete.ItemClick += barDelete_ItemClick;
            barSetDefault.ItemClick += barSetDefault_ItemClick;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void SetSource(object value)
        {
            if (value == null) return;
            if ((value as List<BookingDelegate>).Count == 0) return;
            _isChanged = false;
            bindingSourceMain.DataSource = value;
            bindingSourceMain.ResetBindings(false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="workitem"></param>
        public void SetService(WorkItem workitem)
        {
            InitControls();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void SetContext(BusinessOperationContext context)
        {
            Context = context;
        }
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        public bool ValidateData()
        {
            Validate();
            bindingSourceMain.EndEdit();

            foreach (var item in bindingSourceMain.List)
            {
                if ((item as BookingDelegate).Validate
                    (
                    ) == false) return false;
            }

            return true;
        }

        /// <summary>
        /// 构建保存对象
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <returns></returns>
        public List<SaveRequestBookingDelegate> BuildSaveRequest(Guid operationID,OperationType operationType)
        {
            gridViewMain.CloseEditor();
            if (DataList.Count != 0)
            {
                List<BookingDelegate> changedLists = DataList.FindAll(o => o.IsDirty && o.CancelRemark.IsNullOrEmpty());
                if (operationID == Guid.Empty)
                {
                    changedLists = DataList;
                }
                List<BookingDelegate> cancelLists = DataList.FindAll(o => !o.CancelRemark.IsNullOrEmpty());
                if (cancelLists.Count > 0)
                {
                    SaveRequestBookingDelegate cancelRequest = new SaveRequestBookingDelegate()
                        {
                            OperationID = operationID,
                            ItemIDs = cancelLists.Select(fItem => new Guid?(fItem.ID)).ToArray(),
                            SaveBy = LocalData.UserInfo.LoginID,
                            UpdateDate = DateTime.Now,
                        };
                    FCMCommonService.RemoveBookingDelegate(cancelRequest);
                }
                if (changedLists.Count > 0)
                {
                    List<SaveRequestBookingDelegate> commands = new List<SaveRequestBookingDelegate>()
                    {
                        new SaveRequestBookingDelegate()
                        {
                            OperationID = operationID,
                            OperationType = operationType,
                            ItemIDs = changedLists.Select(fItem => new Guid?(fItem.ID)).ToArray(),
                            BookingMapIDs = changedLists.Select(fItem => fItem.BookingMapID).ToArray(),
                            BookingNos = changedLists.Select(fItem => fItem.BookingNo).ToArray(),
                            BookingNames = changedLists.Select(fItem => fItem.BookingName).ToArray(),
                            FreightMethodTypes = changedLists.Select(fItem => fItem.FreightMethodType).ToArray(),
                            ShipmentTypes = changedLists.Select(fItem => fItem.ShipmentType).ToArray(),
                            IncoTermIDs = changedLists.Select(fItem => fItem.IncoTermID).ToArray(),
                            TradeTypes = changedLists.Select(fItem => fItem.TradeType).ToArray(),
                            TransportClauseIDs = changedLists.Select(fItem => fItem.TransportClauseID).ToArray(),
                            BookingDates = changedLists.Select(fItem => fItem.BookingDate).ToArray(),
                            SalesIDs = changedLists.Select(fItem => fItem.SalesID).ToArray(),
                            IsInsurances = changedLists.Select(fItem => fItem.IsInsurance).ToArray(),
                            IsTrucks = changedLists.Select(fItem => fItem.IsTruck).ToArray(),
                            IsDeclarations = changedLists.Select(fItem => fItem.IsDeclaration).ToArray(),
                            CustomerIDs = changedLists.Select(fItem => fItem.CustomerID).ToArray(),
                            ShipperIDs = changedLists.Select(fItem => fItem.ShipperID).ToArray(),
                            ConsigneeIDs = changedLists.Select(fItem => fItem.ConsigneeID).ToArray(),
                            POLIDs = changedLists.Select(fItem => fItem.POLID).ToArray(),
                            POLAddressMapIDs = changedLists.Select(fItem => fItem.POLAddressMapID).ToArray(),
                            POLAddresss = changedLists.Select(fItem => fItem.POLAddress).ToArray(),
                            ETDForPOLs = changedLists.Select(fItem => fItem.ETDForPOL).ToArray(),
                            PODIDs = changedLists.Select(fItem => fItem.PODID).ToArray(),
                            ETAForPODs = changedLists.Select(fItem => fItem.ETAForPOD).ToArray(),
                            PODAddressMapIDs = changedLists.Select(fItem => fItem.PODAddressMapID).ToArray(),
                            PODAddresss = changedLists.Select(fItem => fItem.PODAddress).ToArray(),
                            Containerss = changedLists.Select(fItem => fItem.Containers).ToArray(),
                            Quantitys = changedLists.Select(fItem => fItem.Quantity).ToArray(),
                            QuantityUnitIDs = changedLists.Select(fItem => fItem.QuantityUnitID).ToArray(),
                            Weights = changedLists.Select(fItem => fItem.Weight).ToArray(),
                            WeightUnitIDs = changedLists.Select(fItem => fItem.WeightUnitID).ToArray(),
                            Measurements = changedLists.Select(fItem => fItem.Measurement).ToArray(),
                            MeasurementUnitIDs = changedLists.Select(fItem => fItem.MeasurementUnitID).ToArray(),
                            SaveBy = LocalData.UserInfo.LoginID,
                            UpdateDate = DateTime.Now,
                        },
                    };

                    return commands;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public void AfterSaved()
        {
            _isChanged = false;
        }
        #endregion
    }
}
