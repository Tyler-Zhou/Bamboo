using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using ICP.FAM.UI.WriteOff.Parts;
using ICP.FCM.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Commands;
using DevExpress.XtraBars.Docking;
using ICP.Framework.CommonLibrary.Client;
using ICP.Business.Common.UI.EventList;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.FAM.UI.WriteOff
{
    [ToolboxItem(false)]
    public partial class MainWorkspace : BasePart
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IFCMCommonClientService FCMCommonClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFCMCommonClientService>();
            }
        }

        public MainWorkspace()
        {
            InitializeComponent();

            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Workspaces.Remove(ToolbarWorkspace);
                    Workitem.Workspaces.Remove(SearchWorkspace);
                    Workitem.Workspaces.Remove(ListWorkspace);
                    Workitem.Items.Remove(this);

                    SearchWorkspace.PerformLayout();
                    ToolbarWorkspace.PerformLayout();
                    ListWorkspace.PerformLayout();

                    PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;
                }
            };

            dpSearch.Text = LocalData.IsEnglish ? "Search" : "查询";

            Load += new EventHandler(MainWorkspace_Load);
        }

        ToolBar toolBar;
        WriteOffList list;

        void MainWorkspace_Load(object sender, EventArgs e)
        {
            toolBar = Workitem.SmartParts.AddNew<ToolBar>();
            IWorkspace toolBarSpace = Workitem.Workspaces[WriteOffWorkSpace.ToolBarWorkspace];
            toolBarSpace.Show(toolBar);

            SearchPanel searchPanel = Workitem.SmartParts.AddNew<SearchPanel>();
            IWorkspace searchSpace = Workitem.Workspaces[WriteOffWorkSpace.SearchWorkspace];
            searchSpace.Show(searchPanel);

            list = Workitem.SmartParts.AddNew<WriteOffList>();
            IWorkspace listSpace = Workitem.Workspaces[WriteOffWorkSpace.ListWorkspace];
            listSpace.Show(list);

            MultiSelections multi = Workitem.SmartParts.AddNew<MultiSelections>();
            IWorkspace multiSpace = Workitem.Workspaces[WriteOffWorkSpace.MultiSelection];
            multiSpace.Show(multi);

            EventListPart eventListPart = Workitem.SmartParts.AddNew<EventListPart>();
            IWorkspace eventListWorkspace = Workitem.Workspaces[WriteOffWorkSpace.EventListWorkspace];
            eventListWorkspace.Show(eventListPart);

            WriteOffUIAdapter adapter = new WriteOffUIAdapter();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add(toolBar.GetType().Name, toolBar);
            dic.Add(searchPanel.GetType().Name, searchPanel);
            dic.Add(list.GetType().Name, list);
            dic.Add(multi.GetType().Name, multi);
            dic.Add(eventListPart.GetType().Name, eventListPart);
            adapter.Init(dic);
            if(Workitem.RootWorkItem.State[ModuleConstantsForFAM.FAM_STATEOBJECT_WRITEOFFLIST_BANKTRANSACTION]!=null)
            {
                adapter.AdvanceSearch();
            }
        }

        [CommandHandler(WriteOffCommands.Command_AllowMultiSelection)]
        public void Command_AllowMultiSelection(object sender, EventArgs e)
        {

            BarCheckItem bar = toolBar.bbiMultiSelectionView;

            if (bar.Checked)
            {
                splitContainerControl3.PanelVisibility = SplitPanelVisibility.Both;
            }
            else
            {
                splitContainerControl3.PanelVisibility = SplitPanelVisibility.Panel1;
            }

            toolBar.ForMultiView(bar.Checked);
            list.SetCheck(bar.Checked);
        }

        [CommandHandler(WriteOffCommands.Command_ShowSearch)]
        public void Command_ShowSearch(object o, EventArgs e)
        {
            if (dpSearch.Visibility == DockVisibility.Hidden)
            {
                dpSearch.Visibility = DockVisibility.Visible;
            }
            else
            {
                dpSearch.Visibility = DockVisibility.Hidden;
            }
            ToolbarWorkspace.SendToBack();
            Refresh();
        }

    }
}
