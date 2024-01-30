
namespace ICP.MailCenter.CommonUI
{
   public interface IReadOnlyControl
    {
       bool ReadOnly { get; set; }
       void SetChildControlReadOnly();
    }
}
