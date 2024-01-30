using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using System;
using System.Windows.Forms;

namespace ICP.Business.Common.UI.Contact
{
    public partial class UCContactListPartFrom : XtraForm, IDisposable
    {
        public WorkItem WorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }
      
     
        /// <summary>
        /// 查询参数
        /// </summary>
        public BusinessOperationContext BusinessOperationContext { get; set; }

        public UCContactListPartFrom()
        {
            InitializeComponent();

            this.Load += (sender, e) =>
            {
                UCContactListPart UCContactListPart = new Contact.UCContactListPart();
                UCContactListPart.Margin = new Padding(15, 15, 15, 3);
                UCContactListPart.Dock = DockStyle.Fill;
                this.panel1.Controls.Add(UCContactListPart);
                UCContactListPart.DataBind(BusinessOperationContext);
            };
         
        }

      

        private void UCContactListPartFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
