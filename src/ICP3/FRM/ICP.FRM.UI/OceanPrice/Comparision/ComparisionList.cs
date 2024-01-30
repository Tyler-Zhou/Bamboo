using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ICP.FRM.UI
{
    /// <summary>
    /// 比较合约
    /// </summary>
    [ToolboxItem(false)]
    public partial class ComparisionList : BasePart
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IOceanPriceService OceanPriceService
        {
            get
            {
                return ServiceClient.GetService<IOceanPriceService>();
            }
        }
        #endregion

        #region 私有变量

        List<string> C1ColumnNameList = new List<string>();

        List<string> C2ColumnNameList = new List<string>();

        List<string> GapColumnNameList = new List<string>();
        #endregion

        #region 属性
        /// <summary>
        /// 合约1 ID
        /// </summary>
        public Guid Contract1ID
        {
            get;
            set;
        }
        /// <summary>
        /// 合约2 ID
        /// </summary>
        public Guid Contract2ID
        {
            get;
            set;
        }
        /// <summary>
        /// 合约1 标题
        /// </summary>
        public string Contract1Title
        {
            get;
            set;
        }
        /// <summary>
        /// 合约2 标题
        /// </summary>
        public string Contract2Title
        {
            get;
            set;
        }
        /// <summary>
        /// 对比数据列表
        /// </summary>
        public List<OceanContractCompar> BaseDataList = new List<OceanContractCompar>();
        /// <summary>
        /// 箱型列表
        /// </summary>
        public List<string> UnitList = new List<string>();

        #endregion

        #region 初始化
        /// <summary>
        /// 比较合约
        /// </summary>
        public ComparisionList()
        {
            InitializeComponent();
            Disposed += delegate
            {
                BaseDataList = null;
                UnitList = null;
                gcMain.DataSource = null;
                bsList.DataSource = null;
                bsList.Dispose();
                C1ColumnNameList = null;
                C2ColumnNameList = null;
                GapColumnNameList = null;
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

            if (!DesignMode)
            {
                Utility.ShowGridRowNo(gvMain);
                InitColumns();
                BindingData();

                Width = Screen.PrimaryScreen.Bounds.Width - 10;

            }

        }
        /// <summary>
        /// 初始化列
        /// </summary>
        private void InitColumns()
        {

            colContract1.Caption = Contract1Title;
            colContract2.Caption = Contract2Title;



            #region 添加列名
            C1ColumnNameList.Add("C1_20HQ");
            C1ColumnNameList.Add("C1_20HT");
            C1ColumnNameList.Add("C1_20OT");
            C1ColumnNameList.Add("C1_20FR");
            C1ColumnNameList.Add("C1_20RF");
            C1ColumnNameList.Add("C1_20RH");
            C1ColumnNameList.Add("C1_20TK");
            C1ColumnNameList.Add("C1_40FR");
            C1ColumnNameList.Add("C1_40HT");
            C1ColumnNameList.Add("C1_40OT");
            C1ColumnNameList.Add("C1_40RF");
            C1ColumnNameList.Add("C1_40RH");
            C1ColumnNameList.Add("C1_40TK");
            C1ColumnNameList.Add("C1_45FR");
            C1ColumnNameList.Add("C1_45GP");
            C1ColumnNameList.Add("C1_45HT");
            C1ColumnNameList.Add("C1_45RF");
            C1ColumnNameList.Add("C1_45RH");
            C1ColumnNameList.Add("C1_45TK");
            C1ColumnNameList.Add("C1_45OT");
            C1ColumnNameList.Add("C1_40NOR");
            C1ColumnNameList.Add("C1_20GP");
            C1ColumnNameList.Add("C1_40HQ");
            C1ColumnNameList.Add("C1_45HQ");
            C1ColumnNameList.Add("C1_20NOR");
            C1ColumnNameList.Add("C1_40GP");
            C1ColumnNameList.Add("C1_53HQ");

            C2ColumnNameList.Add("C2_20HQ");
            C2ColumnNameList.Add("C2_20HT");
            C2ColumnNameList.Add("C2_20OT");
            C2ColumnNameList.Add("C2_20FR");
            C2ColumnNameList.Add("C2_20RF");
            C2ColumnNameList.Add("C2_20RH");
            C2ColumnNameList.Add("C2_20TK");
            C2ColumnNameList.Add("C2_40FR");
            C2ColumnNameList.Add("C2_40HT");
            C2ColumnNameList.Add("C2_40OT");
            C2ColumnNameList.Add("C2_40RF");
            C2ColumnNameList.Add("C2_40RH");
            C2ColumnNameList.Add("C2_40TK");
            C2ColumnNameList.Add("C2_45FR");
            C2ColumnNameList.Add("C2_45GP");
            C2ColumnNameList.Add("C2_45HT");
            C2ColumnNameList.Add("C2_45RF");
            C2ColumnNameList.Add("C2_45RH");
            C2ColumnNameList.Add("C2_45TK");
            C2ColumnNameList.Add("C2_45OT");
            C2ColumnNameList.Add("C2_40NOR");
            C2ColumnNameList.Add("C2_20GP");
            C2ColumnNameList.Add("C2_40HQ");
            C2ColumnNameList.Add("C2_45HQ");
            C2ColumnNameList.Add("C2_20NOR");
            C2ColumnNameList.Add("C2_40GP");
            C2ColumnNameList.Add("C2_53HQ");

            GapColumnNameList.Add("Gap_20HQ");
            GapColumnNameList.Add("Gap_20HT");
            GapColumnNameList.Add("Gap_20OT");
            GapColumnNameList.Add("Gap_20FR");
            GapColumnNameList.Add("Gap_20RF");
            GapColumnNameList.Add("Gap_20RH");
            GapColumnNameList.Add("Gap_20TK");
            GapColumnNameList.Add("Gap_40FR");
            GapColumnNameList.Add("Gap_40HT");
            GapColumnNameList.Add("Gap_40OT");
            GapColumnNameList.Add("Gap_40RF");
            GapColumnNameList.Add("Gap_40RH");
            GapColumnNameList.Add("Gap_40TK");
            GapColumnNameList.Add("Gap_45FR");
            GapColumnNameList.Add("Gap_45GP");
            GapColumnNameList.Add("Gap_45HT");
            GapColumnNameList.Add("Gap_45RF");
            GapColumnNameList.Add("Gap_45RH");
            GapColumnNameList.Add("Gap_45TK");
            GapColumnNameList.Add("Gap_45OT");
            GapColumnNameList.Add("Gap_40NOR");
            GapColumnNameList.Add("Gap_20GP");
            GapColumnNameList.Add("Gap_40HQ");
            GapColumnNameList.Add("Gap_45HQ");
            GapColumnNameList.Add("Gap_20NOR");
            GapColumnNameList.Add("Gap_40GP");
            GapColumnNameList.Add("Gap_53HQ");

            #endregion
        }
        #endregion

        #region 绑定列表数据
        /// <summary>
        /// 绑定列表数据
        /// </summary>
        private void BindingData()
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading......");

            OceanContractComparDataList data = OceanPriceService.GetComparList(Contract1ID, Contract2ID);
            if (data == null )
           {
               return;    
           }
            UnitList = data.UnitList;
            BaseDataList = data.DataList;
            SetDataSource(data.DataList);

            LoadingServce.CloseLoadingForm(theradID);

        }
        /// <summary>
        /// 显示转换列
        /// </summary>
        /// <param name="list"></param>
        public void TransSearchOceanRateList(List<OceanContractCompar> list)
        {
            #region  SetVisible= false;
            colC1_45FR.Visible = false;
            colC1_40RF.Visible = false;
            colC1_45HT.Visible = false;
            colC1_20RF.Visible = false;
            colC1_20HQ.Visible = false;
            colC1_20TK.Visible = false;
            colC1_20GP.Visible = false;
            colC1_40TK.Visible = false;
            colC1_40OT.Visible = false;
            colC1_20FR.Visible = false;
            colC1_45GP.Visible = false;
            colC1_40GP.Visible = false;
            colC1_45RF.Visible = false;
            colC1_20RH.Visible = false;
            colC1_45OT.Visible = false;
            colC1_40NOR.Visible = false;
            colC1_40FR.Visible = false;
            colC1_20OT.Visible = false;
            colC1_45TK.Visible = false;
            colC1_20NOR.Visible = false;
            colC1_40HT.Visible = false;
            colC1_40RH.Visible = false;
            colC1_45RH.Visible = false;
            colC1_45HQ.Visible = false;
            colC1_20HT.Visible = false;
            colC1_40HQ.Visible = false;
            colC1_53HQ.Visible = false;

            colC2_45FR.Visible = false;
            colC2_40RF.Visible = false;
            colC2_45HT.Visible = false;
            colC2_20RF.Visible = false;
            colC2_20HQ.Visible = false;
            colC2_20TK.Visible = false;
            colC2_20GP.Visible = false;
            colC2_40TK.Visible = false;
            colC2_40OT.Visible = false;
            colC2_20FR.Visible = false;
            colC2_45GP.Visible = false;
            colC2_40GP.Visible = false;
            colC2_45RF.Visible = false;
            colC2_20RH.Visible = false;
            colC2_45OT.Visible = false;
            colC2_40NOR.Visible = false;
            colC2_40FR.Visible = false;
            colC2_20OT.Visible = false;
            colC2_45TK.Visible = false;
            colC2_20NOR.Visible = false;
            colC2_40HT.Visible = false;
            colC2_40RH.Visible = false;
            colC2_45RH.Visible = false;
            colC2_45HQ.Visible = false;
            colC2_20HT.Visible = false;
            colC2_40HQ.Visible = false;
            colC2_53HQ.Visible = false;

            colGap_45FR.Visible = false;
            colGap_40RF.Visible = false;
            colGap_45HT.Visible = false;
            colGap_20RF.Visible = false;
            colGap_20HQ.Visible = false;
            colGap_20TK.Visible = false;
            colGap_20GP.Visible = false;
            colGap_40TK.Visible = false;
            colGap_40OT.Visible = false;
            colGap_20FR.Visible = false;
            colGap_45GP.Visible = false;
            colGap_40GP.Visible = false;
            colGap_45RF.Visible = false;
            colGap_20RH.Visible = false;
            colGap_45OT.Visible = false;
            colGap_40NOR.Visible = false;
            colGap_40FR.Visible = false;
            colGap_20OT.Visible = false;
            colGap_45TK.Visible = false;
            colGap_20NOR.Visible = false;
            colGap_40HT.Visible = false;
            colGap_40RH.Visible = false;
            colGap_45RH.Visible = false;
            colGap_45HQ.Visible = false;
            colGap_20HT.Visible = false;
            colGap_40HQ.Visible = false;
            colGap_53HQ.Visible = false;
            #endregion

            #region Set Columns Visible
            int C1VisibleIndex = 6;
            int C2VisibleIndex=32;
            int GapVisibleIndex = 58;

            foreach (string item in UnitList)
            {
                switch (item)
                {
                    #region 20GP
                    case "20GP":
                            colC1_20GP.Visible = true;
                            colC1_20GP.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;                     
                            colC2_20GP.Visible = true;
                            colC2_20GP.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                            colGap_20GP.Visible = true;
                            colGap_20GP.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                        break;
                    #endregion

                    #region 40GP
                    case "40GP":
                            colC1_40GP.Visible = true; ;
                            colC1_40GP.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;
                            colC2_40GP.Visible = true;
                            colC2_40GP.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                            colGap_40GP.Visible = true;
                            colGap_40GP.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                        break;
                    #endregion

                    #region 40HQ
                    case "40HQ":
                            colC1_40HQ.Visible = true;
                            colC1_40HQ.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;
                            colC2_40HQ.Visible = true;
                            colC2_40HQ.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                            colGap_40HQ.Visible = true;
                            colGap_40HQ.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                        break;
                    #endregion

                    #region 45HQ
                    case "45HQ":
                            colC1_45HQ.Visible = true;
                            colC1_45HQ.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;
                            colC2_45HQ.Visible = true;
                            colC2_45HQ.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                            colGap_45HQ.Visible = true;
                            colGap_45HQ.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                        break;
                    #endregion

                    #region 20NOR
                    case "20NOR":
                            colC1_20NOR.Visible = true;
                            colC1_20NOR.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;
                            colC2_20NOR.Visible = true;
                            colC2_20NOR.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                            colGap_20NOR.Visible = true;
                            colGap_20NOR.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                        break;
                    #endregion

                    #region 40NOR
                    case "40NOR":
                            colC1_40NOR.Visible = true;
                            colC1_40NOR.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;
                            colC2_40NOR.Visible = true;
                            colC2_40NOR.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                            colGap_40NOR.Visible = true;
                            colGap_40NOR.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                        break;
                    #endregion

                    #region 20FR
                    case "20FR":
                            colC2_20FR.Visible = true;
                            colC2_20FR.VisibleIndex = C2VisibleIndex;
                            C1VisibleIndex++;
                            colGap_20FR.Visible = true;
                            colGap_20FR.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                            colC1_20FR.Visible = true;
                            colC1_20FR.VisibleIndex = C1VisibleIndex;
                            C2VisibleIndex++;
                        break;
                    #endregion

                    #region 20RH
                    case "20RH":
                            colC1_20RH.Visible = true;
                            colC1_20RH.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;
                            colC2_20RH.Visible = true;
                            colC2_20RH.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                            colGap_20RH.Visible = true;
                            colGap_20RH.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                        break;
                    #endregion

                    #region 20RF
                    case "20RF":
                            colC1_20RF.Visible = true;
                            colC1_20RF.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;
                            colC2_20RF.Visible = true;
                            colC2_20RF.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                            colGap_20RF.Visible = true;
                            colGap_20RF.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                        break;
                    #endregion

                    #region 20HQ
                    case "20HQ":
                            colC1_20HQ.Visible = true;
                            colC1_20HQ.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;
                            colC2_20HQ.Visible = true;
                            colC2_20HQ.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                            colGap_20HQ.Visible = true;
                            colGap_20HQ.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                        break;
                    #endregion

                    #region 20TK
                    case "20TK":
                            colC1_20TK.Visible = true;
                            colC1_20TK.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;
                            colC2_20TK.Visible = true;
                            colC2_20TK.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                            colGap_20TK.Visible = true;
                            colGap_20TK.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                        break;
                    #endregion

                    #region 20OT
                    case "20OT":
                            colC1_20OT.Visible = true;
                            colC1_20OT.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;
                            colC2_20OT.Visible = true;
                            colC2_20OT.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                            colGap_20OT.Visible = true;
                            colGap_20OT.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                        break;
                    #endregion

                    #region 20HT
                    case "20HT":
                            colC1_20HT.Visible = true;
                            colC1_20HT.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;
                            colC2_20HT.Visible = true;
                            colC2_20HT.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                            colGap_20HT.Visible = true;
                            colGap_20HT.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;

                        break;
                    #endregion

                    #region 40TK
                    case "40TK":
                            colC1_40TK.Visible = true;
                            colC1_40TK.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;
                            colC2_40TK.Visible = true;
                            colC2_40TK.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                            colGap_40TK.Visible = true;
                            colGap_40TK.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                        break;
                    #endregion

                    #region 40OT
                    case "40OT":
                            colC1_40OT.Visible = true;
                            colC1_40OT.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;
                            colC2_40OT.Visible = true;
                            colC2_40OT.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                            colGap_40OT.Visible = true;
                            colGap_40OT.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                        break;
                    #endregion

                    #region 40FR
                    case "40FR":
                            colC1_40FR.Visible = true;
                            colC1_40FR.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;
                            colC2_40FR.Visible = true;
                            colC2_40FR.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                            colGap_40FR.Visible = true;
                            colGap_40FR.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                        break;
                    #endregion

                    #region 40HT
                    case "40HT":
                            colGap_40HT.Visible = true;
                            colGap_40HT.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                            colC1_40HT.Visible = true;
                            colC1_40HT.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;
                            colC2_40HT.Visible = true;
                            colC2_40HT.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                        break;
                    #endregion

                    #region 40RH
                    case "40RH":
                            colGap_40RH.Visible = true;
                            colGap_40RH.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                            colC1_40RH.Visible = true;
                            colC1_40RH.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;
                            colC2_40RH.Visible = true;
                            colC2_40RH.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                        break;
                    #endregion

                    #region 40RF
                    case "40RF":
                            colGap_40RF.Visible = true;
                            colGap_40RF.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                            colC1_40RF.Visible = true;
                            colC1_40RF.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;
                            colC2_40RF.Visible = true;
                            colC2_40RF.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                            break;
                    #endregion

                    #region 45GP
                    case "45GP":
                            colC1_45GP.Visible = true;
                            colC1_45GP.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;
                            colC2_45GP.Visible = true;
                            colC2_45GP.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                            colGap_45GP.Visible = true;
                            colGap_45GP.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                        break;
                    #endregion

                    #region 45RF
                    case "45RF":
                            colC1_45RF.Visible = true;
                            colC1_45RF.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;
                            colC2_45RF.Visible = true;
                            colC2_45RF.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                            colGap_45RF.Visible = true;
                            colGap_45RF.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                        break;
                    #endregion

                    #region 45HT
                    case "45HT":
                            colC1_45HT.Visible = true;
                            colC1_45HT.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;
                            colC2_45HT.Visible = true;
                            colC2_45HT.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                            colGap_45HT.Visible = true;
                            colGap_45HT.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                        break;
                    #endregion

                    #region 45FR
                    case "45FR":
                            colC1_45FR.Visible = true;
                            colC1_45FR.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;
                            colC2_45FR.Visible = true;
                            colC2_45FR.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                            colGap_45FR.Visible = true;
                            colGap_45FR.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                        break;
                    #endregion

                    #region 45OT
                    case "45OT":
                            colC1_45OT.Visible = true;
                            colC1_45OT.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;
                            colC2_45OT.Visible = true;
                            colC2_45OT.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                            colGap_45OT.Visible = true;
                            colGap_45OT.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                        break;
                    #endregion

                    #region 45TK
                    case "45TK":
                            colC1_45TK.Visible = true;
                            colC1_45TK.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;
                            colC2_45TK.Visible = true;
                            colC2_45TK.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                            colGap_45TK.Visible = true;
                            colGap_45TK.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                        break;
                    #endregion

                    #region 45RH
                    case "45RH":
                            colC1_45RH.Visible = true;
                            colC1_45RH.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;
                            colC2_45RH.Visible = true;
                            colC2_45RH.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                            colGap_45RH.Visible = true;
                            colGap_45RH.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                        break;
                    #endregion

                    #region 53HQ
                    case "53HQ":
                            colC1_53HQ.Visible = true;
                            colC1_53HQ.VisibleIndex = C1VisibleIndex;
                            C1VisibleIndex++;
                            colC2_53HQ.Visible = true;
                            colC2_53HQ.VisibleIndex = C2VisibleIndex;
                            C2VisibleIndex++;
                            colGap_53HQ.Visible = true;
                            colGap_53HQ.VisibleIndex = GapVisibleIndex;
                            GapVisibleIndex++;
                        break;
                    #endregion
                }
            }

            #endregion
        }

        #endregion

        #region 设置Grid 样式
        /// <summary>
        /// 如果差异价格不等于0，则红色显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (GapColumnNameList.Contains(e.Column.FieldName))
            {
                if (e.CellValue == null)
                {
                    return;
                }
                if (Convert.ToDecimal(e.CellValue)!=0)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }
        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            OceanContractCompar item = gvMain.GetRow(e.RowHandle) as OceanContractCompar;
            if (item == null)
            {
                return;
            }

            if (item.IDS.IndexOf(",") < 0)
            {
                e.Appearance.ForeColor = Color.Red;
            }
        }
        #endregion

        #region 选择的模式发生改变时
        private void rgpMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            int theradID = 0;
            if (rgpMode.SelectedIndex == 0)
            {
                SetDataSource(BaseDataList);
            }
            else if (rgpMode.SelectedIndex == 1)
            { 
                theradID=LoadingServce.ShowLoadingForm("Loading......");

                List<OceanContractCompar> list = (from d in BaseDataList where d.IDS.IndexOf(",") == -1 select d).ToList();
                SetDataSource(list);

                LoadingServce.CloseLoadingForm(theradID);

            }
            else if (rgpMode.SelectedIndex == 2)
            {
                theradID=LoadingServce.ShowLoadingForm("Loading......");

                List<OceanContractCompar> list = (from d in BaseDataList where 
                                                         d.Gap_20GP!=0||
                                                     d.Gap_20FR!=0||
                                                     d.Gap_20HQ!=0||
                                                     d.Gap_20HT!=0||
                                                     d.Gap_20NOR!=0||
                                                     d.Gap_20OT!=0||
                                                     d.Gap_20RF!=0||
                                                     d.Gap_20RH!=0||
                                                     d.Gap_20TK!=0||
                                                     d.Gap_40FR!=0||
                                                     d.Gap_40GP!=0||
                                                     d.Gap_40HQ!=0||
                                                     d.Gap_40HT!=0||
                                                     d.Gap_40NOR!=0||
                                                     d.Gap_40OT!=0||
                                                     d.Gap_40RF!=0||
                                                     d.Gap_40RH!=0||
                                                     d.Gap_40TK!=0||
                                                     d.Gap_45FR!=0||
                                                     d.Gap_45GP!=0||
                                                     d.Gap_45HQ!=0||
                                                     d.Gap_45HT!=0||
                                                     d.Gap_45OT!=0||
                                                     d.Gap_45RF!=0||
                                                     d.Gap_45RH!=0||
                                                     d.Gap_45TK!=0||
                                                     d.Gap_53HQ != 0
                                                     select d
                                                      ).ToList();
                SetDataSource(list);

                LoadingServce.CloseLoadingForm(theradID);

            }
            

        }
        /// <summary>
        /// 设置数据源
        /// </summary>
        /// <param name="list"></param>
        private void SetDataSource(List<OceanContractCompar> list)
        {
            TransSearchOceanRateList(list);

            bsList.DataSource = list;
            bsList.ResetBindings(false);

            SetGridrWidth(list.Count);
        }
        private void SetGridrWidth(int count)
        { 
            if(count>=1000&&count<=9999)
            {
                gvMain.IndicatorWidth = 45;
            }
            else if (count >= 10000)
            {
                gvMain.IndicatorWidth = 50;
            }
            else
            {
                gvMain.IndicatorWidth = 40;
            }
        }
        #endregion

		 
	    #region 导出到Excel
        private void btnExportToExcel_Click(object sender, EventArgs e)
        {


            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = "Ocean Contract Comparision List";
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

    }
}
