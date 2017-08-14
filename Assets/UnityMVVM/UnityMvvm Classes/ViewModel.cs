using System;
using System.Collections.Generic;

namespace UnityMvvm
{
    /// <summary>
    /// Class implements base functionality for ViewModel classes and connect Data with View
    /// </summary>
    public abstract class ViewModel : IViewModel
    {
        public event Action OnChanged;

        private static List<WeakReference> liveViewModels = new List<WeakReference>();

        private List<IView> subscribers = new List<IView>();
        private List<IViewModel> dependers = new List<IViewModel>();

        public ViewModel()
        {
            liveViewModels.Add(new WeakReference(this));
        }

        public static void Flush()
        {
            foreach (var item in liveViewModels)
                if (item.IsAlive)
                    (item.Target as ViewModel).UnsubscribeAll();
            liveViewModels.Clear();
        }

        public void NotifySubscribers()
        {
            for (int i = 0; i < subscribers.Count; ++i)
            {
                if (subscribers[i] != null
                    && !subscribers[i].Equals(null)) continue;
                subscribers.RemoveAt(i);
                i--;
            }
            foreach (var s in subscribers)
                if (!s.Equals(null))
                    s.MarkDirty();

            for (int i = 0; i < dependers.Count; ++i)
            {
                if (dependers[i] != null
                    && !dependers[i].Equals(null)) continue;
                dependers.RemoveAt(i);
                i--;
            }
            foreach (var s in dependers)
                if (!s.Equals(null))
                    s.NotifySubscribers();

            if (OnChanged != null)
                OnChanged();
        }

        public void SubscribeView(IView _view)
        {
            if (subscribers.Find(v => v.Equals(_view)) == null)
                subscribers.Add(_view);
        }

        public void UnsubscribeView(IView _view)
        {
            if (subscribers.Find(v => v.Equals(_view)) != null)
                subscribers.Remove(_view);
        }

        public void UnsubscribeAll()
        {
            subscribers.Clear();
        }

        public void AddDependecy(IViewModel _context)
        {
            if (dependers.Find(d => d.Equals(_context)) == null)
            {
                dependers.Add(_context);
                OnChanged += _context.OnDependencyChanded;
            }
        }

        public void RemoveDependecy(IViewModel _context)
        {
            if (dependers.Find(d => d.Equals(_context)) != null)
            {
                dependers.Remove(_context);
                OnChanged -= _context.OnDependencyChanded;
            }
        }

        public virtual void OnDependencyChanded() { }
    } 
}
