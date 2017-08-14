namespace UnityMvvm
{
    public interface IViewModel
    {
        void SubscribeView(IView _view);
        void UnsubscribeView(IView _view);
        void NotifySubscribers();
        void OnDependencyChanded();
    }
}
