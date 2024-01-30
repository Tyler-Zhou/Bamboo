using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FAM.UI.GLCode.Finder
{
    public class GLCodeFinderWorkitem : WorkItem
    {

        public event EventHandler<DataFindEventArgs> DataChoosed;
        GLCodeSearhcPart _searchPart = null;
        GLCodeFinderListPart _listPart = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DataChoosed = null;
                _searchPart = null;
                _listPart = null;
            }
            base.Dispose(disposing);
        }
        public void Show(IWorkspace mainWorkspace, List<SolutionGLCodeList> list, string[] returnFields, Dictionary<string, object> initValues)
        {
            if (mainWorkspace == null)
                mainWorkspace = Workspaces[ClientConstants.MainWorkspace];

            GLCodeMainWorkSpace locationMainSpce = SmartParts.Get<GLCodeMainWorkSpace>("GLCodeMainWorkSpace");
            if (locationMainSpce == null)
            {
                locationMainSpce = SmartParts.AddNew<GLCodeMainWorkSpace>("GLCodeMainWorkSpace");

                #region AddPart

                GLCodeFinderToolBar toolBar = SmartParts.AddNew<GLCodeFinderToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)Workspaces[GLCodeWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                _listPart = SmartParts.AddNew<GLCodeFinderListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[GLCodeWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(_listPart);

                _searchPart = SmartParts.AddNew<GLCodeSearhcPart>();
                IWorkspace searchWorkspace = (IWorkspace)Workspaces[GLCodeWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(_searchPart);

                #endregion

                BulidConnection(toolBar, _searchPart, _listPart, returnFields);
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                string titel = LocalData.IsEnglish ? "GLCode Finder" : "查找会计科目";

                smartPartInfo.Title = titel;
                mainWorkspace.Show(locationMainSpce, smartPartInfo);
            }
            else
            {
                mainWorkspace.Activate(locationMainSpce);
            }

            _searchPart.Init(initValues);
            _listPart.Init(initValues);
            if (list != null)
            {
                _listPart.DataSource = list;

                //根据CodeOrName查询出来的结果可能会有很多。
                //出于便利性考虑，默定位到与code完全匹配的结果中。
                //if (_listPart is CustomerSingleMainListPart)
                //{
                //    ((CustomerSingleMainListPart)_listPart).LocateToMatchedItem(_searchPart.txtCodeOrName.Text);
                //}
            }
        }

        private void BulidConnection(BaseEditPart toolBar
                                     , BaseSearchPart searchPart
                                     , BaseListPart listPart
                                     , string[] returnFields)
        {
            listPart.Selected += delegate(object sender, object data)
            {
                SolutionGLCodeList list = data as SolutionGLCodeList;
                if (list == null) return;
                if (DataChoosed != null)
                {
                    DataChoosed(sender, new DataFindEventArgs(FAMUtility.GetSingleSearchResult<SolutionGLCodeList>(list, returnFields)));
                }
            };

            listPart.CurrentChanged += delegate(object sender, object data)
            {
                toolBar.DataSource = data;
            };

            searchPart.OnSearched += delegate(object sender, object results)
            {
                listPart.DataSource = results;

                //根据CodeOrName查询出来的结果可能会有很多。
                //出于便利性考虑，默定位到与code完全匹配的结果中。
                //if (listPart is CustomerSingleMainListPart)
                //{
                //    ((CustomerSingleMainListPart)listPart).LocateToMatchedItem(_searchPart.txtCodeOrName.Text);
                //}
            };
        }
    }
}
