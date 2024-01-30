using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.UI.Common;
namespace ICP.Common.UI.TransportFoundation.Commodity
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class CommodityEditPart : BaseControl, ICP.Framework.ClientComponents.UIManagement.IDataContentPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        #endregion

        #region init

        public CommodityEditPart()
        {
            InitializeComponent();this.Enabled = false;

            if (LocalData.IsEnglish == false)
            {
                SetCnText();
            }
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
   

        private void InitControls()
        {

            List<CommodityList> commodityList = TransportFoundationService.GetCommodityList(string.Empty, true, 0);
            commodityList = commodityList.Where(c => c.ParentID != null).ToList();
            cmbCategary.AllowMultSelect = false;
            cmbCategary.RootValue = Guid.Empty;
            cmbCategary.ParentMember = "ParentID";
            cmbCategary.ValueMember = "ID";
            cmbCategary.DisplayMember = LocalData.IsEnglish ? "EName" : "CName";
            cmbCategary.DataSource = commodityList;
        }

        private void SetCnText()
        {
            labCName.Text = "中文名";
            labEName.Text = "英文名";
            labParent.Text = "分类";
            labRemark.Text = "备注";

        }
        #endregion

       
        public void BindingData(object data)
        {
            CommodityInfo info = (CommodityInfo)data;

            if (info == null) { this.bindingSource1.DataSource = typeof(CommodityInfo); this.Enabled = false; }
            else
            {
                if (info.Equals(this.Current) == false)
                {
                    this.InitControls();
                }
                info.ParentName = (string.IsNullOrEmpty(info.ParentName) 
                    || info.ParentName.Equals("品名") 
                    || info.ParentName.Equals("Commodity")) ? "" : info.ParentName;

                this.bindingSource1.DataSource = info;
                if (info.IsValid == false) { this.Enabled = false; info.EndEdit(); }
                else
                {
                    this.Enabled = true; info.BeginEdit();
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
            //CommodityInfo info = (CommodityInfo)this.Current;
            //if (info != null)
            //{
            //    info.ParentName = cmbCategary.Text.Trim();
            //}
            this.Validate();
            bindingSource1.EndEdit();
        }
        #endregion
    }
}
