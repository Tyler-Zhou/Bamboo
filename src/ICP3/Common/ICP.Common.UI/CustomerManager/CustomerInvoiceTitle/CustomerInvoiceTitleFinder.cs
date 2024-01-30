using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using System.Windows.Forms;
using System;
using ICP.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI.WinForms;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.Common.UI.CustomerManager
{
    public class CustomerInvoiceTitleFinder : IDataFinder,IDisposable
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        #endregion

        #region 缓存的控件

        TaxCustomerInvoiceTitleListPart _listPart;
        InvoiceTitleSearchPart _searchPart;
        string[] ReturnFields;

        private const string CustomerInvoiceTitleFinderWorkspace = "CustomerInvoiceTitleFinder";
        #endregion

        #region 显示界面
        
        private void Show(string searchValue, string property
                            , SearchConditionCollection conditions
                            , FinderTriggerType triggerType
                            , GetExistValueHandler getExistValueHandler
                            , System.Windows.Forms.Control container)
        {
            DeckWorkspace workspce = this.Workitem.Workspaces.Get<DeckWorkspace>(CustomerInvoiceTitleFinderWorkspace);
            if (workspce == null || workspce.IsDisposed)
            {
                if (this.Workitem.Workspaces.Contains(CustomerInvoiceTitleFinderWorkspace))
                {
                    this.Workitem.Workspaces.Remove(workspce);
                }

                workspce = this.Workitem.Workspaces.AddNew<DeckWorkspace>(CustomerInvoiceTitleFinderWorkspace);
                workspce.Dock = System.Windows.Forms.DockStyle.Fill;
                workspce.BringToFront();
            }
            container.Controls.Clear();
            container.Controls.Add(workspce);
            Show(searchValue, property, conditions, triggerType, getExistValueHandler, CustomerInvoiceTitleFinderWorkspace);
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
                form.Text = LocalData.IsEnglish ? "Find Invoice Title" : "选择发票抬头";
                Show(searchValue, property, conditions, triggerType, getExistValueHandler, form);
            }
            else
            {
                BulidFinderByWorkspaceName(searchValue, property, conditions, triggerType, workspaceName);
            }


        }

        private void BulidFinderByWorkspaceName(string searchValue, string property, SearchConditionCollection conditions, FinderTriggerType triggerType, string workspaceName)
        {
            WorkItem tempWorkitem = Workitem.WorkItems.Get<WorkItem>(CustomerInvoiceTitleFinderConstants.CUSTOMERINVOICETITLEFINDERITEM);
            if (tempWorkitem == null)
            {
                tempWorkitem = Workitem.WorkItems.AddNew<WorkItem>(CustomerInvoiceTitleFinderConstants.CUSTOMERINVOICETITLEFINDERITEM);
                #region Show

                FindInvoiceTitleMainWorkSpace mainSpce = tempWorkitem.SmartParts.Get<FindInvoiceTitleMainWorkSpace>("FindInvoiceTitleMainWorkSpace");
                if (mainSpce == null)
                {
                    mainSpce = tempWorkitem.SmartParts.AddNew<FindInvoiceTitleMainWorkSpace>("FindInvoiceTitleMainWorkSpace");

                    #region AddPart


                    TaxCustomerInvoiceTitleListPart listPart = tempWorkitem.SmartParts.AddNew<TaxCustomerInvoiceTitleListPart>();
                    IWorkspace listWorkspace = (IWorkspace)tempWorkitem.Workspaces[FindInvoiceTitleWorkSpaceConstants.FindInvoiceTitleListWorkspace];
                    listWorkspace.Show(listPart);

                    InvoiceTitleSearchPart searchPart = tempWorkitem.SmartParts.AddNew<InvoiceTitleSearchPart>();
                    IWorkspace searchWorkspace = (IWorkspace)tempWorkitem.Workspaces[FindInvoiceTitleWorkSpaceConstants.FindInvoiceTitleSearchWorkspace];
                    searchWorkspace.Show(searchPart);

                    #endregion

                    IWorkspace mainWorkspace = tempWorkitem.Workspaces[workspaceName];
                    SmartPartInfo smartPartInfo = new SmartPartInfo();
                    smartPartInfo.Title = LocalData.IsEnglish ? "Invoce Tiel Finder" : "选择发票抬头";
                    mainWorkspace.Show(mainSpce, smartPartInfo);

                    #region
                    mainSpce.Disposed += delegate
                    {
                        tempWorkitem.Dispose();
                    };

                    _listPart = listPart;
                    _searchPart = searchPart;


                    #region 查询
                    _searchPart.OnSearched += delegate(object sender, object results)
                    {
                        _listPart.DataSource = results;
                    };
                    #endregion


                    Dictionary<string, object> values = new Dictionary<string, object>();
                    if (conditions != null)
                    {
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

        #endregion

        #region Workitem Common
        [CommandHandler(CustomerInvoiceTitleFinderConstants.Command_CustomerInvoiceTitle_FinderConfirm)]
        public void Command_FinderConfirm(object sender, EventArgs e)
        {
            if (DataChoosed != null)
            {
                CustomerInvoiceTitleInfo item = _listPart.CurrentRow as CustomerInvoiceTitleInfo;
                if (item != null)
                {
                   object[] returnItemList=  CommonUtility.GetSingleSearchResult<CustomerInvoiceTitleInfo>(item, ReturnFields);

                   DataChoosed(this, new DataFindEventArgs(returnItemList));
                }
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
            if (string.IsNullOrEmpty(workspaceName))
                workspaceName = ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace;

            IWorkspace workspace = this.Workitem.Workspaces[workspaceName];

            string name = string.Empty;
            if (triggerType == FinderTriggerType.KeyEnter)
            {
                name = searchValue;
            }
            List<CustomerInvoiceTitleInfo> list;
            if (triggerType == FinderTriggerType.ClickButton && string.IsNullOrEmpty(name))
            {
                //单击选择按钮时，如果没有输入查询时，则不进行搜索。只弹出查询对话框。
                list = new List<CustomerInvoiceTitleInfo>();
            }
            else
            {
                list = CustomerService.GetCustomerInvoiceTitleListForFinder(string.Empty, name, LocalData.UserInfo.DefaultCompanyName);
                //list = CustomerService.GetCustomerInvoiceTitleListForFinder(string.Empty, name, LocalData.UserInfo.DefaultCompanyID.ToString());
            }

            if (list != null && list.Count == 1)
            {
                if (DataChoosed != null)
                    DataChoosed(this, new DataFindEventArgs(CommonUtility.GetSingleSearchResult<CustomerInvoiceTitleInfo>(list[0], returnFields)));

                return;
            }

            ReturnFields = returnFields;

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
            
            this._listPart = null;
            this._searchPart = null;
            this.DataChoosed = null;
            this.ReturnFields = null;
            this.Workitem = null;
        }

        #endregion
    }

    public class CustomerInvoiceTitleFinderConstants
    {
        public const string CUSTOMERINVOICETITLEFINDERITEM = "CUSTOMERINVOICETITLEFINDERITEM";

        public const string Command_CustomerInvoiceTitle_FinderConfirm = "Command_CustomerInvoiceTitle_FinderConfirm";

    }

    /// <summary>
    /// WorkSpace常量
    /// </summary>
    public class FindInvoiceTitleWorkSpaceConstants
    {
        public const string FindInvoiceTitleSearchWorkspace = "FindInvoiceTitleSearchWorkspace";
        public const string FindInvoiceTitleListWorkspace = "FindInvoiceTitleListWorkspace";

    }





}
