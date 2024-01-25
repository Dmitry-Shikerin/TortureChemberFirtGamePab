using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Sources.Domain.Visitors;
using Sources.Infrastructure.StateMachines.FiniteStateMachines.States;
using Sources.Presentation.Views.Visitors;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints.Interfaces;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Repositoryes.CollectionRepository;
using UnityEngine;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorMoveToExitState : FiniteState
    {
        private readonly IVisitorView _visitorView;
        private readonly Visitor _visitor;
        private readonly IVisitorAnimation _visitorAnimation;
        private readonly CollectionRepository _collectionRepository;
        private readonly VisitorImageUIContainer _visitorImageUIContainer;

        public VisitorMoveToExitState
        (
            IVisitorView visitorView,
            Visitor visitor,
            IVisitorAnimation visitorAnimation,
            CollectionRepository collectionRepository,
            VisitorInventory visitorInventory,
            VisitorImageUIContainer visitorImageUIContainer
        )
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
            _visitorImageUIContainer.BackGroundImage.HideImage();
            _visitorImageUIContainer.OrderImage.HideImage();
            // Debug.Log("Посетитель в состоянии движения к выходу");
            MovingAsync();
        }

        public override void Exit()
        {
        }

        private async void MovingAsync()
        {
            _visitor.SetMove();
            _visitorAnimation.PlayStandUp();
            _visitorAnimation.PlayWalk();
            await MoveAsync();
            _visitor.SetIdle();
            _visitorView.StopMove();
        }

        private async UniTask MoveAsync()
        {
            IVisitorPoint outDoorPoint = _collectionRepository.Get<OutDoorPoint>().FirstOrDefault();

            _visitorView.SetDestination(outDoorPoint.Position);

            while (Vector3.Distance(_visitorView.Position, outDoorPoint.Position) >
                   _visitorView.NavMeshAgent.stoppingDistance)
            {
                await UniTask.Yield();
            }
        }
    }
}