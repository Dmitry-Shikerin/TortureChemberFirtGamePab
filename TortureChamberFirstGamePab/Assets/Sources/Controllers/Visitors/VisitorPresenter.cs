using System;
using Sources.ControllersInterfaces;
using Sources.Domain.Visitors;
using Sources.Infrastructure.StateMachines.FiniteStateMachines;
using Sources.Infrastructure.StateMachines.FiniteStateMachines.States;
using Sources.InfrastructureInterfaces.Services.UpdateServices.Changer;
using Sources.PresentationInterfaces.Views.Visitors;

namespace Sources.Controllers.Visitors
{
    public class VisitorPresenter : FiniteStateMachine, IPresenter
    {
        private readonly FiniteState _firstState;
        private readonly IUpdateServiceChanger _updateService;
        private readonly Visitor _visitor;
        private readonly IVisitorView _visitorView;

        public VisitorPresenter(
            FiniteState firstState,
            IVisitorView visitorView,
            Visitor visitor,
            IUpdateServiceChanger updateService)
        {
            _firstState = firstState ?? throw new ArgumentNullException(nameof(firstState));
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
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

        private void Start()
        {
            Start(_firstState);
        }

        private void OnUpdate(float deltaTime)
        {
            Update();
        }
    }
}