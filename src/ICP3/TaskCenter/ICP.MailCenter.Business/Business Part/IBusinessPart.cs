using System.Collections.Generic;
using System.Data;
using ICP.DataCache.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
namespace ICP.Common.Business.ServiceInterface
{  
    /// <summary>
    /// 业务面板接口
    /// </summary>
   public interface IBusinessPart
    {  
       /// <summary>
       /// 行格式化所依据的列名
       /// </summary>
      List<string> ConditionColumnNames { get; set; }
       /// <summary>
       /// 发件人来源
       /// </summary>
      EmailSourceType SourceType { get; set; }

       /// <summary>
       /// 
       /// </summary>
       string TemplateCode { get; set; }
       /// <summary>
       /// 行格式化所依据的单号
       /// </summary>
       List<string> Nos { get; set; }
       /// <summary>
       /// 初始化
       /// </summary>
       /// <param name="mail"></param>
     // void Init();//Message.ServiceInterface.Message mail
       /// <summary>
       /// 数据绑定
       /// </summary>
       /// <param name="mail"></param>
     //  void BindData(ICP.Message.ServiceInterface.Message mail);
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
      // void PreBindData(Message.ServiceInterface.Message mail);
       /// <summary>
       /// 保存列表自定义信息
       /// </summary>
       void SaveCustomColumnInfo();

    }
}
