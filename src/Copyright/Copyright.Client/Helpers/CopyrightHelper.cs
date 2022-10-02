/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-02 14:06:30
 * Author:zhoubiyu@hotmail.com
 * Update history:
********************************************************/
using Copyright.Client.Common;
using Copyright.Client.Models;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Copyright.Client.Helpers
{
    /// <summary>
    /// ��Ȩ������
    /// </summary>
    public class CopyrightHelper
    {
        //�����Կ��� string[] ContentLines = Content.Split(new string[] { "\r\n" }, StringSplitOptions.None);
        //���Կ��� string[] ContentLines = Content.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries); 

        /// <summary>
        /// Customize Template
        /// </summary>
        /// <param name="originalContent"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static string CustomizeTemplate(string originalContent, ObservableCollection<BaseDataModel> collection)
        {
            //�Ƿ�����Header
            bool isContainsHeader = !"using".Equals(originalContent.Substring(0, 5));
            //�Ƿ����Ԥ����ָ��
            bool isContainsPreprocessorDirectives = originalContent.Contains("#region");
            //�Ƿ�������캯��
            int constructorCount = Regex.Matches(originalContent, @"public \$safeitemrootname").Count;
            //�Ƿ�ӿ�
            bool isInterface = originalContent.Contains("interface");
            string[] lines = originalContent.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            //���������
            int LeftBracesCount = 0;
            //�Ҵ�������
            int RightBracesCount = 0;
            //���캯��֮ǰ
            bool beforeConstructor = false;
            //���캯������
            bool endConstructor = false;

            StringBuilder customizeContent=new StringBuilder();
            if(!isContainsHeader)
            {
                customizeContent.AppendLine(collection.SingleOrDefault(item => item.Name.Equals(AppConstant.HEADER_NAME)).Description);
            }
            foreach (string line in lines)
            {
                if (line.Contains("{"))
                    LeftBracesCount++;
                if (line.Contains("}"))
                    RightBracesCount++;

                if (beforeConstructor && !isContainsPreprocessorDirectives)
                {
                    //Member
                    customizeContent.AppendLine($"        #region {collection.SingleOrDefault(item => item.Name.Equals(AppConstant.MEMBER_NAME)).Description}");
                    customizeContent.AppendLine($"        #endregion");
                    customizeContent.AppendLine($"");
                    //Service
                    customizeContent.AppendLine($"        #region {collection.SingleOrDefault(item => item.Name.Equals(AppConstant.SERVICE_NAME)).Description}");
                    customizeContent.AppendLine($"        #endregion");
                    customizeContent.AppendLine($"");
                    //Command
                    customizeContent.AppendLine($"        #region {collection.SingleOrDefault(item => item.Name.Equals(AppConstant.COMMAND_NAME)).Description}");
                    customizeContent.AppendLine($"        #endregion");
                    customizeContent.AppendLine($"");
                    if(isInterface)
                    {
                        //Public Method
                        customizeContent.AppendLine($"        #region {collection.SingleOrDefault(item => item.Name.Equals(AppConstant.PUBLICMETHOD_NAME)).Description}");
                        customizeContent.AppendLine($"        #endregion");
                        customizeContent.AppendLine($"");
                        //Private Method
                        customizeContent.AppendLine($"        #region {collection.SingleOrDefault(item => item.Name.Equals(AppConstant.PRIVATEMETHOD_NAME)).Description}");
                        customizeContent.AppendLine($"        #endregion");
                        customizeContent.AppendLine($"");
                    }
                    else
                    {
                        if (constructorCount > 0)
                        {
                            customizeContent.AppendLine($"        #region {collection.SingleOrDefault(item => item.Name.Equals(AppConstant.CONSTRUCTOR_NAME)).Description}");
                        }
                        else
                        {
                            customizeContent.AppendLine($"        #region {collection.SingleOrDefault(item => item.Name.Equals(AppConstant.CONSTRUCTOR_NAME)).Description}");
                            customizeContent.AppendLine($"        #endregion");
                            customizeContent.AppendLine($"");
                            //Event
                            customizeContent.AppendLine($"        #region {collection.SingleOrDefault(item => item.Name.Equals(AppConstant.EVENT_NAME)).Description}");
                            customizeContent.AppendLine($"        #endregion");
                            customizeContent.AppendLine($"");
                            //Public Method
                            customizeContent.AppendLine($"        #region {collection.SingleOrDefault(item => item.Name.Equals(AppConstant.PUBLICMETHOD_NAME)).Description}");
                            customizeContent.AppendLine($"        #endregion");
                            customizeContent.AppendLine($"");
                            //Private Method
                            customizeContent.AppendLine($"        #region {collection.SingleOrDefault(item => item.Name.Equals(AppConstant.PRIVATEMETHOD_NAME)).Description}");
                            customizeContent.AppendLine($"        #endregion");
                            customizeContent.AppendLine($"");
                        }
                    }
                    customizeContent.AppendLine(line);
                    beforeConstructor = false;
                }
                else
                {
                    if(endConstructor && !isContainsPreprocessorDirectives)
                    {
                        customizeContent.AppendLine($"        #endregion");
                        customizeContent.AppendLine($"");
                        //Event
                        customizeContent.AppendLine($"        #region {collection.SingleOrDefault(item => item.Name.Equals(AppConstant.EVENT_NAME)).Description}");
                        customizeContent.AppendLine($"        #endregion");
                        customizeContent.AppendLine($"");
                        //Public Method
                        customizeContent.AppendLine($"        #region {collection.SingleOrDefault(item => item.Name.Equals(AppConstant.PUBLICMETHOD_NAME)).Description}");
                        customizeContent.AppendLine($"        #endregion");
                        customizeContent.AppendLine($"");
                        //Private Method
                        customizeContent.AppendLine($"        #region {collection.SingleOrDefault(item => item.Name.Equals(AppConstant.PRIVATEMETHOD_NAME)).Description}");
                        customizeContent.AppendLine($"        #endregion");
                        customizeContent.AppendLine($"");
                        endConstructor = false;
                    }
                    customizeContent.AppendLine(line);
                }
                if (!endConstructor && line.Contains("}") && RightBracesCount == constructorCount)
                {
                    endConstructor = true;
                }
                if (!beforeConstructor && line.Contains("{") && LeftBracesCount == 2)
                {
                    beforeConstructor = true;
                }
            }
            return customizeContent.ToString();
        }

        public static string GetItemTemplatePath()
        {
            return $"{GetDevenvPath()}ItemTemplates\\CSharp\\";
        }

        /// <summary>
        /// ��ȡVisual Studio Ŀ¼
        /// </summary>
        static string GetDevenvPath()
        {
            return GetApplicationPath("devenv");
        }

        /// <summary>
        /// ͨ��Ӧ�ó������ƻ�ȡ��װ·��
        /// </summary>
        /// <param name="applicationName">Ӧ�ó�����</param>
        /// <returns>Ӧ�ó���·��</returns>
        static string GetApplicationPath(string applicationName)
        {
            string path = string.Empty;
            try
            {
                string strKeyName = string.Empty;
                string softPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\";
                RegistryKey regKey = Registry.LocalMachine;
                RegistryKey regSubKey = regKey.OpenSubKey(softPath + applicationName + ".exe", false);

                object objResult = regSubKey.GetValue(strKeyName);
                RegistryValueKind regValueKind = regSubKey.GetValueKind(strKeyName);
                if (regValueKind == RegistryValueKind.String)
                {
                    path = objResult.ToString();
                }
                path = path.Replace("\"", "");
                path = path.Replace(applicationName + ".exe", "");
            }
            catch
            {
                throw new Exception("Failed to get application path");
            }
            return path;
        }

        public static string GetContent(string fullPath)
        {
            if (!File.Exists(fullPath))
                return "�ļ�������";
            return File.ReadAllText(fullPath);
        }
    }
}
