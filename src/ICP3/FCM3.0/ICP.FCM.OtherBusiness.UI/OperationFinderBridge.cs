using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

using ICP.FCM.OtherBusiness.UI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.OtherBusiness.UI
{
    public class OperationFinderBridge
    {
        ButtonEdit _txtOperation;
        IDataFindClientService _dfService;
        bool _isEnglish;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="txtOperation">各种绑定的更新模式会被设置成 OnPropertyChanged</param>
        /// <param name="dfService"></param>
        /// <param name="isEnglish"></param>
        public OperationFinderBridge(ButtonEdit txtOperation,
            IDataFindClientService dfService, bool isEnglish)
        {
            this._txtOperation = txtOperation;
            this._dfService = dfService;
            this._isEnglish = isEnglish;

            
            foreach(System.Windows.Forms.Binding item in this._txtOperation.DataBindings)
            {
                item.DataSourceUpdateMode = System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged;
            }

            this.Init();
        }

        public event EventHandler ValueChanged;
        public event EventHandler Cleard;

        public void Init()
        {
            _dfService.Register(_txtOperation, CommonFinderConstants.LocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                   delegate(object inputSource, object[] resultData)
                   {
                       _txtOperation.Tag = new Guid(resultData[0].ToString());
                       this.WriteTagToEntity();

                       string text = this._isEnglish ? resultData[2].ToString() : resultData[3].ToString();

                       _txtOperation.Text = text; 
                       
                       if (this._txtOperation.DataBindings["Text"] != null)
                       {
                           this._txtOperation.DataBindings["Text"].WriteValue();
                       }

                       if (this.ValueChanged != null)
                       {
                           this.ValueChanged(this, new EventArgs());
                       }
                   },
                   delegate()
                   {
                       _txtOperation.Text = string.Empty;
                       _txtOperation.Tag = Guid.Empty;
                       if (this.Cleard != null)
                       {
                           this.Cleard(this, new EventArgs());
                       }
                   }, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

        }
        void WriteTagToEntity()
        {
            foreach (System.Windows.Forms.Binding item in this._txtOperation.DataBindings)
            {
                if (item.PropertyName == "Tag")
                {
                    item.WriteValue();
                    break;
                }
            }
        }
    }
}
