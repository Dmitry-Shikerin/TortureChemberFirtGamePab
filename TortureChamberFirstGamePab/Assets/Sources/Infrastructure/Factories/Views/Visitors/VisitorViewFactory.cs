using System;
using Sources.Controllers.Visitors;
using Sources.Domain.Taverns;
using Sources.Domain.Visitors;
using Sources.Infrastructure.Factories.Controllers.Visitors;
using Sources.Infrastructure.Factories.Prefabs;
using Sources.Infrastructure.Services.ObjectPools;
using Sources.Presentation.Views.ObjectPolls;
using Sources.Presentation.Views.Visitors;
using Sources.PresentationInterfaces.Views;
using Unity.VisualScripting;

namespace Sources.Infrastructure.Factories.Views.Visitors
{
    public class VisitorViewFactory
    {
        private const string VisitorViewPrefabPath = "Prefabs/Visitor";
        
        private readonly VisitorPresenterFactory _visitorPresenterFactory;
        private readonly PrefabFactory _prefabFactory;
        private readonly ObjectPool<VisitorView> _objectPool;

        public VisitorViewFactory(VisitorPresenterFactory visitorPresenterFactory, 
            PrefabFactory prefabFactory, ObjectPool<VisitorView> objectPool)
        {
            _visitorPresenterFactory = visitorPresenterFactory ?? 
                                       throw new ArgumentNullException(nameof(visitorPresenterFactory));
            _prefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
            _objectPool = objectPool ?? throw new ArgumentNullException(nameof(objectPool));
        }

        public IVisitorView Create(Visitor visitor,
            TavernMood tavernMood, VisitorCounter visitorCounter, VisitorView visitorView)
        {
            VisitorInventory visitorInventory = CreateInventory(visitorView);
            
            VisitorPresenter visitorPresenter = _visitorPresenterFactory.Create(
                visitorView, visitorView.Animation, visitor, 
                visitorInventory,  tavernMood, visitorCounter);
            
            visitorView.Construct(visitorPresenter);
            
            return visitorView;
        }

        public IVisitorView Create(Visitor visitor,
            TavernMood tavernMood, VisitorCounter visitorCounter)
        {
            VisitorView visitorView = CreateView();
            
            Create(visitor, tavernMood, visitorCounter, visitorView);

            return visitorView;
        }
        
        private VisitorView CreateView() =>
            _prefabFactory.Create<VisitorView>(VisitorViewPrefabPath)
                .AddComponent<PoolableObject>()
                .SetPool(_objectPool)
                .GetComponent<VisitorView>();


        private VisitorInventory CreateInventory(VisitorView visitorView)
        {
            VisitorInventory visitorInventory = new VisitorInventory();
            VisitorInventoryViewFactory visitorInventoryViewFactory = new VisitorInventoryViewFactory();
            visitorInventoryViewFactory.Create(visitorView.Inventory, visitorInventory);

            return visitorInventory;
        }
    }
}
