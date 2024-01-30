//-----------------------------------------------------------------------
// <copyright file="PickUpDescription.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.OceanExport.ServiceInterface.DataObjects
{
    using System;
    using DevExpress.XtraEditors.DXErrorProvider;
    using ICP.FCM.Common.ServiceInterface.Common;

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
    public partial class OceanContainerList : IDXDataErrorInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public OceanContainerList()
        {
            this.IsSelected = true;
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
                    if (string.IsNullOrEmpty(this.No))
                    {
                        //SetErrorInfo(info, "箱号必须填写", ErrorType.Warning);

                        //if (string.IsNullOrEmpty(_error) == false)
                        //{
                        //    _error += System.Environment.NewLine;
                        //}
                        //_error += "箱号必须填写.";
                    }
                    else
                    {

                        string errorInfo = ValidateContainerHelper.CheckContainerNo(this.No);
                        if (string.IsNullOrEmpty(errorInfo) == false)
                        {
                            SetErrorInfo(info, errorInfo, ErrorType.Warning);

                            if (string.IsNullOrEmpty(_error) == false)
                            {
                                _error += System.Environment.NewLine;
                            }
                            _error += errorInfo;
                        }
                    }
                    break;

                case "TypeID":
                    if (this.TypeID == System.Guid.Empty)
                    {
                        SetErrorInfo(info, "箱型必须填写.", ErrorType.Warning);

                        if (string.IsNullOrEmpty(_error) == false)
                        {
                            _error += System.Environment.NewLine;
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