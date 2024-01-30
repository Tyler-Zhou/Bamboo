using System;
using System.ComponentModel;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;

namespace ICP.FCM.AirExport.UI
{
    [ToolboxItem(false)]
    public partial class LWToolBar : XtraUserControl
    {
        public void AddToolBarItem(BarButtonItem barItem)
        {
            barManager1.Items.AddRange(new BarItem[] {barItem});
            bar2.LinksPersistInfo.AddRange(new LinkPersistInfo[] {
            new LinkPersistInfo(barItem)});
        }
        public void AddToolBarItem(BarButtonItem[] barItems)
        {
            barManager1.Items.AddRange(barItems);
            foreach (var item in barItems)
            {
                bar2.LinksPersistInfo.Add(new LinkPersistInfo(BarLinkUserDefines.PaintStyle, item, "", true, true, true, 0, null, BarItemPaintStyle.CaptionGlyph));
            }

        }

        public LWToolBar()
        {
            InitializeComponent();
            Load += new EventHandler(LWToolBar_Load);
            
        }

        void LWToolBar_Load(object sender, EventArgs e)
        {
            if(bar2.ItemLinks!=null && bar2.ItemLinks.Count >0)
            {
                int height = 0;
                for (int i = 0; i < bar2.ItemLinks.Count; i++)
			    {
    			    if(bar2.ItemLinks[i].Bounds.Height >height)
                        height = bar2.ItemLinks[i].Bounds.Height;
			    }
                Height = height+5;
            }
        }
    }
}
