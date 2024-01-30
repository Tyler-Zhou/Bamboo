using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;
using DevExpress.XtraEditors;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FRM.UI
{
    [ToolboxItem(false)]
    public partial class WeeklyReportEdit : BasePart
    {
        public WeeklyReportEdit()
        {
            InitializeComponent();
            Disposed += delegate
            {
                _ShippingLines = null;
                customerList = null;
                shipline = null;
                SaveData = null;
                gcMain.DataSource = null;
                bsList.DataSource = null;
                bsList.Dispose();
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

        public IBusinessInfoService BusinessInfoService
        {
            get
            {
                return ServiceClient.GetService<IBusinessInfoService>();
            }
        }

        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        #endregion

        #region 属性


        /// <summary>
        /// 数据源
        /// </summary>
        public List<BusinessWeeklyReportInfo> DataSource
        {
            get
            {
                List<BusinessWeeklyReportInfo> list = bsList.DataSource as List<BusinessWeeklyReportInfo>;
                if (list == null)
                {
                    list = new List<BusinessWeeklyReportInfo>();
                }
                return list;
            }
            set
            {
                for (int i = 0; i < value.Count; i++)
                {
                    if (value[i].Description.IndexOf("<br>") >= 0)
                    {
                        value[i].Description = value[i].Description.Substring(4).Substring(0, value[i].Description.LastIndexOf("<br>"));
                        value[i].Description = value[i].Description.Replace("<br>", Environment.NewLine);
                    }
                    if (value[i].ShippingSpace.IndexOf("<br>") >= 0)
                    {
                        value[i].ShippingSpace = value[i].ShippingSpace.Substring(4).Substring(0, value[i].ShippingSpace.LastIndexOf("<br>"));
                        value[i].ShippingSpace = value[i].ShippingSpace.Replace("<br>", Environment.NewLine);
                    }
                }
                bsList.DataSource = value;

                Utility.SetXraGridViewColWordrap(gvMain, "Rates");
                Utility.SetXraGridViewColWordrap(gvMain, "ShippingSpace");
                Utility.SetXraGridViewColWordrap(gvMain, "Description");
            }
        }

        /// <summary>
        /// 是否已初始化下拉框
        /// </summary>
        public bool IsInitCombox
        {
            get;
            set;
        }

        /// <summary>
        /// 当前行数据
        /// </summary>
        public BusinessWeeklyReportInfo CurrentRow
        {
            get
            {
                return bsList.Current as BusinessWeeklyReportInfo;
            }
        }

        private bool isDirty;
        /// <summary>
        /// 是否有数据发生改变
        /// </summary>
        public bool IsDirty
        {
            get
            {
                if (isDirty)
                {
                    return true;
                }
                gvMain.CloseEditor();
                foreach (BusinessWeeklyReportInfo item in DataSource)
                {
                    if (item.IsDirty)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        /// <summary>
        /// 周日期 
        /// </summary>
        public string WeeklyDate
        {
            get;
            set;
        }

        /// <summary>
        /// 更改过的数据
        /// </summary>
        public List<BusinessWeeklyReportInfo> ChargeDataList
        {
            get
            {
                List<BusinessWeeklyReportInfo> list = bsList.DataSource as List<BusinessWeeklyReportInfo>;
                if (list == null)
                {
                    list = new List<BusinessWeeklyReportInfo>();
                }

                list = list.FindAll(o => o.IsDirty);

                return list;
            }
            set
            {
                bsList.DataSource = value;
            }
        }

        /// <summary>
        /// 获取主航线
        /// </summary>
        List<ShippingLineList> _ShippingLines = null;
        public List<ShippingLineList> ShippingLines
        {
            get
            {
                if (_ShippingLines != null) return _ShippingLines;
                else
                {
                    _ShippingLines = TransportFoundationService.GetShippingLineList(string.Empty, string.Empty, true, 0);

                    _ShippingLines = (from d in _ShippingLines where d.ParentID == d.ID select d).ToList();

                    return _ShippingLines;
                }
            }
        }
        #endregion

        #region 私有字段
        List<ShippingLineList> shipline = new List<ShippingLineList>();
        List<CustomerList> customerList = new List<CustomerList>();
        #endregion

        #region 事件
        /// <summary>
        ///  保存数据
        /// </summary>
        public event SavedHandler SaveData;


        #endregion

        #region 初始化控件
        /// <summary>
        /// 加载窗体
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                Utility.ShowGridRowNo(gvMain);
                InitControls();
            }
        }


        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            InitPermission();
        }
        /// <summary>
        /// 初始化权限
        /// </summary>
        private void InitPermission()
        {
            if (!LocalCommonServices.PermissionService.HaveActionPermission(BWPermissionCommandConstants.WEEKLYREPORT_BUSINESS))
            {
                gvMain.OptionsBehavior.Editable = false;
            }

        }
        /// <summary>
        /// 搜索类型为“船东”型的“客户”
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForForwarding()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("CustomerType", CustomerType.Carrier, false);

            return conditions;
        }
        /// <summary>
        /// 初始化下拉框
        /// </summary>
        public void InitCombox()
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading...");

            #region 绑定航线
            shipline = ShippingLines;
            foreach (ShippingLineList item in shipline)
            {
                string name = LocalData.IsEnglish ? item.EName : item.CName;
                cmbShiplineID.Properties.Items.Add(new ImageComboBoxItem(name, item.ID));
            }

            #endregion

            IsInitCombox = true;
            LoadingServce.CloseLoadingForm(theradID);

        }

        #endregion

        #region Workitem Command
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BWRCommonConstants.Command_AddData)]
        public void Command_AddData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (DataSource == null)
                {
                    DataSource = new List<BusinessWeeklyReportInfo>();
                }
                BusinessWeeklyReportInfo item = new BusinessWeeklyReportInfo();
                item.DivisionID = LocalData.UserInfo.DefaultCompanyID;
                item.DivisionName = LocalData.UserInfo.DefaultCompanyName;

                item.CreateByID = LocalData.UserInfo.LoginID;
                item.CreateByName = LocalData.UserInfo.LoginName;
                item.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

                bsList.Add(item);

                bsList.ResetBindings(false);

            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BWRCommonConstants.Command_DeleteData)]
        public void Command_DeleteData(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }

            string message = "Srue Delete Current Data?";
            if (Utility.ShowResultMessage(message))
            {

                if (!Utility.GuidIsNullOrEmpty(CurrentRow.ID))
                {
                    try
                    {
                        BusinessInfoService.RemoveBusinessWeeklyReport(CurrentRow.ID, CurrentRow.UpdateDate, LocalData.UserInfo.LoginID, LocalData.IsEnglish);
                    }
                    catch (Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm()
                    , (LocalData.IsEnglish ? "Delete Faily:" : "删除失败:") + ex.Message);
                        return;
                    }

                    if (SaveData != null)
                    {
                        SaveData(null);
                    }

                }

                bsList.RemoveCurrent();

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Delete Success!" : "删除成功!");

            }

        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BWRCommonConstants.Command_SaveData)]
        public void Command_SaveData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Save();
            }
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            if (DataSource == null || DataSource.Count == 0)
            {
                return false;
            }
            if (!IsDirty)
            {
                return false;
            }
            if (!ValidateDate())
            {
                return false;
            }

            List<BusinessWeeklyReportSaveRequest> list = GetDataList();
            foreach (BusinessWeeklyReportSaveRequest bwr in list)
            {
                string[] strDescription = bwr.Descriptions;
                string[] strShippingSpace = bwr.ShippingSpace;
                for (int i = 0; i < strDescription.Length; i++)
                {
                    if (!string.IsNullOrEmpty(strDescription[i]))
                    {
                        strDescription[i] = strDescription[i].Replace(Environment.NewLine, "<br>");
                        strDescription[i] = "<br>" + strDescription[i] + "<br>";
                    }
                    if (!string.IsNullOrEmpty(strShippingSpace[i]))
                    {
                        strShippingSpace[i] = strShippingSpace[i].Replace(Environment.NewLine, "<br>");
                        strShippingSpace[i] = "<br>" + strShippingSpace[i] + "<br>";
                    }
                }
            }
            if (list.Count == 0)
            {
                return false;
            }

            try
            {
                //调用服务端方法保存数据
                Dictionary<Guid, SaveResponse> manyResult = BusinessInfoService.SaveBusinessWeeklyReport(list[0]);

                List<BusinessWeeklyReportInfo> chargeDataList = ChargeDataList;

                SaveResponse.Analyze(list.Cast<SaveRequest>().ToList(), manyResult, true);

                RefreshUI(list);


                if (SaveData != null && chargeDataList != null && chargeDataList.Count > 0)
                {
                    foreach (BusinessWeeklyReportInfo item in chargeDataList)
                    {
                        ShippingLineList shippline = (from d in shipline where d.ID == item.ShiplineID select d).SingleOrDefault();
                        if (shippline != null)
                        {
                            item.ShiplineName = LocalData.IsEnglish ? shippline.EName : shippline.CName;
                        }

                        CustomerList customer = (from d in customerList where d.ID == item.CarrierID select d).SingleOrDefault();
                        if (customer != null)
                        {
                            item.CarrierName = LocalData.IsEnglish ? customer.EName : customer.CName;
                            item.CarrierCode = customer.Code;
                        }
                    }

                    //SaveData(chargeDataList);
                    SaveData(null);
                }

                isDirty = false;
                DataSource.ForEach(o => o.IsDirty = false);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Success!" : "保存成功!");

                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm()
                , (LocalData.IsEnglish ? "Save Faily:" : "保存失败:") + ex.Message);
                return false;
            }

        }

        /// <summary>
        /// 更新列表数据
        /// </summary>
        /// <param name="list"></param>
        public void RefreshUI(List<BusinessWeeklyReportSaveRequest> list)
        {
            foreach (BusinessWeeklyReportSaveRequest item in list)
            {
                List<BusinessWeeklyReportInfo> hblListList = item.UnBoxInvolvedObject<BusinessWeeklyReportInfo>();
                ManyResult result = item.ManyResult;

                for (int i = 0; i < hblListList.Count; i++)
                {
                    hblListList[i].ID = result.Items[i].GetValue<Guid>("ID");
                    hblListList[i].UpdateDate = result.Items[i].GetValue<DateTime?>("UpdateDate");
                    hblListList[i].IsDirty = false;
                }
            }
        }
        /// <summary>
        /// 获得数据
        /// </summary>
        private List<BusinessWeeklyReportSaveRequest> GetDataList()
        {
            List<BusinessWeeklyReportSaveRequest> saveDataList = new List<BusinessWeeklyReportSaveRequest>();

            List<BusinessWeeklyReportInfo> list = DataSource.FindAll(o => o.IsDirty);
            if (list == null || list.Count == 0)
            {
                return saveDataList;
            }

            List<Guid?> idList = new List<Guid?>();
            List<Guid> divisionIDList = new List<Guid>();
            List<Guid?> shiplineIDList = new List<Guid?>();
            List<Guid?> carrierIDList = new List<Guid?>();
            List<String> ratesList = new List<String>();
            List<String> shippingSpaceList = new List<String>();
            List<String> descriptionList = new List<String>();
            List<DateTime?> updateDateList = new List<DateTime?>();
            //List<DateTime?> monthDate = new List<DateTime?>();

            foreach (BusinessWeeklyReportInfo item in list)
            {
                idList.Add(item.ID);
                divisionIDList.Add(item.DivisionID);
                shiplineIDList.Add(item.ShiplineID);
                carrierIDList.Add(item.CarrierID);
                ratesList.Add(item.Rates);
                shippingSpaceList.Add(item.ShippingSpace);
                descriptionList.Add(item.Description);
                updateDateList.Add(item.UpdateDate);
                //monthDate.Add(Convert.ToDateTime(item.MonthDate));
            }

            BusinessWeeklyReportSaveRequest saveData = new BusinessWeeklyReportSaveRequest();
            saveData.CarrierIDs = carrierIDList.ToArray();
            saveData.CreateByID = LocalData.UserInfo.LoginID;
            saveData.Descriptions = descriptionList.ToArray();
            saveData.DivisionIDs = divisionIDList.ToArray();
            saveData.IDs = idList.ToArray();
            saveData.Rates = ratesList.ToArray();
            saveData.ShiplineIDs = shiplineIDList.ToArray();
            saveData.ShippingSpace = shippingSpaceList.ToArray();
            saveData.UpdateDates = updateDateList.ToArray();
            saveData.WeeklyDate = WeeklyDate;
            //saveData.MonthDate = monthDate.ToArray();

            list.ForEach(o => saveData.AddInvolvedObject(o));

            saveDataList.Add(saveData);

            return saveDataList;


        }

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        private bool ValidateDate()
        {
            foreach (BusinessWeeklyReportInfo item in DataSource)
            {
                if (item.IsDirty)
                {
                    if (!item.Validate())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion

        #region Grid 事件
        string _RatesValueTemp;
        void txtRates_Closed(object sender, ClosedEventArgs e)
        {

            if (e.CloseMode == PopupCloseMode.Immediate)
            {
                (sender as MemoExEdit).Text = _RatesValueTemp;
            }
        }
        void txtRates_Popup(object sender, EventArgs e)
        {
            _RatesValueTemp = (sender as MemoExEdit).Text;
        }
        string _DescriptionValueTemp;
        void txtDescription_Closed(object sender, ClosedEventArgs e)
        {

            if (e.CloseMode == PopupCloseMode.Immediate)
            {
                (sender as MemoExEdit).Text = _DescriptionValueTemp;
            }
        }
        void txtDescription_Popup(object sender, EventArgs e)
        {
            _DescriptionValueTemp = (sender as MemoExEdit).Text;
        }

        string _ShippingSpaceValueTemp;
        void txtShippingSpace_Closed(object sender, ClosedEventArgs e)
        {

            if (e.CloseMode == PopupCloseMode.Immediate)
            {
                (sender as MemoExEdit).Text = _ShippingSpaceValueTemp;
            }
        }
        void txtShippingSpace_Popup(object sender, EventArgs e)
        {
            _ShippingSpaceValueTemp = (sender as MemoExEdit).Text;
        }

        #endregion

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                e.Handled = true;
            }
        }


    }
}
