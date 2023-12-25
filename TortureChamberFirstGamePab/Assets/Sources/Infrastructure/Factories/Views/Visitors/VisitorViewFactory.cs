using System;
using JetBrains.Annotations;
using Sources.Controllers;
using Sources.Domain.Visitors;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Factorys.Controllers;
using Sources.Presentation.Animations;
using Sources.Presentation.UI;
using Sources.Presentation.Views;
using Sources.Presentation.Views.Visitors;
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
            VisitorAnimation visitorAnimation, Visitor visitor,
            ItemRepository<IItem> itemRepository, VisitorImageUI visitorImageUI,
            VisitorInventory visitorInventory, ImageUIFactory imageUIFactory)
        {
            if (visitorView == null) 
                throw new ArgumentNullException(nameof(visitorView));
            if (visitorAnimation == null) 
                throw new ArgumentNullException(nameof(visitorAnimation));
            if (visitor == null)
                throw new ArgumentNullException(nameof(visitor));
            if (itemRepository == null) 
                throw new ArgumentNullException(nameof(itemRepository));
            if (visitorImageUI == null) 
                throw new ArgumentNullException(nameof(visitorImageUI));
            if (visitorInventory == null) 
                throw new ArgumentNullException(nameof(visitorInventory));
            if (imageUIFactory == null) 
                throw new ArgumentNullException(nameof(imageUIFactory));

            VisitorPresenter visitorPresenter = _visitorPresenterFactory.Create(
                visitorView, visitorAnimation, visitor, itemRepository, visitorImageUI,
                visitorInventory, imageUIFactory);
            
            visitorView.Construct(visitorPresenter);

            return visitorView;
        }
    }
}
