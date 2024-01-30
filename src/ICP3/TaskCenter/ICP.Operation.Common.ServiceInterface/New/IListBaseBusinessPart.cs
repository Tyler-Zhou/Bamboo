using System.Collections.Generic;
using ICP.Common.ServiceInterface;
using ICP.DataCache.ServiceInterface;
using System.Data;
using System.Drawing;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 业务列表面板接口
    /// </summary>
    public interface IListBaseBusinessPart : IBaseBusinessPart_New
    {
        #region Property
        /// <summary>
        /// 订舱号
        /// </summary>
        string SONO { get; }
        /// <summary>
        /// 加载数据时是否显示进度动画
        /// </summary>
        bool IsShowLoadingForm
        {
            get;
            set;
        }
        /// <summary>
        /// 列表行字体名称
        /// </summary>
        Font RowFont
        {
            get;
        }
        /// <summary>
        /// 业务上下文
        /// </summary>
        BusinessOperationContext BusinessOperationContext
        {
            get;
            set;
        }
        /// <summary>
        /// 当前数据行
        /// </summary>
        DataRow FocusedDataRow
        {
            get;
        }
        /// <summary>
        /// 事件类表操作类
        /// </summary>
        EventObjects EventObjects
        {
            get;
            set;
        }
        #endregion

        #region Method
        /// <summary>
        /// 保存列表自定义信息
        /// </summary>
        void SaveCustomColumnInfo();
        /// <summary>
        /// 获取列表自定义显示信息
        /// </summary>
        /// <returns></returns>
        UserCustomGridInfo GetUserCustomGridInfo();
        /// <summary>
        /// 获取列构建信息
        /// </summary>
        List<BusinessColumnInfo> GetColumnInfos(string templateCode);
        /// <summary>
        /// 设置列表数据源
        /// </summary>
        /// <param name="data"></param>
        void SetDataSource(object data);
        /// <summary>
        /// 设置获得焦点的行索引(可间接触发焦点行改变事件)
        /// </summary>
        /// <param name="rowHandle"></param>
        void SetFocusedRowHandle(int rowHandle);
        /// <summary>
        /// 保存列表数据更改
        /// </summary>
        /// <returns></returns>
        bool Save(); 
        #endregion
    }
}
