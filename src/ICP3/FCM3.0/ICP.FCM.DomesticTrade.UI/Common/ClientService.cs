﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ICP.FCM.DomesticTrade.UI
{
    public interface IChildPart
    {
        event EventHandler DataChanged;
        bool IsChanged { get;}
        bool ValidateData();
        void AfterSaved();
        object DataSource { get; }
        void SetSource(object value);
        void SetService(Microsoft.Practices.CompositeUI.WorkItem workitem);
    }
}
