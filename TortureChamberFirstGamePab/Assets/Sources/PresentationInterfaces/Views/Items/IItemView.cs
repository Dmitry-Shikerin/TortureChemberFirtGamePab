using UnityEngine;

namespace Sources.PresentationInterfaces.Views
{
    public interface IItemView
    {
        void SetPosition(Transform parentTransform);
        void SetParent(Transform parentTransform);
        void Destroy();
    }
}