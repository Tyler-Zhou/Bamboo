/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-02 9:35:24
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Copyright.Client.Helpers
{
    /// <summary>
    /// Json���л�������
    /// </summary>
    public class JsonSerializerHelper
    {
        /// <summary>
        /// ���л�
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SerializeObject(object value)
        {
            string josnText = "";
            DataContractJsonSerializer js = new DataContractJsonSerializer(value.GetType());
            using (MemoryStream msObj = new MemoryStream())
            {
                //�����л�֮���Json��ʽ����д������
                js.WriteObject(msObj, value);
                msObj.Position = 0;
                //��0���λ�ÿ�ʼ��ȡ���е�����
                using (StreamReader sr = new StreamReader(msObj, Encoding.UTF8))
                {
                    josnText = sr.ReadToEnd();
                }
            }
            return josnText;
        }
        /// <summary>
        /// �����л�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string value)
        {
            T tJSON;
            try
            {
                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(value)))
                {
                    DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(T));
                    //�����л�ReadObject
                    tJSON = (T)deseralizer.ReadObject(ms);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"�����л�ʧ��{ex.Message}");
            }
            return tJSON;
        }
    }
}
