using DevExpress.XtraBars;
using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ICP.FCM.OtherBusiness.UI.Common
{
    /// <summary>
    /// 其他业务-工具栏
    /// </summary>
    [ToolboxItem(false)]
    public partial class OBMainToolBar : BaseToolBar
    {
        #region 字段 & 属性 & 事件
        public AddType addType
        {
            get;
            set;
        }
        /// <summary>
        /// 是否隐藏核销单
        /// </summary>
        protected virtual bool IsHidebarVerifiSheet { get; set; }
        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();
        [EventPublication("AddData")]
        public event EventHandler<DataEventArgs<AddType>> AddEvent;
        #endregion

        #region Service
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public OBMainToolBar()
        {
            InitializeComponent();
            Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
            if (LocalData.IsEnglish == false) SetCnText();

            BulidBarItemDictionary();
            //BulidCommond();

            //if (IsHidebarVerifiSheet)
            //{
            //    barVerifiSheet.Visibility = BarItemVisibility.Never;
            //    barPickUp.Visibility = BarItemVisibility.Never;
            //}
            //else
            //{
            //    barVerifiSheet.Visibility = BarItemVisibility.Always;
            //    barPickUp.Visibility = BarItemVisibility.Always;
            //}

            //if (addType == AddType.OtherBusinessOrder)
            //{
            //    SetPermissions();
            //}
        } 
        #endregion

        
        private void SetCnText()
        {
            barAdd.Caption = "新增";
            barEdit.Caption = "编辑";
            barDownload.Caption = "下载";
            barClose.Caption = "关闭";
            barRefresh.Caption = "刷新";
            barFaxLog.Caption = "传真日志";
            barPrint.Caption = "打印";
            barRemark.Caption = "备注";
            barBill.Caption = "账单";
            barVerifiSheet.Caption = "核销单";
            barCancel.Caption = "作废";
            barSearch.Caption = "查询";
        }
        private void BulidBarItemDictionary()
        {
            foreach (BarItem item in barManager1.Items)
            {
                _barItemDic.Add(item.Name, item);
            }
        }
        /// <summary>
        /// 构建Command
        /// </summary>
        private void BulidCommond()
        {
            //barAdd.ItemClick += delegate
            //{
            //    AddType type = AddType.OtherBusinessOrder;
            //    //其他业务订单新增
            //    if (!IsHidebarVerifiSheet)
            //    {
            //        type = AddType.OtherBusiness;

            //    }
            //    Workitem.RootWorkItem.State["AddData"] = type;
            //    Workitem.Commands[OBCommandConstants.Command_AddOtherData].Execute();
            //};
            //barRefresh.ItemClick += delegate { Workitem.Commands[OBCommandConstants.Command_RefreshData].Execute(); };
            //barEdit.ItemClick += delegate { Workitem.Commands[OBCommandConstants.Command_EditData].Execute(); };
            //barCopy.ItemClick += delegate { Workitem.Commands[OBCommandConstants.Command_CopyData].Execute(); };
            //barCancel.ItemClick += delegate { Workitem.Commands[OBCommandConstants.Command_CancelData].Execute(); };
            //barOpContact.ItemClick += delegate { Workitem.Commands[OBCommandConstants.Command_OpContact].Execute(); };
            //barProfit.ItemClick += delegate { Workitem.Commands[OBCommandConstants.Command_Profit].Execute(); };
            //barSearch.ItemClick += delegate { Workitem.Commands[OBCommandConstants.Command_ShowSearch].Execute(); };
            //barBill.ItemClick += delegate { Workitem.Commands[OBCommandConstants.Command_Bill].Execute(); };
            //if (!IsHidebarVerifiSheet)
            //{
            //    barVerifiSheet.ItemClick += delegate { Workitem.Commands[OBCommandConstants.Command_VerifiSheet].Execute(); };
            //}

            //barPickUp.ItemClick += delegate { Workitem.Commands[OBCommandConstants.Command_PickUp].Execute(); };  
            //barClose.ItemClick += delegate { this.FindForm().Close(); };
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
