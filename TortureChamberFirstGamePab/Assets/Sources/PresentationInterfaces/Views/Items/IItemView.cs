using UnityEngine;

namespace Sources.PresentationInterfaces.Views
{
    public interface IItemView
    {
        void SetTransformPosition(Transform parentTransform);
        void SetParent(Transform parentTransform);
        void Destroy();
    }
}