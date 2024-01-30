using System.Collections.Generic;
using System.Windows.Forms;
using ICP.FAM.UI.WriteOff.Parts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.FAM.ServiceInterface.DataObjects;
using System;
using ICP.Business.Common.UI.EventList;

namespace ICP.FAM.UI.WriteOff
{
    #region WriteOffWorkItem
    /// <summary>
    /// 
    /// </summary>
    public class WriteOffWorkItem : WorkItem
    {
        protected override void OnRunStarted()
        {
            base.OnRunStarted();

            IWorkspace main = Workspaces[ClientConstants.MainWorkspace];

            main.Show(Items.AddNew<MainWorkspace>(), new SmartPartInfo(LocalData.IsEnglish ? "Check List" : "销账列表", ""));
        }
    }
    #endregion
}
