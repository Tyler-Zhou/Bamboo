using ICP.EDI.ServiceInterface;
using System;
using System.IO;
using System.Reflection;

namespace ICP.EDI.ServiceComponent
{
    /// <summary>
    /// 映射对象帮助类
    /// </summary>
   public class ReflectHelper
    {
        /// <summary>
        /// 动态加载程序方法
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="NameSpaceAndClassName"></param>
        /// <param name="MethodName"></param>
        /// <param name="Parameters"></param>
        public static void CallAssembly(string path, string nameSpaceAndClassName, string methodName, object[] parameters)
        {
            try
            {
                Assembly ass = Assembly.LoadFrom(path);//调入文件(不限于dll,exe亦可,只要是.net) 
                Type tp = ass.GetType(nameSpaceAndClassName);//NameSpaceAndClassName是"名字空间.类名",如"namespace1.Class1" 
                MethodInfo mi = tp.GetMethod(methodName);//MethodName是要调用的方法名,如"Main" 
                object meobj = System.Activator.CreateInstance(tp);
                mi.Invoke(meobj, parameters);//Parameters是调用目标方法时传入的参数列表 
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IMapping GetEDIPlugIn(string ediServerDir,string assembly)
        {
            if (string.IsNullOrEmpty(assembly))
            {
                throw new Exception("程序集配置错误。");
            }
            try
            {
                string[] assemArr = assembly.Split(",".ToCharArray());
                string path = ediServerDir + "\\Plugs\\" + assemArr[0] + ".dll";
                if (File.Exists(path))
                {
                    Assembly ass = Assembly.LoadFrom(path);//调入文件(不限于dll,exe亦可,只要是.net) 

                    Type tp = ass.GetType(assemArr[1]);//NameSpaceAndClassName是"名字空间.类名",如"namespace1.Class1" 
                    object meobj = Activator.CreateInstance(tp);
                    return (IMapping)meobj;
                }
                else
                {
                    return null;
                }
            }
            catch (InvalidCastException)
            {
                throw new Exception("插件必须实现ICP.EDI.ServiceInterface.dll程序集中IMapping接口");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 实例化插件对象
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static object GetPlugInInstance(string ediServerDir, string assembly)
        {
            if (string.IsNullOrEmpty(assembly))
            {
                throw new Exception("程序集配置错误。");
            }
            try
            {
                string[] assemArr = assembly.Split(",".ToCharArray());

                string path = ediServerDir + "\\Plugs\\" + assemArr[0] + ".dll";
                if (File.Exists(path))
                {
                    Assembly ass = Assembly.LoadFrom(path);//调入文件(不限于dll,exe亦可,只要是.net) 

                    Type tp = ass.GetType(assemArr[1]);//NameSpaceAndClassName是"名字空间.类名",如"namespace1.Class1" 
                    if (tp == null)
                    {
                        throw new Exception(string.Format("未找到类:{0}", assemArr[1]));
                    }
                    return Activator.CreateInstance(tp);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
