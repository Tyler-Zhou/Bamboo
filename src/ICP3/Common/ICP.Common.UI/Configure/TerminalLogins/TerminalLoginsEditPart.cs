using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.UI.Common;
using ICP.FCM.Common.ServiceInterface;

namespace ICP.Common.UI.Configure.TerminalLogins
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class TerminalLoginsEditPart : BaseControl, ICP.Framework.ClientComponents.UIManagement.IDataContentPart
    {
        #region 服务注入

        //static ITerminalService terminalService = ServiceProxyFactory.Create<ITerminalService>("TerminalService");

        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        #endregion
        public TerminalLoginsEditPart()
        {
            InitializeComponent();
            this.Enabled = false;
            if (LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate
            {
                this.DataChanged = null;
                this.dxErrorProvider1.DataSource = null;
                this.bindingSource1.DataSource = null;
                this.bindingSource1.Dispose();
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }

            };
        }

        private void SetCnText()
        {
            labCode.Text = "代码";
            labUserID.Text = "用户名";
            labPassword.Text = "密码";
        }

        public void BindingData(object data)
        {
            if (data == null) { this.bindingSource1.DataSource = typeof(ICP.Crawler.ServiceInterface.DataObjects.TerminalLogins); this.Enabled = false; }

            else
            {
                this.bindingSource1.DataSource = data;
                //if ((data as ICP.Crawler.ServiceInterface.DataObjects.TerminalLogins).IsValid == false) { this.Enabled = false; ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).EndEdit(); }
                //else
                //{
                    this.Enabled = true; ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).BeginEdit();
                //}
            }

        }

        #region IDataContentPart 成员
        public bool AutoWidth { get; set; }
        public object Current { get { return this.bindingSource1.Current; } }
        public event EventHandler<ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>> DataChanged;
        public object DataSource
        {
            get { return bindingSource1.DataSource; }
            set { BindingData(value); }
        }
        public void EndEdit()
        {
            ICP.Crawler.ServiceInterface.DataObjects.TerminalLogins logins = (ICP.Crawler.ServiceInterface.DataObjects.TerminalLogins)this.Current;
            //if (currency != null)
            //{
            //    currency.CountryName = cmbCountry.Text.Trim();
            //}

            this.Validate();
            bindingSource1.EndEdit();
        }
        #endregion
    }
}
