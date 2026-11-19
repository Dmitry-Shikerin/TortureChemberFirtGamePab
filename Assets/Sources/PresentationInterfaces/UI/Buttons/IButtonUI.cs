using PresentationInterfaces.Views;
using UnityEngine.Events;

namespace PresentationInterfaces.UI.Buttons
{
    public interface IButtonUI : IView
    {
        void AddListener(UnityAction action);
        void RemoveListener(UnityAction action);
        void Enable();
        void Disable();
    }
}