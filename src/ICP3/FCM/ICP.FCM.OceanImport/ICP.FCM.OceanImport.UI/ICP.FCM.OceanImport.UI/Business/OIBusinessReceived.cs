using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.OceanImport.UI
{
    public partial class OIBusinessReceived : DevExpress.XtraEditors.XtraUserControl
    {
        public OIBusinessReceived()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
                this.Disposed += delegate
               {
                   UCBusinessBLList.Dispose();
                   if (this.WorkItem != null)
                   {
                       this.WorkItem.Items.Remove(this);
                       this.WorkItem = null;
                   }
               };
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        #region 服务
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public IOceanImportService OceanImportService 
        {
            get 
            {
                return ServiceClient.GetService<IOceanImportService>();
            }
        }

        #endregion

        #region 属性
        public bool OBLRcved
        {
            get;
            set;
        }
        public Guid BusinessID
        {
            get;
            set;
        }
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
            }
        }

        private void InitControls()
        {
            this.UCBusinessBLList.SetTools(false);
            this.UCBusinessBLList.BusinessID = BusinessID;

            
            List<OceanBusinessHBLList>  list=OceanImportService.GetOIBookingHBLList(BusinessID);

             this.UCBusinessBLList.BindHBLList(list);
        
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.UCBusinessBLList.IsChanged)
            {
                try
                {
                    List<HBLInfoSaveRequest> list = this.UCBusinessBLList.GetHBLSaveInfo();
                    if (list.Count > 0)
                    {
                        OceanImportService.SaveOIBookingHBLInfo(list[0]);
                    }

                    OBLRcved=this.UCBusinessBLList.GetReceiverState();

                    this.FindForm().DialogResult = DialogResult.OK;
                    this.FindForm().Close();
                }
                catch (Exception ex)
                {
                    string message = (LocalData.IsEnglish ? "Received Faily" : "设置失败.") + ex.Message;
                    DevExpress.XtraEditors.XtraMessageBox.Show(message);
                }

            }
        }


    }
}
