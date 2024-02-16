using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Domain.Visitors;
using Sources.Infrastructure.StateMachines.FiniteStateMachines.States;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.Views;
using Sources.PresentationInterfaces.Views.Visitors;
using Sources.Utils.Repositoryes;
using Sources.Utils.Repositoryes.CollectionRepository;
using UnityEngine;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorMoveToSeat : FiniteState
    {
        private readonly IVisitorView _visitorView;
        private readonly Visitor _visitor;
        private readonly IVisitorAnimation _visitorAnimation;
        private readonly IPauseService _pauseService;

        private CancellationTokenSource _cancellationTokenSource;

        public VisitorMoveToSeat
        (
            IVisitorView visitorView,
            Visitor visitor,
            IVisitorAnimation visitorAnimation,
            CollectionRepository collectionRepository,
            IPauseService pauseService
        )
        {
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorAnimation = visitorAnimation ??
                                throw new ArgumentNullException(nameof(visitorAnimation));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
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
            {
                await UniTask.Yield(cancellationToken);
            }
        }
    }
}