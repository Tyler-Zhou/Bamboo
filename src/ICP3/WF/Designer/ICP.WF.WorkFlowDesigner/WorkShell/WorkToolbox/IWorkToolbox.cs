
namespace ICP.WF.WorkFlowDesigner
{
    using System.ComponentModel.Design;
    using System.Drawing.Design;

    public interface IWorkToolbox : IToolboxService, IBasePart
    {
        IDesignerHost CurrentDesignerHost { get; set; }
    }
}
