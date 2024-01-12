using System;
using Sources.Controllers;
using Sources.Domain.Taverns;
using Sources.Domain.Visitors;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.BuilderFactories;
using Sources.Infrastructure.Factories.Controllers.Visitors;
using Sources.Infrastructure.Factories.Views.Items.Common;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Factories.Views.Visitors;
using Sources.Presentation.Animations;
using Sources.Presentation.UI;
using Sources.Presentation.Views.Visitors;
using Sources.Presentation.Views.Visitors.Inventorys;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Repositoryes;

namespace Sources.Infrastructure.Factorys.Views
{
    public class VisitorViewFactory
    {
        private readonly VisitorPresenterFactory _visitorPresenterFactory;

        public VisitorViewFactory(VisitorPresenterFactory visitorPresenterFactory)
        {
            _visitorPresenterFactory = visitorPresenterFactory ?? 
                                       throw new ArgumentNullException(nameof(visitorPresenterFactory));
        }

        public IVisitorView Create(VisitorView visitorView,
            ItemRepository<IItem> itemRepository,
             ImageUIFactory imageUIFactory,
            ItemViewFactory itemViewFactory, TavernMood tavernMood, GarbageBuilder garbageBuilder,
            CoinBuilder coinBuilder, VisitorCounter visitorCounter)
        {
            if (visitorView == null) 
                throw new ArgumentNullException(nameof(visitorView));
            
            if (itemRepository == null) 
                throw new ArgumentNullException(nameof(itemRepository));
            
            if (imageUIFactory == null) 
                throw new ArgumentNullException(nameof(imageUIFactory));
            
            if (itemViewFactory == null) 
                throw new ArgumentNullException(nameof(itemViewFactory));
            
            if (tavernMood == null)
                throw new ArgumentNullException(nameof(tavernMood));
            
            if (visitorCounter == null) 
                throw new ArgumentNullException(nameof(visitorCounter));
            
            VisitorInventory visitorInventory = CreateInventory(visitorView);
            
            Visitor visitor = new Visitor();

            VisitorPresenter visitorPresenter = _visitorPresenterFactory.Create(
                visitorView, visitorView.Animation, visitor, itemRepository, visitorView.ImageUIContainer,
                visitorInventory, imageUIFactory, itemViewFactory, tavernMood, garbageBuilder,
                coinBuilder, visitorCounter);
            
            visitorView.Construct(visitorPresenter);

            return visitorView;
        }

        private VisitorInventory CreateInventory(VisitorView visitorView)
        {
            VisitorInventory visitorInventory = new VisitorInventory();
            VisitorInventoryViewFactory visitorInventoryViewFactory = new VisitorInventoryViewFactory();
            visitorInventoryViewFactory.Create(visitorView.Inventory, visitorInventory);

            return visitorInventory;
        }
    }
}
