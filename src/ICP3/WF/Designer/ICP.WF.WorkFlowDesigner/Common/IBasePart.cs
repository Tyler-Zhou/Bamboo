namespace ICP.WF.WorkFlowDesigner
{
    using System.ComponentModel.Design;
    using DevExpress.XtraEditors.Controls;

    public interface IBasePart
    {
        event ExceptionEventHandler OnException;

    }
}
