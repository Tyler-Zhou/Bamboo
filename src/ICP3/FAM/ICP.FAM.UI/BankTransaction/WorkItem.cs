using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.Collections.Generic;

namespace ICP.FAM.UI.BankTransaction
{
    /// <summary>
    /// 银行流水WorkItem
    /// </summary>
    public class BankTransactionWorkItem : WorkItem
    {

        protected override void OnRunStarted()
        {
            base.OnRunStarted();

            Show();
        }

        private void Show()
        {
            MainWorkspace mainSpace = SmartParts.Get<MainWorkspace>(BankTransactionConstants.MainWorkspace);

            if (mainSpace == null)
            {
                mainSpace = Items.AddNew<MainWorkspace>(BankTransactionConstants.MainWorkspace);

                #region AddPart

                ToolBar toolBar = Items.AddNew<ToolBar>();
                IWorkspace toolBarSpace = Workspaces[BankTransactionConstants.ToolBarWorkspace];
                toolBarSpace.Show(toolBar);

                SearchPart searchPart = Items.AddNew<SearchPart>();
                IWorkspace searchSpace = Workspaces[BankTransactionConstants.SearchWorkspace];
                searchSpace.Show(searchPart);

                ListPart listPart = SmartParts.AddNew<ListPart>();
                IWorkspace listWorkspace = Workspaces[BankTransactionConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                EditPart editor = SmartParts.AddNew<EditPart>();
                editor.Enabled = false;
                IWorkspace editworkspace = Workspaces[BankTransactionConstants.EditWorkspace];
                editworkspace.Show(editor);
                #endregion

                BankTransactionUIAdapter adapter = new BankTransactionUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(editor.GetType().Name, editor);
                adapter.Init(dic);


                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Bank TraTransaction" : "银行流水";
                mainWorkspace.Show(mainSpace, smartPartInfo);
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(mainSpace);
            }
        }
    }
}
