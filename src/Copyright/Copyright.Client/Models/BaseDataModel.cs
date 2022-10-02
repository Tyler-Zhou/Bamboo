/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-02 14:44:09
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/

namespace Copyright.Client.Models
{
    /// <summary>
    /// 基础绑定数据模型
    /// </summary>
    public class BaseDataModel : BindableBase
    {
        #region 是否选中
        private bool _IsSelected = false;
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                _IsSelected = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 名称
        private string _Name = "";
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 描述
        private string _Description = "";
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
                RaisePropertyChanged();
            }
        }
        #endregion
    }
}
