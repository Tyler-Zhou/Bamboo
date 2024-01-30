using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FRM.UI
{
    public partial class EditMangerCommentsPart : BasePart
    {
        public EditMangerCommentsPart()
        {
            InitializeComponent();
            Disposed += delegate {
                gcMain.DataSource = null;
                bsList.PositionChanged -= bsList_PositionChanged;
                bsList.DataSource = null;
                bsList.Dispose();
                Saved = null;
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

        #endregion

        #region 属性
        /// <summary>
        /// 贸易区ID
        /// </summary>
        public Guid ShipLineID
        {
            get;
            set;
        }
        /// <summary>
        /// 口岸公司ID
        /// </summary>
        public Guid CompanyID
        {
            get;
            set;
        }
        /// <summary>
        /// 周数
        /// </summary>
        public string WeeklyData
        {
            get;
            set;
        }
        #endregion

        #region 私有字段
        /// <summary>
        /// 当前行数据
        /// </summary>
        private BusinessWeeklyReportList_Manager CurrentData
        {
            get
            {
                return bsList.Current as BusinessWeeklyReportList_Manager;
            }
        }
        /// <summary>
        /// 当前数据源
        /// </summary>
        private List<BusinessWeeklyReportList_Manager> DataSourceList
        {
            get
            {
                List<BusinessWeeklyReportList_Manager> list = bsList.DataSource as List<BusinessWeeklyReportList_Manager>;
                if (list == null)
                {
                    list = new List<BusinessWeeklyReportList_Manager>();
                }
                foreach (BusinessWeeklyReportList_Manager b in list)
                {
                    if (b.Description.IndexOf("<br>") >= 0)
                        b.Description = b.Description.Substring(4).Substring(0, b.Description.LastIndexOf("<br>")).Replace("<br>", Environment.NewLine);
                }
                return list;
            }
        }
        /// <summary>
        /// 更新改的数据
        /// </summary>
        private List<BusinessWeeklyReportList_Manager> chargeDataList
        {
            get
            {
                return (from d in DataSourceList where d.IsDirty select d).ToList();
            }
        }
        private bool isCharge;
        /// <summary>
        /// 是否有数据发生改变
        /// </summary>
        private bool IsCharge
        {
            get
            {
                if (isCharge)
                {
                    return true;
                }
                foreach (BusinessWeeklyReportList_Manager item in DataSourceList)
                {
                    if (item.IsDirty)
                    {
                        return true;
                    }

                }
                return false;
            }
        }
        #endregion

        #region 加载数据
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                BindDataList();
            }
        }
        private void BindDataList()
        {
            List<BusinessWeeklyReportList_Manager> DataList = BusinessInfoService.GetBusinessWeeklyReportManagerList(WeeklyData, ShipLineID, CompanyID, LocalData.IsEnglish);
            foreach (BusinessWeeklyReportList_Manager b in DataList)
            {
                if (b.Description.IndexOf("<br>") >= 0)
                {
                    b.Description = b.Description.Substring(4).Substring(0, b.Description.LastIndexOf("<br>")).Replace("<br>", Environment.NewLine);
                }
            }
            bsList.DataSource = DataList;
            bsList.ResetBindings(false);


        }
        #endregion

        #region 选择的数据发生改变时
        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentData != null)
            {
                txtRemark.Text = CurrentData.Description;

                #region 是否允许编辑
                if (CurrentData.CreateByID == LocalData.UserInfo.LoginID)
                {
                    txtRemark.Properties.ReadOnly = false;
                }
                else
                {
                    txtRemark.Properties.ReadOnly = true;
                }
                #endregion

                #region 是否允许删除
                if (CurrentData.CreateByID == LocalData.UserInfo.LoginID)
                {
                    barRemove.Enabled = true;
                }
                else
                {
                    barRemove.Enabled = false;
                }
                #endregion
            }
        }
        #endregion

        #region 新增
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            BusinessWeeklyReportList_Manager item = new BusinessWeeklyReportList_Manager();
            item.CompanyID = CompanyID;
            item.ShipLineID = ShipLineID;
            item.WeeklyDate = WeeklyData;
            item.CreateByID = LocalData.UserInfo.LoginID;
            item.CreateByName = LocalData.UserInfo.LoginName;
            item.CreateDate = DateTime.Now;
            item.IsDirty = true;
            item.ID = Guid.NewGuid();

            bsList.Insert(0, item);

            gvMain.FocusedRowHandle = 0;

            txtRemark.Focus();

        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barRemove_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CurrentData == null)
            {
                return;
            }

            string message = LocalData.IsEnglish ? "Are you sure to deleted the selected data " : "确认删除数据?";
            if (XtraMessageBox.Show(message) != DialogResult.OK)
            {
                return;
            }


            try
            {
                BusinessInfoService.RemoveWeeklyReportManager(CurrentData.ID, CurrentData.UpdateDate, LocalData.UserInfo.LoginID, LocalData.IsEnglish);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }

            bsList.RemoveCurrent();


            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Remove Successfully" : "删除成功");

            UpdateDate();

        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            gvMain.Focus();

            if (CurrentData != null)
            {
                CurrentData.EndEdit();
            }

            if (chargeDataList == null || chargeDataList.Count == 0)
            {
                return;
            }
            if (!ValidateData())
            {
                return;
            }

            List<Guid?> IDs = new List<Guid?>();
            List<Guid> DivisionIDs = new List<Guid>();
            List<Guid> ShiplineIDs = new List<Guid>();
            List<Guid> JobIDs = new List<Guid>();
            List<String> Descriptions = new List<string>();
            List<DateTime?> UpdateDates = new List<DateTime?>();

            BusinessWeeklyReport_ManagerSaveRequest saveRequest = new BusinessWeeklyReport_ManagerSaveRequest();

            foreach (BusinessWeeklyReportList_Manager item in chargeDataList)
            {
                item.Description = "<br>"+item.Description.Replace(Environment.NewLine, "<br>") + "<br>";
                IDs.Add(item.ID);
                DivisionIDs.Add(item.CompanyID);
                ShiplineIDs.Add(item.ShipLineID);
                JobIDs.Add(new Guid("716982A1-4903-41BF-907E-66253C4A3AEE"));
                Descriptions.Add(item.Description);
                UpdateDates.Add(item.UpdateDate);
            }

            saveRequest.IDs = IDs.ToArray();
            saveRequest.CreateByID = LocalData.UserInfo.LoginID;
            saveRequest.Descriptions = Descriptions.ToArray();
            saveRequest.DivisionIDs = DivisionIDs.ToArray();
            saveRequest.isEnglish = LocalData.IsEnglish;
            saveRequest.JobIDs = JobIDs.ToArray();
            saveRequest.MonthDate = string.Empty;
            saveRequest.ShiplineIDs = ShiplineIDs.ToArray();
            saveRequest.UpdateDates = UpdateDates.ToArray();
            saveRequest.WeeklyDate = WeeklyData;

            try
            {
                ManyResult result = BusinessInfoService.SaveWeeklyReportManager(saveRequest);

                foreach (SingleResult item in result.Items)
                {
                    Guid id = item.GetValue<Guid>("ID");
                    DateTime? updateDate = item.GetValue<DateTime?>("UpdateDate");

                    foreach (BusinessWeeklyReportList_Manager dateInfo in chargeDataList)
                    {
                        if (dateInfo.ID == id)
                        {
                            dateInfo.UpdateDate = updateDate;
                        }
                    }
                }

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");

                UpdateDate();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }


        }

        private bool ValidateData()
        {
            foreach (BusinessWeeklyReportList_Manager item in chargeDataList)
            {
                if (!item.Validate())
                {
                    return false;
                }
            }
            return true;
        }

        private void UpdateDate()
        {
            if (Saved != null)
            {
                Saved(new object[1] { GetManagerInfo() });
            }
        }
        #endregion

        #region 获得经理批注信息
        public string GetManagerInfo()
        {
            RichTextBox richTextBox = new RichTextBox();

            StringBuilder str = new StringBuilder();
            foreach (BusinessWeeklyReportList_Manager item in DataSourceList)
            {
                if (string.IsNullOrEmpty(item.Description))
                {
                    item.Description = "<br>" + item.Description.Replace(Environment.NewLine, "<br>") + "<br>";
                    continue;
                }

                str.AppendLine(item.CreateByName + ":" + item.Description);
            }

            richTextBox.Text = str.ToString();

            return richTextBox.Rtf;
        }
        #endregion

        #region 事件
        public event SavedHandler Saved;
        #endregion

   

    }
}
