#region Comment

/*
 * 
 * FileName:    OutlookAdvancedSearch.cs
 * CreatedOn:   2014/9/23 17:50:28
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

using System.Threading;
using Microsoft.Office.Interop.Outlook;
using System.Collections.Generic;

namespace ICP.MailCenter.UI
{
    class OutlookAdvancedSearch
    {
        #region 成员变量
        #region 属性-查询结果集
        /// <summary>
        /// 查询结果
        /// </summary>
        private List<object> _SearchResults;
        /// <summary>
        /// 查询结果
        /// </summary>
        public List<object> SearchResults
        {
            get { return _SearchResults; }
        }
        #endregion

        /// <summary>
        /// 超时时间10S
        /// </summary>
        private int _TimeOut = 10 * 1000;
        /// <summary>
        /// 未执行完等待时间(1000ms)
        /// </summary>
        private int _WaitTime = 1000;
        /// <summary>
        /// 总花费时间
        /// </summary>
        private int _TotalTime;
        /// <summary>
        /// 查询目录：默认个人文件夹
        /// </summary>
        string _Scope;
        /// <summary>
        /// 筛选器
        /// </summary>
        string _Filter;
        /// <summary>
        /// 高级查询
        /// </summary>
        Search advancedSearch;
        /// <summary>
        /// 异步委托：监听查询结果
        /// </summary>
        /// <param name="Scope">查询目录</param>
        /// <param name="Filter">筛选</param>
        /// <param name="SearchSubFolders">是否查询子目录</param>
        /// <param name="Tag">Tag标记</param>
        /// <returns></returns>
        private bool _SearchIsCompleted;
        #endregion

        #region 构造函数
        /// <summary>
        /// Outlook高级查询
        /// </summary>
        /// <param name="strIMessageID">Mail IMessageID</param>
        /// <param name="strSubject">Mail Subject</param>
        public OutlookAdvancedSearch(string strIMessageID, string strSubject = "")
        {
            try
            {
                #region 0.构建筛选
                _Filter = "";
                if (!string.IsNullOrEmpty(strIMessageID))
                {
                    //从Outlook MessageID或Custom MessageID查找
                    _Filter += "(\"" + ClientUtility.OutlookMessageIDTag + "\" = \'" + strIMessageID + "\') "
                        + " OR  (\"" + ClientUtility.CustomMessageIDTag + "/0000001F\"=\'" + strIMessageID + "\')";
                }
                if (!string.IsNullOrEmpty(strSubject))
                {
                    //通过Subject查找
                    if (!string.IsNullOrEmpty(_Filter))
                        _Filter += " AND ";
                    _Filter += "(\"urn:schemas:mailheader:subject\" LIKE \'%" + strSubject + "%\')";
                }
                #endregion

                _SearchResults = new List<object>();
                _Scope = string.Empty;
                _SearchIsCompleted = false;
            }
            catch
            {
            }
        }
        #endregion

        #region 公用方法

        /// <summary>
        /// 运行高级查询
        /// </summary>
        public void RunAdvancedSearch()
        {
            try
            {
                if (!string.IsNullOrEmpty(_Filter))
                {
                    StartSearch();
                    if (advancedSearch != null)
                    {
                        int mailResultCount = advancedSearch.Results.Count;
                        foreach (var result in advancedSearch.Results)
                        {
                            if (result is MailItem)
                            {
                                _SearchResults.Add(result);
                                (result as MailItem).Close(OlInspectorClose.olDiscard);
                            }
                                
                        }
                    }
                    ClientUtility.OLApplication.AdvancedSearchComplete -= OLApplication_AdvancedSearchComplete;
                }
            }
            catch (System.Exception ex)
            {

            }
        }
        /// <summary>
        /// 开始查询
        /// </summary>
        /// <param name="SearchAllStore">是否查询所有Store</param>
        void StartSearch(bool SearchAllStore = false)
        {
            _Scope = "\'" + ClientUtility.OLNameSpace.DefaultStore.GetRootFolder().FolderPath + "\'";
            ClientUtility.OLApplication.AdvancedSearchComplete += OLApplication_AdvancedSearchComplete;
            advancedSearch = ClientUtility.OLApplication.AdvancedSearch(_Scope, _Filter, true, "");
            //高级查询未结束且未到达超时时间
            while (!_SearchIsCompleted || _TotalTime < _TimeOut)
            {
                Thread.Sleep(_WaitTime);
                _TotalTime += _WaitTime;
            }
        }

        /// <summary>
        /// 高级查询完成事件
        /// </summary>
        void OLApplication_AdvancedSearchComplete(Search SearchObject)
        {
            _SearchIsCompleted = true;
        }
        #endregion

        #region 析构函数
        /// <summary>
        /// 释放对象
        /// </summary>
        ~OutlookAdvancedSearch()
        {
            advancedSearch = null;
            if (_SearchResults != null)
                _SearchResults.Clear();
            _SearchResults = null;
        } 
        #endregion
    }
}
