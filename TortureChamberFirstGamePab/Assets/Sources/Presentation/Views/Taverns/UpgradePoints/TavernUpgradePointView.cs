using UnityEngine;

namespace Sources.Presentation.Views.Taverns.UpgradePoints
{
    public class TavernUpgradePointView : MonoBehaviour
    {
        public void Show() => 
            gameObject.SetActive(true);

        public void Hide() => 
            gameObject.SetActive(false);
    }
}