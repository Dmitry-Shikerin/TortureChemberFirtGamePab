using System;
using JetBrains.Annotations;
using Sources.Domain.Taverns;
using Sources.Domain.Visitors;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factories.Prefabs;
using Sources.Infrastructure.Factories.Views.Items.Common;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Factories.Views.Visitors;
using Sources.Infrastructure.Factorys.Controllers;
using Sources.Infrastructure.Factorys.Views;
using Sources.Infrastructure.Services;
using Sources.Presentation.Animations;
using Sources.Presentation.UI;
using Sources.Presentation.Views.ObjectPolls;
using Sources.Presentation.Views.Visitors;
using Sources.Presentation.Views.Visitors.Inventorys;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Repositoryes;
using Unity.VisualScripting;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.BuilderFactories
{
    public class VisitorBuilder
    {
        private const string VisitorPrefabPath = "Prefabs/Visitor";
        
        private readonly CollectionRepository _collectionRepository;
        private readonly ItemRepository<IItem> _itemRepository;
        private readonly ProductShuffleService _productShuffleService;
        private readonly ItemViewFactory _itemViewFactory;
        private readonly ImageUIFactory _imageUIFactory;
        private readonly TavernMood _tavernMood;
        private readonly GarbageBuilder _garbageBuilder;
        private readonly CoinBuilder _coinBuilder;
        private readonly PrefabFactory _prefabFactory;
        private readonly VisitorCounter _visitorCounter;

        public VisitorBuilder(CollectionRepository collectionRepository, ItemRepository<IItem> itemRepository,
            ProductShuffleService productShuffleService, ItemViewFactory itemViewFactory,
            ImageUIFactory imageUIFactory, TavernMood tavernMood, GarbageBuilder garbageBuilder,
            CoinBuilder coinBuilder, PrefabFactory prefabFactory, VisitorCounter visitorCounter)
        {
            _collectionRepository = collectionRepository ?? 
                                    throw new ArgumentNullException(nameof(collectionRepository));
            _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
            _productShuffleService = productShuffleService ??
                                     throw new ArgumentNullException(nameof(productShuffleService));
            _itemViewFactory = itemViewFactory ?? throw new ArgumentNullException(nameof(itemViewFactory));
            _imageUIFactory = imageUIFactory ?? throw new ArgumentNullException(nameof(imageUIFactory));
            _tavernMood = tavernMood ?? throw new ArgumentNullException(nameof(tavernMood));
            _garbageBuilder = garbageBuilder ?? throw new ArgumentNullException(nameof(garbageBuilder));
            _coinBuilder = coinBuilder ?? throw new ArgumentNullException(nameof(coinBuilder));
            _prefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
            _visitorCounter = visitorCounter ?? throw new ArgumentNullException(nameof(visitorCounter));
        }
        
        public IVisitorView Create(IObjectPool objectPool)
        {
            VisitorView visitorView = _prefabFactory.Create<VisitorView>(VisitorPrefabPath);
            visitorView.AddComponent<PoolableObject>().SetPool(objectPool);
            
            VisitorInventoryView visitorInventoryView = visitorView.GetComponentInChildren<VisitorInventoryView>();
            VisitorInventory visitorInventory = new VisitorInventory();
            VisitorInventoryPresenterFactory visitorInventoryPresenterFactory = 
                new VisitorInventoryPresenterFactory();
            VisitorInventoryViewFactory visitorInventoryViewFactory = 
                new VisitorInventoryViewFactory(visitorInventoryPresenterFactory);
            visitorInventoryViewFactory.Create(visitorInventoryView, visitorInventory);
            
            Visitor visitor = new Visitor();
            VisitorAnimation visitorAnimation = visitorView.gameObject.GetComponent<VisitorAnimation>();
            VisitorImageUI visitorImageUI =
                visitorView.gameObject.GetComponentInChildren<VisitorImageUI>();
            VisitorPresenterFactory visitorPresenterFactory = new VisitorPresenterFactory(
                _collectionRepository, _productShuffleService);
            VisitorViewFactory visitorViewFactory = new VisitorViewFactory(
                visitorPresenterFactory);
            visitorViewFactory.Create(visitorView, visitorAnimation, visitor,
                _itemRepository, visitorImageUI, visitorInventory, _imageUIFactory,
                _itemViewFactory, _tavernMood, _garbageBuilder, _coinBuilder, _visitorCounter);

            return visitorView;
        }
    }
}