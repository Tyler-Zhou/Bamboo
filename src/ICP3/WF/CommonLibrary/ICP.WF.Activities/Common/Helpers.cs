using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Serialization;
using System.Xml;
using ICP.WF.Activities.Common;
using Microsoft.Win32;

namespace ICP.WF.Activities
{
    internal static class WFHelpers
    {
        internal static XmlWriter CreateXmlWriter(object output)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            settings.OmitXmlDeclaration = true;
            settings.CloseOutput = true;
            if (output is string)
            {
                return XmlWriter.Create(output as string, settings);
            }
            if (output is TextWriter)
            {
                return XmlWriter.Create(output as TextWriter, settings);
            }
            return null;
        }

        internal static XmlReader CreateXmlReader(object output)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.CloseInput = true;
            settings.ValidationType = ValidationType.Schema;
            if (output is string)
            {
                return XmlReader.Create(output as string, settings);
            }
            if (output is TextReader)
            {
                return XmlReader.Create(output as TextReader, settings);
            }
            return null;
        }

        internal static void DeserializeDesignersFromStream(ICollection activities, Stream stateStream)
        {
            if (stateStream.Length != 0L)
            {
                BinaryReader reader = new BinaryReader(stateStream);
                stateStream.Seek(0L, SeekOrigin.Begin);
                Queue<IComponent> queue = new Queue<IComponent>();
                foreach (IComponent component in activities)
                {
                    queue.Enqueue(component);
                }
                while (queue.Count > 0)
                {
                    IComponent component2 = queue.Dequeue();
                    if ((component2 != null) && (component2.Site != null))
                    {
                        IDesignerHost service = component2.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;
                        if (service == null)
                        {
                            throw new InvalidOperationException(SR.GetString("General_MissingService", new object[] { typeof(IDesignerHost).Name }));
                        }
                        ActivityDesigner designer = service.GetDesigner(component2) as ActivityDesigner;
                        if (designer != null)
                        {
                            try
                            {
                                ((IPersistUIState)designer).LoadViewState(reader);
                                CompositeActivity activity = component2 as CompositeActivity;
                                if (activity != null)
                                {
                                    foreach (IComponent component3 in activity.Activities)
                                    {
                                        queue.Enqueue(component3);
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
        }

        internal static AccessTypes GetAccessType(PropertyInfo property, object owner, IServiceProvider serviceProvider)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("owner");
            }
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }
            if (property != null)
            {
                IDynamicPropertyTypeProvider provider = owner as IDynamicPropertyTypeProvider;
                if (provider != null)
                {
                    return provider.GetAccessType(serviceProvider, property.Name);
                }
            }
            return AccessTypes.Read;
        }

        private static Activity GetActivity(Activity containerActivity, string id)
        {
            if (containerActivity != null)
            {
                Queue queue = new Queue();
                queue.Enqueue(containerActivity);
                while (queue.Count > 0)
                {
                    Activity activity = (Activity)queue.Dequeue();
                    if (activity.Enabled)
                    {
                        if (activity.Name == id)
                        {
                            return activity;
                        }
                        if (activity is CompositeActivity)
                        {
                            foreach (Activity activity2 in ((CompositeActivity)activity).Activities)
                            {
                                queue.Enqueue(activity2);
                            }
                        }
                    }
                }
            }
            return null;
        }

        public static IList<Activity> GetAllEnabledActivities(CompositeActivity compositeActivity)
        {
            if (compositeActivity == null)
            {
                throw new ArgumentNullException("compositeActivity");
            }
            List<Activity> list = new List<Activity>(compositeActivity.EnabledActivities);
            foreach (Activity activity in compositeActivity.Activities)
            {
                if (activity.Enabled && IsFrameworkActivity(activity))
                {
                    list.Add(activity);
                }
            }
            return list;
        }

        internal static Activity[] GetAllNestedActivities(CompositeActivity compositeActivity)
        {
            if (compositeActivity == null)
            {
                throw new ArgumentNullException("compositeActivity");
            }
            ArrayList list = new ArrayList();
            Queue queue = new Queue();
            queue.Enqueue(compositeActivity);
            while (queue.Count > 0)
            {
                CompositeActivity activity = (CompositeActivity)queue.Dequeue();
                if ((activity == compositeActivity) || !IsCustomActivity(activity))
                {
                    foreach (Activity activity2 in activity.Activities)
                    {
                        list.Add(activity2);
                        if (activity2 is CompositeActivity)
                        {
                            queue.Enqueue(activity2);
                        }
                    }
                    foreach (Activity activity3 in activity.EnabledActivities)
                    {
                        if (!list.Contains(activity3))
                        {
                            list.Add(activity3);
                            if (activity3 is CompositeActivity)
                            {
                                queue.Enqueue(activity3);
                            }
                        }
                    }
                }
            }
            return (Activity[])list.ToArray(typeof(Activity));
        }

        internal static T GetAttributeFromObject<T>(object attributeObject) where T : Attribute
        {
            if (attributeObject is AttributeInfoAttribute)
            {
                return (T)((AttributeInfoAttribute)attributeObject).AttributeInfo.CreateAttribute();
            }
            if (attributeObject is T)
            {
                return (T)attributeObject;
            }
            return default(T);
        }

        internal static string GetBaseIdentifier(Activity activity)
        {
            string name = activity.GetType().Name;
            StringBuilder builder = new StringBuilder(name.Length);
            for (int i = 0; i < name.Length; i++)
            {
                if (char.IsUpper(name[i]) && (((i == 0) || (i == (name.Length - 1))) || char.IsUpper(name[i + 1])))
                {
                    builder.Append(char.ToLowerInvariant(name[i]));
                }
                else
                {
                    builder.Append(name.Substring(i));
                    break;
                }
            }
            return builder.ToString();
        }

        internal static System.Type GetBaseType(PropertyInfo property, object owner, IServiceProvider serviceProvider)
        {
            if (owner == null)
            {
                throw new ArgumentNullException("owner");
            }
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }
            if (property == null)
            {
                return null;
            }
            IDynamicPropertyTypeProvider provider = owner as IDynamicPropertyTypeProvider;
            if (provider != null)
            {
                System.Type propertyType = provider.GetPropertyType(serviceProvider, property.Name);
                if (propertyType != null)
                {
                    return propertyType;
                }
            }
            return property.PropertyType;
        }

        internal static string GetClassName(string fullQualifiedName)
        {
            if (fullQualifiedName == null)
            {
                return null;
            }
            string str = fullQualifiedName;
            int num = fullQualifiedName.LastIndexOf('.');
            if (num != -1)
            {
                str = fullQualifiedName.Substring(num + 1);
            }
            return str;
        }

        internal static CodeTypeDeclaration GetCodeNamespaceAndClass(CodeNamespaceCollection namespaces, string namespaceName, string className, out CodeNamespace codeNamespace)
        {
            codeNamespace = null;
            foreach (CodeNamespace namespace2 in namespaces)
            {
                if (namespace2.Name == namespaceName)
                {
                    codeNamespace = namespace2;
                    break;
                }
            }
            if (codeNamespace != null)
            {
                foreach (CodeTypeDeclaration declaration2 in codeNamespace.Types)
                {
                    if (declaration2.Name == className)
                    {
                        return declaration2;
                    }
                }
            }
            return null;
        }

        //internal static Activity GetDataSourceActivity(Activity activity, string inputName, out string name)
        //{
        //    if (activity == null)
        //    {
        //        throw new ArgumentNullException("activity");
        //    }
        //    if (string.IsNullOrEmpty(inputName))
        //    {
        //        throw new ArgumentException("inputName");
        //    }
        //    name = inputName;
        //    if (inputName.IndexOf('.') == -1)
        //    {
        //        return activity;
        //    }
        //    int length = inputName.LastIndexOf('.');
        //    string activityName = inputName.Substring(0, length);
        //    name = inputName.Substring(length + 1);
        //    Activity activity2 = ParseActivityForBind(activity, activityName);
        //    if (activity2 == null)
        //    {
        //        activity2 = ParseActivity(GetRootActivity(activity), activityName);
        //    }
        //    return activity2;
        //}

        internal static System.Type GetDataSourceClass(Activity activity, IServiceProvider serviceProvider)
        {
            if (activity == null)
            {
                throw new ArgumentNullException("activity");
            }
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }
            string name = null;
            if (activity == GetRootActivity(activity))
            {
                name = activity.GetValue(WorkflowMarkupSerializer.XClassProperty) as string;
            }
            if (string.IsNullOrEmpty(name))
            {
                return activity.GetType();
            }
            ITypeProvider service = (ITypeProvider)serviceProvider.GetService(typeof(ITypeProvider));
            if (service == null)
            {
                throw new InvalidOperationException(SR.GetString("General_MissingService", new object[] { typeof(ITypeProvider).Name }));
            }
            return service.GetType(name);
        }

        internal static CompositeActivity GetDeclaringActivity(Activity activity)
        {
            if (activity == null)
            {
                throw new ArgumentNullException("activity");
            }
            for (CompositeActivity activity2 = activity.Parent; activity2 != null; activity2 = activity2.Parent)
            {
                if (activity2.Parent == null)
                {
                    return activity2;
                }
                if (IsCustomActivity(activity2))
                {
                    return activity2;
                }
            }
            return null;
        }

        internal static System.Type GetDelegateFromEvent(EventInfo eventInfo)
        {
            if (eventInfo.EventHandlerType != null)
            {
                return eventInfo.EventHandlerType;
            }
            return TypeProvider.GetEventHandlerType(eventInfo);
        }

        internal static string GetDesignTimeTypeName(object owner, object key)
        {
            string str = null;
            DependencyObject obj2 = owner as DependencyObject;
            if (((obj2 != null) && (key != null)) && obj2.UserData.Contains(WFUserDataKeys.DesignTimeTypeNames))
            {
                Hashtable hashtable = obj2.UserData[WFUserDataKeys.DesignTimeTypeNames] as Hashtable;
                if ((hashtable != null) && hashtable.ContainsKey(key))
                {
                    str = hashtable[key] as string;
                }
            }
            return str;
        }

      
      

     
    
        internal static Activity[] GetNestedActivities(CompositeActivity compositeActivity)
        {
            if (compositeActivity == null)
            {
                throw new ArgumentNullException("compositeActivity");
            }
            ArrayList list2 = new ArrayList();
            Queue queue = new Queue();
            queue.Enqueue(compositeActivity);
            while (queue.Count > 0)
            {
                CompositeActivity activity = (CompositeActivity)queue.Dequeue();
                foreach (Activity activity2 in activity.Activities)
                {
                    list2.Add(activity2);
                    if (activity2 is CompositeActivity)
                    {
                        queue.Enqueue(activity2);
                    }
                }
            }
            return (Activity[])list2.ToArray(typeof(Activity));
        }

        internal static Activity GetRootActivity(Activity activity)
        {
            if (activity == null)
            {
                throw new ArgumentNullException("activity");
            }
            while (activity.Parent != null)
            {
                activity = activity.Parent;
            }
            return activity;
        }

        internal static string GetRootNamespace(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }
            string rootNamespace = string.Empty;
            IWorkflowCompilerOptionsService service = (IWorkflowCompilerOptionsService)serviceProvider.GetService(typeof(IWorkflowCompilerOptionsService));
            if ((service != null) && (service.RootNamespace != null))
            {
                rootNamespace = service.RootNamespace;
            }
            return rootNamespace;
        }

        private static Guid GetRuntimeContextGuid(Activity currentActivity)
        {
            Activity parent = currentActivity;
            Guid guid = (Guid)parent.GetValue(Activity.ActivityContextGuidProperty);
            while ((guid == Guid.Empty) && (parent.Parent != null))
            {
                parent = parent.Parent;
                guid = (Guid)parent.GetValue(Activity.ActivityContextGuidProperty);
            }
            return guid;
        }

       

        internal static Activity[] GetTopLevelActivities(ICollection activities)
        {
            if (activities == null)
            {
                throw new ArgumentNullException("activities");
            }
            List<Activity> list = new List<Activity>();
            foreach (object obj2 in activities)
            {
                Activity activity = obj2 as Activity;
                if (activity != null)
                {
                    bool flag = false;
                    for (Activity activity2 = activity.Parent; (activity2 != null) && !flag; activity2 = activity2.Parent)
                    {
                        foreach (object obj3 in activities)
                        {
                            if (obj3 == activity2)
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                    if (!flag)
                    {
                        list.Add(activity);
                    }
                }
            }
            return list.ToArray();
        }

        internal static bool IsActivityLocked(Activity activity)
        {
            if (activity == null)
            {
                throw new ArgumentNullException("activity");
            }
            for (CompositeActivity activity2 = activity.Parent; activity2 != null; activity2 = activity2.Parent)
            {
                if (activity2.Parent == null)
                {
                    return false;
                }
                if (IsCustomActivity(activity2))
                {
                    return true;
                }
            }
            return false;
        }

        internal static bool IsAlternateFlowActivity(Activity activity)
        {
            if (activity == null)
            {
                return false;
            }
            bool flag = false;
            if (!activity.UserData.Contains(typeof(AlternateFlowActivityAttribute)))
            {
                flag = activity.GetType().GetCustomAttributes(typeof(AlternateFlowActivityAttribute), true).Length != 0;
                activity.UserData[typeof(AlternateFlowActivityAttribute)] = flag;
                return flag;
            }
            return (bool)activity.UserData[typeof(AlternateFlowActivityAttribute)];
        }

        internal static bool IsChildActivity(CompositeActivity parent, Activity activity)
        {
            foreach (Activity activity2 in parent.Activities)
            {
                if (activity == activity2)
                {
                    return true;
                }
                if ((activity2 is CompositeActivity) && IsChildActivity(activity2 as CompositeActivity, activity))
                {
                    return true;
                }
            }
            return false;
        }

        internal static bool IsCustomActivity(CompositeActivity compositeActivity)
        {
            if (compositeActivity == null)
            {
                throw new ArgumentNullException("compositeActivity");
            }
            if (compositeActivity.UserData.Contains(WFUserDataKeys.CustomActivity))
            {
                return (bool)compositeActivity.UserData[WFUserDataKeys.CustomActivity];
            }
            try
            {
                CompositeActivity activity = Activator.CreateInstance(compositeActivity.GetType()) as CompositeActivity;
                if ((activity != null) && (activity.Activities.Count > 0))
                {
                    compositeActivity.UserData[WFUserDataKeys.CustomActivityDefaultName] = activity.Name;
                    compositeActivity.UserData[WFUserDataKeys.CustomActivity] = true;
                    return true;
                }
            }
            catch
            {
            }
            compositeActivity.UserData[WFUserDataKeys.CustomActivity] = false;
            return false;
        }

        private static bool IsDeclaringActivityMatchesContext(Activity currentActivity, Activity context)
        {
            CompositeActivity compositeActivity = context as CompositeActivity;
            CompositeActivity declaringActivity = GetDeclaringActivity(currentActivity);
            if (IsActivityLocked(context) && ((compositeActivity == null) || !IsCustomActivity(compositeActivity)))
            {
                compositeActivity = GetDeclaringActivity(context);
            }
            return (compositeActivity == declaringActivity);
        }

        internal static bool IsFileNameValid(string fileName)
        {
            int num = Path.GetInvalidPathChars().GetLength(0) + 5;
            char[] array = new char[num];
            Path.GetInvalidPathChars().CopyTo(array, 0);
            array[num - 5] = ':';
            array[num - 4] = '?';
            array[num - 3] = '*';
            array[num - 2] = '/';
            array[num - 1] = '\\';
            return ((((fileName != null) && (fileName.Length != 0)) && (fileName.Length <= 260)) && (fileName.IndexOfAny(array) == -1));
        }

        public static bool IsFrameworkActivity(Activity activity)
        {
            return (((activity is CancellationHandlerActivity) || (activity is CompensationHandlerActivity)) || (activity is FaultHandlersActivity));
        }

        internal static string MergeNamespaces(string primaryNs, string secondaryNs)
        {
            string str = primaryNs;
            if ((secondaryNs != null) && (secondaryNs.Length > 0))
            {
                if ((str != null) && (str.Length > 0))
                {
                    str = str + "." + secondaryNs;
                }
                else
                {
                    str = secondaryNs;
                }
            }
            if (str == null)
            {
                str = string.Empty;
            }
            return str;
        }

        [DllImport("msi.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        private static extern int MsiGetProductInfoW(string szProduct, string szProperty, StringBuilder lpValueBuf, ref int pcchValueBuf);
        internal static IDictionary PairUpCommonParentActivities(ICollection activities)
        {
            if (activities == null)
            {
                throw new ArgumentNullException("activities");
            }
            Hashtable hashtable = new Hashtable();
            foreach (Activity activity in activities)
            {
                if (activity.Parent != null)
                {
                    ArrayList list = (ArrayList)hashtable[activity.Parent];
                    if (list == null)
                    {
                        list = new ArrayList();
                        hashtable.Add(activity.Parent, list);
                    }
                    list.Add(activity);
                }
            }
            return hashtable;
        }

        internal static Activity ParseActivity(Activity parsingContext, string activityName)
        {
            if (parsingContext == null)
            {
                throw new ArgumentNullException("parsingContext");
            }
            if (activityName == null)
            {
                throw new ArgumentNullException("activityName");
            }
            string id = activityName;
            string str2 = string.Empty;
            int index = activityName.IndexOf(".");
            if (index != -1)
            {
                id = activityName.Substring(0, index);
                str2 = activityName.Substring(index + 1);
                if (str2.Length == 0)
                {
                    return null;
                }
            }
            Activity containerActivity = GetActivity(parsingContext, id);
            if (containerActivity == null)
            {
                return null;
            }
            if (str2.Length > 0)
            {
                if (!(containerActivity is CompositeActivity) || !IsCustomActivity(containerActivity as CompositeActivity))
                {
                    return null;
                }
                string[] strArray = str2.Split(new char[] { '.' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    Activity activity = GetActivity(containerActivity, strArray[i]);
                    if ((activity == null) || !IsActivityLocked(activity))
                    {
                        return null;
                    }
                    CompositeActivity declaringActivity = GetDeclaringActivity(activity);
                    if (containerActivity != declaringActivity)
                    {
                        return null;
                    }
                    containerActivity = activity;
                }
                return containerActivity;
            }
            if (IsActivityLocked(containerActivity) && !IsDeclaringActivityMatchesContext(containerActivity, parsingContext))
            {
                return null;
            }
            return containerActivity;
        }

        internal static Activity ParseActivityForBind(Activity context, string activityName)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (activityName == null)
            {
                throw new ArgumentNullException("activityName");
            }
            if (string.Equals(activityName, "/Self", StringComparison.Ordinal))
            {
                return context;
            }
            if (activityName.StartsWith("/Parent", StringComparison.OrdinalIgnoreCase))
            {
                Activity activity = context;
                string[] strArray = activityName.Split(new char[] { '/' }, 1);
                for (int i = 0; (i < strArray.Length) && (activity != null); i++)
                {
                    activity = string.Equals(strArray[i].Trim(), "Parent", StringComparison.OrdinalIgnoreCase) ? activity.Parent : null;
                }
                return activity;
            }
            if (!IsActivityLocked(context))
            {
                Activity activityByName = null;
                Activity activity8 = context;
                bool flag = false;
                CompositeActivity compositeActivity = activity8 as CompositeActivity;
                if (((compositeActivity != null) && (activity8.Parent != null)) && IsCustomActivity(compositeActivity))
                {
                    flag = true;
                    activity8 = activity8.Parent;
                }
                while ((activityByName == null) && (activity8 != null))
                {
                    activityByName = activity8.GetActivityByName(activityName, true);
                    activity8 = activity8.Parent;
                }
                if (flag && (activityByName == null))
                {
                    activityByName = context.GetActivityByName(activityName, true);
                }
                if (activityByName != null)
                {
                    return activityByName;
                }
                return ParseActivity(GetRootActivity(context), activityName);
            }
            Activity activity2 = null;
            Activity declaringActivity = GetDeclaringActivity(context);
            Guid runtimeContextGuid = GetRuntimeContextGuid(context);
            Guid guid2 = GetRuntimeContextGuid(declaringActivity);
            Activity parsingContext = context;
            Activity parent = context.Parent;
            Guid guid3 = GetRuntimeContextGuid(parent);
            while ((activity2 == null) && (guid2 != runtimeContextGuid))
            {
                while ((parent != null) && (guid3 == runtimeContextGuid))
                {
                    parsingContext = parent;
                    parent = parent.Parent;
                    guid3 = GetRuntimeContextGuid(parent);
                }
                activity2 = ParseActivity(parsingContext, activityName);
                runtimeContextGuid = guid3;
            }
            if (activity2 == null)
            {
                activity2 = ParseActivity(declaringActivity, activityName);
            }
            if (activity2 == null)
            {
                if (!declaringActivity.UserData.Contains(WFUserDataKeys.CustomActivityDefaultName))
                {
                    Activity activity6 = Activator.CreateInstance(declaringActivity.GetType()) as Activity;
                    declaringActivity.UserData[WFUserDataKeys.CustomActivityDefaultName] = activity6.Name;
                }
                if (((string)declaringActivity.UserData[WFUserDataKeys.CustomActivityDefaultName]) == activityName)
                {
                    activity2 = declaringActivity;
                }
            }
            return activity2;
        }





        internal static string GenerateQualifiedNameForLockedActivity(Activity activity, string id)
        {
            StringBuilder builder = new StringBuilder();
            string str = string.IsNullOrEmpty(id) ? activity.Name : id;
            CompositeActivity declaringActivity = WFHelpers.GetDeclaringActivity(activity);
            if (declaringActivity != null)
            {
                builder.Append(declaringActivity.QualifiedName).Append(".").Append(str);
            }
            else
            {
                builder.Append(str);
            }
            return builder.ToString();
        }

        internal static void UpdateSiteName(Activity activity, string newID)
        {
            if (activity == null)
            {
                throw new ArgumentException("activity");
            }
            string str = newID;
            if (WFHelpers.IsActivityLocked(activity))
            {
                str = GenerateQualifiedNameForLockedActivity(activity, newID);
            }
            activity.Site.Name = str;
            if (activity is CompositeActivity)
            {
                foreach (Activity activity2 in WFHelpers.GetNestedActivities(activity as CompositeActivity))
                {
                    if (WFHelpers.IsActivityLocked(activity2))
                    {
                        Activity declaringActivity = WFHelpers.GetDeclaringActivity(activity2);
                        activity2.Site.Name = declaringActivity.Site.Name + "." + activity2.Name;
                    }
                }
            }
        }
    }
}
