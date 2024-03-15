using System;
using Scripts.Domain.Visitors;
using Scripts.Infrastructure.StateMachines.FiniteStateMachines.States;
using Scripts.InfrastructureInterfaces.Services.ObjectPolls;
using Scripts.PresentationInterfaces.Views.Visitors;

namespace Scripts.Controllers.Visitors.States
{
    public class VisitorReturnToPoolState : FiniteState
    {
        private readonly IObjectPool _objectPool;
        private readonly Visitor _visitor;
        private readonly VisitorCounter _visitorCounter;
        private readonly IVisitorView _visitorView;

        public VisitorReturnToPoolState(
            IVisitorView visitorView,
            Visitor visitor,
            VisitorCounter visitorCounter)
        {
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorCounter = visitorCounter ?? throw new ArgumentNullException(nameof(visitorCounter));
        }

        public override void Enter()
        {
            _visitor.SetHappy();
            _visitor.SetIdle();
            _visitor.SetUnSeat();

            _visitorCounter.RemoveActiveVisitor();

            _visitorView.Destroy();
        }
    }
}