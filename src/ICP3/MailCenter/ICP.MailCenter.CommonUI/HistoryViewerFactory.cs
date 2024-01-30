using ICP.Message.ServiceInterface;
using Microsoft.Practices.CompositeUI;

namespace ICP.MailCenter.CommonUI
{
  public sealed  class HistoryViewerFactory
    { 
      [ServiceDependency]
      public WorkItem WorkItem { get; set; }
      public IHistoryViewer GetViewer(MessageType type)
      {
          return null;
          //if (type == MessageType.Email)
          //{
          //    EmailViewer viewer = WorkItem.Items.Get<EmailViewer>("EmailViewer");
          //    if (viewer == null)
          //    {
          //       viewer= WorkItem.Items.AddNew<EmailViewer>("EmailViewer");
          //    }
          //    return viewer;
          //}
          //else if (type == MessageType.Fax)
          //{
          //    FaxViewer viewer = WorkItem.Items.Get<FaxViewer>("FaxViewer");
          //    if (viewer == null)
          //    {
          //        viewer = WorkItem.Items.AddNew<FaxViewer>("FaxViewer");
          //    }
          //    return viewer;
              
          //}
          //else if (type == MessageType.EDI)
          //{
          //    EDIViewer viewer = WorkItem.Items.Get<EDIViewer>("EDIViewer");
          //    if (viewer == null)
          //    {
          //        viewer = WorkItem.Items.AddNew<EDIViewer>("EDIViewer");
          //    }
          //    return viewer;
          //}
          //else
          //    throw new NotImplementedException();

      }
    }
}
