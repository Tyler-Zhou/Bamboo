using System;
using System.Collections.Generic;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FAM.UI.ChargeConfigure
{
    public partial class AddContainer : BaseEditPart
    {
        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        public delegate void AddContainerType(string TypeName,decimal Price);
        public event AddContainerType AddS;

        public AddContainer()
        {
            InitializeComponent();
        }

        private void AddContainer_Load(object sender, EventArgs e)
        {
            List<ContainerList> ctntypes = TransportFoundationService.GetContainerList(string.Empty, true, 0);
            cmbContainerUnit.Properties.BeginUpdate();
            foreach (var item in ctntypes)
            {
                cmbContainerUnit.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
            }
            cmbContainerUnit.Properties.EndUpdate();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (AddS != null)
            {
                AddS(cmbContainerUnit.Text,numPrice.Value);
            }
        }
    }
}
