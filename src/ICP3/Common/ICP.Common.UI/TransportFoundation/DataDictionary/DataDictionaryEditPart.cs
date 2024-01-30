using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.UI.Common;
namespace ICP.Common.UI.TransportFoundation.DataDictionary
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class DataDictionaryEditPart : BaseControl, ICP.Framework.ClientComponents.UIManagement.IDataContentPart
    {
        //public override string Title { get { return LocalData.IsEnglish ? "DataDictionaryEdit" : "编辑数据字典"; } }

        #region 服务注入
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        #endregion
        #region

        public DataDictionaryEditPart()
        {
            InitializeComponent();this.Enabled = false;
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate
            {
                this.DataChanged = null;
                this.dxErrorProvider1.DataSource = null;
                this.bindingSource1.DataSource = null;
                this.bindingSource1.Dispose();
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        private void SetCnText()
        {
            //chkIsVoid.Properties.Caption = "是否有效";
            labCName.Text = "中文名";
            labCode.Text = "代码";
            labDescription.Text = "描述";
            labEName.Text = "英文名";
            labType.Text = "类型";

        }

        protected override void OnLoad(EventArgs e)
        {
            InitControls();
            base.OnLoad(e);
        }

        private void InitControls()
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<DataDictionaryType>> customerTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<DataDictionaryType>(LocalData.IsEnglish);
           // cmbType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", null));

            foreach (var item in customerTypes)
            {
                cmbType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            //cmbType.EditValue = null;
        }

        public void BindingData(object data)
        {
            if (data == null) { this.bindingSource1.DataSource = typeof(DataDictionaryInfo); this.Enabled = false; }
            else
            {
                this.bindingSource1.DataSource = data;

                if ((data as DataDictionaryInfo).IsValid == false) { this.Enabled = false; ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).EndEdit(); }
                else
                {
                    this.EndEdit();
                    this.Enabled = true; 
                    ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).BeginEdit();
                }
            }
        }

        void RefreshUIState(DataDictionaryInfo info)
        {
            if (info == null) return;
            if (info.IsValid)
            {
                txtCName.Properties.ReadOnly = false;
                txtCode.Properties.ReadOnly = false;
                txtEName.Properties.ReadOnly = false;
                txtDescription.Properties.ReadOnly = false;
                cmbType.Properties.ReadOnly = false;
            }
            else
            {
                txtCName.Properties.ReadOnly = true;
                txtCode.Properties.ReadOnly = true;
                txtEName.Properties.ReadOnly = true;
                txtDescription.Properties.ReadOnly = true;
                cmbType.Properties.ReadOnly = true;
            }
        }
        #endregion

        #region IDataContentPart 成员
        public object Current { get { return this.bindingSource1.Current; } }
        public event EventHandler<ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>> DataChanged;
        public object DataSource
        {
            get { return bindingSource1.DataSource; }
            set { BindingData(value); }
        }

        public void EndEdit()
        {
            DataDictionaryInfo ddi = (DataDictionaryInfo)this.Current;
            if (ddi != null)
            {
                if (ddi.Type == DataDictionaryType.None)
                {
                    ddi.SetError("Type", LocalData.IsEnglish?"Type must input":"类型必须填写");
                }
                else
                {
                    ddi.SetError("Type", "");
                }
            }

            this.Validate();
            bindingSource1.EndEdit();          
        }
        #endregion
    }
}
