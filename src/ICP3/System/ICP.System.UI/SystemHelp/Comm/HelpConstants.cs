
namespace ICP.Sys.UI.SystemHelp.Comm
{
    public class HelpConstants
    {
        public static string Url_NewFeedback = @"http://feedback.cityocean.com:8080/tswa/UI/Pages/WorkItems/WorkItemEdit.aspx?pid=282&state-guid={0}&oqs=wit%3dBug".ToLower();
        public static string Url_NewFeedback_AssignToElementID = "ctl00_c_we_ctl13_wc_txt";
        public static string Url_NewFeedback_CreatebyElementID = "ctl00_c_we_ctl21_wc_txt";
        public static string Url_Login = @"http://feedback.cityocean.com:8080/tswa/UI/Pages/Login.aspx".ToLower();
        public static string Url_Login_UserElementID = "ctl00_c_l1_eddUserName";
        public static string Url_Login_PwdElementID = "ctl00_c_l1_eddPassword";
        public static string Url_Login_SubmitElementID = "ctl00_c_l1_btnConnect";
        public static string Url_Login_RememberMeElementID = "ctl00_c_l1_chkRememberMe";
        public static string Url_MyFeedbacks = @"http://feedback.cityocean.com:8080/tswa/UI/Pages/WorkItems/EditQuery.aspx?pid=282".ToLower();
        public static string Url_MyFeedbacks_SearchElementID = "ctl00_wtb_tt1_0";
        public static string Url_MyFeedbacks_CreatebyElementID = "ctl00_c_qe1_clauseValue1_txt";
        public static string Url_MyFeedbacks_FieldElementID = "ctl00_c_qe1_clauseFieldName1_txt";
        public static string Url_MyFeedbacks_OperatorElementID = "ctl00_c_qe1_clauseOp1_txt";

        public static string Url_HelpDocument = "http://feedback.cityocean.com:8080/tswa/ICPHelp/index.html";


        public static string User_Email = "tomlai@cityocean.com";
    }


    public class HelpWorkSpaceConstants
    {
        public const string FeedbackMainWorkspace = "FeedbackMainWorkspace";
    }


}
