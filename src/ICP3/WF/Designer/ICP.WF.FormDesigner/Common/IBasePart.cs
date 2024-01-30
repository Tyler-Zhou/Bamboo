namespace ICP.WF.FormDesigner
{
    using System.ComponentModel.Design;

    public interface IBasePart
    {
        event ExceptionEventHandler OnException;

        IDesignerHost CurrentDesignerHost { get; set; }
    }
}
