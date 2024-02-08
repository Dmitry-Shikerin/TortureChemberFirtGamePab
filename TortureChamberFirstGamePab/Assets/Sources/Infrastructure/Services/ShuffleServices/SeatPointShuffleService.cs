using System;
using System.Collections.Generic;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.Utils.Repositoryes.CollectionRepository;

namespace Sources.Infrastructure.Services.ShuffleServices
{
    //TODO внедрить этот сеервис в логику
    public class SeatPointShuffleService : ShuffleService<SeatPointView>
    {
        private readonly CollectionRepository _collectionRepository;

        private List<SeatPointView> _items;
        
        public SeatPointShuffleService(CollectionRepository collectionRepository)
        {
            _collectionRepository = collectionRepository ?? 
                                    throw new ArgumentNullException(nameof(collectionRepository));
        }

        //TODO что тут за варнинг
        // protected List<SeatPointView> Items => _items ??= new List<SeatPointView>(
        //     _collectionRepository.Get<SeatPointView>());
        //
        protected override List<SeatPointView> GetItems()
        {
            return _items ??= new List<SeatPointView>(_collectionRepository.Get<SeatPointView>());
        }
    }
}