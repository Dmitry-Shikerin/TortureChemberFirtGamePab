using Scripts.PresentationInterfaces.Views;
using UnityEngine;

namespace Scripts.Presentation.Views
{
    public abstract class View : MonoBehaviour, IView
    {
        public void Hide() =>
            gameObject.SetActive(false);

        public virtual void Show() =>
            gameObject.SetActive(true);

        public void SetParent(Transform parentTransform) =>
            transform.SetParent(parentTransform);

        public void SetTransformPosition(Transform parentTransform) =>
            transform.position = parentTransform.position;

        public virtual void Destroy() =>
            Destroy(gameObject);
    }
}