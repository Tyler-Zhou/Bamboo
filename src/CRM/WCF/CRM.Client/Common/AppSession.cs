namespace CRM.Client.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class AppSession
    {
        #region 成员(Member)
        /// <summary>
        /// 
        /// </summary>
        public static long UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public static string AccessToken { get; set; } = "";
        /// <summary>
        /// 
        /// </summary>
        public static string RefreshToken { get; set; } = "";

        #endregion
    }
}
