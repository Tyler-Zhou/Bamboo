using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.UI.Common;
namespace ICP.Common.UI.TransportFoundation.TransportClause
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class TransportClauseEditPart : BaseControl, ICP.Framework.ClientComponents.UIManagement.IDataContentPart
    {
        #region 服务注入
        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        #endregion

        #region init

        public TransportClauseEditPart()
        {
            InitializeComponent();this.Enabled = false;

            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate {
                this._dictionaryList = null;
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

        private void SetCnText()
        {
            labDescription.Text = "描述";
            labDestination.Text = "目的地";
            labOriginal.Text = "起始地";

        }

        protected override void OnLoad(EventArgs e)
        {
            InitControls();
            base.OnLoad(e);

            TransportClauseInfo info = this.bindingSource1.DataSource as TransportClauseInfo;
            if (info != null)
            {
                info.CancelEdit(); info.BeginEdit();
            }
        }

        List<DataDictionaryList> _dictionaryList = null;

        void InitControls()
        {
            _dictionaryList = TransportFoundationService.GetDataDictionaryList(string.Empty, string.Empty, DataDictionaryType.TransportClause, true, 0);

            cmbDestinationCode.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem
                    (string.Empty, Guid.Empty));
            cmbOriginalCode.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem
                   (string.Empty, Guid.Empty));

            foreach (DataDictionaryList item in _dictionaryList)
            {
                cmbDestinationCode.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem
                    (item.Code, item.ID));
                cmbOriginalCode.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem
                    (item.Code, item.ID));
            }
        }
        #endregion

        public void BindingData(object data)
        {
            if (data == null) { this.bindingSource1.DataSource = typeof(TransportClauseInfo); this.Enabled = false; }
            else
            {
                this.bindingSource1.DataSource = data;
                if ((data as TransportClauseInfo).ID == Guid.Empty
                    && _dictionaryList.Count>0)
                {
                    (data as TransportClauseInfo).DestinationCodeID = Guid.Empty;
                    (data as TransportClauseInfo).DestinationCode = string.Empty;
                    (data as TransportClauseInfo).OriginalCodeID = Guid.Empty;
                    (data as TransportClauseInfo).OriginalCode = string.Empty;
                }

                ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).EndEdit();
                if ((data as TransportClauseInfo).IsValid == false) { this.Enabled = false; ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).EndEdit(); }
                else
                {
                    this.Enabled = true;
                    ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).BeginEdit();
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
            if (this.Validate() == false) return;

            bindingSource1.EndEdit();
        }
        #endregion
    }
}
