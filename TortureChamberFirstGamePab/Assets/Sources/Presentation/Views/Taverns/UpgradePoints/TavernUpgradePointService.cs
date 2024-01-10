using System;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.PresentationInterfaces.Views.Taverns.TavernUpgradePoints;
using UnityEngine;

namespace Sources.Presentation.Views.Taverns.UpgradePoints
{
    public class TavernUpgradePointService : MonoBehaviour
    {
        [SerializeField] private TavernUpgradeTrigger _trigger;
        [SerializeField] private TavernUpgradePointView _tavernUpgradePointView; 
        
        //TODO не с первого раза срабатывает триггер
        protected  void OnEnable()
        {
            _trigger.Entered += OnEnter;
            _trigger.Exited += OnExit;
        }

        protected void OnDisable()
        {
            _trigger.Entered -= OnEnter;
            _trigger.Exited -= OnExit;
        }

        private void OnEnter(IPlayerMovementView playerMovementView)
        {
            _tavernUpgradePointView.Show();
        }

        private void OnExit(IPlayerMovementView playerMovementView) => 
            _tavernUpgradePointView.Hide();
        
        //TODO улучшения улучшают сразу все на максимум
    }
}