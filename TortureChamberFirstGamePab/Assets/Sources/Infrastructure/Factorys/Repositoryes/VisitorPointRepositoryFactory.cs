using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Sources.Infrastructure.Services;
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
        public CollectionRepozitory Create()
        {
            VisitorPoints visitorPoint = _rootGamePoints.GetComponentInChildren<VisitorPoints>();
            // //TODO плохое название? и плохой каст?
            List<IVisitorPoint> visitorPoints =
                visitorPoint.GetComponentsInChildren<IVisitorPoint>().ToList();
            
            if (visitorPoints.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(visitorPoints));
            
            List<SeatPoint> seatPoints = visitorPoint.
                GetComponentsInChildren<SeatPoint>().ToList();
            
            List<OutDoorPoint> outDoorPoints = visitorPoint.
                GetComponentsInChildren<OutDoorPoint>().ToList();
            
            //TODO надеюсь заработает
            CollectionRepozitory collectionRepozitory = new CollectionRepozitory();
            collectionRepozitory.Add<SeatPoint>(seatPoints);
            collectionRepozitory.Add<OutDoorPoint>(outDoorPoints);

            return collectionRepozitory;
        } 
    }
}