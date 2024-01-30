namespace ICP.MailCenter.UI
{
   public interface IAPPArgHandler
    {   
       /// <summary>
       /// 启动参数
       /// </summary>
       string[] Parameters {get;set;}
       /// <summary>
       /// 预处理方法
       /// </summary>
       void PreHandle();
       /// <summary>
       /// 处理方法
       /// </summary>
       void Handle();
       /// <summary>
       /// 处理后，清理方法
       /// </summary>
       void PostHandle();

    }
}
