using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.Drawing;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.UI.Common;
using ICP.MailCenter.CommonUI;
using ICP.MailCenter.ServiceInterface;
using System;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.ObjectBuilder;

namespace ICP.FCM.OceanExport.UI.BL
{
    public class OEBLWorkitem : WorkItem
    {
        #region  服务
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public DocumentListPresenter documentPresenter { get; set; }
        #endregion

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            OEBLMainWorkspace mainSpce = this.SmartParts.Get<OEBLMainWorkspace>("OEBLMainWorkspace");
            if (mainSpce == null)
            {
                mainSpce = this.SmartParts.AddNew<OEBLMainWorkspace>("OEBLMainWorkspace");

                #region AddPart

                OEBLToolBar toolBar = this.SmartParts.AddNew<OEBLToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[OEBLWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                OEBLListPart listPart = this.SmartParts.AddNew<OEBLListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[OEBLWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                BLSearchPart searchPart = this.SmartParts.AddNew<BLSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[OEBLWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);

                BLFastSearchPart fastSearchPart = this.SmartParts.AddNew<BLFastSearchPart>();
                IWorkspace fastSearchPartWorkspace = (IWorkspace)this.Workspaces[OEBLWorkSpaceConstants.FastSearchWorkspace];
                fastSearchPartWorkspace.Show(fastSearchPart);

                MemoListPart memoListPart = this.Items.AddNew<MemoListPart>();
                IWorkspace memoListWorkspace = (IWorkspace)this.Workspaces[OEBLWorkSpaceConstants.MemoListWorkspace];
                memoListWorkspace.Show(memoListPart);

                 UCCommunicationHistory faxMailEDIListPart = this.Items.AddNew<UCCommunicationHistory>();
                //FaxEMailLogListPart faxMailListPart = this.Items.AddNew<FaxEMailLogListPart>();
                IWorkspace faxMailListWorkspace = (IWorkspace)this.Workspaces[OEBLWorkSpaceConstants.FaxMailEDIListWorkspace];
               faxMailListWorkspace.Show(faxMailEDIListPart);

                UCDocumentList documentListPart = this.Items.AddNew<UCDocumentList>();
                //DocumentListPart documentListPart = this.Items.AddNew<DocumentListPart>();
                IWorkspace documentListWorkspace = (IWorkspace)this.Workspaces[OEBLWorkSpaceConstants.DocumentListWorkspace];
                documentListWorkspace.Show(documentListPart);

                #endregion

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Ocean Export BL List" : "海出提单";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                OEBLUIAdapter blAdapter = new OEBLUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(fastSearchPart.GetType().Name, fastSearchPart);
                dic.Add(memoListPart.GetType().Name, memoListPart);
                dic.Add(faxMailEDIListPart.GetType().Name, faxMailEDIListPart);
                dic.Add(documentListPart.GetType().Name, documentListPart);
                blAdapter.Init(dic,documentPresenter);

                if (_values != null)
                {
                    fastSearchPart.Init(_values);
                    fastSearchPart.RaiseSearched();
                    _values = null;
                }
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }


        IDictionary<string, object> _values;
        /// <summary>
        /// 简易搜索器
        /// </summary>
        /// <param name="values"></param>
        public void Init(IDictionary<string, object> values)
        {
            _values = values;
        }
    }


    public class OEBLColorConstant
    {
        /// <summary>
        /// 对单中
        /// </summary>
        public static Color CheckingColor = Color.FromArgb(67, 156, 50); //Color.Lime;
        /// <summary>
        /// 对单完成
        /// </summary>
        public static Color CheckedColor = Color.FromArgb(237, 75, 11); //Color.Orange;

        /// <summary>
        /// 放单
        /// </summary>
        public static Color ReleaseColor = Color.FromArgb(233, 160, 0); //Color.Blue;
    }

    /// <summary>
    /// 命令常量
    /// </summary>
    public class OEBLCommandConstants
    {
        /// <summary>
        /// 新增MBL
        /// </summary>
        public const string Command_AddMBL = "Command_AddMBL";
        /// <summary>
        /// 新增HBL
        /// </summary>
        public const string Command_AddHBL = "Command_AddHBL";
        /// <summary>
        /// 复制提单
        /// </summary>
        public const string Command_CopyData = "Command_OELCopyData";
        /// <summary>
        /// 编辑提单
        /// </summary>
        public const string Command_EditData = "Command_OELEditData";
        /// <summary>
        /// 删除提单
        /// </summary>
        public const string Command_DeleteData = "Command_OELDeleteData";
        /// <summary>
        /// 开始对单
        /// </summary>
        public const string Command_Check = "Command_OELCheck";
        /// <summary>
        /// 完成对单
        /// </summary>
        public const string Command_CompleteCheck = "Command_OELCompleteCheck";
        /// <summary>
        /// 打印提单
        /// </summary>
        public const string Command_PrintBL = "Command_OELPrintBL";
        /// <summary>
        /// 打印利润表
        /// </summary>
        public const string Command_PrintProfit = "Command_OEPrintProfit";
        /// <summary>
        /// 打印装箱单
        /// </summary>
        public const string Command_PrintLoadCtn = "Command_OELPrintLoadCtn";
        /// <summary>
        /// 打印装货单
        /// </summary>
        public const string Command_PrintLoadGoods = "Command_OELPrintLoadGoods";
        /// <summary>
        /// 确认装船
        /// </summary>
        public const string Command_LoadShip = "Command_OELLoadShip";
        /// <summary>
        /// 申请代理
        /// </summary>
        public const string Command_ReplyAgent = "Command_OELReplyAgent";
        /// <summary>
        /// E-MBL
        /// </summary>
        public const string Command_E_MBL = "Command_E_OELMBL";
        /// <summary>
        /// ISF
        /// </summary>
        public const string Command_ISF = "Command_OELISF";
        /// <summary>
        /// 帐单
        /// </summary>
        public const string Command_Bill = "Command_OELBill";

        /// <summary>
        /// 显示搜索面板
        /// </summary>
        public const string Command_ShowSearch = "Command_OELShowSearch";

        /// <summary>
        /// 刷新
        /// </summary>
        public const string Command_RefreshData = "Command_OELRefreshData";
        /// <summary>
        /// 分单
        /// </summary>
        public const string Command_SplitBL = "Command_OELSplitBL";
        /// <summary>
        /// 合单
        /// </summary>
        public const string Command_Merge = "Command_OELMerge";
        /// <summary>
        /// 确认放单
        /// </summary>
        public const string Command_ConfirmReleaseBL = "Command_OELConfirmReleaseBL";
        /// <summary>
        /// 查看模式-MBL
        /// </summary>
        public const string Command_VisibleMBL = "Command_OELVisibleMBL";
        /// <summary>
        /// 查看模式-HBL
        /// </summary>
        public const string Command_VisibleHBL = "Command_OELVisibleHBL";
        /// <summary>
        /// 查看模式-全部
        /// </summary>
        public const string Command_VisibleALL = "Command_OELVisibleALL";

        /// <summary>
        /// 核销单
        /// </summary>
        public const string Command_VerifiSheet = "Command_OELVerifiSheet";

        /// <summary>
        /// 装箱
        /// </summary>
        public const string Command_LoadContainer = "Command_LoadContainer";
    }

    /// <summary>
    /// WorkSpace常量
    /// </summary>
    public class OEBLWorkSpaceConstants
    {
        /// <summary>
        /// 工具栏
        /// </summary>
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        /// <summary>
        /// 搜索
        /// </summary>
        public const string SearchWorkspace = "SearchWorkspace";
        /// <summary>
        /// 列表
        /// </summary>
        public const string ListWorkspace = "ListWorkspace";
        /// <summary>
        /// 快捷搜索
        /// </summary>
        public const string FastSearchWorkspace = "FastSearchWorkspace";

        public const string MemoListWorkspace = "MemoListWorkspace";
        public const string FaxMailEDIListWorkspace = "FaxMailEDIListWorkspace";
        public const string DocumentListWorkspace = "DocumentListWorkspace";
    }

    public class OEBLUIAdapter
    {
        #region parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        ISearchPart _fastSearchPart;
        OEBLListPart _mainListPart;
        IListPart _memolistPart;
       UCCommunicationHistory _faxMailEDIListPart;
        UCDocumentList _DocumentListPart;

        #endregion

        #region interface and RefreshBarEnabled

        public void Init(Dictionary<string, object> controls,DocumentListPresenter documentPresenter)
        {

            _toolBar = (IToolBar)controls[typeof(OEBLToolBar).Name];
            _searchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(BLSearchPart).Name];
            _mainListPart = (OEBLListPart)controls[typeof(OEBLListPart).Name];
            _fastSearchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(ICP.FCM.OceanExport.UI.BL.BLFastSearchPart).Name];
            _memolistPart = (IListPart)controls[typeof(MemoListPart).Name];
            _faxMailEDIListPart = (UCCommunicationHistory)controls[typeof(UCCommunicationHistory).Name];
            _DocumentListPart = (UCDocumentList)controls[typeof(UCDocumentList).Name];

            RefreshBarEnabled(_toolBar, null);

            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                OceanBLList listData = data as OceanBLList;
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add("ParentList", data);

                if (listData != null)
                {
                    MemoParam para = new MemoParam();
                    para.OperationId = listData.OceanBookingID;
                    para.FormID = listData.ID;
                    if (listData.BLType == FCMBLType.MBL)
                    {
                        para.FormType = ICP.Framework.CommonLibrary.Common.FormType.MBL;
                    }
                    else
                    {
                        para.FormType = ICP.Framework.CommonLibrary.Common.FormType.HBL;
                    }

                    para.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.OceanExport;
                    _memolistPart.DataSource = para;
                    Utility.SetDocumentListDataSource(_DocumentListPart, documentPresenter, para);
                    //设置Fax/Email/EDI数据源
                    Utility.SetCommunicationDataSource(_faxMailEDIListPart, para);
                }

                #region toolBar

                RefreshBarEnabled(_toolBar, listData);

                #endregion
            };

            _mainListPart.KeyDown += delegate(object sender, System.Windows.Forms.KeyEventArgs e)
            {
                if (sender != null)
                {
                    Dictionary<string, object> keyValue = sender as Dictionary<string, object>;
                    if (keyValue != null)
                    {
                        _searchPart.Init(keyValue);
                        _searchPart.RaiseSearched();
                    }
                }
            };

            #endregion

            #region _searchPart.OnSearched
            _searchPart.OnSearched += delegate(object sender, object results)
            {
                _mainListPart.DataSource = results;
            };
            #endregion


            #region _searchPart.OnSearched
            _fastSearchPart.OnSearched += delegate(object sender, object results)
            {
                _mainListPart.DataSource = results;
            };
            #endregion

            #endregion

            // _fastSearchPart.RaiseSearched();
        }

        private void RefreshBarEnabled(IToolBar toolBar, OceanBLList listData)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barCopy", false);
                toolBar.SetEnable("barEdit", false);
                toolBar.SetEnable("barDelete", false);
                toolBar.SetEnable("barReleaseBL", false);

                toolBar.SetEnable("barCheck", false);
                toolBar.SetEnable("barCompleteCheck", false);

                toolBar.SetEnable("barPrint", false);
                toolBar.SetEnable("barPrintBL", false);
                toolBar.SetEnable("barPrintLoadCtn", false);
                toolBar.SetEnable("barPrintLoadGoods", false);
                toolBar.SetEnable("barLoadShip", false);
                toolBar.SetEnable("barReplyAgent", false);
                toolBar.SetEnable("barE_MBL", false);
                toolBar.SetEnable("barISF", false);
                toolBar.SetEnable("barBill", false);
                toolBar.SetEnable("barVerifiSheet", false);
                toolBar.SetEnable("barDocument", false);
                toolBar.SetEnable("barFaxEmail", false);
                toolBar.SetEnable("barMemo", false);

                toolBar.SetEnable("barSplitAndMerge", false);
                toolBar.SetEnable("barSplitBL", false);
                toolBar.SetEnable("barMergeBL", false);
            }
            else
            {

                toolBar.SetEnable("barEdit", true);

                toolBar.SetEnable("barDocument", true);
                toolBar.SetEnable("barFaxEmail", true);
                toolBar.SetEnable("barMemo", true);
                toolBar.SetEnable("barSplitAndMerge", true);

                if (listData.IsValid == false)
                {
                    toolBar.SetEnable("barCopy", false);
                    toolBar.SetEnable("barPrintBL", false);
                    toolBar.SetEnable("barPrintLoadCtn", false);
                    toolBar.SetEnable("barPrintLoadGoods", false);
                    toolBar.SetEnable("barE_MBL", false);
                    toolBar.SetEnable("barISF", false);
                    toolBar.SetEnable("barBill", false);
                    toolBar.SetEnable("barVerifiSheet", false);
                    toolBar.SetEnable("barDelete", false);
                    toolBar.SetEnable("barPrint", false);

                    toolBar.SetEnable("barLoadShip", false);
                    toolBar.SetEnable("barCheck", false);
                    toolBar.SetEnable("barCompleteCheck", false);
                    toolBar.SetEnable("barReplyAgent", false);

                }
                else
                {
                    toolBar.SetEnable("barCopy", true);
                    toolBar.SetEnable("barPrintBL", true);
                    toolBar.SetEnable("barPrintLoadCtn", true);
                    toolBar.SetEnable("barPrintLoadGoods", true);
                    toolBar.SetEnable("barE_MBL", true);
                    toolBar.SetEnable("barISF", true);
                    toolBar.SetEnable("barBill", true);
                    toolBar.SetEnable("barVerifiSheet", true);
                    toolBar.SetEnable("barPrint", true);

                    toolBar.SetEnable("barDelete", false);
                    toolBar.SetEnable("barLoadShip", false);
                    toolBar.SetEnable("barCheck", false);
                    toolBar.SetEnable("barCompleteCheck", false);
                    toolBar.SetEnable("barReplyAgent", false);
                    toolBar.SetEnable("barSplitBL", false);
                    toolBar.SetEnable("barMergeBL", false);
                    toolBar.SetEnable("barReleaseBL", false);

                    #region
                    //删除条件：
                    //MBL:没有HBL，并且没有费用 
                    //HBL:没有费用
                    if (!listData.ExistFees)
                    {
                        if (listData.HBLCount == 0 && listData.BLType == FCMBLType.MBL)
                        {
                            toolBar.SetEnable("barDelete", true);
                        }
                        if (listData.BLType == FCMBLType.HBL)
                        {
                            toolBar.SetEnable("barDelete", true);
                        }

                    }
                    ////有箱可以打印 
                    //if (string.IsNullOrEmpty(listData.ContainerNos)) toolBar.SetEnable("barPrint", false);
                    if (listData.BLType == FCMBLType.HBL) toolBar.SetEnable("barPrintLoadGoods", false);
                    // 如果有箱 或顺签提单，确认装船
                    if (string.IsNullOrEmpty(listData.ContainerNos) == false || listData.IssueType == IssueType.Post_date)
                    {
                        toolBar.SetEnable("barLoadShip", true);
                    }
                    if ((listData.OEOperationType == FCMOperationType.LCL || listData.OEOperationType == FCMOperationType.BULK)
                        && string.IsNullOrEmpty(listData.ContainerNos))
                    {
                        //装箱或散货，在没有箱号的情况下，不能进行装船确认
                        toolBar.SetEnable("barLoadShip", false);
                    }
                    if (listData.State == OEBLState.Draft
                        && listData.HBLCount <= 0 && string.IsNullOrEmpty(listData.ContainerNos) == false)
                    {
                        toolBar.SetEnable("barCheck", true);
                    }

                    if (listData.State != OEBLState.Release && listData.State != OEBLState.Checked
                       && listData.HBLCount <= 0 && string.IsNullOrEmpty(listData.ContainerNos) == false)
                    {
                        toolBar.SetEnable("barCompleteCheck", true);
                    }

                    if (listData.State != OEBLState.Checked && listData.State != OEBLState.Release)
                    {
                        toolBar.SetEnable("barMergeBL", true);
                        toolBar.SetEnable("barReplyAgent", true);
                    }

                    if (listData.State != OEBLState.Checked && listData.State != OEBLState.Release && listData.HBLCount <= 0)
                    {
                        toolBar.SetEnable("barSplitBL", true);
                    }
                    if (listData.State == OEBLState.Checked)
                    {
                        toolBar.SetEnable("barReleaseBL", true);
                    }

                    #endregion
                }
            }
        }

        #endregion
    }
}
