using HtmlAgilityPack;
using Reader.Client.Models;
using Reader.Client.Spider.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Xml;

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
                Url = $"{_BookSource.SearchLink}{keyWord}";
                GetHtmlContent();
                HtmlDocument docMain = new HtmlDocument();
                docMain.LoadHtml(HtmlContent);
                //判断是否有数据
                if (docMain.Text == "")
                    return books;

                //获取查询列表
                HtmlNodeCollection queryList = docMain.DocumentNode.SelectNodes(_BookSource.SearchXPathList);
                //查询列表第一项为表头，所有查询项数据需要大于1
                if (queryList == null || queryList.Count == 1)
                    return books;
                //移除表头
                queryList.RemoveAt(0);

                foreach (HtmlNode htmlNode in queryList)
                {
                    HtmlDocument docOne = new HtmlDocument();
                    docOne.LoadHtml(htmlNode.InnerHtml);
                    BookModel bookModel = new BookModel();
                    //获取小说Key
                    string keyText = docOne.XPathInnerText(_BookSource.SearchXPathKey, _BookSource.IsDebug);
                    bookModel.Key = keyText.RegexText(_BookSource.SearchRegexKey, _BookSource.IsDebug);
                    //获取小说名称
                    bookModel.Name = docOne.XPathInnerText(_BookSource.SearchXPathName, _BookSource.IsDebug);
                    //获取小说主页链接
                    bookModel.Link = docOne.XPathAttributeValue(_BookSource.SearchXPathLink, _BookSource.SearchAttributeLink, _BookSource.IsDebug);
                    if (string.IsNullOrWhiteSpace(bookModel.Key) && !string.IsNullOrWhiteSpace(bookModel.Link))
                    {
                        bookModel.Key = bookModel.Link.RegexText(_BookSource.SearchRegexKey, _BookSource.IsDebug);
                    }
                    //获取小说作者
                    bookModel.Author = docOne.XPathInnerText(_BookSource.SearchXPathAuthor, _BookSource.IsDebug);
                    //获取小说类型
                    bookModel.Tag = docOne.XPathInnerText(_BookSource.SearchXPathTag, _BookSource.IsDebug);
                    //获取更新时间
                    bookModel.UpdateTime = docOne.XPathDateTime(_BookSource.SearchXPathUpdateTime);
                    //获取小说状态
                    bookModel.Status = docOne.XPathInnerText(_BookSource.SearchXPathStatus, _BookSource.IsDebug);
                    bookModel.SourceID = _BookSource.ID;

                    books.Add(bookModel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                books = new ObservableCollection<BookModel>();
            }
            return books;
        }

        /// <summary>
        /// 补充书籍信息并返回章节列表
        /// </summary>
        /// <param name="bookModel"></param>
        /// <returns></returns>
        public ObservableCollection<ChapterModel> ReplenishBookReturnChapterList(BookModel bookModel)
        {
            ObservableCollection<ChapterModel> chapters = new ObservableCollection<ChapterModel>();
            try
            {
                Url = bookModel.Link;
                GetHtmlContent();

                HtmlDocument _doc_Main = new HtmlDocument();
                _doc_Main.LoadHtml(HtmlContent);
                //判断是否有数据
                if (_doc_Main.Text == "")
                    return chapters;

                //获取小说信息
                string bookInfo = _doc_Main.XPathInnerText(_BookSource.DetailXPathInfo, _BookSource.IsDebug);
                if (!string.IsNullOrWhiteSpace(bookInfo))
                {
                    HtmlDocument docOne = new HtmlDocument();
                    docOne.LoadHtml(bookInfo);
                    //获取小说Key
                    if(string.IsNullOrWhiteSpace(bookModel.Key))
                    {
                        string keyText = docOne.XPathInnerText(_BookSource.DetailXPathKey, _BookSource.IsDebug);
                        bookModel.Key = keyText.RegexText(_BookSource.DetailRegexKey);
                    }
                    //获取小说作者
                    if (string.IsNullOrWhiteSpace(bookModel.Author))
                        bookModel.Author = docOne.XPathInnerText(_BookSource.DetailXPathAuthor, _BookSource.IsDebug);
                    //获取最后更新时间
                    if (bookModel.UpdateTime==DateTime.MinValue)
                        bookModel.UpdateTime = docOne.XPathDateTime(_BookSource.DetailXPathUpdateTime);
                    //获取状态
                    if(string.IsNullOrWhiteSpace(bookModel.Status))
                        bookModel.Status = docOne.XPathInnerText(_BookSource.DetailXPathStatus, _BookSource.IsDebug);
                    //小说类型
                    if (string.IsNullOrWhiteSpace(bookModel.Tag))
                        bookModel.Tag = docOne.XPathInnerText(_BookSource.DetailXPathTag, _BookSource.IsDebug);
                    //获取小说简介
                    bookModel.Introduction = docOne.XPathInnerText(_BookSource.DetailXPathIntroduction, _BookSource.IsDebug);
                    //小说封皮链接
                    bookModel.PosterLink = docOne.XPathAttributeValue(_BookSource.DetailXPathPosterLink, _BookSource.DetailAttributePosterLink, _BookSource.IsDebug);
                    //获取小说封皮
                    //获取最后更新章节及链接
                }

                //获取章节列表
                HtmlNodeCollection _hnc_Chapter_List = _doc_Main.DocumentNode.SelectNodes(_BookSource.DetailXPathChapterList);
                if (_hnc_Chapter_List!=null && _hnc_Chapter_List.Count != 0)
                {
                    int orderIndex = chapters.Count;
                    foreach (HtmlNode htmlNode in _hnc_Chapter_List)
                    {
                        orderIndex++;
                        HtmlDocument docOne = new HtmlDocument();
                        docOne.LoadHtml(htmlNode.InnerHtml);

                        ChapterModel chapter = new ChapterModel();
                        chapter.BookKey = bookModel.Key;
                        string keyText= docOne.XPathInnerText(_BookSource.DetailXPathChapterKey, _BookSource.IsDebug);
                        chapter.Key = keyText.RegexText(_BookSource.DetailRegexKey,_BookSource.IsDebug);
                        chapter.Name = docOne.XPathInnerText(_BookSource.DetailXPathChapterName, _BookSource.IsDebug);
                        chapter.Link = docOne.XPathInnerText(_BookSource.DetailXPathChapterLink, _BookSource.IsDebug);
                        chapter.OrderIndex = orderIndex;
                        if (string.IsNullOrWhiteSpace(chapter.Key) && !string.IsNullOrWhiteSpace(chapter.Link))
                        {
                            chapter.Link = chapter.Link.RegexText(_BookSource.DetailRegexKey, _BookSource.IsDebug);
                        }
                        chapters.Add(chapter);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return chapters;
        }

        /// <summary>
        /// 完善章节信息
        /// </summary>
        /// <param name="chapter"></param>
        public void ReplenishChapter(ChapterModel chapter)
        {
            Url = chapter.Link;
            GetHtmlContent();

            HtmlDocument _doc_Main = new HtmlDocument();
            _doc_Main.LoadHtml(HtmlContent);
            //判断是否有数据
            if (_doc_Main.Text == "")
                return;

            //章节名称
            if (string.IsNullOrWhiteSpace(chapter.Name))
            {
                chapter.Name = _doc_Main.XPathInnerText(_BookSource.ChapterXPathName);
            }
            //获取章节内容
            chapter.Content = _doc_Main.XPathInnerText(_BookSource.ChapterRegexContent);
        }
    }
}
