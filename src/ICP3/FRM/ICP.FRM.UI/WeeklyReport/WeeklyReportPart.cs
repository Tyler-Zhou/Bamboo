using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraTab;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraEditors.Controls;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.FRM.ServiceInterface;

namespace ICP.FRM.UI
{
    [ToolboxItem(false)]
    public partial class WeeklyReportPart : BaseEditPart
    {
        public WeeklyReportPart()
        {
            InitializeComponent();
            Closing += WeeklyReportPart_Closing;
            Disposed += delegate
            {
                dateWeeklyList = null;

                Closing -= WeeklyReportPart_Closing;
                if (Workitem != null)
                {
                    
                    if (UCGeneralWeeklyList != null)
                    {
                        Workitem.Items.Remove(UCGeneralWeeklyList);
                        UCGeneralWeeklyList = null;
                    }
                    if (UCCompanyWeeklyList != null)
                    {
                        UCCompanyWeeklyList.SaveData -= UCCompanyWeeklyList_SaveData;
                        Workitem.Items.Remove(UCCompanyWeeklyList);
                        UCCompanyWeeklyList = null;
                    }
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

        #endregion

        #region 私有字段
        /// <summary>
        /// 周日期对象列表
        /// </summary>
        List<DateWeekly> dateWeeklyList = new List<DateWeekly>();

        WeeklyReportDataList UCGeneralWeeklyList;
        WeeklyReportEdit UCCompanyWeeklyList;
        #endregion

        #region 初始化

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                LoadPanel();
                InitControls();
                InitPermission();
            }
        }
        /// <summary>
        /// 初始化权限
        /// </summary>
        private void InitPermission()
        {
            if (!LocalCommonServices.PermissionService.HaveActionPermission(BWPermissionCommandConstants.WEEKLYREPORT_BUSINESS))
            {
                bar2.Visible = false;
                barAppend.Enabled = false;
                barSave.Enabled = false;
                barRemove.Enabled = false;
            }

        }
        /// <summary>
        /// 加载面板
        /// </summary>
        private void LoadPanel()
        {
            UCGeneralWeeklyList = Workitem.Items.AddNew<WeeklyReportDataList>();
            UCGeneralWeeklyList.Dock = DockStyle.Fill;
            pnlGeneral.Controls.Add(UCGeneralWeeklyList);

            UCCompanyWeeklyList = Workitem.Items.AddNew<WeeklyReportEdit>();
            UCCompanyWeeklyList.Dock = DockStyle.Fill;
            pnlCompany.Controls.Add(UCCompanyWeeklyList);

            UCCompanyWeeklyList.SaveData += new SavedHandler(UCCompanyWeeklyList_SaveData);
        }
        /// <summary>
        /// 保存数据 
        /// </summary>
        /// <param name="prams"></param>
        void UCCompanyWeeklyList_SaveData(params object[] prams)
        {
            UCGeneralWeeklyList.EditPartSaved(prams);
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            tabCompanyWeeklyReport.Text = LocalData.UserInfo.DefaultCompanyName + " Weekly Report";

            #region 初始化时间下拉框

            //获取服务器的时间，不能用本机的时间
            DateTime dtStart = new DateTime(2011, 01, 01);
            DateTime dtEnd = BusinessInfoService.GetServerDate();

            dateWeeklyList = new List<DateWeekly>();

            int i = 1;
            for (DateTime dt = dtStart; dt <= dtEnd; dt = dt.AddDays(0))
            {
                DateWeekly item = new DateWeekly();
                item.StartDate = dt.Date;
                item.Index = i++;
                item.Year = dt.Year;
                item.Weekly = GetWeekOfYear(dt);

                //结束日期为dt的所在周的最后一天
                DateTime startWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d")));
                item.EndDate = startWeek.AddDays(6);

                dt = dt.AddDays(((item.EndDate - item.StartDate).Days + 1));

                dateWeeklyList.Add(item);
            }
            //DateWeekly startLastUpdate = new DateWeekly();
            //startLastUpdate.StartDate = DateTime.Now;
            //startLastUpdate.Index = Int16.MaxValue;
            //startLastUpdate.Year = DateTime.Now.Year;
            //startLastUpdate.ItemID = "0";
            //startLastUpdate.ItemName = LocalData.IsEnglish ? "#Last Update" : "#最近更新";
            //dateWeeklyList.Add(startLastUpdate);

            dateWeeklyList = (from d in dateWeeklyList orderby d.Index descending select d).ToList();

            foreach (DateWeekly item in dateWeeklyList)
            {
                cmbWeekDate.Properties.Items.Add(new ImageComboBoxItem(item.ItemName, item.Index.ToString()));
            }
           
            cmbWeekDate.SelectedIndex = 0;


            #endregion

        }

        /// <summary>
        /// 获得指定日期在当年中所处的周数
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private int GetWeekOfYear(DateTime date)
        {
            int firstWeekend = 7 - Convert.ToInt32(DateTime.Parse(date.Year + "-1-1").DayOfWeek);

            int currentDay = date.DayOfYear;

            return Convert.ToInt32(Math.Ceiling((currentDay - firstWeekend) / 7.0)) + 1;
        }

        #endregion

        #region 选择的周日期发生改变时
        /// <summary>
        /// 选择的周日期发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbWeekDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDataSource();
        }

        /// <summary>
        /// 设置明细数据源
        /// </summary>
        private void SetDataSource()
        {
            DateWeekly currencyItem = GetSeletctWeekly();
            //st-Hylun
            WeeklyAppCache.WeeklyItem = currencyItem.ItemID.Substring(1, currencyItem.ItemID.Length - 1);
            WeeklyAppCache.WeeklyDate = currencyItem.ItemID;
            //st-End
            if (currencyItem == null)
            {
                return;
            }
            Setbutton(currencyItem);


            if (tabWeeklyReport.SelectedTabPage == tabCompanyWeeklyReport)
            {
                List<BusinessWeeklyReportInfo> list = BusinessInfoService.GetBusinessWeeklyReportList(LocalData.UserInfo.DefaultCompanyID, currencyItem.ItemID, LocalData.IsEnglish);
                UCCompanyWeeklyList.WeeklyDate = currencyItem.ItemID;
                UCCompanyWeeklyList.DataSource = list;

            }
            else
            {
                //List<BusinessWeeklyReportInfo> list = BusinessInfoService.GetBusinessWeeklyReportList(null, currencyItem.ItemID, LocalData.IsEnglish);
                //this.UCGeneralWeeklyList.DataSource = list;
                UCGeneralWeeklyList.WeeklyDate = currencyItem.ItemID;
                UCGeneralWeeklyList.BindDataList();

            }
        }
        /// <summary>
        /// 获得当前选择的日期
        /// </summary>
        /// <returns></returns>
        private DateWeekly GetSeletctWeekly()
        {
            if (cmbWeekDate.EditValue == null)
            {
                return null;
            }
            int index = Convert.ToInt32(cmbWeekDate.EditValue);

            DateWeekly currencyItem = (from d in dateWeeklyList where d.Index == index select d).SingleOrDefault();
            if (currencyItem == null)
            {
                return null;
            }
            return currencyItem;
        }
        /// <summary>
        /// 设置工具栏是否可用
        /// </summary>
        /// <param name="isEnabled"></param>
        private void SetToolButtom(bool isEnabled)
        {
            barSave.Enabled = isEnabled;
            barRemove.Enabled = isEnabled;
            barAppend.Enabled = isEnabled;
        }
        /// <summary>
        /// 当前活动的Tab发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabWeeklyReport_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            DateWeekly selectWeekly = GetSeletctWeekly();
            if (selectWeekly == null)
            {
                return;
            }
            if (tabWeeklyReport.SelectedTabPage == tabCompanyWeeklyReport)
            {
                //初始化客户数据
                if (!UCCompanyWeeklyList.IsInitCombox)
                {
                    UCCompanyWeeklyList.InitCombox();
                }
                if (UCCompanyWeeklyList.WeeklyDate != selectWeekly.ItemID)
                {
                    SetDataSource();
                }
            }
            else
            {
                if (UCGeneralWeeklyList.WeeklyDate != selectWeekly.ItemID)
                {
                    SetDataSource();
                }
            }
        }
        /// <summary>
        /// 如果当前选择的周日期，是当前日期的两周前，则禁用按钮
        /// </summary>
        /// <param name="currencyItem"></param>
        private void Setbutton(DateWeekly currencyItem)
        {
            DateTime dateTime = BusinessInfoService.GetServerDate();
            if ((dateTime - currencyItem.EndDate).TotalDays > 14)
            {
                barSave.Enabled = false;
                barAppend.Enabled = false;
                barRemove.Enabled = false;
            }
            else
            {
                barSave.Enabled = true;
                barAppend.Enabled = true;
                barRemove.Enabled = true;
            }

        }
        #endregion

        #region
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            cmbWeekDate.Focus();
            Workitem.Commands[BWRCommonConstants.Command_SaveData].Execute();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAppend_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BWRCommonConstants.Command_AddData].Execute();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barRemove_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BWRCommonConstants.Command_DeleteData].Execute();
        }

        #endregion

        #region 关闭或切换周日期时，验证并提示保存
        /// <summary>
        /// 周日期切换时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbWeekDate_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (!isAllowSwitching())
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// 窗体关闭时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WeeklyReportPart_Closing(object sender, FormClosingEventArgs e)
        {
            if (!isAllowSwitching())
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// 是否允许切换
        /// </summary>
        /// <returns></returns>
        private bool isAllowSwitching()
        {
            if (!UCCompanyWeeklyList.IsDirty)
            {
                return true;
            }
            else
            {
                DialogResult dialog = Utility.EnquireIsSaveCurrentDataByUpdated();

                if (dialog == DialogResult.OK)
                {
                    if (!UCCompanyWeeklyList.Save())
                    {
                        return false;
                    }
                }
                if (dialog == DialogResult.No)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region 分组
        private void rgGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rgGroup.SelectedIndex == 0)
            {
                Workitem.Commands[BWRCommonConstants.Command_GroupByShipperingLine].Execute();
            }
            else
            {
                Workitem.Commands[BWRCommonConstants.Command_GroupByCompany].Execute();
            }
        }
        #endregion
    }

    /// <summary>
    /// 周日期对象
    /// </summary>
    public class DateWeekly
    {
        /// <summary>
        /// 顺序
        /// </summary>
        public Int32 Index
        {
            get;
            set;
        }
        /// <summary>
        /// 开始日期 
        /// </summary>
        public DateTime StartDate
        {
            get;
            set;
        }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate
        {
            get;
            set;
        }
        /// <summary>
        /// 年份
        /// </summary>
        public Int32 Year
        {
            get;
            set;
        }
        /// <summary>
        /// 周数
        /// </summary>
        public Int32 Weekly
        {
            get;
            set;
        }
        private string itemID;
        /// <summary>
        /// 当前项ID
        /// </summary>
        public string ItemID
        {
            get
            {
                if (!string.IsNullOrEmpty(itemID))
                {
                    return itemID;
                }
                return "#" + Year + "-" + Weekly;
            }
            set
            {
                itemID = value;
            }

        }
        private string itemName;
        /// <summary>
        /// 显示名称
        /// </summary>
        public string ItemName
        {
            get
            {
                if (!string.IsNullOrEmpty(itemName))
                {
                    return itemName;
                }
                return "#" + Year + "-" + Weekly + "( " + StartDate.ToShortDateString() + "~" + EndDate.ToShortDateString() + " )";
            }
            set
            {
                itemName = value;  
            }
        }

    }

}
