using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.Service;
using ICP.FRM.UI.OceanPrice;

namespace ICP.FRM.UI.SearchRate
{
    [ToolboxItem(false)]
    public partial class SearchRateSearchPart : BaseSearchPart
    {
        public SearchRateSearchPart()
        {
            InitializeComponent();
            Disposed += delegate {
               
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
        public OceanPriceUIDataHelper OceanPriceUIDataHelper
        {
            get
            {
                return ClientHelper.Get<OceanPriceUIDataHelper, OceanPriceUIDataHelper>();
            }
        }
         
        #endregion


        #region 私有字段 
        /// <summary>
        /// 是否需要重新绑定船东
        /// </summary>
        private bool isBindCarrier = true;
        protected SearchRateParameter searchOceanParameter = new SearchRateParameter();
        #endregion

        #region 初始化

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                InitMessage();
                InitToolTip();
                SetLazyLoaders();
                InitControls();
            }
        }
        /// <summary>
        /// 注册消息
        /// </summary>
        private void InitMessage()
        {

            RegisterMessage("InutCodeNameToolTip", "Please input Code, Chinese name or English name");

        }

        /// <summary>
        /// 初始化ToolTip
        /// </summary>
        private void InitToolTip()
        {
            cmbCarrier.ToolTip = NativeLanguageService.GetText(this, "InutCodeNameToolTip");
            txtPOD.ToolTip = NativeLanguageService.GetText(this, "InutCodeNameToolTip");
            txtPOL.ToolTip = NativeLanguageService.GetText(this, "InutCodeNameToolTip");

        }
        /// <summary>
        /// 延迟加载
        /// </summary>
        private void SetLazyLoaders()
        {
            //航线   
            Utility.SetEnterToExecuteOnec(cmbShipline, delegate
            {
                List<ShippingLineList> list = OceanPriceUIDataHelper.ShippingLines;
                foreach (ShippingLineList item in list)
                {
                    string name = LocalData.IsEnglish ? item.EName : item.CName;
                    cmbShipline.AddItem(item.ID, name);
                }
            });
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            #region 查询运价状态
            List<EnumHelper.ListItem<SearchPriceStatus>> searchPriceStatus
                = EnumHelper.GetEnumValues<SearchPriceStatus>(LocalData.IsEnglish);
            foreach (var item in searchPriceStatus)
            {
                cmbScope.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value, (short)item.Value));
            }
            #endregion
            cmbScope.SelectedIndex = 0;


            dateMonthControl1.From = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            dateMonthControl1.To = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).AddDays(15);

            Utility.SearchPartKeyEnterToSearch(new List<Control> 
            {
                 cmbShipline,cmbCarrier,txtPOD,txtPOL
            }, btnSearch);

            Utility.SearchPartKeyF2ToSearch(new List<Control> 
            {
                cmbShipline,cmbCarrier,txtPOD,txtPOL
            }, btnClear);
        }

        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定船东
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void cmbCarrier_Enter(object sender, EventArgs e)
        {
            if (isBindCarrier)
            {
                try
                {
                    Dictionary<Guid, string> list = GetCarrierList();
                    cmbCarrier.BeginUpdate();
                    foreach (var item in list)
                    {
                        cmbCarrier.AddItem(item.Key, item.Value);
                    }
                    cmbCarrier.EndUpdate();

                    isBindCarrier = false;
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); }
            }

        }

        protected virtual Dictionary<Guid, string> GetCarrierList() { return new Dictionary<Guid, string>(); }

        #endregion

        #region 控件事件
        
        /// <summary>
        /// 航线发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void cmbShipline_EditValueChanged(object sender, EventArgs e)
        {
            ResetCarrierAndPort();
        }
        /// <summary>
        /// 状态发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void cmbScope_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetCarrierAndPort();
        }
        /// <summary>
        /// 重置其他查询数据
        /// </summary>
        private void ResetCarrierAndPort()
        {
            cmbCarrier.ClearItems();
            isBindCarrier = true;
        }

        #endregion

        #region 获得控件数据
        /// <summary>
        /// 当前状态
        /// </summary>
        protected SearchPriceStatus GetStatus
        {
            get
            {
                return (SearchPriceStatus)cmbScope.SelectedIndex;
            }
        }
        /// <summary>
        /// 获得当前航线ID集合
        /// </summary>
        protected Guid[] GetShipLineIDs
        {
            get
            {
                string text = cmbShipline.SelectValues;
                string[] list = text.Split(';');
                List<Guid> idList = new List<Guid>();

                foreach (string str in list)
                {
                    if (!string.IsNullOrEmpty(str) && !idList.Contains(new Guid(str)))
                    {
                        idList.Add(new Guid(str));
                    }
                }

                return idList.ToArray();

            }
        }
        /// <summary>
        /// 船东ID集合
        /// </summary>
        protected Guid[] GetCarrierIDs
        {
            get
            {
                string text = cmbCarrier.SelectValues;
                string[] list = text.Split(';');
                List<Guid> idList = new List<Guid>();

                foreach (string str in list)
                {
                    if (!string.IsNullOrEmpty(str) && !idList.Contains(new Guid(str)))
                    {
                        idList.Add(new Guid(str));
                    }
                }

                return idList.ToArray();

            }
        }
        #endregion

        #region Button Event
        #region 清空
        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnClear_Click(object sender, EventArgs e)
        {
            cmbShipline.ClearEditValue();
            cmbCarrier.ClearEditValue();
            txtPOL.Text = txtPOD.Text = string.Empty;
            cmbScope.SelectedIndex = 0;
        }


        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                searchOceanParameter.ShiplineIDs = cmbShipline.SelectValuesToGuid;
                searchOceanParameter.CarrierIDs = cmbCarrier.SelectValuesToGuid;
                searchOceanParameter.Pol = txtPOL.Text.Trim();
                searchOceanParameter.Pod = txtPOD.Text.Trim();
                searchOceanParameter.Status = (SearchPriceStatus)cmbScope.EditValue;
                searchOceanParameter.DurationStart = dateMonthControl1.From;
                searchOceanParameter.DurationEnd = dateMonthControl1.To;

                searchOceanParameter.DataPageInfo.CurrentPage = 1;

                if (string.IsNullOrEmpty(searchOceanParameter.DataPageInfo.SortByName))
                {
                    searchOceanParameter.DataPageInfo.SortByName = "CreateDate";
                    searchOceanParameter.DataPageInfo.SortOrderType = SortOrderType.Asc;
                }

                AfterClickSearch();
            }
        }

        protected virtual void AfterClickSearch() { }
        #endregion

        #endregion

        #region 重写
        
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="values"></param>
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null)
            {
                return;
            }
            cmbShipline.ClearEditValue();
            cmbCarrier.ClearEditValue();
            txtPOD.Text = txtPOL.Text = string.Empty;

            foreach (var item in values)
            {
                 string value = item.Value == null ? string.Empty : item.Value.ToString();
                 switch (item.Key)
                 {
                     case "CarrierName":
                         cmbCarrier.SetEditText(value);
                         break;
                     case "POLName":
                         txtPOL.Text =value==null? string.Empty: value.ToString();
                         break;
                     case "PODName":
                         txtPOD.Text = value == null ? string.Empty : value.ToString();
                         break;
                 }
            }


        }

        /// <summary>
        /// 热键查询
        /// </summary>
        public override void RaiseSearched()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                btnSearch.PerformClick();
            }
        }
        
        
        #endregion
    }
    /// <summary>
    /// 查询实体
    /// </summary>
    public class SearchRateParameter
    {
        public SearchRateParameter()
        {
            DataPageInfo = new DataPageInfo();
        }
        public Guid[] ShiplineIDs{get;set;}
        public Guid[] CarrierIDs { get; set; }
        public string Pol { get; set; }
        public string Pod { get; set; }
        public string Zipcode { get; set; }
        public DateTime? DurationStart { get; set; }
        public DateTime? DurationEnd { get; set; }
        public SearchPriceStatus Status { get; set; }
        public DataPageInfo DataPageInfo{get;set;}
    }
}
