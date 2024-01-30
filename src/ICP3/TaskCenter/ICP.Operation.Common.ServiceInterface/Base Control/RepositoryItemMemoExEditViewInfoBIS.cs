using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using System.Drawing;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 
    /// </summary>
    class RepositoryItemMemoExEditViewInfoBIS : MemoExEditViewInfo, IHeightAdaptable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public RepositoryItemMemoExEditViewInfoBIS(RepositoryItem item)
            : base(item)
        { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="useDisplayText"></param>
        protected override void CalcTextSize(Graphics g, bool useDisplayText)
        {
            base.CalcTextSize(g, true);
        }


        #region IHeightAdaptable Members
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public int CalcHeight(GraphicsCache cache, int width)
        {
            CalcTextSize(cache.Graphics);
            return TextSize.Height;

        }
        /// <summary>
        /// 
        /// </summary>
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
