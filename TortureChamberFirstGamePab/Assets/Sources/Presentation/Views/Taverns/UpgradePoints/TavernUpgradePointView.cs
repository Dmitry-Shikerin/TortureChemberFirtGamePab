using System;
using UnityEngine;

namespace Sources.Presentation.Views.Taverns.UpgradePoints
{
    public class TavernUpgradePointView : MonoBehaviour
    {
        private void Awake()
        {
            // Debug.Log("Awake");
            //  Hide();
        }

        public void Show()
        {
            Debug.Log("Show");

            gameObject.SetActive(true);
        }

        public void Hide() => 
            gameObject.SetActive(false);
    }
}