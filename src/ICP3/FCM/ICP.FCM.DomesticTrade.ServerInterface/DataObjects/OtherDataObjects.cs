//-----------------------------------------------------------------------
// <copyright file="PickUpDescription.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.DomesticTrade.ServiceInterface.DataObjects
{
    using System;
    using DevExpress.XtraEditors.DXErrorProvider;
    using Common.ServiceInterface.Common;

    /// <summary>
    /// 键值数据
    /// </summary>
    [Serializable]
    public class KeyValueData
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }
    /// <summary>
    /// 箱列表对象
    /// </summary>
    public partial class DTContainerList : IDXDataErrorInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DTContainerList()
        {
            IsSelected = true;
        }

        /// <summary>
        /// 用在HBL箱列表中，选择输入所在HBL的箱
        /// </summary>
        public bool IsSelected { get; set; }

        private string _error = "";

        #region IDXDataErrorInfo Members

        void IDXDataErrorInfo.GetPropertyError(string propertyName, ErrorInfo info)
        {
            switch (propertyName)
            {
                case "No":
                    if (string.IsNullOrEmpty(No))
                    {
                        SetErrorInfo(info, "箱号必须填写", ErrorType.Warning);

                        if (string.IsNullOrEmpty(_error) == false)
                        {
                            _error += Environment.NewLine;
                        }
                        _error += "箱号必须填写.";
                    }
                    else
                    {

                        string errorInfo = ValidateContainerHelper.CheckContainerNo(No);
                        if (string.IsNullOrEmpty(errorInfo) == false)
                        {
                            SetErrorInfo(info, errorInfo, ErrorType.Warning);

                            if (string.IsNullOrEmpty(_error) == false)
                            {
                                _error += Environment.NewLine;
                            }
                            _error += errorInfo;
                        }
                    }
                    break;

                case "TypeID":
                    if (TypeID == Guid.Empty)
                    {
                        SetErrorInfo(info, "箱型必须填写.", ErrorType.Warning);

                        if (string.IsNullOrEmpty(_error) == false)
                        {
                            _error += Environment.NewLine;
                        }

                        _error += "箱型必须填写.";
                    }

                    break;
            }
        }
        void IDXDataErrorInfo.GetError(ErrorInfo info) { }
        //</gridControl1>
        #endregion

        private void SetErrorInfo(ErrorInfo info, string errorText, ErrorType errorType)
        {
            info.ErrorText = errorText;
            info.ErrorType = errorType;
        }

    }


}