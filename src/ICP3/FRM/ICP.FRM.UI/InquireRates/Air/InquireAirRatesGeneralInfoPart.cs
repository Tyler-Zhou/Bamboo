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
    public partial class InquireAirRatesGeneralInfoPart : BaseEditPart, IDataBind
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

        #region 属性-空运询价
        /// <summary>
        /// 空运询价
        /// </summary>
        ClientInquierAirRate _clientInquierAirRate;
        /// <summary>
        /// 空运询价
        /// </summary>
        public ClientInquierAirRate CurrentInquierRate
        {
            get { return _clientInquierAirRate; }
            set { _clientInquierAirRate = value; }
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
                    if (_clientInquierAirRate.IsDirty)
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
                txtMeasurement.Properties.ReadOnly = value;
                txtExpectedPrice.Properties.ReadOnly = value;
                txtMAWB.Properties.ReadOnly = value;
                txtHAWB.Properties.ReadOnly = value;
                txtCartonsOrPallets.Properties.ReadOnly = value;
                if (!value)
                {
                    txtCommodity.Properties.Appearance.BackColor = Color.LightYellow;
                    txtCargoReady.Properties.Appearance.BackColor = Color.LightYellow;
                    txtCargoWeight.Properties.Appearance.BackColor = Color.LightYellow;
                    txtMeasurement.Properties.Appearance.BackColor = Color.LightYellow;
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
        public delegate void ChangedUnitEventHandler(object sender, Guid inquirePriceID, List<InquireUnit> e);
        public event ChangedUnitEventHandler ChangedUnitEvent;

        [EventPublication(InquireRatesCommandConstants.Command_Transit)]
        public event EventHandler<DataEventArgs<object>> TransitEvent;

        //[EventPublication(InquireRatesCommandConstants.Command_RefreshAirDiscussingPart)]
        //public event EventHandler<DataEventArgs<object>> RefreshAirDiscussingPartEvent;
        #endregion
        #endregion

        #region Init

        /// <summary>
        /// 构造函数
        /// </summary>
        public InquireAirRatesGeneralInfoPart()
        {
            InitializeComponent();
            Enabled = false;
            ControlEvent(true);
            Disposed += (sender, args) =>
            {
                ChangedUnitEvent = null;
                TransitEvent = null;
                ControlEvent(false);
                //this.RefreshAirDiscussingPartEvent = null;
                _clientInquierAirRate = null;
                _ControlValidator = null;
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

        private void InitControls()
        {
            #region 客户搜索器

            //客户搜索器
            DataFindClientService.Register(txtCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
                SearchFieldConstants.ResultValue,
                      delegate(object inputSource, object[] resultData)
                      {
                          txtCustomer.Tag = _clientInquierAirRate.CustomerID = new Guid(resultData[0].ToString());
                          txtCustomer.Text = _clientInquierAirRate.CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();

                      }, delegate
                      {
                          txtCustomer.Tag = _clientInquierAirRate.CustomerID = Guid.Empty;
                          txtCustomer.Text = _clientInquierAirRate.CustomerName = string.Empty;
                      },
                      ClientConstants.MainWorkspace);

            #endregion

            #region 必填项验证的注册
            _ControlValidator = new ControlValidator();
            _ControlValidator.RegisterField(txtCommodity, ControlValidatorType.IsRequired, labCommodity.Text, labCommodity.Text);
            _ControlValidator.RegisterField(txtCargoWeight, ControlValidatorType.IsRequired, txtCargoWeight.Text, txtCargoWeight.Text);
            _ControlValidator.RegisterField(txtCargoReady, ControlValidatorType.IsRequired, labCargoReady.Text, labCargoReady.Text);
            _ControlValidator.RegisterField(txtMeasurement, ControlValidatorType.IsRequired, labMeasurement.Text, labMeasurement.Text);
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
                btnRateUnit.Click += btnRateUnit_Click;  //更改箱型

            }
            else
            {
                tsmHandled.Click -= HandledInquireRate; //Handled处理事件
                tsmUnHandled.Click -= HandledInquireRate;//Un Handled处理事件
                bsInquireBys.CurrentChanged -= bsInquireBys_CurrentChanged;//行改变事件
                checkEditHandled.EditValueChanged -= checkEditHandled_EditValueChanged;//处理复选框改变事件
                btnTransit.Click -= btnTransit_Click; //转移询价
                btnRateUnit.Click -= btnRateUnit_Click;  //更改箱型
            }
        }
        #endregion

        #region Base & Interface

        #region BaseEditPart
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
                List<Guid> rateIDs = new List<Guid>();
                List<Guid> rateUnitIDs = new List<Guid>();
                List<decimal> rateRates = new List<decimal>();

                foreach (var item in _clientInquierAirRate.RateUnitList)
                {
                    rateIDs.Add(item.ID);
                    rateUnitIDs.Add(item.UnitID);
                    rateRates.Add(item.Rate);
                }

                SingleResult result = InquireRatesService.SaveInquireRateInfo(_clientInquierAirRate.ID, _clientInquierAirRate.No,
                    InquierType.AirRates,
                    rateIDs.ToArray(),
                    rateUnitIDs.ToArray(),
                    rateRates.ToArray(),
                    _clientInquierAirRate.ShippingLineID.Value,
                    _clientInquierAirRate.CustomerID,
                    _clientInquierAirRate.ExpCarrierName,
                    _clientInquierAirRate.POLID,
                    _clientInquierAirRate.PODID,
                    _clientInquierAirRate.PlaceOfDeliveryID,
                    _clientInquierAirRate.ExpCommodity,
                    _clientInquierAirRate.ExpTransportClauseID,
                    _clientInquierAirRate.CargoWeight,
                    _clientInquierAirRate.Measurement,
                    _clientInquierAirRate.CargoReady,
                    _clientInquierAirRate.CartonsOrPallets,
                    _clientInquierAirRate.MAWB,
                    _clientInquierAirRate.HAWB,
                    string.Empty,
                    _clientInquierAirRate.EstimateTimeOfDelivery,
                    _clientInquierAirRate.IsWillBooking,
                    _clientInquierAirRate.ExpPrice,
                    null,//_clientInquierAirRate.DiscussingWhenNew
                    _clientInquierAirRate.RespondByID,
                    _clientInquierAirRate.InquireByID,
                    DateTimeOffset.Now,
                    _clientInquierAirRate.UpdateDate,
                    LocalData.UserInfo.DefaultCompanyID, LocalData.UserInfo.LoginID);

                _clientInquierAirRate.ID = result.GetValue<Guid>("ID");
                if (result.ContainProperty("UpdateDate"))
                    _clientInquierAirRate.UpdateDate = result.GetValue<DateTime>("UpdateDate");
                else
                    _clientInquierAirRate.UpdateDate = null;
                //this._clientInquierAirRate.ShippingLineName = cmbShipLine.Text;
                _clientInquierAirRate.CancelEdit();
                _clientInquierAirRate.BeginEdit();
                //if (Saved != null) Saved(this._clientInquierAirRate, new object[] { result });
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "SaveSuccessfully"));
                //if (RefreshAirDiscussingPartEvent != null)
                //{
                //    RefreshAirDiscussingPartEvent(this, new DataEventArgs<object>(new object()));
                //}

                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                return false;
            }
        }

        /// <summary>
        /// 触发保存事件
        /// </summary>
        public override void RaiseSaved()
        {
            SaveData();
        }
        #endregion

        #region IDataBind 成员

        public void DataBind(BusinessOperationContext business)
        {
            BindingData(new ClientInquierAirRate() { ID = business.OperationID, MainRecordID = business.ParentOperationID });
        }

        public void ControlsReadOnly(bool flg)
        {

        }
        #endregion

        #endregion

        #region Commond & Event

        [EventSubscription(InquireRatesCommandConstants.Command_Save)]
        public void Command_Save(object sender, DataEventArgs<object> e)
        {
            if (Visible == false) return;
            if (txtCargoReady.Properties.ReadOnly) return;
            //if (_clientInquierAirRate.IsDirty == false) return;

            SaveData();
        }

        private void btnRateUnit_Click(object sender, EventArgs e)
        {
            RateUnitPart unitPart = Workitem.Items.AddNew<RateUnitPart>();
            //RateUnitPart unitPart = new RateUnitPart();
            unitPart.SetSouce(_clientInquierAirRate.RateUnitList, InquierType.AirRates);

            DialogResult dr = PartLoader.ShowDialog(unitPart, "Rate Unit", FormBorderStyle.Sizable);

            //直接点X关闭是不会更变箱型的
            if (dr == DialogResult.OK && unitPart.IsChanged)
            {
                txtRateUnitString.Text = string.Empty;
                _clientInquierAirRate.RateUnitList = unitPart.CurruntUnitList;
                foreach (var item in _clientInquierAirRate.RateUnitList)
                {
                    txtRateUnitString.Text += item.UnitName + "/";
                }

                _clientInquierAirRate.IsDirty = true;

                if (ChangedUnitEvent != null)
                {
                    ChangedUnitEvent(this, _clientInquierAirRate.ID, _clientInquierAirRate.RateUnitList);
                }
            }
        }

        private void btnTransit_Click(object sender, EventArgs e)
        {
            TransitPart transitPart = Workitem.Items.AddNew<TransitPart>();
            transitPart.SetSouce(_clientInquierAirRate);
            DialogResult dr = PartLoader.ShowDialog(transitPart, "Transit", FormBorderStyle.Sizable);

            if (dr == DialogResult.OK)
            {
                //_clientInquierAirRate.RespondByID = transitPart.CurrentRate.RespondByID;
                txtRespondBy.Text = _clientInquierAirRate.RespondByName;
                if (TransitEvent != null)
                {
                    TransitEvent(this, new DataEventArgs<object>(new object()));
                }
            }
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
        /// 行改变事件
        /// </summary>
        private void bsInquireBys_CurrentChanged(object sender, EventArgs e)
        {
            //设置控件状态
            SetControlState();
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
        #region 绑定数据

        private void BindingData(object data)
        {
            var airRateOnGrid = data as ClientInquierAirRate;
            Guid id = airRateOnGrid == null ? Guid.Empty : (airRateOnGrid.MainRecordID == null ? airRateOnGrid.ID : airRateOnGrid.MainRecordID.Value);
            if (airRateOnGrid == null || id == Guid.Empty)
            {
                Enabled = false;
                bsGeneralInfo.DataSource = typeof(ClientInquierAirRate);
                bsGeneralInfo.ResetBindings(false);
                bsInquireBys.DataSource = typeof(InquirePriceInquireBys);
                bsInquireBys.ResetBindings(false);
            }
            else
            {
                Enabled = true;

                //保证只能显示主询价的General Info and discuss
                _clientInquierAirRate = InquireRatesHelper.TransformS2C(
                    InquireRatesService.GetInquierAirRateInfoForInquireBy(id, LocalData.UserInfo.LoginID)
                    , airRateOnGrid.RateUnitList, null);
                _clientInquierAirRate.RateUnitList = new List<InquireUnit>();
                foreach (var sub in airRateOnGrid.RateUnitList)
                {
                    _clientInquierAirRate.RateUnitList.Add(Utility.Clone<InquireUnit>(sub));
                }

                bsGeneralInfo.DataSource = _clientInquierAirRate;
                bsGeneralInfo.ResetBindings(false);

                //绑定InquireBys
                bsInquireBys.DataSource = _clientInquierAirRate.InquirePriceInquireBysList;
                bsInquireBys.ResetBindings(false);

                //txtRateUnitString.Text = "Min/+45/+100/+300/+500/+800/+1000/+1300";// 这有问题 
                if (_clientInquierAirRate.RateUnitList != null)
                {
                    string textString = string.Empty;
                    foreach (var unit in _clientInquierAirRate.RateUnitList)
                    {
                        if (!string.IsNullOrEmpty(textString))
                        {
                            textString += "/";
                        }

                        textString += unit.UnitName;
                    }

                    txtRateUnitString.Text = textString;
                }

                _clientInquierAirRate.CancelEdit();
                _clientInquierAirRate.BeginEdit();

                if (_clientInquierAirRate.InquireByID == LocalData.UserInfo.LoginID)
                {
                    InitControls();
                    IsReadOnly = false;
                }
                else
                {
                    IsReadOnly = true;
                }

                SetTransiteAndChangeUnit(_clientInquierAirRate.RespondByID == LocalData.UserInfo.LoginID ? true : false);

            }
        }

        #endregion

        public bool ValidateData()
        {
            return _ControlValidator.ValidateData();
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

        private void SetTransiteAndChangeUnit(bool value)
        {
            btnRateUnit.Enabled = value;
            btnTransit.Enabled = value;
        } 
        #endregion
    }
}
