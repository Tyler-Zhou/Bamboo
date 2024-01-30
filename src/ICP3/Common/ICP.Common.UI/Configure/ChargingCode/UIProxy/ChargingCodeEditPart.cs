using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.UI.Common;
namespace ICP.Common.UI.Configure.ChargingCode
{
    [ToolboxItem(false)]
    public partial class ChargingCodeEditPart : BaseControl, ICP.Framework.ClientComponents.UIManagement.IDataContentPart
    {
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public IConfigureService configureService
        {
            get {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        public ChargingCodeEditPart()
        {
            InitializeComponent();
            this.Enabled = false;
            if (LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate {
                this.dxErrorProvider1.DataSource = null;
                    this.bindingSource1.DataSource = null;
                    this.bindingSource1.Dispose();
                    this.DataChanged = null;
                    this.bsParent.DataSource = null;
                    this.bsParent.Dispose();
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        private void InitControls()
        {
            List<ChargingGroupList> groupList = configureService.GetChargingGroupList(
                string.Empty,
                string.Empty,
                null,
                0);

            groupList = groupList.Where(c => c.ParentID != null).ToList();
            cmbCategary.AllowMultSelect = false;
            cmbCategary.RootValue = Guid.Empty;
            cmbCategary.ParentMember = "ParentID";
            cmbCategary.ValueMember = "ID";
            cmbCategary.DisplayMember = LocalData.IsEnglish ? "EName" : "CName";
            cmbCategary.DataSource = groupList;
        }

        private void SetCnText()
        {
            chkIsCommission.Properties.Caption = "佣金";
            labCName.Text = "中文名";
            labCode.Text = "代码";
            labParent.Text = "分类";
            labEName.Text = "英文名";
        }

        public void BindingData(object data)
        {
            if (data == null) { this.bindingSource1.DataSource = typeof(ChargingCodeInfo); this.Enabled = false; }
            
            else
            {
                this.InitControls();

                this.bindingSource1.DataSource = data;
                if ((data as ChargingCodeInfo).IsValid == false) { this.Enabled = false; ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).EndEdit(); }
                else
                {
                    this.Enabled = true; ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).BeginEdit();
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
