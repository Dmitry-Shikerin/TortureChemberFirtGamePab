﻿using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using ModestTree;
using Sources.Domain.Constants;
using Sources.Domain.GamePlays;
using Sources.Domain.Points;
using Sources.Domain.Taverns;
using Sources.Domain.Visitors;
using Sources.Infrastructure.Factories.Views.Visitors;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.Infrastructure.Services.Providers.Taverns;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
using Sources.Presentation.Views.Visitors;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Repositoryes.CollectionRepository;
using UnityEngine;

namespace Sources.Infrastructure.Services
{
    public class VisitorSpawnService
    {
        private readonly VisitorCounter _visitorCounter;
        private readonly IPrefabFactory _prefabFactory;
        private readonly ObjectPool<VisitorView> _objectPool;
        private readonly VisitorViewFactory _visitorViewFactory;
        private readonly ITavernProvider _tavernProvider;
        private readonly CollectionRepository _collectionRepository;

        private TavernMood _tavernMood;
        private GamePlay _gamePlay;

        private CancellationTokenSource _cancellationTokenSource;

        public VisitorSpawnService
        (
            IPrefabFactory prefabFactory,
            ObjectPool<VisitorView> objectPool,
            VisitorViewFactory visitorViewFactory,
            ITavernProvider tavernProvider,
            CollectionRepository collectionRepository
        )
        {
            if (tavernProvider == null) 
                throw new ArgumentNullException(nameof(tavernProvider));
            //TODO Нет проверок на нулл
            _visitorCounter = new VisitorCounter();
            
            _prefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
            _objectPool = objectPool ?? throw new ArgumentNullException(nameof(objectPool));
            _visitorViewFactory = visitorViewFactory ?? throw new ArgumentNullException(nameof(visitorViewFactory));
            _tavernProvider = tavernProvider;
            _collectionRepository = collectionRepository ?? 
                                    throw new ArgumentNullException(nameof(collectionRepository));
        }

        private GamePlay GamePlay => _gamePlay ??= _tavernProvider.GamePlay;
        private TavernMood TavernMood => _tavernMood ??= _tavernProvider.TavernMood;
        
        private bool CanSpawn()
        {
            int freeSeatPoints = _collectionRepository
                .Get<SeatPointView>()
                .Count(seatPoint => seatPoint.IsOccupied == false);
            
            return _visitorCounter.ActiveVisitorsCount < GamePlay.MaximumVisitorsCapacity &&
                   _visitorCounter.ActiveVisitorsCount < freeSeatPoints;
        }
        
        public async void SpawnVisitorAsync()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            //TODO сделать условие
            while (true)
            {
                if (CanSpawn())
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(Constant.Visitors.SpawnDelay),
                        cancellationToken: _cancellationTokenSource.Token);

                    Spawn();
                }

                await UniTask.Yield(_cancellationTokenSource.Token);
            }
        }

        public void Cancel() =>
            _cancellationTokenSource.Cancel();

        private void Spawn()
        {
            Create();

            _visitorCounter.AddActiveVisitorsCount();
        }

        private IVisitorView Create()
        {
            Visitor visitor = new Visitor();

            return CreateFromPool(visitor, TavernMood, _visitorCounter) ??
                   _visitorViewFactory.Create(visitor, TavernMood, _visitorCounter);
        }

        private IVisitorView CreateFromPool(Visitor visitor, TavernMood tavernMood, VisitorCounter visitorCounter)
        {
            VisitorView visitorView = _objectPool.Get<VisitorView>();

            if (visitorView == null)
                return null;

            return _visitorViewFactory.Create(visitor, tavernMood, visitorCounter, visitorView);
        }
    }
}