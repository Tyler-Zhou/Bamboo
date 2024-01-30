using System;
using System.ComponentModel;

namespace ICP.FCM.AirExport.UI
{
    [ToolboxItem(false)]
    public partial class LWToolBar : DevExpress.XtraEditors.XtraUserControl
    {
        public void AddToolBarItem(DevExpress.XtraBars.BarButtonItem barItem)
        {
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {barItem});
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(barItem)});
        }
        public void AddToolBarItem(DevExpress.XtraBars.BarButtonItem[] barItems)
        {
            this.barManager1.Items.AddRange(barItems);
            foreach (var item in barItems)
            {
                this.bar2.LinksPersistInfo.Add(new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, item, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph));
            }

        }

        public LWToolBar()
        {
            InitializeComponent();
            this.Load += new EventHandler(LWToolBar_Load);
            
        }

        void LWToolBar_Load(object sender, EventArgs e)
        {
            if(this.bar2.ItemLinks!=null && this.bar2.ItemLinks.Count >0)
            {
                int height = 0;
                for (int i = 0; i < this.bar2.ItemLinks.Count; i++)
			    {
    			    if(this.bar2.ItemLinks[i].Bounds.Height >height)
                        height = this.bar2.ItemLinks[i].Bounds.Height;
			    }
                this.Height = height+5;
            }
        }
    }
}
