using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.Drawing;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.MailCenter.CommonUI;
using Microsoft.Practices.ObjectBuilder;

namespace ICP.FCM.AirExport.UI.BL
{
    public class AEBLWorkitem : WorkItem
    {
        #region 服务
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
            AEBLMainWorkspace mainSpce = this.SmartParts.Get<AEBLMainWorkspace>("AEBLMainWorkspace");
            if (mainSpce == null)
            {
                mainSpce = this.SmartParts.AddNew<AEBLMainWorkspace>("AEBLMainWorkspace");

                #region AddPart

                AEBLToolBar toolBar = this.SmartParts.AddNew<AEBLToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[AEBLWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                AEBLListPart listPart = this.SmartParts.AddNew<AEBLListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[AEBLWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                BLSearchPart searchPart = this.SmartParts.AddNew<BLSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[AEBLWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);


                BLFastSearchPart fastSearchPart = this.SmartParts.AddNew<BLFastSearchPart>();
                IWorkspace fastSearchPartWorkspace = (IWorkspace)this.Workspaces[AEBLWorkSpaceConstants.FastSearchWorkspace];
                fastSearchPartWorkspace.Show(fastSearchPart);


                ICP.FCM.Common.UI.Memolist.MemoListPart memoListPart = this.Items.AddNew<ICP.FCM.Common.UI.Memolist.MemoListPart>();
                IWorkspace memoListWorkspace = (IWorkspace)this.Workspaces[AEBLWorkSpaceConstants.MemoListWorkspace];
                memoListWorkspace.Show(memoListPart);

                UCCommunicationHistory faxMailEDIListPart = this.Items.AddNew<UCCommunicationHistory>();
                IWorkspace faxMailListWorkspace = (IWorkspace)this.Workspaces[AEBLWorkSpaceConstants.FaxMailEDIListWorkspace];
                faxMailListWorkspace.Show(faxMailEDIListPart);


                UCDocumentList documentPart = this.Items.AddNew<UCDocumentList>();
                IWorkspace documentPartWorkspace = (IWorkspace)this.Workspaces[AEBLWorkSpaceConstants.DocumentListWorkspace];
                documentPartWorkspace.Show(documentPart);

                #endregion

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Air BL" : "空运出口提单";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                AEBLUIAdapter blAdapter = new AEBLUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(fastSearchPart.GetType().Name, fastSearchPart);
                dic.Add(memoListPart.GetType().Name, memoListPart);
                dic.Add(faxMailEDIListPart.GetType().Name, faxMailEDIListPart);
                dic.Add(documentPart.GetType().Name, documentPart);

                blAdapter.Init(dic, documentPresenter);

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


    public class AEBLColorConstant
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
    public class AEBLCommandConstants
    {
        /// <summary>
        /// 新增MBL
        /// </summary>
        public const string Command_AddMBL = "Command_AddMAWB";
        /// <summary>
        /// 新增HBL
        /// </summary>
        public const string Command_AddHBL = "Command_AddHAWB";
        /// <summary>
        /// 复制提单
        /// </summary>
        public const string Command_CopyData = "Command_CopyData";
        /// <summary>
        /// 编辑提单
        /// </summary>
        public const string Command_EditData = "Command_EditData";
        /// <summary>
        /// 删除提单
        /// </summary>
        public const string Command_DeleteData = "Command_DeleteData";
        /// <summary>
        /// 开始对单
        /// </summary>
        public const string Command_Check = "Command_Check";
        /// <summary>
        /// 完成对单
        /// </summary>
        public const string Command_CompleteCheck = "Command_CompleteCheck";
        /// <summary>
        /// 打印提单
        /// </summary>
        public const string Command_PrintBL = "Command_PrintBL";
        /// <summary>
        /// 打印装箱单
        /// </summary>
        public const string Command_PrintLoadCtn = "Command_PrintLoadCtn";
        /// <summary>
        /// 打印装货单
        /// </summary>
        public const string Command_PrintLoadGoods = "Command_PrintLoadGoods";

        /// <summary>
        /// 申请代理
        /// </summary>
        public const string Command_ReplyAgent = "Command_ReplyAgent";
        /// <summary>
        /// E-MBL
        /// </summary>
        public const string Command_E_MBL = "Command_E_MBL";
        /// <summary>
        /// ISF
        /// </summary>
        public const string Command_ISF = "Command_ISF";

        /// <summary>
        /// 帐单
        /// </summary>
        public const string Command_Bill = "Command_Bill";

        /// <summary>
        /// 核销单
        /// </summary>
        public const string Command_VerifiSheet = "Command_VerifiSheet";

        /// <summary>
        /// 显示搜索面板
        /// </summary>
        public const string Command_ShowSearch = "Command_ShowSearch";

        /// <summary>
        /// 刷新
        /// </summary>
        public const string Command_RefreshData = "Command_RefreshData";

        ///// <summary>
        ///// 分单
        ///// </summary>
        //public const string Command_SplitBL = "Command_SplitBL";
        ///// <summary>
        ///// 合单
        ///// </summary>
        //public const string Command_Merge = "Command_Merge";

        /// <summary>
        /// 确认放单
        /// </summary>
        public const string Command_ConfirmReleaseBL = "Command_ConfirmReleaseBL";
        /// <summary>
        /// 查看模式-MBL
        /// </summary>
        public const string Command_VisibleMBL = "Command_VisibleMBL";
        /// <summary>
        /// 查看模式-HBL
        /// </summary>
        public const string Command_VisibleHBL = "Command_VisibleHBL";
        /// <summary>
        /// 查看模式-全部
        /// </summary>
        public const string Command_VisibleALL = "Command_VisibleALL";
    }

    /// <summary>
    /// WorkSpace常量
    /// </summary>
    public class AEBLWorkSpaceConstants
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

    public class AEBLUIAdapter
    {

        #region parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        ISearchPart _fastSearchPart;
        AEBLListPart _mainListPart;
        IListPart _memolistPart;
        UCCommunicationHistory _faxMailEDIListPart;
        UCDocumentList _DocumentListPart;
        #endregion

        #region interface and RefreshBarEnabled

        public void Init(Dictionary<string, object> controls, DocumentListPresenter documentPresenter)
        {

            _toolBar = (IToolBar)controls[typeof(AEBLToolBar).Name];
            _searchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(BLSearchPart).Name];
            _mainListPart = (AEBLListPart)controls[typeof(AEBLListPart).Name];
            _fastSearchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(ICP.FCM.AirExport.UI.BL.BLFastSearchPart).Name];
            _memolistPart = (IListPart)controls[typeof(ICP.FCM.Common.UI.Memolist.MemoListPart).Name];
            _faxMailEDIListPart = (UCCommunicationHistory)controls[typeof(UCCommunicationHistory).Name];
            _DocumentListPart = (UCDocumentList)controls[typeof(UCDocumentList).Name];

            RefreshBarEnabled(_toolBar, null);

            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                AirBLList listData = data as AirBLList;
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add("ParentList", data);

                if (listData != null)
                {
                    MemoParam para = new MemoParam();
                    para.OperationId = listData.ID;
                    para.FormID = listData.ID;
                    if (listData.BLType == BLType.MAWB)
                    {
                        para.FormType = ICP.Framework.CommonLibrary.Common.FormType.MBL;
                    }
                    else
                    {
                        para.FormType = ICP.Framework.CommonLibrary.Common.FormType.HBL;
                    }

                    para.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.AirExport;
                    _memolistPart.DataSource = para;
                    //设置沟通历史记录数据源
                    ICP.FCM.Common.UI.Utility.SetCommunicationDataSource(_faxMailEDIListPart, para);
                    //设置文档中心数据源
                    ICP.FCM.Common.UI.Utility.SetDocumentListDataSource(_DocumentListPart, documentPresenter, para);
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

            //_fastSearchPart.RaiseSearched();
        }

        private void RefreshBarEnabled(IToolBar toolBar, AirBLList listData)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barCopy", false);
                toolBar.SetEnable("barEdit", false);
                toolBar.SetEnable("barDelete", false);
                toolBar.SetEnable("barReleaseBL", false);

                toolBar.SetEnable("barSubCheck", false);

                toolBar.SetEnable("barPrint", false);
                toolBar.SetEnable("barPrintBL", false);
                toolBar.SetEnable("barPrintLoadCtn", false);
                toolBar.SetEnable("barPrintLoadGoods", false);
                toolBar.SetEnable("barReplyAgent", false);
                toolBar.SetEnable("barE_MBL", false);
                toolBar.SetEnable("barISF", false);
                toolBar.SetEnable("barBill", false);
                toolBar.SetEnable("barVerifiSheet", false);

                toolBar.SetEnable("barDocument", false);
                toolBar.SetEnable("barFaxEmail", false);
                toolBar.SetEnable("barMemo", false);
            }
            else
            {

                toolBar.SetEnable("barEdit", true);

                toolBar.SetEnable("barDocument", true);
                toolBar.SetEnable("barFaxEmail", true);
                toolBar.SetEnable("barMemo", true);

                //if (listData.IsValid == false)
                //{
                //    toolBar.SetEnable("barCopy", false);
                //    toolBar.SetEnable("barPrintBL", false);
                //    toolBar.SetEnable("barE_MBL", false);
                //    toolBar.SetEnable("barISF", false);
                //    toolBar.SetEnable("barBill", false);
                //    toolBar.SetEnable("barDelete", false);
                //    toolBar.SetEnable("barPrint", false);

                //    toolBar.SetEnable("barSubCheck", false);
                //    toolBar.SetEnable("barReplyAgent", false);
                //}
                //else
                //{
                toolBar.SetEnable("barCopy", true);
                toolBar.SetEnable("barPrintBL", true);
                toolBar.SetEnable("barE_MBL", true);
                toolBar.SetEnable("barISF", true);
                toolBar.SetEnable("barBill", true);
                toolBar.SetEnable("barVerifiSheet", true);
                toolBar.SetEnable("barPrint", true);

                toolBar.SetEnable("barDelete", false);
                toolBar.SetEnable("barSubCheck", false);
                toolBar.SetEnable("barReplyAgent", false);

                toolBar.SetEnable("barReleaseBL", false);

                #region
                //没有HBL和状态是草稿,可以删除
                if (listData.HBLCount <= 0 && listData.State == AEBLState.Draft) toolBar.SetEnable("barDelete", true);
                ////有箱可以打印
                //if (string.IsNullOrEmpty(listData.ContainerNos) == false) toolBar.SetEnable("barPrint", true);

                //MBL可以申请代理
                if (listData.BLType == BLType.MAWB) toolBar.SetEnable("barReplyAgent", true);

                if (listData.State != AEBLState.Release && listData.State != AEBLState.Checked
                   && listData.HBLCount <= 0)
                {
                    toolBar.SetEnable("barSubCheck", true);
                }

                if (listData.State == AEBLState.Checked) toolBar.SetEnable("barReleaseBL", true);

                #endregion
                //}
            }
        }

        #endregion
    }
}
