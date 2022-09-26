using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using Reader.Client.Models;
using System.Collections.ObjectModel;
using System;
using System.Windows.Forms;
using System.Linq;
using Reader.Client.Interfaces;
using System.Threading.Tasks;
using Reader.Client.Services;
using System.Collections.Generic;
using Reader.Client.Extensions;
using System.Text;
using System.Reflection;
using Reader.Client.Common;

namespace Reader.Client.ViewModels
{
    /// <summary>
    /// 导出视图模型
    /// </summary>
    public class ExportViewModel : BaseViewModel
    {
        #region 成员(Member)
        #region 导出路径
        /// <summary>
        /// 导出路径
        /// </summary>
        public string ExportPath
        {
            get { return ReaderContext.Setting.LastExportPath; }
            set
            {
                ReaderContext.Setting.LastExportPath = value;
                RaisePropertyChanged(nameof(ExportPath));
            }
        }
        #endregion

        #region 当前书籍
        private BookModel _CurrentBook;
        /// <summary>
        /// 当前书籍
        /// </summary>
        public BookModel CurrentBook
        {
            get
            {
                if (Books != null && Books.Count > 0)
                {
                    _CurrentBook = Books.FirstOrDefault();
                }
                return _CurrentBook;
            }
            set
            {
                _CurrentBook = value;
                RaisePropertyChanged(nameof(CurrentBook));
            }
        }
        #endregion

        #region 书籍集合
        private ObservableCollection<BookModel> _Books;
        /// <summary>
        /// 书籍集合
        /// </summary>
        public ObservableCollection<BookModel> Books
        {
            get
            {
                return _Books;
            }
            set
            {
                _Books = value;
                RaisePropertyChanged(nameof(Books));
            }
        }
        #endregion

        #region 电子书类型选中项
        private BaseDataModel _SelectBookType = null;
        /// <summary>
        /// 电子书类型选中项
        /// </summary>
        public BaseDataModel SelectBookType
        {
            get
            {
                if (_SelectBookType ==null && BookTypes != null && BookTypes.Count>0)
                {
                    _SelectBookType = BookTypes.FirstOrDefault();
                }
                return _SelectBookType;
            }
            set
            {
                _SelectBookType = value;
                RaisePropertyChanged(nameof(SelectBookType));
            }
        }
        #endregion

        #region 电子书类型集合
        private ObservableCollection<BaseDataModel> _BookTypes;
        /// <summary>
        /// 电子书类型集合
        /// </summary>
        public ObservableCollection<BaseDataModel> BookTypes
        {
            get
            {
                if (_BookTypes == null)
                    _BookTypes = new ObservableCollection<BaseDataModel>();
                if (_BookTypes.Count <= 0)
                {
                    _BookTypes.Add(new BaseDataModel() { Name = "EPUB", Description = "EPUB" });
                    _BookTypes.Add(new BaseDataModel() { Name = "AWZ3", Description = "AWZ3" });
                }
                return _BookTypes;
            }
            set
            {
                _BookTypes = value;
                RaisePropertyChanged(nameof(BookTypes));
            }
        }
        #endregion

        #region 书籍属性集
        private string _BookExample = "";
        /// <summary>
        /// 书籍属性集
        /// </summary>
        public string BookExample
        {
            get { return _BookExample; }
            set
            {
                _BookExample = value;
                RaisePropertyChanged(nameof(BookExample));
            }
        }
        #endregion

        #region 章节属性集
        private string _ChapterExample = "";
        /// <summary>
        /// 章节属性集
        /// </summary>
        public string ChapterExample
        {
            get { return _ChapterExample; }
            set
            {
                _ChapterExample = value;
                RaisePropertyChanged(nameof(ChapterExample));
            }
        }
        #endregion

        #region 选中的CSS样式
        private BaseDataModel _SelectCSSStyle;
        /// <summary>
        /// 选中的CSS样式
        /// </summary>
        public BaseDataModel SelectCSSStyle
        {
            get
            {
                if (_SelectCSSStyle==null && CSSStyles != null && CSSStyles.Count > 0)
                {
                    _SelectCSSStyle = CSSStyles.FirstOrDefault();
                }
                return _SelectCSSStyle;
            }
            set
            {
                _SelectCSSStyle = value;
                RaisePropertyChanged(nameof(SelectCSSStyle));
            }
        }
        #endregion

        #region CSS样式集合
        private ObservableCollection<BaseDataModel> _CSSStyles;
        /// <summary>
        /// CSS样式集合
        /// </summary>
        public ObservableCollection<BaseDataModel> CSSStyles
        {
            get
            {
                if (_CSSStyles == null)
                    _CSSStyles = new ObservableCollection<BaseDataModel>();
                return _CSSStyles;
            }
            set
            {
                _CSSStyles = value;
                RaisePropertyChanged(nameof(CSSStyles));
            }
        }
        #endregion

        #region 首页模板
        private BaseDataModel _IndexTemplate;
        /// <summary>
        /// 首页模板
        /// </summary>
        public BaseDataModel IndexTemplate
        {
            get
            {
                if (_IndexTemplate == null)
                    _IndexTemplate = new BaseDataModel() {IsSelected=true,Name= "Index" };
                return _IndexTemplate;
            }
            set
            {
                _IndexTemplate = value;
                RaisePropertyChanged(nameof(IndexTemplate));
            }
        }
        #endregion

        #region 详细模板
        private BaseDataModel _DetailTemplate;
        /// <summary>
        /// 详细模板
        /// </summary>
        public BaseDataModel DetailTemplate
        {
            get
            {
                if (_DetailTemplate == null)
                    _DetailTemplate = new BaseDataModel() { IsSelected = true,Name="Detail" };
                return _DetailTemplate;
            }
            set
            {
                _DetailTemplate = value;
                RaisePropertyChanged(nameof(DetailTemplate));
            }
        }
        #endregion

        #region 选中的替换规则
        private BaseDataModel _SelectReplaceRule;
        /// <summary>
        /// 选中的替换规则
        /// </summary>
        public BaseDataModel SelectReplaceRule
        {
            get
            {
                if (_SelectReplaceRule==null && ReplaceRules != null && ReplaceRules.Count > 0)
                {
                    _SelectReplaceRule = ReplaceRules.FirstOrDefault();
                }
                return _SelectReplaceRule;
            }
            set
            {
                _SelectReplaceRule = value;
                RaisePropertyChanged(nameof(SelectReplaceRule));
            }
        }
        #endregion

        #region 替换规则集合
        private ObservableCollection<BaseDataModel> _ReplaceRules;
        /// <summary>
        /// 替换规则集合
        /// </summary>
        public ObservableCollection<BaseDataModel> ReplaceRules
        {
            get
            {
                if (_ReplaceRules == null)
                    _ReplaceRules = new ObservableCollection<BaseDataModel>();
                return _ReplaceRules;
            }
            set
            {
                _ReplaceRules = value;
                RaisePropertyChanged(nameof(ReplaceRules));
            }
        }
        #endregion

        #endregion

        #region 服务(Service)
        /// <summary>
        /// 书籍服务
        /// </summary>
        IBookService _BookService;
        /// <summary>
        /// 模板服务
        /// </summary>
        ITemplateService _TemplateService;
        /// <summary>
        /// 章节服务
        /// </summary>
        IChapterService _ChapterService;
        #endregion

        #region 命令(Commands)
        /// <summary>
        /// 添加源
        /// </summary>
        public DelegateCommand SelectPathCommand { get; private set; }
        /// <summary>
        /// 导出电子书
        /// </summary>
        public DelegateCommand ExportCommand { get; private set; }
        /// <summary>
        /// 添加样式
        /// </summary>
        public DelegateCommand AddStyleCommand { get; private set; }
        /// <summary>
        /// 移除样式
        /// </summary>
        public DelegateCommand RemoveStyleCommand { get; private set; }
        /// <summary>
        /// 添加替换规则
        /// </summary>
        public DelegateCommand AddReplaceRuleCommand { get; private set; }
        /// <summary>
        /// 移除替换规则
        /// </summary>
        public DelegateCommand RemoveReplaceRuleCommand { get; private set; }
        /// <summary>
        /// 保存模板
        /// </summary>
        public DelegateCommand SaveCommand { get; private set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 导出视图模型
        /// </summary>
        /// <param name="containerProvider"></param>
        public ExportViewModel(IContainerProvider containerProvider) : base(containerProvider)
        {
            _BookService = containerProvider.Resolve<IBookService>();
            _TemplateService = containerProvider.Resolve<ITemplateService>();
            _ChapterService = containerProvider.Resolve<IChapterService>();
            SelectPathCommand = new DelegateCommand(SelectPath);
            ExportCommand = new DelegateCommand(ExportElectronicBookResources);
            AddStyleCommand = new DelegateCommand(AddCSSStyle);
            RemoveStyleCommand = new DelegateCommand(RemoveCSSStyle);
            AddReplaceRuleCommand = new DelegateCommand(AddReplaceRule);
            RemoveReplaceRuleCommand = new DelegateCommand(RemoveReplaceRule);
            SaveCommand = new DelegateCommand(SaveTemplate);
            InitData();
        }
        #endregion

        #region 重写方法(Override)
        /// <summary>
        /// 是否可以处理请求的导航行为,当前视图/模型是否可以重用
        /// </summary>
        /// <param name="navigationContext">导航内容</param>
        /// <remarks>true:</remarks>
        /// <returns></returns>
        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }
        /// <summary>
        /// 从本页面转到其它页面时
        /// </summary>
        /// <param name="navigationContext">导航内容</param>
        /// <remarks>NavigationContext包含目标页面的URI</remarks>
        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
        /// <summary>
        /// 从其它页面导航至本页面时
        /// </summary>
        /// <param name="navigationContext">导航内容</param>
        /// <remarks>NavigationContext包含传递过来的参数</remarks>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
        #endregion

        #region 方法(Method)
        void InitData()
        {
            Books = _BookService.GetAll();
            CSSStyles = _TemplateService.GetCollection("StyleSheet");
            IndexTemplate = _TemplateService.GetSingle("Index");
            DetailTemplate = _TemplateService.GetSingle("Detail");
            ReplaceRules = _TemplateService.GetCollection("ReplaceRule");
            BookExample = BookExampleText();
            ChapterExample = ChapterExampleText();
        }
        /// <summary>
        /// 书籍举例文本
        /// </summary>
        /// <returns></returns>
        string BookExampleText()
        {
            StringBuilder stringBuilder = new StringBuilder();
            PropertyInfo[] properties = typeof(BookModel).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo item in properties)
            {
                stringBuilder.AppendLine($"{typeof(BookModel).Name}.{item.Name}");
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 章节举例文本
        /// </summary>
        /// <returns></returns>
        string ChapterExampleText()
        {
            StringBuilder stringBuilder = new StringBuilder();
            PropertyInfo[] properties = typeof(ChapterModel).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo item in properties)
            {
                stringBuilder.AppendLine($"{typeof(ChapterModel).Name}.{item.Name}");
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 选择路径
        /// </summary>
        void SelectPath()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                ExportPath = folderBrowserDialog.SelectedPath;
            }
        }

        /// <summary>
        /// 导出电子书所需资源
        /// </summary>
        void ExportElectronicBookResources()
        {
            if (string.IsNullOrWhiteSpace(ExportPath))
                return;
            Task.Run(() =>
            {
                try
                {
                    ShowInformation($"开始导出[{CurrentBook.Name}]");
                    string bookKey = CurrentBook.Key;
                    ObservableCollection<string> chapterKeys = _ChapterService.GetAllKey(bookKey);
                    if (chapterKeys != null && chapterKeys.Count > 0)
                    {
                        int index = 0;
                        string extensionName = ".xhtml";
                        string contentSrc = "<content src=\"$fileName$\"/>";
                        if ("AWZ3".Equals(SelectBookType.Name))
                        {
                            extensionName = ".html";
                            //Calibre 将 AWZ3 的 html 文件自动添加到text目录
                            contentSrc = "<content src=\"text/$fileName$\"/>";
                        }
                        DocumentService documentService = new DocumentService($"{ExportPath}\\{bookKey}\\");
                        try
                        {
                            documentService.StyleSheet("0001", CSSStyles.Where(item => item.IsSelected));
                        }
                        catch (Exception ex)
                        {
                            ShowError($"[{CurrentBook.Name}]导出样式表发生异常:{ex.Message}");
                        }
                        try
                        {
                            documentService.Poster("cover.jpg", CurrentBook.PosterContent);
                        }
                        catch (Exception ex)
                        {
                            ShowError($"[{CurrentBook.Name}]导出封面发生异常:{ex.Message}");
                        }

                        StringBuilder tocContent = new StringBuilder();
                        
                        string startFileName = index.Completion(4, "part", extensionName);

                        #region 目录标题
                        tocContent.AppendLine("<docTitle>");
                        tocContent.AppendLine($"<text>{CurrentBook.Name}</text>");
                        tocContent.AppendLine("</docTitle>");
                        tocContent.AppendLine("<navMap>");
                        #endregion

                        Dictionary<string, object> dicMain = new Dictionary<string, object>();
                        dicMain.Add(CurrentBook.GetType().Name, CurrentBook);

                        try
                        {
                            documentService.HtmlPage(startFileName, IndexTemplate, ReplaceRules.Where(item => item.IsSelected), dicMain);
                        }
                        catch (Exception ex)
                        {
                            ShowError($"[{CurrentBook.Name}]导出首页发生异常:{ex.Message}");
                        }

                        #region 首页目录
                        //<navPoint>
                        tocContent.AppendLine($"<navPoint id=\"num_{(index + 1)}\" playOrder=\"{(index + 1)}\">");
                        //<navLabel>
                        tocContent.AppendLine("<navLabel>");
                        //<text></text>
                        tocContent.AppendLine($"<text>首页</text>");
                        //</navLabel>
                        tocContent.AppendLine("</navLabel>");
                        //<content />
                        tocContent.AppendLine(contentSrc.Replace("$fileName$", startFileName));
                        tocContent.AppendLine("</navPoint>");
                        #endregion

                        List<BaseDataModel> replaceRules = ReplaceRules.Where(item => item.IsSelected).ToList();
                        foreach (string chapterKey in chapterKeys.OrderBy(item => item, new SemiNumericComparer()))
                        {
                            index++;
                            string fileName = index.Completion(4, "part", extensionName);
                            ChapterModel chapter = _ChapterService.SingleOrDefault(bookKey, chapterKey);
                            Dictionary<string, object> dicDetail = new Dictionary<string, object>();
                            dicDetail.Add(chapter.GetType().Name, chapter);
                            //小说中包含链接，移除后重新添加到隐藏链接中
                            BaseDataModel replaceRule = new BaseDataModel() { IsSelected = true, Name = chapter.Link, Description = "" };
                            replaceRules.Add(replaceRule);
                            try
                            {
                                documentService.HtmlPage(fileName, DetailTemplate, replaceRules, dicDetail);
                            }
                            catch (Exception ex)
                            {
                                ShowError($"[{CurrentBook.Name}]导出章节[{chapter.Name}]发生异常:{ex.Message}");
                            }
                            replaceRules.Remove(replaceRule);

                            #region 章节目录
                            //<navPoint>
                            tocContent.AppendLine($"<navPoint id=\"num_{index + 1}\" playOrder=\"{index + 1}\">");
                            //<navLabel>
                            tocContent.AppendLine("<navLabel>");
                            //<text></text>
                            tocContent.AppendLine($"<text>{chapter.Name}</text>");
                            //</navLabel>
                            tocContent.AppendLine("</navLabel>");
                            //<content />
                            tocContent.AppendLine(contentSrc.Replace("$fileName$", startFileName));
                            tocContent.AppendLine("</navPoint>");
                            #endregion
                        }
                        //</navMap>
                        tocContent.AppendLine("</navMap>");
                        documentService.TocFile("toc.txt", tocContent.ToString());

                        ShowInformation($"导出[{CurrentBook.Name}]完成");
                    }
                }
                catch (Exception ex)
                {
                    ShowError($"导出书籍[{CurrentBook.Name}]发生异常:{ex.Message}");
                }
            });
        }
        /// <summary>
        /// 无效样式
        /// </summary>
        /// <returns></returns>
        bool InvalidStyle()
        {
            if (SelectCSSStyle == null)
                return false;
            if (string.IsNullOrWhiteSpace(SelectCSSStyle.Name)
                || string.IsNullOrWhiteSpace(SelectCSSStyle.Description)
                )
                return true;
            return false;
        }
        /// <summary>
        /// 添加样式
        /// </summary>
        void AddCSSStyle()
        {
            if (InvalidStyle())
                return;
            SaveTemplate();
            BaseDataModel model = new BaseDataModel();
            model.IsSelected = true;
            CSSStyles.Insert(0, model);
            SelectCSSStyle = model;
        }
        /// <summary>
        /// 移除样式
        /// </summary>
        void RemoveCSSStyle()
        {
            CSSStyles.Remove(SelectCSSStyle);
        }
        /// <summary>
        /// 无效替换规则
        /// </summary>
        /// <returns></returns>
        bool InvalidReplaceRule()
        {
            if (SelectReplaceRule == null)
                return false;
            if (string.IsNullOrWhiteSpace(SelectReplaceRule.Name))
                return true;
            return false;
        }
        /// <summary>
        /// 添加替换规则
        /// </summary>
        void AddReplaceRule()
        {
            if (InvalidReplaceRule())
                return;
            SaveTemplate();
            BaseDataModel model = new BaseDataModel();
            model.IsSelected = true;
            ReplaceRules.Insert(0, model);
            SelectReplaceRule = model;
        }
        /// <summary>
        /// 移除替换规则
        /// </summary>
        void RemoveReplaceRule()
        {
            ReplaceRules.Remove(SelectReplaceRule);
        }
        /// <summary>
        /// 保存所有模板
        /// </summary>
        void SaveTemplate()
        {
            if (InvalidStyle() || InvalidReplaceRule())
                return;
            _TemplateService.SaveCollection("StyleSheet", CSSStyles);
            _TemplateService.SaveSingle(IndexTemplate);
            _TemplateService.SaveSingle(DetailTemplate);
            _TemplateService.SaveCollection("ReplaceRule", ReplaceRules);
        }
        #endregion
    }
}
