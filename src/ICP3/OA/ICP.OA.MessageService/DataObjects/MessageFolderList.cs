#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/7/27 星期五 19:27:18
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Message.ServiceInterface
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class MessageFolderList : BaseDataObject
    {
        public override bool IsNew { get { return ID == Guid.Empty; } }

        Guid _id;
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    base.OnPropertyChanged("ID", value);
                }
            }
        }


        Guid _userid;
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserID
        {
            get
            {
                return _userid;
            }
            set
            {
                if (_userid != value)
                {
                    _userid = value;
                    base.OnPropertyChanged("UserID", value);
                }
            }
        }


        string _username;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return _username;
            }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    base.OnPropertyChanged("UserName", value);
                }
            }
        }


        string _name;
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(MaximumLength = 200, CMessage = "名称", EMessage = "Name")]
        [Required(CMessage = "名称", EMessage = "Name")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    base.OnPropertyChanged("Name", value);
                }
            }
        }

        MessageFolderType _type;
        /// <summary>
        /// 类型
        /// </summary>
        public MessageFolderType Type
        {
            get
            {
                return _type;
            }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    base.OnPropertyChanged("Type", value);
                }
            }
        }


        Guid? _parentid;
        /// <summary>
        /// 父ID
        /// </summary>
        public Guid? ParentID
        {
            get
            {
                return _parentid;
            }
            set
            {
                if (_parentid != value)
                {
                    _parentid = value;
                    base.OnPropertyChanged("ParentID", value);
                }
            }
        }


        string _parentname;
        /// <summary>
        /// 父
        /// </summary>
        public string ParentName
        {
            get
            {
                return _parentname;
            }
            set
            {
                if (_parentname != value)
                {
                    _parentname = value;
                    base.OnPropertyChanged("ParentName", value);
                }
            }
        }

        int _messagelogcount;
        /// <summary>
        /// 文件夹下日志数
        /// </summary>
        public int MessageLogCount
        {
            get
            {
                return _messagelogcount;
            }
            set
            {
                if (_messagelogcount != value)
                {
                    _messagelogcount = value;
                    base.OnPropertyChanged("MessageLogCount", value);
                }
            }
        }
        DateTime? _updateDate;
        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime? UpdateDate
        {
            get
            {
                return _updateDate;
            }
            set
            {
                if (_updateDate != value)
                {
                    _updateDate = value;
                    base.OnPropertyChanged("UpdateDate", value);
                }
            }
        }

        public override bool Equals(object obj)
        {
            MessageFolderList newObj = obj as MessageFolderList;
            if (newObj == null) return false;
            return newObj.ID == ID;
        }
    }
}
