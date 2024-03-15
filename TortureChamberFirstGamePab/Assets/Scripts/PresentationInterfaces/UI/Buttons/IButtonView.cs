using Scripts.PresentationInterfaces.Views;
using UnityEngine.Events;

namespace Scripts.PresentationInterfaces.UI.Buttons
{
    public interface IButtonView : IView
    {
        void AddClickListener(UnityAction onClick);
        void RemoveClickListener(UnityAction onClick);
    }
}