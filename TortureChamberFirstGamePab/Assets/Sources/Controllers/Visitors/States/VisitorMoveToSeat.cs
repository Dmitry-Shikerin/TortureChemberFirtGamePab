using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Domain.Visitors;
using Sources.Infrastructure.StateMachines.FiniteStateMachines.States;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.Views.Visitors;
using UnityEngine;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorMoveToSeat : FiniteState
    {
        private readonly Visitor _visitor;
        private readonly IVisitorAnimation _visitorAnimation;
        private readonly IVisitorView _visitorView;

        private CancellationTokenSource _cancellationTokenSource;

        public VisitorMoveToSeat(
            IVisitorView visitorView,
            Visitor visitor,
            IVisitorAnimation visitorAnimation)
        {
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorAnimation = visitorAnimation ??
                                throw new ArgumentNullException(nameof(visitorAnimation));
        }

        public override void Enter()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            MovingAsync(_cancellationTokenSource.Token);
        }

        public override void Exit()
        {
            _cancellationTokenSource.Cancel();
        }

        private async void MovingAsync(CancellationToken cancellationToken)
        {
            try
            {
                _visitorAnimation.PlayWalk();
                await MoveAsync(cancellationToken);
                _visitor.SetIdle();
            }
            catch (OperationCanceledException)
            {
            }
        }

        private async UniTask MoveAsync(CancellationToken cancellationToken)
        {
            _visitorView.SetDestination(_visitor.SeatPointView.Position);

            while (Vector3.Distance(_visitorView.Position, _visitor.SeatPointView.Position) >
                   _visitorView.NavMeshAgent.stoppingDistance)
                await UniTask.Yield(cancellationToken);
        }
    }
}