using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface.DataObjects;

using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FRM.UI.OceanPrice
{
    [ToolboxItem(false)]
    public partial class OPAssociatedRatesPart : BasePart
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public OceanPriceUIDataHelper OceanPriceUIDataHelper
        {
            get
            {
                return ClientHelper.Get<OceanPriceUIDataHelper, OceanPriceUIDataHelper>();
            }
        }

        #endregion

        public OPAssociatedRatesPart()
        {
            InitializeComponent();
            Disposed += delegate {
                gcAddFee.DataSource = null;
                gcMain.DataSource = null;
                bsList.DataSource = null;
                bsList.Dispose();
                bsList2.DataSource = null;
                bsList2.Dispose();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitComboboxSource();
        }

        void InitComboboxSource()
        {
            #region 运输条款
            foreach (var item in OceanPriceUIDataHelper.TransportClauses)
            {
                rcmbTransportClause.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
            }
            #endregion

            #region GeographyType
            List<EnumHelper.ListItem<GeographyType>> accountTypes = EnumHelper.GetEnumValues<GeographyType>(LocalData.IsEnglish);
            foreach (var item in accountTypes)
            {
                if (item.Value == GeographyType.None) continue;

                cmbGeographyType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));

            }
            #endregion
        }

        #region interface

        List<ClientArbitraryList> _Source = null;


        public void SetArbitrarySource(List<ArbitraryList> list, OceanList oceanList)
        {
            BulidArbitraryColumns(oceanList.OceanUnits);
            _Source = OceanPriceTransformHelper.TransformOceanItemsToClientObjects(list, oceanList.OceanUnits);

            List<ClientArbitraryList> originals = _Source;

            bsList.DataSource = originals;
            bsList.ResetBindings(false);
        }

        public void SetAdditionalFee(List<AdditionalFeeList> list, OceanList oceanList)
        {
            BulidAdditionalFeeColumns(oceanList.OceanUnits);
            List<ClientAdditionalFeeList> _AddSource = OceanPriceTransformHelper.TransformS2C(list, oceanList.OceanUnits);

            bsList2.DataSource = _AddSource;
            bsList2.ResetBindings(false);
        }


        #region BulidColumns
        /// <summary>
        /// 绑定Arbitrary列
        /// </summary>
        /// <param name="units"></param>
        private void BulidArbitraryColumns(List<OceanUnitList> units)
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
            #endregion

            int visibleIndex = 6;

            foreach (var item in units)
            {
                #region  SetVisible= true;
                switch (item.UnitName)
                {
                    case "20GP": colRate_20GP.VisibleIndex = visibleIndex; break;
                    case "40GP": colRate_40GP.VisibleIndex = visibleIndex; break;
                    case "40HQ": colRate_40HQ.VisibleIndex = visibleIndex; break;
                    case "45HQ": colRate_45HQ.VisibleIndex = visibleIndex; break;
                    case "20NOR": colRate_20NOR.VisibleIndex = visibleIndex; break;
                    case "40NOR": colRate_40NOR.VisibleIndex = visibleIndex; break;

                    case "20FR": colRate_20FR.VisibleIndex = visibleIndex; break;
                    case "20RH": colRate_20RH.VisibleIndex = visibleIndex; break;
                    case "20RF": colRate_20RF.VisibleIndex = visibleIndex; break;
                    case "20HQ": colRate_20HQ.VisibleIndex = visibleIndex; break;
                    case "20TK": colRate_20TK.VisibleIndex = visibleIndex; break;
                    case "20OT": colRate_20OT.VisibleIndex = visibleIndex; break;
                    case "20HT": colRate_20HT.VisibleIndex = visibleIndex; break;

                    case "40TK": colRate_40TK.VisibleIndex = visibleIndex; break;
                    case "40OT": colRate_40OT.VisibleIndex = visibleIndex; break;
                    case "40FR": colRate_40FR.VisibleIndex = visibleIndex; break;
                    case "40HT": colRate_40HT.VisibleIndex = visibleIndex; break;
                    case "40RH": colRate_40RH.VisibleIndex = visibleIndex; break;
                    case "40RF": colRate_40RF.VisibleIndex = visibleIndex; break;

                    case "45GP": colRate_45GP.VisibleIndex = visibleIndex; break;
                    case "45RF": colRate_45RF.VisibleIndex = visibleIndex; break;
                    case "45HT": colRate_45HT.VisibleIndex = visibleIndex; break;
                    case "45FR": colRate_45FR.VisibleIndex = visibleIndex; break;
                    case "45OT": colRate_45OT.VisibleIndex = visibleIndex; break;
                    case "45TK": colRate_45TK.VisibleIndex = visibleIndex; break;
                    case "45RH": colRate_45RH.VisibleIndex = visibleIndex; break;
                }
                visibleIndex++;
                #endregion
            }

            colArbitraryNo.VisibleIndex = 0;
            colItemCode.VisibleIndex = 1;
            colGeographyType .VisibleIndex = 2;
            colPOL.VisibleIndex = 3; 
            colPOD .VisibleIndex = 4;
            colTransportClauseID.VisibleIndex = 5;
        }

        /// <summary>
        /// 绑定AdditionalFee列
        /// </summary>
        /// <param name="units"></param>
        private void BulidAdditionalFeeColumns(List<OceanUnitList> units)
        {
            #region  SetVisible= false;
            col_Rate_45FR.Visible = false;
            col_Rate_40RF.Visible = false;
            col_Rate_45HT.Visible = false;
            col_Rate_20RF.Visible = false;
            col_Rate_20HQ.Visible = false;
            col_Rate_20TK.Visible = false;
            col_Rate_20GP.Visible = false;
            col_Rate_40TK.Visible = false;
            col_Rate_40OT.Visible = false;
            col_Rate_20FR.Visible = false;
            col_Rate_45GP.Visible = false;
            col_Rate_40GP.Visible = false;
            col_Rate_45RF.Visible = false;
            col_Rate_20RH.Visible = false;
            col_Rate_45OT.Visible = false;
            col_Rate_40NOR.Visible = false;
            col_Rate_40FR.Visible = false;
            col_Rate_20OT.Visible = false;
            col_Rate_45TK.Visible = false;
            col_Rate_20NOR.Visible = false;
            col_Rate_40HT.Visible = false;
            col_Rate_40RH.Visible = false;
            col_Rate_45RH.Visible = false;
            col_Rate_45HQ.Visible = false;
            col_Rate_20HT.Visible = false;
            col_Rate_40HQ.Visible = false;
            #endregion

            int visibleIndex = 6;

            foreach (var item in units)
            {
                #region  SetVisible= true;
                switch (item.UnitName)
                {
                    case "20GP": col_Rate_20GP.VisibleIndex = visibleIndex; break;
                    case "40GP": col_Rate_40GP.VisibleIndex = visibleIndex; break;
                    case "40HQ": col_Rate_40HQ.VisibleIndex = visibleIndex; break;
                    case "45HQ": col_Rate_45HQ.VisibleIndex = visibleIndex; break;
                    case "20NOR": col_Rate_20NOR.VisibleIndex = visibleIndex; break;
                    case "40NOR": col_Rate_40NOR.VisibleIndex = visibleIndex; break;

                    case "20FR": col_Rate_20FR.VisibleIndex = visibleIndex; break;
                    case "20RH": col_Rate_20RH.VisibleIndex = visibleIndex; break;
                    case "20RF": col_Rate_20RF.VisibleIndex = visibleIndex; break;
                    case "20HQ": col_Rate_20HQ.VisibleIndex = visibleIndex; break;
                    case "20TK": col_Rate_20TK.VisibleIndex = visibleIndex; break;
                    case "20OT": col_Rate_20OT.VisibleIndex = visibleIndex; break;
                    case "20HT": col_Rate_20HT.VisibleIndex = visibleIndex; break;

                    case "40TK": col_Rate_40TK.VisibleIndex = visibleIndex; break;
                    case "40OT": col_Rate_40OT.VisibleIndex = visibleIndex; break;
                    case "40FR": col_Rate_40FR.VisibleIndex = visibleIndex; break;
                    case "40HT": col_Rate_40HT.VisibleIndex = visibleIndex; break;
                    case "40RH": col_Rate_40RH.VisibleIndex = visibleIndex; break;
                    case "40RF": col_Rate_40RF.VisibleIndex = visibleIndex; break;

                    case "45GP": col_Rate_45GP.VisibleIndex = visibleIndex; break;
                    case "45RF": col_Rate_45RF.VisibleIndex = visibleIndex; break;
                    case "45HT": col_Rate_45HT.VisibleIndex = visibleIndex; break;
                    case "45FR": col_Rate_45FR.VisibleIndex = visibleIndex; break;
                    case "45OT": col_Rate_45OT.VisibleIndex = visibleIndex; break;
                    case "45TK": col_Rate_45TK.VisibleIndex = visibleIndex; break;
                    case "45RH": col_Rate_45RH.VisibleIndex = visibleIndex; break;
                }
                visibleIndex++;
                #endregion

                colChargingCode.VisibleIndex = 0;
                colChargingCodeDescription.VisibleIndex = 1;
                colCurrencyID.VisibleIndex = 2;
                colPercent.VisibleIndex = 3;

            }

        }

        #endregion

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ClientArbitraryList> originals = new List<ClientArbitraryList>();
            if (radioGroup1.SelectedIndex == 0)
            {
                originals = _Source;
            }
            else if (radioGroup1.SelectedIndex == 1)
            {
                originals = _Source.FindAll(a => a.AssociatedType == GeographyType.Original);
            }
            else
            {
                originals = _Source.FindAll(a => a.AssociatedType == GeographyType.Destination);
            }

            bsList.DataSource = originals;
            bsList.ResetBindings(false);
        }

        #endregion
    }
}
