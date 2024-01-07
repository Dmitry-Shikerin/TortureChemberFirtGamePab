using System;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Controllers.Taverns;
using Sources.PresentationInterfaces.Views.Taverns.TavernUpgradePoints;
using UnityEngine;

namespace Sources.Presentation.Views.Taverns.UpgradePoints
{
    public class TavernUpgradePointView : PresentableView<TavernUpgradePointPresenter>, ITavernUpgradePoint
    {
        //TODO нормально ли получилось?
        [SerializeField] private TavernUpgradeTrigger _trigger;
        
        //TODO как лучше сделать запуск этого окна?
        private void Start()
        {
            Hide();
            _trigger.Entered += OnEnter;
            _trigger.Exited += OnExit;
        }

        protected override void OnAfterEnable()
        {
            // _trigger.Entered += OnEnter;
            // _trigger.Exited += OnExit;
        }

        //TODO как быть с этими подписками?
        protected override void OnAfterDisable()
        {
            _trigger.Entered -= OnEnter;
            _trigger.Exited -= OnExit;
        }

        private void OnEnter(IPlayerMovementView playerMovementView) => 
            Show();

        private void OnExit(IPlayerMovementView playerMovementView) => 
            Hide();
    }
}