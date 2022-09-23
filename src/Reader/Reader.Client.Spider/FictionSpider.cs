using HtmlAgilityPack;
using Reader.Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Client.Spider
{
    /// <summary>
    /// 小说爬虫
    /// </summary>
    public class FictionSpider: HttpWebRequestSpider
    {
        /// <summary>
        /// 书源模型
        /// </summary>
        BookSourceModel _BookSource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookSource"></param>
        public FictionSpider(BookSourceModel bookSource)
        {
            _BookSource = bookSource;
        }

        /// <summary>
        /// 查找书籍
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public ObservableCollection<BookModel> SearchBookList(string keyWord)
        {
            ObservableCollection<BookModel> books = new ObservableCollection<BookModel>();
            try
            {
                Url = $"{_BookSource.SearchUrl}{keyWord}";
                GetHtmlContent();
                HtmlDocument docMain = new HtmlDocument();
                docMain.LoadHtml(HtmlContent);
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
