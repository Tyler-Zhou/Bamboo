/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-02 9:45:07
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Copyright.Client.Models
{
    /// <summary>
    /// Item Template 
    /// </summary>
    public class ItemTemplateModel: BindableBase
    {
        #region Key
        private string _Key = "";
        /// <summary>
        /// Key
        /// </summary>
        public string Key
        {
            get
            {
                return _Key;
            }
            set
            {
                _Key = value;
                RaisePropertyChanged(nameof(Key));
            }
        }
        #endregion

        #region Full Path
        private string _FullPath = "";
        /// <summary>
        /// Full Path
        /// </summary>
        public string FullPath
        {
            get
            {
                return _FullPath;
            }
            set
            {
                _FullPath = value;
                RaisePropertyChanged(nameof(FullPath));
            }
        }
        #endregion

        #region Original Content
        private string _OriginalContent = "";
        /// <summary>
        /// Original Content
        /// </summary>
        [IgnoreDataMember]
        public string OriginalContent
        {
            get
            {
                return _OriginalContent;
            }
            set
            {
                _OriginalContent = value;
                RaisePropertyChanged(nameof(OriginalContent));
            }
        }
        #endregion

        #region Customize Content
        private string _CustomizeContent = "";
        /// <summary>
        /// Customize Content
        /// </summary>
        [IgnoreDataMember]
        public string CustomizeContent
        {
            get
            {
                return _CustomizeContent;
            }
            set
            {
                _CustomizeContent = value;
                RaisePropertyChanged(nameof(CustomizeContent));
            }
        }
        #endregion
    }
}
