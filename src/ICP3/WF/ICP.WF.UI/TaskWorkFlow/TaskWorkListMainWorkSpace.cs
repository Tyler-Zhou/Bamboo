using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.WF.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.WF.ServiceInterface.DataObject;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Common;
using ICP.WF.Controls;
using ICP.Framework.ClientComponents.Controls;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using System.Collections;
using ICP.WF.UI.Common;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary;

namespace ICP.WF.UI
{
    public partial class TaskWorkListMainWorkSpace : XtraUserControl
    {
        public TaskWorkListMainWorkSpace()
        {
            InitializeComponent();
        }

        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region 属性
        WorkListToolBarPart toolPart = null;
        WorkListSearch searchPart = null;
        WorkListPart listPart = null;
        WorkListFlowChatPart chatPart = null;


        public string ViewCode
        {
            get;
            set;
        }
        public string StrQuery
        {
            get;
            set;
        }
        #endregion


        #region 加载
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                toolPart = Workitem.Items.AddNew<WorkListToolBarPart>();
                toolPart.Dock = DockStyle.Fill;
                this.pnlTop.Controls.Add(toolPart);

                listPart = Workitem.Items.AddNew<WorkListPart>();
                listPart.Dock = DockStyle.Fill;
                listPart.ViewCode = ViewCode;
                listPart.StrQuery = StrQuery;
                this.pnlMain.Controls.Add(listPart);

                chatPart = Workitem.Items.AddNew<WorkListFlowChatPart>();
                chatPart.Dock = DockStyle.Fill;
                this.pnlWorkView.Controls.Add(chatPart);

                searchPart = Workitem.Items.AddNew<WorkListSearch>();

                ///控件之间的交互
                WorkFlowUIAdapter adapter = new WorkFlowUIAdapter();
                adapter.Init(toolPart,
                             searchPart,
                             listPart,
                             chatPart,
                             Workitem);
            }

        }

        #endregion

    }
}
