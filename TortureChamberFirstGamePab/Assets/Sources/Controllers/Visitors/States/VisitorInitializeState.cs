using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Domain.Visitors;
using Sources.Infrastructure.StateMachines.FiniteStateMachines.States;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.Views;
using Sources.PresentationInterfaces.Views.Visitors;
using Sources.Utils.Extensions.ShuffleExtensions;
using Sources.Utils.Repositoryes.CollectionRepository;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorInitializeState : FiniteState
    {
        private readonly Visitor _visitor;
        private readonly CollectionRepository _collectionRepository;
        private readonly VisitorCounter _visitorCounter;

        public VisitorInitializeState
        (
            IVisitorView visitorView,
            Visitor visitor,
            IVisitorAnimation visitorAnimation,
            CollectionRepository collectionRepository,
            VisitorCounter visitorCounter
        )
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _collectionRepository = collectionRepository ??
                                    throw new ArgumentNullException(nameof(collectionRepository));
            _visitorCounter = visitorCounter ?? throw new ArgumentNullException(nameof(visitorCounter));
        }

        public override void Enter()
        {
            SeatPointView seatPointView = _collectionRepository
                .Get<SeatPointView>()
                .Where(seatPointView => seatPointView.IsOccupied == false)
                .GetRandomItem();

            _visitor.SetTargetPosition(seatPointView.Position);
            _visitor.SetSeatPoint(seatPointView);
            seatPointView.Occupy();
        }

        public override void Exit()
        {
        }
    }
}