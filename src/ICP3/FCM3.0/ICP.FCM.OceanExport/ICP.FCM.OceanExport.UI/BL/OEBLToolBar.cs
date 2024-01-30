using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;


namespace ICP.FCM.OceanExport.UI.BL
{
    [ToolboxItem(false)]
    public partial class OEBLToolBar : ICP.Framework.ClientComponents.UIFramework.BaseToolBar
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();

        public OEBLToolBar()
        {
            InitializeComponent();
            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();

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
            barSplitAndMerge.Caption = "分/合单";
            barSplitBL.Caption = "分单";
            barMergeBL.Caption = "合单";

            barAdd.Caption = "新增";
            barAddMBL.Caption = "MBL";
            barAddHBL.Caption = "HBL";
            barCopy.Caption = "复制(&O)";
            barEdit.Caption = "编辑(&E)";

            barSubCheck.Caption = "对单";
            barCheck.Caption = "申请(&K)";
            barCompleteCheck.Caption = "完成(&M)";
            barReleaseBL.Caption = "确认放单(&A)";
            barPrint.Caption = "打印";
            barPrintBL.Caption = "打印提单";
            barPrintLoadCtn.Caption = "打印装箱单";
            barPrintLoadGoods.Caption = "打印装货单";
            barLoadShip.Caption = "确认装船(&L)";
            barReplyAgent.Caption = "申请代理(&R)";
            barE_MBL.Caption = "电子补料";
            barISF.Caption = "ISF";
            barBill.Caption = "帐单(&B)";
            barVisibleMode.Caption = "全部";
            barVisibleAll.Caption = "全部";
            barVisibleHBL.Caption = "HBL";
            barVisibleMBL.Caption = "MBL";

        }

        private void BulidCommond()
        {
            barAddMBL.ItemClick += delegate { Workitem.Commands[OEBLCommandConstants.Command_AddMBL].Execute(); };
            barAddHBL.ItemClick += delegate { Workitem.Commands[OEBLCommandConstants.Command_AddHBL].Execute(); };
            barCopy.ItemClick += delegate { Workitem.Commands[OEBLCommandConstants.Command_CopyData].Execute(); };
            barEdit.ItemClick += delegate { Workitem.Commands[OEBLCommandConstants.Command_EditData].Execute(); };
            barDelete.ItemClick += delegate { Workitem.Commands[OEBLCommandConstants.Command_DeleteData].Execute(); };

            barCheck.ItemClick += delegate { Workitem.Commands[OEBLCommandConstants.Command_Check].Execute(); };
            barCompleteCheck.ItemClick += delegate { Workitem.Commands[OEBLCommandConstants.Command_CompleteCheck].Execute(); };

            barPrintBL.ItemClick += delegate { Workitem.Commands[OEBLCommandConstants.Command_PrintBL].Execute(); };

            barProfit.ItemClick += delegate { Workitem.Commands[OEBLCommandConstants.Command_PrintProfit].Execute(); };

            barPrintLoadCtn.ItemClick += delegate { Workitem.Commands[OEBLCommandConstants.Command_PrintLoadCtn].Execute(); };
            barPrintLoadGoods.ItemClick += delegate { Workitem.Commands[OEBLCommandConstants.Command_PrintLoadGoods].Execute(); };

            barLoadShip.ItemClick += delegate { Workitem.Commands[OEBLCommandConstants.Command_LoadShip].Execute(); };
            barReleaseBL.ItemClick += delegate { Workitem.Commands[OEBLCommandConstants.Command_ConfirmReleaseBL].Execute(); };
            barReplyAgent.ItemClick += delegate { Workitem.Commands[OEBLCommandConstants.Command_ReplyAgent].Execute(); };
            barE_MBL.ItemClick += delegate { Workitem.Commands[OEBLCommandConstants.Command_E_MBL].Execute(); };
            barISF.ItemClick += delegate { Workitem.Commands[OEBLCommandConstants.Command_ISF].Execute(); };
            barBill.ItemClick += delegate { Workitem.Commands[OEBLCommandConstants.Command_Bill].Execute(); };
            barShowSearch.ItemClick += delegate { Workitem.Commands[OEBLCommandConstants.Command_ShowSearch].Execute(); };
            barVerifiSheet.ItemClick += delegate { Workitem.Commands[OEBLCommandConstants.Command_VerifiSheet].Execute(); };
            barLoadContainer.ItemClick += delegate { Workitem.Commands[OEBLCommandConstants.Command_LoadContainer].Execute(); };
            barSplitBL.ItemClick += delegate { Workitem.Commands[OEBLCommandConstants.Command_SplitBL].Execute(); };
            barMergeBL.ItemClick += delegate { Workitem.Commands[OEBLCommandConstants.Command_Merge].Execute(); };
            barClose.ItemClick += delegate { this.FindForm().Close(); };

            barVisibleAll.ItemClick += delegate
            {
                barVisibleAll.Checked = true;
                Workitem.Commands[OEBLCommandConstants.Command_VisibleALL].Execute();
                barVisibleHBL.Checked = barVisibleMBL.Checked = false;
                barVisibleMode.Caption = barVisibleAll.Caption;
            };
            barVisibleHBL.ItemClick += delegate
            {
                barVisibleHBL.Checked = true;
                Workitem.Commands[OEBLCommandConstants.Command_VisibleHBL].Execute();
                barVisibleAll.Checked = barVisibleMBL.Checked = false;
                barVisibleMode.Caption = barVisibleHBL.Caption;
            };
            barVisibleMBL.ItemClick += delegate
            {
                barVisibleMBL.Checked = true;
                Workitem.Commands[OEBLCommandConstants.Command_VisibleMBL].Execute();
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
