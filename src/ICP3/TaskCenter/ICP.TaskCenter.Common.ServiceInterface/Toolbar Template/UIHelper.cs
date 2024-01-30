using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using ICP.Framework.CommonLibrary.Client;
using ICP.MailCenter.Business.ServiceInterface;

namespace ICP.MailCenter.Business.ServiceInterface
{
    public static class UIHelper
    {
      
        public static ImageList GetImageList(String imageFilePath)
        {
            try
            {
                //D:\ICP3\icpclient\Images\Toolbar
                string[] imageFilePaths = System.IO.Directory.GetFiles(imageFilePath).Where(file => System.Text.RegularExpressions.Regex.IsMatch(file, @"^.+\.(gif|jpg|jpeg|ico)$", RegexOptions.IgnoreCase)).ToArray();
                if (imageFilePaths == null || imageFilePaths.Length <= 0)
                    return new ImageList();
                System.Windows.Forms.ImageList list = new ImageList();
                list.ImageSize = new System.Drawing.Size(16, 16);
                foreach (string imageFile in imageFilePaths)
                {
                    string imageName = Path.GetFileName(imageFile);
                    list.Images.Add(imageName, System.Drawing.Image.FromFile(imageFile));
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static Dictionary<string, string> ExtractNoPairs(string refNo, char[] pairSeparator, char keyValueSeparator)
        {
            refNo = refNo ?? string.Empty;
            if (string.IsNullOrEmpty(refNo))
                return new Dictionary<string, string>();
            string[] pairs = refNo.Split(pairSeparator);
            Dictionary<string, string> dicPair = new Dictionary<string, string>();
            for (int i = 0; i < pairs.Length; i++)
            {
                string pair = pairs[i] ?? string.Empty;
                pair = pair.Trim();
                if (string.IsNullOrEmpty(pair))
                    continue;
                string[] keyValue = pair.Split(keyValueSeparator);
                if (keyValue == null || keyValue.Length != 2)
                    continue;
                string key = keyValue[0].Trim();
                string value = keyValue[1].Trim();
                if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                    continue;
                dicPair.Add(key, value);

            }
            return dicPair;

        }
    }
    /// <summary>
    /// SO相关设置辅助类
    /// 信息保存到本地配置文件
    /// </summary>
    public class SOSetting
    {
        static SOSetting instance;
      
        public static SOSetting Current
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }
                else
                {
                    instance = new SOSetting();
                    return instance;
                }
            }
        }
        private SOSetting()
        {
            this.autoFillSO = GetValue(Constants.AutoFillSOKey);
            this.notfiyCS = GetValue(Constants.NotifyCSKey);
        }
        private bool autoFillSO = true;
        private bool notfiyCS = true;
        /// <summary>
        /// 上传SO附件时是否自动填充SO号
        /// </summary>
        public bool AutoFillSO
        {
            get {
                return this.autoFillSO;
            }
            set
            {
                this.autoFillSO = value;
                ClientConfig.Current.AddValue(Constants.AutoFillSOKey, value.ToString());
            }
        }
        /// <summary>
        /// 是否在上传SO时自动通知客服
        /// </summary>
        public bool NotfiyCS
        {
            get {
                return this.notfiyCS;
            }
            set {
                this.notfiyCS = value;
                ClientConfig.Current.AddValue(Constants.NotifyCSKey, value.ToString());
            }
        }
        private bool GetValue(string key)
        {
            bool result = true;
            if (ClientConfig.Current.Contains(key))
            {
                result = Boolean.Parse(ClientConfig.Current.GetValue(key));
            }
            return result;
        }
    }
}
