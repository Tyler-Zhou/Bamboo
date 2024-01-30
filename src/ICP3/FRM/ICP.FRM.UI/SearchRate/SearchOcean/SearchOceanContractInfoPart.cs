using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FRM.UI.SearchRate
{
    [ToolboxItem(false)]
    public partial class SearchOceanContractInfoPart : BaseEditPart
    {
        public SearchOceanContractInfoPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                gcSCN.DataSource = null;
                bsList.DataSource = null;
                bsList.Dispose();
                bsSCNList.DataSource = null;
                bsSCNList.Dispose();

               
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            
            };
        }
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public ISearchRatesService SearchRatesService
        {
            get
            {
                return ServiceClient.GetService<ISearchRatesService>();
            }
        }
        #endregion

        #region 属性
        SearchOceanContractInfo contractInfo;
        #endregion

        #region 绑定数据
        public override object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }
        private void BindingData(object data)
        {
            txtContractNo.Text = string.Empty;
            SearchOceanRateList list = data as SearchOceanRateList;
            if (list == null || list.SearchrateType == SearchRateType.Inquiry)
            {
                contractInfo = new SearchOceanContractInfo();
                bsList.DataSource = contractInfo;
                bsList.ResetBindings(false);

                bsSCNList.DataSource = new List<SCNInfo>();
                bsSCNList.ResetBindings(false);
                return;
            }
            else
            {
                contractInfo = SearchRatesService.GetSearchOceanContractInfo(list.ID, list.SearchrateType);

                //没有权限的，不能查看合约号
                if (!LocalCommonServices.PermissionService.HaveActionPermission(PermissionCommandConstants.SEARCHOCEAN_VIEWCONTRACTNO))
                {
                    txtContractNo.Text = contractInfo.ContractNo = string.Empty;
                }

                bsList.DataSource = contractInfo;
                bsList.ResetBindings(false);

                bsSCNList.DataSource = contractInfo.SCNList;
                bsSCNList.ResetBindings(false);
            }
            
        }
        #endregion

    }
}
