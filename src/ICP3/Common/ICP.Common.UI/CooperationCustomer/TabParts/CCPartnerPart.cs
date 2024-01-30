using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.Common.UI.CC
{
    [ToolboxItem(false)]
    public partial class CCPartnerPart : ICP.Framework.ClientComponents.UIFramework.BasePart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public CCPartnerPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            List<CooperationCustomerPartnerList> list = new List<CooperationCustomerPartnerList>();
            list.Add(UIModelHelper.GetNormalObject<CooperationCustomerPartnerList>());
            list[0].PartnerType = PartnerType.Consignee;

            list.Add(UIModelHelper.GetNormalObject<CooperationCustomerPartnerList>());
            list[1].PartnerType = PartnerType.Shipper;

            list.Add(UIModelHelper.GetNormalObject<CooperationCustomerPartnerList>());
            list[2].PartnerType = PartnerType.NotifyParty;

            list.Add(UIModelHelper.GetNormalObject<CooperationCustomerPartnerList>());
            list[3].PartnerType = PartnerType.Shipper;

            bsList.DataSource = list;
            bsList.ResetBindings(false);
        }
    }
}
