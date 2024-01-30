using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraEditors;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface.DataObjects;

namespace ICP.FRM.UI.OceanPrice
{
    [ToolboxItem(false)]
    public partial class OceanRateExportToExcel : BaseListPart
    {
        public OceanRateExportToExcel()
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

        #region 数据源

        public void BindData(List<OceanRateDataObject> list, List<string> unitsNameList)
        {
            if (list == null)
            {
                list = new List<OceanRateDataObject>();
            }


            BulidGridViewColumnsByOceanUnits(unitsNameList);

            bsList.DataSource = list;
            bsList.ResetBindings(false);

        }

        private void BulidGridViewColumnsByOceanUnits(List<string> unitsNameList)
        {
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

            int visibleIndex = 11;

            foreach (var item in unitsNameList)
            {
                #region  SetVisible= true;

                switch (item)
                {
                    case "20GP":
                        colRate_20GP.Visible = true;
                        colRate_20GP.Width = 100;
                        colRate_20GP.VisibleIndex = visibleIndex + 1;
                        break;
                    case "40GP":
                        colRate_40GP.Visible = true;
                        colRate_40GP.Width = 100;
                        colRate_40GP.VisibleIndex = visibleIndex + 2;
                        break;
                    case "40HQ":
                        colRate_40HQ.Visible = true;
                        colRate_40HQ.Width = 100;
                        colRate_40HQ.VisibleIndex = visibleIndex + 3;
                        break;
                    case "45HQ":
                        colRate_45HQ.Visible = true;
                        colRate_45HQ.Width = 100;
                        colRate_45HQ.VisibleIndex = visibleIndex + 4;
                        break;
                    case "20NOR":
                        colRate_20NOR.Visible = true;
                        colRate_20NOR.Width = 100;
                        colRate_20NOR.VisibleIndex = visibleIndex + 5;
                        break;
                    case "40NOR":
                        colRate_40NOR.Visible = true;
                        colRate_40NOR.Width = 100;
                        colRate_40NOR.VisibleIndex = visibleIndex + 6;
                        break;

                    case "20FR":
                        colRate_20FR.Visible = true;
                        colRate_20FR.Width = 100;
                        colRate_20FR.VisibleIndex = visibleIndex + 7;
                        break;
                    case "20RH":
                        colRate_20RH.Visible = true;
                        colRate_20RH.Width = 100;
                        colRate_20RH.VisibleIndex = visibleIndex + 8; 
                        break;
                    case "20RF":
                        colRate_20RF.Visible = true;
                        colRate_20RF.Width = 100;
                        colRate_20RF.VisibleIndex = visibleIndex + 9;
                        break;
                    case "20HQ":
                        colRate_20HQ.Visible = true;
                        colRate_20HQ.Width = 100;
                        colRate_20HQ.VisibleIndex = visibleIndex + 19;
                        break;
                    case "20TK":
                        colRate_20TK.Visible = true;
                        colRate_20TK.Width = 100;
                        colRate_20TK.VisibleIndex = visibleIndex + 10;
                        break;
                    case "20OT":
                        colRate_20OT.Visible = true;
                        colRate_20OT.Width = 100;
                        colRate_20OT.VisibleIndex = visibleIndex + 11;
                        break;
                    case "20HT":
                        colRate_20HT.Visible = true;
                        colRate_20HT.Width = 100;
                        colRate_20HT.VisibleIndex = visibleIndex + 12;
                        break;

                    case "40TK":
                        colRate_40TK.Visible = true;
                        colRate_40TK.Width = 100;
                        colRate_40TK.VisibleIndex = visibleIndex + 13;
                        break;
                    case "40OT":
                        colRate_40OT.Visible = true;
                        colRate_40OT.Width = 100;
                        colRate_40OT.VisibleIndex = visibleIndex + 14;
                        break;
                    case "40FR":
                        colRate_40FR.Visible = true;
                        colRate_40FR.Width = 100;
                        colRate_40FR.VisibleIndex = visibleIndex + 15;
                        break;
                    case "40HT":
                        colRate_40HT.Visible = true;
                        colRate_40HT.Width = 100;
                        colRate_40HT.VisibleIndex = visibleIndex + 16;
                        break;
                    case "40RH":
                        colRate_40RH.Visible = true;
                        colRate_40RH.Width = 100;
                        colRate_40RH.VisibleIndex = visibleIndex + 17;
                        break;
                    case "40RF":
                        colRate_40RF.Visible = true;
                        colRate_40RF.Width = 100;
                        colRate_40RF.VisibleIndex = visibleIndex + 18;
                        break;

                    case "45GP":
                        colRate_45GP.Visible = true;
                        colRate_45GP.Width = 100;
                        colRate_45GP.VisibleIndex = visibleIndex + 19;
                        break;
                    case "45RF":
                        colRate_45RF.Visible = true;
                        colRate_45RF.Width = 100;
                        colRate_45RF.VisibleIndex = visibleIndex + 20;
                        break;
                    case "45HT":
                        colRate_45HT.Visible = true;
                        colRate_45HT.Width = 100;
                        colRate_45HT.VisibleIndex = visibleIndex + 21;
                        break;
                    case "45FR":
                        colRate_45FR.Visible = true;
                        colRate_45FR.Width = 100;
                        colRate_45FR.VisibleIndex = visibleIndex + 22;
                        break;
                    case "45OT":
                        colRate_45OT.Visible = true;
                        colRate_45OT.Width = 100;
                        colRate_45OT.VisibleIndex = visibleIndex + 23;
                        break;
                    case "45TK":
                        colRate_45TK.Visible = true;
                        colRate_45TK.Width = 100;
                        colRate_45TK.VisibleIndex = visibleIndex + 24;
                        break;
                    case "45RH":
                        colRate_45RH.Visible = true;
                        colRate_45RH.Width = 100;
                        colRate_45RH.VisibleIndex = visibleIndex + 25;
                        break;

                    case "53HQ":
                        colRate_53HQ.Visible = true;
                        colRate_53HQ.Width = 100;
                        colRate_53HQ.VisibleIndex = visibleIndex + 26;
                        break;
                }

                #endregion
            }


            colCarrier.Visible = true;
            colPOL.Visible = true;
            colVIA.Visible = true;
            colPOD.Visible = true;
            colDelivery.Visible = true;
            colItemCode.Visible = true;
            colCommodity.Visible = true;
            colTerm.Visible = true;
          
            colCLS.Visible = true;
            colDurationFrom.Visible = true;
            colDurationTo.Visible = true;
            colDescription.Visible = true;


            colCarrier.VisibleIndex = 0;
            colPOL.VisibleIndex = 1;
            colVIA.VisibleIndex = 2;
            colPOD.VisibleIndex = 3;
            colDelivery.VisibleIndex = 4;
            colItemCode.VisibleIndex = 5;
            colCommodity.VisibleIndex = 6;
            colTerm.VisibleIndex = 7;
            colSurCharge.VisibleIndex = 8;
            colCLS.VisibleIndex = 9;
 

            colDurationFrom.VisibleIndex = 27;
            colDurationTo.VisibleIndex = visibleIndex+28;
            colSurCharge.VisibleIndex = visibleIndex + 29;
            colDescription.VisibleIndex = visibleIndex+30;

            colCarrier.Width = 200;
            colPOL.Width = 200;
            colVIA.Width = 200;
            colPOD.Width = 200;
            colDelivery.Width = 200;
            colItemCode.Width = 200;
            colCommodity.Width = 200;
            colTerm.Width =200;
            colSurCharge.Width =200;
            colCLS.Width = 200;
            colDurationFrom.Width = 220;
            colDurationTo.Width = 220;
            colDescription.Width = 200;
        }

        #endregion

        #region 导出到Excel
        public void ExportToExcel(string fileName)
        {

            try
            {
                gvMain.ExportToXls(fileName);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
        #endregion


    }
}
