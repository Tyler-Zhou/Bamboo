/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-02 9:49:08
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/
using Copyright.Client.Helpers;
using System;
using System.IO;
using System.Text;

namespace Copyright.Client.Services
{
    /// <summary>
    /// Json����
    /// </summary>
    public class JsonService
    {
        #region ��Ա(Member)
        /// <summary>
        /// ��չ��
        /// </summary>
        string _ExtensionName { get; set; } = ".json";
        /// <summary>
        /// ����·��
        /// </summary>
        string _BasePath
        {
            get
            {
                string path = $"{AppDomain.CurrentDomain.BaseDirectory}\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }
        #endregion

        #region ����(Service)
        #endregion

        #region ���캯��(Constructor)
        #endregion

        #region ����(Method)

        /// <summary>
        /// ���������ļ�
        /// </summary>
        /// <param name="configName">��������</param>
        /// <param name="obj">�����ļ�����</param>
        public bool Save(string configName, object obj)
        {
            return  SaveJSON(configName, obj);
        }

        /// <summary>
        /// ��ȡJson
        /// </summary>
        /// <param name="configName">��������</param>
        /// <typeparam name="TJson">Jsonʵ�����</typeparam>
        /// <returns></returns>
        public TJson Get<TJson>(string configName)
        {
            return GetJSON<TJson>(configName);
        }
        
        #endregion

        #region ˽�з���(Private Method)

        /// <summary>
        /// ����Json�ļ�
        /// </summary>
        /// <param name="configName">��������</param>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool SaveJSON(string configName, object obj)
        {
            try
            {
                string fullPath = $"{_BasePath}{configName}{_ExtensionName}";
                //���л�
                string josnText = JsonSerializerHelper.SerializeObject(obj);
                //д���ļ���
                using (StreamWriter sw = new StreamWriter(fullPath))
                {
                    sw.WriteLine(josnText);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="subDirectory">��Ŀ¼</param>
        /// <param name="configName">��������</param>
        /// <typeparam name="TJSON">����ʵ�����</typeparam>
        /// <returns></returns>
        TJSON GetJSON<TJSON>(string configName)
        {
            try
            {
                string fullPath = $"{_BasePath}{configName}{_ExtensionName}";
                if (!File.Exists(fullPath))
                {
                    throw new Exception($"�ļ�[{fullPath}]������");
                }
                string josnText = "";
                using (StreamReader sr = new StreamReader(fullPath, Encoding.UTF8))
                {
                    josnText = sr.ReadToEnd();
                }
                return JsonSerializerHelper.DeserializeObject<TJSON>(josnText);
            }
            catch
            {
                return default(TJSON);
            }
        }
        
        #endregion
    }
}
