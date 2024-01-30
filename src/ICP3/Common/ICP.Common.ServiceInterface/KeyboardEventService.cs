using System.Collections.Generic;

namespace ICP.Common.ServiceInterface
{
   public class KeyboardEventService:IKeyboardEventService
    {
        #region IKeyboardEventService 成员
       private List<IKeyboardEventHandleService> services = new List<IKeyboardEventHandleService>();
        public void RegisterHandler(IKeyboardEventHandleService handleService)
        {
            if (services.Contains(handleService))
            {
                return;
            }
            services.Add(handleService);
        }
        public void Fire(KeyboardEventInfo eventInfo)
        {
            foreach (IKeyboardEventHandleService handleService in services)
            {
                handleService.Handle(eventInfo);
            }
        }
        public void UnRegisterHandler(IKeyboardEventHandleService handleService)
        { 
         if(services.Contains(handleService))
         {
             services.Remove(handleService);
         }
        }


        #endregion
    }
}
