using System;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Common.UI.ShippingLineManager
{
    public partial class SippingLineEditPart : BaseEditPart
    {
        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        private ShippingLineList CurrentShippingLine 
        {
            get 
            {
                return bsSippingLine.DataSource as ShippingLineList;
            }
        
        }


        public SippingLineEditPart()
        {
            InitializeComponent();
        }

        public override object DataSource
        {
            get { return bsSippingLine.DataSource; }
            set { BindingData(value); }
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        private void BindingData(object data)
        {
            if (data == null) { this.bsSippingLine.DataSource = typeof(ShippingLineList); this.Enabled = false; }
            else
            {
                this.bsSippingLine.DataSource = data;
                if ((data as ShippingLineList).IsValid == false) { this.Enabled = false; ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).EndEdit(); }
                else
                {
                    this.Enabled = true; ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).BeginEdit();
                }
            }
        }

        [CommandHandler(ShippingLineCommandConstants.Command_AddData)]
        public void Command_AddData(object sender, EventArgs e) 
        {
            ShippingLineList newData = bsSippingLine.DataSource as ShippingLineList;
            if (newData.ParentID==null||newData.ParentID==Guid.Empty)
            {
                return;
            }

            ShippingLineList snew = new ShippingLineList();
            snew.ParentID = newData.ParentID;
            //snew.CreateByID = LocalData.UserInfo.LoginID;
            snew.ParentID = newData.ParentID;
            snew.CreateByName = LocalData.UserInfo.LoginName;
            snew.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            snew.IsDirty = false;
            snew.IsValid = true;
            BindingData(snew);          
        }

        /// <summary>
        /// 保存航线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bsSippingLine.EndEdit();
           SingleResultData result = TransportFoundationService.SaveShippingLineInfo(CurrentShippingLine.ID,
                                                               (Guid)CurrentShippingLine.ParentID,
                                                               CurrentShippingLine.Code,
                                                               CurrentShippingLine.CName,
                                                               CurrentShippingLine.EName,
                                                               LocalData.UserInfo.LoginID,
                                                               CurrentShippingLine.UpdateDate);

           CurrentShippingLine.CancelEdit();
           CurrentShippingLine.ID = result.ID;
           CurrentShippingLine.UpdateDate = result.UpdateDate;
           CurrentShippingLine.BeginEdit();

            if (Saved !=null)
            {
                this.Saved(this.CurrentShippingLine);
            }

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
        }
    }
}
