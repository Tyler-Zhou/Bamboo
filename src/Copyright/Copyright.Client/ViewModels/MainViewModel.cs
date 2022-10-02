/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-02 9:40:54
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/
using Copyright.Client.Helpers;
using Copyright.Client.Models;
using Copyright.Client.Services;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Copyright.Client.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class MainViewModel : BindableBase
    {
        #region Members

        #region Current Item
        private ItemTemplateModel _CurrentItem;
        /// <summary>
        /// Current Item
        /// </summary>
        public ItemTemplateModel CurrentItem
        {
            get
            {
                if (_CurrentItem == null && CurrentCollection.Count > 0)
                    _CurrentItem = CurrentCollection.FirstOrDefault();
                return _CurrentItem;
            }
            set
            {
                _CurrentItem = value;
                RaisePropertyChanged(nameof(CurrentItem));
            }
        }
        #endregion

        #region Current Collection
        private ObservableCollection<ItemTemplateModel> _CurrentCollection;
        /// <summary>
        /// Current Collection
        /// </summary>
        public ObservableCollection<ItemTemplateModel> CurrentCollection
        {
            get
            {
                if (_CurrentCollection == null)
                    _CurrentCollection = new ObservableCollection<ItemTemplateModel>();
                return _CurrentCollection;
            }
            set
            {
                _CurrentCollection = value;
                RaisePropertyChanged(nameof(CurrentCollection));
            }
        }
        #endregion

        #region Select Template
        private BaseDataModel _SelectTemplate;
        /// <summary>
        /// Select Template
        /// </summary>
        public BaseDataModel SelectTemplate
        {
            get
            {
                if (_SelectTemplate == null && Templates != null && Templates.Count > 0)
                {
                    _SelectTemplate = Templates.FirstOrDefault();
                }
                return _SelectTemplate;
            }
            set
            {
                _SelectTemplate = value;
                RaisePropertyChanged(nameof(SelectTemplate));
            }
        }
        #endregion

        #region Template Collection
        private ObservableCollection<BaseDataModel> _Templates;
        /// <summary>
        /// Template Collection
        /// </summary>
        public ObservableCollection<BaseDataModel> Templates
        {
            get
            {
                if (_Templates == null)
                    _Templates = new ObservableCollection<BaseDataModel>();
                return _Templates;
            }
            set
            {
                _Templates = value;
                RaisePropertyChanged(nameof(Templates));
            }
        }
        #endregion

        #endregion

        #region Services

        /// <summary>
        /// ItemTemplate Service
        /// </summary>
        ItemTemplateService _ItemTemplateService = null;
        /// <summary>
        /// Template Service
        /// </summary>
        TemplateService _TemplateService = null;
        #endregion

        #region Commands
        public DelegateCommand SelectPathCommand { get; private set; }
        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand RemoveCommand { get; private set; }
        public DelegateCommand SaveCommand { get; private set; }
        public DelegateCommand WriteCommand { get; private set; }
        public DelegateCommand SaveTemplateCommand { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public MainViewModel()
        {
            _ItemTemplateService=new ItemTemplateService();
            _TemplateService = new TemplateService();
            SelectPathCommand = new DelegateCommand(SelectPathExecute);
            AddCommand = new DelegateCommand(AddExecute);
            RemoveCommand = new DelegateCommand(RemoveExecute);
            SaveCommand = new DelegateCommand(SaveExecute);
            WriteCommand = new DelegateCommand(WriteExecute);
            SaveTemplateCommand = new DelegateCommand(SaveTemplateExecute);

            InitData();
        }
        #endregion

        #region Private Method

        void InitData()
        {
            CurrentCollection = _ItemTemplateService.GetCollection();
            RaisePropertyChanged(nameof(CurrentCollection));
            Templates = _TemplateService.Get();
            RaisePropertyChanged(nameof(Templates));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        void SelectPathExecute(object obj)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "cs documents (.cs)|*.cs",
                InitialDirectory = CopyrightHelper.GetItemTemplatePath(),
                Multiselect = false,
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                CurrentItem.FullPath = openFileDialog.FileName;
                CurrentItem.OriginalContent = CopyrightHelper.GetContent(CurrentItem.FullPath);
                CurrentItem.CustomizeContent = CopyrightHelper.CustomizeTemplate(CurrentItem.OriginalContent, Templates);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        void AddExecute(object obj)
        {
            if (InvalidData())
                return;
            SaveExecute(null);
            ItemTemplateModel model = new ItemTemplateModel();
            CurrentCollection.Add(model);
            CurrentItem = model;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        void SaveExecute(object obj)
        {
            _ItemTemplateService.SaveCollection(CurrentCollection);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        void RemoveExecute(object obj)
        {
            CurrentCollection.Remove(CurrentItem);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        void WriteExecute(object obj)
        {
            try
            {
                foreach (ItemTemplateModel item in CurrentCollection)
                {
                    File.WriteAllText(item.FullPath, item.CustomizeContent);
                }
            }
            catch
            {
                
            }
        }
        /// <summary>
        /// Invalid Data
        /// </summary>
        /// <returns></returns>
        bool InvalidData()
        {
            if (CurrentItem == null)
                return false;
            if (string.IsNullOrWhiteSpace(CurrentItem.Key)
                || string.IsNullOrWhiteSpace(CurrentItem.FullPath)
                || string.IsNullOrWhiteSpace(CurrentItem.OriginalContent)
                || string.IsNullOrWhiteSpace(CurrentItem.CustomizeContent)
                )
                return true;
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        void SaveTemplateExecute(object obj)
        {
            _TemplateService.Save(Templates);
        }
        #endregion
    }
}
