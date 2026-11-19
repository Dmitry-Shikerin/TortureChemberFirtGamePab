using System;
using ControllersInterfaces;
using Infrastructure.StateMachines.FiniteStateMachines;
using Infrastructure.StateMachines.FiniteStateMachines.States;
using InfrastructureInterfaces.Services.UpdateServices.Changer;

namespace Controllers.Visitors
{
    public class VisitorPresenter : FiniteStateMachine, IPresenter
    {
        private readonly FiniteState _firstState;
        private readonly IUpdateServiceChanger _updateService;

        public VisitorPresenter(
            FiniteState firstState,
            IUpdateServiceChanger updateService)
        {
            _firstState = firstState ?? throw new ArgumentNullException(nameof(firstState));
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
        }

        public void Enable()
        {
            Start();

            _updateService.ChangedUpdate += OnUpdate;
        }

        public void Disable()
        {
            _updateService.ChangedUpdate -= OnUpdate;

            Stop();
        }

        private void Start() =>
            Start(_firstState);

        private void OnUpdate(float deltaTime) =>
            Update();
    }
}