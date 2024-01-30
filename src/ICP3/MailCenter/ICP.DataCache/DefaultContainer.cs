#region Comment

/*
 * 
 * FileName:    DefaultContainer.cs
 * CreatedOn:   2015/5/25 14:01:15
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion
using Castle.Facilities.WcfIntegration;
using Castle.Windsor;
using System;
using System.ServiceModel.Description;
using ICP.Framework.CommonLibrary;

namespace ICP.DataCache
{
    /// <summary>
    /// Windsor容器的扩展实现
    /// </summary>
    public class DefaultContainer : WindsorContainer
    {

        static IWindsorContainer _Container;

        /// <summary>
        /// 
        /// </summary>
        public static IWindsorContainer Container
        {
            get
            {
                if (_Container == null) _Container = new DefaultContainer();
                return DefaultContainer._Container;
            }
        }

        /// <summary>
        /// 默认构造方法
        /// </summary>
        /// <exception cref="Exception">日志启动错误</exception>
        public DefaultContainer()
        {
            AddFacility(new WcfFacility());


            AddFacility(new Castle.Facilities.Startable.StartableFacility());


            try
            {
                Install(Castle.Windsor.Installer.Configuration.FromAppConfig());
                _Container = this;
            }
            catch(Exception ex)
            {
                Logger.Log.Error(ex);
                //TempLogger.TempWriteLog(ex.Message);
            }
        }
    }
}