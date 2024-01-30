
//-----------------------------------------------------------------------
// <copyright file="NameCreationService.cs" company="ICP">
//     Copyright (c) ICP. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.WorkFlowDesigner
{
    using System;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;
    using System.ComponentModel;

    /// <summary>
    /// 创建唯一命名服务
    /// </summary>
    public class LWNameCreationService : INameCreationService
    {
        public LWNameCreationService()
        {
        }

        public string CreateName(
            IContainer container,
            Type dataType)
        {
            string baseName = dataType.Name;
            int uniqueID = 1;

            bool unique = false;
            while (!unique)
            {
                unique = true;
                for (int i = 0; i < container.Components.Count; i++)
                {
                    if (container.Components[i].Site.Name.StartsWith(baseName + uniqueID.ToString()))
                    {
                        unique = false;
                        uniqueID++;
                        break;
                    }
                }
            }

            return baseName + uniqueID.ToString();
        }

        public bool IsValidName(string name)
        {
            for (int i = 0; i < name.Length; i++)
            {
                char ch = name[i];
                UnicodeCategory uc = Char.GetUnicodeCategory(ch);
                switch (uc)
                {
                    case UnicodeCategory.UppercaseLetter:
                    case UnicodeCategory.LowercaseLetter:
                    case UnicodeCategory.TitlecaseLetter:
                    case UnicodeCategory.DecimalDigitNumber:
                        break;
                    default:
                        return false;
                }
            }
            return true;
        }


        public void ValidateName(string name)
        {
            for (int i = 0; i < name.Length; i++)
            {
                char ch = name[i];
                UnicodeCategory uc = Char.GetUnicodeCategory(ch);
                switch (uc)
                {
                    case UnicodeCategory.UppercaseLetter:
                    case UnicodeCategory.LowercaseLetter:
                    case UnicodeCategory.TitlecaseLetter:
                    case UnicodeCategory.DecimalDigitNumber:
                        break;
                    default:
                        throw new Exception("The name '" + name + "' is not a valid identifier.");
                }
            }
        }
    }

}
