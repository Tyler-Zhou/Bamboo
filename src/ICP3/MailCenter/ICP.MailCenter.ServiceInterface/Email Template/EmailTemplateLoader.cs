namespace ICP.MailCenter.ServiceInterface
{
    /// <summary>
    /// 邮件模板文件载入类
    /// </summary>
    public class EmailTemplateLoader : TemplateBaseDictionary<string, EmailTemplateFileData>
    {

        private EmailTemplateLoader() { }
        private static EmailTemplateLoader context;
        private static object synObj = new object();
        /// <summary>
        /// 返回唯一实例
        /// </summary>
        public static EmailTemplateLoader Current
        {
            get
            {
                //if (context == null)
                //{
                //    lock (synObj.GetType())
                //    {
                //        if (context == null)
                //        {
                context = new EmailTemplateLoader();
                //        }
                //    }
                //}
                return context;

            }

        }
        public override void EnsureExists(string key)
        {
            if (this.ContainsKey(key))
                return;
            EmailTemplateFileData fileData = new EmailTemplateFileData { LanguageName = key };
            fileData.Init();
            this.Add(key, fileData);


        }



    }
}
