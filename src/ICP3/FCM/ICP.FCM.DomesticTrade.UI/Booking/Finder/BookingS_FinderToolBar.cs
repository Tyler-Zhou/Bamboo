using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;

namespace ICP.FCM.DomesticTrade.UI.Booking.Finder
{
    [ToolboxItem(false)]
    public partial class BookingS_FinderToolBar : BaseToolBar
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();

        public BookingS_FinderToolBar()
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
            barAdd.Caption = "新增(&A)";
            barCopy.Caption = "复制(&Y)";
            barEdit.Caption = "编辑(&E)";
            barConfirm.Caption = "确定(&O)";
            barClose.Caption = "关闭(&C)";
            barShowSearch.Caption = "查询(&H)";
        }

        private void BulidCommond()
        {
            barAdd.ItemClick += delegate { Workitem.Commands[DTBookingFinderCommandConstants.Command_AddData].Execute(); };
            barCopy.ItemClick += delegate { Workitem.Commands[DTBookingFinderCommandConstants.Command_CopyData].Execute(); };
            barEdit.ItemClick += delegate { Workitem.Commands[DTBookingFinderCommandConstants.Command_EditData].Execute(); };
            barConfirm.ItemClick += delegate { Workitem.Commands[DTBookingFinderCommandConstants.Command_Confirm].Execute(); };
            barShowSearch.ItemClick += delegate { Workitem.Commands[DTBookingFinderCommandConstants.Command_ShowSearch].Execute(); };

            barClose.ItemClick += delegate { FindForm().Close(); };
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
