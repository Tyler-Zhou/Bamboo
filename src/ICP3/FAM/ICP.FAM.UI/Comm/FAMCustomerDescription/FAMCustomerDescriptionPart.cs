using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;

namespace ICP.FAM.UI.Comm
{
    public partial class FAMCustomerDescriptionPart : XtraUserControl
    {
       #region 属性

        Control _parentControl = null;
        public Control ParentControl
        {
            get { return _parentControl; }
        }

        /// <summary>
        /// 返回一个CargoDescription
        /// </summary>
        public event EventHandler DataChanged;

        FAMCustomerDescription _FAMCustomerDescription = null;

        #endregion

        #region 初始化


        public FAMCustomerDescriptionPart()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false) SetCNText();
            Disposed += delegate
            {
                popupContainerEdit1.CloseUp -= popupContainerEdit1_CloseUp;
                DataChanged = null;
                _TopParentControl = null;
                _FAMCustomerDescription = null;
                if (_parentControl != null)
                {
                    _parentControl.Enter -= _parentControl_Enter;
                    
                    _parentControl.Leave -= _parentControl_Leave;
                    _parentControl = null;
                    
                }
            
            };
        }

        private void SetCNText()
        {
            labCustomerAddress.Text = "地址";
            labCustomerFax .Text = "传真";
            labCustomerName .Text = "名称";
            labCustomerTel .Text = "电话";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (Parent != null)
                BackColor = popupContainerControl1.BackColor = Parent.BackColor;

            if (DesignMode) return;

            Hide();
            SendToBack();
            popupContainerEdit1.SendToBack();
            popupContainerEdit1.CloseUp += popupContainerEdit1_CloseUp;
        }

        #endregion

        #region 接口
        Control _TopParentControl = null;

        public void SetParentControl(Control parentControl, FAMCustomerDescription customerDescription)
        {
            if (customerDescription == null)
                throw new ApplicationException("FAMCustomerDescriptionPart.SetParentControl 方法中请先初始化对象中FAMCustomerDescription的属性");

            if (_parentControl == parentControl)
            {
                SetCustomerDescription(customerDescription);
                popupContainerEdit1.Properties.PopupControl = popupContainerControl1;
                return;
            }

            SetCustomerDescription(customerDescription);
            _parentControl = parentControl;
            if(_TopParentControl==null)
                _TopParentControl = GetTopParentControl(_parentControl);

            if (Parent != _TopParentControl)
            {
                Parent.Controls.Remove(this);
                _TopParentControl.Controls.Add(this);
            }

            SetLocation(_TopParentControl);
            _parentControl = parentControl;
           
            _parentControl.Enter -= _parentControl_Enter;
            _parentControl.Enter += _parentControl_Enter;
            _parentControl.Leave -= _parentControl_Leave;
            _parentControl.Leave += _parentControl_Leave;
            
        }

        public void SetCustomerDescription(FAMCustomerDescription customerDescription)
        {
            _FAMCustomerDescription = customerDescription;
            bindingSource1.ResetBindings(false);
            bindingSource1.DataSource = _FAMCustomerDescription = customerDescription;
            Refresh();
        }

        void _parentControl_Enter(object sender, EventArgs e)
        {
            popupContainerEdit1.ShowPopup();
            _parentControl.Focus();
        }


        #endregion

        #region 本地方法

        private Control GetTopParentControl(Control control)
        {
            if (control.Parent == null) return control;

            if (control.Parent is GroupBox || control.Parent is Panel || control.Parent is PanelControl
                || control.Parent is XtraTabPage || control.Parent is XtraTabControl)
            {
                return GetTopParentControl(control.Parent);
            }
            else return control.Parent;
        }
        private void SetLocation(Control topParentControl)
        {
            int xAdd = 0, yAdd = 0;
            GetTopParentControlLoationAdd(_parentControl, ref xAdd, ref yAdd);
            xAdd += topParentControl.Location.X;
            yAdd += topParentControl.Location.Y;

            bool overstepWidth = false;
            if ((_parentControl.Location.X + _parentControl.Width + xAdd + Width) > topParentControl.Location.X + topParentControl.Width)
                overstepWidth = true;

            if (overstepWidth)
                xAdd -= _parentControl.Width;

            yAdd -= _parentControl.Height;

            Location = new Point(_parentControl.Location.X + _parentControl.Width + xAdd, _parentControl.Location.Y + yAdd);
        }
        private void GetTopParentControlLoationAdd(Control control, ref int xAdd, ref int yAdd)
        {
            if (control.Parent == null) return;

            if (control.Parent is GroupBox || control.Parent is Panel || control.Parent is PanelControl
                || control.Parent is XtraTabPage || control.Parent is XtraTabControl)
            {
                xAdd += control.Parent.Location.X;
                yAdd += control.Parent.Location.Y;
                GetTopParentControlLoationAdd(control.Parent, ref  xAdd, ref yAdd);
            }
        }

        #endregion

        #region 事件

        private void btnOK_Click(object sender, EventArgs e)
        {
            popupContainerEdit1.ClosePopupControl();
        }

        void _parentControl_Leave(object sender, EventArgs e)
        {
            popupContainerEdit1.ClosePopupControl();
        }

        void popupContainerEdit1_CloseUp(object sender, CloseUpEventArgs e)
        {
            if (_parentControl == null) return;
            if (DataChanged != null)
            {
                EndEdit();
                DataChanged(_FAMCustomerDescription, null);
            }
        }

        protected virtual void EndEdit() { }

        #endregion
    }

    public class UnClosePopupContainerEdit : PopupContainerEdit
    {
        /// <summary>
        /// 已禁止DEV自带的关闭,请使用ClosePopupControl()方法
        /// </summary>
        protected override void DoClosePopup(PopupCloseMode closeMode)
        {
            //DoNothing
        }
        /// <summary>
        /// 已禁止DEV自带的关闭,请使用ClosePopupControl()方法
        /// </summary>
        public override void ClosePopup()
        {
            //base.DoClosePopup(PopupCloseMode.Normal);
        }

        /// <summary>
        /// 关闭弹出框
        /// </summary>
        public void ClosePopupControl()
        {
            base.DoClosePopup(PopupCloseMode.Normal);
        }
    }
}
