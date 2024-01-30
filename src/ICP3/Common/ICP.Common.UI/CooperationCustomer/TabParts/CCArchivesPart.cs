using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;

namespace ICP.Common.UI.CC
{
    [ToolboxItem(false)]
    public partial class CCArchivesPart : ICP.Framework.ClientComponents.UIFramework.BasePart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public CCArchivesPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                rButtonEdit1.ButtonClick -= rButtonEdit1_ButtonClick;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            List<CCPrecautionsList> pList = new List<CCPrecautionsList>();
            pList.Add(new CCPrecautionsList { ID = Guid.NewGuid(), Type = PrecautionsType.WareHouse, Description = "仓储" });
            pList.Add(new CCPrecautionsList { ID = Guid.NewGuid(), Type = PrecautionsType.Truck, Description = "拖车" });
            pList.Add(new CCPrecautionsList { ID = Guid.NewGuid(), Type = PrecautionsType.Customs, Description = "报关" });
            pList.Add(new CCPrecautionsList { ID = Guid.NewGuid(), Type = PrecautionsType.CommodityInspection, Description = "商检" });
            pList.Add(new CCPrecautionsList { ID = Guid.NewGuid(), Type = PrecautionsType.Fumigated, Description = "熏蒸" });
            pList.Add(new CCPrecautionsList { ID = Guid.NewGuid(), Type = PrecautionsType.Certificate, Description = "产地证" });
            pList.Add(new CCPrecautionsList { ID = Guid.NewGuid(), Type = PrecautionsType.SI, Description = "补料" });
            pList.Add(new CCPrecautionsList { ID = Guid.NewGuid(), Type = PrecautionsType.Invoice, Description = "财务" });
            bsPrecautions.DataSource = pList;
            bsPrecautions.ResetBindings(false);

            rButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(rButtonEdit1_ButtonClick);
        }

        void rButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

            CCEditPrecautionsPart precautionsPart = Workitem.Items.AddNew<CCEditPrecautionsPart>();
            //opbForm.SetSource(_parentList);
            //DialogResult dr = PartLoader.ShowDialog(precautionsPart, NativeLanguageService.GetText(this, "BatchEditPartTitel"), FormBorderStyle.Sizable);
            DialogResult dr = PartLoader.ShowDialog(precautionsPart, "Add Precautions", FormBorderStyle.Sizable);
            if (dr != DialogResult.OK) return;
        }
    }
}
