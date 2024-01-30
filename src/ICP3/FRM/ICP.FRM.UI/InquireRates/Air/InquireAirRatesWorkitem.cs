using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using System.Windows.Forms;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.ClientComponents.Service;
using ICP.Business.Common.UI.Communication;

namespace ICP.FRM.UI.InquireRates
{
    public class InquireAirRatesWorkitem : WorkItem
    {
        /// <summary>
        /// 
        /// </summary>
        public InquireAirRatesMainWorkspace AirRatesMainWorkspace
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
            InquireAirRatesMainWorkspace srMainWorkspace = SmartParts.Get<InquireAirRatesMainWorkspace>("InquireAirRatesMainWorkspace");
            IWorkspace mainWorkspace = Workspaces[InquireRatesWorkSpaceConstants.AirRatesWorkspace];

            if (srMainWorkspace == null)
            {
                srMainWorkspace = SmartParts.AddNew<InquireAirRatesMainWorkspace>("InquireAirRatesMainWorkspace");

                #region AddPart
                //询价信息集合
                InquireAirRatesListPart ioMainListPart = SmartParts.AddNew<InquireAirRatesListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[InquireAirRatesWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(ioMainListPart);

                //沟通历史记录
                UCCommunicationHistory faxMailEDIListPart = Items.AddNew<UCCommunicationHistory>();
                faxMailEDIListPart.Dock = DockStyle.Fill;
                IWorkspace faxMailListWorkspace = (IWorkspace)Workspaces[InquireAirRatesWorkSpaceConstants.CommunicationHistoryWorkspace];
                faxMailListWorkspace.Show(faxMailEDIListPart);
                //询价通用信息
                InquireAirRatesGeneralInfoPart generalInfoPart = SmartParts.AddNew<InquireAirRatesGeneralInfoPart>();
                IWorkspace generalInfoWorkspace = (IWorkspace)Workspaces[InquireAirRatesWorkSpaceConstants.GeneralInfoWorkspace];
                generalInfoWorkspace.Show(generalInfoPart);

                ioMainListPart.GeneralInfoPart = generalInfoPart;
                ioMainListPart.GeneralInfoPart.ChangedUnitEvent += new InquireAirRatesGeneralInfoPart.ChangedUnitEventHandler(ioMainListPart.GeneralInfoPart_ChangedUnitEvent);

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
                ioMainListPart.CurrentChanged += delegate(object sender, object data)
                        {
                            BusinessOperationContext contex = new BusinessOperationContext();
                            ClientInquierAirRate currentData = data as ClientInquierAirRate;
                            if (currentData == null)
                            {
                                faxMailEDIListPart.BindData(contex);
                                generalInfoPart.DataSource = null;
                            }
                            else
                            {
                                if (generalInfoPart.CurrentInquierRate == null ||
                               (currentData.ID != generalInfoPart.CurrentInquierRate.ID &&
                               currentData.ID != generalInfoPart.CurrentInquierRate.MainRecordID &&
                               currentData.MainRecordID != generalInfoPart.CurrentInquierRate.ID &&
                               (currentData.MainRecordID == null ||
                               currentData.MainRecordID != generalInfoPart.CurrentInquierRate.MainRecordID)))
                                {
                                    generalInfoPart.DataSource = currentData;
                                }
                                //不管是否选择主询价，沟通记录信息都查询
                                //CommunicationHistory设置数据源
                                contex.OperationID = currentData.ID;
                                faxMailEDIListPart.BindData(contex);
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
                if (mainWorkspace != null)
                    mainWorkspace.Activate(srMainWorkspace);
            }
            AirRatesMainWorkspace = srMainWorkspace;
        }

        #endregion
    }

    #region Constants

    /// <summary>
    /// SearchAir WorkSpace常量
    /// </summary>
    public class InquireAirRatesWorkSpaceConstants
    {
        public const string ListWorkspace = "ListWorkspace";
        public const string GeneralInfoWorkspace = "GeneralInfoWorkspace";
        public const string DiscussingWorkspace = "DiscussingWorkspace";
        /// <summary>
        /// Communication History Workspace(沟通历史记录容器)
        /// </summary>
        public const string CommunicationHistoryWorkspace = "CommunicationHistoryWorkspace";
    }
  
    #endregion
}
