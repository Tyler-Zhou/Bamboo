using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FRM.UI.SearchRate;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.OceanExport.UI.Common
{
    /// <summary>
    /// 海出运价合约/询价选择界面
    /// </summary>
    [ToolboxItem(false)]
    public partial class FreightInfo : BaseListPart
    {
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
        SelectType _type;//选择合约运价还是询价
        bool _isMore = false;
        bool _isOI = false;
        bool isLoad = false;
        private FreightList _selectedPrice;
        #endregion

        #region 属性
        /// <summary>
        /// 运价费用明细面板
        /// </summary>
        OceanRateFeeDetailPart feeDetailPart
        {
            get;
            set;
        }
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;

        }
        #endregion

        #region 服务
        

        /// <summary>
        /// 海出服务
        /// </summary>
        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }

        #endregion

        #region 构造函数
        /// <summary>
        /// 海出运价合约/询价选择界面
        /// </summary>
        public FreightInfo()
        {
            InitializeComponent();
            Disposed += SelectFees_Disposed;
        } 
        #endregion

        #region Event
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                pnlTop.Visible = false;
                gridViewFee.IndicatorWidth = 40;
                InitControls();
                InitListDate();

                if (_type != null && _type == SelectType.InquirePrice)
                {
                    pnlFee.Visible = false;
                }
                else
                {
                    pnlFee.Visible = true;
                }

                if (_isOI)
                {
                    btnSearch.Enabled = false;
                    btnShowMore.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 选择费用
        /// </summary>
        void SelectFees_Disposed(object sender, EventArgs e)
        {
            _prices = null;
            _selectedPrice = null;
            _unitList = null;
            gridControl1.DataSource = null;
            bsFreight.DataSource = null;
            bsFreight.PositionChanged -= bsFreight_PositionChanged;
            bsFreight.Dispose();
            if (Workitem != null)
            {
                if (feeDetailPart != null)
                {
                    Workitem.Items.Remove(feeDetailPart);
                    feeDetailPart.Dispose();
                    feeDetailPart = null;
                }
                Workitem.Items.Remove(this);
                Workitem = null;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        void bsFreight_PositionChanged(object sender, EventArgs e)
        {
            SetFeeDataSource();
        }

        #region 查询  by  No/date
        /// <summary>
        /// 查询
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Guid? fdID = Guid.Empty;
                if (chkIsFinalDestination.Checked)
                {
                    fdID = _finalDestination;
                }


                FreightDataList freightDataList = OceanExportService.GetFreight(txtFreight.Text.Trim(), _shipownerid,
                                                                    _placeOfReceiptID,
                                                                    _polid,
                                                                    _podid,
                                                                    fdID,
                                                                    string.Empty,
                                                                    new DateTime(dteStartDate.DateTime.Year, dteStartDate.DateTime.Month, dteStartDate.DateTime.Day, 0, 0, 0),
                                                                    new DateTime(dtpEndDate.DateTime.Year, dtpEndDate.DateTime.Month, dtpEndDate.DateTime.Day, 23, 59, 59), _freightID, _type);



                bsFreight.DataSource = freightDataList.DataList;
                bsFreight.ResetBindings(false);

                gridViewFee.BestFitColumns();
                TransSearchOceanRateList(freightDataList.UnitList);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        #endregion

        /// <summary>
        /// OK，确定选择
        /// </summary>
        void btnOK_Click(object sender, EventArgs e)
        {
            if (bsFreight.Current == null) return;

            FreightList current = bsFreight.Current as FreightList;

            //if (string.IsNullOrEmpty(current.Carrier)) return;
            _selectedPrice = current;
            var findForm = FindForm();
            if (findForm == null) return;
            findForm.DialogResult = DialogResult.OK;
            findForm.Close();
        }

        /// <summary>
        /// 双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gridViewFee_DoubleClick(object sender, EventArgs e)
        {
            if (bsFreight.Current == null) return;

            FreightList current = bsFreight.Current as FreightList;
            _selectedPrice = current;

            var findForm = FindForm();
            if (findForm == null) return;
            findForm.DialogResult = DialogResult.OK;
            findForm.Close();
        }

        /// <summary>
        /// 显示/隐藏 更多
        /// </summary>
        void btnShowMore_Click(object sender, EventArgs e)
        {
            _isMore = !_isMore;

            if (_isMore)
            {
                btnShowMore.Text = LocalData.IsEnglish ? "HideMore(&M)" : "隐藏更多(&M)";
                pnlTop.Visible = true;

                if (!isLoad)
                {
                    if (ArgumentHelper.GuidIsNullOrEmpty(_freightID))
                    {
                        dteStartDate.DateTime = DateTime.Now.AddDays(-15);
                        dteStartDate.EditValue = DateTime.Now.AddDays(-15);
                        dtpEndDate.DateTime = DateTime.Now.AddDays(15);
                        dtpEndDate.EditValue = DateTime.Now.AddDays(15);
                    }
                    else
                    {
                        dteStartDate.DateTime = _etd.Value.AddDays(-7);
                        dteStartDate.EditValue = _etd.Value.AddDays(-7);
                        dtpEndDate.DateTime = _etd.Value.AddDays(7);
                        dtpEndDate.EditValue = _etd.Value.AddDays(7);
                    }
                }
                isLoad = true;

            }
            else
            {
                btnShowMore.Text = LocalData.IsEnglish ? "ShowMore(&M)" : "显示更多(&M)";
                pnlTop.Visible = !pnlTop.Visible;
            }
            FreightDataList freightDataList = OceanExportService.GetFreight(_scno, _shipownerid, _placeOfReceiptID, _polid, _podid, _finalDestination, string.Empty,
                                              DateTime.SpecifyKind(dteStartDate.DateTime, DateTimeKind.Unspecified),
                                              DateTime.SpecifyKind(dtpEndDate.DateTime, DateTimeKind.Unspecified), _freightID, _type);


            bsFreight.DataSource = freightDataList.DataList;
            gridViewFee.BestFitColumns();
            TransSearchOceanRateList(freightDataList.UnitList);
        }
        /// <summary>
        /// 取消
        /// </summary>
        void btnCancle_Click(object sender, EventArgs e)
        {
            var findForm = FindForm();
            if (findForm == null) return;
            findForm.DialogResult = DialogResult.Cancel;
            findForm.Close();
        }

        /// <summary>
        /// 网格样式设置
        /// </summary>
        void gridViewFee_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0 || _freightID == null) return;
            FreightList list = gridViewFee.GetRow(e.RowHandle) as FreightList;
            if (list == null) return;

            if (_freightID != null && list.ID == _freightID.Value)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Error);
            }

        }

        /// <summary>
        /// 行号绘制
        /// </summary>
        private void gridViewFee_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        } 
        #endregion

        #region Method
        /// <summary>
        /// 设置数据源
        /// </summary>
        /// <param name="freightParameter"></param>
        public void SetDataSource(
         FreightParameter freightParameter)
        {
            _prices = freightParameter.prices;
            _unitList = freightParameter.unitList;
            txtFreight.Text = _scno = freightParameter.scno;
            _shipownerid = freightParameter.shipownerid;
            _placeOfReceiptID = freightParameter.placeOfReceiptID;
            _polid = freightParameter.polid;
            _podid = freightParameter.podid;
            _finalDestination = freightParameter.finalDestinationID;
            _goodsdes = freightParameter.goodsdes;
            _freightID = freightParameter.freightID;
            _type = freightParameter.type;
            if (freightParameter.etd != null && freightParameter.etd != DateTime.MinValue)
            {
                _etd = freightParameter.etd;
            }

        }

        /// <summary>
        /// 设置OI
        /// </summary>
        /// <param name="isOI"></param>
        public void setOI(bool isOI)
        {
            _isOI = isOI;
        }

        /// <summary>
        /// 选择价格
        /// </summary>
        public FreightList SelectedPrice
        {
            get { return _selectedPrice; }
            set { _selectedPrice = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                btnCancle.PerformClick();
            }

            return base.ProcessDialogKey(keyData);
        }
        void InitControls()
        {
            feeDetailPart = Workitem.Items.AddNew<OceanRateFeeDetailPart>();
            pnlFee.Controls.Clear();
            feeDetailPart.Dock = DockStyle.Fill;
            pnlFee.Controls.Add(feeDetailPart);

            pnlFee.Text = LocalData.IsEnglish ? "Fees" : "费用明细";
            bsFreight.DataSource = _prices;
            TransSearchOceanRateList(_unitList);
            bsFreight.ResetBindings(false);
            if (LocalData.IsEnglish)
            {
                lblFreight.Text = _type == SelectType.Contract ? "Contract NO" : "Inquire Price NO";
            }
            else
            {
                lblFreight.Text = _type == SelectType.Contract ? "合约号" : "询价号";
            }
            //SetFeeDataSource();
        }

        /// <summary>
        /// 
        /// </summary>
        void SetFeeDataSource()
        {
            FreightList item = bsFreight.Current as FreightList;
            if (item != null && feeDetailPart != null)
            {
                feeDetailPart.DataSource = item.ID;
            }
        }

        /// <summary>
        /// 初始化列表数据
        /// </summary>
        void InitListDate()
        {
            gridViewFee.BestFitColumns();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitsNameList"></param>
        private void TransSearchOceanRateList(IEnumerable<string> unitsNameList)
        {
            colFreightNo.VisibleIndex = 0;
            colBeginDate.VisibleIndex = 1;
            colEndDate.VisibleIndex = 2;
            colPol.VisibleIndex = 3;
            colPod.VisibleIndex = 4;
            colDelivery.VisibleIndex = 5;
            colDestination.VisibleIndex = 6;
            colTerm.VisibleIndex = 7;

            const int visibleIndex = 7;

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

            if (unitsNameList != null)
            {
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
            }






            colGoods.VisibleIndex = visibleIndex + 27;
            colAccount.VisibleIndex = visibleIndex + 28;
            colCarrier.VisibleIndex = visibleIndex + 29;
            colShipper.VisibleIndex = visibleIndex + 30;
            colConsignee.VisibleIndex = visibleIndex + 31;
            colNotifyPart.VisibleIndex = visibleIndex + 32;
            colAdditionalCharges.VisibleIndex = visibleIndex + 33;
            colPaymentTreaty.VisibleIndex = visibleIndex + 34;
            colRemark.VisibleIndex = visibleIndex + 35;
            colRemarkDetail.VisibleIndex = visibleIndex + 36;
            colTransitTime.VisibleIndex = visibleIndex + 37;

            colRemark.Width = 150;
            colGoods.Width = 250;


        } 
        #endregion
    }

    #region 选择运价/询价面板参数
    /// <summary>
    /// 选择运价/询价面板参数
    /// </summary>
    [Serializable]
    public class FreightParameter
    {
        /// <summary>
        /// 
        /// </summary>
        public List<FreightList> prices { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> unitList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string scno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid? shipownerid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid? placeOfReceiptID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid polid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid? podid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid? finalDestinationID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goodsdes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid? freightID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? etd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SelectType type { get; set; }
    } 
    #endregion
}
