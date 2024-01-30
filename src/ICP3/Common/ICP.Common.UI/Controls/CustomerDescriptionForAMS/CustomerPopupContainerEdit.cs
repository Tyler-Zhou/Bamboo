

//-----------------------------------------------------------------------
// <copyright file="CustomerDescriptionForAMSControl.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.Common.UI.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using ICP.Common.ServiceInterface;
    using ICP.Common.ServiceInterface.DataObjects;

    /// <summary>
    /// 客户资料选择控件
    /// </summary>
    public class CustomerPopupContainerForAMSEdit : PopupContainerEdit
    {

        #region 构造函数

        CustomerDescriptionForAMSControl _customerDescriptionForAMSControl = null;
        private bool isProcessing = false;
        public IGeographyService geographyService
        {
            get
            {
                if (_customerDescriptionForAMSControl != null)
                {
                    return _customerDescriptionForAMSControl.geographyService;
                }
                return null;
            }
            set
            {
                if (_customerDescriptionForAMSControl != null)
                {
                    _customerDescriptionForAMSControl.geographyService = value;
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public CustomerPopupContainerForAMSEdit()
            : base()
        {
            if (this.DesignMode) return;

            DevExpress.XtraEditors.PopupContainerControl popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            popupContainerControl1.Width = 268;
            popupContainerControl1.Height = 245;

            if (_customerDescriptionForAMSControl == null)
            {
                _customerDescriptionForAMSControl = new CustomerDescriptionForAMSControl();
                _customerDescriptionForAMSControl.Dock = DockStyle.Fill;
                _customerDescriptionForAMSControl.OnOk += new EventHandler(customerDescriptionControl_OnOk);
                _customerDescriptionForAMSControl.OnClear += new EventHandler(customerDescriptionControl_OnClear);
                popupContainerControl1.Controls.Add(_customerDescriptionForAMSControl);
            }


            this.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});

            this.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.Properties.PopupControl = popupContainerControl1;
            this.Properties.ShowPopupCloseButton = false;
            this.Properties.PopupSizeable = false;
            this.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.Properties.ActionButtonIndex = 1;
            this.Properties.PopupBorderStyle = PopupBorderStyles.Simple;

            this.TextChanged += new EventHandler(CustomerPopupContainerEdit_TextChanged);

            this.QueryCloseUp -= new CancelEventHandler(CustomerDescriptionControl_QueryCloseUp);
            this.QueryCloseUp += new CancelEventHandler(CustomerDescriptionControl_QueryCloseUp);

            this.Disposed += delegate
            {

                this._customerDescriptionForAMS = null;
                this.TextChanged -= new EventHandler(CustomerPopupContainerEdit_TextChanged);

                this.QueryCloseUp -= new CancelEventHandler(CustomerDescriptionControl_QueryCloseUp);
                this.OnOk = null;
                this.OnClear = null;
            };


        }

        #endregion


        #region 公共属性，方法，事件
        /// <summary>
        /// 弹出窗口位置
        /// </summary>
        public ICP.Framework.ClientComponents.Controls.PopupFormPosition PopupFormPosition { get; set; }

        /// <summary>
        /// 清空事件
        /// </summary>
        public event EventHandler OnClear;

        /// <summary>
        /// 选择事件
        /// </summary>
        public event EventHandler OnOk;

        CustomerDescriptionForAMS _customerDescriptionForAMS;
        /// <summary>
        /// 客户资料信息
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(false)]
        public CustomerDescriptionForAMS CustomerDescriptionForAMS
        {
            get
            {
                _customerDescriptionForAMS = _customerDescriptionForAMSControl.CustomerDescriptionForAMS;

                return _customerDescriptionForAMS;
            }
            set
            {
                _customerDescriptionForAMS = value;

                if (_customerDescriptionForAMSControl != null)
                {
                    _customerDescriptionForAMSControl.SetDataBinding(value);
                }
            }
        }

        /// <summary>
        /// 设置语言环境
        /// </summary>
        /// <param name="isEnglish">是否英文</param>
        public void SetLanguage(bool isEnglish)
        {
            if (_customerDescriptionForAMSControl != null)
            {
                _customerDescriptionForAMSControl.SetLanguage(isEnglish);
            }
        }

        /// <summary>
        /// 国家数据列表
        /// </summary>
        public ComboBoxItemCollection CountryItems
        {
            get
            {
                return _customerDescriptionForAMSControl.CountryItems;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            base.ClosePopup(PopupCloseMode.Immediate);
        }

        /// <summary>
        /// 
        /// </summary>
        public new bool Focused
        {
            get
            {
                bool focused = this.hasFocused(this);
                if (focused == false)
                {
                    focused = this.hasFocused(this.PopupForm);
                }

                return focused;
            }
        }
        #endregion


        #region 事件处理

        void CustomerPopupContainerEdit_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(this.Text.Trim()))
            //{
            //    this.Tag = null;
            //    _customerDescriptionForAMSControl.Clear();
            //    if (string.IsNullOrEmpty(this.Text.Trim()))
            //    {
            //        this.OnKeyDown(new KeyEventArgs(Keys.Delete));
            //    }
            //}
            //else
            //{
            //    if (OnOk != null)
            //    {
            //        OnOk(sender, e);
            //    }
            //}
        }

        void customerDescriptionControl_OnClear(object sender, EventArgs e)
        {
            if (OnClear != null)
            {
                OnClear(this, EventArgs.Empty);
            }
        }

        void customerDescriptionControl_OnOk(object sender, EventArgs e)
        {
            base.ClosePopup(PopupCloseMode.Immediate);

            if (_customerDescriptionForAMSControl != null)
            {
                _customerDescriptionForAMS = _customerDescriptionForAMSControl.CustomerDescriptionForAMS;
            }

            if (OnOk != null)
            {
                OnOk(this, EventArgs.Empty);
            }
        }


        void CustomerDescriptionControl_QueryCloseUp(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        protected override Rectangle ConstrainFormBounds(Rectangle r)
        {
            switch (this.PopupFormPosition)
            {
                case ICP.Framework.ClientComponents.Controls.PopupFormPosition.Left:
                    r.Location = new Point(r.Location.X - this.Properties.PopupControl.Width - this.Width, r.Location.Y - this.Height);
                    break;
                case ICP.Framework.ClientComponents.Controls.PopupFormPosition.Top:
                    r.Location = new Point(r.Location.X, r.Location.Y - this.Height - this.Properties.PopupControl.Height);
                    break;
                case ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right:
                    r.Location = new Point(r.Location.X + this.Width, r.Location.Y - this.Height);
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
            base.OnEnter(e);
            try
            {
                //if (this.Tag ==null || this.Tag == DBNull.Value)
                //    return;
                // Guid? val = (Guid?)this.Tag;
                //if (val.HasValue && val.Value != Guid.Empty)
                if (this._customerDescriptionForAMS != null)
                {
                    if (this.IsPopupOpen == false)
                    {
                        this.ShowPopup();

                        this.Focus();
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if (this.Focused == false && (this.EditorContainsFocus == false))
            {

                base.ClosePopup(PopupCloseMode.Immediate);
                if (_customerDescriptionForAMSControl != null)
                {
                    _customerDescriptionForAMS = _customerDescriptionForAMSControl.CustomerDescriptionForAMS;
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




            //    if (OnOk != null)
            //    {
            //        OnOk(this, e);
            //    }
            //}
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
                if (this.PopupForm != null)
                {
                    this.PopupForm.Focus();
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

                if (_customerDescriptionForAMSControl != null)
                {
                    _customerDescriptionForAMS = _customerDescriptionForAMSControl.CustomerDescriptionForAMS;
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
        public void Refresh()
        {
            if (OnOk != null)
            {
                OnOk(this, null);
            }


        }

        #region 本地方法

        bool hasFocused(Control parent)
        {
            if (parent == null) return false;
            if (parent.Focused) return true;

            foreach (Control c in parent.Controls)
            {
                if (this.hasFocused(c)) return true;
            }

            return false;
        }

        #endregion
    }
}
