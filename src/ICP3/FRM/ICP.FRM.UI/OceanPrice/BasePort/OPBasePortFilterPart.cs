using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.FRM.UI.OceanPrice
{
    [ToolboxItem(false)]
    public partial class OPBasePortFilterForm : BasePart
    {
        public OPBasePortFilterForm()
        {
            InitializeComponent();
            Disposed += delegate
            {
                bindingSource1.DataSource = null;
                bindingSource1.Dispose();

            
            };
            if (!DesignMode) { InitMessage(); }
        }
        private void InitMessage()
        {
            RegisterMessage("AccountToolTip", "You can use semicolons for dividing multi-Account, e.g. IBM; Google");
            RegisterMessage("PortToolTip", "You can use semicolons for dividing multi-Ports, e.g. Yuantian; Hongkong");
            RegisterMessage("ItemCodeToolTip", "You can use semicolons for dividing multi-Item Code, e.g. A.1.3; A.1.4");
            RegisterMessage("TermToolTip", "You can use semicolons for dividing multi- Term, e.g. CY-CY; CY-CFS");
            RegisterMessage("SurChargeToolTip", "You can use semicolons for dividing multi- SurCharge, e.g. ABC; CDE");
            RegisterMessage("DescriptionToolTip", "You can use semicolons for dividing multi-Description, e.g. ABC; CBA");
            RegisterMessage("CheckedToolTip", "Checked if the filtering would not contain the value.");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SetToolTip();

            Utility.SearchPartKeyEnterToSearch(new List<Control> 
            { txtAccount, txtPOL, txtVIA, txtPOD, txtDelivery, txtItemCode, txtComm, txtTerm, txtSurCharge,txtDescription
            }
            , btnFind);

        }

        private void SetToolTip()
        {
            txtAccount.ToolTip = NativeLanguageService.GetText(this, "AccountToolTip");
            txtPOL.ToolTip = 
            txtVIA.ToolTip = 
            txtPOD.ToolTip =
            txtDelivery.ToolTip = NativeLanguageService.GetText(this, "PortToolTip");

            txtItemCode.ToolTip = NativeLanguageService.GetText(this, "ItemCodeToolTip");
            //txtComm.ToolTip = "You can use semicolons for dividing multi-Commodity, e.g. FAK; Furniture";
            txtTerm.ToolTip = NativeLanguageService.GetText(this, "TermToolTip");
            txtSurCharge.ToolTip = NativeLanguageService.GetText(this, "SurChargeToolTip");
            txtDescription.ToolTip = NativeLanguageService.GetText(this, "DescriptionToolTip");


            chkCommExcl.ToolTip = chkDeliveryExcl.ToolTip = chkDescriptionExcl.ToolTip
               = chkItemCodeExcl.ToolTip = chkPODExcl.ToolTip = chkPOLExcl.ToolTip = chkSurChargeExcl.ToolTip
               = chkTermExcl.ToolTip = chkVIAExcl.ToolTip = NativeLanguageService.GetText(this, "CheckedToolTip");
        }

        public BasePortFilterObject FilterObject = null;
        public void SetSouce(BasePortFilterObject filterObject)
        {
            if (filterObject == null) FilterObject = new BasePortFilterObject();
            else FilterObject = filterObject;

            bindingSource1.DataSource = FilterObject;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            FindForm().DialogResult = DialogResult.OK;
            FindForm().Close();
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            FilterObject = new BasePortFilterObject();
            FindForm().DialogResult = DialogResult.OK;
            FindForm().Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }

    }


}
