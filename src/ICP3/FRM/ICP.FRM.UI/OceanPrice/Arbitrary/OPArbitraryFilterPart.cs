using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.FRM.UI.OceanPrice
{
    [ToolboxItem(false)]
    public partial class OPArbitraryFilterForm : BasePart
    {
        public OPArbitraryFilterForm()
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

            RegisterMessage("PortToolTip", "You can use semicolons for dividing multi-Ports, e.g. Yuantian; Hongkong");
            RegisterMessage("ItemCodeToolTip", "You can use semicolons for dividing multi-Item Code, e.g. A.1.3; A.1.4");
            RegisterMessage("TermToolTip", "You can use semicolons for dividing multi- Term, e.g. CY-CY; CY-CFS");
           

            RegisterMessage("CheckedToolTip", "Checked if the filtering would not contain the value.");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetToolTip();

            Utility.SearchPartKeyEnterToSearch(new List<Control> 
            { txtPOL, txtPOD, txtItemCode,txtTerm, }
            , btnFind);

        }

        private void SetToolTip()
        {
            txtPOL.ToolTip =
            txtPOD.ToolTip = NativeLanguageService.GetText(this, "PortToolTip");
            txtItemCode.ToolTip = NativeLanguageService.GetText(this, "ItemCodeToolTip");
            txtTerm.ToolTip = NativeLanguageService.GetText(this, "TermToolTip");

            chkItemCodeExcl.ToolTip = chkPODExcl.ToolTip = chkPOLExcl.ToolTip = chkTermExcl.ToolTip
                   = NativeLanguageService.GetText(this, "CheckedToolTip");
        }

        public ArbitraryFilterObject FilterObject = null;
        public void SetSouce(ArbitraryFilterObject filterObject)
        {
            if (filterObject == null) FilterObject = new ArbitraryFilterObject();
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
            FilterObject = new ArbitraryFilterObject();
            FindForm().DialogResult = DialogResult.OK;
            FindForm().Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }

    }
}
