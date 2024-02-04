using Sources.PresentationInterfaces.Views;
using UnityEngine.Events;

namespace Sources.PresentationInterfaces.UI.Buttons
{
    public interface IButtonView : IView
    {
        void AddClickListener(UnityAction onClick);
        void RemoveClickListener(UnityAction onClick);
    }
}