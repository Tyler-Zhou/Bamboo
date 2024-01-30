using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface;
using ICP.Framework.ClientComponents.Controls;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.Utility;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.Service;
using ICP.FRM.UI.Comm;

namespace ICP.FRM.UI.InquireRates
{
    [ToolboxItem(false)]
    public partial class InquireTruckingRatesGeneralInfoPart : BaseEditPart, IInquierRateDataBind,IDataBind
    {
        #region Services

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        IInquireRatesService InquireRatesService
        {
            get
            {
                return ServiceClient.GetService<IInquireRatesService>();
            }
        }

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        #endregion

        #region Local variables

        /// <summary>
        /// 控件验证
        /// </summary>
        ControlValidator _ControlValidator;

        #region 属性-拖车询价对象
        /// <summary>
        /// 拖车询价对象
        /// </summary>
        ClientInquierTruckingRate _clientInquierTruckingRate;

        public ClientInquierTruckingRate CurrentInquierRate
        {
            get { return _clientInquierTruckingRate; }
            set { _clientInquierTruckingRate = value; }
        }
        #endregion

        #region 属性-是否有数据发生更新
        /// <summary>
        /// 是否有数据发生更新
        /// </summary>
        private bool _isChanged = false;
        /// <summary>
        /// 是否有数据发生更新
        /// </summary>
        public bool IsChanged
        {
            get
            {
                if (_isChanged)
                {
                    return true;
                }
                else
                {
                    if (_clientInquierTruckingRate == null)
                        return false;
                    if (_clientInquierTruckingRate.IsDirty)
                    {
                        return true;
                    }
                }

                return false;
            }
            set
            {
                _isChanged = value;
            }
        }
        #endregion

        #region 属性-控件只读设置
        bool _IsReadOnly;
        public bool IsReadOnly
        {
            set
            {
                txtCargoReady.Properties.ReadOnly = value;
                txtCargoWeight.Properties.ReadOnly = value;
                txtCommodity.Properties.ReadOnly = value;
                txtCustomer.Properties.ReadOnly = value;
                txtETOD.Properties.ReadOnly = value;
                txtExpectedCarrier.Properties.ReadOnly = value;

                txtCartonsOrPallets.Properties.ReadOnly = value;
                txtMeasurement.Properties.ReadOnly = value;
                txtExpectedPrice.Properties.ReadOnly = value;
                if (!value)
                {
                    txtCommodity.Properties.Appearance.BackColor = Color.LightYellow;
                }

                _IsReadOnly = value;
            }
            get
            {
                return _IsReadOnly;
            }
        } 
        #endregion

        #region 属性-当前选择询价人
        /// <summary>
        /// 当前选择询价人
        /// </summary>
        InquirePriceInquireBys CurrentInquireBy
        {
            get
            {
                if (bsInquireBys.Current == null)
                    return null;
                InquirePriceInquireBys item = bsInquireBys.Current as InquirePriceInquireBys;
                return item;
            }
        } 
        #endregion

        #region Delegate & Event
        /// <summary>
        /// 转移询价
        /// </summary>
        [EventPublication(InquireRatesCommandConstants.Command_Transit)]
        public event EventHandler<DataEventArgs<object>> TransitEvent;

        //[EventPublication(InquireRatesCommandConstants.Command_RefreshTruckingDiscussingPart)]
        //public event EventHandler<DataEventArgs<object>> RefreshTruckingDiscussingPartEvent;
        #endregion 

        #endregion

        #region Init
        /// <summary>
        /// 构造函数
        /// </summary>
        public InquireTruckingRatesGeneralInfoPart()
        {
            InitializeComponent();
            gcInquireBys.ContextMenuStrip = cmsGeneralList;
            ControlEvent(true);
            Disposed += (sender,args)=>
            {
                ControlEvent(false);
                _clientInquierTruckingRate = null;
                _ControlValidator = null;
                TransitEvent = null;
                bsGeneralInfo.DataSource = null;
                bsGeneralInfo.Dispose();
                bsInquireBys.DataSource = null;
                bsInquireBys.Dispose();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            //#region 运输条款

            //List<TransportClauseList> transportClauses = TransportFoundationService.GetTransportClauseList(string.Empty, string.Empty, true, 0);
            //TransportClauseList emptyTransportClause = new TransportClauseList();
            //emptyTransportClause.ID = Guid.Empty;
            //emptyTransportClause.Code = string.Empty;
            //transportClauses.Insert(0, emptyTransportClause);

            //foreach (var item in transportClauses)
            //{
            //    cmbTransportClause.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Code, item.ID));
            //}

            //#endregion

            #region 客户搜索器

            //客户搜索器
            DataFindClientService.Register(txtCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
                SearchFieldConstants.ResultValue,
                      delegate(object inputSource, object[] resultData)
                      {
                          txtCustomer.Tag = _clientInquierTruckingRate.CustomerID = new Guid(resultData[0].ToString());
                          txtCustomer.Text = _clientInquierTruckingRate.CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();

                      }, delegate
                      {
                          txtCustomer.Tag = _clientInquierTruckingRate.CustomerID = Guid.Empty;
                          txtCustomer.Text = _clientInquierTruckingRate.CustomerName = string.Empty;
                      },
                      ClientConstants.MainWorkspace);

            #endregion

            #region 必填项验证的注册
            _ControlValidator = new ControlValidator();
            //_ControlValidator.RegisterField(txtCommodity, ControlValidatorType.IsRequired, labCommodity.Text, labCommodity.Text);
            #endregion
        }

        /// <summary>
        /// 初始化控件事件
        /// </summary>
        private void ControlEvent(bool isAdd)
        {
            if (isAdd)
            {
                tsmHandled.Click += HandledInquireRate; //Handled处理事件
                tsmUnHandled.Click += HandledInquireRate;//Un Handled处理事件
                bsInquireBys.CurrentChanged += bsInquireBys_CurrentChanged;//行改变事件
                checkEditHandled.EditValueChanged += checkEditHandled_EditValueChanged;//处理复选框改变事件
                btnTransit.Click += btnTransit_Click; //转移询价
            }
            else
            {
                tsmHandled.Click -= HandledInquireRate; //Handled处理事件
                tsmUnHandled.Click -= HandledInquireRate;//Un Handled处理事件
                bsInquireBys.CurrentChanged -= bsInquireBys_CurrentChanged;//行改变事件
                checkEditHandled.EditValueChanged -= checkEditHandled_EditValueChanged;//处理复选框改变事件
                btnTransit.Click -= btnTransit_Click; //转移询价
            }
        }
        #endregion

        #region Base & Interface

        #region BaseEditPart

        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }

        /// <summary>
        /// 触发保存事件
        /// </summary>
        public override void RaiseSaved()
        {
            SaveData();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public override bool SaveData()
        {
            if (!_ControlValidator.ValidateData())
            {
                return false;
            }

            bsGeneralInfo.EndEdit();
            try
            {
                //List<Guid> rateIDs = new List<Guid>();
                //List<Guid> rateUnitIDs = new List<Guid>();
                //List<decimal> rateRates = new List<decimal>();

                //foreach (var item in _clientInquierTruckingRate.RateUnitList)
                //{
                //    rateIDs.Add(item.ID);
                //    rateUnitIDs.Add(item.UnitID);
                //    rateRates.Add(item.Rate);
                //}

                SingleResult result = InquireRatesService.SaveInquireRateInfo(_clientInquierTruckingRate.ID, _clientInquierTruckingRate.No,
                    InquierType.TruckingRates,
                    new Guid[1],
                    new Guid[1],
                    new decimal[1],
                    _clientInquierTruckingRate.ShippingLineID.Value,
                    _clientInquierTruckingRate.CustomerID,
                    _clientInquierTruckingRate.ExpCarrierName,
                    _clientInquierTruckingRate.POLID,
                    _clientInquierTruckingRate.PODID,
                    _clientInquierTruckingRate.PlaceOfDeliveryID,
                    _clientInquierTruckingRate.ExpCommodity,
                    _clientInquierTruckingRate.ExpTransportClauseID,
                    _clientInquierTruckingRate.CargoWeight,
                    _clientInquierTruckingRate.Measurement,
                    _clientInquierTruckingRate.CargoReady,
                    _clientInquierTruckingRate.CartonsOrPallets,
                    string.Empty,
                    string.Empty,
                    _clientInquierTruckingRate.ZipCode,
                    _clientInquierTruckingRate.EstimateTimeOfDelivery,
                    _clientInquierTruckingRate.IsWillBooking,
                    _clientInquierTruckingRate.ExpPrice,
                    null,//_clientInquierTruckingRate.DiscussingWhenNew,
                    _clientInquierTruckingRate.RespondByID,
                    _clientInquierTruckingRate.InquireByID,
                    DateTimeOffset.Now,
                    _clientInquierTruckingRate.UpdateDate,
                    LocalData.UserInfo.DefaultCompanyID, LocalData.UserInfo.LoginID);

                _clientInquierTruckingRate.ID = result.GetValue<Guid>("ID");
                if (result.ContainProperty("UpdateDate"))
                    _clientInquierTruckingRate.UpdateDate = result.GetValue<DateTime>("UpdateDate");
                else
                    _clientInquierTruckingRate.UpdateDate = null;
                //this._clientInquierTruckingRate.ShippingLineName = cmbShipLine.Text;
                _clientInquierTruckingRate.CancelEdit();
                _clientInquierTruckingRate.BeginEdit();
                //if (Saved != null) Saved(this._clientInquierTruckingRate, new object[] { result });
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "SaveSuccessfully"));
                //if (RefreshTruckingDiscussingPartEvent != null)
                //{
                //    RefreshTruckingDiscussingPartEvent(this, new DataEventArgs<object>(new object()));
                //}

                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                return false;
            }
        }
        #endregion 

        #region IDataBind 成员

        public void DataBind(BusinessOperationContext business)
        {
            DataSourceBind(new ClientInquierTruckingRate() { ID = business.OperationID, MainRecordID = business.ParentOperationID });
        }

        public void ControlsReadOnly(bool flg)
        {

        }
        #endregion

        #endregion

        #region Commond & Event
        /// <summary>
        /// 行改变事件
        /// </summary>
        private void bsInquireBys_CurrentChanged(object sender, EventArgs e)
        {
            //设置控件状态
            SetControlState();
        }

        /// <summary>
        /// 处理状态值改变
        /// </summary>
        void checkEditHandled_EditValueChanged(object sender, EventArgs e)
        {
            //调用询价处理
            HandledInquireRate(null, null);
        } 
        /// <summary>
        /// 转移询价
        /// </summary>
        private void btnTransit_Click(object sender, EventArgs e)
        {
            TransitPart transitPart = Workitem.Items.AddNew<TransitPart>();
            transitPart.SetSouce(_clientInquierTruckingRate);
            DialogResult dr = PartLoader.ShowDialog(transitPart, "Transit", FormBorderStyle.Sizable);

            if (dr == DialogResult.OK)
            {
                txtRespondBy.Text = _clientInquierTruckingRate.RespondByName;
                if (TransitEvent != null)
                {
                    TransitEvent(this, new DataEventArgs<object>(new object()));
                }
            }
        }
        /// <summary>
        /// 处理询价
        /// </summary>
        public void HandledInquireRate(object sender, EventArgs e)
        {
            try
            {
                //1.判断是否有选择询价人
                if (CurrentInquireBy == null)
                    return;
                //设置询价人询价状态
                InquireRatesService.HandledInquirePriceInquireBys(CurrentInquireBy.ID, CurrentInquireBy.Handled);
                //刷新界面询价人
                CurrentInquireBy.Handled = !CurrentInquireBy.Handled;
                bsInquireBys.EndEdit();
                bsInquireBys.ResetBindings(false);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        #endregion

        #region Method

        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="data">绑定数据对象</param>
        public void DataSourceBind(object data)
        {
            DataSource = data;
        }

        /// <summary>
        /// 设置控件状态
        /// </summary>
        private void SetControlState()
        {
            tsmHandled.Visible = true;
            tsmUnHandled.Visible = true;
            //网格无数据、控件启用状态取消
            if (CurrentInquireBy == null)
            {
                tsmHandled.Visible = false;
                tsmUnHandled.Visible = false;
            }
            else
            {
                //通过判断当前选择询价人设置控件状态
                if (CurrentInquireBy == null)
                    return;
                if (CurrentInquireBy.Handled)   //当前询价人询价状态
                {
                    //禁用处理按钮
                    tsmHandled.Visible = false;
                }
                else
                {
                    //禁用未处理按钮
                    tsmUnHandled.Visible = false;
                }
            }
        }

        /// <summary>
        /// 验证录入数据
        /// </summary>
        /// <returns></returns>
        public bool ValidateData()
        {
            return _ControlValidator.ValidateData();
        }

        /// <summary>
        /// 设置转移、集装箱尺寸功能是否可用
        /// </summary>
        /// <param name="value"></param>
        private void SetTransiteAndChangeUnit(bool value)
        {
            btnRateUnit.Enabled = false;
            btnTransit.Enabled = value;
        }

        #region 绑定数据

        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="data">数据源</param>
        private void BindingData(object data)
        {
            var truckingRateOnGrid = data as ClientInquierTruckingRate;

            Guid id = truckingRateOnGrid == null ? Guid.Empty : (truckingRateOnGrid.MainRecordID == null ? truckingRateOnGrid.ID : truckingRateOnGrid.MainRecordID.Value);
            if (truckingRateOnGrid == null || id == Guid.Empty)
            {
                Enabled = false;
                bsGeneralInfo.DataSource = typeof(ClientInquierTruckingRate);
                bsGeneralInfo.ResetBindings(false);
                bsInquireBys.DataSource = typeof(InquirePriceInquireBys);
                bsInquireBys.ResetBindings(false);
            }
            else
            {
                Enabled = true;

                //保证只能显示主询价的General Info
                _clientInquierTruckingRate = InquireRatesHelper.TransformS2C(
                        InquireRatesService.GetInquierTruckingRateInfoForInquireBy(id, LocalData.UserInfo.LoginID)
                        , truckingRateOnGrid.RateUnitList, null);

                _clientInquierTruckingRate.RateUnitList = new List<InquireUnit>();
                if (truckingRateOnGrid.RateUnitList != null)
                {
                    foreach (var sub in truckingRateOnGrid.RateUnitList)
                    {
                        _clientInquierTruckingRate.RateUnitList.Add(Utility.Clone<InquireUnit>(sub));
                    }
                }
                bsGeneralInfo.DataSource = _clientInquierTruckingRate;
                bsGeneralInfo.ResetBindings(false);
                //绑定InquireBys
                bsInquireBys.DataSource = _clientInquierTruckingRate.InquirePriceInquireBysList;
                bsInquireBys.ResetBindings(false);
                //txtRateUnitString.Text = "20GP/40GP/40HQ";
                //if (_clientInquierTruckingRate.RateUnitList != null)
                //{
                //    string textString = string.Empty;
                //    foreach (var unit in _clientInquierTruckingRate.RateUnitList)
                //    {
                //        if (!string.IsNullOrEmpty(textString))
                //        {
                //            textString += "/";
                //        }

                //        textString += unit.UnitName;
                //    }

                //    txtRateUnitString.Text = textString;
                //}
                //else
                //{
                //    txtRateUnitString.Text = string.Empty;
                //}

                _clientInquierTruckingRate.CancelEdit();
                _clientInquierTruckingRate.BeginEdit();

                if (_clientInquierTruckingRate.InquireByID == LocalData.UserInfo.LoginID)
                {
                    InitControls();
                    IsReadOnly = false;
                }
                else
                {
                    IsReadOnly = true;
                }

                SetTransiteAndChangeUnit(_clientInquierTruckingRate.RespondByID == LocalData.UserInfo.LoginID ? true : false);
            }
        }

        #endregion
        #endregion


        #region Comment Code
        //private void btnRateUnit_Click(object sender, System.EventArgs e)
        //{
        //    RateUnitPart unitPart = Workitem.Items.AddNew<RateUnitPart>();
        //    unitPart.SetSouce(_clientInquierTruckingRate.RateUnitList, InquierType.TruckingRates);

        //    DialogResult dr = PartLoader.ShowDialog(unitPart, "Rate Unit", FormBorderStyle.Sizable);

        //    //直接点X关闭是不会更变箱型的
        //    if (dr == DialogResult.OK && unitPart.IsChanged)
        //    {
        //        _clientInquierTruckingRate.RateUnitList = unitPart.CurruntUnitList;
        //        foreach (var item in _clientInquierTruckingRate.RateUnitList)
        //        {
        //            txtRateUnitString.Text += item.UnitName + "/";
        //        }

        //        _clientInquierTruckingRate.IsDirty = true;
        //    }
        //}
        //[EventSubscription(InquireRatesCommandConstants.Command_Save)]
        //public void Command_Save(object sender, DataEventArgs<object> e)
        //{
        //    if (this.Visible == false) return;
        //    if (txtCargoReady.Properties.ReadOnly) return;
        //    if (_clientInquierTruckingRate.IsDirty == false) return;

        //    this.SaveData();
        //} 
        #endregion
    }
}
