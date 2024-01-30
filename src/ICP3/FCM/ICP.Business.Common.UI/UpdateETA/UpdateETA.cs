using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.ClientComponents;

namespace ICP.Business.Common.UI.UpdateETA
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UpdateETA : BaseEditPart
    {
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IClientOceanImportService ClientOceanImportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanImportService>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public UpdateETAInfo _updateETAinfo = null;
        /// <summary>
        /// 
        /// </summary>
        public UpdateETA()
        {
            InitializeComponent();
            SearchRegister();
        }
        /// <summary>
        /// 
        /// </summary>
        public override object DataSource
        {
            get { return bindingSourceupdate.DataSource; }
            set { BindingData(value); }
        }

        void BindingData(object data)
        {
            _updateETAinfo = data as UpdateETAInfo;
            this.bindingSourceupdate.DataSource = _updateETAinfo;
        }


        void SearchRegister()
        {
            //MBL提货地
            this.DataFindClientService.Register(this.buttonEdit1, CommonFinderConstants.CustoemrFinder,
              ICP.Common.UI.SearchFieldConstants.CodeName, ICP.Common.UI.SearchFieldConstants.ResultValue,
               GetConditionsForFinalWareHouse,
               delegate(object inputSouce, object[] resultData)
               {
                   buttonEdit1.Tag = this._updateETAinfo.WareHouseID = new Guid(resultData[0].ToString());
                   buttonEdit1.EditValue = this._updateETAinfo.WareHouseName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
               },
               delegate
               {
                   this.buttonEdit1.Tag = this._updateETAinfo.WareHouseID = Guid.Empty;
                   this.buttonEdit1.EditValue = this._updateETAinfo.WareHouseName = string.Empty;
               },
           ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

        }


        /// <summary>
        /// “提货地”是类型为“码头”或“堆场”的“客户”
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForFinalWareHouse()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CustomerType", CustomerType.Storage, false);
            conditions.AddWithValue("CustomerType", CustomerType.Terminal, false);
            return conditions;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                List<Guid> operationids = FCMCommonService.GetOperationIdsForUpdate(_updateETAinfo);

                if (operationids == null || operationids.Count < 1)
                {
                    return;
                }

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Update Successfully" : "更新成功");

                //发送通知邮件
                ClientOceanImportService.MailEtachangeServerSend(operationids);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Update Successfully" : "更新成功");
            }
            catch (Exception)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "System error." : "系统错误！");
            }
        }
    }
}
