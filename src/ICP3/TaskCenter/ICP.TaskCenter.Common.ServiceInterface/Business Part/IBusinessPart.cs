using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Message.ServiceInterface;
using ICP.Framework.CommonLibrary.Server;
using System.Data;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Common.ServiceInterface.DataObjects;
namespace ICP.MailCenter.Business.ServiceInterface
{  
    /// <summary>
    /// 业务面板接口
    /// </summary>
   public interface IBusinessPart
    {  
       /// <summary>
       /// 行格式化所依据的列名
       /// </summary>
       string ConditionColumnName { get; set; }
       /// <summary>
       /// 发件人来源
       /// </summary>
       EmailSourceType SourceType { get; set; }
       /// <summary>
       /// 列表类型
       /// </summary>
       ListFormType ListType { get; set; }
       /// <summary>
       /// 行格式化所依据的单号
       /// </summary>
       List<string> Nos { get; set; }
       /// <summary>
       ///列表是否需要绑定数据 
       /// </summary>
       bool NeedBindData { get;set; }
       /// <summary>
       /// 初始化
       /// </summary>
       /// <param name="mail"></param>
       void Init(Message.ServiceInterface.Message mail);
       /// <summary>
       /// 数据绑定
       /// </summary>
       /// <param name="mail"></param>
       void BindData(ICP.Message.ServiceInterface.Message mail);
       /// <summary>
       /// 获取工具栏按钮数据
       /// </summary>
       List<OperationToolbarCommand> GetToolbarCommands();

       /// <summary>
       /// 获取列构建信息
       /// </summary>
       List<BusinessColumnInfo> GetColumnInfos();
       /// <summary>
       /// 获取列表自定义显示信息
       /// </summary>
       /// <returns></returns>
       UserCustomGridInfo GetUserCustomGridInfo();
       /// <summary>
       /// 获取具体业务类型公共上下文菜单项
       /// </summary>
       /// <returns></returns>
       List<ContextMenuItemInfo> GetContextMenuItems(DataRow row);
    
       /// <summary>
       /// 数据绑定前的初始化
       /// </summary>
       void PreBindData(Message.ServiceInterface.Message mail);
       /// <summary>
       /// 获取当前面板所在列表类型
       /// <remarks>当前业务面板上可能会有多个子面板切换，如承运人面板</remarks>
       /// </summary>
       /// <returns></returns>
       ListFormType GetListFormType();
       /// <summary>
       /// 保存列表自定义信息
       /// </summary>
       void SaveCustomColumnInfo();

    }
}
