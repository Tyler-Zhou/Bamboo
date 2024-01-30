using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using System.ComponentModel;
using ICP.Common.ServiceInterface;

namespace ICP.FAM.UI.GLCode.Finder
{
    public class GLCodeMultiFinderWorkitem : WorkItem
    {
        public event EventHandler<DataFindEventArgs> DataChoosed;
        GLCodeSearhcPart searchPart = null;

        #region Service

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DataChoosed = null;
                searchPart = null;
            }
            base.Dispose(disposing);
        }

        public void Show(IWorkspace mainWorkspace,List<SolutionGLCodeList> list, List<SolutionGLCodeList> existList, string[] returnFields, Dictionary<string, object> initValues)
        {
            if (mainWorkspace == null)
                mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
            GLCodeMultiFinderWorkspace mainSpace = SmartParts.Get<GLCodeMultiFinderWorkspace>("GLCodeMultiFinderWorkspace");
            if (mainSpace == null)
            {
                mainSpace = SmartParts.AddNew<GLCodeMultiFinderWorkspace>("GLCodeMultiFinderWorkspace");

                #region AddPart

                GLCodeMultiFinderToolBar toolBar = SmartParts.AddNew<GLCodeMultiFinderToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)Workspaces[GLCodeWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                GLCodeMultiMainListPart listPart = SmartParts.AddNew<GLCodeMultiMainListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[GLCodeWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                GLCodeEditPart editPart = SmartParts.AddNew<GLCodeEditPart>();
                IWorkspace editWorkspace = (IWorkspace)Workspaces[GLCodeWorkSpaceConstants.EditWorkspace];
                editWorkspace.Show(editPart);

                MultiFinderSelectedToolBar selectedToolBar = SmartParts.AddNew<MultiFinderSelectedToolBar>();
                IWorkspace selectedToolBarWorkspace = (IWorkspace)Workspaces[GLCodeWorkSpaceConstants.SelectedToolBarWorkspace];
                selectedToolBarWorkspace.Show(selectedToolBar);

                GLCodeMultiSelectedListPart selectedListPart = SmartParts.AddNew<GLCodeMultiSelectedListPart>();
                IWorkspace selectedListWorkspace = (IWorkspace)Workspaces[GLCodeWorkSpaceConstants.SelectedListWorkspace];
                selectedListWorkspace.Show(selectedListPart);

                searchPart = SmartParts.AddNew<GLCodeSearhcPart>();
                IWorkspace searchWorkspace = (IWorkspace)Workspaces[GLCodeWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);

                #endregion

                BulidConnection(toolBar, editPart, searchPart, listPart, selectedListPart, selectedToolBar, returnFields);
                listPart.DataSource = list;
                selectedListPart.DataSource = existList;
                string title = LocalData.IsEnglish ? "GLCode Finder" : "查找会计科目";
                if (initValues != null && initValues.Keys.Contains("FormTitle"))
                {
                    if (initValues["FormTitle"] != null)
                        title = initValues["FormTitle"].ToString();
                }
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = title;
                mainWorkspace.Show(mainSpace, smartPartInfo);
            }
            else
                mainWorkspace.Activate(mainSpace);
            searchPart.Init(initValues);
        }

        private void BulidConnection(BaseToolBar toolBar
                                    , BaseEditPart editPart
                                    , BaseSearchPart searchPart
                                    , BaseListPart listPart
                                    , BaseListPart selectedListPart
                                    , BaseToolBar selectedToolBar
                                    , string[] returnFields)
        {
            listPart.CurrentChanging += delegate(object sender, CancelEventArgs e)
            {
                UIConnectionHelper.ParentChangingForEditPart(listPart
                                                            ,editPart.SaveData
                                                            ,(editPart.DataSource as SolutionGLCodeList)
                                                            ,e
                                                            ,LocalData.IsEnglish?"GLCode Edit":"编辑会计科目");
            };

            listPart.Selected += delegate(object sender, object data)
            {
                List<SolutionGLCodeList> newselectedList = data as List<SolutionGLCodeList>;
                if (newselectedList == null || newselectedList.Count == 0)
                    return;
                List<SolutionGLCodeList> selected = selectedListPart.DataSource as List<SolutionGLCodeList>;
                if (selected == null)
                    selected = new List<SolutionGLCodeList>();
                List<SolutionGLCodeList> needAddList = new List<SolutionGLCodeList>();
                foreach (var item in newselectedList)
                {
                    SolutionGLCodeList tager = selected.Find(delegate(SolutionGLCodeList obj) { return obj.ID == item.ID; });
                    if (tager != null)
                        continue;
                    needAddList.Add(item);
                }

                selected.AddRange(needAddList);
                selectedListPart.DataSource = selected;
            };

            listPart.CurrentChanged += delegate(object sender, object data)
            {
                SolutionGLCodeList listData = data as SolutionGLCodeList;
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add("ParentList", data);

                #region EditPart

                SolutionGLCodeList info = null;
                if (listData != null)
                {
                    if (listData.IsNew)
                    {
                        info = new SolutionGLCodeList();
                        info.CreateByID = LocalData.UserInfo.LoginID;
                        info.CreateByName = LocalData.UserInfo.LoginName;
                        info.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                        info.IsValid = true;
                        info.IsDirty = false;
                    }
                    else
                        info = ConfigureService.GetSolutionGLCodeInfoNew(((SolutionGLCodeList)data).ID, LocalData.IsEnglish);
                }
                editPart.DataSource = info;

                #endregion
            };

            selectedListPart.Selected += delegate(object sender, object data)
            {
                List<SolutionGLCodeList> newSelectedList = data as List<SolutionGLCodeList>;
                if (newSelectedList == null)
                    newSelectedList = new List<SolutionGLCodeList>();
                if (DataChoosed != null)
                    DataChoosed(sender, new DataFindEventArgs(FAMUtility.GetMultiSearchResult<SolutionGLCodeList>(newSelectedList, returnFields)));
            };

            selectedListPart.CurrentChanged += delegate(object sender, object data)
            {
                SolutionGLCodeList listData = data as SolutionGLCodeList;
                if (listData == null)
                {
                    selectedToolBar.SetEnable("barRemove", false);
                    selectedToolBar.SetEnable("barRemoveAll", false);
                }
                else
                {
                    selectedToolBar.SetEnable("barRemove", true);
                    selectedToolBar.SetEnable("barRemoveAll", true);
                }
            };

            editPart.Saved += delegate(object[] prams)
            {
                if (listPart.Current == null || prams == null)
                    return;
                SolutionGLCodeList list = prams[0] as SolutionGLCodeList;
                SolutionGLCodeList currentRow = listPart.Current as SolutionGLCodeList;
                FAMUtility.CopyToValue(list, currentRow, typeof(SolutionGLCodeList));
            };

            searchPart.OnSearched += delegate(object sender, object results)
            {
                listPart.DataSource = results;
            };
        }
    }
}
