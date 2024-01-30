////#region Comment
/////*
//// * Create By:Taylor Zhou
//// * Create On:2018/7/19 星期四 14:27:43
//// *
//// * Description:
//// *         ->
//// *
//// * History:
//// *         ->
//// */
////#endregion

////using System;
////using System.Collections.Generic;
////using System.IO;
////using System.Linq;
////using System.Runtime.Serialization;
////using System.Runtime.Serialization.Formatters.Binary;
////using System.Text;

////namespace ICP.Common.ServiceInterface
////{
////    /// <summary>
////    /// 字符串扩展方法
////    /// </summary>
////    public static class ICPCommonStringExtensionMethods
////    {
////        /// <summary>
////        /// 将沟通阶段枚举值字符串转换为沟通阶段名称字符串
////        /// </summary>
////        /// <param name="input"></param>
////        /// <returns></returns>
////        public static string ToStageNames(this string input)
////        {
////            if (string.IsNullOrEmpty(input))
////                return string.Empty;
////            List<string> valueList = input.Split(',').ToList();
////            StringBuilder builder = new StringBuilder(100);
////            valueList.ForEach(value =>
////            {
////                if (!string.IsNullOrEmpty(value))
////                {
////                    ContactStage temp = (ContactStage)Enum.Parse(typeof(ContactStage), value.Trim(), true);

////                    builder.Append(temp.ToString() + ",");
////                }
////            });
////            return builder.ToString().Substring(0, builder.Length - 1);

////        }
////        /// <summary>
////        /// 
////        /// </summary>
////        /// <param name="input"></param>
////        /// <param name="type"></param>
////        /// <param name="obj"></param>
////        /// <returns></returns>
////        public static string ToSerializerXml(this string input, object obj)
////        {
////            try
////            {
////                IFormatter formatter = new BinaryFormatter();
////                using (Stream stream = new FileStream(input, FileMode.CreateNew))
////                {
////                    formatter.Serialize(stream, obj);
////                }
////            }
////            catch (Exception ex)
////            {
////                return null;
////            }

////            return input;
////        }

////        /// <summary>
////        /// 
////        /// </summary>
////        /// <param name="input"></param>
////        /// <returns></returns>
////        public static object ToDeserializeXml(this string input)
////        {
////            if (!File.Exists(input))
////                return null;
////            try
////            {
////                IFormatter formatter = new BinaryFormatter();
////                using (Stream stream = new FileStream(input, FileMode.Open, FileAccess.Read, FileShare.Read))
////                {
////                    return formatter.Deserialize(stream);
////                }
////            }
////            catch (Exception ex)
////            {
////                return null;
////            }
////        }
////    }
////}
