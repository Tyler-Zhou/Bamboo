using Bamboo.Client.Core.Extensions;
using Bamboo.Client.Core.Interface;
using Bamboo.Client.Core.ViewModels;
using Bamboo.Library.Client.Interface;
using Bamboo.Library.Common.Dto;
using Bamboo.Library.Common.Parameter;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Bamboo.Library.Client.ViewModels
{
    /// <summary>
    /// 章节视图模型
    /// </summary>
    public class ChapterViewModel : NavigationViewModel
    {
        #region 上级Dto
        /// <summary>
        /// ParentDto
        /// </summary>
        private BookDto _ParentDto = new BookDto();
        /// <summary>
        /// 上级Dto
        /// </summary>
        public BookDto ParentDto
        {
            get { return _ParentDto; }
            set { _ParentDto = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 下拉列表选中状态值
        /// <summary>
        /// SelectedIndex
        /// </summary>
        private int _SelectedIndex = 0;
        /// <summary>
        /// 下拉列表选中状态值
        /// </summary>
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set { _SelectedIndex = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 搜索条件
        /// <summary>
        /// Search
        /// </summary>
        private string _Search = "";
        /// <summary>
        /// 搜索条件
        /// </summary>
        public string Search
        {
            get { return _Search; }
            set { _Search = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 右侧编辑窗口是否展开
        /// <summary>
        /// IsRightDrawerOpen
        /// </summary>
        private bool _IsRightDrawerOpen = false;

        /// <summary>
        /// 右侧编辑窗口是否展开
        /// </summary>
        public bool IsRightDrawerOpen
        {
            get { return _IsRightDrawerOpen; }
            set { _IsRightDrawerOpen = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 编辑选中/新增时对象
        /// <summary>
        /// CurrentDto
        /// </summary>
        private ChapterDto _CurrentDto = new ChapterDto();
        /// <summary>
        /// 编辑选中/新增时对象
        /// </summary>
        public ChapterDto CurrentDto
        {
            get { return _CurrentDto; }
            set { _CurrentDto = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 章节集合
        /// <summary>
        /// ChapterDtos
        /// </summary>
        private ObservableCollection<ChapterDto> _ChapterDtos = new ObservableCollection<ChapterDto>();
        /// <summary>
        /// 章节集合
        /// </summary>
        public ObservableCollection<ChapterDto> ChapterDtos
        {
            get { return _ChapterDtos; }
            set { _ChapterDtos = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 服务(Service)
        /// <summary>
        /// 章节服务
        /// </summary>
        private readonly IChapterService _ChapterService;
        /// <summary>
        /// 对话框主机服务
        /// </summary>
        private readonly IDialogHostService _DialogHostService;
        #endregion

        #region 命令
        /// <summary>
        /// 执行所有命令，根据类型参数选择不同操作
        /// </summary>
        public DelegateCommand<string> ExecuteCommand { get; private set; }
        /// <summary>
        /// 选择
        /// </summary>
        public DelegateCommand<ChapterDto> SelectedCommand { get; private set; }
        /// <summary>
        /// 删除
        /// </summary>
        public DelegateCommand<ChapterDto> DeleteCommand { get; private set; }
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 章节视图模型
        /// </summary>
        /// <param name="bookService"></param>
        /// <param name="provider"></param>
        /// <param name="logger"></param>
        public ChapterViewModel(IChapterService bookService, IContainerProvider provider)
            : base(provider)
        {
            _ChapterService = bookService;
            _DialogHostService = provider.Resolve<IDialogHostService>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            SelectedCommand = new DelegateCommand<ChapterDto>(Selected);
            DeleteCommand = new DelegateCommand<ChapterDto>(Delete);
        }
        #endregion

        #region 重写方法(Override)
        /// <summary>
        /// 导航到当前窗体时
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            if (!navigationContext.Parameters.ContainsKey("BookDto"))
            {
                throw new Exception("获取书籍Dto错误");
            }
            ParentDto = navigationContext.Parameters.GetValue<BookDto>("BookDto");
            GetDataAsync();
        }
        #endregion

        #region 方法(Method)

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj"></param>
        private async void Delete(ChapterDto obj)
        {
            try
            {
                var dialogResult = await _DialogHostService.Question("温馨提示", $"确认删除章节:{obj.Name} ?");
                if (dialogResult.Result != Prism.Services.Dialogs.ButtonResult.OK) return;

                UpdateLoading(true);
                var deleteResult = await _ChapterService.DeleteAsync(obj.Id);
                if (deleteResult.Status)
                {
                    var model = ChapterDtos.FirstOrDefault(t => t.Id.Equals(obj.Id));
                    if (model != null)
                        ChapterDtos.Remove(model);
                }
            }
            finally
            {
                UpdateLoading(false);
            }
        }
        /// <summary>
        /// 选择数据(编辑/查看)
        /// </summary>
        /// <param name="obj"></param>
        private async void Selected(ChapterDto obj)
        {
            try
            {
                UpdateLoading(true);
                var todoResult = await _ChapterService.GetFirstOfDefaultAsync(obj.Id);
                if (todoResult.Status)
                {
                    CurrentDto = todoResult.Result;
                    IsRightDrawerOpen = true;
                }
            }
            finally
            {
                UpdateLoading(false);
            }
        }
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="obj"></param>
        private void Execute(string obj)
        {
            switch (obj)
            {
                case "新增": Add(); break;
                case "查询": GetDataAsync(); break;
                case "保存": Save(); break;
            }
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        private void Add()
        {
            CurrentDto = new ChapterDto();
            IsRightDrawerOpen = true;
        }
        /// <summary>
        /// 保存
        /// </summary>
        private async void Save()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(CurrentDto.Key)
                || string.IsNullOrWhiteSpace(CurrentDto.Name)
                || string.IsNullOrWhiteSpace(CurrentDto.Content)
                )
                    return;

                UpdateLoading(true);
                if (CurrentDto.Id > 0)
                {
                    var updateResult = await _ChapterService.UpdateAsync(CurrentDto);
                    if (updateResult.Status)
                    {
                        var updateData = ChapterDtos.FirstOrDefault(t => t.Id == CurrentDto.Id);
                        if (updateData != null)
                        {
                            updateData.Key = CurrentDto.Key;
                            updateData.Name = CurrentDto.Name;
                            updateData.Content = CurrentDto.Content;
                            updateData.Link = CurrentDto.Link;
                        }
                        else
                        {
                            SendMessage(updateResult.Message);
                        }
                    }
                    IsRightDrawerOpen = false;
                }
                else
                {
                    CurrentDto.BookKey = ParentDto.Key;

                    var addResult = await _ChapterService.AddAsync(CurrentDto);
                    if (addResult.Status)
                    {
                        ChapterDtos.Add(addResult.Result);
                        IsRightDrawerOpen = false;
                    }
                    else
                    {
                        SendMessage(addResult.Message);
                    }
                }
            }
            finally
            {
                UpdateLoading(false);
            }
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        async void GetDataAsync()
        {
            try
            {
                UpdateLoading(true);

                int? Status = SelectedIndex == 0 ? null : SelectedIndex == 2 ? 1 : 0;

                var todoResult = await _ChapterService.GetAllFilterAsync(new ChapterParameter()
                {
                    PageIndex = 0,
                    PageSize = 100,
                    Search = Search,
                    BookKey = ParentDto.Key,
                });

                if (todoResult.Status)
                {
                    ChapterDtos.Clear();
                    foreach (var item in todoResult.Result.Items)
                    {
                        ChapterDtos.Add(item);
                    }
                }
            }
            finally
            {
                UpdateLoading(false);
            }
        }
        #endregion
    }
}
