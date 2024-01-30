using System;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface;

namespace ICP.Common.UI
{
    /// <summary>
    /// 港口搜索器桥接器
    /// </summary>
    public class LocationFinderBridge:IDisposable
    {
        ButtonEdit _txtPort;
        IDataFindClientService _dfService;
        bool _isEnglish;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="txtPort">各种绑定的更新模式会被设置成 OnPropertyChanged</param>
        /// <param name="dfService"></param>
        /// <param name="isEnglish"></param>
        public LocationFinderBridge(ButtonEdit txtPort,
            IDataFindClientService dfService, bool isEnglish)
        {
            this._txtPort = txtPort;
            this._dfService = dfService;
            this._isEnglish = isEnglish;


            //foreach (System.Windows.Forms.Binding item in this._txtPort.DataBindings)
            //{
            //    item.DataSourceUpdateMode = System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged;
            //}

            this.Init();
        }

        public event EventHandler ValueChanged;
        public event EventHandler Cleard;

        public void Init()
        {
            _dfService.Register(_txtPort, CommonFinderConstants.LocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                   delegate(object inputSource, object[] resultData)
                   {
                       _txtPort.Tag = new Guid(resultData[0].ToString());
                       this.WriteTagToEntity();

                       string text = this._isEnglish ? resultData[2].ToString() : resultData[3].ToString();

                       _txtPort.Text = text;

                       if (this._txtPort.DataBindings["Text"] != null)
                       {
                           this._txtPort.DataBindings["Text"].WriteValue();
                       }

                       if (this.ValueChanged != null)
                       {
                           this.ValueChanged(this, new EventArgs());
                       }
                   },
                   delegate()
                   {
                       if (_txtPort.DataBindings["Tag"] != null)
                       {
                           _txtPort.Tag = null;
                           _txtPort.DataBindings["Tag"].WriteValue();
                       }
                       if (_txtPort.DataBindings["Text"] != null)
                       {
                           _txtPort.Text = string.Empty;
                           _txtPort.DataBindings["Text"].WriteValue();
                       }
                       if (this.Cleard != null)
                       {
                           this.Cleard(this, new EventArgs());
                       }
                   }, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

        }
        void WriteTagToEntity()
        {
            foreach (System.Windows.Forms.Binding item in this._txtPort.DataBindings)
            {
                if (item.PropertyName == "Tag")
                {
                    item.WriteValue();
                    break;
                }
            }
        }


        #region IDisposable 成员

        public void Dispose()
        {
            this._txtPort = null;
            this.ValueChanged = null;
            this.Cleard = null;
        }

        #endregion
    }
}
