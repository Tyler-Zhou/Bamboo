#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/3/29 10:07:40
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System.Windows.Forms;

namespace Cityocean.TaskSchedule.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomListView : ListView
    {
        /// <summary>
        /// 
        /// </summary>
        public CustomListView()
        {
            // Activate double buffering  
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            // Enable the OnNotifyMessage event so we get a chance to filter out   
            // Windows messages before they get to the form's WndProc  
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        protected override void OnNotifyMessage(Message m)
        {
            //Filter out the WM_ERASEBKGND message  
            if (m.Msg != 0x14)
            {
                base.OnNotifyMessage(m);
            }
        }
    }
}
