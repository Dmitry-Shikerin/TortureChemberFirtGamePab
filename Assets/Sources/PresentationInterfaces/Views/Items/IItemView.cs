using UnityEngine;

namespace PresentationInterfaces.Views.Items
{
    public interface IItemView
    {
        void SetTransformPosition(Transform parentTransform);
        void SetParent(Transform parentTransform);
        void Destroy();
    }
}