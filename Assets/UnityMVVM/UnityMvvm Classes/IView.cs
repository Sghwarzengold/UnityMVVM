namespace UnityMvvm
{
    /// <summary>
    /// Base UI interface. MUST use for all representations
    /// </summary>
    public interface IView
    {
        void BindWith(IViewModel context);
        void MarkDirty();
    }
}
