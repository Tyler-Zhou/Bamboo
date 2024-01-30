using System;
using System.Collections.Generic;
using System.Text;

namespace LongWin.DataWarehouseReport.WinUI
{
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void CompanySelectChangedEventHandler(object sender, CompanySelectChangedEventArgs e);
    /// <summary>
    /// 保存后事件参数

    /// </summary>
    public class CompanySelectChangedEventArgs : System.EventArgs
    {
        private object _selectValue;
        public object SelectValue
        {
            get { return this._selectValue; }
            set { _selectValue = value; }
        }
    }
}
