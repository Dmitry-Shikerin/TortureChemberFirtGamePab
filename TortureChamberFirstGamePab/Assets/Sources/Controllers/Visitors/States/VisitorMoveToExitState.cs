using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Domain.Visitors;
using Sources.Infrastructure.StateMachines.FiniteStateMachines.States;
using Sources.Presentation.Views.Visitors;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints.Interfaces;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.Views.Visitors;
using Sources.Utils.Repositoryes.CollectionRepository;
using UnityEngine;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorMoveToExitState : FiniteState
    {
        private readonly CollectionRepository _collectionRepository;
        private readonly Visitor _visitor;
        private readonly IVisitorAnimation _visitorAnimation;
        private readonly VisitorImageUIContainer _visitorImageUIContainer;
        private readonly IVisitorView _visitorView;

        private CancellationTokenSource _cancellationTokenSource;

        public VisitorMoveToExitState(
            IVisitorView visitorView,
            Visitor visitor,
            IVisitorAnimation visitorAnimation,
            CollectionRepository collectionRepository,
            VisitorImageUIContainer visitorImageUIContainer)
        {
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorAnimation = visitorAnimation ??
                                throw new ArgumentNullException(nameof(visitorAnimation));
            _collectionRepository = collectionRepository ??
                                    throw new ArgumentNullException(nameof(collectionRepository));
            _visitorImageUIContainer = visitorImageUIContainer
                ? visitorImageUIContainer
                : throw new ArgumentNullException(nameof(visitorImageUIContainer));
        }

        public override void Enter()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            _visitorImageUIContainer.BackGroundImage.HideImage();
            _visitorImageUIContainer.OrderImage.HideImage();

            MovingAsync(_cancellationTokenSource.Token);
        }

        public override void Exit()
        {
            _cancellationTokenSource.Cancel();
        }

        private async void MovingAsync(CancellationToken cancellationToken)
        {
            _visitor.SetMove();
            _visitorAnimation.PlayStandUp();
            _visitorAnimation.PlayWalk();

            await MoveAsync(cancellationToken);

            _visitor.SetIdle();
            _visitorView.StopMove();
        }

        private async UniTask MoveAsync(CancellationToken cancellationToken)
        {
            try
            {
                IVisitorPoint outDoorPoint = _collectionRepository.Get<OutDoorPoint>().FirstOrDefault();

                _visitorView.SetDestination(outDoorPoint.Position);

                while (Vector3.Distance(_visitorView.Position, outDoorPoint.Position) >
                       _visitorView.NavMeshAgent.stoppingDistance)
                    await UniTask.Yield(cancellationToken);
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}