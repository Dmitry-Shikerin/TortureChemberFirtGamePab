using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.Utils.Repositoryes;
using Sources.Voids.GamePoints.VisitorsPoints.Interfaces;

namespace Sources.Infrastructure.Factorys
{
    public class VisitorPointRepositoryFactory
    {
        private readonly RootGamePoints _rootGamePoints;

        public VisitorPointRepositoryFactory(RootGamePoints rootGamePoints)
        {
            _rootGamePoints = rootGamePoints ? rootGamePoints : 
                throw new ArgumentNullException(nameof(rootGamePoints));
        }
        public CollectionRepository Create()
        {
            VisitorPoints visitorPoint = _rootGamePoints.GetComponentInChildren<VisitorPoints>();
            List<IVisitorPoint> visitorPoints =
                visitorPoint.GetComponentsInChildren<IVisitorPoint>().ToList();
            
            if (visitorPoints.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(visitorPoints));
            
            List<SeatPointView> seatPoints = visitorPoint.
                GetComponentsInChildren<SeatPointView>().ToList();
            
            List<OutDoorPoint> outDoorPoints = visitorPoint.
                GetComponentsInChildren<OutDoorPoint>().ToList();
            
            CollectionRepository collectionRepository = new CollectionRepository();
            collectionRepository.Add<SeatPointView>(seatPoints);
            collectionRepository.Add<OutDoorPoint>(outDoorPoints);

            return collectionRepository;
        } 
    }
}