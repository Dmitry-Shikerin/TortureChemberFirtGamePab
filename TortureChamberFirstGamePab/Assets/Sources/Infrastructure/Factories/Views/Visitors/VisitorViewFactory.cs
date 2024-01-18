using System;
using Sources.Controllers.Visitors;
using Sources.Domain.Taverns;
using Sources.Domain.Visitors;
using Sources.Infrastructure.Factories.Controllers.Visitors;
using Sources.Presentation.Views.Visitors;
using Sources.PresentationInterfaces.Views;

namespace Sources.Infrastructure.Factories.Views.Visitors
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
            TavernMood tavernMood, VisitorCounter visitorCounter)
        {
            if (visitorView == null) 
                throw new ArgumentNullException(nameof(visitorView));
            
            if (tavernMood == null)
                throw new ArgumentNullException(nameof(tavernMood));
            
            if (visitorCounter == null) 
                throw new ArgumentNullException(nameof(visitorCounter));
            
            VisitorInventory visitorInventory = CreateInventory(visitorView);
            
            Visitor visitor = new Visitor();

            VisitorPresenter visitorPresenter = _visitorPresenterFactory.Create(
                visitorView, visitorView.Animation, visitor, 
                visitorInventory,  tavernMood, visitorCounter);
            
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
