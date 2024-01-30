#region Comment

/*
 * 
 * FileName:    InquireOceanRatesGeneralInfoPart.cs
 * CreatedOn:   
 * CreatedBy:   LiXuBin
 * 
 * 
 * Description：
 *      ->General Info
 * History：
 *      ->在Inquire Bys网格邮件新增右键菜单：1.根据当前选中职员新增询价
 * 
 * 
 * 
 */

#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.FRM.UI.Comm;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.Utility;

namespace ICP.FRM.UI.InquireRates
{
    [ToolboxItem(false)]
    public partial class InquireOceanRatesGeneralInfoPart : BaseEditPart, IInquierRateDataBind, IDataBind
    {
        #region 服务
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 询价服务
        /// </summary>
        IInquireRatesService InquireRatesService
        {
            get
            {
                return ServiceClient.GetService<IInquireRatesService>();
            }
        }
        /// <summary>
        /// 基础数据服务管理
        /// </summary>
        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }
        /// <summary>
        /// 搜索器客户端服务
        /// </summary>
        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        [ServiceDependency]
        public IClientInquireRateService ClientInquireRateService { get; set; }

        public ClientInquierOceanRate ClientInquierOceanRate { get; set; }

        #endregion

        #region 委托事件
        /// <summary>
        /// 价位
        /// </summary>
        /// <param name="inquirePriceID">询价Guid ID</param>
        public delegate void ChangedUnitEventHandler(object sender, Guid inquirePriceID, List<InquireUnit> e);
        /// <summary>
        /// 价位
        /// </summary>
        public event ChangedUnitEventHandler ChangedUnitEvent;
        /// <summary>
        /// 转移询价单
        /// </summary>
        [EventPublication(InquireRatesCommandConstants.Command_Transit)]
        public event EventHandler<DataEventArgs<object>> TransitEvent;
        /// <summary>
        /// 复制主询价
        /// </summary>
        [EventPublication(InquireRatesCommandConstants.Command_ReInquire)]
        public event EventHandler<DataEventArgs<object>> ReInquireEvent;
        #endregion

        #region 属性
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
        /// <summary>
        /// 询价基础数据模型
        /// </summary>
        ClientInquierOceanRate _clientInquierOceanRate;
        /// <summary>
        /// 控件验证
        /// </summary>
        ControlValidator _ControlValidator;

        private bool _isChanged = false;
        /// <summary>
        /// 是否有数据发生更新
        /// </summary>
        public bool IsChanged
        {
            get
            {
                //未赋值
                if (_clientInquierOceanRate == null)
                    return false;
                if (_isChanged)
                {
                    return true;
                }
                else
                {
                    bsGeneralInfo.EndEdit();
                    if (_clientInquierOceanRate.IsDirty)
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

        bool _IsReadOnly;
        /// <summary>
        /// 控件只读状态
        /// </summary>
        public bool IsReadOnly
        {
            set
            {
                txtCargoReady.Properties.ReadOnly = value;
                txtCargoWeight.Properties.ReadOnly = value;
                txtCommodity.Properties.ReadOnly = value;
                txtCustomer.Properties.ReadOnly = value;
                txtExpectedCarrier.Properties.ReadOnly = value;
                txtMeasurement.Properties.ReadOnly = value;
                txtExpectedPrice.Properties.ReadOnly = value;
                cmbTransportClause.Properties.ReadOnly = value;
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

        #region 绑定数据
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
        /// 绑定数据(General Info & Inquire Bys)
        /// </summary>
        /// <param name="data">数据源</param>
        private void BindingData(object data)
        {
            var oceanRateOnGrid = data as ClientInquierOceanRate;
            Guid id = oceanRateOnGrid == null ? Guid.Empty : (oceanRateOnGrid.MainRecordID == null ? oceanRateOnGrid.ID : oceanRateOnGrid.MainRecordID.Value);

            if (data == null || id == Guid.Empty)
            {
                //清空数据
                Enabled = false;
                bsGeneralInfo.DataSource = typeof(ClientInquierOceanRate);
                bsGeneralInfo.ResetBindings(false);
                bsInquireBys.DataSource = typeof(InquirePriceInquireBys);
                bsInquireBys.ResetBindings(false);
            }
            else
            {
                if (!Visible)
                    return;
                
                if (oceanRateOnGrid == null)
                    return;
                Enabled = true;
                //获取询价人列表
                oceanRateOnGrid.InquirePriceInquireBysList=InquireRatesService.GetInquirePriceInquireBys(oceanRateOnGrid.ID, oceanRateOnGrid.MainRecordID);
                //保证只能显示主询价的General Info
                
                //获取询价通用信息
                _clientInquierOceanRate = InquireRatesHelper.TransformS2C(
                    InquireRatesService.GetInquierOceanRateInfoForInquireBy(id, LocalData.UserInfo.LoginID)
                    , null, null);
                //构建箱型列表
                _clientInquierOceanRate.RateUnitList = new List<InquireUnit>();
                if (oceanRateOnGrid.RateUnitList != null)
                {
                    foreach (var sub in oceanRateOnGrid.RateUnitList)
                    {
                        _clientInquierOceanRate.RateUnitList.Add(Utility.Clone<InquireUnit>(sub));
                    }
                }
                //绑定GeneralInfo
                bsGeneralInfo.DataSource = _clientInquierOceanRate;
                bsGeneralInfo.ResetBindings(false);
                //绑定InquireBys
                bsInquireBys.DataSource = _clientInquierOceanRate.InquirePriceInquireBysList;
                bsInquireBys.ResetBindings(false);
                //显示箱型
                if (_clientInquierOceanRate.RateUnitList != null)
                {
                    string textString = string.Empty;
                    foreach (var unit in _clientInquierOceanRate.RateUnitList)
                    {
                        if (!string.IsNullOrEmpty(textString))
                        {
                            textString += "/";
                        }
                        textString += unit.UnitName;
                    }
                    txtRateUnitString.Text = textString;
                }
                //判断是否包含当前询价人以控制面板控件的编辑状态
                if (_clientInquierOceanRate.InquirePriceInquireBysList.Exists(o => o.InquireByID == LocalData.UserInfo.LoginID))
                {
                    InitControls();
                    IsReadOnly = false;
                }
                else
                {
                    IsReadOnly = true;
                }
                //设置按钮的可用状态
                SetTransiteAndChangeUnit(_clientInquierOceanRate.RespondByID == LocalData.UserInfo.LoginID ? true : false);
            }
        }

        #endregion

        #region 构造函数
        public InquireOceanRatesGeneralInfoPart()
        {
            InitializeComponent();
            //1.设置控件事件
            ControlEvent(true);
            //2.设置右键菜单
            gcInquireBys.ContextMenuStrip = cmsGeneralList;
            Disposed += (sender,args)=>
            {
                ControlEvent(false);
                ChangedUnitEvent = null;
                TransitEvent = null;
                _clientInquierOceanRate = null;
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
        
        #endregion

        #region Override
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
            if (IsReadOnly == true) return false;

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

                foreach (var item in _clientInquierOceanRate.RateUnitList)
                {
                    //箱型添加
                    rateIDs.Add(item.ID);
                    rateUnitIDs.Add(item.UnitID);
                    rateRates.Add(item.Rate);
                }
                //保存询价信息
                SingleResult result = InquireRatesService.SaveInquireRateInfo(_clientInquierOceanRate.ID,_clientInquierOceanRate.No,
                    InquierType.OceanRates,
                    rateIDs.ToArray(),
                    rateUnitIDs.ToArray(),
                    rateRates.ToArray(),
                    _clientInquierOceanRate.ShippingLineID.Value,
                    _clientInquierOceanRate.CustomerID,
                    _clientInquierOceanRate.ExpCarrierName,
                    _clientInquierOceanRate.POLID,
                    _clientInquierOceanRate.PODID,
                    _clientInquierOceanRate.PlaceOfDeliveryID,
                    _clientInquierOceanRate.ExpCommodity,
                    _clientInquierOceanRate.ExpTransportClauseID,
                    _clientInquierOceanRate.CargoWeight,
                    _clientInquierOceanRate.Measurement,
                    _clientInquierOceanRate.CargoReady,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    _clientInquierOceanRate.EstimateTimeOfDelivery,
                    _clientInquierOceanRate.IsWillBooking,
                    _clientInquierOceanRate.ExpPrice,
                    _clientInquierOceanRate.DiscussingWhenNew,
                    _clientInquierOceanRate.RespondByID,
                    _clientInquierOceanRate.InquireByID,
                    DateTimeOffset.Now,
                    _clientInquierOceanRate.UpdateDate,
                    LocalData.UserInfo.DefaultCompanyID, LocalData.UserInfo.LoginID);

                _clientInquierOceanRate.ID = result.GetValue<Guid>("ID");
                if (result.ContainProperty("UpdateDate"))
                    _clientInquierOceanRate.UpdateDate = result.GetValue<DateTime>("UpdateDate");
                else
                    _clientInquierOceanRate.UpdateDate = null;
                //this._oceanRateInfo.ShippingLineName = cmbShipLine.Text;
                //取消控件编辑：编辑内容不在界面显示
                _clientInquierOceanRate.CancelEdit();

                //启动编辑
                _clientInquierOceanRate.BeginEdit();
                //if (Saved != null) Saved(this._oceanRateInfo, new object[] { result });
                //LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), NativeLanguageService.GetText(this, "SaveSuccessfully"));
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                return false;
            }
        }

        
        #endregion

        #region 窗体事件

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
        /// 新增询价
        /// </summary>
        private void tsmNewInquirePrice_Click(object sender, EventArgs e)
        {
            //1.获取当前选中员工
            InquirePriceInquireBys objIPIB = bsInquireBys.Current as InquirePriceInquireBys;
            if (objIPIB == null)
                return;
            //2.提示是否新增询价
            if (!Utility.ShowResultMessage("Are you sure to create a new Inquire Price with the selected staff?"))
            {
                return;
            }
            //3.打开新增询价
            try
            {
                if (ReInquireEvent != null)
                {
                    ReInquireEvent(this, new DataEventArgs<object>(new object()));
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }

        /// <summary>
        /// 更改箱型
        /// </summary>
        private void btnRateUnit_Click(object sender, EventArgs e)
        {
            RateUnitPart unitPart = Workitem.Items.AddNew<RateUnitPart>();
            //RateUnitPart unitPart = new RateUnitPart();
            unitPart.SetSouce(_clientInquierOceanRate.RateUnitList, InquierType.OceanRates);

            DialogResult dr = PartLoader.ShowDialog(unitPart, "Rate Unit", FormBorderStyle.Sizable);

            //直接点X关闭是不会更变箱型的
            if (dr == DialogResult.OK && unitPart.IsChanged)
            {
                txtRateUnitString.Text = string.Empty;
                _clientInquierOceanRate.RateUnitList = unitPart.CurruntUnitList;
                foreach (var item in _clientInquierOceanRate.RateUnitList)
                {
                    txtRateUnitString.Text += item.UnitName + "/";
                }

                //_clientInquierOceanRate.IsDirty = true;

                if (ChangedUnitEvent != null)
                {
                    ChangedUnitEvent(this, _clientInquierOceanRate.ID, _clientInquierOceanRate.RateUnitList);
                }
            }
        }

        private void btnTransit_Click(object sender, EventArgs e)
        {
            TransitPart transitPart = Workitem.Items.AddNew<TransitPart>();
            transitPart.SetSouce(_clientInquierOceanRate);
            DialogResult dr = PartLoader.ShowDialog(transitPart, "Transit", FormBorderStyle.Sizable);

            if (dr == DialogResult.OK)
            {
                txtRespondBy.Text = _clientInquierOceanRate.RespondByName;
                if (TransitEvent != null)
                {
                    TransitEvent(this, new DataEventArgs<object>(new object()));
                }
            }
        }

        /// <summary>
        /// 处理询价
        /// </summary>
        public void HandledInquireRate(object sender,EventArgs e)
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

        #region 方法定义
        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="data">绑定数据对象</param>
        public void DataSourceBind(object data)
        {
            DataSource = data;
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            #region 运输条款

            List<TransportClauseList> transportClauses = TransportFoundationService.GetTransportClauseList(string.Empty, string.Empty, true, 0);
            TransportClauseList emptyTransportClause = new TransportClauseList();
            emptyTransportClause.ID = Guid.Empty;
            emptyTransportClause.Code = string.Empty;
            transportClauses.Insert(0, emptyTransportClause);

            foreach (var item in transportClauses)
            {
                cmbTransportClause.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
            }

            #endregion

            #region 客户搜索器

            //客户搜索器
            DataFindClientService.Register(txtCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
                SearchFieldConstants.ResultValue,
                      delegate(object inputSource, object[] resultData)
                      {
                          txtCustomer.Tag = _clientInquierOceanRate.CustomerID = new Guid(resultData[0].ToString());
                          txtCustomer.Text = _clientInquierOceanRate.CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();

                      }, delegate
                      {
                          txtCustomer.Tag = _clientInquierOceanRate.CustomerID = Guid.Empty;
                          txtCustomer.Text = _clientInquierOceanRate.CustomerName = string.Empty;
                      },
                      ClientConstants.MainWorkspace);

            #endregion

            #region 必填项验证的注册
            _ControlValidator = new ControlValidator();
            _ControlValidator.RegisterField(txtCommodity, ControlValidatorType.IsRequired, labCommodity.Text, labCommodity.Text);
            #endregion
        }

        /// <summary>
        /// 初始化控件事件
        /// </summary>
        private void ControlEvent(bool isAdd)
        {
            if (isAdd)
            {
                tsmNewInquirePrice.Click += tsmNewInquirePrice_Click; //右键菜单：新建询价
                tsmHandled.Click += HandledInquireRate; //Handled处理事件
                tsmUnHandled.Click += HandledInquireRate;//Un Handled处理事件
                bsInquireBys.CurrentChanged += bsInquireBys_CurrentChanged;//行改变事件
                checkEditHandled.EditValueChanged += checkEditHandled_EditValueChanged;//处理复选框改变事件
            }
            else
            {
                tsmNewInquirePrice.Click -= tsmNewInquirePrice_Click; //右键菜单：新建询价
                tsmHandled.Click -= HandledInquireRate; //Handled处理事件
                tsmUnHandled.Click -= HandledInquireRate;//Un Handled处理事件
                bsInquireBys.CurrentChanged -= bsInquireBys_CurrentChanged;//行改变事件
                checkEditHandled.EditValueChanged -= checkEditHandled_EditValueChanged;//处理复选框改变事件
            }
            
            
        }

        /// <summary>
        /// 设置控件状态
        /// </summary>
        private void SetControlState()
        {
            tsmNewInquirePrice.Enabled = true;
            tsmHandled.Visible = true;
            tsmUnHandled.Visible = true;
            //网格无数据、控件启用状态取消
            if (CurrentInquireBy == null)
            {
                tsmNewInquirePrice.Enabled = false;
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
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        public bool ValidateData()
        {
            return _ControlValidator.ValidateData();
        }

        /// <summary>
        /// 设置按钮可用状态
        /// </summary>
        /// <param name="value">Boolean：是否启用</param>
        private void SetTransiteAndChangeUnit(bool value)
        {
            btnRateUnit.Enabled = value;
            btnTransit.Enabled = value;
        }

        #region IDataBind 成员

        public void DataBind(BusinessOperationContext business)
        {
            DataSourceBind(new ClientInquierOceanRate() { ID = business.OperationID,MainRecordID=business.ParentOperationID });
        }

        public void ControlsReadOnly(bool flg)
        {

        }
        #endregion
        #endregion

        #region Comment Code
        //[EventSubscription(InquireRatesCommandConstants.Command_Save)]
        //public void Command_Save(object sender, DataEventArgs<object> e)
        //{
        //    if (this.Visible == false) return;
        //    if (txtCargoReady.Properties.ReadOnly) return;
        //    //if (_oceanRateInfo.IsDirty == false) return;

        //    this.SaveData();
        //} 
        #endregion
    }
}
