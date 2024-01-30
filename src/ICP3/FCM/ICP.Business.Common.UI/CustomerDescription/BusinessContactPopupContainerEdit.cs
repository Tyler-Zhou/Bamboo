//-----------------------------------------------------------------------
// <copyright file="CustomerPopupContainerEdit.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------


namespace ICP.Business.Common.UI
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using Framework.CommonLibrary.Common;
    using Framework.ClientComponents.Controls;
    using System.Collections.Generic;
    using ICP.Common.ServiceInterface.DataObjects;
    using FCM.Common.ServiceInterface.DataObjects;
    using Framework.CommonLibrary.Client;

    /// <summary>
    ///业务联系人控件
    /// </summary>
    public class BusinessContactPopupContainerEdit : PopupContainerEdit
    {
        #region Field、Property
        /// <summary>
        /// 是否正在处理
        /// </summary>
        private bool isProcessing = false;
        /// <summary>
        /// 是否首次获得焦点
        /// </summary>
        private bool isFirstEnter = true;
        /// <summary>
        /// 业务联系人面板
        /// </summary>
        BusinessContactDetailInfoControl _customerDescriptionControl = null;
        /// <summary>
        /// 客户描述
        /// </summary>
        CustomerDescription _customerDescription;
        /// <summary>
        /// 
        /// </summary>
        public new Object Tag
        {
            get
            {
                return base.Tag;
            }
            set
            {
                if (value == null || value == DBNull.Value)
                {
                    base.Tag = Guid.Empty;
                    return;
                }
                if ((base.Tag == null && (Guid)value != Guid.Empty) || (base.Tag != null && (Guid)base.Tag != (Guid)value))
                {
                    if (BeforeEditValueChanged != null)
                    {
                        BeforeEditValueChanged(this, new ChangingEventArgs(base.Tag, value));
                    }
                    base.Tag = value;
                    if (AfterEditValueChanged != null)
                    {
                        AfterEditValueChanged(this, EventArgs.Empty);
                    }

                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public new bool Focused
        {
            get
            {
                bool focused = hasFocused(this);
                if (focused == false)
                {
                    focused = hasFocused(PopupForm);
                }

                return focused;
            }
        }
        /// <summary>
        /// 弹出窗口位置
        /// </summary>
        public PopupFormPosition PopupFormPosition { get; set; }
        /// <summary>
        /// 联系人类型
        /// </summary>
        public ContactType ContactType
        {
            get
            {
                return _customerDescriptionControl.ContactType;
            }
            set
            {
                _customerDescriptionControl.ContactType = value;
            }
        }
        /// <summary>
        /// 联系人信息是否更改
        /// </summary>
        public bool IsContactDataChanged
        {
            get
            {
                return _customerDescriptionControl.IsContactDataChanged;
            }
        }
        /// <summary>
        /// 客户资料信息
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(true)]
        public CustomerDescription CustomerDescription
        {
            get
            {
                _customerDescription = _customerDescriptionControl.CustomerDescription;

                return _customerDescription;
            }
            set
            {
                _customerDescription = value;

                if (_customerDescriptionControl != null)
                {
                    _customerDescriptionControl.SetDataBinding(value);

                }
            }
        }
        /// <summary>
        /// 客户承运人列表
        /// </summary>
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<CustomerCarrierObjects> ContactList
        {
            get
            {
                return _customerDescriptionControl.ContactList;
            }
            set
            {
                if (!LocalData.IsDesignMode)
                {
                    _customerDescriptionControl.ContactList = value;
                }
            }
        }
        #endregion

        #region Custom Event
        /// <summary>
        /// 清空事件
        /// </summary>
        public event EventHandler OnClear;
        /// <summary>
        /// 选择事件
        /// </summary>
        public event EventHandler OnOk;
        /// <summary>
        /// 刷新
        /// </summary>
        public event EventHandler OnRefresh;
        /// <summary>
        /// 联系人值改变后
        /// </summary>
        public event EventHandler AfterEditValueChanged;
        /// <summary>
        /// 联系人值改变之前
        /// </summary>
        public event ChangingEventHandler BeforeEditValueChanged;
        /// <summary>
        /// 是否首次进入
        /// </summary>
        public EventHandler OnFirstEnter;
        #endregion

        #region Constructor
        /// <summary>
        /// 构造函数
        /// </summary>
        public BusinessContactPopupContainerEdit()
        {
            Disposed += delegate
            {
                OnOk = null;
                OnClear = null;
                OnFirstEnter = null;
                _customerDescription = null;
                BeforeEditValueChanged = null;
                AfterEditValueChanged = null;
                if (_customerDescriptionControl != null)
                {
                    _customerDescriptionControl.OnOk -= customerDescriptionControl_OnOk;
                    _customerDescriptionControl.OnClear -= customerDescriptionControl_OnClear;
                    _customerDescriptionControl = null;
                }
            };
            if (DesignMode) return;

            PopupContainerControl popupContainerControl1 = new PopupContainerControl();
            popupContainerControl1.Width = 393;
            popupContainerControl1.Height = 347;

            if (_customerDescriptionControl == null)
            {
                _customerDescriptionControl = new BusinessContactDetailInfoControl();
                _customerDescriptionControl.Dock = DockStyle.Fill;
                _customerDescriptionControl.OnOk += customerDescriptionControl_OnOk;
                _customerDescriptionControl.OnClear += customerDescriptionControl_OnClear;
                _customerDescriptionControl.OnRefresh += _customerDescriptionControl_OnRefresh;
                ButtonClick += BusinessContactPopupContainerEdit_ButtonClick;
                popupContainerControl1.Controls.Add(_customerDescriptionControl);
            }


            Properties.Buttons.AddRange(new[] { new EditorButton(), new EditorButton(ButtonPredefines.SpinRight)});

            Properties.TextEditStyle = TextEditStyles.Standard;
            Properties.PopupControl = popupContainerControl1;

            Properties.PopupSizeable = false;
            PopupFormPosition = PopupFormPosition.Right;
            Properties.ActionButtonIndex = 1;
            Properties.PopupBorderStyle = PopupBorderStyles.Simple;
            Properties.CloseOnLostFocus = false;
            Properties.CloseOnOuterMouseClick = false;


            QueryCloseUp -= CustomerDescriptionControl_QueryCloseUp;
            QueryCloseUp += CustomerDescriptionControl_QueryCloseUp;


        }
        #endregion

        #region Control Event
        /// <summary>
        /// 按钮点击事件：为右侧按钮时显示客户描述
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BusinessContactPopupContainerEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.SpinRight)
            {
                if (Properties.PopupControl.Visible)
                {
                    QueryCloseUp -= popupContainerEdit_QueryCloseUp;
                    ClosePopup();
                    QueryCloseUp += popupContainerEdit_QueryCloseUp;

                }
                else
                {
                    ShowPopup();
                }


            }
            else
            {
                QueryCloseUp -= popupContainerEdit_QueryCloseUp;
                ClosePopup();
                QueryCloseUp += popupContainerEdit_QueryCloseUp;
            }

        }
        /// <summary>
        /// 查询关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void popupContainerEdit_QueryCloseUp(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }
        /// <summary>
        /// 业务联系人面板刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _customerDescriptionControl_OnRefresh(object sender, EventArgs e)
        {
            if (OnRefresh != null)
            {
                OnRefresh(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// 业务联系人面板清空信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void customerDescriptionControl_OnClear(object sender, EventArgs e)
        {
            if (OnClear != null)
            {
                OnClear(sender, new EventArgs());
            }
        }

        /// <summary>
        /// 业务联系人面板确定信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void customerDescriptionControl_OnOk(object sender, EventArgs e)
        {
            base.ClosePopup(PopupCloseMode.Immediate);

            if (_customerDescriptionControl != null)
            {
                _customerDescription = _customerDescriptionControl.CustomerDescription;
            }

            if (OnOk != null)
            {
                OnOk(sender, e);
            }
        }

        /// <summary>
        /// 业务联系人面板查询关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CustomerDescriptionControl_QueryCloseUp(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// 业务联系人面板显示位置
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        protected override Rectangle ConstrainFormBounds(Rectangle r)
        {
            switch (PopupFormPosition)
            {
                case PopupFormPosition.Left:
                    r.Location = new Point(r.Location.X - Properties.PopupControl.Width - Width, r.Location.Y - Height);
                    break;
                case PopupFormPosition.Top:
                    r.Location = new Point(r.Location.X, r.Location.Y - Height - Properties.PopupControl.Height);
                    break;
                case PopupFormPosition.Right:
                    r.Location = new Point(r.Location.X + Width, r.Location.Y - Height);
                    break;
                default:
                    break;
            }
            return base.ConstrainFormBounds(r);
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEnter(EventArgs e)
        {
            if (OnFirstEnter != null && isFirstEnter)
            {
                OnFirstEnter(this, e);
                isFirstEnter = false;
            }
            base.OnEnter(e);

            if (Tag == null || Tag == DBNull.Value)
                return;
            Guid? val = (Guid?)Tag;
            if (val.HasValue && val.Value != Guid.Empty)
            {
                if (IsPopupOpen == false)
                {
                    ShowPopup();

                    Focus();
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Tab)
            {
                if (PopupForm != null)
                {
                    PopupForm.Focus();
                }
            }


        }

        /// <summary>
        /// 失去焦点
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if (this.Focused == false && (this.EditorContainsFocus == false))
            {

                base.ClosePopup(PopupCloseMode.Immediate);
                if (_customerDescriptionControl != null)
                {
                    _customerDescription = _customerDescriptionControl.CustomerDescription;
                }
            }

            if (OnOk != null)
            {
                if (!isProcessing)
                {
                    try
                    {
                        isProcessing = true;
                        OnOk(this, e);
                    }
                    finally
                    {
                        isProcessing = false;
                    }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="closeMode"></param>
        protected override void ClosePopup(PopupCloseMode closeMode)
        {
            if (closeMode == PopupCloseMode.ButtonClick)
            {
                base.ClosePopup(PopupCloseMode.Immediate);

                if (_customerDescriptionControl != null)
                {
                    _customerDescription = _customerDescriptionControl.CustomerDescription;
                }

                if (OnOk != null)
                {
                    OnOk(this, null);
                }
            }
            else
            {
                base.ClosePopup(PopupCloseMode.Cancel);
            }
        }

        #endregion

        #region Custom Method
        /// <summary>
        /// 设置语言环境
        /// </summary>
        /// <param name="isEnglish">是否英文</param>
        public void SetLanguage(bool isEnglish)
        {
            if (_customerDescriptionControl != null)
            {
                _customerDescriptionControl.SetLanguage(isEnglish);
            }
        }
        /// <summary>
        /// 设置业务联系人所属沟通阶段
        /// </summary>
        /// <param name="contactStage"></param>
        public void SetContactStage(ContactStage contactStage)
        {
            _customerDescriptionControl.ContactStage = contactStage;
        }
        /// <summary>
        /// 设置联系人类型
        /// </summary>
        /// <param name="contactType"></param>
        public void SetContactType(ContactType contactType)
        {
            _customerDescriptionControl.ContactType = contactType;
        }
        /// <summary>
        /// 设置关联的业务上下文
        /// </summary>
        /// <param name="operationContext"></param>
        public void SetOperationContext(BusinessOperationContext operationContext)
        {
            _customerDescriptionControl.OperationContext = operationContext;
        }
        /// <summary>
        /// 国家数据列表
        /// </summary>
        public void SetCountryList(List<CountryList> countryList)
        {
            _customerDescriptionControl.SetCountryList(countryList);
        }
        /// <summary>
        /// 设置客户ID
        /// </summary>
        /// <param name="customerID"></param>
        public void SetCustomerID(Guid customerID)
        {
            _customerDescriptionControl.CustomerID = customerID;
        }
        /// <summary>
        /// 获取客户ID
        /// </summary>
        /// <returns></returns>
        public Guid GetCustomerID()
        {
            return _customerDescriptionControl.CustomerID;
        }
        /// <summary>
        /// 关闭面板
        /// </summary>
        public void Close()
        {
            base.ClosePopup(PopupCloseMode.Immediate);
        }
        /// <summary>
        /// 以自定义方法公开继承自PopupContainerEdit关闭组件的方法
        /// 不重新设置客户描述
        /// </summary>
        /// <param name="e"></param>
        public void BaseOnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
        }
        /// <summary>
        /// 以自定义方法公开继承自PopupContainerEdit关闭组件方法
        /// 不重新设置客户描述
        /// </summary>
        /// <param name="closeMode"></param>
        public void BaseClosePopup(PopupCloseMode closeMode)
        {
            base.ClosePopup(closeMode);
        }
        /// <summary>
        /// 是否获得焦点
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        bool hasFocused(Control parent)
        {
            if (parent == null) return false;
            if (parent.Focused) return true;

            foreach (Control c in parent.Controls)
            {
                if (hasFocused(c)) return true;
            }
            return false;
        }
        /// <summary>
        /// 刷新
        /// </summary>
        public void Refresh()
        {
            //if (OnOk != null)
            //{
            //    OnOk(this, null);
            //}
        }
        #endregion
    }
}
