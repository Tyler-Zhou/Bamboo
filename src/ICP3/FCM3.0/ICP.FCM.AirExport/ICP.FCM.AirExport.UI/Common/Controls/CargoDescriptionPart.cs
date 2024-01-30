using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.AirExport.UI.Common
{
    [ToolboxItem(false)]
    public partial class CargoDescriptionPart : DevExpress.XtraEditors.XtraUserControl
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
            this.Load += new EventHandler(CargoDescriptionPart_Load);
        }

        void CargoDescriptionPart_Load(object sender, EventArgs e)
        {
            if (this.Parent != null)
                this.BackColor = this.popupContainerControl1.BackColor = this.Parent.BackColor;

            if (DesignMode) return;

            this.Hide();
            this.SendToBack();
            this.popupContainerEdit1.SendToBack();
            this.popupContainerEdit1.CloseUp += new DevExpress.XtraEditors.Controls.CloseUpEventHandler(popupContainerEdit1_CloseUp);

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
                this.Refresh();
                this.popupContainerEdit1.Properties.PopupControl = this.popupContainerControl1;
                this.popupContainerControl1.Show();
                this.popupContainerEdit1.ShowPopup();
                return;
            }
            else if (_parentControl != null)
            {
                _parentControl.Leave -= new EventHandler(_parentControl_Leave);
            }

            
            _cargoDescription = cargoDescription;
            SetSource(_cargoDescription.Cargo);

            _descriptionTextBox = descriptionTextBox;

            this._parentControl = parentControl;

            if(_TopParentControl==null)
                _TopParentControl = GetTopParentControl(_parentControl);

            if (this.Parent != _TopParentControl)
            {
                this.Parent.Controls.Remove(this);
                _TopParentControl.Controls.Add(this);
            }

            SetLocation(_TopParentControl);
            _parentControl = parentControl;
            this.popupContainerEdit1.ShowPopup();

            _parentControl.Focus();
            _parentControl.Leave += new EventHandler(_parentControl_Leave);
            this.SetSource(_cargoDescription.Cargo);
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

            if (control.Parent is GroupBox || control.Parent is Panel || control.Parent is DevExpress.XtraEditors.PanelControl
                || control.Parent is DevExpress.XtraTab.XtraTabPage || control.Parent is DevExpress.XtraTab.XtraTabControl)
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
            if ((this._parentControl.Location.X + this._parentControl.Width + xAdd + this.Width) > topParentControl.Location.X + topParentControl.Width)
                overstepWidth = true;

            if (overstepWidth)
                xAdd -= this._parentControl.Width;

            yAdd -= this._parentControl.Height;

            this.Location = new Point(this._parentControl.Location.X + this._parentControl.Width + xAdd, this._parentControl.Location.Y + yAdd);
        }
        private void GetTopParentControlLoationAdd(Control control, ref int xAdd, ref int yAdd)
        {
            if (control.Parent == null) return;

            if (control.Parent is GroupBox || control.Parent is Panel || control.Parent is DevExpress.XtraEditors.PanelControl
                || control.Parent is DevExpress.XtraTab.XtraTabPage || control.Parent is DevExpress.XtraTab.XtraTabControl)
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
            this.popupContainerEdit1.ClosePopupControl();
        }

        void _parentControl_Leave(object sender, EventArgs e)
        {
            this.popupContainerEdit1.ClosePopupControl();
        }

        void popupContainerEdit1_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            if (_parentControl == null) return;
            if (DataChanged != null)
            {
                this.EndEdit();
                DataChanged(_cargoDescription, null);
            }

            if (_descriptionTextBox != null)
                _descriptionTextBox.Text = _cargoDescription.Cargo.ToString(LocalData.IsEnglish);
        }

        protected virtual void EndEdit() { }

        #endregion

        public virtual void ShowWeightUnit(List<ICP.Common.ServiceInterface.DataObjects.DataDictionaryList> units)
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
