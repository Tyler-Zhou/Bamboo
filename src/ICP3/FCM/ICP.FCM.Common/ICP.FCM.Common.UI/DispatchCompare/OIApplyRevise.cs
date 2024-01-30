using System;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;

namespace ICP.FCM.Common.UI.DispatchCompare
{
    /// <summary>
    /// 修改代理账单后向海出发起修订页面
    /// 2013-07-31 joe initial
    /// </summary>
    [SmartPart]
    public partial class OIApplyRevise : DevExpress.XtraEditors.XtraUserControl
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }

        IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }



        public IClientOceanImportService ClientOceanImportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanImportService>();
            }

        }
        #endregion

        #region  Public Member

        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID
        {
            get;
            set;
        }

        /// <summary>
        /// 代理信息
        /// </summary>
        public string AgentName
        {
            set { txtAgentName.Text = value; }
        }
        /// <summary>
        /// 代理信息是否显示默认显示
        /// </summary>
        public bool IsAgentVisible
        {
            set
            {
                if (value==false)
                {
                    this.groupBox2.Visible = false;
                    this.groupBox2.Height = 0;
                }
            }
        }
        #endregion

        #region   private member

        /// <summary>
        /// 账单类型
        /// </summary>
        private BillType billType = BillType.DC;

        SimpleBusinnessInfo simpleBusInfo =null;
        #endregion

        public OIApplyRevise()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.simpleBusInfo = null;
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            
            };
        }



        private void btnCheckRevise_Click(object sender, EventArgs e)
        {
            if (simpleBusInfo != null)
            {
                FCMUIUtility.ShowAcceptedDocumentCompareNew(Workitem, simpleBusInfo.OEBusinessID, OperationID, true);
            }
            else
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Not Ecean Export Business!" : "没有对应的海出业务!");
                return;
            }
        
                     
        }

        private void BtnApplyRevise_Click(object sender, EventArgs e)
        {
            string strRemark=edtRemark.Text.Trim();
            if (strRemark.Length<1)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Remark is not empty!" : "备注不能为空!");
                return;
            }
            int result = FCMCommonService.ApplyRevise(OperationID, LocalData.UserInfo.LoginID, strRemark);
            if (result ==-1)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Apply Revise fail!" : "申请修订失败!");
                return;
            }
            else if (result == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? " Bill is empty!" : "账单为空");
                return;
            }
            else 
            {
              if(simpleBusInfo!=null)   ClientOceanImportService.MailApplyBillRevise(LocalData.IsEnglish, simpleBusInfo.OEBusinessID);
                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? " Apply Revise Successfully!" : "申请修订成功!");
             
            }
            this.FindForm().Close();
        }

        private void OIApplyRevise_Load(object sender, EventArgs e)
        {
            simpleBusInfo = FCMCommonService.GetOEIDByOIID(OperationID)[0];
        }




    }
}
