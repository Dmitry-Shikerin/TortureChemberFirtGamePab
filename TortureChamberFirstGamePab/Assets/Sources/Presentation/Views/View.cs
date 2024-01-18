using Sources.PresentationInterfaces.Views;
using UnityEngine;

namespace Sources.Presentation.Views
{
    public abstract class View : MonoBehaviour,IView
    {
        public void Hide() => 
            gameObject.SetActive(false);

        public void Show() => 
            gameObject.SetActive(true);
    }
}