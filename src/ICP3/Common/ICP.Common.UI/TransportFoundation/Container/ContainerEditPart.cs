using System;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.UI.Common;
namespace ICP.Common.UI.TransportFoundation.Container
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class ContainerEditPart : BaseControl, ICP.Framework.ClientComponents.UIManagement.IDataContentPart
    {
        //public override string Title { get { return LocalData.IsEnglish ? "ContainerEdit" : "编辑箱"; } }

        #region 服务注入
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        #endregion
        #region
        public ContainerEditPart()
        {
            InitializeComponent();this.Enabled = false;

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
            labCode.Text = "代码";
            labCreateBy.Text = "创建人";
            labCreateDate.Text = "创建日期";
            labDescription.Text = "描述";
            labIsoCode.Text = "国际代码";
            labTeu.Text = "箱量";
        }
        #endregion

        public void BindingData(object data)
        {
            if (data == null) { this.bindingSource1.DataSource = typeof(ContainerInfo); this.Enabled = false; }
            else
            {
                this.bindingSource1.DataSource = data;
                if ((data as ContainerInfo).IsValid == false) { this.Enabled = false; ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).EndEdit(); }
                else
                {
                    this.Enabled = true; ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).BeginEdit();
                }
            }
        }

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
            this.Validate();
            bindingSource1.EndEdit();
        }
        #endregion
    }
}
