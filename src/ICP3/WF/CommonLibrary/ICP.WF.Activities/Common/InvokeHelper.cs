using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Workflow.ComponentModel;
using System.Reflection.Emit;
using ICP.WF.ServiceInterface;
using ICP.Framework.CommonLibrary;

namespace ICP.WF.Activities.Common
{
    internal static class InvokeHelper
    {
        internal static object CloneOutboundValue(object source, BinaryFormatter formatter, string name)
        {
            if ((source == null) || source.GetType().IsValueType)
            {
                return source;
            }
            ICloneable cloneable = source as ICloneable;
            if (cloneable != null)
            {
                return cloneable.Clone();
            }
            MemoryStream serializationStream = new MemoryStream(0x400);
            try
            {
                formatter.Serialize(serializationStream, source);
            }
            catch (SerializationException exception)
            {
                throw new InvalidOperationException(SR.GetString("Error_CallExternalMethodArgsSerializationException", new object[] { name }), exception);
            }
            serializationStream.Position = 0L;
            return formatter.Deserialize(serializationStream);
        }

        internal static object[] GetParameters(MethodBase methodBase, ParameterCollection parameterBindings, System.Collections.Generic.Dictionary<string, object> paramerterValues)
        {
            ParameterInfo[] parameters = methodBase.GetParameters();
            object[] objArray = new object[parameters.Length];
            int index = 0;
            foreach (ParameterInfo info in parameters)
            {
                if (parameterBindings.Contain(info.Name))
                {
                    ParameterData binding = parameterBindings[info.Name];
                    objArray[index] = binding.GetValue(paramerterValues);
                }
                index++;
            }
            return objArray;
        }

        internal static object[] GetParameters(MethodBase methodBase, ParameterCollection parameterBindings, System.Collections.Generic.Dictionary<string, object> paramerterValues, out ParameterModifier[] parameterModifiers)
        {
            ParameterInfo[] parameters = methodBase.GetParameters();
            object[] objArray = new object[parameters.Length];
            if (objArray.Length == 0)
            {
                parameterModifiers = new ParameterModifier[0];
                return objArray;
            }
            int index = 0;
            BinaryFormatter formatter = null;
            ParameterModifier modifier = new ParameterModifier(objArray.Length);
            foreach (ParameterInfo info in parameters)
            {
                if (info.ParameterType.IsByRef)
                {
                    modifier[index] = true;
                }
                else
                {
                    modifier[index] = false;
                }
                if (parameterBindings.Contain(info.Name))
                {
                    ParameterData binding = parameterBindings[info.Name.ToUpper()];
                    if (formatter == null)
                    {
                        formatter = new BinaryFormatter();
                    }
                    //objArray[index] =Convert.ChangeType(CloneOutboundValue(binding.GetValue(paramerterValues), formatter, info.Name),info.ParameterType);
                    try
                    {
                        objArray[index] = ChangeType(CloneOutboundValue(binding.GetValue(paramerterValues), formatter, info.Name), info.ParameterType);
                    }
                    catch (Exception ex)
                    {
                        string tip = "Data conversion exception\r\n";
                        tip += "\r\nKey: " + info.Name;
                        tip += "\r\nValue: " + binding.GetValue(paramerterValues) + "\r\n\r\n" + ex.Message;
                        throw new Exception(tip);
                    }
                }
                index++;
            }
            ParameterModifier[] modifierArray = new ParameterModifier[] { modifier };
            parameterModifiers = modifierArray;
            return objArray;
        }

        internal static object ChangeType(object sourceob, Type destType)
        {
            if (sourceob == null || sourceob == System.DBNull.Value) return null;

            if (sourceob.GetType().IsArray == false)
            {
                if (destType == typeof(Guid) || destType == typeof(Guid?))
                {
                    return new Guid(sourceob.ToString());
                }
                else
                {
                    return Convert.ChangeType(sourceob, destType);
                }
            }
            else
            {
                return ChangeArrayType(sourceob, destType);
            }
        }

        internal static object ChangeArrayType(object val, Type desttype)
        {

            if (desttype.IsArray == false) return val;

            try
            {
                object[] obs = val as object[];

                if (desttype.Equals(typeof(Guid[])))
                {
                    if (val == null) return val;
                    List<Guid> ls = new List<Guid>();
                    foreach (object b in obs)
                    {
                        ls.Add(DataTypeHelper.GetGuid(b, Guid.Empty));
                    }
                    return ls.ToArray();
                }
                else if (desttype.Equals(typeof(Guid?[])))
                {
                    if (val == null) return val;
                    List<Guid?> ls = new List<Guid?>();
                    foreach (object b in obs)
                    {
                        ls.Add(Convert.ChangeType(b, typeof(Guid?)) as Guid?);
                    }

                    return ls.ToArray();
                }
                else if (desttype.Equals(typeof(string[])))
                {
                    if (val == null) return val;
                    List<string> ls = new List<string>();
                    foreach (object b in obs)
                    {
                        ls.Add(Convert.ToString(b));
                    }

                    return ls.ToArray();
                }
                else if (desttype.Equals(typeof(decimal[])))
                {
                    if (val == null) return val;
                    List<decimal> ls = new List<decimal>();
                    foreach (object b in obs)
                    {
                        ls.Add(DataTypeHelper.GetDecimal(b, 0));
                    }

                    return ls.ToArray();
                }
                else if (desttype.Equals(typeof(decimal?[])))
                {
                    if (val == null) return val;
                    List<decimal?> ls = new List<decimal?>();
                    foreach (object b in obs)
                    {
                        ls.Add(Convert.ChangeType(b, typeof(decimal?)) as decimal?);
                    }

                    return ls.ToArray();
                }
                else if (desttype.Equals(typeof(DateTime[])))
                {
                    if (val == null) return val;
                    List<DateTime> ls = new List<DateTime>();
                    foreach (object b in obs)
                    {
                        ls.Add(Convert.ToDateTime(b));
                    }

                    return ls.ToArray();
                }
                else if (desttype.Equals(typeof(DateTime?[])))
                {
                    if (val == null) return val;
                    List<DateTime?> ls = new List<DateTime?>();
                    foreach (object b in obs)
                    {
                        ls.Add(Convert.ChangeType(b, typeof(DateTime?)) as DateTime?);
                    }

                    return ls.ToArray();
                }
                else if (desttype.Equals(typeof(int[])))
                {
                    if (val == null) return val;
                    List<int> ls = new List<int>();
                    foreach (object b in obs)
                    {
                        ls.Add(DataTypeHelper.GetInt(b, 0));
                    }

                    return ls.ToArray();
                }
                else if (desttype.Equals(typeof(int?[])))
                {
                    if (val == null) return val;
                    List<int?> ls = new List<int?>();
                    foreach (object b in obs)
                    {
                        ls.Add(Convert.ChangeType(b, typeof(int?)) as int?);
                    }

                    return ls.ToArray();
                }

                else if (desttype.Equals(typeof(short[])))
                {
                    if (val == null) return val;
                    List<short> ls = new List<short>();
                    foreach (object b in obs)
                    {
                        ls.Add(Convert.ToInt16(b));
                    }

                    return ls.ToArray();
                }
                else if (desttype.Equals(typeof(short?[])))
                {
                    if (val == null) return val;
                    List<short?> ls = new List<short?>();
                    foreach (object b in obs)
                    {
                        ls.Add(Convert.ChangeType(b, typeof(short?)) as short?);
                    }

                    return ls.ToArray();
                }

                return null;
            }
            catch
            {
                return val;
            }
        }


        //internal static void InitializeParameters(MethodInfo methodBase, ParameterCollection parameterBindings)
        //{
        //    foreach (ParameterInfo info in methodBase.GetParameters())
        //    {
        //        if (!parameterBindings.Contain(info.Name))
        //        {
        //            parameterBindings.Add(new ParameterData(info.Name));
        //        }
        //    }
        //    if ((methodBase.ReturnType != typeof(void)) && !parameterBindings.Contains("(ReturnValue)"))
        //    {
        //        parameterBindings.Add(new ParameterData("(ReturnValue)"));
        //    }
        //}

        //internal static void SaveOutRefParameters(object[] actualParameters, MethodBase methodBase, ParameterCollection parameterBindings)
        //{
        //    int index = 0;
        //    BinaryFormatter formatter = null;
        //    foreach (ParameterInfo info in methodBase.GetParameters())
        //    {
        //        if (parameterBindings.Contain(info.Name) 
        //            && (info.ParameterType.IsByRef || (info.IsIn && info.IsOut)))
        //        {
        //            ParameterData binding = parameterBindings[info.Name];
        //            if (formatter == null)
        //            {
        //                formatter = new BinaryFormatter();
        //            }
        //            binding.Value = CloneOutboundValue(actualParameters[index], formatter, info.Name);
        //        }
        //        index++;
        //    }
        //}
    }
}
