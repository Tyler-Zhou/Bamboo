namespace ICP.Common.ServiceInterface
{
   public interface IKeyboardEventService
    {
       void RegisterHandler(IKeyboardEventHandleService handleService);
       void UnRegisterHandler(IKeyboardEventHandleService handleService);
       void Fire(KeyboardEventInfo eventInfo);
    }
    public class KeyboardEventInfo
    {
    public bool Alt{get;set;}
    public bool Ctrl {get;set;}
    public bool Shift {get;set;}
    public string KeyCode { get; set; }
    }
}
