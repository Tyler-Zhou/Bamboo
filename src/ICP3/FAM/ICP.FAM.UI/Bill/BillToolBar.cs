using System.Collections.Generic;
using System.ComponentModel;
using ICP.FAM.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;
using DevExpress.XtraBars;
using ICP.Framework.CommonLibrary.Client;
using System;

namespace ICP.FAM.UI.Bill
{
    [ToolboxItem(false)]
    public partial class BillToolBar : BaseToolBar
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IFinanceClientService FinanceClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFinanceClientService>();
            }
        }

        #endregion

        #region 初始化

        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();

        public BillToolBar()
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                //应收转代理账单权限
                if (LocalCommonServices.PermissionService.HaveActionPermission(ActionsConstants.CovnertBillForARToDN))
                {
                    barConvertBillFromARToDN.Visibility = BarItemVisibility.Always;
                }
                else
                {
                    barConvertBillFromARToDN.Visibility = BarItemVisibility.Never;
                }
            }
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
            barViewInvoice.Caption = "发票信息(&I)";
            barBusinessInfo.Caption = "业务信息(&B)";
            barClose.Caption = "关闭(&C)";
            barFeeDetail .Caption = "帐单明细(&F)";
            barShowSelected .Caption = "显示批量选择";
            barShowTotal .Caption = "显示合计";
            barWriteOffHistory .Caption = "销账信息(&W)";
        }

        private void BulidCommond()
        {
            barBusinessInfo.ItemClick += delegate { Workitem.Commands[BillCommandConstants.Command_ViewBusinessInfo].Execute(); };
            barBillList.ItemClick += delegate { Workitem.Commands[BillCommandConstants.Command_ViewBillList].Execute(); };
            barWriteOffHistory.ItemClick += delegate { Workitem.Commands[BillCommandConstants.Command_WriteOffHistory].Execute(); };
            barFeeDetail.ItemClick += delegate { Workitem.Commands[BillCommandConstants.Command_FeeDetail].Execute(); };
            barShowSearch.ItemClick += delegate { Workitem.Commands[BillCommandConstants.Command_ShowSearch].Execute(); };
            barShowSelected.ItemClick += delegate { Workitem.Commands[BillCommandConstants.Command_ShowSelected].Execute(); };
            barShowTotal.ItemClick += delegate { Workitem.Commands[BillCommandConstants.Command_ShowTotal].Execute(); };
            barViewInvoice.ItemClick += delegate { Workitem.Commands[BillCommandConstants.Command_ViewInvoice].Execute(); };
            barAllCheck.ItemClick += delegate { Workitem.Commands[BillCommandConstants.Command_AllCheck].Execute(); };
            barOpenTaskCenter.ItemClick += delegate { Workitem.Commands[BillCommandConstants.Command_OpenTaskCenter].Execute(); };
            barConvertBillFromARToDN.ItemClick += delegate { Workitem.Commands[BillCommandConstants.Command_ConvertBillFromARToDN].Execute(); };

            barSetInvoiceNo.ItemClick += delegate { Workitem.Commands[BillCommandConstants.Command_SetInvoiceNo].Execute(); };
            barClose.ItemClick += delegate { FindForm().Close(); };
        }

        #endregion

        #region IToolBar成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="enable"></param>
        public override void SetEnable(string name, bool enable)
        {
            if (_barItemDic.ContainsKey(name) && _barItemDic[name] != null)
                _barItemDic[name].Enabled = enable;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="visible"></param>
        public override void SetVisible(string name, bool visible)
        {
            if (_barItemDic.ContainsKey(name) && _barItemDic[name] != null)
                _barItemDic[name].Visibility = visible ? BarItemVisibility.Always : BarItemVisibility.Never;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        public override void SetText(string name, string text)
        {
            if (_barItemDic.ContainsKey(name) && _barItemDic[name] != null)
                _barItemDic[name].Caption = text;
        }

        #endregion
    }
}
