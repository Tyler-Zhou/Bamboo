using System;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.FAM.ServiceInterface.DataObjects;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.WinForms;

namespace ICP.FAM.UI.Bill.Finder
{
    public class BillFinder : IDataFinder, IDisposable
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region 缓存的控件

        IToolBar _toolBar;
        ISearchPart _searchPart;
        BaseListPart _mainListPart;
        BillOperationListPart _selectedListPart;
        List<Guid> _selectedData;
        bool _isMulti = false;
        private const string BillFinderWorkspace = "BillFinderWorkspace";
        #endregion

        #region

        private void Show(string searchValue, string property
                            , SearchConditionCollection conditions
                            , FinderTriggerType triggerType
                            , GetExistValueHandler getExistValueHandler
                            , Control container)
        {
            DeckWorkspace workspce = Workitem.Workspaces.Get<DeckWorkspace>(BillFinderWorkspace);
            if (workspce == null || workspce.IsDisposed)
            {
                if (Workitem.Workspaces.Contains(BillFinderWorkspace))
                {
                    Workitem.Workspaces.Remove(workspce);
                }

                workspce = Workitem.Workspaces.AddNew<DeckWorkspace>(BillFinderWorkspace);
                workspce.Dock = DockStyle.Fill;
                workspce.BringToFront();
            }
            container.Controls.Clear();
            container.Controls.Add(workspce);
            Show(searchValue, property, conditions, triggerType, getExistValueHandler, BillFinderWorkspace);
        }

        private void Show(string searchValue, string property
                            , SearchConditionCollection conditions
                            , FinderTriggerType triggerType
                            , GetExistValueHandler getExistValueHandler
                            , string workspaceName)
        {

            if (string.IsNullOrEmpty(workspaceName))
            {
                XtraForm form = new XtraForm();
                form.Height = 600;
                form.Width = 800;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Text = LocalData.IsEnglish ? "Bill Finder" : "选择帐单";
                Show(searchValue, property, conditions, triggerType, getExistValueHandler, form);
            }
            else
            {
                BulidFinderByWorkspaceName(searchValue, property, conditions, triggerType, workspaceName);
            }


        }

        private void BulidFinderByWorkspaceName(string searchValue, string property, SearchConditionCollection conditions, FinderTriggerType triggerType, string workspaceName)
        {
            WorkItem tempWorkitem = Workitem.WorkItems.Get<WorkItem>(BillFinderConstants.BILLFINDERTEMPWORKITEM);
            if (tempWorkitem == null)
            {
                tempWorkitem = Workitem.WorkItems.AddNew<WorkItem>(BillFinderConstants.BILLFINDERTEMPWORKITEM);
            }
            #region Show
            BillFinderWorkspace mainSpce = tempWorkitem.SmartParts.Get<BillFinderWorkspace>("BillFinderWorkspace");
            if (mainSpce == null)
            {
                mainSpce = tempWorkitem.SmartParts.AddNew<BillFinderWorkspace>("BillFinderWorkspace");

                #region AddPart

                BillFinderToolBar toolBar = tempWorkitem.SmartParts.AddNew<BillFinderToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)tempWorkitem.Workspaces[BillWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                BillFinderListPart listPart = tempWorkitem.SmartParts.AddNew<BillFinderListPart>();
                IWorkspace listWorkspace = (IWorkspace)tempWorkitem.Workspaces[BillWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                BillFinderSearchPart searchPart = tempWorkitem.SmartParts.AddNew<BillFinderSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)tempWorkitem.Workspaces[BillWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);

                BillOperationListPart selectedPart = tempWorkitem.SmartParts.AddNew<BillOperationListPart>();
                IWorkspace sselectedWorkspace = (IWorkspace)tempWorkitem.Workspaces[BillWorkSpaceConstants.OperationListWorkspace];
                sselectedWorkspace.Show(selectedPart);


                #endregion

                #region
                mainSpce.Disposed += delegate
                {
                    tempWorkitem.Dispose();
                };

                _toolBar = toolBar;
                _searchPart = searchPart;
                _selectedListPart = selectedPart;
                _mainListPart = listPart;

                _searchPart.OnSearched += OnSearchPartSearched;

                listPart.Selected += OnListPartSelected;


                #region 分页
                _mainListPart.InvokeGetData += OnMainListPartInvokeGetData;
                #endregion

                #endregion

                IWorkspace mainWorkspace = tempWorkitem.Workspaces[workspaceName];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Bill Finder" : "选择帐单";
                mainWorkspace.Show(mainSpce, smartPartInfo);


            }
            else
            {
                tempWorkitem.Workspaces[ClientConstants.MainWorkspace].Activate(mainSpce);
            }
            #endregion
            //}
            //else
            //{
            //    tempWorkitem.Activate();
            //}

            #region  BulidList
            Dictionary<string, object> values = new Dictionary<string, object>();
            if (conditions != null)
            {
                if (conditions.Contain("CustomerID"))
                {
                    values.Add("CustomerID", conditions.GetValue("CustomerID").Value);
                }
                if (conditions.Contain("CompanyID"))
                {
                    values.Add("CompanyID", conditions.GetValue("CompanyID").Value);
                }
                if (conditions.Contain("IsValidateCustomer"))
                {
                    values.Add("IsValidateCustomer", conditions.GetValue("IsValidateCustomer").Value);
                }
                if (conditions.Contain("IsValidateCompany"))
                {
                    values.Add("IsValidateCompany", conditions.GetValue("IsValidateCompany").Value);
                }
                if (conditions.Contain("CustomerName"))
                {
                    values.Add("CustomerName", conditions.GetValue("CustomerName").Value);
                }
                if (conditions.Contain("IsInvoiceSearch"))
                {
                    values.Add("IsInvoiceSearch", conditions.GetValue("IsInvoiceSearch").Value);
                }

                //if (conditions.Contain("BillProgram"))
                //{
                //    values.Add("BillProgram", conditions.GetValue("BillProgram").Value);
                //}
            }
            values.Add("IsMulti", _isMulti);
            _mainListPart.Init(values);
            #endregion

            _searchPart.InitialValues(searchValue, property, conditions, triggerType);
            //_searchPart.RaiseSearched();
        }
        private void OnSearchPartSearched(object sender, object results)
        {
            _mainListPart.DataSource = results;
        }
        private void OnListPartSelected(object sender, object data)
        {
            _selectedListPart.DataSource = data;
        }
        void listPart_Selected(object sender, object data)
        {

        }
        private void OnMainListPartInvokeGetData(object sender, object data)
        {
            _searchPart.RaiseSearched(data);
        }
        #endregion

        #region Workitem Common
        [CommandHandler(BillCommandConstants.Command_FinderConfirm)]
        public void Command_FinderConfirm(object sender, EventArgs e)
        {
            if (DataChoosed != null)
            {
                if (_isMulti)
                {
                    List<CurrencyBillList> list = _selectedListPart.DataSource as List<CurrencyBillList>;
                    if (list != null)
                    {
                        DataChoosed(this, new DataFindEventArgs(list.ToArray()));
                        _selectedListPart.FindForm().Close();
                    }
                }
                else
                {
                    CurrencyBillList data = _mainListPart.DataSource as CurrencyBillList;

                    if (data != null)
                    {
                        DataChoosed(this, new DataFindEventArgs(new object[] { data }));
                        _mainListPart.FindForm().Close();
                    }
                }


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
                            , Control container)
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
                            , Control container)
        {
            _isMulti = true;
            Show(searchValue, property, conditions, triggerType, null, container);
        }

        public void PickMany(string searchValue, string property
                            , SearchConditionCollection conditions
                            , string[] returnFields, FinderTriggerType triggerType
                            , GetExistValueHandler getExistValueHandler
                            , string workspaceName)
        {
            _isMulti = true;
            Show(searchValue, property, conditions, triggerType, null, workspaceName);
        }

        #endregion

        #endregion


        #region IDisposable 成员

        public void Dispose()
        {
            DataChoosed = null;
            if (_mainListPart != null)
            {
                _mainListPart.InvokeGetData -= OnMainListPartInvokeGetData;
                _mainListPart.Selected -= OnListPartSelected;


                _mainListPart = null;
               

            }
            if (_searchPart != null)
            {
                _searchPart.OnSearched -= OnSearchPartSearched;
                _searchPart = null;
            }
            _selectedData = null;
            if (_selectedListPart != null)
            {

                _selectedListPart = null;
            }
            _toolBar = null;

            

            


            
           
            
        }

        #endregion
    }

    public class BillFinderConstants
    {
        public const string BILLFINDERTEMPWORKITEM = "BILLFINDERTEMPWORKITEM";

    }
}
