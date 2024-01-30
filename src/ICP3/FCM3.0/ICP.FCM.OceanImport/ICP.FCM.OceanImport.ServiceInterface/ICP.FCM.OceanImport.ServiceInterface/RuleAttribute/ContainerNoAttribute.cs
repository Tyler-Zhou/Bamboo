//-----------------------------------------------------------------------
// <copyright file="ContainerNoAttribute.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.OceanImport.ServiceInterface.RuleAttribute
{
    using System;
    using ICP.FCM.Common.ServiceInterface.Common;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// 箱号规则验证特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ContainerNoAttribute : ValidationAttribute
    {
        string error;
        public override bool IsValid(object value)
        {
            if (value == null) return false;

            string val = value.ToString();
            error= ValidateContainerHelper.CheckContainerNo(val);
            if(string.IsNullOrEmpty(error)==false)
            {
                return false;
            }

            return true;
        }

        public new string ErrorMessage
        {
            get
            {
                return error;
            }
            set
            {

            }
        }

    }

}
