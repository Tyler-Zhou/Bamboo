using System;
using System.Collections.Generic;
using System.Reflection;

namespace ICP.Operation.Common.ServiceInterface
{
    public class GetBillInfoFactory : IGetBillInfoFactory
    {
        static Dictionary<string, IGetRefNoCommand> dicGetRefCommands = new Dictionary<string, IGetRefNoCommand>();
        static Dictionary<string, IGetDescriptionCommand> dicGetDescriptionCommands = new Dictionary<string, IGetDescriptionCommand>();
        public IGetRefNoCommand GetRefNoCommand(object billInfo)
        {
            
            return GetCommand<IGetRefNoCommand>(dicGetRefCommands, billInfo, "GetRefNoCommand");

        }


        public IGetDescriptionCommand GetDescriptionCommand(object billInfo)
        {
            return GetCommand<IGetDescriptionCommand>(dicGetDescriptionCommands, billInfo, "GetDescriptionCommand");
        }

        private T GetCommand<T>(Dictionary<string, T> commands, object billInfo, string suffix)
        {
            string key = billInfo.GetType().Name;// + "GetRefNoCommand";
            if (commands.ContainsKey(key))
                return commands[key];
            else
            {
                string typeName = key + suffix;
                string assemblyFullName = Assembly.GetExecutingAssembly().GetName().FullName;
                string assemblyName = assemblyFullName.Substring(0, assemblyFullName.IndexOf(","));
                Type type = Type.GetType(string.Format("{0}.{1},{0}", assemblyName, typeName));
                T command = (T)Activator.CreateInstance(type);
                commands[key] = command;
                return command;
            }
        }

    }
}
