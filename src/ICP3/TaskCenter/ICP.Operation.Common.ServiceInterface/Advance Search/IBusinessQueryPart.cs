﻿using ICP.Framework.CommonLibrary.Common;
using System.Collections.Generic;

namespace ICP.Operation.Common.ServiceInterface
{  
    /// <summary>
    /// 业务高级查询接口
    /// </summary>
   public interface IBusinessQueryPart
    {  
       /// <summary>
       /// 初始化查询界面
       /// </summary>
       /// <param name="initValues"></param>
       void Init(Dictionary<string, object> initValues);
       /// <summary>
       /// 本地化
       /// </summary>
       void Locale();
       /// <summary>
       /// 重置界面上控件的值
       /// </summary>
       void Reset();
       /// <summary>
       /// 界面类型
       /// </summary>
       BusinessType Type { get;}
       /// <summary>
       /// 获取查询结果
       /// </summary>
       /// <returns></returns>
       string GetAdvanceQueryString();
       /// <summary>
       /// 上一次的查询条件
       /// </summary>
       void LastAdvanceQueryString(Dictionary<string, object> initValues);
    }
}
