using UnityEngine;

namespace UnityMvvm
{
    public abstract class View : MonoBehaviour, IView
    {
        protected bool IsDirty;
        public void MarkDirty()
        {
            IsDirty = true;

            if (gameObject != null && !gameObject.activeSelf)
                UpdateState();
        }

        void Update()
        {
            if (IsDirty)
            {
                UpdateState();
                IsDirty = false;
            }
        }

        public void BindWith(IViewModel _context)
        {
            _context.SubscribeView(this);
            MarkDirty();
        }

        protected abstract void UpdateState();
    }

    public abstract class View<T> : MonoBehaviour, IView
        where T : class, IViewModel
    {
        private T context;
        public T GetViewModel() { return context; }

        protected bool IsDirty;
        public void MarkDirty()
        {
            IsDirty = true;

            //Ability to update state if inactive
            if (gameObject != null && !gameObject.activeSelf)
                SafeCallUpdateState();
        }

        protected virtual void Update()
        {
            if (IsDirty)
            {
                SafeCallUpdateState();
                IsDirty = false;
            }
        }

        private void SafeCallUpdateState()
        {
            if (GetViewModel() != null)
                UpdateState();
        }

        public void BindWith(IViewModel _context)
        {
            Unsubscribe();

            context = _context as T;
            context.SubscribeView(this);
            MarkDirty();
            InitState();
        }

        public void Unsubscribe()
        {
            if (context != null)
                context.UnsubscribeView(this);
        }
        
        /// <summary>
        /// Will call every time when context notify it
        /// </summary>
        protected abstract void UpdateState();

        /// <summary>
        /// Calls once after binding
        /// </summary>
        protected virtual void InitState()
        { }

        void OnDestroy()
        {
            if (context != null)
                context.UnsubscribeView(this);
        }
    } 
}

