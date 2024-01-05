using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using JetBrains.Annotations;
using Sources.Domain.Visitors;
using Sources.Infrastructure.StateMachines.States;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Repositoryes;
using Sources.Voids.GamePoints.VisitorsPoints.Interfaces;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorInitializeState : FiniteState
    {
        private readonly IVisitorView _visitorView;
        private readonly Visitor _visitor;
        private readonly IVisitorAnimation _visitorAnimation;
        private readonly CollectionRepository _collectionRepository;

        public VisitorInitializeState(IVisitorView visitorView, Visitor visitor,
            IVisitorAnimation visitorAnimation , CollectionRepository collectionRepository)
        {
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorAnimation = visitorAnimation ??
                                throw new ArgumentNullException(nameof(visitorAnimation));
            _collectionRepository = collectionRepository ?? 
                                   throw new ArgumentNullException(nameof(collectionRepository));
        }
        
        public override void Enter()
        {
            List<SeatPointView> seatPoints = _collectionRepository.Get<SeatPointView>();
            //TODO заменить эту запись
            // SeatPointView seatPointView = seatPoints.FirstOrDefault(seatPoint => seatPoint.IsOccupied == false);
            // var seatPointViews = seatPoints.Select(seatPoint => seatPoint.IsOccupied == false).FirstOrDefault();
                                  // throw new NullReferenceException(nameof(seatPointView));
            var seatPointView = seatPoints.Where(
                    seatPointView => seatPointView.IsOccupied == false)
                .Select(seatPointView => seatPointView).FirstOrDefault();
            
            _visitor.SetTargetPosition(seatPointView.Position);
            _visitor.SetSeatPoint(seatPointView);
            seatPointView.SetIsOccupied(true);
        }

        public override void Exit()
        {
        }
    }
}