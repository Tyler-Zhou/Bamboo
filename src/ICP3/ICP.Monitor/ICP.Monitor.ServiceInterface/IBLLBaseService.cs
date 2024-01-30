#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/9 17:42:59
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using ICP.Monitor.Model.Framework;

namespace ICP.Monitor.Interface
{
    public interface IBLLBaseService
    {
        /// <summary>
        /// Web程序上下文
        /// </summary>
        WebAppContext WebAppContext { get; set; }
    }
}
