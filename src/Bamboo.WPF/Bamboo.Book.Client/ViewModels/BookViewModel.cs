using Bamboo.Book.Client.Interface;
using Bamboo.Book.Common.Dto;
using Bamboo.Book.Common.Parameter;
using Bamboo.Client.Core.Extensions;
using Bamboo.Client.Core.Interface;
using Bamboo.Client.Core.ViewModels;
using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Bamboo.Book.Client.ViewModels
{
    /// <summary>
    /// 书籍视图模型
    /// </summary>
    public class BookViewModel : NavigationViewModel
    {
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
        private BookDto _CurrentDto = new BookDto();
        /// <summary>
        /// 编辑选中/新增时对象
        /// </summary>
        public BookDto CurrentDto
        {
            get { return _CurrentDto; }
            set { _CurrentDto = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 书籍集合
        /// <summary>
        /// BookDtos
        /// </summary>
        private ObservableCollection<BookDto> _BookDtos = new ObservableCollection<BookDto>();
        /// <summary>
        /// 书籍集合
        /// </summary>
        public ObservableCollection<BookDto> BookDtos
        {
            get { return _BookDtos; }
            set { _BookDtos = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 服务(Service)
        /// <summary>
        /// 书籍服务
        /// </summary>
        private readonly IBookService _BookService;
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
        public DelegateCommand<BookDto> SelectedCommand { get; private set; }
        /// <summary>
        /// 删除
        /// </summary>
        public DelegateCommand<BookDto> DeleteCommand { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 书籍视图模型
        /// </summary>
        /// <param name="bookService"></param>
        /// <param name="provider"></param>
        /// <param name="logger"></param>
        public BookViewModel(IBookService bookService, IContainerProvider provider)
            : base(provider)
        {
            _BookService = bookService;
            _DialogHostService = provider.Resolve<IDialogHostService>();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            SelectedCommand = new DelegateCommand<BookDto>(Selected);
            DeleteCommand = new DelegateCommand<BookDto>(Delete);
        }
        #endregion

        #region Method
        /// <summary>
        /// 导航到当前窗体时
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            if (navigationContext.Parameters.ContainsKey("Value"))
                SelectedIndex = navigationContext.Parameters.GetValue<int>("Value");
            else
                SelectedIndex = 0;
            GetDataAsync();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj"></param>
        private async void Delete(BookDto obj)
        {
            try
            {
                var dialogResult = await _DialogHostService.Question("温馨提示", $"确认删除书籍:{obj.Name} ?");
                if (dialogResult.Result != Prism.Services.Dialogs.ButtonResult.OK) return;

                UpdateLoading(true);
                var deleteResult = await _BookService.DeleteAsync(obj.Id);
                if (deleteResult.Status)
                {
                    var model = BookDtos.FirstOrDefault(t => t.Id.Equals(obj.Id));
                    if (model != null)
                        BookDtos.Remove(model);
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
        private async void Selected(BookDto obj)
        {
            try
            {
                UpdateLoading(true);
                var todoResult = await _BookService.GetFirstOfDefaultAsync(obj.Id);
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
            CurrentDto = new BookDto();
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
                || string.IsNullOrWhiteSpace(CurrentDto.Author)
                || string.IsNullOrWhiteSpace(CurrentDto.Link)
                )
                    return;

                UpdateLoading(true);
                if (CurrentDto.Id > 0)
                {
                    var updateResult = await _BookService.UpdateAsync(CurrentDto);
                    if (updateResult.Status)
                    {
                        var updateData = BookDtos.FirstOrDefault(t => t.Id == CurrentDto.Id);
                        if (updateData != null)
                        {
                            updateData.Key = CurrentDto.Key;
                            updateData.Name = CurrentDto.Name;
                            updateData.Author = CurrentDto.Author;
                            updateData.Link = CurrentDto.Link;
                            updateData.Tag = CurrentDto.Tag;
                            updateData.Introduction = CurrentDto.Introduction;
                            updateData.Status = CurrentDto.Status;
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
                    var addResult = await _BookService.AddAsync(CurrentDto);
                    if (addResult.Status)
                    {
                        BookDtos.Add(addResult.Result);
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

                var todoResult = await _BookService.GetAllFilterAsync(new BookParameter()
                {
                    PageIndex = 0,
                    PageSize = 100,
                    Search = Search,
                    Status = Status
                });

                if (todoResult.Status)
                {
                    BookDtos.Clear();
                    foreach (var item in todoResult.Result.Items)
                    {
                        BookDtos.Add(item);
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
