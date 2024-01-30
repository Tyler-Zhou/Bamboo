//---------------------------------------------------------------------
//  This file is part of the WindowsWorkflow.NET web site samples.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
// 
//  This source code is intended only as a supplement to Microsoft
//  Development Tools and/or on-line documentation.  See these other
//  materials for detailed information regarding Microsoft code samples.
// 
//  THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
//---------------------------------------------------------------------

using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.ComponentModel.Design;
using System.Reflection;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Design;
using Microsoft.CSharp;
namespace ICP.WF.WorkFlowDesigner
{

    /// <summary>
    /// 定义用于在设计时动态创建、更新和移除某一类型的代码元素的方法。还提供了用于在设计时显示代码的方法。
    /// </summary>
    internal class MemberCreationService : IMemberCreationService
    {
        #region 本地变量

        private const string DependencyPropertyInit_CS = "DependencyProperty.Register(\"{0}\", typeof({1}), typeof({2}){3})";
        private const string DependencyPropertyOption = ", new PropertyMetadata({0})";

        private WorkflowLoader loader = null;
        private IServiceProvider serviceProvider = null;
        private CodeDomProvider provider = null;
        protected const BindingFlags baseMemberBindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.FlattenHierarchy;

        private WorkflowLoader Loader
        {
            get
            {
                return this.loader;
            }
        }
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.serviceProvider;
            }
        }
        private CodeDomProvider CodeProvider
        {
            get
            {
                return this.provider;
            }
        }
        private Activity RootActivity
        {
            get
            {
                IDesignerHost host = this.ServiceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
                if (host == null)
                {
                    throw new InvalidOperationException(typeof(IDesignerHost).FullName);
                }

                return host.RootComponent as Activity;
            }
        }

        #endregion

        #region 构造函数

        internal MemberCreationService(IServiceProvider serviceProvider, WorkflowLoader loader)
        {
            this.serviceProvider = serviceProvider;
            this.loader = loader;
            this.provider = new CSharpCodeProvider();
        }

        #endregion

        #region IMemberCreationService 接口成员实现
        /// <summary>
        /// 在指定类上使用指定的字段名、字段类型、参数类型、属性和文本初始化表达式创建一个字段。
        /// </summary>
        /// <param name="className">类名</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="fieldType">字段类型</param>
        /// <param name="genericParameterTypes">与字段关联的任何参数的 Type 数组。</param>
        /// <param name="attributes">用于定义字段的成员属性标识符。</param>
        /// <param name="initializationExpression">一个 CodeSnippetExpression，其中包含字段的文本表达式</param>
        /// <param name="overwriteExisting">为 true 则在创建新字段时删除 className 上的任何现有字段；否则为 false。</param>
        public void CreateField(string className, string fieldName, Type fieldType, Type[] genericParameterTypes, MemberAttributes attributes, CodeSnippetExpression initializationExpression, bool overwriteExisting)
        {
            if (string.IsNullOrEmpty(className))
            {
                throw new ArgumentNullException("className");
            }
            if (string.IsNullOrEmpty(fieldName))
            {
                throw new ArgumentNullException("fieldName");
            }
            if (fieldType == null)
            {
                throw new ArgumentNullException("fieldType");
            }
            if (fieldName == null)
            {
                throw new ArgumentNullException("fieldName");
            }

            if (!this.CodeProvider.IsValidIdentifier(fieldName))
            {
                throw new ArgumentException();
            }

            if (DoesFieldExist(className, fieldName))
            {
                if (overwriteExisting == true)
                {
                    RemoveField(className, fieldName);
                }
                else
                {
                    throw new Exception("Error_DifferentTypeFieldExists");
                }
            }

            Type type = null;
            if ((genericParameterTypes != null) && (fieldType.IsGenericTypeDefinition))
            {
                type = fieldType.MakeGenericType(genericParameterTypes);
            }
            else
            {
                type = fieldType;
            }

            CodeMemberField field = new CodeMemberField();
            field.Name = fieldName;
            field.Type = GetCodeTypeReference(className, type);
            field.UserData["UserVisible"] = true;
            field.Attributes = attributes;

            if (initializationExpression == null)
            {
                string formattedType = FormatType(type);
                if (type.GetConstructor(Type.EmptyTypes) != null)
                {
                    field.InitExpression = new CodeSnippetExpression("new " + formattedType + "()");
                }
                else
                {
                    field.InitExpression = new CodeSnippetExpression("default(" + formattedType + ")");
                }
            }
            else
            {
                field.InitExpression = initializationExpression;
            }

            string nsName = null;
            Helpers.GetNamespaceAndClassName(className, out nsName, out className);

            CodeTypeDeclaration typeDeclaration = GetCodeTypeDeclFromCodeCompileUnit(nsName, className);

            int index = 0;
            foreach (CodeTypeMember member in typeDeclaration.Members)
            {
                if (member is CodeMemberField)
                {
                    index++;
                }
                else
                {
                    break;
                }
            }

            typeDeclaration.Members.Insert(index, field);
            field.Type.UserData[typeof(Type)] = fieldType;
        }

        /// <summary>
        /// 移出字段
        /// </summary>
        /// <param name="className">类名</param>
        /// <param name="fieldName">字段名</param>
        public void RemoveField(string className, string fieldName)
        {
            if (string.IsNullOrEmpty(className))
            {
                throw new ArgumentNullException("className");
            }
            if (string.IsNullOrEmpty(fieldName))
            {
                throw new ArgumentNullException("fieldName");
            }

            string nsName = null;
            Helpers.GetNamespaceAndClassName(className, out nsName, out className);

            CodeTypeDeclaration typeDeclaration = GetCodeTypeDeclFromCodeCompileUnit(nsName, className);
            CodeTypeMemberCollection fields = typeDeclaration.Members;

            CodeMemberField fieldToRemove = null;
            if (fields != null)
            {
                foreach (CodeTypeMember member in fields)
                {
                    if (member is CodeMemberField)
                    {
                        CodeMemberField field = (CodeMemberField)member;
                        if (field.Name == fieldName)
                        {
                            fieldToRemove = field;
                        }
                        else if (String.Compare(field.Name, fieldName, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            fieldToRemove = field;
                        }
                    }
                }

                if (fieldToRemove != null)
                {
                    fields.Remove(fieldToRemove);
                }
            }
            if (fieldToRemove == null)
            {
                throw new Exception(fieldName);
            }

            //ITypeProvider typeProvider = this.ServiceProvider.GetService(typeof(ITypeProvider)) as ITypeProvider;
            //if (typeProvider == null)
            //{
            //    throw new InvalidOperationException(typeof(ITypeProvider).FullName);
            //}
            // ((TypeProvider)typeProvider).RefreshCodeCompileUnit(this.loader.CodeBesideCCU, new EventHandler(RefreshCCU));

        }


        /// <summary>
        /// 在指定类上使用指定的属性 (property) 名、属性 (property) 类型和属性 (attribute) 创建一个属性 (property)。
        /// </summary>
        /// <param name="className">一个字符串，用于定义类的名称以添加字段。</param>
        /// <param name="propertyName">一个字符串，用于定义字段的名称。</param>
        /// <param name="propertyType">新属性的 Type。</param>
        /// <param name="attributes">一个 AttributeInfo 数组，其中包含有关要分配给属性 (property) 的所有属性 (attribute) 的信息</param>
        /// <param name="emitDependencyProperty">为 true 则发出任何与属性关联的依赖项属性；否则为 false</param>
        /// <param name="isMetaProperty">为 true 则作为元属性来创建属性；否则为 false</param>
        /// <param name="isAttached">为 true 则指示发出的依赖项属性的 IsAttached 属性设置为 true；否则为 false</param>
        /// <param name="ownerType">声明依赖项属性的 Type</param>
        /// <param name="isReadOnly">为 true 则创建的属性作为只读属性；为 false 则创建的属性作为读写属性</param>
        public void CreateProperty(string className, string propertyName, Type propertyType, AttributeInfo[] attributes, bool emitDependencyProperty, bool isMetaProperty, bool isAttached, Type ownerType, bool isReadOnly)
        {
            if (string.IsNullOrEmpty(className))
            {
                throw new ArgumentNullException("className");
            }
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }
            if (propertyType == null)
            {
                throw new ArgumentNullException("propertyType");
            }

            if (!this.CodeProvider.IsValidIdentifier(propertyName))
            {
                throw new ArgumentException("Invalid Identifier");
            }

            if (!this.DoesPropertyExist(className, propertyName, propertyType))
            {
                CodeMemberProperty property = new CodeMemberProperty();
                property.Name = propertyName;
                property.Type = GetCodeTypeReference(className, propertyType);
                property.Attributes = MemberAttributes.Public | MemberAttributes.Final;

                if (attributes != null)
                {
                    foreach (AttributeInfo attribute in attributes)
                    {
                        CodeTypeReference attributeTypeRef = GetCodeTypeReference(className, attribute.AttributeType);
                        CodeAttributeDeclaration attribDecl = new CodeAttributeDeclaration(attributeTypeRef);
                        foreach (object param in attribute.ArgumentValues)
                        {
                            if (param is CodeExpression)
                            {
                                attribDecl.Arguments.Add(new CodeAttributeArgument(param as CodeExpression));
                            }
                        }
                        property.CustomAttributes.Add(attribDecl);
                    }
                }

                IDesignerHost host = this.ServiceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
                if (host == null)
                {
                    throw new InvalidOperationException(typeof(IDesignerHost).FullName);
                }

                ITypeProvider typeProvider = this.ServiceProvider.GetService(typeof(ITypeProvider)) as ITypeProvider;
                if (typeProvider == null)
                {
                    throw new InvalidOperationException(typeof(ITypeProvider).FullName);
                }

                string fieldName = null;
                if (emitDependencyProperty)
                {
                    fieldName = propertyName + "Property";
                    property.UserData["_vsDependencyPropertyFieldKey"] = fieldName;
                    if (!isAttached)
                    {
                        CreateStaticFieldForDependencyProperty(className, propertyName, propertyType, fieldName, isMetaProperty, false);
                    }
                }
                else
                {
                    bool existingField = false;
                    fieldName = GeneratePropertyAssociatedFieldName(className, propertyName, propertyType, out existingField);
                    if (!existingField)
                    {
                        CreateField(className, fieldName, propertyType, null, MemberAttributes.Private, null, true);
                    }
                }

                if (emitDependencyProperty)
                {
                    CodeFieldReferenceExpression fieldRef = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(isAttached ? ownerType.FullName : className), fieldName);
                    property.HasGet = true;
                    CodeTypeReference typeRef = new CodeTypeReference(propertyType);
                    property.GetStatements.Add(new CodeMethodReturnStatement(new CodeCastExpression(typeRef, new CodeMethodInvokeExpression(new CodeBaseReferenceExpression(), "GetValue", fieldRef))));
                    if (!isReadOnly)
                    {
                        property.HasSet = true;
                        property.SetStatements.Add(new CodeMethodInvokeExpression(new CodeBaseReferenceExpression(), "SetValue", fieldRef, new CodeSnippetExpression("value")));
                    }
                }
                else
                {
                    property.HasGet = true;
                    property.GetStatements.Add(new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName)));

                    if (!isReadOnly)
                    {
                        property.HasSet = true;
                        CodeExpression ifNOTDesignModeExpression = new CodeBinaryOperatorExpression(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "DesignMode"), CodeBinaryOperatorType.ValueEquality, new CodePrimitiveExpression(false));
                        property.SetStatements.Add(new CodeConditionStatement(ifNOTDesignModeExpression, new CodeThrowExceptionStatement(new CodeObjectCreateExpression(typeof(InvalidOperationException), new CodeExpression[] { }))));
                        property.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), fieldName), new CodeSnippetExpression("value")));
                    }
                }

                string nsName = null;
                Helpers.GetNamespaceAndClassName(className, out nsName, out className);

                CodeTypeDeclaration typeDeclaration = GetCodeTypeDeclFromCodeCompileUnit(nsName, className);

                int index = 0;
                foreach (CodeTypeMember member in typeDeclaration.Members)
                {
                    if (member is CodeMemberProperty)
                    {
                        index++;
                    }
                    else
                    {
                        break;
                    }
                }

                typeDeclaration.Members.Insert(index, property);
                // ((TypeProvider)typeProvider).RefreshCodeCompileUnit(this.loader.CodeBesideCCU, new EventHandler(RefreshCCU));
            }
        }

        public void UpdateProperty(string className, string oldPropertyName, Type oldPropertyType, string newPropertyName, Type newPropertyType, AttributeInfo[] attributes, bool emitDependencyProperty, bool isMetaProperty)
        {
            return;
        }

        public void RemoveProperty(string className, string propertyName, Type propertyType)
        {
            return;
        }

        /// <summary>
        /// 用指定的事件名称、事件类型和属性在指定类上创建一个事件
        /// </summary>
        /// <param name="className">一个字符串，其中包含事件所要添加到的类名称</param>
        /// <param name="eventName">定义事件的名称的字符串</param>
        /// <param name="eventType">分配给事件的 Type</param>
        /// <param name="attributes">一个 AttributeInfo 数组，其中包含有关要分配给事件的所有属性的信息</param>
        /// <param name="emitDependencyProperty">为 true 则发出与事件关联的任何依赖项属性；否则为 false</param>
        public void CreateEvent(string className, string eventName, Type eventType, AttributeInfo[] attributes, bool emitDependencyProperty)
        {
            if (string.IsNullOrEmpty(className))
            {
                throw new ArgumentNullException("className");
            }
            if (string.IsNullOrEmpty(eventName))
            {
                throw new ArgumentNullException("eventName");
            }
            if (eventType == null)
            {
                throw new ArgumentNullException("eventType");
            }
            if (!this.CodeProvider.IsValidIdentifier(eventName))
            {
                throw new ArgumentException();
            }

            if (!this.DoesEventExist(className, eventName, eventType))
            {
                CodeMemberEvent eventInfo = new CodeMemberEvent();
                eventInfo.Name = eventName;
                eventInfo.Type = new CodeTypeReference(eventType);
                eventInfo.Type.UserData["_vsEventHandlerTypeKey"] = eventType;
                eventInfo.Attributes = MemberAttributes.Public | MemberAttributes.Final;

                if (attributes != null)
                {
                    foreach (AttributeInfo attribute in attributes)
                    {
                        CodeTypeReference attributeTypeRef = GetCodeTypeReference(className, attribute.AttributeType);
                        CodeAttributeDeclaration attribDecl = new CodeAttributeDeclaration(attributeTypeRef);
                        foreach (object param in attribute.ArgumentValues)
                        {
                            if (param is CodeExpression)
                            {
                                attribDecl.Arguments.Add(new CodeAttributeArgument(param as CodeExpression));
                            }
                        }
                        eventInfo.CustomAttributes.Add(attribDecl);
                    }
                }

                if (emitDependencyProperty)
                {
                    string fieldName = eventName + "Event";
                    CreateStaticFieldForDependencyProperty(className, eventName, eventType, fieldName, false, true);
                    eventInfo.UserData["_vsEventHandlerTypeKey"] = fieldName;
                }

                string nsName = null;
                Helpers.GetNamespaceAndClassName(className, out nsName, out className);

                CodeTypeDeclaration typeDeclaration = GetCodeTypeDeclFromCodeCompileUnit(nsName, className);

                int index = 0;
                foreach (CodeTypeMember member in typeDeclaration.Members)
                {
                    if (member is CodeMemberEvent)
                    {
                        index++;
                    }
                    else
                    {
                        break;
                    }
                }

                ITypeProvider typeProvider = this.ServiceProvider.GetService(typeof(ITypeProvider)) as ITypeProvider;
                if (typeProvider == null)
                {
                    throw new InvalidOperationException(typeof(ITypeProvider).FullName);
                }
                typeDeclaration.Members.Insert(index, eventInfo);
            }

        }

        public void UpdateEvent(string className, string oldEventName, Type oldEventType, string newEventName, Type newEventType, AttributeInfo[] attributes, bool emitDependencyProperty, bool isMetaProperty)
        {
            return;
        }

        public void RemoveEvent(string className, string eventName, Type eventType)
        {
            return;
        }

        public void ShowCode(Activity contextActivity, string methodName, Type delegateType)
        {
            return;
        }

        public void ShowCode()
        {
            throw new NotImplementedException();
        }

        public void ShowCode(string fileName)
        {
            return;
        }

        public void UpdateBaseType(string className, Type baseType)
        {
            return;
        }

        public void UpdateTypeName(string oldClassName, string newClassName)
        {

            return;
        }

        #endregion

        #region 本地方法

        private void RefreshCCU(object sender, EventArgs e)
        {
        }

        internal static string FormatType(Type type)
        {
            string typeName = string.Empty;
            if (type.IsArray)
            {
                typeName = FormatType(type.GetElementType());
                typeName += '[';
                typeName += new string(',', type.GetArrayRank() - 1);
                typeName += ']';
            }
            else
            {
                typeName = type.FullName;
                int indexOfSpecialChar = typeName.IndexOf('`');
                if (indexOfSpecialChar != -1)
                {
                    typeName = typeName.Substring(0, indexOfSpecialChar);
                }
                typeName = typeName.Replace('+', '.');

                if (type.ContainsGenericParameters || type.IsGenericType)
                {
                    Type[] genericArguments = type.GetGenericArguments();
                    typeName += '<';
                    bool first = true;
                    foreach (Type genericArgument in genericArguments)
                    {
                        if (!first)
                        {
                            typeName += ", ";
                        }
                        else
                        {
                            first = false;
                        }
                        typeName += FormatType(genericArgument);
                    }

                    typeName += '>';
                }
            }
            return typeName;
        }

        private string GeneratePropertyAssociatedFieldName(string className, string propertyName, Type propertyType, out bool exisitingField)
        {
            exisitingField = false;

            IIdentifierCreationService identCreationService = this.ServiceProvider.GetService(typeof(IIdentifierCreationService)) as IIdentifierCreationService;
            if (identCreationService == null)
            {
                throw new InvalidOperationException();
            }

            string baseFieldName = "_";
            if (propertyName.StartsWith("@"))
            {
                baseFieldName += propertyName.Substring(1);
            }
            else
            {
                baseFieldName += propertyName;
            }

            bool validIdent = false;
            int identCount = 1;
            string fieldName = String.Empty;
            while (!validIdent && identCount < Int32.MaxValue)
            {
                fieldName = baseFieldName + System.Convert.ToString(identCount);

                try
                {
                    identCreationService.ValidateIdentifier(RootActivity, fieldName);
                    validIdent = true;

                    if (DoesFieldExist(className, fieldName, propertyType))
                    {
                        exisitingField = true;
                    }
                    else if (DoesFieldExist(className, fieldName))
                    {
                        validIdent = false;
                    }
                }
                catch
                {
                    validIdent = false;
                }

                identCount += 1;
            }

            return fieldName;
        }

        private bool DoesFieldExist(string className, string fieldName)
        {
            return DoesFieldExist(className, fieldName, null);
        }

        private bool DoesFieldExist(string className, string fieldName, Type fieldType)
        {
            if (className == null || className.Length == 0)
            {
                return false;
            }

            ITypeProvider typeProvider = (ITypeProvider)this.ServiceProvider.GetService(typeof(ITypeProvider));
            if (typeProvider == null)
            {
                throw new Exception("Error_MissingTypeProvider");
            }

            Type typeDeclaration = typeProvider.GetType(className, true);
            foreach (FieldInfo member in typeDeclaration.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
            {
                if (fieldType == null || member.FieldType == fieldType)
                {
                    if (member.Name == fieldName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool DoesPropertyExist(string className, string propertyName, Type propertyType)
        {
            if (string.IsNullOrEmpty(className) || string.IsNullOrEmpty(propertyName) || propertyType == null)
            {
                return false;
            }

            ITypeProvider typeProvider = (ITypeProvider)this.ServiceProvider.GetService(typeof(ITypeProvider));
            if (typeProvider == null)
            {
                throw new Exception("Error_MissingTypeProvider");
            }

            Type typeDeclaration = typeProvider.GetType(className, true);
            foreach (PropertyInfo property in typeDeclaration.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                if (property.Name == propertyName && property.PropertyType == propertyType)
                {
                    return true;
                }
            }

            return false;
        }

        private bool DoesEventExist(string className, string eventName, Type eventHandlerType)
        {
            if (string.IsNullOrEmpty(className) || string.IsNullOrEmpty(eventName) || eventHandlerType == null)
            {
                return false;
            }

            ITypeProvider typeProvider = (ITypeProvider)this.ServiceProvider.GetService(typeof(ITypeProvider));
            if (typeProvider == null)
            {
                throw new Exception("Error_MissingTypeProvider");
            }

            Type typeDeclaration = typeProvider.GetType(className, true);
            foreach (EventInfo eventInfo in typeDeclaration.GetEvents(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                if (eventInfo.Name == eventName && eventInfo.EventHandlerType == eventHandlerType)
                {
                    return true;
                }
            }

            return false;
        }

        private CodeTypeReference GetCodeTypeReference(string className, Type type)
        {
            CodeTypeReference codeTypeReference = new CodeTypeReference();
            codeTypeReference.ArrayRank = 0;
            codeTypeReference.ArrayElementType = null;
            codeTypeReference.BaseType = type.FullName;
            return codeTypeReference;
        }

        private CodeTypeDeclaration GetCodeTypeDeclFromCodeCompileUnit(string nsName, string className)
        {
            return null;
        }

        private void CreateStaticFieldForDependencyProperty(string className, string propertyName, Type propertyType, string fieldName, bool isMetaProperty, bool isEvent)
        {
            //Emit the DependencyProperty.Register statement
            CodeSnippetExpression initExpression = (CodeSnippetExpression)CreateStaticFieldInitExpression(className, propertyName, propertyType.FullName, TypeProvider.IsAssignable(typeof(Delegate), propertyType), isMetaProperty, isEvent);
            CreateField(className, fieldName, typeof(DependencyProperty), null, MemberAttributes.Public | MemberAttributes.Static, initExpression, true);
        }

        private CodeExpression CreateStaticFieldInitExpression(string className, string propertyName, string propTypeName, bool isDelegateType, bool isMetaProperty, bool isEvent)
        {
            CodeSnippetExpression initExpression = new CodeSnippetExpression();
            const string DependencyPropertyInit_CS = "DependencyProperty.Register(\"{0}\", typeof({1}), typeof({2}){3})";
            const string DependencyPropertyOption = ", new PropertyMetadata({0})";

            string metaOptions = string.Empty;
            if (isMetaProperty)
            {
                metaOptions = "DependencyPropertyOptions.Metadata";
            }
            if (!isEvent && isDelegateType)
            {
                if (!string.IsNullOrEmpty(metaOptions))
                {
                    metaOptions += " | ";
                    metaOptions += "DependencyPropertyOptions.DelegateProperty";
                }
                else
                {
                    metaOptions = "DependencyPropertyOptions.DelegateProperty";
                }
            }

            if (!string.IsNullOrEmpty(metaOptions))
            {
                metaOptions = string.Format(DependencyPropertyOption, metaOptions);
            }


            initExpression.Value = String.Format(DependencyPropertyInit_CS, propertyName, propTypeName, className, metaOptions);

            return initExpression;
        }

        #endregion
    }
}
