using System;
using System.Collections.Generic;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Common.UI.ShippingLineManager
{
    public partial class ShippingLineListPart : BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        #endregion

        public override object Current
        {
            get { return bsList.Current; }
        }

        protected ShippingLineList CurrentRow
        {
            get { return Current as ShippingLineList; }
        }


        public ShippingLineListPart()
        {
            InitializeComponent();
        }

        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;
        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                bsList.DataSource = value;
                bsList.ResetBindings(false);
                treeMain.ExpandAll();
                //if (CurrentChanged != null) CurrentChanged(this, Current);
            }
        }

        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null)
            {
                CurrentChanged(this, Current);
            }
        }

        public void EditPartSaved(object[] prams)
        {
            ShippingLineList data = prams[0] as ShippingLineList;
            List<ShippingLineList> source = this.DataSource as List<ShippingLineList>;
            if (source == null || source.Count == 0)
            {
                bsList.Add(data);
                bsList.ResetBindings(false);
            }
            else
            {
                ShippingLineList tager = source.Find(delegate(ShippingLineList item) { return item.ID == data.ID; });
                if (tager == null)
                {
                    bsList.Insert(0, data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    bsList.ResetItem(bsList.IndexOf(tager));
                }

            }
            if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
        }


        #region Workitem Common

        #region  作废 激活
        [CommandHandler(ShippingLineCommandConstants.Command_Invalid)]
        public void Command_Invalid(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }
            SingleResultData result = TransportFoundationService.ChangeShippingLineState(CurrentRow.ID, !CurrentRow.IsValid, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
            CurrentRow.IsValid = !CurrentRow.IsValid;
            CurrentRow.UpdateDate = result.UpdateDate;
            bsList.ResetCurrentItem();
            if (CurrentChanged != null)
            {
                CurrentChanged(this, CurrentRow);
            }
        }
        #endregion

        #endregion


    }
}
