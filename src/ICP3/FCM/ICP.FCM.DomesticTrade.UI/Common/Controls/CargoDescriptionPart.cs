using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.DomesticTrade.UI.Common
{
    [ToolboxItem(false)]
    public partial class CargoDescriptionPart : XtraUserControl
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

        CargoDescription _cargoDescription=null;
        Control _descriptionTextBox = null;

        #endregion

        #region 初始化


        public CargoDescriptionPart()
        {
            InitializeComponent();
            Load += new EventHandler(CargoDescriptionPart_Load);
            Disposed += delegate
            {
                if (_parentControl != null)
                {
                    _parentControl.Leave -= _parentControl_Leave;

                }
                _TopParentControl = null;
                _weightUnitsList = null;
                DataChanged = null;
                popupContainerEdit1.CloseUp -= popupContainerEdit1_CloseUp;
                popupContainerEdit1 = null;
                popupContainerControl1 = null;
            
            };
        }

        void CargoDescriptionPart_Load(object sender, EventArgs e)
        {
            if (Parent != null)
                BackColor = popupContainerControl1.BackColor = Parent.BackColor;

            if (DesignMode) return;

            Hide();
            SendToBack();
            popupContainerEdit1.SendToBack();
            popupContainerEdit1.CloseUp += new CloseUpEventHandler(popupContainerEdit1_CloseUp);

        }

        #endregion

        #region 接口
        Control _TopParentControl = null;

        public virtual void SetParentControl(Control parentControl, CargoDescription cargoDescription, Control descriptionTextBox)
        {
            if (cargoDescription == null)
                throw new ApplicationException("CargoDescriptionPart.SetParentControl 方法中请先初始化对象中cargoDescription的属性");

            if (_parentControl == parentControl)
            {
                _cargoDescription = cargoDescription;
                _descriptionTextBox = descriptionTextBox;
                Refresh();
                popupContainerEdit1.Properties.PopupControl = popupContainerControl1;
                popupContainerControl1.Show();
                popupContainerEdit1.ShowPopup();
                return;
            }
            else if (_parentControl != null)
            {
                _parentControl.Leave -= new EventHandler(_parentControl_Leave);
            }

            
            _cargoDescription = cargoDescription;
            SetSource(_cargoDescription.Cargo);

            _descriptionTextBox = descriptionTextBox;

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
            popupContainerEdit1.ShowPopup();

            _parentControl.Focus();
            _parentControl.Leave += new EventHandler(_parentControl_Leave);
            SetSource(_cargoDescription.Cargo);
            _parentControl.Focus();
        }

        public virtual void SetParentControl(Control parentControl, CargoDescription cargoDescription)
        {
            SetParentControl(parentControl, cargoDescription, null);
        }

        protected List<DataDictionaryList> _weightUnitsList = null;
        public  void SetWeightUnits(List<DataDictionaryList> weightUnitsList) 
        { 
            _weightUnitsList = weightUnitsList;
        }

        protected virtual void SetSource(CommonCargo commonCargo)
        {

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
                DataChanged(_cargoDescription, null);
            }

            if (_descriptionTextBox != null)
                _descriptionTextBox.Text = _cargoDescription.Cargo.ToString(LocalData.IsEnglish);
        }

        protected virtual void EndEdit() { }

        #endregion

        public virtual void ShowWeightUnit(List<DataDictionaryList> units)
        {
            //throw new NotImplementedException();
        }
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
