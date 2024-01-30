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
    public partial class ChargingGroupEditPart : BaseControl, ICP.Framework.ClientComponents.UIManagement.IDataContentPart
    {
        //public override string Title { get { return LocalData.IsEnglish ? "ChargingGroupEdit" : "编辑目录"; } }
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public IConfigureService configureService
        {
            get
            {

                return ServiceClient.GetService<IConfigureService>();
            }
        }

        public ChargingGroupEditPart()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false)
            {
                this.SetCnText();
            }
            this.Disposed += delegate
            {
                this.dxErrorProvider1.DataSource = null;
                this.DataChanged = null;
                this.bindingSource1.DataSource = null;
                this.bindingSource1.Dispose();
                this.bsParent.DataSource = null;
                this.bsParent.Dispose();

                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            };
        }

        private void SetCnText()
        {
            labCName.Text = "中文名";
            labCode.Text = "代码";
            labParent.Text = "分类";
            labEName.Text = "英文名";
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



        public void BindingData(object data)
        {
            InitControls();

            this.bindingSource1.DataSource = data;
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
