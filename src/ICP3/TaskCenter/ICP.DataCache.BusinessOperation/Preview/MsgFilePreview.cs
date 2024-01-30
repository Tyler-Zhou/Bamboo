using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.MailCenter.ServiceInterface;
using Microsoft.Practices.CompositeUI;
namespace ICP.DataCache.BusinessOperation1.Preview
{
    public class MsgFilePreview : IFilePreview
    {  
        [ServiceDependency]
        public IOutLookService OutlookService { get; set; }
        #region IFilePreview 成员

        public List<string> FileExtensions
        {
            get {
                return new List<string> {".msg"};
             }
        }

        public void Preview(string path)
        {
            OutlookService.Open(path);
        }

        #endregion
    }
}
