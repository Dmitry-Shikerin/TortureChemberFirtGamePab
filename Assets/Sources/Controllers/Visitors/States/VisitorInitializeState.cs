using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Visitors;
using Infrastructure.StateMachines.FiniteStateMachines.States;
using Presentation.Containers.UI.Images;
using Presentation.Views.GamePoints.VisitorsPoints;
using Presentation.Views.Visitors;
using PresentationInterfaces.Views.Visitors;
using Utils.Extensions.ShuffleExtensions;
using Utils.Repositories.CollectionRepository;

namespace Controllers.Visitors.States
{
    public class VisitorInitializeState : FiniteState
    {
        private readonly CollectionRepository _collectionRepository;
        private readonly Visitor _visitor;
        private readonly VisitorImageUIContainer _visitorImageUIContainer;
        private readonly IVisitorView _visitorView;

        public VisitorInitializeState(
            IVisitorView visitorView,
            Visitor visitor,
            CollectionRepository collectionRepository,
            VisitorImageUIContainer visitorImageUIContainer)
        {
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
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

            var activeMeshSkin = _visitorView.MeshSkins.GetRandomItem();

            _visitorView.MeshSkins
                .Except(new List<MeshSkinView> { activeMeshSkin, })
                .ToList()
                .ForEach(meshSkin => meshSkin.Hide());

            activeMeshSkin.Show();

            var seatPointView = _collectionRepository
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