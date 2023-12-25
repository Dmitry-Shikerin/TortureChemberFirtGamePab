using System;
using JetBrains.Annotations;
using Sources.Controllers;
using Sources.Domain.Visitors;
using Sources.PresentationInterfaces.Views;

namespace Sources.Infrastructure.Factorys.Controllers
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