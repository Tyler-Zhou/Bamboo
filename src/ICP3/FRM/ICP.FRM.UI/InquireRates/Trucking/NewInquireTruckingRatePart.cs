using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
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
using System.Diagnostics;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.FRM.UI.InquireRates
{
    [ToolboxItem(false)]
    public partial class NewInquireTruckingRatePart : BaseEditPart
    {
        public NewInquireTruckingRatePart()
        {
            InitializeComponent();
            Load += new EventHandler(page_Load);
            Disposed += delegate {
                Saved = null;
                SmartPartClosing -= BankEdit_SmartPartClosing;
                bsInquierRateInfo.DataSource = null;
                bsInquierRateInfo.Dispose();
                _ControlValidator = null;
                _inquierRateInfo = null;
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

        InquierTruckingRate _inquierRateInfo;
        ControlValidator _ControlValidator;

        //[EventPublication(InquireRatesCommandConstants.Command_AddDiscussing)]
        //public event EventHandler<DataEventArgs<List<InquireDiscussing>>> AddDiscussingEvent;

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
                _inquierRateInfo = new InquierTruckingRate();

                #region 设置默认值

                _inquierRateInfo.ID = Guid.Empty;
                _inquierRateInfo.InquireByID = LocalData.UserInfo.LoginID;
                _inquierRateInfo.InquireByName = LocalData.UserInfo.LoginName;
                _inquierRateInfo.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
                //this._inquierRateInfo.UnitRates = new List<InquireUnit>();
                //// 默认值=20GP/40GP/40HQ
                //InquireUnit unit1 = new InquireUnit();
                //unit1.ID = Guid.Empty;
                //unit1.UnitID = new Guid("C0F279D5-2AB4-E011-9595-001321CC6D9F");
                //unit1.UnitName = "20GP";
                //_inquierRateInfo.UnitRates.Add(unit1);

                //InquireUnit unit2 = new InquireUnit();
                //unit2.ID = Guid.Empty;
                //unit2.UnitID = new Guid("08502FDE-B91C-E011-B1CD-001321CC6D9F");
                //unit2.UnitName = "40GP";
                //_inquierRateInfo.UnitRates.Add(unit2);

                //InquireUnit unit3 = new InquireUnit();
                //unit3.ID = Guid.Empty;
                //unit3.UnitID = new Guid("80B83CDA-2D46-E011-A69C-001321CC6D9F");
                //unit3.UnitName = "40HQ";
                //_inquierRateInfo.UnitRates.Add(unit3);

                #endregion
            }
            else
            {
                _inquierRateInfo = data as InquierTruckingRate;
            }

            bsInquierRateInfo.DataSource = _inquierRateInfo;
            bsInquierRateInfo.ResetBindings(false);

            //txtRateUnitString.Text = "20GP/40GP/40HQ";
           
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
            if (!ValidateData())
            {
                return false;
            }

            //if (!_ControlValidator.ValidateData())
            //{
            //    return false;
            //}

            if (InquireRatesService.CheckExistTruckingRateSamePort(_inquierRateInfo.POLID.Value, _inquierRateInfo.PlaceOfDeliveryID.Value))
            {
                //string message = LocalData.IsEnglish ? "The new Inquire Rate you are creating is duplicated. The route from %From to %To had already been provided." + System.Environment.NewLine + "Please open the page [Search Rate], goto [Trucking Rate]. you can search the rate with the route from %From to %To." : "你正在创建的询价已重复。系统已提供该路线从%from到%to的拖车询价。" + System.Environment.NewLine + "请打开[运价查询]页面，切换到[Trucking Rate]面板，您可以查到此路线从%from到%to的拖车询价。";
                string message = LocalData.IsEnglish ? string.Format("The new Inquire Rate you are creating is duplicated. The route from {0} to {1} had already been provided." + Environment.NewLine + "Please open the page [Quotation], goto [Search Truck Rates]. you can search the rate with the route from {0} to {1}.", _inquierRateInfo.POLName, _inquierRateInfo.PlaceOfDeliveryName) : string.Format("你正在创建的询价已重复。系统已提供该路线从{0}到{1}的拖车询价。" + Environment.NewLine + "请打开[运价查询]页面，切换到[Search Truck Rates]面板，您可以查到此路线从{0}到{1}的拖车询价。", _inquierRateInfo.POLName, _inquierRateInfo.PlaceOfDeliveryName);
                //城市名称From和To相同时
                if (_inquierRateInfo.POLID.Equals(_inquierRateInfo.PlaceOfDeliveryID))
                {
                    message = LocalData.IsEnglish ? string.Format("It's unique for a rate's POD is same with POL. The route from {0} to {1} had already been provided." + Environment.NewLine + "Please open the page [Quotation], goto [Search Truck Rates]. you can search the rate with the route from {0} to {1}.", _inquierRateInfo.POLName, _inquierRateInfo.PlaceOfDeliveryName) : string.Format("起运地和目的地相同时，运价为统一价格，系统已提供该路线从{0}到{1}的拖车询价。" + Environment.NewLine + "请打开[运价查询]页面，切换到[Search Truck Rates]面板，您可以查到此路线从{0}到{1}的拖车询价。", _inquierRateInfo.POLName, _inquierRateInfo.PlaceOfDeliveryName);
                }
                DialogResult result = XtraMessageBox.Show(message
                , LocalData.IsEnglish ? "Inquire Rate is duplicated" : "此询价重复"
                , MessageBoxButtons.OK
                , MessageBoxIcon.Information);

                return false;
            }
            
            try
            {
                //List<Guid> rateIDs = new List<Guid>();
                //List<Guid> rateUnitIDs = new List<Guid>();
                //List<decimal> rateRates = new List<decimal>();

                //foreach (var item in _inquierRateInfo.UnitRates)
                //{
                //    rateIDs.Add(item.ID);
                //    rateUnitIDs.Add(item.UnitID);
                //    rateRates.Add(item.Rate);
                //}

                SingleResult result = InquireRatesService.SaveInquireRateInfo(_inquierRateInfo.ID,_inquierRateInfo.No,
                    InquierType.TruckingRates,
                    new Guid[1],
                    new Guid[1],
                    new decimal[1],
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
                    string.Empty,
                    string.Empty,
                    _inquierRateInfo.ZipCode,
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
                if (result.ContainProperty("UpdateDate"))
                    _inquierRateInfo.UpdateDate = result.GetValue<DateTime>("UpdateDate");
                else
                    _inquierRateInfo.UpdateDate = null;
                _inquierRateInfo.No = result.GetValue<string>("No");
                _inquierRateInfo.Shared = true;
                _inquierRateInfo.IsNoPriceAll = true;

                CurrencyList defaultCurrency = InquireRatesUIDataHelper.Currencys.Find(t => t.Code == "USD"); 
                _inquierRateInfo.CurrencyID = defaultCurrency.ID;
                _inquierRateInfo.CurrencyName = defaultCurrency.EName;
                _inquierRateInfo.DurationFrom = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                _inquierRateInfo.DurationTo = _inquierRateInfo.DurationFrom.Value.AddMonths(3);

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

        #region 界面输入验证
        private bool ValidateData()
        {
            EndEdit();
            bsInquierRateInfo.EndEdit();

            bool isScrrs = true;
            if (Utility.GuidIsNullOrEmpty(_inquierRateInfo.ShippingLineID))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "[Shipline] is required! You must fill-in it." : "请您输入必填项：[Shipline]。");
                isScrrs = false;
            }

            if (Utility.GuidIsNullOrEmpty(_inquierRateInfo.POLID))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "[From] is required! You must fill-in it." : "请您输入必填项：[From]。");
                isScrrs = false;
            }

            if (Utility.GuidIsNullOrEmpty(_inquierRateInfo.PlaceOfDeliveryID))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "[To] is required! You must fill-in it." : "请您输入必填项：[To]。");
                isScrrs = false;
            }

            if (string.IsNullOrEmpty(_inquierRateInfo.ZipCode))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "[Zip Code] is required! You must fill-in it." : "请您输入必填项：[Zip Code]。");
                isScrrs = false;
            }

            if (Utility.GuidIsNullOrEmpty(_inquierRateInfo.RespondByID))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "[Respond By] is required! You must fill-in it." : "请您输入必填项：[Respond By]。");
                isScrrs = false;
            }

            return isScrrs;
        }

        #endregion

        //private bool ValidateData()
        //{
        //    this.EndEdit();
        //    this.bsInquierRateInfo.EndEdit();         

        //    if (!this._inquierRateInfo.Validate())
        //    {
        //        return false;
        //    }
            
        //    return true;
        //}

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

            DataFindClientService.Register(stxtPOL, CommonFinderConstants.LocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      stxtPOL.Text = _inquierRateInfo.POLName = resultData[2].ToString();
                      stxtPOL.Tag = _inquierRateInfo.POLID = new Guid(resultData[0].ToString());
                  }, Guid.Empty, null);

            //dfService.Register(stxtPOD, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
            //  delegate(object inputSource, object[] resultData)
            //  {
            //      stxtPOD.Text = _inquierRateInfo.PODName = resultData[2].ToString();
            //      stxtPOD.Tag = _inquierRateInfo.PODID = new Guid(resultData[0].ToString());
            //  }, Guid.Empty, null);

            DataFindClientService.Register(stxtDelivery, CommonFinderConstants.LocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                delegate(object inputSource, object[] resultData)
                {
                    stxtDelivery.Text = _inquierRateInfo.PlaceOfDeliveryName = resultData[2].ToString();
                    stxtDelivery.Tag = _inquierRateInfo.PlaceOfDeliveryID = new Guid(resultData[0].ToString());
                }, Guid.Empty, null);

            #endregion
        }

        private void btnRateUnit_Click(object sender, EventArgs e)
        {
            RateUnitPart unitPart = Workitem.Items.AddNew<RateUnitPart>();
            //RateUnitPart unitPart = new RateUnitPart();
            unitPart.SetSouce(_inquierRateInfo.UnitRates, InquierType.TruckingRates);

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
            //Utility.SetEnterToExecuteOnec(this.cmbShipline, delegate
            //{
                List<ShippingLineList> list = InquireRatesUIDataHelper.ShippingLines;
                foreach (ShippingLineList item in list)
                {
                    //string name = LocalData.IsEnglish ? item.EName : item.CName;
                    //this.cmbShipline.AddItem(item.ID, name);
                    cmbShipline.Properties.Items.Add(new ImageComboBoxItem(item.EName, item.ID));       
                }
            //});

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
            _ControlValidator.RegisterField(txtZipCode, ControlValidatorType.IsRequired, labZipCode.Text, labZipCode.Text);
            
            #endregion
        }
     
        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Save())
            {
                FindForm().Close();
            }
        }

        private void lkZipCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://tools.usps.com/go/ZipLookupAction!input.action");
        }
    }
}
