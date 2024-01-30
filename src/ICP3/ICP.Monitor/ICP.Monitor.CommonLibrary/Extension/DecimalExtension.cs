#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/23 16:50:31
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

namespace ICP.Monitor.Common.Extension
{
    public static class DecimalExtension
    {
        /// <summary>
        /// decimal 是否为空或0
        /// </summary>
        public static bool IsNullOrZero(this decimal? input)
        {
            if (input == null) return true;

            if (input.Value == 0) return true;

            return false;
        }
    }
}
