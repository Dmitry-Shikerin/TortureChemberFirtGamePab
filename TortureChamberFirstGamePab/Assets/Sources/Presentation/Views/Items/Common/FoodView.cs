using Sources.DomainInterfaces.Items;
using Sources.PresentationInterfaces.Views;
using UnityEngine;

namespace Sources.Presentation.Views.Items
{
    public class FoodView : MonoBehaviour, IItemView
    {
        public void SetParent(Transform parentTransform) => 
            transform.SetParent(parentTransform);

        public void SetPosition(Transform parentTransform) => 
            transform.position = parentTransform.position;

        public void Destroy() =>
            Destroy(gameObject);
    }
}