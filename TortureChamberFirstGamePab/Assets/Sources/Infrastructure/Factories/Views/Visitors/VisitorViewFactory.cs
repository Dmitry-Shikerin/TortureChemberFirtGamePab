using System;
using System.Linq;
using Sources.Domain.Constants;
using Sources.Domain.Taverns;
using Sources.Domain.Visitors;
using Sources.Infrastructure.Factories.Controllers.Visitors;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
using Sources.Presentation.Views.ObjectPolls;
using Sources.Presentation.Views.Visitors;
using Sources.Presentation.Voids.GamePoints.VisitorsPoints;
using Sources.PresentationInterfaces.Views.Visitors;
using Sources.Utils.Repositoryes.CollectionRepository;
using Unity.VisualScripting;

namespace Sources.Infrastructure.Factories.Views.Visitors
{
    public class VisitorViewFactory
    {
        private readonly CollectionRepository _collectionRepository;
        private readonly ImageUIFactory _imageUIFactory;
        private readonly ObjectPool<VisitorView> _objectPool;
        private readonly IPrefabFactory _prefabFactory;
        private readonly VisitorInventoryViewFactory _visitorInventoryViewFactory;
        private readonly VisitorPresenterFactory _visitorPresenterFactory;

        public VisitorViewFactory(
            CollectionRepository collectionRepository,
            VisitorPresenterFactory visitorPresenterFactory,
            IPrefabFactory prefabFactory,
            ObjectPool<VisitorView> objectPool,
            VisitorInventoryViewFactory visitorInventoryViewFactory,
            ImageUIFactory imageUIFactory)
        {
            _collectionRepository = collectionRepository ??
                                    throw new ArgumentNullException(nameof(collectionRepository));
            _visitorPresenterFactory = visitorPresenterFactory ??
                                       throw new ArgumentNullException(nameof(visitorPresenterFactory));
            _prefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
            _objectPool = objectPool ?? throw new ArgumentNullException(nameof(objectPool));
            _visitorInventoryViewFactory = visitorInventoryViewFactory ??
                                           throw new ArgumentNullException(nameof(visitorInventoryViewFactory));
            _imageUIFactory = imageUIFactory ?? throw new ArgumentNullException(nameof(imageUIFactory));
        }

        public IVisitorView Create(
            Visitor visitor,
            TavernMood tavernMood,
            VisitorCounter visitorCounter,
            VisitorView visitorView)
        {
            var visitorInventory = CreateInventory(visitorView);

            _imageUIFactory.Create(visitorView.VisitorImageUIContainer.OrderImage);
            _imageUIFactory.Create(visitorView.VisitorImageUIContainer.BackGroundImage);

            var visitorPresenter = _visitorPresenterFactory.Create(
                visitorView,
                visitorView.Animation,
                visitor,
                visitorInventory,
                tavernMood,
                visitorCounter);

            visitorView.Construct(visitorPresenter);

            return visitorView;
        }

        public IVisitorView Create(
            Visitor visitor,
            TavernMood tavernMood,
            VisitorCounter visitorCounter)
        {
            var visitorView = CreateView();

            OutDoorPoint spawnPoint = _collectionRepository.Get<OutDoorPoint>().FirstOrDefault() ??
                                      throw new NullReferenceException(nameof(spawnPoint));
            visitorView.SetPosition(spawnPoint.Position);

            Create(visitor, tavernMood, visitorCounter, visitorView);

            return visitorView;
        }

        private VisitorView CreateView()
        {
            return _prefabFactory.Create<VisitorView>(Constant.PrefabPaths.VisitorView)
                .AddComponent<PoolableObject>()
                .SetPool(_objectPool)
                .GetComponent<VisitorView>();
        }

        private VisitorInventory CreateInventory(VisitorView visitorView)
        {
            var visitorInventory = new VisitorInventory();
            _visitorInventoryViewFactory.Create(visitorView.Inventory, visitorInventory);

            return visitorInventory;
        }
    }
}