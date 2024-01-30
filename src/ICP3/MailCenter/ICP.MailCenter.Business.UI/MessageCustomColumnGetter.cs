using System.Collections.Generic;
using ICP.Operation.Common.ServiceInterface;

namespace ICP.MailCenter.Business.UI
{  
    /// <summary>
    /// 消息业务面板自定义列获取器
    /// </summary>
    public class MessageCustomColumnGetter : ICustomColumnGetter
    {
        #region ICustomColumnGetter 成员
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BusinessColumnInfo> Get()
        {  
            //添加消息与业务是否关联列
            BusinessColumnInfo columnInfo = new BusinessColumnInfo();
            columnInfo.Caption = "IsAssociated";
            columnInfo.Dropable = false;
            columnInfo.Editable = false;
            columnInfo.EditType = ColumnEditType.Text;
            columnInfo.FieldName = "IsAssociated";
            columnInfo.Name = "IsAssociated_sort";
            
            
            columnInfo.ReadOnly = true;
            return new List<BusinessColumnInfo> {columnInfo };
        }

        #endregion
    }
}
