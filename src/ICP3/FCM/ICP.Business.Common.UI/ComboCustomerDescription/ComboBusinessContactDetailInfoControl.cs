using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ICP.Business.Common.UI
{
    /// <summary>
    /// 联系人明细控件
    /// </summary>
    public partial class ComboBusinessContactDetailInfoControl : XtraUserControl
    {
        #region Fields & Property & Services & Delegate
        /// <summary>
        /// 是否首次键入
        /// </summary>
        private bool isFirstTimeEnter = true;
        /// <summary>
        /// 是否注册查询
        /// </summary>
        private bool isRegisterSearch = false;
        /// <summary>
        /// 是否已注册查询
        /// </summary>
        private bool isSearchRegistered = false;
        /// <summary>
        /// 是否显示搜索器按钮
        /// </summary>
        private bool isDisplaySearchButton = false;

        /// <summary>
        /// 代码名称
        /// </summary>
        private const string codeName = @"Code/Name";
        /// <summary>
        /// 客户结果字段集合
        /// </summary>
        public static readonly string[] customerResultValue = new string[] { "ID", "Code", "EName", "CName", "Type", "TradeTermID", "TradeTermName", "State", "CheckedState", "Term", "UpdateDate", "Fax", "EMail", "CAddress", "BankAccountNo", "TaxIdNo" };

        #region 业务联系人所属沟通阶段
        /// <summary>
        /// 业务联系人所属沟通阶段
        /// </summary>
        private ContactStage contactStage;
        /// <summary>
        /// 业务联系人所属沟通阶段
        /// </summary>
        public ContactStage ContactStage
        {
            get
            {
                return contactStage;
            }
            set
            {
                contactStage = value;
                customerPopupContainerEdit1.SetContactStage(value);
            }
        }
        #endregion

        #region 是否显示搜索器按钮
        /// <summary>
        /// 是否显示搜索器按钮
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDisplaySearchButton
        {
            get
            {
                return isDisplaySearchButton;
            }
            set
            {

                isDisplaySearchButton = value;
                if (value)
                {
                    lookUpEdit1.Properties.Buttons.AddRange(new EditorButton[] {
                       new EditorButton()
                     });
                    isRegisterSearch = true;

                }

            }
        }
        #endregion

        #region 联系人列表
        /// <summary>
        /// 联系人列表
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<CustomerCarrierObjects> ContactList
        {
            get
            {
                return customerPopupContainerEdit1.ContactList;
            }
            set
            {
                if (!LocalData.IsDesignMode)
                {
                    customerPopupContainerEdit1.ContactList = value;
                }
            }
        }
        #endregion

        #region 联系人类型
        /// <summary>
        /// 联系人类型
        /// </summary>
        public ContactType ContactType
        {
            get
            {
                return customerPopupContainerEdit1.ContactType;
            }
            set
            {
                customerPopupContainerEdit1.ContactType = value;
            }
        }
        #endregion

        #region 客户ID
        /// <summary>
        /// 客户ID
        /// </summary>
        private Guid customerID;
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerID
        {
            get
            {
                return customerID;
            }
            set
            {
                customerID = value;
                customerPopupContainerEdit1.SetCustomerID(value);
            }
        }
        #endregion

        #region 客户类型
        /// <summary>
        /// 客户类型
        /// </summary>
        private CustomerType customerType = CustomerType.Unknown;
        /// <summary>
        /// 客户类型
        /// </summary>
        public CustomerType CustomerType
        {
            get
            {
                return customerType;
            }
            set
            {
                customerType = value;
            }
        }
        #endregion

        #region 联系人数据是否改变
        /// <summary>
        /// 联系人数据是否改变
        /// </summary>
        public bool IsContactDataChanged
        {
            get
            {
                return ContactList.Exists(item => item.IsNew || item.IsDirty);
            }
        }
        #endregion

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            get;
            set;
        }
        /// <summary>
        /// 贸易条款ID
        /// </summary>
        public object TradeTermID
        {
            get;
            set;
        }
        /// <summary>
        /// 贸易条款
        /// </summary>
        public string TradeTermName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayMember
        {
            get
            {
                return lookUpEdit1.Properties.DisplayMember;
            }
            set
            {
                lookUpEdit1.Properties.DisplayMember = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ValueMember
        {
            get
            {
                return lookUpEdit1.Properties.ValueMember;
            }
            set
            {
                lookUpEdit1.Properties.ValueMember = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public object DataSource
        {
            get
            {
                return lookUpEdit1.Properties.DataSource;
            }
            set
            {
                InitColumn();
                lookUpEdit1.Properties.BeginUpdate();
                lookUpEdit1.Properties.DataSource = value;

                lookUpEdit1.Properties.EndUpdate();


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
        /// 公共客户管理服务
        /// </summary>
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        /// <summary>
        /// 首次键入
        /// </summary>
        public EventHandler FirstTimeEnter;

        #endregion

        #region 构造函数 & Override
        /// <summary>
        /// 构造函数
        /// </summary>
        public ComboBusinessContactDetailInfoControl()
        {
            InitializeComponent();

            if (DesignMode == false)
            {
                customerPopupContainerEdit1.OnOk += customerPopupContainerEdit1_OnOk;
                customerPopupContainerEdit1.OnClear += customerPopupContainerEdit1_OnClear;
                customerPopupContainerEdit1.OnRefresh += customerPopupContainerEdit1_OnRefresh;

                lookUpEdit1.EditValueChanging += lookUpEdit1_EditValueChanging;
                lookUpEdit1.EditValueChanged += lookUpEdit1_EditValueChanged;
                lookUpEdit1.Closed += lookUpEdit1_Closed;

                customerPopupContainerEdit1.SetLanguage(true);
            }
            Disposed += delegate
            {
                if (customerPopupContainerEdit1 != null)
                {
                    customerPopupContainerEdit1.OnOk -= customerPopupContainerEdit1_OnOk;
                    customerPopupContainerEdit1.OnClear -= customerPopupContainerEdit1_OnClear;
                    customerPopupContainerEdit1.OnRefresh -= customerPopupContainerEdit1_OnRefresh;
                }
                if (lookUpEdit1 != null)
                {

                    lookUpEdit1.Closed -= lookUpEdit1_Closed;
                    lookUpEdit1.EditValueChanged -= lookUpEdit1_EditValueChanged;
                    lookUpEdit1.EditValueChanging -= lookUpEdit1_EditValueChanging;
                }
            };
        }

        /// <summary>
        /// 禁止修改高度
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Height = 21;
        }

        /// <summary>
        /// Enter
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEnter(EventArgs e)
        {
            if (!isSearchRegistered && isRegisterSearch)
            {
                RegisterSearch();
                isSearchRegistered = true;
            }
            if (FirstTimeEnter != null && isFirstTimeEnter)
            {
                FirstTimeEnter(this, e);
                isFirstTimeEnter = false;
            }
            base.OnEnter(e);
        }
        #endregion

        /// <summary>
        /// 客户值改变
        /// </summary>
        void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {

            if (EditValueChanged != null)
            {
                EditValueChanged(sender, e);
            }


        }
        /// <summary>
        /// 客户值改变ing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lookUpEdit1_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (EditValueChanging != null)
            {
                EditValueChanging(sender, e);
            }
        }
        /// <summary>
        /// 客户控件关闭：显示明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lookUpEdit1_Closed(object sender, ClosedEventArgs e)
        {
            if (e.CloseMode == PopupCloseMode.Normal)
            {
                if (customerPopupContainerEdit1.IsPopupOpen == false)
                {
                    customerPopupContainerEdit1.ShowPopup();
                }
            }
        }
        
        #region 客户描述信息
        #region Fields & Property
        /// <summary>
        /// 清空
        /// </summary>
        public event EventHandler OnClear;
        /// <summary>
        /// 确定
        /// </summary>
        public event EventHandler OnOk;
        /// <summary>
        /// 刷新
        /// </summary>
        public event EventHandler OnRefresh;

        /// <summary>
        /// 明细控件显示位置
        /// </summary>
        public PopupFormPosition PopupFormPosition
        {
            get
            {
                return customerPopupContainerEdit1.PopupFormPosition;
            }
            set
            {
                customerPopupContainerEdit1.PopupFormPosition = value;
            }
        }
        /// <summary>
        /// 客户描述信息
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(true)]
        public CustomerDescription CustomerDescription
        {
            get
            {
                return customerPopupContainerEdit1.CustomerDescription;
            }
            set
            {
                customerPopupContainerEdit1.CustomerDescription = value;
            }
        }
        /// <summary>
        /// Tab标记
        /// </summary>
        [Bindable(true)]
        [DefaultValue("")]
        [Localizable(false)]
        [TypeConverter(typeof(StringConverter))]
        public new object Tag
        {
            get
            {
                return customerPopupContainerEdit1.Tag;
            }
            set
            {
                customerPopupContainerEdit1.Tag = value;
            }
        }
        #endregion

        /// <summary>
        /// 客户明细面板刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void customerPopupContainerEdit1_OnRefresh(object sender, EventArgs e)
        {
            if (OnRefresh != null)
            {
                OnRefresh(this, null);
            }
        }
        /// <summary>
        /// 客户明细面板确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void customerPopupContainerEdit1_OnOk(object sender, EventArgs e)
        {
            if (OnOk != null)
            {
                OnOk(sender, e);
            }
        }
        /// <summary>
        /// 客户明细面板清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void customerPopupContainerEdit1_OnClear(object sender, EventArgs e)
        {
            if (OnClear != null)
            {
                OnClear(sender, e);
            }
        }
        
        /// <summary>
        /// 设置显示语言
        /// </summary>
        /// <param name="isEnglish"></param>
        public void SetLanguage(bool isEnglish)
        {
            customerPopupContainerEdit1.SetLanguage(isEnglish);
        }
        #endregion

        /// <summary>
        /// 初始化列
        /// </summary>
        private void InitColumn()
        {
            if (string.IsNullOrEmpty(ValueMember) == false)
            {
                lookUpEdit1.Properties.Columns.Clear();
                lookUpEdit1.Properties.Columns.AddRange(new LookUpColumnInfo[] {
            new LookUpColumnInfo(DisplayMember, DisplayMember, 20, FormatType.None, "", true, HorzAlignment.Default, ColumnSortOrder.Ascending)});
            }
        }

        /// <summary>
        /// 设置业务操作上下文
        /// </summary>
        /// <param name="operationContext"></param>
        public void SetOperationContext(BusinessOperationContext operationContext)
        {
            customerPopupContainerEdit1.SetOperationContext(operationContext);
        }
       
        /// <summary>
        /// 注册查询
        /// </summary>
        private void RegisterSearch()
        {
            //Customer
            DataFindClientService.Register(lookUpEdit1, CommonFinderConstants.CustoemrFinder, codeName,
                customerResultValue,
                      delegate(object inputSource, object[] resultData)
                      {   
                         
                          Guid oldCustomerId = CustomerID;
                          customerPopupContainerEdit1.ClosePopup();
                          Guid id = new Guid(resultData[0].ToString());

                          List<OperationCustomer> _TradeCustoemrs = new List<OperationCustomer>();

                            _TradeCustoemrs = lookUpEdit1.Properties.DataSource as List<OperationCustomer>;
                          if (_TradeCustoemrs == null)
                          {
                              _TradeCustoemrs = new List<OperationCustomer>();
                          }

                          OperationCustomer tager = _TradeCustoemrs.Find(delegate(OperationCustomer item) { return item.ID == id; });

                          if (tager != null)
                          {
                              lookUpEdit1.Text = resultData[2].ToString();
                              EditValue = id;
                          }
                          else
                          {
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
                                      DialogResult result = XtraMessageBox.Show(LocalData.IsEnglish ? "The customers has not been approved!" : "该客户尚未通过审核!"
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
                                          XtraMessageBox.Show(LocalData.IsEnglish ? "The customer's fax and E-mail are empty, please add customer information and then apply the code!" : "该客户的传真和邮箱都为空，请补充客户资料后再申请代码!"
                      , LocalData.IsEnglish ? "Tip" : "提示"
                      , MessageBoxButtons.OK
                      , MessageBoxIcon.Question);
                                          return;
                                      }

                                      DialogResult result = XtraMessageBox.Show(LocalData.IsEnglish ? "The customer have not yet applied for the code. Whether to apply the code?" : "该客户尚未申请代码，是否要申请代码?"
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
                                          XtraMessageBox.Show(LocalData.IsEnglish ? "The customer's fax and E-mail are empty, please add customer information and then apply the code!" : "该客户的传真和邮箱都为空，请补充客户资料后再申请代码!"
                      , LocalData.IsEnglish ? "Tip" : "提示"
                      , MessageBoxButtons.OK
                      , MessageBoxIcon.Question);
                                          return;
                                      }

                                      DialogResult result = XtraMessageBox.Show("该客户尚未通过审核，若重新申请代码需要去完善客户资料。是否重新申请代码?"
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
                                  CustomerType = (CustomerType)resultData[4];
                              }

                              customerPopupContainerEdit1.Tag = CustomerID = id;
                              Text = CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                              tager = new OperationCustomer();
                              tager.ID = id;
                              tager.Code = resultData[1].ToString();
                              tager.EName = resultData[2].ToString();
                              tager.CName = resultData[3].ToString();
                              tager.Term = int.Parse(resultData[9].ToString());
                              _TradeCustoemrs.Insert(0, tager);
                             
                              EditValue = id;
                              lookUpEdit1.Text = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                              if (resultData[5] != null && (Guid)resultData[5] != Guid.Empty && resultData[6] != null)
                              {
                                  TradeTermID = resultData[5];
                                  TradeTermName = resultData[6].ToString();
                              }

                              if (resultData[5] != null
                          && (Guid)resultData[5] != Guid.Empty
                          && resultData[6] != null)
                              {
                                  TradeTermID = resultData[5];
                                  TradeTermName = resultData[6].ToString();

                              }
                              Tag = CustomerID;
                              if (!lookUpEdit1.IsPopupOpen)
                              {
                                  lookUpEdit1.ShowPopup();
                              }
                              if (oldCustomerId != Guid.Empty && CustomerID == oldCustomerId)
                              {
                                  return;
                              };
                          }

                      }, delegate
                      {
                          Text = CustomerName = string.Empty;
                          EditValue = Guid.Empty;
                          customerPopupContainerEdit1.ClosePopup();
                          Tag = CustomerID = Guid.Empty;

                      },
                      ClientConstants.MainWorkspace);
        }

        bool IsChildFocused(Control parent)
        {
            if (parent.Focused) return true;

            foreach (Control c in parent.Controls)
            {
                if (IsChildFocused(c)) return true;
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
                bool focused = IsChildFocused(lookUpEdit1);
                if (focused == false)
                {
                    focused = customerPopupContainerEdit1.Focused;
                }

                return focused;
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
                
                return lookUpEdit1.EditValue;
            }
            set
            {   
                
                lookUpEdit1.EditValueChanged -= lookUpEdit1_EditValueChanged;
                lookUpEdit1.EditValueChanging -= lookUpEdit1_EditValueChanging;
                if (value == null || value == DBNull.Value)
                {
                    if (EditValueChanging != null)
                    {
                        EditValueChanging(this, new ChangingEventArgs(lookUpEdit1.EditValue, value));
                    }
                    lookUpEdit1.EditValue = Guid.Empty;
                    if (EditValueChanged != null)
                    {
                        EditValueChanged(this, EventArgs.Empty);
                    }
                    lookUpEdit1.EditValueChanged += lookUpEdit1_EditValueChanged;
                    lookUpEdit1.EditValueChanging += lookUpEdit1_EditValueChanging;
                    return;
                }
                if ((lookUpEdit1.EditValue == null && (Guid)value != Guid.Empty) || (lookUpEdit1.EditValue != null && (Guid)lookUpEdit1.EditValue != (Guid)value))
                {
                    if (EditValueChanging != null)
                    {
                        EditValueChanging(this, new ChangingEventArgs(lookUpEdit1.EditValue, value));
                    }
                    lookUpEdit1.EditValue = value;
                    Guid customerId = lookUpEdit1.EditValue == null ? Guid.Empty : (Guid)lookUpEdit1.EditValue;
                    customerID = customerId;
                    customerPopupContainerEdit1.SetCustomerID(customerId);
                    if (EditValueChanged != null)
                    {
                        EditValueChanged(this, EventArgs.Empty);
                    }

                }
                lookUpEdit1.EditValueChanged += lookUpEdit1_EditValueChanged;
                lookUpEdit1.EditValueChanging += lookUpEdit1_EditValueChanging;
            }
        }

        /// <summary>
        /// Gets or sets the background color of an enabled editor.
        /// </summary>    
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [Description("Gets or sets the background color of an enabled editor.")]
        public override Color BackColor
        {
            get
            {
                return lookUpEdit1.BackColor;
            }
            set
            {
                lookUpEdit1.BackColor = value;
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
                return lookUpEdit1.BackColor;
            }
            set
            {
                lookUpEdit1.BackColor = value;
            }
        }

        

        

        #endregion

        #region Comment Code
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
