using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Commands;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.WinForms;
using ICP.WF.ServiceInterface;

namespace ICP.WF.Controls.Form.CustomerExpense
{
    public class CustomerExpenseFinder : IDataFinder, IDisposable
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }



        public IWorkFlowExtendService WorkFlowExtendService
        {
            get
            {
                return ServiceClient.GetService<IWorkFlowExtendService>();
            }
        }

        #endregion

        #region 缓存的控件

        WFCRMCustomerListPart _listPart;
        WFCustomerExpenseSearchPart _searchPart;
        WFCustomerExpenseToolBar _toolBar;
        WFCustomerExpenseTouchLogListPart _touchListPart;
        WFCustomerExpenseLogListPart _wflogListPart;

        private const string WFCustomerExpenseFinderWorkspace = "WFCustomerExpenseFinderWorkspace";
        #endregion

        #region

        private void Show(string searchValue, string property
                            , SearchConditionCollection conditions
                            , FinderTriggerType triggerType
                            , GetExistValueHandler getExistValueHandler
                            , System.Windows.Forms.Control container)
        {
            DeckWorkspace workspce = this.Workitem.Workspaces.Get<DeckWorkspace>(WFCustomerExpenseFinderWorkspace);
            if (workspce == null || workspce.IsDisposed)
            {
                if (this.Workitem.Workspaces.Contains(WFCustomerExpenseFinderWorkspace))
                {
                    this.Workitem.Workspaces.Remove(workspce);
                }

                workspce = this.Workitem.Workspaces.AddNew<DeckWorkspace>(WFCustomerExpenseFinderWorkspace);
                workspce.Dock = System.Windows.Forms.DockStyle.Fill;
                workspce.BringToFront();
            }
            container.Controls.Clear();
            container.Controls.Add(workspce);
            Show(searchValue, property, conditions, triggerType, getExistValueHandler, WFCustomerExpenseFinderWorkspace);
        }

        private void Show(string searchValue, string property
                            , SearchConditionCollection conditions
                            , FinderTriggerType triggerType
                            , GetExistValueHandler getExistValueHandler
                            , string workspaceName)
        {

            if (string.IsNullOrEmpty(workspaceName))
            {
                DevExpress.XtraEditors.XtraForm form = new DevExpress.XtraEditors.XtraForm();
                form.Height = 600;
                form.Width = 800;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Text = LocalData.IsEnglish ? "Business Finder" : "选择退佣业务";
                Show(searchValue, property, conditions, triggerType, getExistValueHandler, form);
            }
            else
            {
                BulidFinderByWorkspaceName(searchValue, property, conditions, triggerType, workspaceName);
            }


        }

        private void BulidFinderByWorkspaceName(string searchValue, string property, SearchConditionCollection conditions, FinderTriggerType triggerType, string workspaceName)
        {
            WorkItem tempWorkitem = Workitem.WorkItems.Get<WorkItem>(CustomerExpenseFinderConstants.CUSTOMEREXPENSEOPERACTIONFINDERITEM);
            if (tempWorkitem == null)
            {
                tempWorkitem = Workitem.WorkItems.AddNew<WorkItem>(CustomerExpenseFinderConstants.CUSTOMEREXPENSEOPERACTIONFINDERITEM);
                #region Show

                WFCustomerExpenseMainWorkspace mainSpce = tempWorkitem.SmartParts.Get<WFCustomerExpenseMainWorkspace>("WFCustomerExpenseMainWorkspace");
                if (mainSpce == null)
                {
                    mainSpce = tempWorkitem.SmartParts.AddNew<WFCustomerExpenseMainWorkspace>("WFCustomerExpenseMainWorkspace");

                    #region AddPart

                    WFCustomerExpenseToolBar toolBar = tempWorkitem.SmartParts.AddNew<WFCustomerExpenseToolBar>();
                    IWorkspace toolBarWorkspace = (IWorkspace)tempWorkitem.Workspaces[WFCustomerExpenseSpaceConstants.ToolBarWorkspace];
                    toolBarWorkspace.Show(toolBar);

                    WFCRMCustomerListPart listPart = tempWorkitem.SmartParts.AddNew<WFCRMCustomerListPart>();
                    IWorkspace listWorkspace = (IWorkspace)tempWorkitem.Workspaces[WFCustomerExpenseSpaceConstants.ListWorkspace];
                    listWorkspace.Show(listPart);

                    WFCustomerExpenseSearchPart searchPart = tempWorkitem.SmartParts.AddNew<WFCustomerExpenseSearchPart>();
                    IWorkspace searchWorkspace = (IWorkspace)tempWorkitem.Workspaces[WFCustomerExpenseSpaceConstants.SearchWorkspace];
                    searchWorkspace.Show(searchPart);


                    WFCustomerExpenseTouchLogListPart touchListPart = tempWorkitem.SmartParts.AddNew<WFCustomerExpenseTouchLogListPart>();
                    IWorkspace selectListWorkspace = (IWorkspace)tempWorkitem.Workspaces[WFCustomerExpenseSpaceConstants.TouchListWorkspace];
                    selectListWorkspace.Show(touchListPart);


                    WFCustomerExpenseLogListPart logListPart = tempWorkitem.SmartParts.AddNew<WFCustomerExpenseLogListPart>();
                    IWorkspace logListWorkspace = (IWorkspace)tempWorkitem.Workspaces[WFCustomerExpenseSpaceConstants.CommissionLogWorkspace];
                    logListWorkspace.Show(logListPart);

                    #endregion

                    IWorkspace mainWorkspace = tempWorkitem.Workspaces[workspaceName];
                    SmartPartInfo smartPartInfo = new SmartPartInfo();
                    smartPartInfo.Title = LocalData.IsEnglish ? "Customer Touch Finder" : "选择跟进纪录";
                    mainWorkspace.Show(mainSpce, smartPartInfo);

                    #region
                    mainSpce.Disposed += delegate
                    {
                        tempWorkitem.Dispose();
                    };

                    _listPart = listPart;
                    _searchPart = searchPart;
                    _toolBar = toolBar;
                    _touchListPart = touchListPart;
                    _wflogListPart = logListPart;


                    #region 查询
                    _searchPart.OnSearched += this.OnSearchPartSearched;
                    #endregion

                    #region 主表换行
                    _listPart.CurrentChanged += OnListPartCurrentChanged;

                    #endregion

                    #region 跟进纪录换
                    touchListPart.CurrentChanged += OnTouchListPartCurrentChanged;
                    #endregion



                    Dictionary<string, object> values = new Dictionary<string, object>();
                    if (conditions != null)
                    {
                        if (conditions.Contain("CustomerID"))
                        {
                            values.Add("CustomerID", conditions.GetValue("CustomerID").Value);
                        }
                        if (conditions.Contain("CustomerName"))
                        {
                            values.Add("CustomerName", conditions.GetValue("CustomerName").Value);
                        }
                    }
                    _searchPart.Init(values);
                    _searchPart.RaiseSearched();
                    #endregion
                }
                else
                {
                    tempWorkitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(mainSpce);
                }
                #endregion
            }
            else
            {
                tempWorkitem.Activate();
            }
        }
        private void OnSearchPartSearched(object sender, object results)
        {
            _listPart.DataSource = results;
        }
        private void OnListPartCurrentChanged(object sender, object data)
        {
            _touchListPart.DataSource = data;
        }
        private void OnTouchListPartCurrentChanged(object sender, object data)
        {
            _wflogListPart.DataSource = data;
        }
        #endregion

        #region Workitem Common
        [CommandHandler(CustomerExpenseFinderConstants.Command_CustomerExpense_FinderConfirm)]
        public void Command_FinderConfirm(object sender, EventArgs e)
        {
            if (DataChoosed != null)
            {
                WFCECRMCustomerTouchLogList item = _touchListPart.CurrentRow as WFCECRMCustomerTouchLogList;
                WFCECRMCustomerList customerInfo = _listPart.CurrentRow;
                if (item != null && customerInfo != null)
                {
                    List<WFCustomerExpenseLogList> logList = WorkFlowExtendService.GetWFCustomerExpenseLogList(new Guid[1] { item.ID }, LocalData.IsEnglish);
                    if (logList != null && logList.Count > 0)
                    {
                        //string omessage = LocalData.IsEnglish ? "Current of Customer Touch , including the Apply record has been retired:" : "选择的客户跟进纪录，包含了已申请业务报销的纪录:";
                        //omessage = omessage + System.Environment.NewLine;
                        //foreach (WFCustomerExpenseLogList logitem in logList)
                        //{
                        //    string strInfo = "ApplyName:[" + logitem.CreateBy + "] ApplyDate:[" + logitem.CreateDate.ToShortDateString() + "] WorkFlowNo:[" + logitem.WorkNo + "] WorkName:[" + logitem.WorkName + "]";
                        //    omessage = omessage + strInfo + System.Environment.NewLine;
                        //}

                        //DevExpress.XtraEditors.XtraMessageBox.Show(omessage);

                        //return;
                        item.ID = new Guid();
                        item.Content = string.Empty;
                    }
                }
                else if (customerInfo == null)
                {
                    string omessage = LocalData.IsEnglish ? "Please select a customer" : "请选择一个客户";
                    DevExpress.XtraEditors.XtraMessageBox.Show(omessage, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    item = new WFCECRMCustomerTouchLogList();
                    item.ID = new Guid();
                    item.Content = string.Empty;
                }

                item.CustomerID = customerInfo.ID;
                item.CustomerName = LocalData.IsEnglish ? customerInfo.EName : customerInfo.CName;

                DataChoosed(this, new DataFindEventArgs(new object[1] { item }));

                _listPart.FindForm().Close();
            }
        }

        #endregion

        #region IDataFinder 成员

        public event EventHandler<DataFindEventArgs> DataChoosed;

        #region One

        public void PickOne(string searchValue, string property
                            , SearchConditionCollection conditions
                            , string[] returnFields
                            , FinderTriggerType triggerType
                            , System.Windows.Forms.Control container)
        {
            Show(searchValue, property, conditions, triggerType, null, container);
        }

        public void PickOne(string searchValue, string property
                            , SearchConditionCollection conditions
                            , string[] returnFields
                            , FinderTriggerType triggerType
                            , string workspaceName)
        {
            Show(searchValue, property, conditions, triggerType, null, workspaceName);
        }

        #endregion

        #region Many

        public void PickMany(string searchValue, string property
                            , SearchConditionCollection conditions
                            , string[] returnFields
                            , FinderTriggerType triggerType
                            , GetExistValueHandler getExistValueHandler
                            , System.Windows.Forms.Control container)
        {
            Show(searchValue, property, conditions, triggerType, null, container);
        }

        public void PickMany(string searchValue, string property
                            , SearchConditionCollection conditions
                            , string[] returnFields, FinderTriggerType triggerType
                            , GetExistValueHandler getExistValueHandler
                            , string workspaceName)
        {
            Show(searchValue, property, conditions, triggerType, null, workspaceName);
        }

        #endregion

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            this.DataChoosed = null;
            if (this._searchPart != null)
            {
                _searchPart.OnSearched -= this.OnSearchPartSearched;
                _searchPart = null;
            }
            if (this._listPart != null)
            {
                _listPart.CurrentChanged -= OnListPartCurrentChanged;
                this._listPart = null;
            }
            if (this._touchListPart != null)
            {
                _touchListPart.CurrentChanged -= OnTouchListPartCurrentChanged;
                this._touchListPart = null;
            }
            this._toolBar = null;
            this._wflogListPart = null;

        }

        #endregion
    }

    public class CustomerExpenseFinderConstants
    {
        public const string CUSTOMEREXPENSEOPERACTIONFINDERITEM = "CUSTOMEREXPENSEOPERACTIONFINDERITEM";

        public const string Command_CustomerExpense_FinderConfirm = "Command_FinderConfirm";

    }


    public class WFCustomerExpenseSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string TouchListWorkspace = "TouchListWorkspace";
        public const string CommissionLogWorkspace = "CommissionLogWorkspace";
    }
}
