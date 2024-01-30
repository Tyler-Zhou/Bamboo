using System;
using System.Collections.Generic;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Client;
using System.ComponentModel;
namespace ICP.FRM.UI.SearchRate
{
    [ToolboxItem(false)]
    public partial class SearchTruckSearchPart : SearchRateSearchPart
    {

        public SearchTruckSearchPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    
                }
            
            };
        }
  

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtZipCode.Width = txtPOD.Width;
            navBarGroupControlContainer1.Height = 250;
        }

        /// <summary>
        /// 获得数据
        /// </summary>
        /// <returns></returns>
        public override object GetData()
        {
            //ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("Loading...");

            try
            {
                PageList list = SearchRatesService.GetSearchTruckList(
                                                     LocalData.UserInfo.LoginID,
                                                     searchOceanParameter.ShiplineIDs,
                                                     searchOceanParameter.CarrierIDs,
                                                     searchOceanParameter.Pol,
                                                     searchOceanParameter.Pod,
                                                     txtZipCode.Text.Trim(),
                                                     searchOceanParameter.DurationStart,
                                                     searchOceanParameter.DurationEnd,
                                                     searchOceanParameter.Status,
                                                     searchOceanParameter.DataPageInfo
                                                    );

                return list;

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                return null;
            }
            finally
            {
                //ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm();
            }

        }
        public override event SearchResultHandler OnSearched;
        protected override void AfterClickSearch()
        {
            if (OnSearched != null)
            {
                using (new CursorHelper())
                {
                    PageList list = GetData() as PageList;
                    if (list != null && list.DataPageInfo != null)
                    {
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "total search " + list.DataPageInfo.TotalCount.ToString() + " data." : "总共查询到 "
                                                    + list.DataPageInfo.TotalCount.ToString() + " 条数据.");
                    }
                    OnSearched(this, list);
                }
            }
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="data"></param>
        public override void RaiseSearched(object data)
        {
            DataPageInfo dataPageInfo = data as DataPageInfo;
            searchOceanParameter.DataPageInfo = dataPageInfo;
            if (OnSearched != null)
            {
                OnSearched(this, GetData());
            }
        }

        protected override Dictionary<Guid, string> GetCarrierList()
        {
            Dictionary<Guid, string> list = new Dictionary<Guid, string>();
            Guid[] shipLineIDs = GetShipLineIDs;
            if (shipLineIDs != null && shipLineIDs.Length > 0)
                list = SearchRatesService.GetTruckRateCarrierList(GetStatus, dateMonthControl1.From, dateMonthControl1.To, GetShipLineIDs);
           
            return list;
        }

    }
}
