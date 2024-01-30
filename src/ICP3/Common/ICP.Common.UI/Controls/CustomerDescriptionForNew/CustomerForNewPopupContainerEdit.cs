#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/6/15 星期五 13:56:24
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion



namespace ICP.Common.UI.Controls
{
    using System;
    using System.Linq;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using Framework.ClientComponents.Controls;
    using ServiceInterface.DataObjects;

    /// <summary>
    /// 客户资料选择控件
    /// </summary>
    public class CustomerForNewPopupContainerEdit : PopupContainerEdit
    {
        #region 构造函数
        private bool isFirstEnter = true;
        private bool isProcessing;

        /// <summary>
        /// 当前数据源
        /// </summary>
        CustomerForNewPopupContainer _PopupContainerControl;
        /// <summary>
        /// 首次获得焦点
        /// </summary>
        public EventHandler OnFirstEnter;
        /// <summary>
        /// 
        /// </summary>
        public CustomerForNewPopupContainerEdit()
        {
            Disposed += delegate
            {
                OnOk = null;
                OnClear = null;
                OnFirstEnter = null;
                _CustomerDescriptionForNew = null;
                if (_PopupContainerControl != null)
                {
                    _PopupContainerControl.OnOk -= customerDescriptionControl_OnOk;
                    _PopupContainerControl.OnClear -= customerDescriptionControl_OnClear;
                    _PopupContainerControl = null;
                }
            };
            if (DesignMode) return;

            PopupContainerControl popupContainerControl1 = new PopupContainerControl {Width = 330, Height = 350};

            if (_PopupContainerControl == null)
            {
                _PopupContainerControl = new CustomerForNewPopupContainer {Dock = DockStyle.Fill};
                _PopupContainerControl.OnOk += customerDescriptionControl_OnOk;
                _PopupContainerControl.OnClear += customerDescriptionControl_OnClear;
                popupContainerControl1.Controls.Add(_PopupContainerControl);
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

            TextChanged += customerPopupContainerEdit_TextChanged;
            
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

        CustomerDescriptionForNew _CustomerDescriptionForNew;
        /// <summary>
        /// 客户资料信息
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(false)]
        public CustomerDescriptionForNew DataSource
        {
            get
            {
                _CustomerDescriptionForNew = _PopupContainerControl.DataSource;

                return _CustomerDescriptionForNew;
            }
            set
            {
                _CustomerDescriptionForNew = value;

                if (_PopupContainerControl != null)
                {
                    _PopupContainerControl.SetDataBinding(value);
                }
            }
        }

        /// <summary>
        /// 设置语言环境
        /// </summary>
        /// <param name="isEnglish">是否英文</param>
        public void SetLanguage(bool isEnglish)
        {
            if (_PopupContainerControl != null)
            {
                _PopupContainerControl.SetLanguage(isEnglish);
            }
        }

        /// <summary>
        /// 国家数据列表
        /// </summary>
        public ImageComboBoxItemCollection CountryItems
        {
            get
            {
                return _PopupContainerControl.CountryItems;
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

        /// <summary>
        /// 
        /// </summary>
        void customerPopupContainerEdit_TextChanged(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        void customerDescriptionControl_OnClear(object sender, EventArgs e)
        {
            if (OnClear != null)
            {
                OnClear(sender, new EventArgs());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        void customerDescriptionControl_OnOk(object sender, EventArgs e)
        {
            base.ClosePopup(PopupCloseMode.Immediate);

            if (_PopupContainerControl != null)
            {
                _CustomerDescriptionForNew = _PopupContainerControl.DataSource;
            }

            if (OnOk != null)
            {
                OnOk(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        void CustomerDescriptionControl_QueryCloseUp(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// 
        /// </summary>
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
            }
            return base.ConstrainFormBounds(r);
        }
        /// <summary>
        /// 
        /// </summary>
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
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if (Focused == false && (EditorContainsFocus == false))
            {

                base.ClosePopup(PopupCloseMode.Immediate);
                if (_PopupContainerControl != null)
                {
                    _CustomerDescriptionForNew = _PopupContainerControl.DataSource;
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
        protected override void ClosePopup(PopupCloseMode closeMode)
        {
            if (closeMode == PopupCloseMode.ButtonClick)
            {
                base.ClosePopup(PopupCloseMode.Immediate);

                if (_PopupContainerControl != null)
                {
                    _CustomerDescriptionForNew = _PopupContainerControl.DataSource;
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

        #region 刷新
        /// <summary>
        /// 
        /// </summary>
        public override void Refresh()
        {
            if (OnOk != null)
            {
                OnOk(this, null);
            }
        } 
        #endregion

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
