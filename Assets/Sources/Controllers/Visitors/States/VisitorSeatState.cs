using System;
using Domain.Taverns;
using Domain.Visitors;
using Infrastructure.StateMachines.FiniteStateMachines.States;
using PresentationInterfaces.Animations;
using PresentationInterfaces.Views.Visitors;

namespace Controllers.Visitors.States
{
    public class VisitorSeatState : FiniteState
    {
        private readonly TavernMood _tavernMood;
        private readonly Visitor _visitor;
        private readonly IVisitorAnimation _visitorAnimation;
        private readonly IVisitorView _visitorView;

        public VisitorSeatState(
            IVisitorView visitorView,
            Visitor visitor,
            IVisitorAnimation visitorAnimation,
            TavernMood tavernMood)
        {
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorAnimation = visitorAnimation ??
                                throw new ArgumentNullException(nameof(visitorAnimation));
            _tavernMood = tavernMood ?? throw new ArgumentNullException(nameof(tavernMood));
        }

        public override void Enter()
        {
            _visitorView.SeatDown(_visitor.SeatPointView.Position, _visitor.SeatPointView.Rotation);

            if (_visitor.SeatPointView.EatPointView.IsClear == false)
                _tavernMood.RemoveTavernMood();

            _visitorAnimation.PlaySeatIdle();
            _visitor.SetSeat();
        }
    }
}