using System;
using JetBrains.Annotations;
using Sources.Domain.Taverns;
using Sources.Domain.Visitors;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factories.Controllers.Visitors;
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
        
        
        private readonly ItemRepository<IItem> _itemRepository;
        private readonly VisitorPresenterFactory _visitorPresenterFactory;
        private readonly ItemViewFactory _itemViewFactory;
        private readonly ImageUIFactory _imageUIFactory;
        private readonly TavernMood _tavernMood;
        private readonly GarbageBuilder _garbageBuilder;
        private readonly CoinBuilder _coinBuilder;
        private readonly PrefabFactory _prefabFactory;
        private readonly VisitorCounter _visitorCounter;

        public VisitorBuilder(ItemRepository<IItem> itemRepository,
            VisitorPresenterFactory visitorPresenterFactory, ItemViewFactory itemViewFactory,
            ImageUIFactory imageUIFactory, TavernMood tavernMood, GarbageBuilder garbageBuilder,
            CoinBuilder coinBuilder, PrefabFactory prefabFactory, VisitorCounter visitorCounter)
        {
            _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
            _visitorPresenterFactory = visitorPresenterFactory ?? throw new ArgumentNullException(nameof(visitorPresenterFactory));
            _itemViewFactory = itemViewFactory ?? throw new ArgumentNullException(nameof(itemViewFactory));
            _imageUIFactory = imageUIFactory ?? throw new ArgumentNullException(nameof(imageUIFactory));
            _tavernMood = tavernMood ?? throw new ArgumentNullException(nameof(tavernMood));
            _garbageBuilder = garbageBuilder ?? throw new ArgumentNullException(nameof(garbageBuilder));
            _coinBuilder = coinBuilder ?? throw new ArgumentNullException(nameof(coinBuilder));
            _prefabFactory = prefabFactory ?? throw new ArgumentNullException(nameof(prefabFactory));
            _visitorCounter = visitorCounter ?? throw new ArgumentNullException(nameof(visitorCounter));
        }
        
        public IVisitorView Create(VisitorView visitorView)
        {
            VisitorViewFactory visitorViewFactory = new VisitorViewFactory(
                _visitorPresenterFactory);
            visitorViewFactory.Create(visitorView,
                _itemRepository, _imageUIFactory,
                _itemViewFactory, _tavernMood, _garbageBuilder, _coinBuilder, _visitorCounter);

            return visitorView;
        }
    }
}