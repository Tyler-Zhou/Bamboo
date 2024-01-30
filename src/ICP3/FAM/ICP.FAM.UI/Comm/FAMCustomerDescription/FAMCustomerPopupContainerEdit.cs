using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;

namespace ICP.FAM.UI.Comm
{
    /// <summary>
    /// FAM客户资料选择控件
    /// </summary>
    public class FAMCustomerPopupContainerEdit : PopupContainerEdit
    {

        #region 构造函数

        FAMCustomerDescriptionControl  _famcustomerDescriptionControl = null;
        private bool isProcessing = false;
        public EventHandler OnFirstEnter;
        /// <summary>
        /// 
        /// </summary>
        public FAMCustomerPopupContainerEdit()
            : base()
        {
            Disposed += delegate
            {
                OnOk = null;
                OnClear = null;
                OnFirstEnter = null;
                _customerDescription = null;
                if (_famcustomerDescriptionControl != null)
                {
                    _famcustomerDescriptionControl.OnOk -= customerDescriptionControl_OnOk;
                    _famcustomerDescriptionControl.OnClear -= customerDescriptionControl_OnClear;
                    _famcustomerDescriptionControl = null;
                }
            };
            if (DesignMode) return;

            PopupContainerControl popupContainerControl1 = new PopupContainerControl {Width = 300, Height = 270};

            if (_famcustomerDescriptionControl == null)
            {
                _famcustomerDescriptionControl = new FAMCustomerDescriptionControl {Dock = DockStyle.Fill};
                _famcustomerDescriptionControl.OnOk += customerDescriptionControl_OnOk;
                _famcustomerDescriptionControl.OnClear += customerDescriptionControl_OnClear;
                popupContainerControl1.Controls.Add(_famcustomerDescriptionControl);
            }


            Properties.Buttons.AddRange(new[] {
            new EditorButton(),
            new EditorButton(ButtonPredefines.SpinRight)});

            Properties.TextEditStyle = TextEditStyles.Standard;
            Properties.PopupControl = popupContainerControl1;
            Properties.ShowPopupCloseButton = false;
            Properties.PopupSizeable = false;
            PopupFormPosition = PopupFormPosition.Right;
            Properties.ActionButtonIndex = 1;
            Properties.PopupBorderStyle = PopupBorderStyles.Simple;
            
            QueryCloseUp -= CustomerDescriptionControl_QueryCloseUp;
            QueryCloseUp += CustomerDescriptionControl_QueryCloseUp;


        }

        #endregion


        #region 公共属性，方法，事件
        /// <summary>
        /// 弹出窗口位置
        /// </summary>
        public PopupFormPosition PopupFormPosition { get; set; }

        /// <summary>
        /// 清空事件
        /// </summary>
        public event EventHandler OnClear;

        /// <summary>
        /// 选择事件
        /// </summary>
        public event EventHandler OnOk;

        FAMCustomerDescription _customerDescription;
        /// <summary>
        /// FAM客户资料信息
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(false)]
        public FAMCustomerDescription CustomerDescription
        {
            get
            {
                _customerDescription = _famcustomerDescriptionControl.CustomerDescription;

                return _customerDescription;
            }
            set
            {
                _customerDescription = value;

                if (_famcustomerDescriptionControl != null)
                {
                    _famcustomerDescriptionControl.SetDataBinding(value);
                }
            }
        }

        /// <summary>
        /// 设置语言环境
        /// </summary>
        /// <param name="isEnglish">是否英文</param>
        public void SetLanguage(bool isEnglish)
        {
            if (_famcustomerDescriptionControl != null)
            {
                _famcustomerDescriptionControl.SetLanguage(isEnglish);
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
                bool focused = hasFocused(this);
                if (focused == false)
                {
                    focused = hasFocused(PopupForm);
                }

                return focused;
            }
        }
        #endregion


        #region 事件处理

        void customerDescriptionControl_OnClear(object sender, EventArgs e)
        {
            if (OnClear != null)
            {
                OnClear(sender, new EventArgs());
            }
        }

        void customerDescriptionControl_OnOk(object sender, EventArgs e)
        {
            base.ClosePopup(PopupCloseMode.Immediate);

            if (_famcustomerDescriptionControl != null)
            {
                _customerDescription = _famcustomerDescriptionControl.CustomerDescription;
            }

            if (OnOk != null)
            {
                OnOk(sender, e);
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
        private bool isFirstEnter = true;
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
            try 
            {
                if (Tag ==null || Tag == DBNull.Value)
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
            if (Focused == false && (EditorContainsFocus == false))
            {

                base.ClosePopup(PopupCloseMode.Immediate);
                if (_famcustomerDescriptionControl != null)
                {
                    _customerDescription = _famcustomerDescriptionControl.CustomerDescription;
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
        /// 
        /// </summary>
        /// <param name="closeMode"></param>
        protected override void ClosePopup(PopupCloseMode closeMode)
        {
            if (closeMode == PopupCloseMode.ButtonClick)
            {
                base.ClosePopup(PopupCloseMode.Immediate);

                if (_famcustomerDescriptionControl != null)
                {
                    _customerDescription = _famcustomerDescriptionControl.CustomerDescription;
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
        /// <summary>
        /// 刷新
        /// </summary>
        public override void Refresh()
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

            return parent.Controls.Cast<Control>().Any(hasFocused);
        }

        #endregion
    }
}
