#region Comment

/*
 * 
 * FileName:    Presenter.cs
 * CreatedOn:   2014/5/14 星期三 17:01:21
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->逻辑处理基类
 * History：
 * 
 * 
 * 
 * 
 */

#endregion


namespace ICP.Document
{
    public class Presenter<IView>
    {
        public IView View { get; private set; }

        public Presenter(IView view)
        {
            this.View = view;
            this.OnViewSet();
        }
        protected virtual void OnViewSet()
        { }
    }
}
