using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FRM.ServiceInterface.DataObjects;

namespace ICP.FRM.UI.SearchRate
{

    [ToolboxItem(false)]
    public partial class SearchTruckRateInfoPart : BaseEditPart
    {

        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        public SearchTruckRateInfoPart()
        {
            InitializeComponent();
            Disposed += delegate {
                bsList.DataSource = null;
                bsList.Dispose();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }

        private void BindingData(object value)
        {
            SearchTruckRateList list = value as SearchTruckRateList;
            if (list == null) bsList.DataSource = typeof(SearchTruckRateList);
            else
            {
                bsList.DataSource = list;
                bsList.ResetBindings(false);
            }
           
        }
    }
}
