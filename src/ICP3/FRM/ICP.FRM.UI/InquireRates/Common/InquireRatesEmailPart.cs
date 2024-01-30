#region Comment

/*
 * 
 * FileName:    InquireRatesEmailPart.cs
 * CreatedOn:   2014/6/10 11:41:08
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->询价沟通邮件
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using ICP.Business.Common.UI.Communication;
using System.ComponentModel;

namespace ICP.FRM.UI.InquireRates
{
    [ToolboxItem(false)]
    public partial class InquireRatesEmailPart : UCCommunicationHistory
    {
        #region 构造函数
        public InquireRatesEmailPart()
        {
            InitializeComponent();
        } 
        #endregion
    }
}
