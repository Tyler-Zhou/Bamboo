#region Comment

/*
 * 
 * FileName:    NewInquireOceanRatePart.cs
 * CreatedOn:   
 * CreatedBy:   LiXuBin
 * 
 * 
 * Description：
 *      ->新增询价信息
 * History：
 *      ->
 * 
 * 
 * 
 */

#endregion

using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.FRM.UI.Comm;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ICP.FRM.UI.InquireRates
{
    /// <summary>
    /// 新增海出询价界面
    /// </summary>
    [ToolboxItem(false)]
    public partial class NewInquireOceanRatePart : BaseEditPart
    {
        #region 服务
        /// <summary>
        /// WorkItem
        /// </summary>
        public WorkItem Workitem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }

        /// <summary>
        /// 询价服务
        /// </summary>
        public IInquireRatesService InquireRatesService
        {
            get
            {
                return ServiceClient.GetService<IInquireRatesService>();
            }
        }

        /// <summary>
        /// 组织服务
        /// </summary>
        public IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<IOrganizationService>();
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

        /// <summary>
        /// 基础数据服务
        /// </summary>
        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        /// <summary>
        /// 用户服务
        /// </summary>
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        /// <summary>
        /// 询价界面帮助类
        /// </summary>
        public InquireRatesUIDataHelper InquireRatesUIDataHelper
        {
            get
            {
                return ClientHelper.Get<InquireRatesUIDataHelper, InquireRatesUIDataHelper>();
            }
        }
        #endregion

        #region 变量
        /// <summary>
        /// 当前海运询价对象
        /// </summary>
        InquierOceanRate _inquierRateInfo;

        /// <summary>
        /// 验证控件
        /// </summary>
        ControlValidator _ControlValidator;
        #endregion

        #region 委托事件
        /// <summary>
        /// 列表面板(InquireOceanRatesListPart)添加新记录
        /// </summary>
        [EventPublication(InquireRatesCommandConstants.Command_AddNewRecord)]
        public event EventHandler<DataEventArgs<InquierOceanRate>> AddNewRecordEvent;
        
        #endregion

        #region 构造函数
        public NewInquireOceanRatePart()
        {
            InitializeComponent();
            Load += new EventHandler(page_Load);
            Disposed += delegate
            {
                Saved = null;
                _ControlValidator = null;
                _inquierRateInfo = null;
                SmartPartClosing -= BankEdit_SmartPartClosing;
                bsInquierRateInfo.DataSource = null;
                bsInquierRateInfo.Dispose();
                AddNewRecordEvent = null;

                if (Workitem != null) Workitem.Items.Remove(this);
            };
        } 
        #endregion

        #region IEditPart 成员
        /// <summary>
        /// 是否修改过
        /// </summary>
        bool isSave;
        /// <summary>
        /// 是否修改过记录
        /// </summary>
        private bool IsDirty
        {
            get
            {
                if (_inquierRateInfo.IsDirty && !isSave)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get { return bsInquierRateInfo.DataSource; }
            set { BindingData(value); }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="data">数据对象</param>
        void BindingData(object data)
        {
            if (data == null)
            {
                _inquierRateInfo = new InquierOceanRate();

                #region 设置默认值

                _inquierRateInfo.ID = Guid.Empty;
                _inquierRateInfo.InquireByID = LocalData.UserInfo.LoginID;
                _inquierRateInfo.InquireByName = LocalData.UserInfo.LoginName;
                _inquierRateInfo.ExpTransportClauseID = new Guid("BC6CF07B-9BEA-4A5B-A1F3-DCBB6BB6BF15");
                _inquierRateInfo.ExpTransportClauseName = "CY-CY";
                _inquierRateInfo.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

                _inquierRateInfo.UnitRates = new List<InquireUnit>();
                // 默认值=20GP/40GP/40HQ
                InquireUnit unit1 = new InquireUnit();
                unit1.ID = Guid.Empty;
                unit1.UnitID = new Guid("C4D1AFB9-94E2-42FD-921E-34EF2D332553");
                unit1.UnitName = "20GP";
                _inquierRateInfo.UnitRates.Add(unit1);

                InquireUnit unit2 = new InquireUnit();
                unit2.ID = Guid.Empty;
                unit2.UnitID = new Guid("9C0619F2-9A75-4DCA-BEE5-606FB5BBAB60");
                unit2.UnitName = "40GP";
                _inquierRateInfo.UnitRates.Add(unit2);

                InquireUnit unit3 = new InquireUnit();
                unit3.ID = Guid.Empty;
                unit3.UnitID = new Guid("3C7C3AB0-D245-41DE-8881-D845A08A1E9A");
                unit3.UnitName = "40HQ";
                _inquierRateInfo.UnitRates.Add(unit3);

                #endregion
            }
            else
            {
                _inquierRateInfo = data as InquierOceanRate;
            }
            if (!Utility.GuidIsNullOrEmpty(_inquierRateInfo.ShippingLineID))
            {
                cmbShipline.ShowSelectedValue(_inquierRateInfo.ShippingLineID
                    , _inquierRateInfo.ShippingLineName);
            }
            if (!Utility.GuidIsNullOrEmpty(_inquierRateInfo.ExpTransportClauseID))
            {
                cmbTransportClause.ShowSelectedValue(_inquierRateInfo.ExpTransportClauseID
                    , _inquierRateInfo.ExpTransportClauseName);
            }
            bsInquierRateInfo.DataSource = _inquierRateInfo;
            bsInquierRateInfo.ResetBindings(false);
            //txtRateUnitString.Text = "20GP/40GP/40HQ";
            string textString = string.Empty;
            if (_inquierRateInfo.UnitRates != null)
            {
                foreach (var item in _inquierRateInfo.UnitRates)
                {
                    if (!string.IsNullOrEmpty(textString))
                    {
                        textString += "/";
                    }

                    textString += item.UnitName;
                }
            }   
            txtRateUnitString.Text = textString;

            //无法找到控制为什么不能自动显示值(已绑定)的原因，手动赋值。
            //cmbShipline.Text = _inquierRateInfo.ShippingLineName;

            _inquierRateInfo.CancelEdit();
            _inquierRateInfo.BeginEdit();
            _inquierRateInfo.IsDirty = true;
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public override bool SaveData()
        {
            return Save();
        }
        /// <summary>
        /// 结束编辑
        /// </summary>
        public override void EndEdit()
        {
            _inquierRateInfo.EndEdit();
        }
        /// <summary>
        /// 保存
        /// </summary>
        public override event SavedHandler Saved;
        #endregion

        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void page_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                InitMessage();
                InitControls();
                SearchRegister();


                SmartPartClosing += new EventHandler<WorkspaceCancelEventArgs>(BankEdit_SmartPartClosing);
                ActivateSmartPartClosingEvent(Workitem);
            }
        }

        /// <summary>
        /// 面板关闭时
        ///     判断用户选择以定义是否取消关闭事件
        /// </summary>
        void BankEdit_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            e.Cancel = !Leaving();
        }

        private void btnRateUnit_Click(object sender, EventArgs e)
        {
            RateUnitPart unitPart = Workitem.Items.AddNew<RateUnitPart>();
            //RateUnitPart unitPart = new RateUnitPart();
            unitPart.SetSouce(_inquierRateInfo.UnitRates, InquierType.OceanRates);

            DialogResult dr = PartLoader.ShowDialog(unitPart, "Rate Unit", FormBorderStyle.Sizable);

            //直接点X关闭是不会更变箱型的
            if (dr == DialogResult.OK && unitPart.IsChanged)
            {
                _inquierRateInfo.UnitRates = unitPart.CurruntUnitList;
                string textString = string.Empty;
                foreach (var item in _inquierRateInfo.UnitRates)
                {
                    if (!string.IsNullOrEmpty(textString))
                    {
                        textString += "/";
                    }

                    textString += item.UnitName;
                }

                txtRateUnitString.Text = textString;
                _inquierRateInfo.IsDirty = true;
            }
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (AddNewRecordEvent != null)
            {
                AddNewRecordEvent(this, new DataEventArgs<InquierOceanRate>(_inquierRateInfo));
            }
            FindForm().Close();
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Save())
            {
                isSave = true;
                //this.FindForm().Close();
                BindingData(_inquierRateInfo);
            }
        } 
        #endregion

        #region 方法定义
        /// <summary>
        /// 是否关闭当前窗体
        ///     提示是否保存数据，选择YES、NO关闭窗体，取消则不关闭
        /// </summary>
        /// <returns></returns>
        public bool Leaving()
        {
            if (IsDirty)
            {
                DialogResult result = Utility.EnquireIsSaveCurrentDataByUpdated();
                if (result == DialogResult.Yes)
                {
                    return Save();
                }
                else if (result == DialogResult.No)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 保存询价--海运
        /// </summary>
        /// <returns>保存结果</returns>
        private bool Save()
        {
            //验证
            if (!_ControlValidator.ValidateData())
            {
                return false;
            }
            //结束编辑
            bsInquierRateInfo.EndEdit();
            try
            {
                List<Guid> rateIDs = new List<Guid>();
                List<Guid> rateUnitIDs = new List<Guid>();
                List<decimal> rateRates = new List<decimal>();

                foreach (var item in _inquierRateInfo.UnitRates)
                {
                    rateIDs.Add(item.ID);
                    rateUnitIDs.Add(item.UnitID);
                    rateRates.Add(item.Rate);
                }

                SingleResult result = InquireRatesService.SaveInquireRateInfo(_inquierRateInfo.ID,_inquierRateInfo.No,
                    InquierType.OceanRates,
                    rateIDs.ToArray(),
                    rateUnitIDs.ToArray(),
                    rateRates.ToArray(),
                    _inquierRateInfo.ShippingLineID.Value,
                    _inquierRateInfo.CustomerID,
                    _inquierRateInfo.ExpCarrierName,
                    _inquierRateInfo.POLID,
                    _inquierRateInfo.PODID,
                    _inquierRateInfo.PlaceOfDeliveryID,
                    _inquierRateInfo.ExpCommodity,
                    _inquierRateInfo.ExpTransportClauseID,
                    _inquierRateInfo.CargoWeight,
                    _inquierRateInfo.Measurement,
                    _inquierRateInfo.CargoReady,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    _inquierRateInfo.EstimateTimeOfDelivery,
                    _inquierRateInfo.IsWillBooking,
                    _inquierRateInfo.ExpPrice,
                    _inquierRateInfo.DiscussingWhenNew,
                    _inquierRateInfo.RespondByID,
                    _inquierRateInfo.InquireByID,
                    DateTimeOffset.Now,
                    null,
                    LocalData.UserInfo.DefaultCompanyID, LocalData.UserInfo.LoginID);

                _inquierRateInfo.ID = result.GetValue<Guid>("ID");
                _inquierRateInfo.No = result.GetValue<String>("No");
                if (result.ContainProperty("UpdateDate"))
                    _inquierRateInfo.UpdateDate = result.GetValue<DateTime>("UpdateDate");
                _inquierRateInfo.Shared = true;
                _inquierRateInfo.IsNoPriceAll = true;

                CurrencyList defaultCurrency = InquireRatesUIDataHelper.Currencys.Find(t => t.Code == "USD");
                _inquierRateInfo.CurrencyID = defaultCurrency.ID;
                _inquierRateInfo.CurrencyName = defaultCurrency.EName;
                //this._inquierRateInfo.ShippingLineName = cmbShipLine.Text;
                _inquierRateInfo.CancelEdit();
                _inquierRateInfo.BeginEdit();
                if (Saved != null) Saved(_inquierRateInfo, new object[] { result });
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "SaveSuccessfully"));
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                return false;
            }
        }

        /// <summary>
        /// 初始化提示消息
        /// </summary>
        private void InitMessage()
        {
            RegisterMessage("1108100001", "确认要作废该数据？");
            RegisterMessage("1108100002", "确认要激活该数据？");
            RegisterMessage("1110250001", "帐号的长度不能超过30位");
            RegisterMessage("1110250002", "备注的的长度不能超过200位");
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            #region 运输条款
            Utility.SetEnterToExecuteOnec(cmbTransportClause, delegate
            {
                List<TransportClauseList> transportClauses = TransportFoundationService.GetTransportClauseList(string.Empty, string.Empty, true, 0);

                TransportClauseList emptyTransportClause = new TransportClauseList();
                emptyTransportClause.ID = Guid.Empty;
                emptyTransportClause.Code = string.Empty;
                cmbTransportClause.Properties.BeginUpdate();
                cmbTransportClause.Properties.Items.Clear();
                transportClauses.Insert(0, emptyTransportClause);

                foreach (var item in transportClauses)
                {
                    cmbTransportClause.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
                }
                cmbTransportClause.Properties.EndUpdate();
            });

            #endregion

            //航线   
            Utility.SetEnterToExecuteOnec(cmbShipline, delegate
            {
                List<ShippingLineList> list = InquireRatesUIDataHelper.ShippingLines;
                cmbShipline.Properties.BeginUpdate();
                cmbShipline.Properties.Items.Clear();
                foreach (ShippingLineList item in list)
                {
                    cmbShipline.Properties.Items.Add(new ImageComboBoxItem(item.EName, item.ID));
                }
                cmbShipline.Properties.EndUpdate();
            });

            //回复人
            Utility.SetEnterToExecuteOnec(cmbRespondBy, delegate
            {
                List<UserList> userList = UserService.GetUnderlingUserList(new Guid[0], new string[] { "商务员", "远东区商务员" }, new string[0], true).OrderBy(i => (LocalData.IsEnglish ? i.EName : i.CName)).ToList();
                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", "Name");
                col.Add("Code", "Code");
                cmbRespondBy.InitSource<UserList>(userList, col, (LocalData.IsEnglish ? "EName" : "CName"), "ID");
            });

            #region 必填项验证的注册
            _ControlValidator = new ControlValidator();
            _ControlValidator.RegisterField(cmbShipline, ControlValidatorType.IsRequired, labShipline.Text, labShipline.Text);
            _ControlValidator.RegisterField(stxtPOL, ControlValidatorType.IsRequired, labPOL.Text, labPOL.Text);
            _ControlValidator.RegisterField(stxtDelivery, ControlValidatorType.IsRequired, labDelivery.Text, labDelivery.Text);
            _ControlValidator.RegisterField(cmbRespondBy, ControlValidatorType.IsRequired, labRespondBy.Text, labRespondBy.Text);
            _ControlValidator.RegisterField(txtCommodity, ControlValidatorType.IsRequired, labCommodity.Text, labCommodity.Text);
            _ControlValidator.RegisterField(txtRateUnitString, ControlValidatorType.IsRequired, btnRateUnit.Text, btnRateUnit.Text);
            _ControlValidator.RegisterField(cmbTransportClause, ControlValidatorType.IsRequired, labExpectedTerm.Text, labExpectedTerm.Text);

            #endregion
        }

        /// <summary>
        /// 查询注册
        /// </summary>
        void SearchRegister()
        {
            #region 客户搜索器

            //客户搜索器
            DataFindClientService.Register(txtCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
                SearchFieldConstants.ResultValue,
                      delegate(object inputSource, object[] resultData)
                      {
                          txtCustomer.Tag = _inquierRateInfo.CustomerID = new Guid(resultData[0].ToString());
                          txtCustomer.Text = _inquierRateInfo.CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();

                      }, delegate
                      {
                          txtCustomer.Tag = _inquierRateInfo.CustomerID = Guid.Empty;
                          txtCustomer.Text = _inquierRateInfo.CustomerName = string.Empty;
                      },
                      ClientConstants.MainWorkspace);

            #endregion

            #region Port

            DataFindClientService.Register(stxtPOL, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      stxtPOL.Text = _inquierRateInfo.POLName = resultData[2].ToString();
                      stxtPOL.Tag = _inquierRateInfo.POLID = new Guid(resultData[0].ToString());
                  }, Guid.Empty, null);

            DataFindClientService.Register(stxtPOD, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
              delegate(object inputSource, object[] resultData)
              {
                  stxtPOD.Text = _inquierRateInfo.PODName = resultData[2].ToString();
                  stxtPOD.Tag = _inquierRateInfo.PODID = new Guid(resultData[0].ToString());
              }, Guid.Empty, null);

            DataFindClientService.Register(stxtDelivery, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                delegate(object inputSource, object[] resultData)
                {
                    stxtDelivery.Text = _inquierRateInfo.PlaceOfDeliveryName = resultData[2].ToString();
                    stxtDelivery.Tag = _inquierRateInfo.PlaceOfDeliveryID = new Guid(resultData[0].ToString());
                }, Guid.Empty, null);

            #endregion
        }

        /// <summary>
        /// 验证数据是否有效
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            EndEdit();
            bsInquierRateInfo.EndEdit();

            if (!_inquierRateInfo.Validate())
            {
                return false;
            }
            return true;
        } 
        #endregion
    }
}
