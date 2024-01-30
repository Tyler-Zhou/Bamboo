using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.FRM.ServiceInterface;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils.Paint;
using DevExpress.XtraGrid.Drawing;
using DevExpress.Utils;

namespace ICP.FRM.UI.BookingReport
{
    [ToolboxItem(false)]
    public partial class BookingReportListPart : BaseListPart
    {
        public BookingReportListPart()
        {
            InitializeComponent();
            Disposed += delegate {
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
        #endregion

        #region 自有属性
        /// <summary>
        /// 当前行数据
        /// </summary>
        public BookingReportData CurrentRow
        {
            get
            {
                return bsList.Current as BookingReportData;
            }
        }
        /// <summary>
        /// 数据列表
        /// </summary>
        public List<BookingReportData> DataList
        {
            get
            {
                List<BookingReportData> list = bsList.DataSource as List<BookingReportData>;
               if (list == null)
               {
                   list = new List<BookingReportData>();
               }
               return list;
            }
        }
        bool IsViewReserve = false;

        List<string> rateColumn = new List<string>();

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
                BindDataList(value);
            }
        }
        private void BindDataList(object value)
        {
            List<BookingReportData> list = value as List<BookingReportData>;
            if (list == null)
            {
                list = new List<BookingReportData>();
            }
            bsList.DataSource = list;
            bsList.ResetBindings(false);

            SetGroupBy();
        }
        #endregion

        #region 初始化
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();            
            }
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            Utility.ShowGridRowNo(gvMain);

            #region 揽货类型
            //List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<SearchPriceStatus>> searchPriceStatus
            //    = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<SearchPriceStatus>(LocalData.IsEnglish);
            //foreach (var item in searchPriceStatus)
            //{
            //    cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value, (short)item.Value - 1));
            //}
            #endregion


        }

        #endregion

        #region 命令

        [CommandHandler(BookingReportCommonConstants.Command_ExportToExcel)]
        public void Command_ExportToExcel(object o, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = "BookingReport";
            saveFile.Filter = "xls files(*.xls)|*.xls";
            saveFile.RestoreDirectory = true;
            saveFile.FilterIndex = 2;
            if (saveFile.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string fileName = saveFile.FileName.ToString();
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Exporting......");

            gvMain.ExportToXls(fileName);

            LoadingServce.CloseLoadingForm(theradID);

        }

        #endregion

        #region 分组方式
        public List<BPGroupBy> GroupByList = new List<BPGroupBy>();
        /// <summary>
        /// 设置分组方式
        /// </summary>
        public void SetGroupBy()
        {
            gvMain.GroupSummary.Clear();
            gvMain.SortInfo.Clear();


            colCarrierName.Visible = true;
            colCompanyName.Visible = true;
            colShipLineName.Visible = true;
            colVoyageName.Visible = true;

            #region 公司
            if (GroupByList.Contains(BPGroupBy.Company) && 
               !GroupByList.Contains(BPGroupBy.ShipLine) && 
               !GroupByList.Contains(BPGroupBy.Carrier)&&
               !GroupByList.Contains(BPGroupBy.VoyageName))
            {
                gvMain.SortInfo.AddRange(new GridColumnSortInfo[] {
                        new GridColumnSortInfo(colCompanyName, ColumnSortOrder.Ascending)});

                gvMain.GroupSummary.AddRange(new GridSummaryItem[] {
                        new GridGroupSummaryItem(SummaryItemType.Count, "CompanyName", null, "(Row Count:{0})"),
                        new GridGroupSummaryItem(SummaryItemType.Sum, "ContainerCount", null, "(ContainerCount:{0})"),
                        new GridGroupSummaryItem(SummaryItemType.Sum, "Profit", null, "(Profit:{0})")});

                colCompanyName.Visible = false;
            }
            #endregion

            #region 公司+航线
            else if (GroupByList.Contains(BPGroupBy.Company) && 
                     GroupByList.Contains(BPGroupBy.ShipLine) && 
                    !GroupByList.Contains(BPGroupBy.Carrier)&&
                    !GroupByList.Contains(BPGroupBy.VoyageName))
            {
                gvMain.SortInfo.AddRange(new GridColumnSortInfo[] {
                         new GridColumnSortInfo(colCompanyName, ColumnSortOrder.Ascending),
                         new GridColumnSortInfo(colShipLineName, ColumnSortOrder.Ascending)});

                gvMain.GroupSummary.AddRange(new GridSummaryItem[] {
                 new GridGroupSummaryItem(SummaryItemType.Count, "CompanyName", null, "(Row Count: {0})"),
                 new GridGroupSummaryItem(SummaryItemType.Sum, "ContainerCount", null, "(ContainerCount: {0})"),
                 new GridGroupSummaryItem(SummaryItemType.Sum, "Profit", null, "(Profit: {0})")});

                colCompanyName.Visible = false;
                colShipLineName.Visible = false;
            }
            #endregion

            #region 公司+船东
            else if (GroupByList.Contains(BPGroupBy.Company) && 
                    !GroupByList.Contains(BPGroupBy.ShipLine) && 
                     GroupByList.Contains(BPGroupBy.Carrier)&&
                    !GroupByList.Contains(BPGroupBy.VoyageName))
            {
                gvMain.SortInfo.AddRange(new GridColumnSortInfo[] {
                        new GridColumnSortInfo(colCompanyName, ColumnSortOrder.Ascending),
                         new GridColumnSortInfo(colCarrierName, ColumnSortOrder.Ascending)});

                gvMain.GroupSummary.AddRange(new GridSummaryItem[] {
                 new GridGroupSummaryItem(SummaryItemType.Count, "CompanyName", null, "(Row Count:{0})"),
                 new GridGroupSummaryItem(SummaryItemType.Sum, "ContainerCount", null, "(ContainerCount: {0})"),
                 new GridGroupSummaryItem(SummaryItemType.Sum, "Profit", null, "(Profit: {0})")});

                colCompanyName.Visible = false;
                colCarrierName.Visible = false;
            }
            #endregion

            #region 公司+船名
            else if (GroupByList.Contains(BPGroupBy.Company) &&
                    !GroupByList.Contains(BPGroupBy.ShipLine) &&
                    !GroupByList.Contains(BPGroupBy.Carrier) &&
                     GroupByList.Contains(BPGroupBy.VoyageName))
            {
                gvMain.SortInfo.AddRange(new GridColumnSortInfo[] {
                         new GridColumnSortInfo(colCompanyName, ColumnSortOrder.Ascending),
                         new GridColumnSortInfo(colVoyageName, ColumnSortOrder.Ascending)});

                gvMain.GroupSummary.AddRange(new GridSummaryItem[] {
                 new GridGroupSummaryItem(SummaryItemType.Count, "CompanyName", null, "(Row Count:{0})"),
                 new GridGroupSummaryItem(SummaryItemType.Sum, "ContainerCount", null, "(ContainerCount: {0})"),
                 new GridGroupSummaryItem(SummaryItemType.Sum, "Profit", null, "(Profit: {0})")});

                colCompanyName.Visible = false;
                colVoyageName.Visible = false;
            }
            #endregion

            #region 公司+航线+船名
            else if (GroupByList.Contains(BPGroupBy.Company) &&
                     GroupByList.Contains(BPGroupBy.ShipLine) &&
                    !GroupByList.Contains(BPGroupBy.Carrier) &&
                     GroupByList.Contains(BPGroupBy.VoyageName))
            {
                gvMain.SortInfo.AddRange(new GridColumnSortInfo[] {
                          new GridColumnSortInfo(colCompanyName, ColumnSortOrder.Ascending),
                          new GridColumnSortInfo(colShipLineName, ColumnSortOrder.Ascending),
                          new GridColumnSortInfo(colVoyageName, ColumnSortOrder.Ascending)});

                gvMain.GroupSummary.AddRange(new GridSummaryItem[] {
                 new GridGroupSummaryItem(SummaryItemType.Count, "CompanyName", null, "(Row Count:{0})"),
                 new GridGroupSummaryItem(SummaryItemType.Sum, "ContainerCount", null, "(ContainerCount: {0})"),
                 new GridGroupSummaryItem(SummaryItemType.Sum, "Profit", null, "(Profit: {0})")});

                colCompanyName.Visible = false;
                colVoyageName.Visible = false;
                colVoyageName.Visible = false;
            }
            #endregion

            #region 公司+船东+船名
            else if (GroupByList.Contains(BPGroupBy.Company) &&
                    !GroupByList.Contains(BPGroupBy.ShipLine) &&
                     GroupByList.Contains(BPGroupBy.Carrier) &&
                     GroupByList.Contains(BPGroupBy.VoyageName))
            {
                gvMain.SortInfo.AddRange(new GridColumnSortInfo[] {
                        new GridColumnSortInfo(colCompanyName, ColumnSortOrder.Ascending),
                        new GridColumnSortInfo(colCarrierName, ColumnSortOrder.Ascending),
                        new GridColumnSortInfo(colVoyageName, ColumnSortOrder.Ascending)});

                gvMain.GroupSummary.AddRange(new GridSummaryItem[] {
                 new GridGroupSummaryItem(SummaryItemType.Count, "CompanyName", null, "(Row Count:{0})"),
                 new GridGroupSummaryItem(SummaryItemType.Sum, "ContainerCount", null, "(ContainerCount: {0})"),
                 new GridGroupSummaryItem(SummaryItemType.Sum, "Profit", null, "(Profit: {0})")});

                colCompanyName.Visible = false;
                colCarrierName.Visible = false;
                colVoyageName.Visible = false;
            }
            #endregion

            #region 公司+航线+船东
            else if (GroupByList.Contains(BPGroupBy.Company) && 
                     GroupByList.Contains(BPGroupBy.ShipLine) && 
                     GroupByList.Contains(BPGroupBy.Carrier)&&
                    !GroupByList.Contains(BPGroupBy.VoyageName))
            {   
                gvMain.SortInfo.AddRange(new GridColumnSortInfo[] {
                         new GridColumnSortInfo(colCompanyName, ColumnSortOrder.Ascending),
                         new GridColumnSortInfo(colShipLineName, ColumnSortOrder.Ascending),                
                         new GridColumnSortInfo(colCarrierName, ColumnSortOrder.Ascending)});

                gvMain.GroupSummary.AddRange(new GridSummaryItem[] {
                 new GridGroupSummaryItem(SummaryItemType.Count, "CompanyName", null, "(Row Count:{0})"),
                 new GridGroupSummaryItem(SummaryItemType.Sum, "ContainerCount", null, "(ContainerCount: {0})"),
                 new GridGroupSummaryItem(SummaryItemType.Sum, "Profit", null, "(Profit: {0})")});

                colCompanyName.Visible = false;
                colShipLineName.Visible = false;
                colCarrierName.Visible = false;
            }
            #endregion

            #region 公司+航线+船东+船名
            else if (GroupByList.Contains(BPGroupBy.Company) &&
                     GroupByList.Contains(BPGroupBy.ShipLine) &&
                     GroupByList.Contains(BPGroupBy.Carrier) &&
                     GroupByList.Contains(BPGroupBy.VoyageName))
            {
                gvMain.SortInfo.AddRange(new GridColumnSortInfo[] {
                         new GridColumnSortInfo(colCompanyName, ColumnSortOrder.Ascending),
                         new GridColumnSortInfo(colShipLineName, ColumnSortOrder.Ascending),                
                         new GridColumnSortInfo(colCarrierName, ColumnSortOrder.Ascending),
                         new GridColumnSortInfo(colVoyageName, ColumnSortOrder.Ascending)});

                gvMain.GroupSummary.AddRange(new GridSummaryItem[] {
                 new GridGroupSummaryItem(SummaryItemType.Count, "CompanyName", null, "(Row Count:{0})"),
                 new GridGroupSummaryItem(SummaryItemType.Sum, "ContainerCount", null, "(ContainerCount: {0})"),
                 new GridGroupSummaryItem(SummaryItemType.Sum, "Profit", null, "(Profit: {0})")});

                colCompanyName.Visible = false;
                colShipLineName.Visible = false;
                colCarrierName.Visible = false;
                colVoyageName.Visible = false;
            }
            #endregion

            #region 航线
            else if (!GroupByList.Contains(BPGroupBy.Company) && 
                      GroupByList.Contains(BPGroupBy.ShipLine) && 
                     !GroupByList.Contains(BPGroupBy.Carrier)&&
                     !GroupByList.Contains(BPGroupBy.VoyageName))
            {
                gvMain.SortInfo.AddRange(new GridColumnSortInfo[] {
                        new GridColumnSortInfo(colShipLineName, ColumnSortOrder.Ascending)});

                gvMain.GroupSummary.AddRange(new GridSummaryItem[] {
                        new GridGroupSummaryItem(SummaryItemType.Count, "ShipLineName", null, "(Row Count:{0})"),
                        new GridGroupSummaryItem(SummaryItemType.Sum, "ContainerCount", null, "(ContainerCount: {0})"),
                        new GridGroupSummaryItem(SummaryItemType.Sum, "Profit", null, "(Profit: {0})")});

                colShipLineName.Visible = false;
            }
            #endregion

            #region 航线+船名
            else if (!GroupByList.Contains(BPGroupBy.Company) &&
                      GroupByList.Contains(BPGroupBy.ShipLine) &&
                     !GroupByList.Contains(BPGroupBy.Carrier) &&
                      GroupByList.Contains(BPGroupBy.VoyageName))
            {
                gvMain.SortInfo.AddRange(new GridColumnSortInfo[] {
                        new GridColumnSortInfo(colShipLineName, ColumnSortOrder.Ascending),
                        new GridColumnSortInfo(colVoyageName, ColumnSortOrder.Ascending)});

                gvMain.GroupSummary.AddRange(new GridSummaryItem[] {
                        new GridGroupSummaryItem(SummaryItemType.Count, "ShipLineName", null, "(Row Count:{0})"),
                        new GridGroupSummaryItem(SummaryItemType.Sum, "ContainerCount", null, "(ContainerCount: {0})"),
                        new GridGroupSummaryItem(SummaryItemType.Sum, "Profit", null, "(Profit: {0})")});

                colShipLineName.Visible = false;
                colVoyageName.Visible = false;
            }
            #endregion

            #region 航线+船东
            else if (!GroupByList.Contains(BPGroupBy.Company) && 
                      GroupByList.Contains(BPGroupBy.ShipLine) && 
                      GroupByList.Contains(BPGroupBy.Carrier) &&
                     !GroupByList.Contains(BPGroupBy.VoyageName))
            {
                gvMain.SortInfo.AddRange(new GridColumnSortInfo[] {
                        new GridColumnSortInfo(colShipLineName, ColumnSortOrder.Ascending),
                        new GridColumnSortInfo(colCarrierName, ColumnSortOrder.Ascending)});

                gvMain.GroupSummary.AddRange(new GridSummaryItem[] {
                        new GridGroupSummaryItem(SummaryItemType.Count, "ShipLineName", null, "(Row Count:{0})"),
                        new GridGroupSummaryItem(SummaryItemType.Sum, "ContainerCount", null, "(ContainerCount: {0})"),
                        new GridGroupSummaryItem(SummaryItemType.Sum, "Profit", null, "(Profit: {0})")});

                colShipLineName.Visible = false;
                colCarrierName.Visible = false;
            }
            #endregion

            #region 航线+船东+船名
            else if (!GroupByList.Contains(BPGroupBy.Company) &&
                     GroupByList.Contains(BPGroupBy.ShipLine) &&
                     GroupByList.Contains(BPGroupBy.Carrier) &&
                     GroupByList.Contains(BPGroupBy.VoyageName))
            {
                gvMain.SortInfo.AddRange(new GridColumnSortInfo[] {
                        new GridColumnSortInfo(colShipLineName, ColumnSortOrder.Ascending),
                        new GridColumnSortInfo(colCarrierName, ColumnSortOrder.Ascending),
                        new GridColumnSortInfo(colVoyageName, ColumnSortOrder.Ascending)});

                gvMain.GroupSummary.AddRange(new GridSummaryItem[] {
                        new GridGroupSummaryItem(SummaryItemType.Count, "ShipLineName", null, "(Row Count:{0})"),
                        new GridGroupSummaryItem(SummaryItemType.Sum, "ContainerCount", null, "(ContainerCount: {0})"),
                        new GridGroupSummaryItem(SummaryItemType.Sum, "Profit", null, "(Profit: {0})")});

                colShipLineName.Visible = false;
                colCarrierName.Visible = false;
                colVoyageName.Visible = false;
            }
            #endregion

            #region 船东
            else if (!GroupByList.Contains(BPGroupBy.Company) && 
                     !GroupByList.Contains(BPGroupBy.ShipLine) && 
                      GroupByList.Contains(BPGroupBy.Carrier)&&
                     !GroupByList.Contains(BPGroupBy.VoyageName) )
            {
                gvMain.SortInfo.AddRange(new GridColumnSortInfo[] {
                        new GridColumnSortInfo(colCarrierName, ColumnSortOrder.Ascending)});

                gvMain.GroupSummary.AddRange(new GridSummaryItem[] {
                        new GridGroupSummaryItem(SummaryItemType.Count, "CarrierName", null, "(Row Count:{0})"),
                        new GridGroupSummaryItem(SummaryItemType.Sum, "ContainerCount", null, "(ContainerCount: {0})"),
                        new GridGroupSummaryItem(SummaryItemType.Sum, "Profit", null, "(Profit: {0})")});

                colCarrierName.Visible = false;
            }
            #endregion

            #region 船东+船名
            else if (!GroupByList.Contains(BPGroupBy.Company) &&
                     !GroupByList.Contains(BPGroupBy.ShipLine) &&
                      GroupByList.Contains(BPGroupBy.Carrier) &&
                      GroupByList.Contains(BPGroupBy.VoyageName))
            {
                gvMain.SortInfo.AddRange(new GridColumnSortInfo[] {
                        new GridColumnSortInfo(colCarrierName, ColumnSortOrder.Ascending),
                        new GridColumnSortInfo(colVoyageName, ColumnSortOrder.Ascending)});

                gvMain.GroupSummary.AddRange(new GridSummaryItem[] {
                        new GridGroupSummaryItem(SummaryItemType.Count, "CarrierName", null, "(Row Count:{0})"),
                        new GridGroupSummaryItem(SummaryItemType.Sum, "ContainerCount", null, "(ContainerCount: {0})"),
                        new GridGroupSummaryItem(SummaryItemType.Sum, "Profit", null, "(Profit: {0})")});

                colCarrierName.Visible = false;
                colVoyageName.Visible = false;
            }
            #endregion

            #region 船名
            else if (!GroupByList.Contains(BPGroupBy.Company) &&
                     !GroupByList.Contains(BPGroupBy.ShipLine) &&
                     !GroupByList.Contains(BPGroupBy.Carrier) &&
                      GroupByList.Contains(BPGroupBy.VoyageName))
            {
                gvMain.SortInfo.AddRange(new GridColumnSortInfo[] {
                        new GridColumnSortInfo(colVoyageName, ColumnSortOrder.Ascending)});

                gvMain.GroupSummary.AddRange(new GridSummaryItem[] {
                        new GridGroupSummaryItem(SummaryItemType.Count, "VoyageName", null, "(Row Count:{0})"),
                        new GridGroupSummaryItem(SummaryItemType.Sum, "ContainerCount", null, "(ContainerCount: {0})"),
                        new GridGroupSummaryItem(SummaryItemType.Sum, "Profit", null, "(Profit: {0})")});
               
                colVoyageName.Visible = false;
            }
            #endregion

            gvMain.GroupCount = GroupByList.Count;
            gcMain.Refresh();
           
        }

        /// <summary>
        /// 粗体显示统计数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gvMain_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
        {
            GridGroupRowInfo info = e.Info as GridGroupRowInfo;
            GridGroupRowPainter painter = e.Painter as GridGroupRowPainter;
            if (info == null || painter == null) return;
            if (info.RowHandle == gvMain.FocusedRowHandle) return;

            AppearanceObject appObject = painter.GetGroupColorAppearance(info);
            GridViewInfo vi = gvMain.GetViewInfo() as GridViewInfo;
            vi.Painter.ElementsPainter.GroupRow.DrawGroupRowBackground(e.Info);
            e.Painter.DrawObject(e.Info);

            MultiColorDrawStringParams mcds = new MultiColorDrawStringParams(appObject);
            mcds.Text = gvMain.GetGroupRowDisplayText(info.RowHandle);
           
            CharacterRangeWithFormat crwf0 = new CharacterRangeWithFormat(
                0, mcds.Text.Length, appObject.GetForeColor(), appObject.GetBackColor());

            int rowIndex= mcds.Text.IndexOf("Row Count:");
            int ccIndex = mcds.Text.IndexOf("ContainerCount:");
            int profitIndex = mcds.Text.IndexOf("Profit:");


            #region ForeColor
            CharacterRangeWithFormat crwfRow = new CharacterRangeWithFormat(
             rowIndex + 10, ccIndex - rowIndex - 14, colorsSortValue.ForeColor, colorsSortValue.BackColor);

            CharacterRangeWithFormat crwfCC = new CharacterRangeWithFormat(
            ccIndex + 15, profitIndex - ccIndex - 19, colorsSortValue.ForeColor, colorsSortValue.BackColor);

            CharacterRangeWithFormat crwfProfit = new CharacterRangeWithFormat(
            profitIndex + 7, mcds.Text.Length - profitIndex - 8, colorsSortValue.ForeColor, colorsSortValue.BackColor);

            mcds.Ranges = new CharacterRangeWithFormat[] { crwf0, crwfRow, crwfCC, crwfProfit };


            #endregion



            Rectangle r = painter.GetGroupClientBounds(info);
            r.X += info.ButtonBounds.Width + 7;
            r.Width -= (info.ButtonBounds.Width + 7);
            mcds.Bounds = r;

            e.Cache.Paint.MultiColorDrawString(e.Cache, mcds);
            e.Handled = true;

        }

        ColorsObject colorsSortValue = new ColorsObject(Color.Blue, Color.Empty);
        #endregion

      
    }


    public class ColorsObject
    {
        Color fforeColor, fbackColor;
        public ColorsObject(Color fforeColor, Color fbackColor)
        {
            this.fforeColor = fforeColor;
            this.fbackColor = fbackColor;
        }
        public Color ForeColor { get { return fforeColor; } set { fforeColor = value; } }
        public Color BackColor { get { return fbackColor; } set { fbackColor = value; } }
    }

}
