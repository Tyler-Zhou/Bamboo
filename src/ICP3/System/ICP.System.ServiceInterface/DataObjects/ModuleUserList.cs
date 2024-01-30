using System;

namespace ICP.Sys.ServiceInterface.DataObjects
{
    /// <summary>
    /// 用户列表
    /// </summary>
    [Serializable]
    public class ModuleUserList:UserList
    {

        string _departmentname;
        /// <summary>
        /// 部门名
        /// </summary>
        public string DepartmentName
        {
            get
            {
                return _departmentname;
            }
            set
            {
                if (_departmentname != value)
                {
                    _departmentname = value;
                    base.OnPropertyChanged("DepartmentName", value);
                }
            }
        }


        Guid _departmentid;
        /// <summary>
        /// 部门ID
        /// </summary>
        public Guid DepartmentID
        {
            get
            {
                return _departmentid;
            }
            set
            {
                if (_departmentid != value)
                {
                    _departmentid = value;
                    base.OnPropertyChanged("DepartmentID", value);
                }
            }
        }

    }
}
