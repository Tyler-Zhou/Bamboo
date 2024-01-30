using System;
using System.Collections.Generic;
using ICP.DataCache.ServiceInterface.File;
using ICP.DataCache.ServiceInterface;
using ICP.MailCenter.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;


namespace ICP.MailCenter.UI
{
    public class MailConverter : BaseFileConverter
    {
        public override FileType FileType
        {
            get
            {
                return FileType.msg;
            }
        }

        public override List<String> FileExtensions
        {
            get { return new List<String> { ".msg" }; }
        }

        public override void Convert(String path)
        {
            base.Convert(path);
            FileNewPath= OutlookService.ConvertMailToPDF(path);
        }
        private IOutLookService OutlookService
        {
            get
            {
                if (LocalData.ApplicationType == ICP.Framework.CommonLibrary.Common.ApplicationType.EmailCenter)
                {
                    return ServiceClient.GetClientService<IOutLookService>();
                }
                else
                {
                    ClientHelper.EnsureEmailCenterAppStarted();
                    return ServiceClient.GetService<IOutLookService>();
                }
            }
        }
    }

}
