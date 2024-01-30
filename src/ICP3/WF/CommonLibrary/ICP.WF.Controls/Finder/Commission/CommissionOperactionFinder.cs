using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI.Commands;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.WinForms;
using ICP.WF.ServiceInterface;
using System.Linq;

namespace ICP.WF.Controls.Form.Commission
{
    public class CommissionOperactionFinder : IDataFinder, IDisposable
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

        WFBusinessListPart _listPart;
        WFBusinessSearchPart _searchPart;
        WFBusinessToolBar _toolBar;
        WFSelectBusinessListPart _selectListPart;
        WFCommissionLogListPart _logListPart;

        private const string CommissionOperactionFinderWorkspace = "CommissionOperactionFinderWorkspace";
        #endregion

        #region

        private void Show(string searchValue, string property
                            , SearchConditionCollection conditions
                            , FinderTriggerType triggerType
                            , GetExistValueHandler getExistValueHandler
                            , System.Windows.Forms.Control container)
        {
            DeckWorkspace workspce = this.Workitem.Workspaces.Get<DeckWorkspace>(CommissionOperactionFinderWorkspace);
            if (workspce == null || workspce.IsDisposed)
            {
                if (this.Workitem.Workspaces.Contains(CommissionOperactionFinderWorkspace))
                {
                    this.Workitem.Workspaces.Remove(workspce);
                }

                workspce = this.Workitem.Workspaces.AddNew<DeckWorkspace>(CommissionOperactionFinderWorkspace);
                workspce.Dock = System.Windows.Forms.DockStyle.Fill;
                workspce.BringToFront();
            }
            container.Controls.Clear();
            container.Controls.Add(workspce);
            Show(searchValue, property, conditions, triggerType, getExistValueHandler, CommissionOperactionFinderWorkspace);
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
            WorkItem tempWorkitem = Workitem.WorkItems.Get<WorkItem>(CommissionOperactionFinderConstants.COMMISSIONOPERACTIONFINDERITEM);
            if (tempWorkitem == null)
            {
                tempWorkitem = Workitem.WorkItems.AddNew<WorkItem>(CommissionOperactionFinderConstants.COMMISSIONOPERACTIONFINDERITEM);
                #region Show

                WFBusinessMainWorkspace mainSpce = tempWorkitem.SmartParts.Get<WFBusinessMainWorkspace>("WFBusinessMainWorkspace");
                if (mainSpce == null)
                {
                    mainSpce = tempWorkitem.SmartParts.AddNew<WFBusinessMainWorkspace>("WFBusinessMainWorkspace");

                    #region AddPart

                    WFBusinessToolBar toolBar = tempWorkitem.SmartParts.AddNew<WFBusinessToolBar>();
                    IWorkspace toolBarWorkspace = (IWorkspace)tempWorkitem.Workspaces[WFBusinessWorkSpaceConstants.ToolBarWorkspace];
                    toolBarWorkspace.Show(toolBar);

                    WFBusinessListPart listPart = tempWorkitem.SmartParts.AddNew<WFBusinessListPart>();
                    IWorkspace listWorkspace = (IWorkspace)tempWorkitem.Workspaces[WFBusinessWorkSpaceConstants.ListWorkspace];
                    listWorkspace.Show(listPart);

                    WFBusinessSearchPart searchPart = tempWorkitem.SmartParts.AddNew<WFBusinessSearchPart>();
                    IWorkspace searchWorkspace = (IWorkspace)tempWorkitem.Workspaces[WFBusinessWorkSpaceConstants.SearchWorkspace];
                    searchWorkspace.Show(searchPart);


                    WFSelectBusinessListPart selectListPart = tempWorkitem.SmartParts.AddNew<WFSelectBusinessListPart>();
                    IWorkspace selectListWorkspace = (IWorkspace)tempWorkitem.Workspaces[WFBusinessWorkSpaceConstants.SelectListWorkspace];
                    selectListWorkspace.Show(selectListPart);


                    WFCommissionLogListPart logListPart = tempWorkitem.SmartParts.AddNew<WFCommissionLogListPart>();
                    IWorkspace logListWorkspace = (IWorkspace)tempWorkitem.Workspaces[WFBusinessWorkSpaceConstants.CommissionLogWorkspace];
                    logListWorkspace.Show(logListPart);

                    #endregion

                    IWorkspace mainWorkspace = tempWorkitem.Workspaces[workspaceName];
                    SmartPartInfo smartPartInfo = new SmartPartInfo();
                    smartPartInfo.Title = LocalData.IsEnglish ? "Commission Finder" : "选择退佣业务";
                    mainWorkspace.Show(mainSpce, smartPartInfo);

                    #region
                    mainSpce.Disposed += delegate
                    {
                        tempWorkitem.Dispose();
                    };

                    _listPart = listPart;
                    _searchPart = searchPart;
                    _toolBar = toolBar;
                    _selectListPart = selectListPart;
                    _logListPart = logListPart;


                    #region 查询
                    _searchPart.OnSearched += this.OnSearchPartSearched;
                    #endregion

                    #region 主表选择
                    _listPart.Selected += this.OnListPartSelected;
                    #endregion

                    #region 主表换行
                    _listPart.CurrentChanged += OnListPartCurrentChanged;
                    #endregion

                    #region 分页
                    _listPart.InvokeGetData += OnListPartInvokeGetData;
                    #endregion

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
                        if (conditions.Contain("CustomerName"))
                        {
                            values.Add("CustomerName", conditions.GetValue("CustomerName").Value);
                        }
                        if (conditions.Contain("SelectIDs"))
                        {
                            values.Add("SelectIDs", conditions.GetValue("SelectIDs").Value);
                        }
                        if (conditions.Contain("SelectNos"))
                        {
                            values.Add("SelectNos", conditions.GetValue("SelectNos").Value);
                        }
                    }
                    _searchPart.Init(values);
                    _selectListPart.Init(values);
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

            #region  BulidList

            #endregion

        }
        private void OnSearchPartSearched(object sender, object results)
        {
            _listPart.DataSource = results;
        }
        private void OnListPartSelected(object sender, object data)
        {
            _selectListPart.DataSource = data;
        }
        private void OnListPartInvokeGetData(object sender, object data)
        {
            _searchPart.RaiseSearched(data);
        }
        private void OnListPartCurrentChanged(object sender, object data)
        {
            _logListPart.DataSource = data;
        }




        #endregion

        #region Workitem Common
        [CommandHandler(CommissionOperactionFinderConstants.Command_Commission_FinderConfirm)]
        public void Command_FinderConfirm(object sender, EventArgs e)
        {
            if (DataChoosed != null)
            {
                List<WFBusinessList> list = _selectListPart.DataList as List<WFBusinessList>;
                if (list != null)
                {
                    List<Guid> operationIDList = (from d in list select d.ID).ToList();
                    List<WFCommissionLogList> logList = WorkFlowExtendService.GetCommissionLogList(operationIDList.ToArray(), LocalData.IsEnglish);
                    if (logList != null && logList.Count > 0)
                    {
                        string omessage = LocalData.IsEnglish ? "Select list of business, including the Commission record has been retired,Whether to continue?" : "选择的业务列表中，包含了已退佣的纪录,是否继续?";
                        omessage = omessage + System.Environment.NewLine;
                        foreach (WFCommissionLogList logitem in logList)
                        {
                            string strInfo = logitem.OperactioNo + ":  ApplyName:[" + logitem.CreateName + "] ApplyDate:[" + logitem.CreateDate.ToShortDateString() + "] WorkFlowNo:[" + logitem.WorkFlowNo + "] ";
                            omessage = omessage + strInfo + System.Environment.NewLine;
                        }

                        DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(omessage,
                          LocalData.IsEnglish ? "Tip" : "提示",
                          MessageBoxButtons.YesNo,
                          MessageBoxIcon.Question);
                        if (result != DialogResult.Yes)
                        {
                            return;
                        }
                    }


                    DataChoosed(this, new DataFindEventArgs(list.ToArray()));
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
            if (this._listPart != null)
            {
                _listPart.Selected -= this.OnListPartSelected;

                _listPart.CurrentChanged -= OnListPartCurrentChanged;

                _listPart.InvokeGetData -= OnListPartInvokeGetData;
                _listPart = null;
                
            }
            if (this._searchPart != null)
            {
                _searchPart.OnSearched -= this.OnSearchPartSearched;
                _searchPart = null;
            }
            this._toolBar = null;
            this._selectListPart = null;
            this._logListPart = null;
           

        }

        #endregion
    }

    public class CommissionOperactionFinderConstants
    {
        public const string COMMISSIONOPERACTIONFINDERITEM = "COMMISSIONOPERACTIONFINDERITEM";

        public const string Command_Commission_FinderConfirm = "Command_Commission_FinderConfirm";

    }
}
