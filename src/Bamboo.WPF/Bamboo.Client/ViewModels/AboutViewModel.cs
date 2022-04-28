using Bamboo.Client.Models;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace Bamboo.Client.ViewModels
{
    /// <summary>
    /// 关于视图模型
    /// </summary>
    public class AboutViewModel : BindableBase
    {
        #region 相关主页列表
        /// <summary>
        /// Homepages
        /// </summary>
        private ObservableCollection<Homepage> _Homepages;
        /// <summary>
        /// 相关主页列表
        /// </summary>
        public ObservableCollection<Homepage> Homepages
        {
            get { return _Homepages; }
            set { _Homepages = value; RaisePropertyChanged(); }
        }
        #endregion

        /// <summary>
        /// 关于视图模型
        /// </summary>
        public AboutViewModel()
        {
            _Homepages = new ObservableCollection<Homepage>();
            CreateHomepages();
        }

        /// <summary>
        /// 添加相关主页列表
        /// </summary>
        void CreateHomepages()
        {
            Homepages.Add(new Homepage() { Text = "Github:", Link = "https://github.com/HenJigg/MyToDoApp" });
            Homepages.Add(new Homepage() { Text = "bilibili:", Link = "https://www.bilibili.com/video/BV1nY411a7T8" });
        }

    }
}
