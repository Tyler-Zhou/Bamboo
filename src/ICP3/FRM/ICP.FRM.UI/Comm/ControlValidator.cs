#region Comment

/*
 * 
 * FileName:    ControlValidator.cs
 * CreatedOn:   
 * CreatedBy:   
 * 
 * 
 * Description：
 *      ->控件验证
 * History：
 *      ->
 * 
 * 
 * 
 */

#endregion

using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Client;
using System.Reflection;
using System.Windows.Forms;
namespace ICP.FRM.UI.Comm
{
    /// <summary>
    /// 控件验证类型
    /// </summary>
    public enum ControlValidatorType
    {
        /// <summary>
        /// 询价
        /// </summary>
        IsRequired
    }
    /// <summary>
    /// 控件验证选项
    /// </summary>
    public class ControlValidatorItem
    {
        /// <summary>
        /// 字段
        /// </summary>
        public Control Field
        {
            get;
            set;
        }
        /// <summary>
        /// 验证类型
        /// </summary>
        public ControlValidatorType ValidatorType
        {
            get;
            set;
        }
        /// <summary>
        /// 字段英文名称
        /// </summary>
        public string FieldEName
        {
            get;
            set;
        }
        /// <summary>
        /// 字段中文名
        /// </summary>
        public string FieldCName
        {
            get;
            set;
        }
        /// <summary>
        /// 字段值
        /// </summary>
        public object FieldValue
        {
            get
            {
                Type type = Field.GetType();
                PropertyInfo propValue;

                propValue = SeekValueProp(type);
                return  propValue.GetValue(Field, null);
            }
        }
        /// <summary>
        /// 查找属性信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>属性信息</returns>
        PropertyInfo SeekValueProp(Type type)
        {
            PropertyInfo propValue;

            propValue = type.GetProperty("Value");

            if (propValue == null)
                propValue = type.GetProperty("EditValue");
            else
                return propValue;

            if (propValue == null)
                propValue = type.GetProperty("SelectValue");
            else
                return propValue;

            if (propValue == null)
                propValue = type.GetProperty("Text");
            else
                return propValue;

            if (propValue == null)
                throw new Exception(type.Name + ", an unkown control to validating.");
            else
                return propValue ;
        }

        /// <summary>
        /// 验证值是否为空
        /// </summary>
        /// <returns>验证结果</returns>
        public bool IsValueEmpty()
        {
            object value = FieldValue;
            if (value == null)
                return true;
            else
            {
                if (value.ToString().Trim() == string.Empty)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 构造函数
        ///     初始化值
        /// </summary>
        /// <param name="field">字段(控件)</param>
        /// <param name="validatorType">验证类型</param>
        /// <param name="fieldCName">字段中文名</param>
        /// <param name="fieldEName">字段英文名</param>
        public ControlValidatorItem(Control field, ControlValidatorType validatorType, string fieldCName, string fieldEName)
        {
            Field = field;
            ValidatorType = validatorType;
            FieldCName = fieldCName;
            FieldEName = fieldEName;
        }
    }

    /// <summary>
    /// 控件值验证
    /// </summary>
    public class ControlValidator
    {
        /// <summary>
        /// 验证控件列表
        /// </summary>
        List<ControlValidatorItem> _Items = new List<ControlValidatorItem>();
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public ControlValidator()
        {

        }
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns>验证结果</returns>
        public bool ValidateData()
        {
            bool result = true;

            foreach (var item in _Items)
            {
                if (item.ValidatorType == ControlValidatorType.IsRequired && item.IsValueEmpty() == true)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(item.Field, LocalData.IsEnglish ? 
                        string.Format(@"[{0}] is required! You must fill-in it.", item.FieldEName)
                        : string.Format(@"请您输入必填项：[{0}]。", item.FieldCName));
                    result = false;
                }
            }

            return result;
        }
        /// <summary>
        /// 注册字段
        /// </summary>
        /// <param name="field">验证字段(需验证的控件)</param>
        /// <param name="validatorType">验证类型</param>
        /// <param name="fieldCName">中文名</param>
        /// <param name="fieldEName">英文名</param>
        public void RegisterField(Control field, ControlValidatorType validatorType, string fieldCName, string fieldEName)
        {
            _Items.Add(new ControlValidatorItem(field, validatorType, fieldCName, fieldEName));
        }

        
    }
}
