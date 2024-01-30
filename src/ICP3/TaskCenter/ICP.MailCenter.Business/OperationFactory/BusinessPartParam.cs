using System;
using System.Collections.Generic;
using System.Data;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;

namespace ICP.Operation.Common.ServiceInterface
{

    /// <summary>
    /// 业务面板需要传递的信息类
    /// </summary>
    public class BusinessPartParam
    {

        /// <summary>
        /// 视图模板Code
        /// </summary>
        public string TemplateCode
        {
            get;
            set;
        }
        /// <summary>
        /// 列表当前数据行
        /// </summary>
        public DataRow CurrentRow
        {
            get;
            set;
        }



       // public string TemplateCode { get; set; }

        /// <summary>
        /// 当前行业务号
        /// </summary>
        public string OperationNo
        {
            get;
            set;
        }
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid ID
        {
            get;
            set;
        }



        /// <summary>
        /// 列表中业务所属的业务类型,操作视图编码的第二部分代表业务类型
        /// </summary>
        public OperationType OperationType
        {
            get;
            set;
        }


        /// <summary>
        /// 面板当前关联的邮件信息
        /// </summary>
        public ICP.Message.ServiceInterface.Message CurrentMessage
        {
            get;
            set;
        }

        /// <summary>
        /// 当前业务面板
        /// </summary>
        public IBaseBusinessPart_New CurrentBusinessPart
        {
            set;
            get;
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? Updatetime
        {
            get;
            set;
        }
    }
}
