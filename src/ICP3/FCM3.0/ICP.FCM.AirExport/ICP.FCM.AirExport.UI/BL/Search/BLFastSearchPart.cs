using System;
using System.Collections.Generic;
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
                List<AirBLList> list = aeService.GetAirBLListForFaster(CompanyIDs
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

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "total search " + list.Count.ToString() + " data." : "总共查询到 " + list.Count.ToString() + " 条数据.");
                return list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return null; }
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
            ((System.ComponentModel.ISupportInitialize)(this.cmbNoSearchType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomerSearchType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPortSearchType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDateSearchType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDateType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).BeginInit();
            this.SuspendLayout();
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
            this.panelControl2.Size = new System.Drawing.Size(976, 37);
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(972, 33);
            // 
            // stxtPort
            // 
            this.stxtPort.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtPort.Properties.Appearance.Options.UseBackColor = true;
            // 
            // stxtCustomer
            // 
            this.stxtCustomer.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtCustomer.Properties.Appearance.Options.UseBackColor = true;
            // 
            // BLFastSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.Name = "BLFastSearchPart";
            this.Size = new System.Drawing.Size(976, 37);
            ((System.ComponentModel.ISupportInitialize)(this.cmbNoSearchType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomerSearchType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPortSearchType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDateSearchType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDateType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtCustomer.Properties)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
