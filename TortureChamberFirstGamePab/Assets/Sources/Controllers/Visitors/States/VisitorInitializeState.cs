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
        private readonly Visitor _visitor;
        private readonly CollectionRepository _collectionRepository;

        public VisitorInitializeState(IVisitorView visitorView, Visitor visitor,
            IVisitorAnimation visitorAnimation , CollectionRepository collectionRepository)
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _collectionRepository = collectionRepository ?? 
                                   throw new ArgumentNullException(nameof(collectionRepository));
        }
        
        public override void Enter()
        {
            List<SeatPointView> seatPoints = _collectionRepository.Get<SeatPointView>();
            var seatPointView = seatPoints.FirstOrDefault(seatPointView => seatPointView.IsOccupied == false);
            
            _visitor.SetTargetPosition(seatPointView.Position);
            _visitor.SetSeatPoint(seatPointView);
            seatPointView.Occupy();
        }

        public override void Exit()
        {
        }
    }
}