using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ICP.WF.ServiceInterface.DataObject;
namespace ICP.WF.Activities.Common
{
    /// <summary>
    /// 参数对象
    /// </summary>
    [Serializable]
	public class ParameterData
	{


        Type parameterType;
        /// <summary>
        /// 参数类型
        /// </summary>
        public Type ParameterType
        {
            get { return parameterType; }
            set { parameterType = value; }
        }

        string parameterName;
        /// <summary>
        /// 参数名
        /// </summary>
        public string ParameterName
        {
            get { return parameterName; }
            set { parameterName = value; }
        }

        string parameterOriginalValue;
        /// <summary>
        /// 参数对应的表单的列名
        /// </summary>
        public string ParameterOriginalValue
        {
            get { return parameterOriginalValue; }
            set { parameterOriginalValue = value; }
        }

     

        string parameterDesc;
        /// <summary>
        /// 参数描述
        /// </summary>
        public string ParameterDesc
        {
            get
            {
                if (string.IsNullOrEmpty(parameterDesc))
                {
                    return parameterName;
                }
                else
                {
                    return parameterDesc;
                }
            }
            set { parameterDesc = value; }
        }

        string aliasName;
        /// <summary>
        /// 参数别名
        /// </summary>
        public string AliasName
        {
            get
            {
                if (string.IsNullOrEmpty(aliasName))
                {
                    return parameterName;
                }
                else
                {
                    return aliasName;
                }
            }
            set { aliasName = value; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(SR.GetString("Name", "名称"));
            sb.Append(":");
            sb.Append(parameterName);
            sb.Append(System.Environment.NewLine);

            sb.Append(SR.GetString("Desc", "描述"));
            sb.Append(":");
            sb.Append(parameterDesc);
            sb.Append(System.Environment.NewLine);

            return sb.ToString();
        }

        public object GetValue( Dictionary<string, object> values)
        {
            if (values != null && values.ContainsKey(this.ParameterOriginalValue))
            {
                return values[this.ParameterOriginalValue];
            }
            else
            {
                return null;
            }
        }
	}


    [Serializable]
    public sealed class ParameterCollection : System.Collections.ObjectModel.Collection<ParameterData>
    {

        protected override void ClearItems()
        {
            base.ClearItems();
        }

        public ParameterData this[string name]
        {
            get
            {
                foreach (ParameterData d in this.Items)
                {
                    if (d.ParameterName.ToUpper().Equals(name.ToUpper()))
                    {
                        return d;
                    }
                }

                return null;
            }
            set
            {
                ParameterData pd = null;
                foreach (ParameterData d in this.Items)
                {
                    if (d.ParameterName.ToUpper().Equals(name.ToUpper()))
                    {
                        pd = d;
                    }
                }

                if (pd != null)
                {
                    pd = value;
                }
            }
        }

        public bool Contain(string name)
        {
            foreach (ParameterData d in this.Items)
            {
                if (d.ParameterName.ToUpper().Equals(name.ToUpper()))
                {
                    return true;
                }
            }

            return false;
        }


        protected override void InsertItem(int index, ParameterData item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            if (base.Contains(item))
            {
                this.Remove(item);
            }
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, ParameterData item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
   
            base.SetItem(index, item);
        }
    }

    /// <summary>
    /// 方法数据对象
    /// </summary>
    [Serializable]
    public class MethodData
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 父节点ID
        /// </summary>
        public int ParentID { get; set; }
        string metodName;
        /// <summary>
        /// 方法名
        /// </summary>
        public string MetodName
        {
            get { return metodName; }
            set { metodName = value; }
        }


        ParameterCollection parameters = new ParameterCollection();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ParameterCollection Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        string methodDesc;
        /// <summary>
        /// 方法描述
        /// </summary>
        public string MethodDesc
        {
            get
            {
                if (string.IsNullOrEmpty(methodDesc))
                {
                    return metodName;
                }
                else
                {
                    return methodDesc;
                }
            }
            set { methodDesc = value; }
        }

        string aliasName;
        /// <summary>
        /// 别名
        /// </summary>
        public string AliasName
        {
            get
            {
                if (string.IsNullOrEmpty(aliasName))
                {
                    return metodName;
                }
                else
                {
                    return aliasName;
                }
            }
            set { aliasName = value; }
        }

        public override string ToString()
        {
            return metodName;
        }

        public string ToDescString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(SR.GetString( "Name","名称"));
            sb.Append(":");
            sb.Append(metodName);
            sb.Append(System.Environment.NewLine);

            sb.Append(SR.GetString( "Desc","描述"));
            sb.Append(":");
            sb.Append(methodDesc);
            sb.Append(System.Environment.NewLine);

            return sb.ToString();
        }
    }

    [Serializable]
    public class ParameterSourceData
    {
        public ParameterSourceData(string _id,string name,string ename, Type[] types)
        {
            id = _id;
            parameterName = name;
            EName = ename;
            parameterType = types;
        }

        private string id;
        /// <summary>
        /// ID
        /// </summary>
        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        string eName;
        /// <summary>
        /// 英文名
        /// </summary>
        public string EName
        {
            get
            {
                return eName;
            }
            set
            {
                eName = value;
            }
        }

        string parameterName;
        /// <summary>
        /// 中文名
        /// </summary>
        public string ParameterName
        {
            get { return parameterName; }
            set { parameterName = value; }
        }

        Type[] parameterType;
        public Type[] ParameterTypes
        {
            get { return parameterType; }
            set { parameterType = value; }
        }

        public  static List<ParameterSourceData> GetList()
        {
            List<ParameterSourceData> ps = new List<ParameterSourceData>();

            ps.Add(new ParameterSourceData(WWFConstants.WorkflowIdCode, WWFConstants.WorkflowId_C, WWFConstants.WorkflowId_E, CanConvertType(typeof(Guid))));
            ps.Add(new ParameterSourceData(WWFConstants.WorkflowNameCode, WWFConstants.WorkflowName_C, WWFConstants.WorkflowName_E, CanConvertType(typeof(string))));
            ps.Add(new ParameterSourceData(WWFConstants.WorkNameCode, WWFConstants.WorkName_C, WWFConstants.WorkName_E, CanConvertType(typeof(string))));
            ps.Add(new ParameterSourceData(WWFConstants.WorkflowNoCode, WWFConstants.WorkflowNo_C, WWFConstants.WorkflowNo_E, CanConvertType(typeof(string))));
            ps.Add(new ParameterSourceData(WWFConstants.ProposerCompanyIDCode, WWFConstants.ProposerCompanyId_C, WWFConstants.ProposerCompanyId_E, CanConvertType(typeof(Guid))));
            ps.Add(new ParameterSourceData(WWFConstants.ProposerDepartmentIDCode, WWFConstants.ProposerDepartmentId_C, WWFConstants.ProposerDepartmentId_E, CanConvertType(typeof(Guid))));           
            ps.Add(new ParameterSourceData(WWFConstants.ProposerIDCode, WWFConstants.ProposerID_C, WWFConstants.ProposerID_E, CanConvertType(typeof(Guid))));

            return ps;

        }

      public  static Type[] CanConvertType(Type type)
        {
            List<Type> types = new List<Type>();
            if (type == typeof(Guid) || type == typeof(Guid?))
            {
                types.Add(typeof(Guid?));
                types.Add(typeof(Guid));
                types.Add(typeof(Guid?[]));
                types.Add(typeof(Guid[]));
            }
            else if (type == typeof(decimal) || type == typeof(decimal?))
            {
                types.Add(typeof(decimal?));
                types.Add(typeof(decimal));
                types.Add(typeof(decimal?[]));
                types.Add(typeof(decimal[]));
            }
            else if (type == typeof(int) || type == typeof(int?))
            {
                types.Add(typeof(int?));
                types.Add(typeof(int));
                types.Add(typeof(int?[]));
                types.Add(typeof(int[]));
            }
            else if (type == typeof(short) || type == typeof(short?))
            {
                types.Add(typeof(short?));
                types.Add(typeof(short));
                types.Add(typeof(short?[]));
                types.Add(typeof(short[]));
            }
            else if (type == typeof(long) || type == typeof(long?))
            {
                types.Add(typeof(long?));
                types.Add(typeof(long));
                types.Add(typeof(long?[]));
                types.Add(typeof(long[]));
            }
            else if (type == typeof(bool) || type == typeof(bool?))
            {
                types.Add(typeof(bool?));
                types.Add(typeof(bool));
                types.Add(typeof(bool?[]));
                types.Add(typeof(bool[]));
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                types.Add(typeof(DateTime?));
                types.Add(typeof(DateTime));
                types.Add(typeof(DateTime?[]));
                types.Add(typeof(DateTime[]));
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                types.Add(typeof(DateTime?));
                types.Add(typeof(DateTime));
                types.Add(typeof(DateTime?[]));
                types.Add(typeof(DateTime[]));
            }
            else if (type == typeof(double) || type == typeof(double?))
            {
                types.Add(typeof(double?));
                types.Add(typeof(double));
                types.Add(typeof(double?[]));
                types.Add(typeof(double[]));
            }
            else if (type == typeof(string))
            {
                types.Add(typeof(string));
                types.Add(typeof(string[]));
            }
            else if (type == typeof(byte) || type == typeof(byte?))
            {
                types.Add(typeof(byte?));
                types.Add(typeof(byte));
                types.Add(typeof(byte?[]));
                types.Add(typeof(byte[]));
            }

            return types.ToArray();
        }

    }

  
}
