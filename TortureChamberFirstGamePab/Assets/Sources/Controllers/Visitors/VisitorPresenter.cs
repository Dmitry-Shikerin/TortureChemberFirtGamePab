using System;
using Sources.ControllersInterfaces;
using Sources.Domain.Visitors;
using Sources.Infrastructure.StateMachines.FiniteStateMachines;
using Sources.Infrastructure.StateMachines.FiniteStateMachines.States;
using Sources.PresentationInterfaces.Views;

namespace Sources.Controllers.Visitors
{
    public class VisitorPresenter : FiniteStateMachine, IPresenter
    {
        private readonly FiniteState _firstState;
        private readonly IVisitorView _visitorView;
        private readonly Visitor _visitor;

        public VisitorPresenter(FiniteState firstState, IVisitorView visitorView, Visitor visitor)
        {
            _firstState = firstState ?? throw new ArgumentNullException(nameof(firstState));
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
        } 
    
        public void Start()
        {
            Start(_firstState);
        }

        public void Enable()
        {
        }

        public void Disable()
        {
        }
    }
}
