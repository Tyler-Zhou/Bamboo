using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.OceanExport.UI.Common
{
    [ToolboxItem(false)]
    public partial class FreightInfo : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }

        [ServiceDependency]
        public IOceanExportService OEService { get; set; }

        #endregion

        #region 本地变量

        List<FreightList> _prices = new List<FreightList>();
        List<string> _unitList = new List<string>();
        string _scno = string.Empty;
        Guid? _placeOfReceiptID;
        Guid _polid;
        Guid? _podid;
        Guid? _deliveryid;
        Guid? _finalDestination;
        Guid? _shipownerid;
        string _goodsdes = string.Empty;//品名
        Guid? _freightID = null;
        DateTime? _etd = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
       
        
        
        bool _isMore = false;


        #endregion


        public FreightInfo()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(SelectFees_Disposed);
           // this.gridViewFee.RowStyle+=new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler();
        }

        void SelectFees_Disposed(object sender, EventArgs e)
        {
            if (this.Workitem != null) { this.Workitem.Items.Remove(this); }
        }


        ICP.FRM.UI.SearchRate.OceanRateFeeDetailPart feeDetailPart
        {
            get;
            set;
        }

        void InitControls()
        {
            feeDetailPart = Workitem.Items.AddNew<ICP.FRM.UI.SearchRate.OceanRateFeeDetailPart>();
            this.pnlFee.Controls.Clear();
            feeDetailPart.Dock = DockStyle.Fill;
            this.pnlFee.Controls.Add(feeDetailPart);

            this.pnlFee.Text = LocalData.IsEnglish ? "Fees" : "费用明细";


            this.bsFreight.DataSource = _prices;
            TransSearchOceanRateList(_unitList);
            this.bsFreight.ResetBindings(false);
            //SetFeeDataSource();

           

        }

        private void bsFreight_PositionChanged(object sender, EventArgs e)
        {
            SetFeeDataSource();
        }
        private void SetFeeDataSource()
        {
            FreightList item = bsFreight.Current as FreightList;
            if (item != null && feeDetailPart != null)
            {
                feeDetailPart.DataSource = item.ID;
            }
        }
           

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                pnlTop.Visible = false;
                this.gridViewFee.IndicatorWidth = 40;
                InitControls();
                InitListDate();
                
            }
        }
        /// <summary>
        /// 初始化列表数据
        /// </summary>
        private void InitListDate()
        {
            gridViewFee.BestFitColumns();
        }

        private void gridViewFee_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }
        #region 外部接口

        public void SetDataSource(
         FreightParameter freightParameter)
        {
            _prices = freightParameter.prices;
            _unitList = freightParameter.unitList;
            this.txtFreight.Text = _scno = freightParameter.scno;
            _shipownerid = freightParameter.shipownerid;
            _placeOfReceiptID = freightParameter.placeOfReceiptID;
            _polid = freightParameter.polid;
            _podid = freightParameter.podid;
            _finalDestination = freightParameter.finalDestinationID;
            _goodsdes = freightParameter.goodsdes;
            _freightID = freightParameter.freightID;
            if (freightParameter.etd != null && freightParameter.etd != DateTime.MinValue)
            {
                _etd = freightParameter.etd;
            }

        }
        private FreightList _selectedPrice;

        public FreightList SelectedPrice
        {
            get { return _selectedPrice; }
            set { _selectedPrice = value; }
        }

        #endregion

        #region button events

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                btnCancle.PerformClick();
            }

            return base.ProcessDialogKey(keyData);
        }

        #region 查询  by  No/date
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Guid? fdID = Guid.Empty;
                if (this.chkIsFinalDestination.Checked)
                {
                    fdID = _finalDestination;
                }


                FreightDataList freightDataList = OEService.GetFreight(txtFreight.Text.Trim(), _shipownerid,
                                                                    _placeOfReceiptID,
                                                                    _polid,
                                                                    _podid,
                                                                    fdID,
                                                                    string.Empty,
                                                                    new DateTime(dteStartDate.DateTime.Year,dteStartDate.DateTime.Month,dteStartDate.DateTime.Day,0,0,0),
                                                                    new DateTime(dtpEndDate.DateTime.Year, dtpEndDate.DateTime.Month, dtpEndDate.DateTime.Day, 23, 59,59),this._freightID);



                bsFreight.DataSource = freightDataList.DataList;
                bsFreight.ResetBindings(false);

                this.gridViewFee.BestFitColumns();
                TransSearchOceanRateList(freightDataList.UnitList);
            }
            catch(Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
            }
        }

        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (bsFreight.Current == null) return;

            FreightList current = bsFreight.Current as FreightList;

            //if (string.IsNullOrEmpty(current.Carrier)) return;

            _selectedPrice = current;
            this.FindForm().DialogResult = DialogResult.OK;
            this.FindForm().Close();

        }

        bool isLoad = false;
        private void btnShowMore_Click(object sender, EventArgs e)
        {
            _isMore = !_isMore;

            if (_isMore)
            {
                btnShowMore.Text = LocalData.IsEnglish ? "HideMore(&M)" : "隐藏更多(&M)";
                pnlTop.Visible = true;

                if (!isLoad)
                {
                    if (Utility.GuidIsNullOrEmpty(_freightID))
                    {
                        this.dteStartDate.DateTime = DateTime.Now.AddDays(-15);
                        this.dteStartDate.EditValue = DateTime.Now.AddDays(-15);
                        this.dtpEndDate.DateTime = DateTime.Now.AddDays(15);
                        this.dtpEndDate.EditValue = DateTime.Now.AddDays(15);
                    }
                    else
                    {
                        this.dteStartDate.DateTime = _etd.Value.AddDays(-7);
                        this.dteStartDate.EditValue = _etd.Value.AddDays(-7);
                        this.dtpEndDate.DateTime = _etd.Value.AddDays(7);
                        this.dtpEndDate.EditValue = _etd.Value.AddDays(7);
                    }
                }
                isLoad = true;

            }
            else
            {
                btnShowMore.Text = LocalData.IsEnglish ? "ShowMore(&M)" : "显示更多(&M)";
                pnlTop.Visible = !pnlTop.Visible;
            }
            FreightDataList freightDataList = OEService.GetFreight(_scno, _shipownerid, _placeOfReceiptID, _polid, _podid, _finalDestination, string.Empty,
                                              DateTime.SpecifyKind(this.dteStartDate.DateTime, DateTimeKind.Unspecified),
                                              DateTime.SpecifyKind(this.dtpEndDate.DateTime, DateTimeKind.Unspecified), this._freightID);


            this.bsFreight.DataSource = freightDataList.DataList;
             gridViewFee.BestFitColumns();
             TransSearchOceanRateList(freightDataList.UnitList);
        }

        private void TransSearchOceanRateList(List<string> unitsNameList)
        {
            this.colFreightNo.VisibleIndex = 0;
            this.colBeginDate.VisibleIndex = 1;            
            this.colEndDate.VisibleIndex = 2;
            this.colPol.VisibleIndex = 3;
            this.colPod.VisibleIndex = 4;
            this.colDelivery.VisibleIndex = 5;
            this.colDestination.VisibleIndex = 6;
            this.colTerm.VisibleIndex = 7;

            int visibleIndex = 7;

            #region  SetVisible= false;
            colRate_45FR.Visible = false;
            colRate_40RF.Visible = false;
            colRate_45HT.Visible = false;
            colRate_20RF.Visible = false;
            colRate_20HQ.Visible = false;
            colRate_20TK.Visible = false;
            colRate_20GP.Visible = false;
            colRate_40TK.Visible = false;
            colRate_40OT.Visible = false;
            colRate_20FR.Visible = false;
            colRate_45GP.Visible = false;
            colRate_40GP.Visible = false;
            colRate_45RF.Visible = false;
            colRate_20RH.Visible = false;
            colRate_45OT.Visible = false;
            colRate_40NOR.Visible = false;
            colRate_40FR.Visible = false;
            colRate_20OT.Visible = false;
            colRate_45TK.Visible = false;
            colRate_20NOR.Visible = false;
            colRate_40HT.Visible = false;
            colRate_40RH.Visible = false;
            colRate_45RH.Visible = false;
            colRate_45HQ.Visible = false;
            colRate_20HT.Visible = false;
            colRate_40HQ.Visible = false;
            colRate_53HQ.Visible = false;
            #endregion


            foreach (var item in unitsNameList)
            {
                #region  SetVisible= true;

                switch (item)
                {
                    case "20GP": colRate_20GP.VisibleIndex = visibleIndex + 1; break;
                    case "40GP": colRate_40GP.VisibleIndex = visibleIndex + 2; break;
                    case "40HQ": colRate_40HQ.VisibleIndex = visibleIndex + 3; break;
                    case "45HQ": colRate_45HQ.VisibleIndex = visibleIndex + 4; break;
                    case "20NOR": colRate_20NOR.VisibleIndex = visibleIndex + 5; break;
                    case "40NOR": colRate_40NOR.VisibleIndex = visibleIndex + 6; break;

                    case "20FR": colRate_20FR.VisibleIndex = visibleIndex + 7; break;
                    case "20RH": colRate_20RH.VisibleIndex = visibleIndex + 8; break;
                    case "20RF": colRate_20RF.VisibleIndex = visibleIndex + 9; break;
                    case "20HQ": colRate_20HQ.VisibleIndex = visibleIndex + 19; break;
                    case "20TK": colRate_20TK.VisibleIndex = visibleIndex + 10; break;
                    case "20OT": colRate_20OT.VisibleIndex = visibleIndex + 11; break;
                    case "20HT": colRate_20HT.VisibleIndex = visibleIndex + 12; break;

                    case "40TK": colRate_40TK.VisibleIndex = visibleIndex + 13; break;
                    case "40OT": colRate_40OT.VisibleIndex = visibleIndex + 14; break;
                    case "40FR": colRate_40FR.VisibleIndex = visibleIndex + 15; break;
                    case "40HT": colRate_40HT.VisibleIndex = visibleIndex + 16; break;
                    case "40RH": colRate_40RH.VisibleIndex = visibleIndex + 17; break;
                    case "40RF": colRate_40RF.VisibleIndex = visibleIndex + 18; break;

                    case "45GP": colRate_45GP.VisibleIndex = visibleIndex + 19; break;
                    case "45RF": colRate_45RF.VisibleIndex = visibleIndex + 20; break;
                    case "45HT": colRate_45HT.VisibleIndex = visibleIndex + 21; break;
                    case "45FR": colRate_45FR.VisibleIndex = visibleIndex + 22; break;
                    case "45OT": colRate_45OT.VisibleIndex = visibleIndex + 23; break;
                    case "45TK": colRate_45TK.VisibleIndex = visibleIndex + 24; break;
                    case "45RH": colRate_45RH.VisibleIndex = visibleIndex + 25; break;

                    case "53HQ": colRate_53HQ.VisibleIndex = visibleIndex + 26; break;
                }

                #endregion
            }





            this.colGoods.VisibleIndex = visibleIndex+27;
            this.colCarrier.VisibleIndex = visibleIndex+28;
            this.colShipper.VisibleIndex = visibleIndex+29;
            this.colConsignee.VisibleIndex = visibleIndex+30;
            this.colNotifyPart.VisibleIndex = visibleIndex+31;
            this.colAdditionalCharges.VisibleIndex = visibleIndex+32;
            this.colPaymentTreaty.VisibleIndex = visibleIndex+33;
            this.colRemark.VisibleIndex = visibleIndex+34;
     
            this.colRemark.Width = 150;
            this.colGoods.Width = 250;
           
           
        }
      

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.FindForm().DialogResult = DialogResult.Cancel;
            this.FindForm().Close();
        }
        #endregion

        private void gridViewFee_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0 || _freightID == null) return;
            FreightList list = gridViewFee.GetRow(e.RowHandle) as FreightList;
            if (list == null) return;

            if (_freightID!=null&&list.ID == _freightID.Value)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Error);
            }

        }

        private void gridViewFee_DoubleClick(object sender, EventArgs e)
        {
            if (bsFreight.Current == null) return;

            FreightList current = bsFreight.Current as FreightList;

            //if (string.IsNullOrEmpty(current.Carrier)) return;

            _selectedPrice = current;

            this.FindForm().DialogResult = DialogResult.OK;
            this.FindForm().Close();

        }


        void gridViewFee_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex > -1)
            {
                //this.gridViewFee.DoubleClick -= new DataGridViewCellEventHandler(gridViewFee_DoubleClick);
                //int columnIndex = e.ColumnIndex;
                //if (columnIndex >= 14 && columnIndex <= 17)
                //{
                //    columnIndex = columnIndex - 14;
                //}

                ////DataGridViewSortHelp.Sort<FreightData>(bsTP, dgvTp, columnIndex);

                //this.gridViewFee.DoubleClick += new DataGridViewCellEventHandler(gridViewFee_DoubleClick);
            }
        }

    }

    [Serializable]
    public class FreightParameter
    {
         public List<FreightList> prices{get;set;}
         public List<string> unitList { get; set; }
         public string scno { get; set; }
         public Guid? shipownerid { get; set; }
         public Guid? placeOfReceiptID { get; set; }
         public Guid polid { get; set; }
         public Guid? podid { get; set; }
         public Guid? finalDestinationID { get; set; }
         public string goodsdes { get; set; }
         public Guid? freightID { get; set; }
         public DateTime? etd { get; set; }
      }


}
