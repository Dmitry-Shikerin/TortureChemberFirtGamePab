using System;
using System.Linq;
using Controllers.Visitors;
using Domain.Constants;
using Domain.Taverns;
using Domain.Visitors;
using Infrastructure.Factories.Controllers.Visitors;
using Infrastructure.Factories.Views.UI;
using Infrastructure.Services.ObjectPools;
using InfrastructureInterfaces.Factories.Prefabs;
using Presentation.Views.GamePoints.VisitorsPoints;
using Presentation.Views.ObjectPolls;
using Presentation.Views.Visitors;
using PresentationInterfaces.Views.Visitors;
using Unity.VisualScripting;
using Utils.Repositories.CollectionRepository;

namespace Infrastructure.Factories.Views.Visitors
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
            VisitorInventory visitorInventory = CreateInventory(visitorView);

            _imageUIFactory.Create(visitorView.VisitorImageUIContainer.OrderImage);
            _imageUIFactory.Create(visitorView.VisitorImageUIContainer.BackGroundImage);

            VisitorPresenter visitorPresenter = _visitorPresenterFactory.Create(
                visitorView,
                visitorView.Animation,
                visitor,
                visitorInventory,
                tavernMood,
                visitorCounter);

            visitorView.Construct(visitorPresenter);

            return visitorView;
        }

        public IVisitorView Create(Visitor visitor, TavernMood tavernMood, VisitorCounter visitorCounter)
        {
            VisitorView visitorView = CreateView();

            OutDoorPoint spawnPoint = _collectionRepository.Get<OutDoorPoint>().FirstOrDefault() ??
                                      throw new NullReferenceException(nameof(spawnPoint));
            visitorView.SetPosition(spawnPoint.Position);

            Create(visitor, tavernMood, visitorCounter, visitorView);

            return visitorView;
        }

        private VisitorView CreateView()
        {
            return _prefabFactory.Create<VisitorView>(PrefabPath.VisitorView)
                .AddComponent<PoolableObject>()
                .SetPool(_objectPool)
                .GetComponent<VisitorView>();
        }

        private VisitorInventory CreateInventory(VisitorView visitorView)
        {
            VisitorInventory visitorInventory = new VisitorInventory();
            _visitorInventoryViewFactory.Create(visitorView.Inventory, visitorInventory);

            return visitorInventory;
        }
    }
}