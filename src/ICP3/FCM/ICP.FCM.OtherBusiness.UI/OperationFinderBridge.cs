using System;
using System.Windows.Forms;
using ICP.Common.ServiceInterface;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.OtherBusiness.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class OperationFinderBridge : IDisposable
    {
        ButtonEdit _txtOperation;
        bool _isEnglish;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="txtOperation">各种绑定的更新模式会被设置成 OnPropertyChanged</param>
        /// <param name="isEnglish"></param>
        public OperationFinderBridge(ButtonEdit txtOperation, bool isEnglish)
        {
            _txtOperation = txtOperation;
            _isEnglish = isEnglish;
            foreach (Binding item in _txtOperation.DataBindings)
            {
                item.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
            }

            Init();
        }

        public event EventHandler ValueChanged;
        public event EventHandler Cleard;
        private IDisposable operationFinder;
        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        public void Init()
        {
            operationFinder = DataFindClientService.Register(_txtOperation, CommonFinderConstants.LocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                    delegate(object inputSource, object[] resultData)
                    {
                        _txtOperation.Tag = new Guid(resultData[0].ToString());
                        WriteTagToEntity();

                        string text = _isEnglish ? resultData[2].ToString() : resultData[3].ToString();

                        _txtOperation.Text = text;

                        if (_txtOperation.DataBindings["Text"] != null)
                        {
                            _txtOperation.DataBindings["Text"].WriteValue();
                        }

                        if (ValueChanged != null)
                        {
                            ValueChanged(this, new EventArgs());
                        }
                    },
                    delegate()
                    {
                        _txtOperation.Text = string.Empty;
                        _txtOperation.Tag = Guid.Empty;
                        if (Cleard != null)
                        {
                            Cleard(this, new EventArgs());
                        }
                    }, ClientConstants.MainWorkspace);

        }
        void WriteTagToEntity()
        {
            foreach (Binding item in _txtOperation.DataBindings)
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
            if (operationFinder != null)
            {
                operationFinder.Dispose();
                operationFinder = null;
            }
            ValueChanged = null;
            Cleard = null;
            _txtOperation = null;
        }

        #endregion
    }
}
