#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/5/10 18:01:05
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Security.Cryptography;
using System.Text;

namespace Cityocean.Crawl.CommonLibrary
{
    /// <summary>
    /// 加密类
    /// </summary>
    /// <remarks>
    /// 封装常用的加密算法
    /// </remarks>
    public sealed class Cryptography
    {
        /// <summary>
        /// 3des解密字符串
        /// </summary>
        /// <param name="a_strString">要解密的字符串</param>
        /// <param name="a_strKey">密钥</param>
        /// <returns>解密后的字符串</returns>
        /// <exception cref="Exception">密钥错误</exception>
        /// <remarks>静态方法，采用默认ascii编码</remarks>
        public static string Decrypt3DES(string a_strString, string a_strKey)
        {
            TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            tripleDESCryptoServiceProvider.Key = mD5CryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(a_strKey));
            tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
            ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateDecryptor();
            string result = "";
            try
            {
                byte[] array = Convert.FromBase64String(a_strString);
                result = Encoding.ASCII.GetString(cryptoTransform.TransformFinalBlock(array, 0, array.Length));
            }
            catch (Exception innerException)
            {
                throw new Exception("Invalid Key or input string is not a valid base64 string", innerException);
            }
            return result;
        }
    }
}
