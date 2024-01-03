using System;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Controllers.Taverns;
using Sources.Presentation.Views.Taverns.UpgradePoints;
using UnityEngine;

namespace Sources.Presentation.Views.Taverns
{
    public class TavernUpgradeTrigger : MonoBehaviour
    {
        [SerializeField] private TavernUpgradePointView _tavernUpgradePointView;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IPlayerMovementView playerInventoryView))
            {
                Debug.Log("игрок вошел");
                _tavernUpgradePointView.Show();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IPlayerMovementView playerInventoryView))
            {
                Debug.Log("игрок вышел");
                _tavernUpgradePointView.Hide();

            }
        }
    }
}