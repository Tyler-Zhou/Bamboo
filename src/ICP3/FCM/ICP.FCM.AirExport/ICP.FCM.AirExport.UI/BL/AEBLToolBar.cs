using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;


namespace ICP.FCM.AirExport.UI.BL
{
    [ToolboxItem(false)]
    public partial class AEBLToolBar : BaseToolBar
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();

        public AEBLToolBar()
        {
            InitializeComponent();
            Disposed += delegate {
                _barItemDic.Clear();
                _barItemDic = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (LocalData.IsEnglish == false) SetCnText();

            BulidBarItemDictionary();
            BulidCommond();
        }

        private void BulidBarItemDictionary()
        {
            foreach (BarItem item in barManager1.Items)
            {
                _barItemDic.Add(item.Name, item);
            }
        }

        private void SetCnText()
        {
            barClose.Caption = "关闭(&C)";
            barDelete.Caption = "删除(&D)";
            barCopy.Caption = "复制(&O)";

            barShowSearch.Caption = "查询(&H)";
            barShowSearch.Hint = "查询(H)";

            barRefresh.Hint = "刷新(R)";
            barRefresh.Caption = "刷新(&R)";

            barEdit.Caption = "编辑(&E)";
            
            barAdd.Caption = "新增";
            barAddMBL.Caption = "MAWB";
            barAddHBL.Caption = "HAWB";
            barCopy.Caption = "复制(&O)";
            barEdit.Caption = "编辑(&E)";

            barSubCheck.Caption = "完成对单";
            //barCheck.Caption = "申请(&K)";
            //barCompleteCheck.Caption = "完成(&M)";
            barReleaseBL.Caption = "确认放单(&A)";
            barPrint.Caption = "打印";
            barPrintBL.Caption = "打印提单";
            //barPrintLoadCtn.Caption = "打印装箱单";
            //barPrintLoadGoods.Caption = "打印装货单";
            barReplyAgent.Caption = "申请代理(&R)";
            barE_MBL.Caption = "E MBL";
            barISF.Caption = "ISF";
            barBill.Caption = "帐单(&B)";
            barVisibleMode.Caption = "全部";
            barVisibleAll.Caption = "全部";
            barVisibleHBL.Caption = "HAWB";
            barVisibleMBL.Caption = "MAWB";
            barDispatch.Caption = "分发文档";
            barDispatchLog.Caption = "分发文档记录";

        }

        private void BulidCommond()
        {
            barAddMBL.ItemClick += delegate { Workitem.Commands[AEBLCommandConstants.Command_AddMBL].Execute(); };
            barAddHBL.ItemClick += delegate { Workitem.Commands[AEBLCommandConstants.Command_AddHBL].Execute(); };
            barCopy.ItemClick += delegate { Workitem.Commands[AEBLCommandConstants.Command_CopyData].Execute(); };
            barEdit.ItemClick += delegate { Workitem.Commands[AEBLCommandConstants.Command_EditData].Execute(); };
            barDelete.ItemClick += delegate { Workitem.Commands[AEBLCommandConstants.Command_DeleteData].Execute(); };

            //barCheck.ItemClick += delegate { Workitem.Commands[AEBLCommandConstants.Command_Check].Execute(); };
            //barCompleteCheck.ItemClick += delegate { Workitem.Commands[AEBLCommandConstants.Command_CompleteCheck].Execute(); }; 
            barSubCheck.ItemClick += delegate { Workitem.Commands[AEBLCommandConstants.Command_CompleteCheck].Execute(); };
           
            barPrintBL.ItemClick += delegate { Workitem.Commands[AEBLCommandConstants.Command_PrintBL].Execute(); };
            barDispatch.ItemClick += delegate { Workitem.Commands[AEBLCommandConstants.Command_Dispatch].Execute(); };
            barDispatchLog.ItemClick += delegate { Workitem.Commands[AEBLCommandConstants.Command_DispatchLog].Execute(); };
           
            barReleaseBL.ItemClick += delegate { Workitem.Commands[AEBLCommandConstants.Command_ConfirmReleaseBL].Execute(); };
            barReplyAgent.ItemClick += delegate { Workitem.Commands[AEBLCommandConstants.Command_ReplyAgent].Execute(); };
            barE_MBL.ItemClick += delegate { Workitem.Commands[AEBLCommandConstants.Command_E_MBL].Execute(); };
            barISF.ItemClick += delegate { Workitem.Commands[AEBLCommandConstants.Command_ISF].Execute(); };
            barBill.ItemClick += delegate { Workitem.Commands[AEBLCommandConstants.Command_Bill].Execute(); };
            barShowSearch.ItemClick += delegate { Workitem.Commands[AEBLCommandConstants.Command_ShowSearch].Execute(); };
            barVerifiSheet.ItemClick += delegate { Workitem.Commands[AEBLCommandConstants.Command_VerifiSheet].Execute(); };
            barClose.ItemClick += delegate { FindForm().Close(); };

            barVisibleAll.ItemClick += delegate
            {
                barVisibleAll.Checked = true;
                Workitem.Commands[AEBLCommandConstants.Command_VisibleALL].Execute();
                barVisibleHBL.Checked = barVisibleMBL.Checked = false;
                barVisibleMode.Caption = barVisibleAll.Caption;
            };
            barVisibleHBL.ItemClick += delegate
            {
                barVisibleHBL.Checked = true;
                Workitem.Commands[AEBLCommandConstants.Command_VisibleMBL].Execute();
                barVisibleAll.Checked = barVisibleMBL.Checked = false;
                barVisibleMode.Caption = barVisibleHBL.Caption;
            };
            barVisibleMBL.ItemClick += delegate
            {
                barVisibleMBL.Checked = true;
                Workitem.Commands[AEBLCommandConstants.Command_VisibleHBL].Execute();
                barVisibleHBL.Checked = barVisibleAll.Checked = false;
                barVisibleMode.Caption = barVisibleMBL.Caption;
            };
        }

        #region IToolBar成员

        public override void SetEnable(string name, bool enable)
        {
            if (_barItemDic.ContainsKey(name) && _barItemDic[name] != null)
                _barItemDic[name].Enabled = enable;
        }

        public override void SetVisible(string name, bool visible)
        {
            if (_barItemDic.ContainsKey(name) && _barItemDic[name] != null)
                _barItemDic[name].Visibility = visible ? BarItemVisibility.Always : BarItemVisibility.Never;
        }

        public override void SetText(string name, string text)
        {
            if (_barItemDic.ContainsKey(name) && _barItemDic[name] != null)
                _barItemDic[name].Caption = text;
        }

        #endregion
    }
}
