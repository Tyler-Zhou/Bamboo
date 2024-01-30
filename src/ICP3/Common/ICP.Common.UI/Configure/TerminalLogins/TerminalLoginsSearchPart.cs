using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIManagement;
using ICP.FCM.Common.ServiceInterface;

namespace ICP.Common.UI.Configure.TerminalLogins
{

    [System.ComponentModel.ToolboxItem(false)]
    public partial class TerminalLoginsSearchPart : DevExpress.XtraEditors.XtraUserControl, ISearchPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        private ITerminalService TerminalService
        {
            get
            {
                return ServiceClient.GetService<ITerminalService>();
            }
        }
        //static ITerminalService terminalService = ServiceProxyFactory.Create<ITerminalService>("TerminalService");

        #endregion

        #region init

        public TerminalLoginsSearchPart()
        {
            InitializeComponent();

            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate
            {
                this.DataReturned = null;
                CommonUtility.RemoveSearchPartKeyEnterToSearch(new List<Control> { this.txtUesrID, txtCode }, KeyEventHandle);
                //this.cmbCountry.QueryPopUp -= this.cmbCountry_QueryPopUp;
                //cmbCountry.SelectedIndexChanged -= this.OncmbCountrySelectedIndexChanged;
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            CommonUtility.SearchPartKeyEnterToSearch(new List<Control> { this.txtUesrID,txtCode }, this.btnSearch,KeyEventHandle);
        }
        private void KeyEventHandle(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnSearch.PerformClick();
        }

        private void SetCnText()
        {
            labCode.Text = "代码";
            labIsValid.Text = "有效";
            labMax.Text = "最大行数";
            labUserID.Text = "用户名";
            lwchkIsValid.CheckedText = "有效";
            lwchkIsValid.NULLText = "全部";
            lwchkIsValid.UnCheckedText = "无效";
            this.btnClear.Text = "清空(&L)";
            this.btnSearch.Text = "查询(&R)";
            nbarBase.Caption = "基本信息";
        }

        #endregion

        #region ISearchPart 成员

        public object GetData()
        {
            List<ICP.Crawler.ServiceInterface.DataObjects.TerminalLogins> data = TerminalService.GetTerminalLoginsList(
                txtCode.Text.Trim(), txtUesrID.Text.Trim(), int.Parse(numMax.Value.ToString()), LocalData.IsEnglish);
            return data;
        }
        public event EventHandler<ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>> DataReturned;

        #endregion

        #region btn
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (DataReturned != null)
                DataReturned(this, new ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>(GetData()));
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
            txtCode.Focus();
        }

        #endregion

        #region ISearchPart 成员

        public virtual void InitialValues(string searchValue, string property, SearchConditionCollection conditions, FinderTriggerType triggerType)
        {
            btnClear.PerformClick();
        }

        #endregion

    }
}

