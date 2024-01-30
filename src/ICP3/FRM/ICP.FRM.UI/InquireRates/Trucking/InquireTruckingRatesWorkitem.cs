using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using System.Windows.Forms;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.ClientComponents.Service;

namespace ICP.FRM.UI.InquireRates
{
    public class InquireTruckingRatesWorkitem : WorkItem
    {
        /// <summary>
        /// 
        /// </summary>
        public InquireTruckingRatesMainWorkspace TruckingRatesMainWorkspace
        {
            get;
            set;
        }

        #region Show

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            InquireTruckingRatesMainWorkspace srMainWorkspace = SmartParts.Get<InquireTruckingRatesMainWorkspace>("InquireTruckingRatesMainWorkspace");
            IWorkspace mainWorkspace = Workspaces[InquireRatesWorkSpaceConstants.TruckingRatesWorkspace];
            if (srMainWorkspace == null)
            {
                srMainWorkspace = SmartParts.AddNew<InquireTruckingRatesMainWorkspace>("InquireTruckingRatesMainWorkspace");

                #region AddPart

                InquireTruckingRatesListPart ioMainListPart = SmartParts.AddNew<InquireTruckingRatesListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[InquireTruckingRatesWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(ioMainListPart);

                //沟通历史记录
                InquireTruckingRatesEmailPart faxMailEDIListPart = Items.AddNew<InquireTruckingRatesEmailPart>();
                faxMailEDIListPart.Dock = DockStyle.Fill;
                IWorkspace faxMailListWorkspace = (IWorkspace)Workspaces[InquireTruckingRatesWorkSpaceConstants.CommunicationHistoryWorkspace];
                faxMailListWorkspace.Show(faxMailEDIListPart);
                //询价历史
                //询价历史记录工具栏
                InquireRatesHistoryToolBar historyToolBar = SmartParts.AddNew<InquireRatesHistoryToolBar>();
                historyToolBar.Dock = DockStyle.Fill;
                IWorkspace historyToolBarWorkspace = (IWorkspace)Workspaces[InquireTruckingRatesWorkSpaceConstants.HistoryToolBarWorkspace];
                historyToolBarWorkspace.Show(historyToolBar);
                //询价历史记录集合
                InquireTruckingRatesHistoryListPart historyListPart = SmartParts.AddNew<InquireTruckingRatesHistoryListPart>();
                historyListPart.Dock = DockStyle.Fill;
                IWorkspace historyWorkspace = (IWorkspace)Workspaces[InquireTruckingRatesWorkSpaceConstants.HistoryWorkspace];
                historyWorkspace.Show(historyListPart);
                //询价通用信息
                InquireTruckingRatesGeneralInfoPart generalInfoPart = SmartParts.AddNew<InquireTruckingRatesGeneralInfoPart>();
                IWorkspace generalInfoWorkspace = (IWorkspace)Workspaces[InquireTruckingRatesWorkSpaceConstants.GeneralInfoWorkspace];
                generalInfoWorkspace.Show(generalInfoPart);

                ioMainListPart.GeneralInfoPart = generalInfoPart;

                #endregion

                #region 定义面板连接

                #region 询价信息集合当前选择行信息改变之前
                ioMainListPart.CurrentChanging += delegate(object sender, CancelEventArgs e)
                        {
                            bool isCancel = false;
                            if (ioMainListPart.IsChanged || generalInfoPart.IsChanged)
                            {
                                DialogResult result = XtraMessageBox.Show(
                            NativeLanguageService.GetText(ioMainListPart, "CurrentChanging")
                            , "Tip"
                            , MessageBoxButtons.YesNoCancel
                            , MessageBoxIcon.Question);

                                if (result == DialogResult.Cancel) isCancel = true;
                                else if (result == DialogResult.No)
                                {
                                    ioMainListPart.ResetCurrent();
                                    isCancel = false;
                                }
                                else
                                {
                                    if (ioMainListPart.IsChanged)
                                    {
                                        isCancel = !ioMainListPart.SaveRateList(false);
                                    }
                                    else
                                    {
                                        generalInfoPart.RaiseSaved();
                                    }
                                }
                            }

                            e.Cancel = isCancel;
                        }; 
                #endregion

                #region 询价信息集合当前选择行信息改变后
                ioMainListPart.CurrentChanged += (sender, data) =>
                {
                    BusinessOperationContext contex = new BusinessOperationContext();

                    ClientInquierTruckingRate currentData = data as ClientInquierTruckingRate;
                    if (currentData == null)
                    {
                        //各面板清空原有数据
                        srMainWorkspace.CurrentInquierRate = null;
                        faxMailEDIListPart.DataSourceBind(contex);
                        generalInfoPart.DataSource = null;
                    }
                    else
                    {
                        srMainWorkspace.CurrentInquierRate = currentData;
                        srMainWorkspace.PanelDataBind();
                    }
                }; 
                #endregion
                //ioMainListPart.InvokeGetData += delegate(object sender, object data)
                //{
                //    opSearchPart.RaiseSearched(data);
                //};

                #endregion

                
                if (mainWorkspace!=null)
                    mainWorkspace.Show(srMainWorkspace);
            }
            else
            {
                if (mainWorkspace!=null)
                    mainWorkspace.Activate(srMainWorkspace);
            }
            TruckingRatesMainWorkspace = srMainWorkspace;
        }

        #endregion
    }

    #region Constants

    /// <summary>
    /// SearchAir WorkSpace常量
    /// </summary>
    public class InquireTruckingRatesWorkSpaceConstants
    {
        public const string ListWorkspace = "ListWorkspace";
        public const string GeneralInfoWorkspace = "GeneralInfoWorkspace";
        public const string DiscussingWorkspace = "DiscussingWorkspace";
        /// <summary>
        /// History ToolBar Workspace(询价历史记录工具栏容器)
        /// </summary>
        public const string HistoryToolBarWorkspace = "HistoryToolBarWorkspace";
        /// <summary>
        /// History Workspace(询价历史记录容器)
        /// </summary>
        public const string HistoryWorkspace = "HistoryWorkspace";
        /// <summary>
        /// Communication History Workspace(沟通历史记录容器)
        /// </summary>
        public const string CommunicationHistoryWorkspace = "CommunicationHistoryWorkspace";
    }
  
    #endregion
}
