using HtmlAgilityPack;
using Reader.Client.Models;
using Reader.Client.Spider.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Xml;

namespace Reader.Client.Spider
{
    /// <summary>
    /// 写入调试日志
    /// </summary>
    /// <param name="logString"></param>
    public delegate void DelegateWriteDebugLog(string logString);

    /// <summary>
    /// 小说爬虫
    /// </summary>
    public class FictionSpider: HttpWebRequestSpider
    {
        #region 成员(Member)
        /// <summary>
        /// 书源模型
        /// </summary>
        BookSourceModel _BookSource { get; set; }

        /// <summary>
        /// 写入调试日志
        /// </summary>
        public DelegateWriteDebugLog OnWriteDebugLog;
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookSource"></param>
        public FictionSpider(BookSourceModel bookSource)
        {
            _BookSource = bookSource;
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 查找书籍
        /// </summary>
        /// <param name="keyWord">搜索关键字</param>
        /// <param name="preciseSearch">精确搜索</param>
        /// <returns></returns>
        public ObservableCollection<BookModel> SearchBookList(string keyWord,bool preciseSearch=false)
        {
            ObservableCollection<BookModel> books = new ObservableCollection<BookModel>();
            try
            {
                Url = $"{_BookSource.SearchLink}{keyWord}";
                WriteDebugLog($"开始搜索关键字:{keyWord}");
                GetHtmlContent();
                WriteDebugLog($"开始解析网页内容");
                HtmlDocument docMain = new HtmlDocument();
                docMain.LoadHtml(HtmlContent);
                //判断是否有数据
                if (docMain.Text == "")
                    return books;
                WriteDebugLog($"{docMain.Text}");
                //获取查询列表
                HtmlNodeCollection queryList = docMain.DocumentNode.SelectNodes(_BookSource.SearchXPathList);
                //查询列表第一项为表头，所有查询项数据需要大于1
                if (queryList == null || queryList.Count == 1)
                    return books;
                WriteDebugLog($"┌获取书籍列表");
                WriteDebugLog($"└列表大小:{queryList.Count}");
                //移除表头
                queryList.RemoveAt(0);

                foreach (HtmlNode htmlNode in queryList)
                {
                    HtmlDocument docOne = new HtmlDocument();
                    docOne.LoadHtml(htmlNode.InnerHtml);
                    string bookName = docOne.XPathInnerText(_BookSource.SearchXPathName, _BookSource.IsDebug);
                    
                    if (preciseSearch)
                    {
                        if (!keyWord.Equals(bookName))
                        {
                            continue;
                        }
                    }
                    BookModel bookModel = new BookModel();
                    bookModel.ID = Guid.NewGuid();
                    bookModel.Name = bookName;
                    //获取小说名称
                    WriteDebugLog($"┌获取书籍名称");
                    WriteDebugLog($"└{bookModel.Name}");

                    //获取小说Key
                    WriteDebugLog($"┌获取书籍Key");
                    string keyText = docOne.XPathInnerText(_BookSource.SearchXPathKey, _BookSource.IsDebug);
                    bookModel.Key = keyText.RegexText(_BookSource.SearchRegexKey,"R", _BookSource.IsDebug);
                    WriteDebugLog($"└{bookModel.Key}");
                    
                    //获取小说主页链接
                    WriteDebugLog($"┌获取书籍链接");
                    bookModel.Link = docOne.XPathAttributeValue(_BookSource.SearchXPathLink, _BookSource.SearchAttributeLink, _BookSource.IsDebug);
                    WriteDebugLog($"└{bookModel.Link}");
                    if (string.IsNullOrWhiteSpace(bookModel.Key) && !string.IsNullOrWhiteSpace(bookModel.Link))
                    {
                        WriteDebugLog($"┌从链接获取书籍Key");
                        bookModel.Key = bookModel.Link.RegexText(_BookSource.SearchRegexKey, "R", _BookSource.IsDebug);
                        WriteDebugLog($"└{bookModel.Key}");
                    }
                    //获取小说作者
                    WriteDebugLog($"┌获取书籍作者");
                    bookModel.Author = docOne.XPathInnerText(_BookSource.SearchXPathAuthor, _BookSource.IsDebug);
                    WriteDebugLog($"└{bookModel.Author}");
                    //获取小说标签(类型)
                    WriteDebugLog($"┌获取书籍标签(类型)");
                    bookModel.Tag = docOne.XPathInnerText(_BookSource.SearchXPathTag, _BookSource.IsDebug);
                    WriteDebugLog($"└{bookModel.Tag}");
                    //获取更新时间
                    WriteDebugLog($"┌获取书籍更新时间");
                    bookModel.UpdateTime = docOne.XPathDateTime(_BookSource.SearchXPathUpdateTime);
                    WriteDebugLog($"└{bookModel.UpdateTime}");
                    //获取小说状态
                    WriteDebugLog($"┌获取书籍状态");
                    bookModel.Status = docOne.XPathInnerText(_BookSource.SearchXPathStatus, _BookSource.IsDebug);
                    WriteDebugLog($"└{bookModel.Status}");
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
        /// 补充书籍信息并返回书籍下载任务
        /// </summary>
        /// <param name="bookModel"></param>
        /// <returns></returns>
        public BookTaskModel ReplenishBookReturnBookTask(BookModel bookModel)
        {
            
            try
            {
                Url = bookModel.Link;
                WriteDebugLog($"访问链接:{Url}");
                GetHtmlContent();
                WriteDebugLog($"开始解析网页内容");
                HtmlDocument _doc_Main = new HtmlDocument();
                _doc_Main.LoadHtml(HtmlContent);
                //判断是否有数据
                if (_doc_Main.Text == "")
                    return null;
                WriteDebugLog($"{_doc_Main.Text}");
                //获取小说信息
                WriteDebugLog($"详细页书籍信息");
                HtmlNode htmlNodeInfo = _doc_Main.DocumentNode.SelectSingleNode(_BookSource.DetailXPathInfo);

                #region 完善书籍信息
                if (htmlNodeInfo!=null && !string.IsNullOrWhiteSpace(htmlNodeInfo.InnerText))
                {
                    WriteDebugLog($"{htmlNodeInfo.InnerHtml}");
                    HtmlDocument docOne = new HtmlDocument();
                    docOne.LoadHtml(htmlNodeInfo.InnerHtml);
                    //获取小说Key
                    if (string.IsNullOrWhiteSpace(bookModel.Key))
                    {
                        WriteDebugLog($"┌完善书籍Key");
                        string keyText = docOne.XPathInnerText(_BookSource.DetailXPathKey, _BookSource.IsDebug);
                        bookModel.Key = keyText.RegexText(_BookSource.DetailRegexKey);
                        WriteDebugLog($"└{bookModel.Key}");
                    }
                    //获取小说作者
                    if (string.IsNullOrWhiteSpace(bookModel.Author))
                    {
                        WriteDebugLog($"┌完善书籍作者");
                        bookModel.Author = docOne.XPathInnerText(_BookSource.DetailXPathAuthor, _BookSource.IsDebug);
                        WriteDebugLog($"└{bookModel.Author}");
                    }
                    //获取最后更新时间
                    if (bookModel.UpdateTime == DateTime.MinValue)
                    {
                        WriteDebugLog($"┌完善书籍更新时间");
                        bookModel.UpdateTime = docOne.XPathDateTime(_BookSource.DetailXPathUpdateTime);
                        WriteDebugLog($"└{bookModel.UpdateTime}");
                    }
                    //获取状态
                    if (string.IsNullOrWhiteSpace(bookModel.Status))
                    {
                        WriteDebugLog($"┌完善书籍状态");
                        bookModel.Status = docOne.XPathInnerText(_BookSource.DetailXPathStatus, _BookSource.IsDebug);
                        WriteDebugLog($"└{bookModel.Status}");
                    }
                    //小说标签(分类)
                    if (string.IsNullOrWhiteSpace(bookModel.Tag))
                    {
                        WriteDebugLog($"┌完善书籍标签(分类)");
                        bookModel.Tag = docOne.XPathInnerText(_BookSource.DetailXPathTag, _BookSource.IsDebug);
                        WriteDebugLog($"└{bookModel.Tag}");
                    }
                    //获取小说简介
                    WriteDebugLog($"┌完善书籍简介");
                    bookModel.Introduction = docOne.XPathInnerText(_BookSource.DetailXPathIntroduction, _BookSource.IsDebug);
                    WriteDebugLog($"└{bookModel.Introduction}");
                    //小说封皮链接
                    WriteDebugLog($"┌完善书籍封皮链接");
                    bookModel.PosterLink = docOne.XPathAttributeValue(_BookSource.DetailXPathPosterLink, _BookSource.DetailAttributePosterLink, _BookSource.IsDebug);
                    WriteDebugLog($"└{bookModel.PosterLink}");
                    //获取小说封皮
                    if(!string.IsNullOrWhiteSpace(bookModel.PosterLink))
                    {
                        WriteDebugLog($"┌完善书籍封皮");
                        bookModel.PosterContent = BookPoster(bookModel.PosterLink);
                        WriteDebugLog($"┌封皮字节大小:{bookModel.PosterContent.Length}");
                    }
                    //获取最后更新章节及链接
                }
                #endregion

                BookTaskModel bookTask = new BookTaskModel();
                bookTask.BookKey = bookModel.Key;
                bookTask.SourceID = bookModel.SourceID;

                //获取章节列表
                WriteDebugLog($"获取章节列表");
                HtmlNodeCollection _hnc_Chapter_List = _doc_Main.DocumentNode.SelectNodes(_BookSource.DetailXPathChapterList);
                if (_hnc_Chapter_List != null && _hnc_Chapter_List.Count != 0)
                {
                    WriteDebugLog($"└列表大小:{_hnc_Chapter_List.Count}");

                   
                    int orderIndex = 0;

                    #region 循环章节列表
                    foreach (HtmlNode htmlNode in _hnc_Chapter_List)
                    {
                        orderIndex++;
                        HtmlDocument docOne = new HtmlDocument();
                        docOne.LoadHtml(htmlNode.InnerHtml);

                        ChapterTaskModel chapterTask = new ChapterTaskModel();
                        chapterTask.BookID = bookModel.ID;
                        chapterTask.BookKey = bookModel.Key;
                        //章节名称
                        WriteDebugLog($"┌获取章节Key");
                        string keyText = docOne.XPathInnerText(_BookSource.DetailXPathChapterKey, _BookSource.IsDebug);
                        chapterTask.Key = keyText.RegexText(_BookSource.DetailRegexChapterKey, "R", _BookSource.IsDebug);
                        WriteDebugLog($"└{chapterTask.Key}");
                        //章节名称
                        WriteDebugLog($"┌获取章节名称");
                        chapterTask.Name = docOne.XPathInnerText(_BookSource.DetailXPathChapterName, _BookSource.IsDebug);
                        WriteDebugLog($"└{chapterTask.Name}");
                        //章节链接
                        WriteDebugLog($"┌获取章节链接");
                        chapterTask.Link = docOne.XPathAttributeValue(_BookSource.DetailXPathChapterLink,_BookSource.DetailAttributeChapterLink, _BookSource.IsDebug);
                        WriteDebugLog($"└{chapterTask.Link}");
                        if (string.IsNullOrWhiteSpace(chapterTask.Key) && !string.IsNullOrWhiteSpace(chapterTask.Link))
                        {
                            WriteDebugLog($"┌从链接获取章节Key");
                            chapterTask.Key = chapterTask.Link.RegexText(_BookSource.DetailRegexChapterKey, "R", _BookSource.IsDebug);
                            WriteDebugLog($"└{chapterTask.Key}");
                        }
                        bookTask.ChapterTasks.Add(chapterTask);
                        if (_BookSource.IsDebug)
                            break;
                    } 
                    #endregion
                }

                return bookTask;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据封面链接获取封面
        /// </summary>
        /// <param name="posterLink"></param>
        /// <returns></returns>
        public byte[] BookPoster(string posterLink)
        {
            Url = posterLink;
            return GetImageByte();
        }

        /// <summary>
        /// 获取章节信息
        /// </summary>
        /// <param name="chapterTask">章节任务</param>
        public ChapterModel GetChapter(ChapterTaskModel chapterTask)
        {
            Url = chapterTask.Link;
            WriteDebugLog($"访问链接:{Url}");
            GetHtmlContent();
            WriteDebugLog($"开始解析网页内容");
            HtmlDocument _doc_Main = new HtmlDocument();
            _doc_Main.LoadHtml(HtmlContent);
            //判断是否有数据
            if (_doc_Main.Text == "")
                return null;
            WriteDebugLog($"{_doc_Main.Text}");
            ChapterModel chapterModel = new ChapterModel();
            chapterModel.Key = chapterTask.Key;
            chapterModel.Link = chapterTask.Link;
            chapterModel.Name = chapterTask.Name;
            chapterModel.BookID = chapterTask.BookID;
            chapterModel.BookKey = chapterTask.BookKey;
            chapterModel.ID = Guid.NewGuid();
            //章节名称
            if (string.IsNullOrWhiteSpace(chapterModel.Name))
            {
                WriteDebugLog($"┌从详细页获取章节名称");
                chapterModel.Name = _doc_Main.XPathInnerText(_BookSource.ChapterXPathName);
                WriteDebugLog($"└{chapterModel.Name}");
            }
            //获取章节内容
            try
            {
                WriteDebugLog($"┌从详细页获取章节内容");
                HtmlNode htmlNodeContent = _doc_Main.DocumentNode.SelectSingleNode(_BookSource.ChapterXPathContent);
                chapterModel.Content = htmlNodeContent.InnerHtml;
                WriteDebugLog($"└{chapterModel.Content}");
            }
            catch(Exception ex)
            {
                WriteDebugLog($"└{ex.Message}");
            }
            if (string.IsNullOrWhiteSpace(chapterModel.Content))
            {
                WriteDebugLog($"┌正则表达式匹配章节内容");
                chapterModel.Content = _doc_Main.Text.RegexText(_BookSource.ChapterRegexContent);
                WriteDebugLog($"└{chapterModel.Content}");
            }
            return chapterModel;
        }
        /// <summary>
        /// 写入日志，加判断委托是否为空
        /// </summary>
        /// <param name="logString"></param>
        private void WriteDebugLog(string logString)
        {
            if(OnWriteDebugLog!=null)
                OnWriteDebugLog(logString);
        }
        #endregion
    }
}
