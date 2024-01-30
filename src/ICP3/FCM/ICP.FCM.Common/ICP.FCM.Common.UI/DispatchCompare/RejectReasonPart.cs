using DevExpress.XtraEditors;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;

namespace ICP.FCM.Common.UI.DispatchCompare
{
    /// <summary>
    /// 修改代理账单后向海出发起修订页面
    /// 2013-07-31 joe initial
    /// </summary>
    [SmartPart]
    public partial class RejectReasonPart : BasePart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }

        public IOperationAgentService OperationAgentService
        {
            get
            { return ServiceClient.GetService<IOperationAgentService>(); }
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
        /// 业务ID
        /// </summary>
        public OperationType OperationType
        {
            get;
            set;
        }

        #endregion

        #region   private member



        #endregion

        /// <summary>
        /// 修改代理账单后向海出发起修订页面
        /// </summary>
        public RejectReasonPart()
        {
            InitializeComponent();
            Disposed += delegate {

                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            
            };
            SetLanguage();
    
        }
        private void SetLanguage()
        {
            if (!LocalData.IsEnglish)
            {
                lbl0.Text = "我们不能接受没有协商好的费用更改，您应该首先跟代理协商好后再修改费用。";
                lbl1.Text = "我们不能接受这些费用更改，因为有费用重复。";
                lbl2.Text = "自己填写：";
                groupBox2.Text = "选择原因";
                btnReject.Text = " 拒  签 ";
                BtnCancel.Text = " 取  消 ";
                gboxTo.Text = "代理信息";
            }
        }
        private void init()
        {
           BusinessAgentAndCustomInfoObject businessAgentAndCustomInfoObject = OperationAgentService.GetOperationAgentNameAndCustomer(OperationID, OperationType);
          
            if (businessAgentAndCustomInfoObject==null)
            {
                return;
            }

           lblAgentValue.Text = businessAgentAndCustomInfoObject.AgentName;

           string AttnValue = businessAgentAndCustomInfoObject.CustomerName;
           if (AttnValue!=null && AttnValue!="")
            {
                if (businessAgentAndCustomInfoObject.FilerName != null && businessAgentAndCustomInfoObject.FilerName != "" && businessAgentAndCustomInfoObject.FilerName != businessAgentAndCustomInfoObject.CustomerName)
                {
                    AttnValue += "," + businessAgentAndCustomInfoObject.FilerName;
                }
                else
                {
                    AttnValue = businessAgentAndCustomInfoObject.FilerName;
                }
            }
           else
           {
               AttnValue = businessAgentAndCustomInfoObject.FilerName;
           }

           lblAttnValue.Text = AttnValue;

        }


        private void btnReject_Click(object sender, EventArgs e)
        {

            string strReason = edtRemark.Text.Trim();
            if (rdReason.SelectedIndex == 2 && strReason.Length < 1)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Reason is not empty!" : "原因不能为空!");
                return;
            }
            if (rdReason.SelectedIndex==0)
            {
                strReason = lbl0.Text; 
            }
            else if (rdReason.SelectedIndex == 1)
            {
                strReason = lbl1.Text;
            }

            OperationAgentService.RejectDispatchOrRevise(OperationID, OperationType, strReason);
            FindForm().DialogResult = System.Windows.Forms.DialogResult.OK;
            FindForm().Close();
        }

        private void lbl_Click(object sender, EventArgs e)
        {
            LabelControl obj = sender as LabelControl;
            int selectIndex = int.Parse(obj.Name.Substring(3));
            rdReason.SelectedIndex = selectIndex;
            if (selectIndex == 2)
            {
                edtRemark.Enabled = true;
            }
            else
            {
                edtRemark.Enabled = false;
            }
            
        }

        private void rdReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdReason.SelectedIndex == 2)
            {
                edtRemark.Enabled = true;
            }
            else
            {
                edtRemark.Enabled = false;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            FindForm().DialogResult = System.Windows.Forms.DialogResult.Cancel;
            FindForm().Close();
        }

        private void RejectReasonPart_Load(object sender, EventArgs e)
        {
            init();
        }

    }
}
