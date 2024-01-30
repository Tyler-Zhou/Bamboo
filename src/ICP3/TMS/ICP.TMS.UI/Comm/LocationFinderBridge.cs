using System;
using ICP.Common.ServiceInterface;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.TMS.UI
{
    /// <summary>
    /// 港口搜索器桥接器
    /// 作者: Pearl
    /// 创建时间: 2011-06-21
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

            
            foreach(System.Windows.Forms.Binding item in this._txtPort.DataBindings)
            {
                item.DataSourceUpdateMode = System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged;
            }

            this.Init();
        }
        private IDisposable portFinder;
        public void Init()
        {
           portFinder= _dfService.Register(_txtPort, CommonFinderConstants.LocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                   delegate(object inputSource, object[] resultData)
                   {
                       _txtPort.Text = this._isEnglish ? resultData[2].ToString() : resultData[3].ToString();
                       _txtPort.Tag = new Guid(resultData[0].ToString());
                   },
                   delegate()
                   {
                       _txtPort.Text = string.Empty;
                       _txtPort.Tag = Guid.Empty;
                   }, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);


            this._txtPort.TextChanged += new EventHandler(_txtPort_TextChanged);
        }

        void _txtPort_TextChanged(object sender, EventArgs e)
        {
            if (this._txtPort.Text.Trim().Length == 0)
            {
                _txtPort.Text = string.Empty;
                _txtPort.Tag = Guid.Empty;
            }
        }

        #region IDisposable 成员

        public void Dispose()
        {
            if (this.portFinder != null)
            {
                this.portFinder.Dispose();
                this.portFinder = null;
            }
            this._dfService = null;
            this._txtPort.TextChanged -= this._txtPort_TextChanged;
            this._txtPort = null;
            
        }

        #endregion
    }
}
