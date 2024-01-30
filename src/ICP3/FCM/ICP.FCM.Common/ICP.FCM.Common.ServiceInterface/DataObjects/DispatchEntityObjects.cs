#region Comment

/*
 * 
 * FileName:    DispatchEntity.cs
 * CreatedOn:   2015/10/26 17:20:54
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      -> 分发实体：包含分发文件及其分发数据
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    /// <summary>
    /// 分发实体对象
    /// </summary>
    [Serializable]
    public class DispatchEntityObjects : BaseDataObject
    {
        /// <summary>
        /// 记录ID
        /// </summary>
        public Guid LogID { get; set; }

        #region 分发文件列表
        private List<DispatchFile> dispatchFileList;
        /// <summary>
        /// 分发文件列表
        /// </summary>
        public List<DispatchFile> DispatchFileList
        {
            get { return dispatchFileList ?? (dispatchFileList = new List<DispatchFile>()); }
            set { dispatchFileList = value; }
        } 
        #endregion

        #region 数据集合
        private DataSet dataList;
        /// <summary>
        /// 数据集合
        /// </summary>
        public DataSet DataList
        {
            get { return dataList ?? (dataList = new DataSet()); }
            set { dataList = value; }
        } 
        #endregion
    }
}
