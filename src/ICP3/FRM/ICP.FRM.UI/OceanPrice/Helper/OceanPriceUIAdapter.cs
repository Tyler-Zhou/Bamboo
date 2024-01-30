using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using System.Windows.Forms;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.Service;

namespace ICP.FRM.UI.OceanPrice
{
    /// <summary>
    /// 合约UI适配器
    /// </summary>
    public class OceanPriceUIAdapter:IDisposable
    {
        #region Service
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 海运运价管理服务
        /// </summary>
        public IOceanPriceService OceanPriceService
        {
            get
            {
                return ServiceClient.GetService<IOceanPriceService>();
            }
        }

        #endregion

        #region init

        OPMainWorkspace _OPMainWorkspace;
        OPToolBar _OPToolBar;
        OPSearchPart _OPSearchPart;
        OPContractListPart _OPContractListPart;
        OPContractEditPart _OPContractEditPart;
        OPBasePortRatesPart _OPBasePortRatesPart;
        OPArbitraryRatesPart _OPArbitraryRatesPart;
        OPAdditionalFeePart _OPAdditionalFeePart;
        OPPermissionsPart _OPPermissionsPart;
        OPAttachmentPart _OPAttachmentPart;

        /// <summary>
        /// 初始化面板
        /// </summary>
        /// <param name="oPMainWorkspace">主面板</param>
        /// <param name="opToolBar">工具栏</param>
        /// <param name="opSearchPart">查询面板</param>
        /// <param name="opContractListPart">合约列表</param>
        /// <param name="opContractEditPart">合约编辑界面</param>
        /// <param name="opBasePortRatesPart">主线价格</param>
        /// <param name="opArbitraryRatesPart">支线价格</param>
        /// <param name="opAdditionalFeePart">附加费</param>
        /// <param name="opPermissionsPart">合约权限界面</param>
        /// <param name="opAttachmentPart">合约附件</param>
        public void InitPart(OPMainWorkspace oPMainWorkspace,
                                    OPToolBar opToolBar,
                                    OPSearchPart opSearchPart,
                                    OPContractListPart opContractListPart,
                                    OPContractEditPart opContractEditPart,
                                    OPBasePortRatesPart opBasePortRatesPart,
                                    OPArbitraryRatesPart opArbitraryRatesPart,
                                    OPAdditionalFeePart opAdditionalFeePart,
                                    OPPermissionsPart opPermissionsPart,
                                    OPAttachmentPart opAttachmentPart)
        {

            _OPMainWorkspace = oPMainWorkspace;
            _OPToolBar = opToolBar;
            _OPSearchPart = opSearchPart;
            _OPContractListPart = opContractListPart;
            _OPContractEditPart = opContractEditPart;
            _OPBasePortRatesPart = opBasePortRatesPart;
            _OPArbitraryRatesPart = opArbitraryRatesPart;
            _OPAdditionalFeePart = opAdditionalFeePart;
            _OPPermissionsPart = opPermissionsPart;
            _OPAttachmentPart = opAttachmentPart;

            _OPContractEditPart.Enabled = false;

            BulidConnection();
        }
        /// <summary>
        /// 构建事件连接
        /// </summary>
        private void BulidConnection()
        {
            #region Connection

            _OPSearchPart.BeForeSearchData += (sender, e) => { e.Cancel = ListChanging(); };

            #region CurrentChanging
            _OPContractListPart.CurrentChanging += (sender, e) => { e.Cancel = ListChanging(); };
            #endregion

            #region CurrentChanging

            _OPMainWorkspace.PageChanging += (sender, e) => { e.Cancel = PageChanging(); };

            #endregion

            #region CurrentChanged
            _OPContractListPart.CurrentChanged += (sender, data) => ListChanged(data);
            #endregion

            #region OnSearched
            _OPSearchPart.OnSearched += (sender, results) => { _OPContractListPart.DataSource = results; };
            #endregion

            #region  _editPart.Saved
            _OPContractEditPart.Saved += EditPartSaved;
            #endregion

            #endregion

            _OPAdditionalFeePart.SetGetIsChangedMothed(BastRatesIsChanged);
        }
        /// <summary>
        /// 运价是否改变
        /// </summary>
        /// <returns></returns>
        bool BastRatesIsChanged()
        {
            bool isChanged = _OPBasePortRatesPart.IsChanged ||
                             _OPArbitraryRatesPart.IsChanged;
            return isChanged;
        }
       
        #endregion

        #region Changing
        /// <summary>
        /// 页面改变
        /// </summary>
        /// <returns></returns>
        bool PageChanging()
        {
            OceanInfo info = _OPContractEditPart.DataSource as OceanInfo;

            if (info == null) return false;

            #region IsNew
            if (info.IsNew)
            {
                DialogResult result = XtraMessageBox.Show(
                NativeLanguageService.GetText(_OPMainWorkspace, "CurrentChanging")
                 , "Tip"
                 , MessageBoxButtons.YesNoCancel
                 , MessageBoxIcon.Question);

                if (result == DialogResult.Cancel) return true;
                if (result == DialogResult.No) { _OPContractListPart.RemoveItem(_OPContractListPart.Current); return false; }
                bool isSaveSrcc = true;

                if (_OPContractEditPart.SaveData() == false) isSaveSrcc = false;

                if (isSaveSrcc) return false;
                return true;
            }
            #endregion IsNew
            if (info.State == OceanState.Published)
            {
                return false;
            }
            #region IsDirty
            if (info.IsDirty)
            {
                DialogResult result = XtraMessageBox.Show(
                    NativeLanguageService.GetText(_OPMainWorkspace, "CurrentChanging")
                    , "Tip"
                    , MessageBoxButtons.YesNoCancel
                    , MessageBoxIcon.Question);

                if (result == DialogResult.Cancel) return true;
                if (result == DialogResult.No)
                {

                    OceanList listData = _OPContractListPart.Current as OceanList;
                    if (listData != null)
                    {
                        OceanInfo newInfo = OceanPriceService.GetOceanInfo(listData.ID);
                        _OPContractEditPart.DataSource = newInfo;
                    }

                    return false;
                }
                bool isSaveSrcc = _OPContractEditPart.SaveData();

                if (isSaveSrcc) return false;
                return true;
            }

            #endregion

            return false;
        }

        /// <summary>
        /// 列表行改变中
        /// </summary>
        /// <returns>返回是否取消换行</returns>
        private bool ListChanging()
        {
            List<OceanList> oceanList = _OPContractListPart.DataSource as List<OceanList>;

            if (oceanList == null || oceanList.Count ==0) return false;

            OceanInfo info = _OPContractEditPart.DataSource as OceanInfo;
            if (info == null) return false;


            if (info.IsNew || info.IsDirty)
            {
                return PageChanging();
            }
            if (info.State == OceanState.Published)
            {
                return false;
            }
            bool isChanged = false;
            if (_OPBasePortRatesPart.IsChanged ||
                _OPArbitraryRatesPart.IsChanged ||
                _OPAdditionalFeePart.IsChanged ||
                _OPPermissionsPart.IsModeChanged ||
                _OPPermissionsPart.IsPermissionsChanged) isChanged = true;

            if (isChanged)
            {
                DialogResult result = XtraMessageBox.Show(
                    NativeLanguageService.GetText(_OPMainWorkspace, "CurrentChanging")
                    , "Tip"
                    , MessageBoxButtons.YesNoCancel
                    , MessageBoxIcon.Question);

                if (result == DialogResult.Cancel) return true;
                if (result == DialogResult.No) return false;
                bool isSaveSrcc = SaveData();

                if (isSaveSrcc) return false;
                return true;
            }

            return false;
        }

        #endregion

        #region Changed

        private void ListChanged(object data)
        {
            int theradID = 0;
            OceanInfo info =null;
            if (data is OceanInfo)
            {
                info = data as OceanInfo;
            }
            else
            {
                OceanList listData = data as OceanList;
                if (listData != null && listData.IsNew ==false)
                {
                    info = OceanPriceService.GetOceanInfo(listData.ID);
                }
            }
            try { theradID = LoadingServce.ShowLoadingForm("Loading..."); }
            catch { }

            if (info != null && info.IsNew)
            {
                Workitem.Commands[OPCommonConstants.Command_MoveToContract].Execute();
            }

            _OPToolBar.DataSource = info;
            _OPContractEditPart.DataSource = info;
            Dictionary<string, object> keyValue = new Dictionary<string, object> { { "ParentList", info } };
            _OPBasePortRatesPart.Init(keyValue);
            _OPArbitraryRatesPart.Init(keyValue);
            _OPAdditionalFeePart.Init(keyValue);
            _OPPermissionsPart.Init(keyValue);
            _OPAttachmentPart.Init(keyValue);

            try { LoadingServce.CloseLoadingForm(theradID); }
            catch { }
        }

        #endregion

        #region EditPartSaved
        /// <summary>
        /// 保存合约后执行事件
        /// </summary>
        /// <param name="prams"></param>
        void EditPartSaved(object[] prams)
        {
            if (_OPContractListPart.Current == null || prams == null) return;

            OceanInfo info = prams[0] as OceanInfo;
            if (info == null) return;
            OceanList currentRow = _OPContractListPart.Current as OceanList;
            if (currentRow == null) return;
            OceanList c = OceanPriceService.GetOceanListByIds(new[] { info.ID }, LocalData.UserInfo.LoginID)[0];
            Utility.CopyToValue(c, currentRow, typeof(OceanList));
            currentRow.OceanUnits = Utility.Clone(c.OceanUnits);

            _OPToolBar.DataSource = c;
            Dictionary<string, object> keyValue = new Dictionary<string, object> {{"ParentList", currentRow}};
            //BasePortRates
            _OPBasePortRatesPart.Init(keyValue);
            //Arbitrary
            _OPArbitraryRatesPart.Init(keyValue);
            //AdditionalFee
            _OPAdditionalFeePart.Init(keyValue);
            //_OPPermissionsPart
            _OPPermissionsPart.Init(keyValue);
            //_OPAttachmentPart
            _OPAttachmentPart.Init(keyValue);
        }

        #endregion

        #region 保存
        /// <summary>
        /// 保存数据
        /// </summary>
        [CommandHandler(OPCommonConstants.Command_SaveData)]
        public void Command_SaveData(object sender, EventArgs e)
        {
            SaveData();
        }
        
        #endregion

        #region 发布
        /// <summary>
        /// 发布运价
        /// </summary>
        [CommandHandler(OPCommonConstants.Command_PublishPauseData)]
        public void Command_PublishPauseData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OceanList _parentList = _OPContractListPart.Current as OceanList;

                if (_parentList == null || Utility.GuidIsNullOrEmpty(_parentList.ID)) return;

                OceanState rstate = OceanState.Draft;
                rstate = _parentList.State == OceanState.Draft ? OceanState.Published : OceanState.Draft;
                string message = string.Empty;
                if (rstate == OceanState.Published)
                {
                    message = "Are you sure you have input special Per Diem & Chassis Free Time in T/T and will Publish the selected contract?";
                }
                else
                {
                    string.Format(NativeLanguageService.GetText(_OPContractListPart, "UpdateSelectedContract"), NativeLanguageService.GetText(_OPContractListPart, "Pause"));
                }
                if (Utility.EnquireYesOrNo(message) == false) return;


                if (GetPartsIsChanged())
                {
                    //如果有数据更改了,先保存，再发布
                    if(!SaveData())
                    {
                        return;
                    }
                }

                try
                {
                    SingleResultData result = OceanPriceService.ChangeOceanState(_parentList.ID, rstate, LocalData.UserInfo.LoginID, _parentList.UpdateDate);

                    _parentList.State = rstate;
                    _parentList.PublishDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    _parentList.PublisherName = LocalData.UserInfo.LoginName;
                    _parentList.UpdateDate = result.UpdateDate;

                    _OPContractListPart.Refresh(new List<OceanList> { _parentList });
                    _OPContractListPart.bsMainList_PositionChanged(null,null);
                   
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(_OPContractListPart.FindForm(), NativeLanguageService.GetText(_OPContractListPart, "SaveSuccessfully"));
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(_OPContractListPart.FindForm(), ex); }
            }
        }
        #endregion

        #region Method
        /// <summary>
        /// 保存合约信息
        /// </summary>
        /// <returns></returns>
        private bool SaveData()
        {
            if (ValidateData() == false) return false;

            #region IsChanged

            if (GetPartsIsChanged() == false) return true;

            #endregion

            try
            {
                OceanList _parentList = _OPContractListPart.Current as OceanList;
                if (_parentList == null) return false;
                bool isCharge = false;
                bool isCopyBasePort = false;

                #region BasePort
                List<ClientBasePortList> clientBasePortItems = null;
                BasePortCollect basePortCollect = null;
                isCopyBasePort = _OPBasePortRatesPart.IsCopyBasePort;
                if (_OPBasePortRatesPart.IsChanged)
                {
                    clientBasePortItems = _OPBasePortRatesPart.GetChangedItem();
                    basePortCollect = new BasePortCollect
                    {
                        BasePortItem =
                            OceanPriceTransformHelper.TransformC2S(clientBasePortItems, _parentList.OceanUnits)
                    };
                    foreach (var item in basePortCollect.BasePortItem.Where(item => item.Comm != null))
                    {
                        item.Comm = item.Comm.Replace(GlobalConstants.ShowDividedSymbol, GlobalConstants.DividedSymbol);
                    }

                    isCharge = true;
                }
                #endregion

                #region Arbitrary

                List<ClientArbitraryList> clientArbitraryItems = null;
                ArbitraryCollect arbitraryCollect = null;
                if (_OPArbitraryRatesPart.IsChanged)
                {
                    clientArbitraryItems = _OPArbitraryRatesPart.GetChangedItem();
                    arbitraryCollect = new ArbitraryCollect
                    {
                        ArbitraryItem =
                            OceanPriceTransformHelper.TransformC2S(clientArbitraryItems, _parentList.OceanUnits)
                    };

                    isCharge = true;
                }
                #endregion

                #region AdditionalFee

                List<ClientAdditionalFeeList> clientAdditionalFeeItmes = null;
                AdditionalFeeCollect additionalFeeCollect = null;
                if (_OPAdditionalFeePart.IsChanged)
                {
                    clientAdditionalFeeItmes = _OPAdditionalFeePart.GetChangedItem();
                    additionalFeeCollect = new AdditionalFeeCollect
                    {
                        AdditionalFees =
                            OceanPriceTransformHelper.TransformC2S(clientAdditionalFeeItmes, _parentList.OceanUnits)
                    };

                    isCharge = true;
                }
                #endregion

                #region PermissionMode

                PermissionsModeCollect permissionsModeCollect = null;
                if (_OPPermissionsPart.IsModeChanged)
                {
                    permissionsModeCollect = _OPPermissionsPart.GetPermissionsModeCollect();
                }

                #endregion

                #region Permissions

                List<OceanPermissionList> permissions = _OPPermissionsPart.DataSource as List<OceanPermissionList>;
                PermissionsCollect permissionsCollect = null;
                if (_OPPermissionsPart.IsPermissionsChanged)
                {
                    permissionsCollect = new PermissionsCollect
                    {
                        permissionIds = new List<Guid?>(),
                        organizationIds = new List<Guid?>(),
                        userObjectIDs = new List<Guid?>(),
                        types = new List<UserObjectType?>(),
                        permissions = new List<OceanPermission>(),
                        updateDates = new List<DateTime?>()
                    };

                    for (int i = 0; i < permissions.Count; i++)
                    {
                        permissionsCollect.permissionIds.Add(permissions[i].ID);
                        permissionsCollect.organizationIds.Add(permissions[i].Type == UserObjectType.User? null : permissions[i].OrganizationID);
                        permissionsCollect.userObjectIDs.Add(permissions[i].Type == UserObjectType.User? permissions[i].UserID : permissions[i].JobID);

                        permissionsCollect.types.Add(permissions[i].Type);
                        permissionsCollect.permissions.Add(permissions[i].Permission);
                        permissionsCollect.updateDates.Add(permissions[i].UpdateDate);
                    }
                }


                #endregion

                #region 保存

                int theradID = 0;
                OceanSavedResult result = null;
                try
                {
                    theradID = LoadingServce.ShowLoadingForm("Saveing......");
                    result = OceanPriceService.SaveOceanDatas
                        (
                            _parentList.ID
                            , basePortCollect
                            , isCopyBasePort
                            , arbitraryCollect
                            , additionalFeeCollect
                            , permissionsModeCollect
                            , permissionsCollect
                            , LocalData.UserInfo.LoginID
                        );
                }
                catch (Exception sex)
                {
                    throw new Exception(string.Format("Saveing exception:{0}", sex.Message));
                }
                finally
                {
                    LoadingServce.CloseLoadingForm(theradID);
                }
                #endregion

                #region 生成运价

                try
                {
                    theradID = LoadingServce.ShowLoadingForm("Builer......");
                    OceanPriceService.BuilerBaseItemForOceanID(_parentList.ID);
                }
                catch (Exception bex)
                {
                    throw new Exception(string.Format("Builer exception:{0}", bex.Message));
                }
                finally
                {
                    LoadingServce.CloseLoadingForm(theradID);
                }

                #endregion

                if (result != null)
                {
                    #region BasePort
                    if (result.BasePortResult != null)
                    {
                        for (int i = 0; i < result.BasePortResult.Items.Count; i++)
                        {
                            clientBasePortItems[i].ID = result.BasePortResult.Items[i].GetValue<Guid>("ID");
                            clientBasePortItems[i].UpdateDate = result.BasePortResult.Items[i].GetValue<DateTime?>("UpdateDate");
                            clientBasePortItems[i].No = result.BasePortResult.Items[i].GetValue<int>("No");
                            clientBasePortItems[i].IsDirty = false;
                        }
                        _OPBasePortRatesPart.RefreshUIData();
                    }

                    #endregion

                    #region Arbitrary

                    if (result.ArbitraryResult != null)
                    {
                        for (int i = 0; i < result.ArbitraryResult.Items.Count; i++)
                        {
                            clientArbitraryItems[i].ID = result.ArbitraryResult.Items[i].GetValue<Guid>("ID");
                            clientArbitraryItems[i].UpdateDate = result.ArbitraryResult.Items[i].GetValue<DateTime?>("UpdateDate");
                            clientArbitraryItems[i].No = result.ArbitraryResult.Items[i].GetValue<int>("No");
                            clientArbitraryItems[i].IsDirty = false;
                        }
                        _OPArbitraryRatesPart.RefreshUIData();
                    }
                    #endregion

                    #region AdditionalFee

                    if (result.AdditionalFeeResult != null)
                    {
                        for (int i = 0; i < result.AdditionalFeeResult.Items.Count; i++)
                        {
                            clientAdditionalFeeItmes[i].ID = result.AdditionalFeeResult.Items[i].GetValue<Guid>("ID");
                            clientAdditionalFeeItmes[i].UpdateDate = result.AdditionalFeeResult.Items[i].GetValue<DateTime?>("UpdateDate");
                            clientAdditionalFeeItmes[i].IsDirty = false;
                        }
                        _OPAdditionalFeePart.RefreshUIData();
                    }
                    #endregion

                    #region PermissionMode

                    if (result.PermissionsModeResult != null)
                    {
                        _OPContractListPart.RefreshUpdate();
                    }

                    #endregion

                    #region Permissions

                    if (result.PermissionsResult != null)
                    {
                        for (int i = 0; i < result.PermissionsResult.Items.Count; i++)
                        {
                            permissions[i].ID = result.PermissionsResult.Items[i].GetValue<Guid>("ID");
                            permissions[i].UpdateDate = result.PermissionsResult.Items[i].GetValue<DateTime?>("UpdateDate");
                            permissions[i].IsDirty = false;
                        }
                        _OPPermissionsPart.RefreshUIData();
                    }

                    #endregion
                }
                

                #region 如果合约的状态是Published,更新了合约信息后，启用各个子面板的Pause按钮

                if (isCharge && _parentList.State == OceanState.Published)
                {
                    //更新各个按钮的状态
                    _OPBasePortRatesPart.SetPublish();
                    _OPArbitraryRatesPart.SetPublish();
                    _OPAdditionalFeePart.SetPublish();
                    _OPPermissionsPart.SetPublish();
                    _OPAttachmentPart.SetPublish();
                    _OPToolBar.SetPublish();
                    _OPContractEditPart.SetPublish();

                    //更新主表中的状态
                    _parentList.State = OceanState.Draft;

                    List<OceanList> list = new List<OceanList>();
                    list.Add(_parentList);
                    _OPContractListPart.Refresh(list);

                }

                #endregion


                #region 保存Copy BasePort的关联
                if (isCopyBasePort)
                {
                    try
                    {
                        BasePortCopyIDToList copyInfo = _OPBasePortRatesPart.GetCopyIDList();

                        OceanPriceService.SaveOceanItem2AdditionalFeeByBasePort(copyInfo.IDList.ToArray(), copyInfo.CopyIDList.ToArray(), LocalData.UserInfo.LoginID, LocalData.IsEnglish);

                        _OPBasePortRatesPart.ClearCopyID();
                    }
                    catch (Exception ex)
                    {
                        _OPMainWorkspace.ShowException(ex);
                        return false;
                    }
                }
                #endregion


                LocalCommonServices.Statusbar.SetStatusBarPanel(NativeLanguageService.GetText(_OPMainWorkspace, "SaveSuccessfully"));
                return true;
            }
            catch (Exception ex)
            {
                _OPMainWorkspace.ShowException(ex);
                return false;
            }
        }
        /// <summary>
        /// 各面板数据是否全部保存
        /// </summary>
        /// <returns></returns>
        private bool GetPartsIsChanged()
        {
            bool isChanged = _OPBasePortRatesPart.IsChanged ||
                             _OPArbitraryRatesPart.IsChanged ||
                             _OPAdditionalFeePart.IsChanged ||
                             _OPPermissionsPart.IsModeChanged ||
                             _OPPermissionsPart.IsPermissionsChanged;
            return isChanged;
        }
        /// <summary>
        /// 验证各面板数据
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            bool isSrcc = true;
            if (_OPBasePortRatesPart.ValidateData() == false) isSrcc = false;

            if (_OPArbitraryRatesPart.ValidateData() == false) isSrcc = false;

            if (_OPAdditionalFeePart.ValidateData() == false) isSrcc = false;

            if (_OPPermissionsPart.ValidateData() == false) isSrcc = false;

            return isSrcc;
        } 
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            _OPAdditionalFeePart = null;
            _OPArbitraryRatesPart = null;
            _OPAttachmentPart = null;
            _OPBasePortRatesPart = null;
            _OPContractEditPart = null;
            _OPContractListPart = null;
            _OPMainWorkspace = null;
            _OPPermissionsPart = null;
            _OPSearchPart = null;
            _OPToolBar = null;
            if (Workitem != null)
            {
                Workitem.Items.Remove(this);


                Workitem = null;
            }
         
        }

        #endregion
    }
}
