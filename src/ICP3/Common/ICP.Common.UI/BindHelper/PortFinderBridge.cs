using System;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface;

namespace ICP.Common.UI
{
    /// <summary>
    /// 港口搜索器桥接器
    /// </summary>
    public class PortFinderBridge:IDisposable
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
        public PortFinderBridge(ButtonEdit txtPort,
            IDataFindClientService dfService, bool isEnglish)
        {
            this._txtPort = txtPort;
            this._dfService = dfService;
            this._isEnglish = isEnglish;


            foreach (System.Windows.Forms.Binding item in this._txtPort.DataBindings)
            {
                item.DataSourceUpdateMode = System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged;
            }

            this.Init();
        }

        public event EventHandler Cleared;

        public void Init()
        {
            _dfService.Register(_txtPort, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                   delegate(object inputSource, object[] resultData)
                   {
                       _txtPort.Tag = new Guid(resultData[0].ToString());

                       this.WriteTagToEntity();//Text改动，可能在UI中被注册了联动事件。而这个事件里面又经常要用到Tag的新值，所以要修改设置Tag。
                       _txtPort.Text = this._isEnglish ? resultData[2].ToString() : resultData[3].ToString();

                       if (this._txtPort.DataBindings["Text"] != null)
                       {
                           this._txtPort.DataBindings["Text"].WriteValue();
                       }
                   },
                   delegate()
                   {
                       _txtPort.Tag = Guid.Empty;

                       this.WriteTagToEntity();
                       _txtPort.Text = string.Empty;

                       if (this.Cleared != null)
                       {
                           this.Cleared(this, new EventArgs());
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
            this.Cleared = null;
            
        }

        #endregion
    }


}
