using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FRM.ServiceInterface.DataObjects;
using System.Linq;
using DevExpress.Data;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Helper;
using System.Data;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.Controls;

namespace ICP.FRM.UI.ProfitRatios
{
    /// <summary>
    /// 列表界面
    /// </summary>
    [ToolboxItem(false)]
    public partial class PRListPart : BaseListPart
    {
        #region Service
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// Excel导出服务
        /// </summary>
        private IExcelService ExcelService
        {
            get
            {
                return ServiceClient.GetClientService<IExcelService>();
            }
        }

        

        #endregion

        #region Init
        /// <summary>
        /// 
        /// </summary>
        public PRListPart()
        {
            InitializeComponent();
            gvMain.SelectionChanged += GvMain_SelectionChanged;
            gvMain.RowStyle += GvMain_RowStyle;
            Disposed += delegate
            {
                gcMain.DataSource = null;
                gvMain.BeforeLeaveRow -= gvMain_BeforeLeaveRow;
                gvMain.CustomDrawRowIndicator -= gvMain_CustomDrawRowIndicator;
                gvMain.KeyDown -= gvMain_KeyDown;
                bsList.DataSource = null;
                bsList.PositionChanged -= bsMainList_PositionChanged;
                gvMain.RowStyle -= GvMain_RowStyle;
                bsList.Dispose();
                CurrentChanged = null;
                CurrentChanging = null;

                KeyDown = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };

            Load += new EventHandler(PRListPart_Load);

            if (!LocalData.IsEnglish)
            {
            }
        }




       
        bool _shown = false;

        void PRListPart_Load(object sender, EventArgs e)
        {
            DataSource = new List<BusinessStatisticsList>();
            _shown = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        private void InitControls()
        {
        }

        #endregion

        #region IListPart 成员
        /// <summary>
        /// 
        /// </summary>
        public override object Current
        {
            get { return bsList.Current; }
        }
        /// <summary>
        /// 
        /// </summary>
        protected BusinessStatisticsList CurrentRow
        {
            get { return Current as BusinessStatisticsList; }
        }

        /// <summary>
        /// 选择的数据
        /// </summary>
        List<BusinessStatisticsList> SelectRow
        {
            get
            {
                int[] rowIndexs = gvMain.GetSelectedRows();
                if (rowIndexs.Length == 0) return null;
                List<BusinessStatisticsList> tagers = rowIndexs.Select(item => gvMain.GetRow(item)).OfType<BusinessStatisticsList>().ToList();
                return tagers;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                bsList.DataSource = null;
                bsList.DataSource = value;

                bsList.ResetBindings(false);
                if (CurrentChanged != null)
                {
                    CurrentChanged(this, Current);
                }

                SetColumnsWidth();

                List<BusinessStatisticsList> list = value as List<BusinessStatisticsList>;
                string message = string.Format("{0} records found", list.Count);
                if (_shown)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), message);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public override void Refresh(object items)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public override void RemoveItem(int index)
        {
            bsList.RemoveAt(index);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public override void RemoveItem(object item)
        {
            bsList.Remove(item);
        }
        /// <summary>
        /// 
        /// </summary>
        public override event CurrentChangedHandler CurrentChanged;
        /// <summary>
        /// 
        /// </summary>
        public override event CancelEventHandler CurrentChanging;

        #endregion

        #region GridView Event
        public new event KeyEventHandler KeyDown;

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6)
            {
                Workitem.Commands[ProfitRatiosCommandConstants.Command_ShowSearch].Execute();
            }
        }

        private void bsMainList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null)
            {
                CurrentChanged(this, Current);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GvMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CurrentChanged != null)
            {
                CurrentChanged(this, SelectRow);
            }
        }

        private void gvMain_BeforeLeaveRow(object sender, DevExpress.XtraGrid.Views.Base.RowAllowEventArgs e)
        {
            if (CurrentChanging != null)
            {
                CancelEventArgs ce = new CancelEventArgs();
                CurrentChanging(this, ce);
                e.Allow = !ce.Cancel;
            }
        }

        private void gvMain_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }

        private void GvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            try
            {
                BusinessStatisticsList data = gvMain.GetRow(e.RowHandle) as BusinessStatisticsList;
                if (data != null && data.AdjustmentAmount != 0)
                {
                    e.Appearance.BackColor = Color.Pink;
                }
                else
                {
                    e.Appearance.BackColor = Color.Transparent;
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        #endregion

        #region Workitem Common

        #region 数据操作

        void SetColumnsWidth()
        {
            gvMain.BestFitColumns();
        }

        /// <summary>
        /// 
        /// </summary>
        [CommandHandler(ProfitRatiosCommandConstants.Command_Export)]
        public void Command_Export(object sender, EventArgs e)
        {
            try
            {
                List<BusinessStatisticsList> list = DataSource as List<BusinessStatisticsList>;
                if (list == null )
                    return;
                List<BusinessStatisticsList> exportList = list.CloneBySteam();
                if (exportList.Any(fItem=>fItem.ContainerDescription.Contains(";")))
                {
                    for (int eIndex = 0; eIndex < exportList.Count; eIndex++)
                    {
                        BusinessStatisticsList item = exportList[eIndex];
                        if (item.ContainerDescription.Contains(";"))
                        {
                            string[] cdArray = item.ContainerDescription.Split(';');
                            for (int sIndex = 1; sIndex < cdArray.Length; sIndex++)
                            {
                                BusinessStatisticsList newItem = item.CloneBySteam<BusinessStatisticsList>();
                                newItem.ContainerDescription = cdArray[sIndex];
                                exportList.Insert(eIndex,newItem);
                                eIndex++;
                            }
                            item.ContainerDescription = cdArray[0];
                        }
                    }
                }

                DataTable dt = DataTableHelper.ToDataTable(exportList, LocalData.IsEnglish,new string[]{ "CompanyName", "ContractNo", "CarrierName", "VesselName", "VoyageName", "ContainerTypeName", "ContainerRate", "ContainerVolume", "BookingNo", "OperationDate", "POLName","PODName", "PlaceOfDeliveryName", "AdjustmentAmount" });
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "Excel（*.xls）|*.xls",
                    FilterIndex = 1,
                    RestoreDirectory = true
                };
                if(sfd.ShowDialog() == DialogResult.OK)
                {
                    string filepath = sfd.FileName.ToString();
                    if (filepath.IsNullOrEmpty())
                    {
                        return;
                    }
                    string error = string.Empty;
                    ExcelService.DataTableToExcelFile2(dt, filepath, out error);
                    if(!error.IsNullOrEmpty())
                    {
                        MessageBoxService.ShowError(error);
                    }
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [CommandHandler(ProfitRatiosCommandConstants.Command_Print)]
        public void Command_Print(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        [CommandHandler(ProfitRatiosCommandConstants.Command_Refresh)]
        public void Command_RefreshData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
            }
        }


        [CommandHandler(ProfitRatiosCommandConstants.Command_BusinessClosing)]
        public void Command_BusinessClosing(object sender, EventArgs e)
        {
            BusinessClosePart closePart = Workitem.Items.AddNew<BusinessClosePart>();

            string title = LocalData.IsEnglish ? "Business Close" : "商务关帐";

            PartLoader.ShowDialog(closePart, title);
        }
        #endregion

        
    }
}
