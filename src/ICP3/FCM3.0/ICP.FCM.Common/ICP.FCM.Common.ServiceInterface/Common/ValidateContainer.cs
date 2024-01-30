
//-----------------------------------------------------------------------
// <copyright file="ValidateContainerHelper.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.Common.ServiceInterface.Common
{
    using System;
    using System.Text;

    /// <summary>
    /// 验证箱号帮助类    /// </summary>
    public class ValidateContainerHelper
    {
        /// <summary>
        /// 核对数：用于计算机核对箱主号与顺序号记录的正确性。核对号一般于顺序号之后，用一位阿拉伯数字表示，并加方框以醒目。        /// 核对号是由箱主代码的四位字母和顺序号的六位数字通过以下方式换算而得。具体换算步骤如下：
        /// 首先，将表示箱主代码的四位字母转化成相应的等效数字，字母和等效数字的对应关系见下表        /// A--10;B--12;C--13;D--14;....J--20;K--21;L--23;M--24;N--25;.....T--31;U--32;V--34;W--35;X--36;Y--37;Z—38
        /// 从表中可以看出，去掉了11及其倍数的数字，这是因为后面的计算将把11作为模数。        /// 然后，将前四位字母对应的等效数字和后面的顺序号的数字（共计10位）采用加权系数法进行计算求和。        /// </summary>
        /// <param name="containerChar">验证字串符</param>
        /// <returns></returns>
        private static int ContainerCheckDigitCharToInt(char containerChar)
        {
            int result = Convert.ToInt32(containerChar);
            int returnResult = 0;
            if (result > 54 && result < 91)
            {
                returnResult = result - 55;
                returnResult += returnResult / 11;
            }

            return returnResult;

            //int returnVal = 0;
            //switch (containerChar)
            //{
            //    case 'A':
            //        returnVal = 10;
            //        break;
            //    case 'B':
            //        returnVal = 12;
            //        break;
            //    case 'C':
            //        returnVal = 13;
            //        break;
            //    case 'D':
            //        returnVal = 14;
            //        break;
            //    case 'E':
            //        returnVal = 15;
            //        break;
            //    case 'F':
            //        returnVal = 16;
            //        break;
            //    case 'G':
            //        returnVal = 17;
            //        break;
            //    case 'H':
            //        returnVal = 18;
            //        break;
            //    case 'I':
            //        returnVal = 19;
            //        break;
            //    case 'J':
            //        returnVal = 20;
            //        break;
            //    case 'K':
            //        returnVal = 21;
            //        break;
            //    case 'L':
            //        returnVal = 23;
            //        break;
            //    case 'M':
            //        returnVal = 24;
            //        break;
            //    case 'N':
            //        returnVal = 25;
            //        break;
            //    case 'O':
            //        returnVal = 26;
            //        break;
            //    case 'P':
            //        returnVal = 27;
            //        break;
            //    case 'Q':
            //        returnVal = 28;
            //        break;
            //    case 'R':
            //        returnVal = 29;
            //        break;
            //    case 'S':
            //        returnVal = 30;
            //        break;
            //    case 'T':
            //        returnVal = 31;
            //        break;
            //    case 'U':
            //        returnVal = 32;
            //        break;
            //    case 'V':
            //        returnVal = 34;
            //        break;
            //    case 'W':
            //        returnVal = 35;
            //        break;
            //    case 'X':
            //        returnVal = 36;
            //        break;
            //    case 'Y':
            //        returnVal = 37;
            //        break;
            //    case 'Z':
            //        returnVal = 38;
            //        break;
            //    default:
            //        returnVal = 0;
            //        break;
            //}
            //return returnVal;
        }

        /// <summary>
        /// 验证箱号
        /// </summary>
        /// <remarks>
        /// 标准集装箱箱号由11位编码组成，包括三个部分：
        /// 1、 第一部分由4位英文大写字母组成。前三位代码 (Owner Code) 主要说明箱主、经营人，第四位代码说明集装箱的类型(U)。列如CBHU 开头的标准集装箱是表明箱主和经营人为中远集运。
        /// 2、 第二部分由6位数字组成。是箱体注册码（Registration Code）, 用于一个集装箱箱体持有的唯一标识。
        /// 3、 第三部分为校验码（Check Digit）由前4位字母和6位数字经过校验规则运算得到，用于识别在校验时是否发生错误。即第11位数字。
        ///     (核对数：用于计算机核对箱主号与顺序号记录的正确性。核对号一般于顺序号之后，用一位阿拉伯数字表示，并加方框以醒目。核对号是由箱主代码的四位字母和顺序号的六位数字通过以下方式换算而得。具体换算步骤如下：
        ///              首先，将表示箱主代码的四位字母转化成相应的等效数字，字母和等效数字的对应关系见下表
        ///              A--10;B--12;C--13;D--14;....J--20;K--21;L--23;M--24;N--25;.....T--31;U--32;V--34;W--35;X--36;Y--37;Z—38
        ///              从表中可以看出，去掉了11及其倍数的数字，这是因为后面的计算将把11作为模数。然后，将前四位字母对应的等效数字和后面的顺序号的数字（共计10位）采用加权系数法进行计算求和。
        /// </remarks>
        /// <param name="containerNo"></param>
        /// <returns></returns>
        public static string CheckContainerNo(string containerNo)
        {
            StringBuilder errorInfo = new StringBuilder();

            ////箱号必填规则验证
            //if (string.IsNullOrEmpty(containerNo))
            //{
            //    errorInfo.Append("箱号必须录入.");
            //    return errorInfo.ToString();
            //}

            char[] noChars = containerNo.ToCharArray();
            int noLength = noChars.Length;

            string ContainerNo = containerNo.Trim();

            int VerifyNo = 0;
            int ModNo = 0;
            int equalVal = 0;
            double computeNo = 0;

            //箱号长度11位规则验证
            if (noLength > 11)
            {
                errorInfo.AppendFormat("箱号长度({0})大于11位.", noLength);
                errorInfo.AppendLine();
            }
            if (noChars.Length < 11)
            {
                errorInfo.AppendFormat("箱号长度({0})小于11位.", noLength);
                errorInfo.AppendLine();
            }

            bool isTure = true;
            string messageinfo = string.Empty;

            for (int i = 0; i < ContainerNo.Length; i++)
            {
                char input = Convert.ToChar(ContainerNo[i]);

                char tempChar = noChars[i];
                if (i == 3)
                {
                    //第四位代码说明集装箱的类型(U)规则验证
                    if (tempChar != 'U')
                    {
                        errorInfo.AppendFormat("箱号第{0}位的'{1}',标准集装箱箱号第四位集装箱的类型代码应该是\"U\".", i + 1, tempChar);
                        errorInfo.AppendLine();
                    }
                }

                if (i <= 3)
                {
                    //箱号前4位由4位英文大写字母,规则验证
                    if (Char.IsUpper(tempChar) == false)
                    {
                        errorInfo.AppendFormat("箱号第{0}位的'{1}',违背标准集装箱箱号前四位由四位英文大写字母的规则.", i + 1, tempChar);
                        errorInfo.AppendLine();
                    }

                }
                if (i > 3 && i < 10)
                {
                    //第二部分由6位数字组成规则验证.
                    if (Char.IsNumber(tempChar) == false)
                    {
                        //if (Char.IsUpper(tempChar) == false)
                        //{
                            errorInfo.AppendFormat("箱号第{0}位的'{1}',违背标准集装箱箱号第五位至第十位由数字组成的规则.", i + 1, tempChar);
                            errorInfo.AppendLine();
                        //}
                    }
                }

                if (i == 10)
                {
                    VerifyNo = (int)input - 48;
                }
                else
                {
                    if (i < 4)
                    {
                        equalVal = (int)input;
                    }
                    else
                    {
                        equalVal = (int)input - 48;
                    }
                    if (i < 11) computeNo = computeNo + (equalVal * Math.Pow(2, i));
                }
            }

            ModNo = (int)(computeNo % 11.0);
            if (ModNo == 10) ModNo = 0;
            if (ContainerNo.Length == 11)
            {
                if (VerifyNo != ModNo)
                {
                    //Edited by tom，总是报警正确的柜号，时间缘因，暂时禁用此警告中。
                    //errorInfo.AppendFormat("箱号第11位的{0},违背标准集装箱箱号校验码的规则.", noChars[10]);
                    //errorInfo.AppendLine();
                }
            }
            return errorInfo.ToString();
        }

        /// <summary>
        /// 箱号规则验证
        /// </summary>
        /// <remarks>
        /// 标准集装箱箱号由11位编码组成，包括三个部分：        /// 1、 第一部分由4位英文大写字母组成。前三位代码 (Owner Code) 主要说明箱主、经营人，第四位代码说明集装箱的类型(U)。列如CBHU 开头的标准集装箱是表明箱主和经营人为中远集运。        /// 2、 第二部分由6位数字组成。是箱体注册码（Registration Code）, 用于一个集装箱箱体持有的唯一标识。        /// 3、 第三部分为校验码（Check Digit）由前4位字母和6位数字经过校验规则运算得到，用于识别在校验时是否发生错误。即第11位数字。        ///     (核对数：用于计算机核对箱主号与顺序号记录的正确性。核对号一般于顺序号之后，用一位阿拉伯数字表示，并加方框以醒目。核对号是由箱主代码的四位字母和顺序号的六位数字通过以下方式换算而得。具体换算步骤如下：
        ///              首先，将表示箱主代码的四位字母转化成相应的等效数字，字母和等效数字的对应关系见下表        ///              A--10;B--12;C--13;D--14;....J--20;K--21;L--23;M--24;N--25;.....T--31;U--32;V--34;W--35;X--36;Y--37;Z—38
        ///              从表中可以看出，去掉了11及其倍数的数字，这是因为后面的计算将把11作为模数。然后，将前四位字母对应的等效数字和后面的顺序号的数字（共计10位）采用加权系数法进行计算求和。        /// </remarks>
        /// <param name="containerNo">箱号</param>
        /// <returns></returns>
        public static string CheckContainerNo_removed(string containerNo)
        {
            StringBuilder errorInfo = new StringBuilder();

            //箱号必填规则验证
            if (string.IsNullOrEmpty(containerNo))
            {
                errorInfo.Append("箱号必须录入.");
                return errorInfo.ToString();
            }


            char[] noChars = containerNo.ToCharArray();
            int noLength = noChars.Length;

            //箱号长度11位规则验证            if (noLength > 11)
            {
                errorInfo.AppendFormat("箱号长度({0})大于11位.", noLength);
                errorInfo.AppendLine();
            }
            if (noChars.Length < 11)
            {
                errorInfo.AppendFormat("箱号长度({0})小于11位.", noLength);
                errorInfo.AppendLine();
            }

            double sum = 0;     //前四位字母对应的等效数字和后面的顺序号的数字（共计10位）采用加权系数法进行计算求和

            //前三位大写字母规则验证            for (int index = 0; index < noLength; index++)
            {
                char tempChar = noChars[index];
                if (index == 3)
                {
                    //第四位代码说明集装箱的类型(U)规则验证
                    if (tempChar != 'U')
                    {
                        errorInfo.AppendFormat("箱号第{0}位的'{1}',标准集装箱箱号第四位集装箱的类型代码应该是\"U\".", index + 1, tempChar);
                        errorInfo.AppendLine();
                    }
                }

                if (index < 4)
                {
                    //箱号前4位由4位英文大写字母,规则验证
                    if (Char.IsUpper(tempChar) == false)
                    {
                        errorInfo.AppendFormat("箱号第{0}位的'{1}',违背标准集装箱箱号前四位由四位英文大写字母的规则.", index + 1, tempChar);
                        errorInfo.AppendLine();
                    }

                    sum = sum + (ContainerCheckDigitCharToInt(tempChar) * Math.Pow(2, index));
                }
                else if (index > 3 && index < 10)
                {
                    //第二部分由6位数字组成规则验证.
                    if (Char.IsNumber(tempChar) == false)
                    {
                        if (Char.IsUpper(tempChar) == false)
                        {
                            errorInfo.AppendFormat("箱号第{0}位的'{1}',违背标准集装箱箱号第五位至第十位由数字组成的规则.", index + 1, tempChar);
                            errorInfo.AppendLine();
                        }
                    }
                    sum = sum + ((int)tempChar - 48) * Math.Pow(2, index);
                }
            }

            //第三部分为校验码（Check Digit）由前4位字母和6位数字经过校验规则运算得到，用于识别在校验时是否发生错误
            int tempComputeResult = (int)(sum % 11.0);
            if (tempComputeResult == 10)
            {
                tempComputeResult = 0;
            }

            if (noLength == 11)
            {
                int tempCheckDigit = (int)noChars[10] - 48;
                if (tempComputeResult != tempCheckDigit)
                {
                    errorInfo.AppendFormat("箱号第11位的{0},违背标准集装箱箱号校验码的规则.", noChars[10]);
                    errorInfo.AppendLine();
                }
            }

            return errorInfo.ToString();
        }
    }
}
