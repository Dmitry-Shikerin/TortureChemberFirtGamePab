using System;
using Sources.ControllersInterfaces;

namespace Sources.Presentation.Views
{
    public abstract class PresentableView<T> : View
        where T : IPresenter
    {
        protected T Presenter { get; private set; }

        public void OnEnable()
        {
            Presenter?.Enable();
            OnAfterEnable();
        }

        public void OnDisable()
        {
            Presenter?.Disable();
            OnAfterDisable();
        }

        public void Construct(T presenter)
        {
            Hide();
            Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            Show();
            OnConstructed();
        }

        protected void DestroyPresenter()
        {
            Presenter = default;
        }

        protected virtual void OnAfterEnable()
        {
        }

        protected virtual void OnAfterDisable()
        {
        }

        protected virtual void OnConstructed()
        {
        }
    }
}