using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.WF.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.WF.Controls.Form.Commission
{
    public partial class WFCommissionLogListPart : BaseListPart
    {
        public WFCommissionLogListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }
            
            };
        }

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IWorkFlowExtendService WorkFlowExtendService 
        {
            get 
            {
                return ServiceClient.GetService<IWorkFlowExtendService>();
            }
        }

        public override object DataSource
        {
            set
            {
                SetDataSource(value);
            }
            get
            {
                return bsList.DataSource; 
            }
        }


        private void SetDataSource(object value)
        {
            WFBusinessList item = value as WFBusinessList;
            if (value != null)
            {
                List<WFCommissionLogList> list = WorkFlowExtendService.GetCommissionLogList(new Guid[1] { item.ID }, LocalData.IsEnglish);
                bsList.DataSource = list;
            }
            else
            {
                bsList.DataSource = new List<WFCommissionLogList>();
            }
            bsList.ResetBindings(false);

        }



    }
}
