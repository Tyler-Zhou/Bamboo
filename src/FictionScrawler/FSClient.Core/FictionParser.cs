using FSClient.Models;
using HtmlAgilityPack;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;

namespace FSClient.Core
{
    /// <summary>
    /// 小说解析器
    /// </summary>
    public class FictionParser
    {
        /// <summary>
        /// 书源模型
        /// </summary>
        BookSourceModel _BookSource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookSource"></param>
        public FictionParser(BookSourceModel bookSource)
        {
            _BookSource = bookSource;
        }

        /// <summary>
        /// 查找书籍
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ObservableCollection<BookModel> QueryBooks(string keyWord)
        {
            HtmlWeb _web_Main = new HtmlWeb();
            ObservableCollection<BookModel> books = new ObservableCollection<BookModel>();
            try
            {
                HtmlDocument docMain = new HtmlDocument();
                docMain.OptionAutoCloseOnEnd = true;

                docMain = _web_Main.Load($"{_BookSource.SearchUrl}{keyWord}");
                //判断是否有数据
                if (docMain.Text == "")
                    return books;

                //获取查询列表
                HtmlNodeCollection queryList = docMain.DocumentNode.SelectNodes(_BookSource.XPath_List);
                //查询列表第一项为表头，所有查询项数据需要大于1
                if (queryList == null || queryList.Count == 1)
                    return books;
                //移除表头
                queryList.RemoveAt(0);

                foreach (HtmlNode htmlNode in queryList)
                {
                    HtmlDocument docOne = new HtmlDocument();
                    docOne.LoadHtml(htmlNode.InnerHtml);
                    BookModel _tfi = new BookModel();
                    
                    //获取小说名称及主页链接
                    HtmlNodeCollection htmlNodeName = docOne.DocumentNode.SelectNodes(_BookSource.XPath_Name);
                    if (htmlNodeName != null && htmlNodeName.Count > 0)
                    {
                        _tfi.Name = htmlNodeName[0].InnerText.Trim();
                        _tfi.Url = htmlNodeName[0].Attributes["href"].Value;
                    }
                    //获取小说作者
                    HtmlNodeCollection htmlNodeAuthor = docOne.DocumentNode.SelectNodes(_BookSource.XPath_Author);
                    if (htmlNodeAuthor != null && htmlNodeAuthor.Count > 0)
                    {
                        _tfi.Author = htmlNodeAuthor[0].InnerText;
                    }
                    //获取小说类型
                    if (!string.IsNullOrWhiteSpace(_BookSource.XPath_Type))
                    {
                        HtmlNodeCollection htmlNodeType = docOne.DocumentNode.SelectNodes(_BookSource.XPath_Type);
                        if (htmlNodeType != null && htmlNodeType.Count > 0)
                        {
                            _tfi.Type = htmlNodeType[0].InnerText.Replace("[", "").Replace("]", "");
                        }
                    }
                    //获取更新时间
                    if (!string.IsNullOrWhiteSpace(_BookSource.XPath_UpdateTime))
                    {
                        HtmlNodeCollection htmlNodeUpdateTime = docOne.DocumentNode.SelectNodes(_BookSource.XPath_UpdateTime);
                        if (htmlNodeUpdateTime != null && htmlNodeUpdateTime.Count > 0)
                        {
                            _tfi.UpdateTime = DateTime.Parse(htmlNodeUpdateTime[0].InnerText);
                        }
                    }
                    //获取小说状态
                    if (!string.IsNullOrWhiteSpace(_BookSource.XPath_Status))
                    {
                        HtmlNodeCollection htmlNodeStatus = docOne.DocumentNode.SelectNodes(_BookSource.XPath_Status);
                        if (htmlNodeStatus != null && htmlNodeStatus.Count > 0)
                        {
                            _tfi.Status = htmlNodeStatus[0].InnerText;
                        }
                    }
                    _tfi.SourceKey = _BookSource.Key;

                    books.Add(_tfi);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                books = new ObservableCollection<BookModel>();
            }
            return books;
        }
    }
}
