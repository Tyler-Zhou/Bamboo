using Prism.Mvvm;
using System;

namespace CRM.Client.Models
{
    /// <summary>
    /// 模型基类
    /// </summary>
    
    public class BaseInfo : BindableBase
    {
        #region 主键
        private long _id = 0;
        /// <summary>
        /// 主键
        /// </summary>
        public long ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                RaisePropertyChanged(nameof(ID));
            }
        }
        #endregion

        #region 创建时间
        private DateTime _CreateTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get
            {
                return _CreateTime;
            }
            set
            {
                _CreateTime = value;
                RaisePropertyChanged(nameof(CreateTime));
            }
        }
        #endregion

        #region 创建人
        private long _CreateUserId = 0;
        /// <summary>
        /// 创建人
        /// </summary>
        public long CreateUserId
        {
            get
            {
                return _CreateUserId;
            }
            set
            {
                _CreateUserId = value;
                RaisePropertyChanged(nameof(CreateUserId));
            }
        }
        #endregion

        #region 修改时间
        private DateTime _ModifyTime;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime
        {
            get
            {
                return _ModifyTime;
            }
            set
            {
                _ModifyTime = value;
                RaisePropertyChanged(nameof(ModifyTime));
            }
        }
        #endregion

        #region 修改人
        private long? _ModifyUserId = 0;
        /// <summary>
        /// 修改人
        /// </summary>
        public long? ModifyUserId
        {
            get
            {
                return _ModifyUserId;
            }
            set
            {
                _ModifyUserId = value;
                RaisePropertyChanged(nameof(ModifyUserId));
            }
        }
        #endregion

        #region 删除时间
        private DateTime _DeleteTime;
        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime DeleteTime
        {
            get
            {
                return _DeleteTime;
            }
            set
            {
                _DeleteTime = value;
                RaisePropertyChanged(nameof(DeleteTime));
            }
        }
        #endregion

        #region 删除人
        private long? _DeleteUserId = 0;
        /// <summary>
        /// 删除人
        /// </summary>
        public long? DeleteUserId
        {
            get
            {
                return _DeleteUserId;
            }
            set
            {
                _DeleteUserId = value;
                RaisePropertyChanged(nameof(DeleteUserId));
            }
        }
        #endregion
    }
}
