using Sources.PresentationInterfaces.Views;
using UnityEngine.Events;

namespace Sources.PresentationInterfaces.UI
{
    public interface IButtonUI : IView
    {
        void AddListener(UnityAction action);
        void RemoveListener(UnityAction action);
        void Enable();
        void Disable();
    }
}