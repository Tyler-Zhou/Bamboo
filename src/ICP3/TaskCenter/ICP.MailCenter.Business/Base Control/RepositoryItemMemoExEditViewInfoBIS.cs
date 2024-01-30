using System;
using System.Drawing;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;



namespace ICP.Operation.Common.ServiceInterface
{
    class RepositoryItemMemoExEditViewInfoBIS : MemoExEditViewInfo, IHeightAdaptable
    {
        public RepositoryItemMemoExEditViewInfoBIS(RepositoryItem item)
            : base(item)
        { }

        protected override void CalcTextSize(Graphics g, bool useDisplayText)
        {
            base.CalcTextSize(g, true);
        }


        #region IHeightAdaptable Members

        public int CalcHeight(DevExpress.Utils.Drawing.GraphicsCache cache, int width)
        {
            CalcTextSize(cache.Graphics);
            return TextSize.Height;

        }

        protected override int MaxTextWidth
        {
            get
            {
                return MaskBoxRect.Width;
            }
        }
      
        #endregion
    }
}
