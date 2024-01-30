using System;
using System.Workflow.ComponentModel;
using System.Text.RegularExpressions;

namespace ICP.WF.Activities
{
      public static class WFUserDataKeys
      {
          public static readonly Guid CustomActivity = new Guid("298CF3E0-E9E0-4d41-A11B-506E9132EB27");
          public static readonly Guid CustomActivityDefaultName = new Guid("8bcd6c40-7bf6-4e60-8eea-bbf40bed92da");
          public static readonly Guid DesignTimeTypeNames = new Guid("8B018FBD-A60E-4378-8A79-8A190AE13EBA");
          public static readonly string MainWorkItem = "24F060BA-211E-438e-9FD2-8329A62F8144";
          public static readonly string RuleFile = "E0E61237-C3FF-493d-BC6D-517D5E00407E";
      }


      internal sealed class SR
      {
         // private static SR loader;
         // private ResourceManager resources = new ResourceManager("ICP.WF.Activities.Properties.Resources", Assembly.GetExecutingAssembly());
         
          internal SR()
          {
          }


          //private static SR GetLoader()
          //{
          //    if (loader == null)
          //    {
          //        loader = new SR();
          //    }
          //    return loader;
          //}

          //internal static string GetString(CultureInfo culture, string name)
          //{
          //    SR loader = GetLoader();
          //    if (loader == null)
          //    {
          //        return null;
          //    }
          //    return loader.resources.GetString(name, culture);
          //}

          //internal static string GetString(string name, params object[] args)
          //{
          //    return GetString(Culture, name, args);
          //}

          //internal static string GetString(CultureInfo culture, string name, params object[] args)
          //{
          //    SR loader = GetLoader();
          //    if (loader == null)
          //    {
          //        return null;
          //    }
          //    string format = loader.resources.GetString(name, culture);
          //    if ((args != null) && (args.Length > 0))
          //    {
          //        return string.Format(CultureInfo.CurrentCulture, format, args);
          //    }
          //    return format;
          //}

          

          //private static CultureInfo Culture
          //{
          //    get
          //    {
          //        return CultureInfo.CurrentCulture;
          //    }
          //}

          internal static string Error_SenderMustBeActivityExecutionContext
          {
              get
              {
                  return GetString("Error_SenderMustBeActivityExecutionContext", new object[] { typeof(ActivityExecutionContext).FullName });
              }
          }

          public static bool IsEnglish
          {
              get
              {
                return   ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish;
              }
          }

          public static string GetString(string key, string defaultValue, params object[] args)
          {
              try
              {
                  if (IsEnglish)
                  {//查找英文资源
                      string enVal = ICP.WF.Activities.Resources.Resource_EN.ResourceManager.GetString(key);
                      if (!string.IsNullOrEmpty(enVal) )
                      {
                          return string.Format(enVal, args);
                      }
                  }
                  else
                  {//查找中文资源
                      string cnVal = ICP.WF.Activities.Resources.Resource_CN.ResourceManager.GetString(key);
                      if (!string.IsNullOrEmpty(cnVal) )
                      {
                          return string.Format(cnVal, args); ;
                      }
                  }

                  return defaultValue;
              }
              catch
              {
                  return defaultValue;
              }
          }

          public static string GetString(string key, string defaultValue)
          {
              try
              {
                  if (IsEnglish)
                  {//查找英文资源
                      string enVal = ICP.WF.Activities.Resources.Resource_EN.ResourceManager.GetString(key);
                      if (!string.IsNullOrEmpty(enVal))
                      {
                          return enVal;
                      }
                  }
                  else
                  {//查找中文资源
                      string cnVal = ICP.WF.Activities.Resources.Resource_CN.ResourceManager.GetString(key);
                      if (!string.IsNullOrEmpty(cnVal))
                      {
                          return cnVal;
                      }
                  }

                  return defaultValue;
              }
              catch
              {
                  return defaultValue;
              }
          }

          public static string GetString(string key, params object[] args)
          {
              try
              {
                  if (IsEnglish)
                  {//查找英文资源
                      string enVal = ICP.WF.Activities.Resources.Resource_EN.ResourceManager.GetString(key);
                      if (!string.IsNullOrEmpty(enVal))
                      {
                          return string.Format(enVal, args);
                      }
                  }
                  else
                  {//查找中文资源
                      string cnVal = ICP.WF.Activities.Resources.Resource_CN.ResourceManager.GetString(key);
                      if (!string.IsNullOrEmpty(cnVal))
                      {
                          return string.Format(cnVal, args); ;
                      }
                  }

                  return string.Empty;
              }
              catch
              {
                  return string.Empty; ;
              }
          }

          internal static string InvalidConditionName
          {
              get
              {
                  return "InvalidConditionName";
              }
          }

          internal static string ContextStackMissing
          {
              get
              {
                  return "ContextStackMissing";
              }
          }


          internal static string NamePropertyDescription
          {
              get
              {
                  return "NamePropertyDescription";
              }
          }
      }
   
}
