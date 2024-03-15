using System;
using Scripts.Domain.Taverns;
using Scripts.Domain.Visitors;
using Scripts.Infrastructure.StateMachines.FiniteStateMachines.States;

namespace Scripts.Controllers.Visitors.States
{
    public class VisitorNotSatisfiedWithOrderState : FiniteState
    {
        private readonly TavernMood _tavernMood;
        private readonly Visitor _visitor;

        public VisitorNotSatisfiedWithOrderState(Visitor visitor, TavernMood tavernMood)
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _tavernMood = tavernMood ?? throw new ArgumentNullException(nameof(tavernMood));
        }

        public override void Enter()
        {
            _visitor.SetUnHappy();
            _tavernMood.RemoveTavernMood();
        }
    }
}