using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.UI.Bill
{
    public partial class BillViewInvoiceList : BaseListPart
    {
        public BillViewInvoiceList()
        {
            InitializeComponent();

            Disposed += delegate {
                dataSource = null;

                if (Workitem != null)
                {
                    if (UCInvoiceList != null)
                    {
                        Workitem.Items.Remove(UCInvoiceList);
                        UCInvoiceList = null;
                    }
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };

        }

        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        #endregion

        #region 属性
        private InvoiceListPart UCInvoiceList;

        private PageList dataSource;
        /// <summary>
        /// 数据源
        /// </summary>
        public new PageList DataSource
        {
            get
            {
                return dataSource;
            }
            set
            {
                dataSource = value;
            }

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
            UCInvoiceList = Workitem.Items.AddNew<InvoiceListPart>();


            UCInvoiceList.Dock = DockStyle.Fill;

            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(UCInvoiceList);

             UCInvoiceList.DataSource = dataSource;


  
     
        }

        
        private void barExpress_ItemClick(object sender, ItemClickEventArgs e)
        {
            UCInvoiceList.Command_Express(null,null);
            //Workitem.Commands[InvoiceCommandConstants.Command_Express].Execute();
        }


        private void barPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            UCInvoiceList.Command_PrintInvoice(null,null);
            //Workitem.Commands[InvoiceCommandConstants.Command_PrintInvoice].Execute();
        }

        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }
    }
}
