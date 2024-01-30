using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;

namespace ICP.TaskCenter.ServiceInterface
{
    /// <summary>
    /// 修改业务所需要传递的对象类
    /// 创建人：joe
    /// 创建时间：2013-05-24
    /// </summary>
    [Serializable]
    public class UpdateBuisinessParam
    {
        /// <summary>
        /// 
        /// </summary>
        List<Guid> _IDList = new List<Guid>();

        /// <summary>
        /// 业务ID列表
        /// </summary>
        public List<Guid> IDList
        {
            get { return _IDList; }
        }
        /// <summary>
        /// 业务类型
        /// </summary>
        public OperationType OperationType
        {
            get;
            set;
        }

        //用户ID
        public Guid UserID
        {
            get;
            set;
        }

        List<string> _SoNoList = new List<string>();

        /// <summary>
        /// 订舱号列表
        /// </summary>
        public List<string> SoNoList
        {
            get { return _SoNoList; }
        }

        List<string> _ContainerDesc = new List<string>();

        /// <summary>
        /// 箱描述
        /// </summary>
        public List<string> ContainerDescList
        {
            get { return _ContainerDesc; }
        }

        List<string> _carrierList= new List<string>();

        /// <summary>
        /// 船东
        /// </summary>
        public List<string> CarrierList
        {
            get { return _carrierList; }
        }

        List<string> _remarkList = new List<string>();

        /// <summary>
        /// 备注
        /// </summary>
        public List<string> RemarkList
        {
            get { return _remarkList; }
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="fieldName">列名称</param>
        /// <param name="val">值</param>
        public void AddData(string fieldName, object val)
        {

            switch (fieldName.ToLower())
            {
                case "id":
                    IDList.Add((Guid)val);
                    break;
                case "sono":
                    SoNoList.Add(val as string );
                    break;
                case "containerdesc":
                    ContainerDescList.Add(val as string);
                    break;

                case "carrier":
                    CarrierList.Add(val as string);
                    break;
                case "remark":
                    RemarkList.Add(val as string);
                    break;
            }

        }

    }
}
