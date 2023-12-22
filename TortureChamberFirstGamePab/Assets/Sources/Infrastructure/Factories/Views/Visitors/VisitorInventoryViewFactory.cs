using System;
using Sources.Controllers;
using Sources.Domain.Visitors;
using Sources.Infrastructure.Factorys.Controllers;
using Sources.Presentation.Views.Visitors.Inventorys;
using Sources.PresentationInterfaces.Views;

namespace Sources.Infrastructure.Factories.Views.Visitors
{
    public class VisitorInventoryViewFactory
    {
        private readonly VisitorInventoryPresenterFactory _visitorInventoryPresenterFactory;

        public VisitorInventoryViewFactory(VisitorInventoryPresenterFactory visitorInventoryPresenterFactory)
        {
            _visitorInventoryPresenterFactory = 
                visitorInventoryPresenterFactory ??
                throw new ArgumentNullException(nameof(visitorInventoryPresenterFactory));
        }

        public IVisitorInventoryView Create(VisitorInventoryView visitorInventoryView, 
            VisitorInventory visitorInventory)
        {
            VisitorInventoryPresenter visitorInventoryPresenter = 
                _visitorInventoryPresenterFactory.Create(visitorInventoryView, visitorInventory);
            
            visitorInventoryView.Construct(visitorInventoryPresenter);

            return visitorInventoryView;
        }
    }
}