using UnityEngine;

namespace Sources.PresentationInterfaces.Views
{
    public interface IView
    {
        void Show();
        void Hide();
        void SetParent(Transform parentTransform);
        void SetTransformPosition(Transform parentTransform);
        void Destroy();
    }
}