using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.UI.Common;
namespace ICP.Common.UI.Configure.Solution
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class SolutionEditPart : BaseControl, ICP.Framework.ClientComponents.UIManagement.IDataContentPart
    {

        #region 服务注入
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        #endregion

        #region Init

        public SolutionEditPart()
        {
            InitializeComponent();
            this.Enabled = false;
            if (LocalData.IsEnglish == false && !LocalData.IsDesignMode) SetCnText();
            this.Disposed += delegate {
                this.dxErrorProvider1.DataSource = null;
                this.bindingSource1.DataSource = null;
                this.bindingSource1.Dispose();
                this.DataChanged = null;
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
           
        }
        
        private void InitControls()
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<InvoiceDateType>> types = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<InvoiceDateType>(LocalData.IsEnglish);
            foreach (var item in types)
            {
                cmbInvoiceDateType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
        }

        private void SetCnText()
        {
            labCName.Text = "中文名";
            labInvoiceDate.Text = "日期类型";
            labEName.Text = "英文名";
            labRemark.Text = "备注";
            labelControl1.Text = "财务共享";
        }

        #endregion

        public void BindingData(object data)
        {
            if (data == null) { this.bindingSource1.DataSource = typeof(SolutionInfo); this.Enabled = false; }
            else
            {
                this.bindingSource1.DataSource = data;
                if ((data as SolutionInfo).IsValid == false) 
                { 
                    this.Enabled = false; 
                   
                    ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).EndEdit(); 
                }
                else
                {
                    this.Enabled = true; 
                    ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).BeginEdit();
                }
            }
        }

        #region IDataContentPart 成员
        public bool AutoWidth { get; set; }
        public object Current { get { return this.bindingSource1.Current; } }
        public event EventHandler<ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>> DataChanged;
        public object DataSource
        {
            get { return bindingSource1.DataSource; }
            set { BindingData(value); }
        }
        public void EndEdit()
        {
            this.Validate();
            bindingSource1.EndEdit();
        }
        #endregion
    }
}
