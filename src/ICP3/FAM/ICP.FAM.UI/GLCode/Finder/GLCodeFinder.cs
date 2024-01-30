using System;
using System.Collections.Generic;
using ICP.FAM.UI.Comm;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI.WinForms;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Common.ServiceInterface.DataObjects;
using System.Collections;

namespace ICP.FAM.UI.GLCode.Finder
{
    public class GLCodeFinder:IDataFinder,IDisposable
    {
        #region 常量变量属性

        private const string GLCodeFinderWorkspace = "GLCodeFinderWorkspace";

        public bool IsBusy { get; set; }

        GLCodeFinderWorkitem finderWorkitem = null;
        GLCodeMultiFinderWorkitem multiFinderWorkitem = null;

        Guid solutionID = Guid.Empty;
        List<Guid> companyIds = new List<Guid>();
        bool? isDepartmentCheck = null;
        bool? isPersonalCheck = null;
        bool? isCustomerCheck = null;
        bool? isJournal = null;
        bool? isBankAccount = null;
        bool? isFee = null;
        bool? onlyLeaf = null;

        #endregion

        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        #endregion

        #region IDataFinder 成员

        public event EventHandler<DataFindEventArgs> DataChoosed;

        private void SetCondition(SearchConditionCollection conditions)
        {
            #region Get Condition

            if (conditions != null)
            {
                if (conditions.Contain("SolutionID"))
                {
                    SearchCondition solutionIDCondition = conditions.GetValue("SolutionID");
                    if (solutionIDCondition != null && solutionIDCondition.Value != null)
                        solutionID = new Guid(solutionIDCondition.Value.ToString());
                }   
                if (conditions.Contain("CompanyIDs"))
                {
                    SearchCondition solutionIDCondition = conditions.GetValue("CompanyIDs");
                    if (solutionIDCondition != null && solutionIDCondition.Value != null)
                        companyIds = solutionIDCondition.Value as List<Guid>;
                    if (companyIds == null)
                    {
                        companyIds = new List<Guid>();
                    }
                }
                if (conditions.Contain("IsDepartmentCheck"))
                {
                    SearchCondition sCondition = conditions.GetValue("IsDepartmentCheck");
                    if (sCondition != null && sCondition.Value != null)
                        isDepartmentCheck = bool.Parse(sCondition.Value.ToString());
                }
                if (conditions.Contain("IsPersonalCheck"))
                {
                    SearchCondition sCondition = conditions.GetValue("IsPersonalCheck");
                    if (sCondition != null && sCondition.Value != null)
                        isPersonalCheck = bool.Parse(sCondition.Value.ToString());
                }
                if (conditions.Contain("IsCustomerCheck"))
                {
                    SearchCondition sCondition = conditions.GetValue("IsCustomerCheck");
                    if (sCondition != null && sCondition.Value != null)
                        isCustomerCheck = bool.Parse(sCondition.Value.ToString());
                }
                if (conditions.Contain("IsJournal"))
                {
                    SearchCondition sCondition = conditions.GetValue("IsJournal");
                    if (sCondition != null && sCondition.Value != null)
                        isJournal = bool.Parse(sCondition.Value.ToString());
                }
                if (conditions.Contain("IsBankAccount"))
                {
                    SearchCondition sCondition = conditions.GetValue("IsBankAccount");
                    if (sCondition != null && sCondition.Value != null)
                        isBankAccount = bool.Parse(sCondition.Value.ToString());
                }
                if (conditions.Contain("IsFee"))
                {
                    SearchCondition sCondition = conditions.GetValue("IsFee");
                    if (sCondition != null && sCondition.Value != null)
                        isFee = bool.Parse(sCondition.Value.ToString());
                }
                if (conditions.Contain("OnlyLeaf"))
                {
                    SearchCondition sCondition = conditions.GetValue("OnlyLeaf");
                    if (sCondition != null && sCondition.Value != null)
                    { onlyLeaf = bool.Parse(sCondition.Value.ToString()); }                                         
                }
                else
                {
                    onlyLeaf = false;
                }
            }

            #endregion
        }

        private Dictionary<string, object> SetInitValues(SearchConditionCollection conditions)
        {
            Dictionary<string, object> initValues = new Dictionary<string, object>();
            initValues.Add("GLCode", glCode);
            initValues.Add("GLName", glName);

            if (solutionID != null && solutionID != Guid.Empty)
                initValues.Add("SolutionID", solutionID);
            initValues.Add("CompanyIDs",companyIds);
            initValues.Add("IsDepartmentCheck", isDepartmentCheck);
            initValues.Add("IsPersonalCheck", isPersonalCheck);
            initValues.Add("IsCustomerCheck", isCustomerCheck);
            initValues.Add("IsJournal", isJournal);
            initValues.Add("IsBankAccount", isBankAccount);
            initValues.Add("IsFee", isFee);
            initValues.Add("OnlyLeaf", onlyLeaf);
            if (conditions != null && conditions.Contain("FormTitle"))
                initValues.Add("FormTitle", conditions.GetValue("FormTitle").Value);
            return initValues;
        }

        public void PickMany(string searchValue, string property, SearchConditionCollection conditions, string[] returnFields, FinderTriggerType triggerType, GetExistValueHandler getExistValueHandler, Control container)
        {
            DeckWorkspace workspace = Workitem.Workspaces.Get<DeckWorkspace>(GLCodeFinderWorkspace);
            if (workspace == null || workspace.IsDisposed)
            {
                workspace = Workitem.Workspaces.AddNew<DeckWorkspace>(GLCodeFinderWorkspace);
                container.Controls.Add(workspace);
                workspace.Dock = DockStyle.Fill;
                workspace.BringToFront();
            }
            PickMany(searchValue, property, conditions, returnFields, triggerType, getExistValueHandler, GLCodeFinderWorkspace);
        }

        public void PickMany(string searchValue, string property, SearchConditionCollection conditions, string[] returnFields, FinderTriggerType triggerType, GetExistValueHandler getExistValueHandler, string workspaceName)
        {
            if (string.IsNullOrEmpty(workspaceName))
                workspaceName = ClientConstants.MainWorkspace;
            IWorkspace workspace = Workitem.Workspaces[workspaceName];

            #region Get Condition

            SetCondition(conditions);

            #endregion

            string glName = string.Empty, glCode = string.Empty;
            if (triggerType == FinderTriggerType.KeyEnter)
            {
                //if (property.Contains(SearchFieldConstants.Code))
                if (Rexlib.IsValidNumber(searchValue))
                    glCode = searchValue;
                else
                    glName = searchValue;
            }
            List<SolutionGLCodeList> list = ConfigureService.GetSolutionGLCodeListNew(solutionID,companyIds.ToArray(), glCode, glName, GLCodeType.Unknown, true
                    , isDepartmentCheck, isPersonalCheck, isCustomerCheck, isJournal, isBankAccount, isFee, LocalData.IsEnglish);
            if (list != null && list.Count == 1)
            {
                if (DataChoosed != null)
                    DataChoosed(this, new DataFindEventArgs(FAMUtility.GetMultiSearchResult<SolutionGLCodeList>(list, returnFields)));
                return;
            }

            List<Guid> existValues = new List<Guid>();
            IList exists = null;
            if (getExistValueHandler != null)
                exists = getExistValueHandler();
            if (exists != null && exists.Count > 0)
            {
                foreach (var item in exists)
                {
                    existValues.Add((Guid)item);
                }
            }
            List<SolutionGLCodeList> existList = new List<SolutionGLCodeList>();
            foreach (var item in list)
            {
                if (existValues.Contains(item.ID))
                    existList.Add(item);
            }

            multiFinderWorkitem = Workitem.WorkItems.Get<GLCodeMultiFinderWorkitem>(GetHashCode().ToString() + "GLCodeMultiFinderWorkitem");
            if (multiFinderWorkitem == null)
            {
                multiFinderWorkitem = Workitem.WorkItems.AddNew<GLCodeMultiFinderWorkitem>(GetHashCode().ToString() + "GLCodeMultiFinderWorkitem");
                multiFinderWorkitem.DataChoosed += OnMultiFinderDataChoosed;
            }          
            multiFinderWorkitem.Show(workspace, list, existList, returnFields, SetInitValues(conditions));
        }

        private void OnMultiFinderDataChoosed(object sender, DataFindEventArgs e)
        {
            if (DataChoosed != null)
                DataChoosed(sender, e);
        }

        public void PickOne(string searchValue, string property, SearchConditionCollection conditions, string[] returnFields, FinderTriggerType triggerType, Control container)
        {
            DeckWorkspace workspace = Workitem.Workspaces.Get<DeckWorkspace>(GLCodeFinderWorkspace);
            if (workspace == null || workspace.IsDisposed)
            {
                if (Workitem.Workspaces.Contains(GLCodeFinderWorkspace))
                    Workitem.Workspaces.Remove(workspace);

                workspace = Workitem.Workspaces.AddNew<DeckWorkspace>(GLCodeFinderWorkspace);
                workspace.Dock = DockStyle.Fill;
                workspace.BringToFront();
            }
            container.Controls.Clear();
            container.Controls.Add(workspace);
            PickOne(searchValue, property, conditions, returnFields, triggerType, GLCodeFinderWorkspace);
        }

        string glCode = string.Empty;
        string glName = string.Empty;
        public void PickOne(string searchValue, string property, SearchConditionCollection conditions, string[] returnFields, FinderTriggerType triggerType, string workspace)
        {
            if (string.IsNullOrEmpty(workspace))
                workspace = ClientConstants.MainWorkspace;

            IWorkspace workspaceName = Workitem.Workspaces[workspace];

            string name = string.Empty;
            if (triggerType == FinderTriggerType.KeyEnter)
                name = searchValue;


            #region Get Condition

            SetCondition(conditions);

            #endregion

            glCode = string.Empty;
            glName = string.Empty;
            List<SolutionGLCodeList> list;
            if (triggerType == FinderTriggerType.ClickButton && string.IsNullOrEmpty(name))
            {
                //单击选择按钮时，如果没有输入查询时，则不进行搜索。只弹出查询对话框。
                list = new List<SolutionGLCodeList>();
            }
            else
            {
                glCode = string.Empty;
                glName = string.Empty;
                if (Rexlib.IsValidNumber(name))
                    glCode = name;
                else
                    glName = name;
                list = ConfigureService.GetSolutionGLCodeListNew(solutionID, companyIds.ToArray(), glCode, glName, GLCodeType.Unknown, true
                    ,isDepartmentCheck,isPersonalCheck,isCustomerCheck,isJournal,isBankAccount,isFee, LocalData.IsEnglish);
            }

            if (list != null && list.Count == 1)
            {
                if (DataChoosed != null)
                    DataChoosed(this, new DataFindEventArgs(FAMUtility.GetSingleSearchResult<SolutionGLCodeList>(list[0], returnFields)));

                return;
            }

            finderWorkitem = Workitem.WorkItems.Get<GLCodeFinderWorkitem>(GetHashCode().ToString() + "GLCodeFinderWorkitem");
            if (finderWorkitem == null)
            {
                finderWorkitem = Workitem.WorkItems.AddNew<GLCodeFinderWorkitem>(GetHashCode().ToString() + "GLCodeFinderWorkitem");
                finderWorkitem.DataChoosed += OnDataChoosed;
             
            }

            finderWorkitem.Show(workspaceName, list, returnFields, SetInitValues(conditions));
        }
        private void OnDataChoosed(object sender, DataFindEventArgs e)
        {
            if (DataChoosed != null)
                DataChoosed(sender, e);
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            Disponse(true);
            GC.SuppressFinalize(this);
        }

        private void Disponse(Boolean isDisponsing)
        {
            if (isDisponsing)
            {
                DataChoosed = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                }
                if (finderWorkitem != null)
                {
                    finderWorkitem.DataChoosed -= OnDataChoosed;
                    finderWorkitem = null;
                }
            }
        }

        #endregion
    }
}
