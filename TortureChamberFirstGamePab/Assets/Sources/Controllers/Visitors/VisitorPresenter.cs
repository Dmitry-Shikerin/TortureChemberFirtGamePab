using System;
using Sources.ControllersInterfaces;
using Sources.Domain.Visitors;
using Sources.Infrastructure.StateMachines.FiniteStateMachines;
using Sources.Infrastructure.StateMachines.FiniteStateMachines.States;
using Sources.InfrastructureInterfaces.Services;
using Sources.InfrastructureInterfaces.Services.UpdateServices.Changer;
using Sources.PresentationInterfaces.Views;
using Sources.PresentationInterfaces.Views.Visitors;

namespace Sources.Controllers.Visitors
{
    public class VisitorPresenter : FiniteStateMachine, IPresenter
    {
        private readonly FiniteState _firstState;
        private readonly IVisitorView _visitorView;
        private readonly Visitor _visitor;
        private readonly IUpdateServiceChanger _updateService;

        public VisitorPresenter
        (
            FiniteState firstState,
            IVisitorView visitorView,
            Visitor visitor,
            IUpdateServiceChanger updateService
        )
        {
            _firstState = firstState ?? throw new ArgumentNullException(nameof(firstState));
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
        }

        public void Enable()
        {
            //TODO два раза входит в инит стейт
            Start();
            
            _updateService.ChangedUpdate += OnUpdate;
        }

        public void Disable()
        {
            _updateService.ChangedUpdate -= OnUpdate;
            
            Stop();
        }

        public void Start() => 
            Start(_firstState);

        public void OnUpdate(float deltaTime) => 
            Update();
    }
}