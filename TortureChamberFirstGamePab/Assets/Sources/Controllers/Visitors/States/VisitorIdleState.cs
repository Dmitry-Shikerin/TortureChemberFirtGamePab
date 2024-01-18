using System;
using Sources.Domain.Visitors;
using Sources.Infrastructure.StateMachines.FiniteStateMachines.States;
using Sources.PresentationInterfaces.Animations;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorIdleState : FiniteState
    {
        private readonly Visitor _visitor;
        private readonly IVisitorAnimation _visitorAnimation;

        public VisitorIdleState(Visitor visitor,
            IVisitorAnimation visitorAnimation )
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorAnimation = visitorAnimation ?? 
                                throw new ArgumentNullException(nameof(visitorAnimation));
        }
        
        public override void Enter()
        {
            _visitorAnimation.PlayIdle();
            _visitor.SetMove();
        }
        
        public override void Exit()
        {
        }
    }
}