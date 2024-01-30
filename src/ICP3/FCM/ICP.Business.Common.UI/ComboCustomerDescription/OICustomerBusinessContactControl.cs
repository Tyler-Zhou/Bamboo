using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.Controls;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanImport.ServiceInterface;
namespace ICP.Business.Common.UI

{
    /// <summary>
    /// 带最近业务和业务联系人的客户编辑控件
    /// </summary>
    public partial class OICustomerBusinessContactControl : XtraUserControl
    {
        private bool isFirstTimeEnter = true;
        public EventHandler FirstTimeEnter;
        private Guid customerID;
        private ContactStage contactStage;
        private OIRecentShipmentPopupContainerControl recentShipmentPopupContainerControl;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]

        public List<CustomerCarrierObjects> ContactList
        {
            get
            {
                return this.comboBusinessContactPopupContainer.ContactList;
            }
            set
            {
                if (!LocalData.IsDesignMode)
                {
                    this.comboBusinessContactPopupContainer.ContactList = value;
                }
            }
        }
        public bool IsContactDataChanged
        {
            get
            {

                return ContactList.Exists(item => item.IsNew || item.IsDirty);

            }
        }
        public ContactStage ContactStage
        {
            get
            {
                return this.contactStage;
            }
            set
            {
                this.contactStage = value;
                if (!LocalData.IsDesignMode)
                {
                    this.comboBusinessContactPopupContainer.SetContactStage(value);
                }
            }
        }

        public ContactType ContactType
        {
            get
            {
                return this.comboBusinessContactPopupContainer.ContactType;
            }
            set {
                this.comboBusinessContactPopupContainer.ContactType = value;
            }


        }

        public void SetOperationContext(BusinessOperationContext operationContext)
        {
            this.comboBusinessContactPopupContainer.SetOperationContext(operationContext);
        }

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        public ICP.Common.ServiceInterface.ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICP.Common.ServiceInterface.ICustomerService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(true)]
        public CustomerDescription CustomerDescription
        {
            get
            {
                return this.comboBusinessContactPopupContainer.CustomerDescription;
            }
            set
            {
                comboBusinessContactPopupContainer.CustomerDescription = value;
            }
        }

        public Guid CustomerID
        {
            get
            {
                return this.customerID;
            }
            set
            {
                if (this.customerID != value)
                {
                    this.customerID = value;
                    if (!LocalData.IsDesignMode)
                    {
                        this.comboBusinessContactPopupContainer.SetCustomerID(value);
                        this.recentShipmentPopupContainerControl.CustomerID = value;
                    }
                }
            }
        }
        private Guid companyID;
        public Guid CompanyID
        {
            get
            {
                return this.companyID;
            }
            set
            {
                if (this.companyID != value)
                {
                    this.companyID = value;

                    if (!LocalData.IsDesignMode)
                    {
                        this.recentShipmentPopupContainerControl.CompanyID = value;
                    }
                }
            }
        }
        private CustomerType customerType = CustomerType.Unknown;

        public CustomerType CustomerType
        {
            get
            {
                return this.customerType;
            }
            set
            {
                this.customerType = value;
            }
        }
        public string CustomerName
        {
            get;
            set;
        }
        public object TradeTermID
        {
            get;
            set;
        }
        public string TradeTermName
        {
            get;
            set;
        }
        public event EventHandler<CommonEventArgs<OceanOrderInfo>> SelectChanged;

        /// <summary>
        /// 禁止修改高度
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Height = 21;
        }
        private bool isSearchRegister = false;
        protected override void OnEnter(EventArgs e)
        {
            if (!isSearchRegister)
            {
                RegisterSearch();
                isSearchRegister = true;
            }
            if (this.FirstTimeEnter != null && isFirstTimeEnter)
            {
                this.FirstTimeEnter(this, e);
                isFirstTimeEnter = false;
            }
            base.OnEnter(e);
        }
        private const string codeName = @"Code/Name";
        public static readonly string[] customerResultValue = new string[] { "ID", "Code", "EName", "CName", "Type", "TradeTermID", "TradeTermName", "State", "CheckedState", "UpdateDate", "Fax", "EMail" };
        private void RegisterSearch()
        {
            //Customer
            DataFindClientService.Register(this.popupContainerEdit, CommonFinderConstants.CustoemrFinder, codeName,
                customerResultValue,
                      delegate(object inputSource, object[] resultData)
                      {
                          Guid oldCustomerId = this.CustomerID;
                          comboBusinessContactPopupContainer.ClosePopup();

                          CustomerStateType state = (CustomerStateType)resultData[7];
                          if (state == CustomerStateType.Invalid)
                          {
                              if (PartLoader.PopCustomerIsInvalid() != DialogResult.Yes)
                              {
                                  return;
                              }
                          }

                          CustomerCodeApplyState? approved = (CustomerCodeApplyState?)resultData[8];
                          if (!approved.HasValue
                              || (approved.HasValue && approved.Value != CustomerCodeApplyState.Passed))
                          {
                              if (approved.Value == CustomerCodeApplyState.Processing)
                              {
                                  DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The customers has not been approved!" : "该客户尚未通过审核!"
                   , LocalData.IsEnglish ? "Tip" : "提示"
                   , MessageBoxButtons.OK
                   , MessageBoxIcon.Question);

                                  return;
                              }
                              else if (approved.Value == CustomerCodeApplyState.UnApply)
                              {
                                  if ((resultData[10] == null ||
                                      string.IsNullOrEmpty(resultData[10].ToString())) &&
                                      (resultData[11] == null ||
                                      string.IsNullOrEmpty(resultData[11].ToString())))
                                  {
                                      DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The customer's fax and E-mail are empty, please add customer information and then apply the code!" : "该客户的传真和邮箱都为空，请补充客户资料后再申请代码!"
                  , LocalData.IsEnglish ? "Tip" : "提示"
                  , MessageBoxButtons.OK
                  , MessageBoxIcon.Question);
                                      return;
                                  }

                                  DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The customer have not yet applied for the code. Whether to apply the code?" : "该客户尚未申请代码，是否要申请代码?"
              , LocalData.IsEnglish ? "Tip" : "提示"
              , MessageBoxButtons.YesNo
              , MessageBoxIcon.Question);
                                  if (result == DialogResult.Yes)
                                  {
                                      CustomerService.ApplyCustomerCode(new Guid(resultData[0].ToString()),
                                                                        LocalData.UserInfo.LoginID,
                                                                        LocalData.IsEnglish ? "Customer AutoApply. Source : order Customer." : "客户代码自动申请。来源：订单 客户。",
                                                                        (DateTime?)resultData[9]);
                                  }

                                  return;
                              }
                              else if (approved.Value == CustomerCodeApplyState.Unpassed)
                              {
                                  if ((resultData[10] == null ||
                                      string.IsNullOrEmpty(resultData[10].ToString())) &&
                                      (resultData[11] == null ||
                                      string.IsNullOrEmpty(resultData[11].ToString())))
                                  {
                                      DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The customer's fax and E-mail are empty, please add customer information and then apply the code!" : "该客户的传真和邮箱都为空，请补充客户资料后再申请代码!"
                  , LocalData.IsEnglish ? "Tip" : "提示"
                  , MessageBoxButtons.OK
                  , MessageBoxIcon.Question);
                                      return;
                                  }

                                  DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show("该客户尚未通过审核，若重新申请代码需要去完善客户资料。是否重新申请代码?"
              , LocalData.IsEnglish ? "Tip" : "提示"
              , MessageBoxButtons.YesNo
              , MessageBoxIcon.Question);
                                  if (result == DialogResult.Yes)
                                  {
                                      CustomerService.ApplyCustomerCode(new Guid(resultData[0].ToString()),
                                                                        LocalData.UserInfo.LoginID,
                                                                        LocalData.IsEnglish ? "Customer AutoApply. Source : order Customer." : "客户代码自动申请。来源：订单 客户。",
                                                                        (DateTime?)resultData[9]);
                                  }

                                  return;
                              }
                          }

                          if (resultData[4] != null)
                          {
                              this.CustomerType = (CustomerType)resultData[4];
                          }

                          this.comboBusinessContactPopupContainer.Tag = this.CustomerID = new Guid(resultData[0].ToString());

                          this.EditValue = this.Text = this.CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();

                          if (resultData[5] != null && (Guid)resultData[5] != Guid.Empty && resultData[6] != null)
                          {
                              this.TradeTermID = resultData[5];
                              this.TradeTermName = resultData[6].ToString();

                          }

                          if (resultData[5] != null
                      && (Guid)resultData[5] != Guid.Empty
                      && resultData[6] != null)
                          {
                              this.TradeTermID = resultData[5];
                              this.TradeTermName = resultData[6].ToString();

                          }
                          this.Tag = this.CustomerID;
                          if (!this.popupContainerEdit.IsPopupOpen)
                          {
                              this.popupContainerEdit.ShowPopup();
                          }
                          if (oldCustomerId != Guid.Empty && this.CustomerID == oldCustomerId)
                          {
                              return;
                          };


                      }, delegate
                      {
                          this.Text = this.CustomerName = string.Empty;
                          
                          this.comboBusinessContactPopupContainer.ClosePopup();
                          this.Tag = this.CustomerID = Guid.Empty;

                      },
                      ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
        }
        private void InitPopupControl()
        {
            if (this.recentShipmentPopupContainerControl == null)
            {
                this.recentShipmentPopupContainerControl = new OIRecentShipmentPopupContainerControl();
                this.recentShipmentPopupContainerControl.SelectChanged += new EventHandler<CommonEventArgs<OceanOrderInfo>>(recentShipmentPopupContainerControl_SelectChanged);
                this.popupContainerEdit.Properties.PopupControl = this.recentShipmentPopupContainerControl;

            }
        }
        public OICustomerBusinessContactControl()
        {
            InitializeComponent();
            if (this.DesignMode == false)
            {
                comboBusinessContactPopupContainer.OnOk += new EventHandler(customerPopupContainerEdit1_OnOk);
                comboBusinessContactPopupContainer.OnClear += new EventHandler(customerPopupContainerEdit1_OnClear);
                comboBusinessContactPopupContainer.OnRefresh += new EventHandler(customerPopupContainerEdit1_OnRefresh);

              
                this.comboBusinessContactPopupContainer.ButtonClick += new ButtonPressedEventHandler(comboBusinessContactPopupContainer_ButtonClick);
                this.popupContainerEdit.ButtonClick += new ButtonPressedEventHandler(popupContainerEdit_ButtonClick);

                popupContainerEdit.QueryCloseUp += new CancelEventHandler(popupContainerEdit_QueryCloseUp);
                this.popupContainerEdit.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
                this.popupContainerEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;


                this.comboBusinessContactPopupContainer.SetLanguage(true);
                string tip = LocalData.IsEnglish ? "Please Input Code or EName or CName." : "请输入代码、中文名称或英文名称.";

                comboBusinessContactPopupContainer.Properties.NullValuePrompt = tip;
                comboBusinessContactPopupContainer.Properties.NullValuePromptShowForEmptyValue = true;


                InitPopupControl();

            }
            this.Disposed += delegate
            {
                if (this.recentShipmentPopupContainerControl != null)
                {
                    this.recentShipmentPopupContainerControl.SelectChanged -= recentShipmentPopupContainerControl_SelectChanged;
                    this.recentShipmentPopupContainerControl.Dispose();

                }
                if (this.comboBusinessContactPopupContainer != null)
                {
                    comboBusinessContactPopupContainer.OnOk -= customerPopupContainerEdit1_OnOk;
                    comboBusinessContactPopupContainer.OnClear -= customerPopupContainerEdit1_OnClear;
                    comboBusinessContactPopupContainer.OnRefresh -= customerPopupContainerEdit1_OnRefresh;
                
                }
        



            };
        }

        void popupContainerEdit_QueryCloseUp(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        void recentShipmentPopupContainerControl_SelectChanged(object sender, CommonEventArgs<OceanOrderInfo> e)
        {
            if (this.SelectChanged != null)
            {
                this.SelectChanged(sender, e);
            }
        }
        /// <summary>
        ///  弹出最近十票业务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void popupContainerEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind != ButtonPredefines.Combo)
            {
                if (this.popupContainerEdit.Properties.PopupControl.Visible)
                {
                    this.popupContainerEdit.QueryCloseUp -= popupContainerEdit_QueryCloseUp;
                    this.popupContainerEdit.ClosePopup();
                    this.popupContainerEdit.QueryCloseUp += popupContainerEdit_QueryCloseUp;

                }
                return;
            }
            if (this.popupContainerEdit.Properties.PopupControl.Visible == false)
            {
                this.popupContainerEdit.ShowPopup();
                
            }
            else
            {
                popupContainerEdit.ClosePopup();
            }
        }


        /// <summary>
        /// 弹出客户联系人详细编辑界面
        /// </summary>
        private void comboBusinessContactPopupContainer_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            if (this.comboBusinessContactPopupContainer.Properties.PopupControl.Visible == false)
            {
                this.comboBusinessContactPopupContainer.ShowPopup();
            }
            else
            {
                comboBusinessContactPopupContainer.ClosePopup();
            }
        }



      

    


        bool IsChildFocused(Control parent)
        {
            if (parent.Focused) return true;

            foreach (Control c in parent.Controls)
            {
                if (this.IsChildFocused(c)) return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasFocused
        {
            get
            {
                bool focused = this.IsChildFocused(this.popupContainerEdit);
                if (focused == false)
                {
                    focused = this.comboBusinessContactPopupContainer.Focused;
                }

                return focused;
            }
        }

        #region 客户描述信息
        void customerPopupContainerEdit1_OnOk(object sender, EventArgs e)
        {
            if (this.OnOk != null)
            {
                OnOk(sender, e);
            }
        }

        void customerPopupContainerEdit1_OnClear(object sender, EventArgs e)
        {
            if (this.OnClear != null)
            {
                OnClear(sender, e);
            }
        }
        void customerPopupContainerEdit1_OnRefresh(object sender, EventArgs e)
        {
            if (this.OnRefresh != null)
            {
                this.OnRefresh(this, null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler OnClear;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler OnOk;

        public event EventHandler OnRefresh;
        /// <summary>
        /// 
        /// </summary>
        public PopupFormPosition PopupFormPosition
        {
            get
            {
                return comboBusinessContactPopupContainer.PopupFormPosition;
            }
            set
            {
                comboBusinessContactPopupContainer.PopupFormPosition = value;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="isEnglish"></param>
        public void SetLanguage(bool isEnglish)
        {
            comboBusinessContactPopupContainer.SetLanguage(isEnglish);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        [Bindable(true)]
        [DefaultValue("")]
        [Localizable(false)]
        [TypeConverter(typeof(StringConverter))]
        public new object Tag
        {
            get
            {
                return popupContainerEdit.Tag;
            }
            set
            {
                if (!LocalData.IsDesignMode)
                {
                    if ((Guid?)popupContainerEdit.Tag != (Guid?)value)
                    {
                        if (this.EditValueChanging != null)
                        {
                            this.EditValueChanging(this, new ChangingEventArgs(popupContainerEdit.Tag, value));
                        }
                        popupContainerEdit.Tag = value;
                        Guid id = popupContainerEdit.Tag == null ? Guid.Empty : (Guid)popupContainerEdit.Tag;
                        this.comboBusinessContactPopupContainer.SetCustomerID(id);
                        
                        if (this.EditValueChanged != null)
                        {
                            this.EditValueChanged(this, EventArgs.Empty);
                        }
                    }
                }
            }
        }

        [Bindable(true)]
        public string Text
        {
            get
            {
                return this.popupContainerEdit.Text;
            }
            set
            {
                if (this.popupContainerEdit.Text != value)
                {
                    this.popupContainerEdit.EditValue = this.popupContainerEdit.Text = value;
                }
            }
        }




        #region Combox特性

        /// <summary>
        /// Occurs when changing the index of the selected value in the combo box editor.
        /// </summary>

        [Category("Events")]
        [Description("Occurs when changing the index of the selected value in the combo box editor.")]
        public event ChangingEventHandler EditValueChanging;

        /// <summary>
        /// Occurs when changing the index of the selected value in the combo box editor.
        /// </summary>
        [Category("Events")]
        [Description("Occurs when changing the index of the selected value in the combo box editor.")]
        public event EventHandler EditValueChanged;

        /// <summary>
        /// Specifies the edit value of the editor.
        /// </summary>   
        [RefreshProperties(RefreshProperties.All)]
        [Category("Data")]
        [Description("Specifies the edit value of the editor.")]
        public object EditValue
        {
            get
            {
                return this.popupContainerEdit.EditValue;
            }
            set
            {
                popupContainerEdit.EditValue = value;
            }
        }

        /// <summary>
        /// Gets or sets the background color of an enabled editor.
        /// </summary>    
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [Description("Gets or sets the background color of an enabled editor.")]
        public override System.Drawing.Color BackColor
        {
            get
            {
                return popupContainerEdit.BackColor;
            }
            set
            {
                popupContainerEdit.BackColor = value;
            }
        }


        /// <summary>
        /// 如果使用BackColor，在“设计时”经常意外丢失，所以增加这个属性
        /// 作者：Royal
        /// 创建时间：2011-05-26 15:46
        /// </summary>    
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [Description("指定的背景色")]
        public Color SpecifiedBackColor
        {
            get
            {
                return this.popupContainerEdit.BackColor;
            }
            set
            {
                this.popupContainerEdit.BackColor = value;
            }
        }

        //
        // 摘要:
        //     Gets settings specific to the combo box editor.
        //[Category("Properties")]
        //[Description("Gets settings specific to the combo box editor.")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //public RepositoryItemLookUpEdit Properties
        //{
        //    get
        //    {
        //        return lookUpEdit1.Properties;
        //    }
        //}


     

        #endregion
    }
}
