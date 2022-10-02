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
    /// ����������ģ��
    /// </summary>
    public class BaseDataModel : BindableBase
    {
        #region �Ƿ�ѡ��
        private bool _IsSelected = false;
        /// <summary>
        /// �Ƿ�ѡ��
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

        #region ����
        private string _Name = "";
        /// <summary>
        /// ����
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

        #region ����
        private string _Description = "";
        /// <summary>
        /// ����
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
