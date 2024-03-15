using Scripts.PresentationInterfaces.Views;
using UnityEngine.Events;

namespace Scripts.PresentationInterfaces.UI.Buttons
{
    public interface IButtonUI : IView
    {
        void AddListener(UnityAction action);
        void RemoveListener(UnityAction action);
        void Enable();
        void Disable();
    }
}