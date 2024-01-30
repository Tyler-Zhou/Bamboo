using DevExpress.Utils;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.ClientComponents.UIManagement;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ICP.Common.UI.Configure.TerminalLogins
{
    public class TerminalLoginsLayoutUIProxyLogic : TerminalLoginsLayoutUIProxy
    {
        public override List<LayoutElement> Elements
        {
            get
            {
                List<LayoutElement> elements = base.Elements;
                (elements[1] as UIProxyElement).ProxyType = typeof(TerminalLoginsUIProxyLogic);
                (elements[2] as UIProxyElement).ProxyType = typeof(TerminalLoginsEditUIProxyLogic);
                return base.Elements;
            }
        }
    }

    public partial class TerminalLoginsUIProxyLogic : TerminalLoginsUIProxy
    {
        #region service


        //static ITerminalService terminalService = ServiceProxyFactory.Create<ITerminalService>("TerminalService");


        IDataHoster _dataHoster;
        [ServiceDependency]
        public IDataHoster hoster
        {
            set
            {
                value.AddToolAction("Disuse", delegate(object obj)
                {
                    if (obj == null) return false;
                    //else return ((ICP.Crawler.ServiceInterface.DataObjects.TerminalLogins)obj).IsValid == false;
                    else return ((ICP.Crawler.ServiceInterface.DataObjects.TerminalLogins)obj).UserID == string.Empty;

                }, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "Available(&D)" : "激活(&D)"));

                //value.AddToolAction("Disuse", delegate(object obj)
                //{
                //    if (obj == null) return false;
                //    else return ((ICP.Framework.CommonLibrary.Common.BaseDataObject)obj).IsNew;

                //}, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "&Delete" : "删除(&D)"));


                value.AddToolAction("Disuse", delegate(object obj)
                {
                    //if (obj == null) return true;
                    //else return ((CurrencyList)obj).ID == Guid.Empty;
                    return obj == null;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));
                //DataPresenceStyle s = new DataPresenceStyle();
                //s.Condition = delegate(object data) { return ((ICP.Crawler.ServiceInterface.DataObjects.TerminalLogins)data).IsValid == false; };
                //s.Style = DataPresenceType.Disabled;
                //value.AddDataPresenceStyle(s);

                DataPresenceStyle s1 = new DataPresenceStyle();
                s1.Condition = delegate(object data) { return ((ICP.Crawler.ServiceInterface.DataObjects.TerminalLogins)data).UserID == string.Empty; };
                s1.Style = DataPresenceType.NewRow;
                value.AddDataPresenceStyle(s1);

                _dataHoster = value;
            }
            get
            {
                return _dataHoster;
            }
        }

        #endregion

        #region Method

        protected override bool AddData(object obj)
        {
            ICP.Crawler.ServiceInterface.DataObjects.TerminalLogins newData = new ICP.Crawler.ServiceInterface.DataObjects.TerminalLogins();

            //newData.UpdateBy = LocalData.UserInfo.LoginID;
            //newData.CreateByName = LocalData.UserInfo.LoginName;
            //newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified); newData.IsDirty = false;
            //newData.IsValid = true;
            workitem.Services.Get<IDataHoster>().RefreshData(newData);

            return true;
        }
        protected override bool DisuseData(object obj)
        {
            ICP.Crawler.ServiceInterface.DataObjects.TerminalLogins currentRow = obj as ICP.Crawler.ServiceInterface.DataObjects.TerminalLogins;
            if (currentRow == null)
            {
                return false;
            }

            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                //if (currentRow.IsNew == false)
                //{
                //    SingleResultData result = ConfigureService.ChangeCurrencyState(currentRow.ID, !currentRow.IsValid, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
                //    currentRow.IsValid = !currentRow.IsValid;
                //    currentRow.UpdateDate = result.UpdateDate;
                //    _dataHoster.RefreshData(currentRow);
                //    _dataHoster.RaiseCurrentChanged(currentRow);

                //}
                //else
                //{
                //    _dataHoster.RemoveData(currentRow);
                //}
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(form, LocalData.IsEnglish ? "Change State Successfully" : "更改状态成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(form, ex);
                return false;
            }
        }

        #endregion
    }

    public class TerminalLoginsEditUIProxyLogic : TerminalLoginsEditUIProxy
    {
        static string oldPassword = string.Empty;
        #region 服务注入

        IDataHoster _dataHoster;
        [ServiceDependency]
        public IDataHoster dataHoster
        {
            set
            {
                value.AddToolAction("Save", delegate(object obj)
                {
                    if (obj == null) return true;
                    else return ((ICP.Crawler.ServiceInterface.DataObjects.TerminalLogins)obj).UserID == string.Empty;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));

                _dataHoster = value;
            }
            get { return _dataHoster; }
        }
        private ITerminalService TerminalService
        {
            get
            {
                return ServiceClient.GetService<ITerminalService>();
            }
        }
        private ICrawlerService CrawlerService
        {
            get
            {
                return ServiceClient.GetService<ICrawlerService>();
            }
        }
        //static ICrawlerService crawlerService = ICP.Crawler.CommonLibrary.ServiceProxyFactory.Create<ICrawlerService>("CrawlerService");
        //static ITerminalService terminalService = ServiceProxyFactory.Create<ITerminalService>("TerminalService");

        #endregion

        protected override bool SaveData(object data)
        {
            string searching = LocalData.IsEnglish ? "Saving data" : "正在保存数据";
            //int threadID = ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm(searching);
            WaitDialogForm waitForm = new WaitDialogForm(searching);
            ICP.Crawler.ServiceInterface.DataObjects.TerminalLogins currentData = _dataHoster.ContentPart.DataSource as ICP.Crawler.ServiceInterface.DataObjects.TerminalLogins;
            if (currentData == null) return false;
            if (currentData.Validate() == false)
            {
                return false;
            }

            Control control = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {

                //SingleResultData result = ConfigureService.SaveCurrencyInfo(currentData.ID,
                //                                                            currentData.Code,
                //                                                            currentData.CName,
                //                                                            currentData.EName,
                //                                                            currentData.CountryID,
                //                                                            LocalData.UserInfo.LoginID,
                //                                                            currentData.UpdateDate);
                //currentData.CancelEdit();
                //currentData.ID = result.ID;
                //currentData.UpdateDate = result.UpdateDate;
                //currentData.BeginEdit();
                ICP.Crawler.ServiceInterface.DataObjects.Terminals terminal =
                    TerminalService.GetTerminalList(ICP.Crawler.CommonLibrary.Enum.SearchUsing.ChangePwdConfig).FirstOrDefault(o => 
                        o.LoginID == currentData.ID);
                if (terminal != null)
                {
                    ////terminal.NewPassword = currentData.Password;
                    ////terminal.Password = oldPassword;
                    ////CrawlerService.StartCrawler(terminal, Crawler.CommonLibrary.Enum.CrawlerMode.Quick);
                    TerminalService.SaveTerminalLogins(currentData.ID, currentData.Code, currentData.UserID, currentData.Password, LocalData.UserInfo.LoginID);
                    currentData.CancelEdit();
                    _dataHoster.RefreshData(currentData);
                    currentData.BeginEdit();
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(control, LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                    return true;
                }
                else
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(control, LocalData.IsEnglish ? "Save failed" : "保存失败");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(control, ex);
                return false;
            }
            finally
            {
                waitForm.Close();
                //ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(threadID);
            }
        }
        protected override bool BeforeParentChanged(object obj)
        {
            //ICP.Crawler.ServiceInterface.DataObjects.TerminalLogins info = obj as ICP.Crawler.ServiceInterface.DataObjects.TerminalLogins;
            //if (info != null)
            //    oldPassword = info.Password;
            return CommonUtility.BeforeParentChanged(obj, _dataHoster, SaveData);
        }
        protected override bool ParentChanged(object obj)
        {
            ICP.Crawler.ServiceInterface.DataObjects.TerminalLogins info = obj as ICP.Crawler.ServiceInterface.DataObjects.TerminalLogins;
            if (info != null)
                oldPassword = info.Password;
            //ICP.Crawler.ServiceInterface.DataObjects.TerminalLogins info = null;
            //if (list != null && list.ID != Guid.Empty)
            //{
            //    info = terminalService.GetTerminalLogins(list.ID);
            //}
            //else
            //{
            //    info = list as ICP.Crawler.ServiceInterface.DataObjects.TerminalLogins;
            //}

            _dataHoster.ContentPart.DataSource = info;
            _dataHoster.ExecuteToolAction("Save", info);
            return true;
        }
    }
}
