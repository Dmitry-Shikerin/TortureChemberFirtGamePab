using Sources.PresentationInterfaces.Views;
using UnityEngine;

namespace Sources.Presentation.Views.Items
{
    public class BeerView : MonoBehaviour, IItemView
    {
        public void SetParent(Transform parentTransform)
        {
            transform.position = parentTransform.position;
            transform.parent = parentTransform;
        }
    }
}