using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.FCM.AirImport.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.AirImport.UI
{
    public partial class OIBusinessReceived : DevExpress.XtraEditors.XtraUserControl
    {
        public OIBusinessReceived()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        #region 服务
   

        [ServiceDependency]
        public IAirImportService oiService { get; set; }

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

            
            List<AirBusinessHBLList>  list=oiService.GetAIBookingHBLList(BusinessID);

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
                        oiService.SaveAIBookingHBLInfo(list[0]);
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
