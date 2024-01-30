using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.FRM.UI
{
    [ToolboxItem(false)]
    public partial class UCWeeklyReportRemark : BasePart
    {
        public UCWeeklyReportRemark()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 备注内容
        /// </summary>
        public string Remark
        {
            get
            {
                return txtRemark.Text;
            }
            set
            {
                txtRemark.Text = value;
            }
        }






    }
}
