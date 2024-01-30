using System;
using Microsoft.Practices.CompositeUI;


namespace ICP.FCM.AirExport.UI
{
    public interface IChildPart
    {
        event EventHandler DataChanged;
        bool IsChanged { get;}
        bool ValidateData();
        void AfterSaved();
        object DataSource { get; }
        void SetSource(object value);
        void SetService(WorkItem workitem);
    }
}
