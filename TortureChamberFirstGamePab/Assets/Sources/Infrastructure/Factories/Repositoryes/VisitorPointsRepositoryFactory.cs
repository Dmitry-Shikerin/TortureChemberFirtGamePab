using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Infrastructure.Factories.Views.Points;
using Sources.Presentation.Containers.GamePoints;
using Sources.Presentation.Views.GamePoints.VisitorsPoints;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.Utils.Repositoryes.CollectionRepository;

namespace Sources.Infrastructure.Factories.Repositoryes
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
            var seatPoints = new List<SeatPointView>();

            foreach (var seatPointView in _rootGamePoints.GetComponentsInChildren<SeatPointView>())
            {
                _seatPointViewFactory.Create(seatPointView);
                _eatPointViewFactory.Create(seatPointView.EatPointView);
                seatPoints.Add(seatPointView);
            }

            var outDoorPoints = _rootGamePoints.GetComponentsInChildren<OutDoorPoint>().ToList();

            _collectionRepository.Add(seatPoints);
            _collectionRepository.Add(outDoorPoints);

            return _collectionRepository;
        }
    }
}