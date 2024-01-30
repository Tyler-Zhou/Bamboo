using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.Service;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.FRM.ServiceInterface;
using ICP.FRM.UI.Comm;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.FRM.UI.InquireRates
{
    [ToolboxItem(false)]
    public partial class NewInquireAirRatePart : BaseEditPart
    {
        public NewInquireAirRatePart()
        {
            InitializeComponent();
            Load += new EventHandler(page_Load);
            Disposed += delegate {
                SmartPartClosing -= BankEdit_SmartPartClosing;
                _ControlValidator = null;
                _inquierRateInfo = null;
                bsInquierRateInfo.DataSource = null;
                bsInquierRateInfo.Dispose();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        void page_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                InitMessage();
                InitControls();
                SearchRegister();
              

                SmartPartClosing += BankEdit_SmartPartClosing;
                ActivateSmartPartClosingEvent(Workitem);
            }
        }

        void BankEdit_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            e.Cancel = !Leaving();
        }

        InquierAirRate _inquierRateInfo;
        ControlValidator _ControlValidator;



        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IInquireRatesService InquireRatesService
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

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        public InquireRatesUIDataHelper InquireRatesUIDataHelper
        {
            get
            {
                return ClientHelper.Get<InquireRatesUIDataHelper, InquireRatesUIDataHelper>();
            }
        }

        #endregion
                
        #region IEditPart 成员

        bool isDirty;

        private bool IsDirty
        {
            get
            {
                if(isDirty)
                {
                    return true;
                }
                if (_inquierRateInfo.IsDirty)
                {
                    return true;
                }

                return false;
            }
        }

        public override object DataSource
        {
            get { return bsInquierRateInfo.DataSource; }
            set { BindingData(value); }
        }

        void BindingData(object data)
        {
            if (data == null)
            {
                _inquierRateInfo = new InquierAirRate();

                #region 设置默认值

                _inquierRateInfo.ID = Guid.Empty;
                _inquierRateInfo.InquireByID = LocalData.UserInfo.LoginID;
                _inquierRateInfo.InquireByName = LocalData.UserInfo.LoginName;
                _inquierRateInfo.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);

                _inquierRateInfo.UnitRates = new List<InquireUnit>();
                // 默认值=Min/+45/+100/+300/+500/+800/+1000/+1300
                InquireUnit unit0 = new InquireUnit();
                unit0.ID = Guid.Empty;
                unit0.UnitID = new Guid("9BA1D95D-EF0D-4FE3-8563-BF26E6F9140F");
                unit0.UnitName = "MIN";
                _inquierRateInfo.UnitRates.Add(unit0);

                InquireUnit unit1 = new InquireUnit();
                unit1.ID = Guid.Empty;
                unit1.UnitID = new Guid("187ED281-8732-E111-8E64-001321CC6D9F");
                unit1.UnitName = "+45";
                _inquierRateInfo.UnitRates.Add(unit1);

                InquireUnit unit2 = new InquireUnit();
                unit2.ID = Guid.Empty;
                unit2.UnitID = new Guid("10372C8B-8732-E111-8E64-001321CC6D9F");
                unit2.UnitName = "+100";
                _inquierRateInfo.UnitRates.Add(unit2);

                InquireUnit unit3 = new InquireUnit();
                unit3.ID = Guid.Empty;
                unit3.UnitID = new Guid("66930794-8732-E111-8E64-001321CC6D9F");
                unit3.UnitName = "+300";
                _inquierRateInfo.UnitRates.Add(unit3);

                InquireUnit unit4 = new InquireUnit();
                unit4.ID = Guid.Empty;
                unit4.UnitID = new Guid("46E56F9A-8732-E111-8E64-001321CC6D9F");
                unit4.UnitName = "+500";
                _inquierRateInfo.UnitRates.Add(unit4);

                InquireUnit unit5 = new InquireUnit();
                unit5.ID = Guid.Empty;
                unit5.UnitID = new Guid("DEC3EFA3-8732-E111-8E64-001321CC6D9F");
                unit5.UnitName = "+800";
                _inquierRateInfo.UnitRates.Add(unit5);

                InquireUnit unit6 = new InquireUnit();
                unit6.ID = Guid.Empty;
                unit6.UnitID = new Guid("DFC3EFA3-8732-E111-8E64-001321CC6D9F");
                unit6.UnitName = "+1000";
                _inquierRateInfo.UnitRates.Add(unit6);

                InquireUnit unit7 = new InquireUnit();
                unit7.ID = Guid.Empty;
                unit7.UnitID = new Guid("3EBA17B2-8732-E111-8E64-001321CC6D9F");
                unit7.UnitName = "+1300";
                _inquierRateInfo.UnitRates.Add(unit7);

                #endregion
            }
            else
            {
                _inquierRateInfo = data as InquierAirRate;
            }

            bsInquierRateInfo.DataSource = _inquierRateInfo;
            bsInquierRateInfo.ResetBindings(false);

            //txtRateUnitString.Text = "Min/+45/+100/+300/+500/+800/+1000/+1300";
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
           
            _inquierRateInfo.CancelEdit();
            _inquierRateInfo.BeginEdit();
            _inquierRateInfo.IsDirty = true;
        }

        public override bool SaveData()
        {
            return Save();
        }

        public override void EndEdit()
        {            
            _inquierRateInfo.EndEdit();
        }

        public override event SavedHandler Saved;

        #endregion

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

        private bool Save()
        {
            //if (!this.ValidateData())
            //{
            //    return false;
            //}

            if (!_ControlValidator.ValidateData())
            {
                return false;
            }

            //if (!IsDirty)
            //{
            //    return true;
            //}
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
                    InquierType.AirRates,
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
                    _inquierRateInfo.CartonsOrPallets,
                    _inquierRateInfo.MAWB,
                    _inquierRateInfo.HAWB,
                    string.Empty,
                    _inquierRateInfo.EstimateTimeOfDelivery,
                    _inquierRateInfo.IsWillBooking,
                    _inquierRateInfo.ExpPrice,
                    null,//_inquierRateInfo.DiscussingWhenNew,
                    _inquierRateInfo.RespondByID,
                    _inquierRateInfo.InquireByID,
                    DateTimeOffset.Now,
                    null,
                    LocalData.UserInfo.DefaultCompanyID, LocalData.UserInfo.LoginID);

                _inquierRateInfo.ID = result.GetValue<Guid>("ID");
                if (result.ContainProperty("UpdateDate"))
                    _inquierRateInfo.UpdateDate = result.GetValue<DateTime>("UpdateDate");
                else
                    _inquierRateInfo.UpdateDate = null;
                _inquierRateInfo.Shared = true;
                _inquierRateInfo.IsNoPriceAll = true;

                CurrencyList defaultCurrency = InquireRatesUIDataHelper.Currencys.Find(t => t.Code == "RMB");
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

            DataFindClientService.Register(stxtPOL, CommonFinderConstants.AirLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      stxtPOL.Text = _inquierRateInfo.POLName = resultData[1].ToString();
                      stxtPOL.Tag = _inquierRateInfo.POLID = new Guid(resultData[0].ToString());
                  }, Guid.Empty, null);        
        
            DataFindClientService.Register(stxtDelivery, CommonFinderConstants.AirLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                delegate(object inputSource, object[] resultData)
                {
                    stxtDelivery.Text = _inquierRateInfo.PlaceOfDeliveryName = resultData[1].ToString();
                    stxtDelivery.Tag = _inquierRateInfo.PlaceOfDeliveryID = new Guid(resultData[0].ToString());
                }, Guid.Empty, null);

            #endregion
        }

        private void btnRateUnit_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                RateUnitPart unitPart = Workitem.Items.AddNew<RateUnitPart>();
                unitPart.SetSouce(_inquierRateInfo.UnitRates, InquierType.AirRates);

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
        }

        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }

        private void InitMessage()
        {
            RegisterMessage("1108100001", "确认要作废该数据？");
            RegisterMessage("1108100002", "确认要激活该数据？");
            RegisterMessage("1110250001", "帐号的长度不能超过30位");
            RegisterMessage("1110250002", "备注的的长度不能超过200位");
        }
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

            //航线   
            Utility.SetEnterToExecuteOnec(cmbShipline, delegate
            {
                List<ShippingLineList> list = InquireRatesUIDataHelper.ShippingLines;
                cmbShipline.Properties.BeginUpdate();
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
            _ControlValidator.RegisterField(txtRateUnitString, ControlValidatorType.IsRequired, btnRateUnit.Text, btnRateUnit.Text);
            _ControlValidator.RegisterField(txtCommodity, ControlValidatorType.IsRequired, labCommodity.Text, labCommodity.Text);
            _ControlValidator.RegisterField(txtCargoWeight, ControlValidatorType.IsRequired, labCargoWeight.Text, labCargoWeight.Text);
            _ControlValidator.RegisterField(txtMeasurement, ControlValidatorType.IsRequired, labMeasurement.Text, labMeasurement.Text);
            _ControlValidator.RegisterField(txtCargoReady, ControlValidatorType.IsRequired, labCargoReady.Text, labCargoReady.Text);
            
            #endregion
        }

        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (Save())
                {
                    FindForm().Close();
                }
            }
        }
    }
}
