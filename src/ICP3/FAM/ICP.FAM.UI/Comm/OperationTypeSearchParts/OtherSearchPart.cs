using System;
using System.ComponentModel;
using ICP.FAM.ServiceInterface.DataObjects;

namespace ICP.FAM.UI.Comm.OperationTypeSearchParts
{
    [ToolboxItem(false)]
    public partial class OtherSearchPart : OperationTypeSearchPart
    {
        #region init
        public OtherSearchPart()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        public override void Clear()
        {
            txtRemark.Text = string.Empty;
        }

        #endregion

        public override OperationParameter GetOperationParameter()
        {
            OtherParameter parameter = new OtherParameter();
            parameter.Remark = txtRemark.Text.Trim();
            return parameter;
        }

    }
}
