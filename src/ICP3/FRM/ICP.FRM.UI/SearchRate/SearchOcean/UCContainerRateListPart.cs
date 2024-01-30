using System.Collections.Generic;
using System.Windows.Forms;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.FRM.UI.SearchRate
{
    public partial class UCContainerRateListPart : UserControl
    {   
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public UCContainerRateListPart()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
                InitControls();
                SetCtnText();   
            }
            Disposed += delegate
            {
                gcMain.DataSource = null;
                bsContainerRateList.DataSource = null;
                bsContainerRateList.Dispose();
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }
            
            };
              
        }

        private void SetCtnText()
        {
            if (!LocalData.IsEnglish)
            {
                colContainer.Caption = "箱型";
                colCurrency.Caption = "货币";
                colPrice.Caption = "价格";
            }
        }

        private void InitControls()
        {
            Utility.ShowGridRowNo(gvMain);
        }

        public object DataSource
        {
            get { return bsContainerRateList.DataSource; }
            set
            {
                if (value == null)
                    bsContainerRateList.DataSource = typeof(List<FrmUnitRateList>);
                else
                    bsContainerRateList.DataSource = value;
                ResetBindings();
            }
        }

        private void ResetBindings()
        {
            bsContainerRateList.ResetBindings(false);
        }

        public void InitGridControl(bool unitNameColumnVisible, bool priceColumnEditable)
        {
            colCurrency.Visible = unitNameColumnVisible;
            if (unitNameColumnVisible)
            {
                colCurrency.VisibleIndex = 1;
                colPrice.VisibleIndex = 2;
            }
            else
            {
                colCurrency.VisibleIndex = -1;
                colPrice.VisibleIndex = 1;
            }
            colPrice.OptionsColumn.AllowEdit = priceColumnEditable;
        }
    }
}
