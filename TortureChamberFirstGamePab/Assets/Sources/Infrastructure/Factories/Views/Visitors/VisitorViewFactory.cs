using System;
using Sources.Controllers.Visitors;
using Sources.Domain.Constants;
using Sources.Domain.Taverns;
using Sources.Domain.Visitors;
using Sources.Infrastructure.Factories.Controllers.Visitors;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
using Sources.Presentation.Views.ObjectPolls;
using Sources.Presentation.Views.Visitors;
using Sources.PresentationInterfaces.Views;
using Unity.VisualScripting;

namespace Sources.Infrastructure.Factories.Views.Visitors
{
    public class VisitorViewFactory
    {
        private readonly VisitorPresenterFactory _visitorPresenterFactory;
        private readonly IPrefabFactory _prefabFactory;
        private readonly ObjectPool<VisitorView> _objectPool;
        private readonly VisitorInventoryViewFactory _visitorInventoryViewFactory;

        public VisitorViewFactory
        (
            VisitorPresenterFactory visitorPresenterFactory,
            IPrefabFactory prefabFactory,
            ObjectPool<VisitorView> objectPool,
            VisitorInventoryViewFactory visitorInventoryViewFactory
        )
        {
            _visitorPresenterFactory = visitorPresenterFactory ??
                                       throw new ArgumentNullException(nameof(visitorPresenterFactory));
            _prefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
            _objectPool = objectPool ?? throw new ArgumentNullException(nameof(objectPool));
            _visitorInventoryViewFactory = visitorInventoryViewFactory ??
                                           throw new ArgumentNullException(nameof(visitorInventoryViewFactory));
        }

        public IVisitorView Create
        (
            Visitor visitor,
            TavernMood tavernMood,
            VisitorCounter visitorCounter,
            VisitorView visitorView
        )
        {
            VisitorInventory visitorInventory = CreateInventory(visitorView);

            VisitorPresenter visitorPresenter = _visitorPresenterFactory.Create(
                visitorView, visitorView.Animation, visitor,
                visitorInventory, tavernMood, visitorCounter);

            visitorView.Construct(visitorPresenter);

            return visitorView;
        }

        public IVisitorView Create
        (
            Visitor visitor,
            TavernMood tavernMood,
            VisitorCounter visitorCounter
        )
        {
            VisitorView visitorView = CreateView();

            Create(visitor, tavernMood, visitorCounter, visitorView);

            return visitorView;
        }

        private VisitorView CreateView() =>
            _prefabFactory.Create<VisitorView>(Constant.PrefabPaths.VisitorView)
                .AddComponent<PoolableObject>()
                .SetPool(_objectPool)
                .GetComponent<VisitorView>();


        private VisitorInventory CreateInventory(VisitorView visitorView)
        {
            VisitorInventory visitorInventory = new VisitorInventory();
            _visitorInventoryViewFactory.Create(visitorView.Inventory, visitorInventory);

            return visitorInventory;
        }
    }
}