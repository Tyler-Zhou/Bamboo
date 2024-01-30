using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.UI.Common;

namespace ICP.Common.UI.Configure.CommpanyConfigure
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class ConfigureKeyValueEditPart : BaseControl, ICP.Framework.ClientComponents.UIManagement.IDataContentPart
    {
        //public override string Title { get { return LocalData.IsEnglish ? "Configure Edit" : "编辑公司配置"; } }

        #region 服务注入

        public WorkItem WorkItem
        {
            get;
            set;
        }
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        #endregion

        public ConfigureKeyValueEditPart()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate {
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
            labKey.Text = "键";
            labValue.Text = "值";
            labCreateBy.Text = "创建人";
            labCreateDate.Text = "创建日期";
        }

    

        private void InitControls()
        {
            List<ConfigureKeyList> configureKeyLists = ConfigureService.GetConfigureKeyList();
            foreach (var item in configureKeyLists)
            {
                cmbConfigureKey.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem
                                                    (item.Name, item.ID));
            }
            ConfigureKeyValueInfo currentData = bindingSource1.DataSource as ConfigureKeyValueInfo;
            if (currentData.ConfigureKeyID == Guid.Empty)
            {
                cmbConfigureKey.SelectedIndex = 0;
            }
        }

        public void BindingData(object data)
        {
            this.bindingSource1.DataSource = data;

            InitControls();
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
