using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FAM.UI
{
    class InvoiceExchangeWorkitem : WorkItem
    {

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            InvoiceExchangeMainWorkSpace mainSpce = SmartParts.Get<InvoiceExchangeMainWorkSpace>("InvoiceExchangeMainWorkSpace");
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<InvoiceExchangeMainWorkSpace>("InvoiceExchangeMainWorkSpace");

                #region AddPart

                InvoiceExchangeListPart listPart = SmartParts.AddNew<InvoiceExchangeListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[InvoiceExchangeWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Invoice Exchange" : "发票汇率";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                //InvoiceUIAdapter bookingAdapter = new InvoiceUIAdapter();
                //Dictionary<string, object> dic = new Dictionary<string, object>();
                //dic.Add(toolBar.GetType().Name, toolBar);
                //dic.Add(listPart.GetType().Name, listPart);
                //dic.Add(searchPart.GetType().Name, searchPart);

                //bookingAdapter.Init(dic);
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }

    /// <summary>
    /// WorkSpace常量
    /// </summary>
    public class InvoiceExchangeWorkSpaceConstants
    {
        public const string ListWorkspace = "ListWorkspace";
    }

}
