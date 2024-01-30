using System;
using System.Collections.Generic;
using System.Drawing;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;

using ICP.Framework.CommonLibrary.Client;
using System.ComponentModel;

namespace ICP.FCM.AirExport.UI.BL
{
    [ToolboxItem(false)]
    public class BLFastSearchPart : FastSearchPart
    {
        public override object GetData()
        {
            try
            {
                List<AirBLList> list = AirExportService.GetAirBLListForFaster(CompanyIDs
                                                        , base.PartNoSearchType
                                                        , txtNo.Text.Trim()
                                                        , base.PartCustomerSearchType
                                                        , stxtCustomer.Text.Trim()
                                                        , base.PartPortSearchType
                                                        , stxtPort.Text.Trim()
                                                        , base.PartDateSearchType
                                                        , base.From
                                                        , base.To
                                                        , 0);

                //LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "total search " + list.Count.ToString() + " data." : "总共查询到 " + list.Count.ToString() + " 条数据.");
                return list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); return null; }
        }

        protected override void OnClickMore()
        {
            Workitem.Commands[AEBLCommandConstants.Command_ShowSearch].Execute();
        }

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                stxtCustomer.Text = stxtPort.Text = txtNo.Text = string.Empty;

                if (cmbPortSearchType.Properties.Items.Count != 0)
                {
                    cmbCustomerSearchType.SelectedIndex = cmbDateSearchType.SelectedIndex = cmbDateType.SelectedIndex
                        = cmbNoSearchType.SelectedIndex = cmbPortSearchType.SelectedIndex = 0;
                }

                switch (item.Key)
                {
                    case "RefNo":
                        txtNo.Text = item.Value == null ? string.Empty : item.Value.ToString();
                        cmbNoSearchType.EditValue = NoSearchType.Operation;
                        break;
                }
            }
        }

        private void InitializeComponent()
        {
            ((ISupportInitialize)(cmbNoSearchType.Properties)).BeginInit();
            ((ISupportInitialize)(txtNo.Properties)).BeginInit();
            ((ISupportInitialize)(cmbCustomerSearchType.Properties)).BeginInit();
            ((ISupportInitialize)(cmbPortSearchType.Properties)).BeginInit();
            ((ISupportInitialize)(cmbDateSearchType.Properties)).BeginInit();
            ((ISupportInitialize)(cmbDateType.Properties)).BeginInit();
            ((ISupportInitialize)(panelControl2)).BeginInit();
            panelControl2.SuspendLayout();
            panel1.SuspendLayout();
            ((ISupportInitialize)(stxtPort.Properties)).BeginInit();
            ((ISupportInitialize)(stxtCustomer.Properties)).BeginInit();
            SuspendLayout();
            // 
            // cmbNoSearchType
            // 
            // 
            // txtNo
            // 
            // 
            // cmbCustomerSearchType
            // 
            // 
            // cmbPortSearchType
            // 
            // 
            // cmbDateSearchType
            // 
            // 
            // cmbDateType
            // 
            // 
            // panelControl2
            // 
            panelControl2.Size = new Size(976, 37);
            // 
            // panel1
            // 
            panel1.Size = new Size(972, 33);
            // 
            // stxtPort
            // 
            stxtPort.Properties.Appearance.BackColor = Color.White;
            stxtPort.Properties.Appearance.Options.UseBackColor = true;
            // 
            // stxtCustomer
            // 
            stxtCustomer.Properties.Appearance.BackColor = Color.White;
            stxtCustomer.Properties.Appearance.Options.UseBackColor = true;
            // 
            // BLFastSearchPart
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            Name = "BLFastSearchPart";
            Size = new Size(976, 37);
            ((ISupportInitialize)(cmbNoSearchType.Properties)).EndInit();
            ((ISupportInitialize)(txtNo.Properties)).EndInit();
            ((ISupportInitialize)(cmbCustomerSearchType.Properties)).EndInit();
            ((ISupportInitialize)(cmbPortSearchType.Properties)).EndInit();
            ((ISupportInitialize)(cmbDateSearchType.Properties)).EndInit();
            ((ISupportInitialize)(cmbDateType.Properties)).EndInit();
            ((ISupportInitialize)(panelControl2)).EndInit();
            panelControl2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((ISupportInitialize)(stxtPort.Properties)).EndInit();
            ((ISupportInitialize)(stxtCustomer.Properties)).EndInit();
            ResumeLayout(false);

        }
    }
}
