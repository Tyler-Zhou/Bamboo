using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;

namespace ICP.Business.Common.UI.Contact
{
    public partial class UCContactListWithToolbarFrom  : XtraForm, IDisposable
    {
        public WorkItem WorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }
      
        /// <summary>
        /// 关联类型
        /// </summary>
        public AssociateType associateType { get; set; }
        /// <summary>
        /// 关联集合
        /// </summary>
        public List<BusinessOperationContext> bussOperationContexts { get; set; }
        /// <summary>
        /// 邮件对象
        /// </summary>
        public Message.ServiceInterface.Message message { get; set; }

        public UCContactListWithToolbarFrom()
        {
            InitializeComponent();

            this.Load += (sender, e) =>
            {
                UCContactListWithToolbar ucContactListWithToolbar = new Contact.UCContactListWithToolbar();
                ucContactListWithToolbar.Margin = new Padding(15, 15, 15, 3);
                ucContactListWithToolbar.Dock = DockStyle.Fill;
                this.panel1.Controls.Add(ucContactListWithToolbar);
                ucContactListWithToolbar.BindData(associateType, bussOperationContexts, message);
            };
        }
    }
}
