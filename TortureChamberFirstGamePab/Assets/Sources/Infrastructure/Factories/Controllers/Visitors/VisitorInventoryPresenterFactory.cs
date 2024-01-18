using System;
using Sources.Controllers.Visitors;
using Sources.Domain.Visitors;
using Sources.PresentationInterfaces.Views;

namespace Sources.Infrastructure.Factories.Controllers.Visitors
{
    public class VisitorInventoryPresenterFactory
    {
        public VisitorInventoryPresenter Create(IVisitorInventoryView visitorInventoryView,
            VisitorInventory visitorInventory)
        {
            if (visitorInventoryView == null) 
                throw new ArgumentNullException(nameof(visitorInventoryView));
            if (visitorInventory == null) 
                throw new ArgumentNullException(nameof(visitorInventory));
            
            return new VisitorInventoryPresenter
            (
                visitorInventoryView,
                visitorInventory
            );
        }
    }
}