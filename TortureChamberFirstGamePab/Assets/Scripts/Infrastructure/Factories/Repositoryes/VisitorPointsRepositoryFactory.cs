using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Infrastructure.Factories.Views.Points;
using Scripts.Presentation.Containers.GamePoints;
using Scripts.Presentation.Views.GamePoints.VisitorsPoints;
using Scripts.Utils.Repositories.CollectionRepository;

namespace Scripts.Infrastructure.Factories.Repositoryes
{
    public class VisitorPointsRepositoryFactory
    {
        private readonly CollectionRepository _collectionRepository;
        private readonly EatPointViewFactory _eatPointViewFactory;
        private readonly RootGamePoints _rootGamePoints;
        private readonly SeatPointViewFactory _seatPointViewFactory;

        public VisitorPointsRepositoryFactory(
            RootGamePoints rootGamePoints,
            CollectionRepository collectionRepository,
            SeatPointViewFactory seatPointViewFactory,
            EatPointViewFactory eatPointViewFactory)
        {
            _collectionRepository = collectionRepository ??
                                    throw new ArgumentNullException(nameof(collectionRepository));
            _seatPointViewFactory = seatPointViewFactory ??
                                    throw new ArgumentNullException(nameof(seatPointViewFactory));
            _eatPointViewFactory = eatPointViewFactory ??
                                   throw new ArgumentNullException(nameof(eatPointViewFactory));
            _rootGamePoints = rootGamePoints
                ? rootGamePoints
                : throw new ArgumentNullException(nameof(rootGamePoints));
        }

        public CollectionRepository Create()
        {
            List<SeatPointView> seatPoints = new List<SeatPointView>();

            foreach (SeatPointView seatPointView in _rootGamePoints.GetComponentsInChildren<SeatPointView>())
            {
                _seatPointViewFactory.Create(seatPointView);
                _eatPointViewFactory.Create(seatPointView.EatPointView);
                seatPoints.Add(seatPointView);
            }

            List<OutDoorPoint> outDoorPoints = _rootGamePoints.GetComponentsInChildren<OutDoorPoint>().ToList();

            _collectionRepository.Add(seatPoints);
            _collectionRepository.Add(outDoorPoints);

            return _collectionRepository;
        }
    }
}