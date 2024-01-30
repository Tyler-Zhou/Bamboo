using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.Common.UI.Configure.EDIConfigure
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class EDIConfigureSearchPart : BaseSearchPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        #endregion

        public EDIConfigureSearchPart()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate {
                this.OnSearched = null;
                this.OnConditionChanged = null;
                this.bsChargingGroup.DataSource = null;
                this.bsChargingGroup.Dispose();
                this.cmbServiceName.OnFirstEnter -= this.OncmbServiceNameFirstEnter;
                this.cmbCarrier.OnFirstEnter -= this.OncmbCarrierFirstEnter;
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        #region 控制器


        public EDIConfigureController Controller
        {
            get
            {
                return ClientHelper.Get<EDIConfigureController, EDIConfigureController>();
            }
        }

        #endregion  

        protected override void OnLoad(EventArgs e)
        {
            this.InitControls();
        }
        private void OncmbServiceNameFirstEnter(object sender, EventArgs e)
        {
            List<ConfigureKeyList> keyList = Controller.GetConfigureKeyListForType(ConfigureType.EDI);
            this.cmbServiceName.Properties.BeginUpdate();
            cmbServiceName.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, null));
            foreach (var item in keyList)
            {
                cmbServiceName.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.ID));
            }
            this.cmbServiceName.Properties.EndUpdate();
        }
        private void OncmbCarrierFirstEnter(object sender, EventArgs e)
        {
            List<CustomerList> customers = CustomerService.GetCustomerListByList(string.Empty, string.Empty, string.Empty, string.Empty,
                                                                                         string.Empty, string.Empty, null, null, null,
                                                                                         CustomerType.Carrier, null, null, null, null, null, 0);

            CustomerList emptyCustomer = new CustomerList();
            emptyCustomer.CName = emptyCustomer.EName = string.Empty;
            //emptyCustomer.ID = Guid.Empty;
            customers.Insert(0, emptyCustomer);
            this.cmbCarrier.Properties.BeginUpdate();
            foreach (CustomerList item in customers)
            {
                cmbCarrier.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
            }
            this.cmbCarrier.Properties.EndUpdate();
        }
        private void InitControls()
        {
            this.cmbServiceName.OnFirstEnter += this.OncmbServiceNameFirstEnter;
            this.cmbCarrier.OnFirstEnter += this.OncmbCarrierFirstEnter;

        }


        private void SetCnText()
        {
            labCompany.Text = "服务名";
            labSolution.Text = "船公司";
            labIsValid.Text = "有效";
            labMax.Text = "最大行数";

            lwchkIsValid.CheckedText = "有效";
            lwchkIsValid.UnCheckedText = "无效";
            lwchkIsValid.NULLText = "全部";

            nbarBase.Caption = "基本信息";

            btnClear.Text = "清空(&L)";
            btnSearch.Text = "查询(&R)";
        }


        #region ISearchPart 接口

        /// <summary>
        /// 查询界面条件改变后触发该事件
        /// <remarks>
        /// 只有场景中需要该接口方法取值时,才需要实现该接口方法.
        /// 如果没用到该接口方法,为了代码的清洁可以清除.
        /// </remarks>
        /// </summary>
        public override event ConditionChangedHandler OnConditionChanged;

        /// <summary>
        /// 查询完成后,触发该事件
        /// <remarks>
        /// 查询完成后,一定要触发该事件
        /// </remarks>
        /// </summary>
        public override event SearchResultHandler OnSearched;

        public override object GetData()
        {
            try
            {
                Guid? serviceConfigureKeyID = (Guid?)cmbServiceName.EditValue;
                Guid? carrierID = (Guid?)cmbCarrier.EditValue;
                if (carrierID == Guid.Empty)
                {
                    carrierID = null;
                }

                List<EDIConfigureList> codeList = ConfigureService.GetEDIConfigureList(
                    serviceConfigureKeyID,
                    carrierID,
                    lwchkIsValid.Checked,
                    int.Parse(numMax.Value.ToString()));

                return codeList;
            }
            catch (Exception ex)
            {
                throw ex;//UNDONE: 错误提示处理
            }
        }   

        /// <summary>
        /// 从外界向查询面版初始化值
        /// <remarks>
        /// 只有场景中需要该接口方法取值时,才需要实现该接口方法.
        /// 如果没用到该接口方法,为了代码的清洁可以清除.
        /// </remarks>
        /// </summary>
        /// <param name="values">初始化值</param>
        public override void Init(IDictionary<string, object> values)
        {
        }

        /// <summary>
        /// 触发工具栏按钮的查询事件(列入下拉工具栏按钮,文本框按钮)
        /// <remarks>
        /// 只有场景中需要该接口方法取值时,才需要实现该接口方法.
        /// 如果没用到该接口方法,为了代码的清洁可以清除.
        /// </remarks>
        /// </summary>
        public override void RaiseSearched()
        {
            if (this.OnSearched != null)
            {
                using (new CursorHelper())
                {
                    object datas = this.GetData();
                    this.OnSearched(this, datas);
                }
            }
        }

        #endregion

        #region btn

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.RaiseSearched();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is DevExpress.XtraEditors.TextEdit
                    && (item is DevExpress.XtraEditors.SpinEdit) == false
                    && (item is DevExpress.XtraEditors.ComboBoxEdit) == false

                    && item.Enabled == true
                    && (item as DevExpress.XtraEditors.TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;
            }

            lwchkIsValid.Checked = true;
            cmbServiceName.EditValue = null;
            cmbServiceName.Text = string.Empty;
            cmbCarrier.Text = string.Empty;
            cmbCarrier.EditValue = null;
        }

        #endregion

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(EDIConfigureConstants.CMD_Refresh)]
        public void CMD_Refresh(object s, EventArgs e)
        {
            this.RaiseSearched();
        } 
    }
}
