using System;
using ICP.Common.UI;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.OceanImport.UI.Business.DownLoad
{
    /// <summary>
    /// 选择客服界面
    /// </summary>
    public partial class OIBusinessAssignToCS : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region Service
        public ICPCommUIHelper ICPCommUIHelper 
        {
            get 
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        #endregion

        #region 事件
        public delegate void SelectedHandler(object sender, object data,string assignToName);
        public  event SelectedHandler Selected;

        public delegate void CancelHandler();
        public event CancelHandler Canceled;
        #endregion

        #region 初始化
        public OIBusinessAssignToCS()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
                RegisterEvents();
                InitControl();
                this.Disposed += delegate 
                {   
                    
                    this.Selected = null;
                    this.Canceled = null;
                    this.mcmbCustomerContact.OnFirstEnter -= this.mcmbCustomerContact_Enter;
                    if (this.WorkItem != null)
                    {
                        this.WorkItem.Items.Remove(this);
                        this.WorkItem = null;
                    }
                };
            }
         
        }

        void RegisterEvents()
        {
            this.btnSure.Click += new EventHandler(btnSure_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        void InitControl()
        {
            this.mcmbCustomerContact.ShowSelectedValue(Guid.Empty, string.Empty);
            if (!LocalData.IsEnglish)
            {
                this.lblAssignToCS.Text = "客服: ";
                btnSure.Text = "确定(&S)";
                btnCancel.Text = "取消(&C)";
            }
            this.mcmbCustomerContact.OnFirstEnter += mcmbCustomerContact_Enter;

        }
        #endregion

        #region 事件
        void btnCancel_Click(object sender, EventArgs e)
        {
            if (Canceled != null) Canceled();
            this.FindForm().Close();
        }

        public void btnSure_Click(object sender, EventArgs e)
        {            
            if (mcmbCustomerContact.EditValue != null)
            {
                if (Selected != null) Selected(sender, mcmbCustomerContact.EditValue,mcmbCustomerContact.EditText);
            }

            this.FindForm().Close();
        }


        //绑定客服
        void mcmbCustomerContact_Enter(object sender, EventArgs e)
        {
            ICPCommUIHelper.SetMcmbUsers(this.mcmbCustomerContact, LocalData.UserInfo.DefaultCompanyID, "文件", string.Empty, true); 
        }

        #endregion
    }
}
