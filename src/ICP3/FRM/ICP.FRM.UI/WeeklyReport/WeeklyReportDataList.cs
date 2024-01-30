using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Controls;

namespace ICP.FRM.UI
{
    [ToolboxItem(false)]
    public partial class WeeklyReportDataList : BasePart
    {

        public WeeklyReportDataList()
        {
            InitializeComponent();
            Disposed += delegate
            {
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

        #endregion

        #region 属性


        /// <summary>
        /// 周日期 
        /// </summary>
        public string WeeklyDate
        {
            get;
            set;
        }

        #endregion

        #region 初始化控件
        /// <summary>
        /// 加载窗体
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!LocalData.IsDesignMode)
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
            if (!LocalCommonServices.PermissionService.HaveActionPermission(BWPermissionCommandConstants.WEEKLYREPORT_GENERALMANAGER))
            {
                colManager.Visible = false;
            }
            colShipline.Visible = false;
            colOrderByCode.Visible = false;


            repositoryItemRichTextEdit1.ContextMenu = new ContextMenu(); ;
            repositoryItemRichTextEdit1.KeyDown += new KeyEventHandler(repositoryItemRichTextEdit1_KeyDown);

        }

        void repositoryItemRichTextEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        #endregion

        #region 绑定数据
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void BindDataList()
        {
            List<BusinessWeeklyReportData> DataList = BusinessInfoService.GetBusinessWeeklyReportDataList(WeeklyDate, LocalData.IsEnglish);


            bsList.DataSource = DataList;
            bsList.ResetBindings(false);
                    

            //gvMain.GroupSummary.Add(DevExpress.Data.SummaryItemType.None, "ShiplineName", gvMain.Columns["ShiplineName"]);
            //gvMain.GroupSummary.Add(DevExpress.Data.SummaryItemType.None, "CompanyName", gvMain.Columns["CompanyName"]);
            //gvMain.Columns["ShiplineName"].GroupIndex = 0;
            //gvMain.ExpandAllGroups();

            //Utility.SetXraGridViewColWordrap(this.gvMain, "Marketing", true);
            //Utility.SetXraGridViewColWordrap(this.gvMain, "SellingGuide", true);
            //Utility.SetXraGridViewColWordrap(this.gvMain, "MangerComments", false);
        }
        #endregion

        #region 处理数据

        #region 保存数据时
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="prams"></param>
        public void EditPartSaved(object[] prams)
        {
            BindDataList();
            //   if (prams == null || prams.Length == 0) return;
            //List<BusinessWeeklyReportInfo> data = prams[0] as List<BusinessWeeklyReportInfo>;

            //List<BusinessWeeklyReportInfo> list = BusinessInfoService.GetBusinessWeeklyReportList(null, WeeklyAppCache.WeeklyDate, LocalData.IsEnglish);
            //if (list == null || list.Count == 0) return;

            //List<BusinessWeeklyReportInfo> source = this.DataSource as List<BusinessWeeklyReportInfo>;
            //if (source == null || source.Count == 0)
            //{
            //    bsList.DataSource = list;
            //    bsList.ResetBindings(false);
            //}
            //else
            //{
            //    foreach (BusinessWeeklyReportInfo items in list)
            //    {
            //        BusinessWeeklyReportInfo tager = source.Find(delegate(BusinessWeeklyReportInfo item) { return item.ID == items.ID; });
            //        if (tager == null)
            //        {
            //            bsList.Insert(0, items);
            //            bsList.ResetBindings(false);
            //        }
            //        else
            //        {
            //            Utility.CopyToValue(items, tager, typeof(BusinessWeeklyReportInfo));
            //            bsList.ResetItem(bsList.IndexOf(tager));
            //        }
            //    }
            //}

            //st-Hylun 2012-05-21
            //SavePostArticle();
            //st-End
        }

        #endregion

        #region 更新到网站
        /// <summary>
        /// 保存或者更新帖子内容
        /// </summary>
        /// 

        //口岸公司模板
        string CompanyTemplate = "<p><strong style='font-family: 宋体; color: #333333; font-size: 12pt; font-weight: bold;'>{0}</strong></p>&nbsp;";
        //

        string PostTemplate = @"<span style='font-family: 宋体; color: #333333; font-size: 10; font-weight: bold;'>{0}</span> &nbsp;&nbsp;
                               <div style='position: relative; left: 30px;'><table style='border: 1px solid #C5D5EF; ' cellpadding='0' cellspacing='0' width='648'><tr><td colspan='2' width='12%' style='border: 0px solid #C5D5EF; border-bottom-width:1px '>
                               <span style='font-family: 宋体; color: #333333; font-size: 10pt;'>{1}</span></td><td style='border: 0px solid #C5D5EF; border-bottom-width:1px ' colspan='2'>
                               <span style='font-family: 宋体; color: #333333; font-size: 9pt;'>{2}<br/>{3}<br/>{4}</span></td></tr>";
        //航线模板
        string ShiplineTemplate = @"<tr><td colspan='2' width='12%' style='border: 0px solid #C5D5EF; border-bottom-width:1px '>
                                   <span style='font-family: 宋体; color: #333333; font-size: 10pt;'>{0}</span></td><td style='border: 0px solid #C5D5EF; border-bottom-width:1px ' colspan='2'>
                                   <span style='font-family: 宋体; color: #333333; font-size: 9pt;'>{1}<br/>{2}<br/>{3}</span></td></tr>";

        public void SavePostArticle()
        {
            //List<BusinessWeeklyReportInfo> lstGeneralWeeklyInfo = DataSource;
            ////排序
            //lstGeneralWeeklyInfo.Sort(delegate(BusinessWeeklyReportInfo x, BusinessWeeklyReportInfo y) { return x.DivisionName.CompareTo(y.DivisionName); });

            //string DivisionName = string.Empty; string ShiplineName = string.Empty;
            //StringBuilder strBuf = new StringBuilder();
            //strBuf.Append("<div>");
            //foreach (BusinessWeeklyReportInfo item in lstGeneralWeeklyInfo)
            //{

            //    FixHtmlContent(item, CompanyTemplate, PostTemplate, ShiplineTemplate, DivisionName, ShiplineName, ref strBuf);

            //    DivisionName = item.DivisionName;
            //    ShiplineName = item.ShiplineName;
            //}
            //strBuf.Append("</table></div><br /><br /></div>");

            //string weeklyItem = WeeklyAppCache.WeeklyItem;
            //string subject = LocalData.IsEnglish ?  weeklyItem + "Weekly" :  weeklyItem + "周报";
            //string IpAddress = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].ToString();
            //Guid userID = LocalData.UserInfo.LoginID;
            ////发表或者更新帖子
            //BusinessInfoService.PostBusinessWeeklyToICP2(subject, strBuf.ToString(), IpAddress, weeklyItem, userID);

        }


        /// <summary>
        /// fix post content
        /// </summary>
        /// <param name="bwInfo"></param>
        /// <param name="companyTemplate">口岸公司模板</param>
        /// /// <param name="shiplineTemplate">航线模板</param>
        /// <param name="postTemplate">内容模板</param>
        /// <param name="divisionName">口岸公司名称</param>         
        /// <param name="shiplineName">航线名称</param>
        /// <param name="strBuf">追加的模板</param>
        public void FixHtmlContent(BusinessWeeklyReportInfo bwInfo, string companyTemplate, string postTemplate, string shiplineTemplate, string divisionName, string shiplineName, ref StringBuilder strBuf)
        {
            StringBuilder arrTemplate = new StringBuilder();
            //口岸公司
            bool IsNew = false;
            bool flag = false;
            if (string.IsNullOrEmpty(divisionName) || !bwInfo.DivisionName.Equals(divisionName))
            {
                //判断口岸公司时候相同
                if (!string.IsNullOrEmpty(divisionName) && !bwInfo.DivisionName.Equals(divisionName))
                {
                    arrTemplate.Append("</table></div>");
                    flag = true;
                }

                arrTemplate.AppendFormat(companyTemplate, bwInfo.DivisionName);
                IsNew = true;
            }
            //航线，船东，价格说明,评价等
            if (bwInfo.DivisionName.Equals(divisionName) || IsNew)
            {
                //航线相同
                if (!string.IsNullOrEmpty(shiplineName) && bwInfo.ShiplineName.Equals(shiplineName) && IsNew == false)
                {
                    arrTemplate.AppendFormat(shiplineTemplate, bwInfo.CarrierName, bwInfo.Rates, bwInfo.ShippingSpace, bwInfo.Description);
                }
                //航线不同
                else
                {
                    //判断航线是否相同
                    if (!string.IsNullOrEmpty(shiplineName) && !bwInfo.ShiplineName.Equals(shiplineName) && flag == false)
                    {
                        arrTemplate.Append("</table></div>");
                    }
                    arrTemplate.AppendFormat(postTemplate, bwInfo.ShiplineName, bwInfo.CarrierName, bwInfo.Rates, bwInfo.ShippingSpace, bwInfo.Description);
                }
            }
            strBuf.Append(arrTemplate);
        }

        #endregion

        #endregion

        #region 单击编辑
        /// <summary>
        /// 当前行数据
        /// </summary>
        private BusinessWeeklyReportData CurrentData
        {
            get
            {
                return bsList.Current as BusinessWeeklyReportData;
            }
        }
        private void gvMain_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column == colManager)
            {
                if (CurrentData == null)
                {
                    return;
                }

                EditMangerCommentsPart editManger = Workitem.Items.AddNew<EditMangerCommentsPart>();
                editManger.CompanyID = CurrentData.CompanyID;
                editManger.ShipLineID = CurrentData.ShiplineID;
                editManger.WeeklyData = WeeklyDate;
                editManger.Saved += delegate(object[] prams)
                {
                    if (prams[0] != null)
                    {
                        CurrentData.MangerComments = prams[0].ToString();
                        //Utility.SetXraGridViewColWordrap(this.gvMain, "MangerComments");
                    }
                };

                PartLoader.ShowDialog(editManger, "Edit Manger Comments");


            }
        }

        #endregion


        #region 样式
        /// <summary>
        /// 设置样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //if (e == null)
            //{
            //    return;
            //}

            //if (e.Column == this.colDivision)
            //{
                //BusinessWeeklyReportData data = this.gvMain.GetRow(e.RowHandle) as BusinessWeeklyReportData;
                //if (data != null && data.IsUpdate)
                //{
                //    e.Appearance.BackColor = Color.Pink;
                //}
            //}



        }
        #endregion

        [CommandHandler(BWRCommonConstants.Command_GroupByShipperingLine)]
        public void Command_GroupByShippingLine(object sender, EventArgs e)
        {
            gvMain.SortInfo.Clear();
            gvMain.SortInfo.AddRange(new GridColumnSortInfo[] {
            new GridColumnSortInfo(colShipline, ColumnSortOrder.Ascending)});

            colShipline.Visible = false;
            colDivision.Visible = true;
            colDivision.VisibleIndex = 0;

            //gvMain.Columns["ShiplineName"].GroupIndex = 0;
            gvMain.ExpandAllGroups();
        }

        [CommandHandler(BWRCommonConstants.Command_GroupByCompany)]
        public void Command_GroupByCompany(object sender, EventArgs e)
        {
            gvMain.SortInfo.Clear();
            gvMain.SortInfo.AddRange(new GridColumnSortInfo[] {
            new GridColumnSortInfo(colDivision, ColumnSortOrder.Ascending)});

            colShipline.Visible = true;
            colShipline.VisibleIndex = 0;
            colDivision.Visible = false;

            //gvMain.Columns["CompanyName"].GroupIndex = 0;
            gvMain.ExpandAllGroups();
        }


        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle <= 0)
            {
                return;
            }
            if (e.RowHandle % 2 != 0)
            {
                e.Appearance.BackColor = Color.Linen;
            }
        }

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                e.Handled = true;
            }
        }

   
      
    }
}
