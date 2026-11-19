using System;
using Domain.Taverns;
using Domain.Visitors;
using Infrastructure.StateMachines.FiniteStateMachines.States;

namespace Controllers.Visitors.States
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