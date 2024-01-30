using System;
using System.ComponentModel;
using System.Drawing;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FAM.UI.Comm.OperationTypeSearchParts
{
    [ToolboxItem(false)]
    public partial class CustomsSearchPart : OperationTypeSearchPart
    {
        public CustomsSearchPart()
        {
            InitializeComponent();
        }

        public override void Clear()
        {
            txtBookNo.Text = txtChargeName.Text = txtCustomsNo.Text = txtMaxNo.Text = txtMinNo.Text = txtPortName.Text = txtTruckNo.Text = string.Empty;
        }


        public override void SetTextBoxLocation(int labX, int txtBoxX, int txtBoxWidth)
        {
            for (int i = 0; i < Controls.Count; i++)
            {

                Controls[i].Location = new Point(labX, Controls[i].Location.Y);
                Controls[i].Width = txtBoxX + txtBoxWidth;
            }

        }

        public override OperationParameter GetOperationParameter()
        {
            CustomsParameter parameter = new CustomsParameter();

            return parameter;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                InitControls();
            }
        }

        private void InitControls()
        {
            cmbCustomsType.ShowSelectedValue(10,LocalData.IsEnglish?"All":"全部");

            //Utility.SetEnterToExecuteOnec(this.cmbCustomsType, delegate
            //{
            //    Utility.SetComboxByEnum<CustomsType>(this.cmbCustomsType, false);
            //});
           
        }

    }
}
