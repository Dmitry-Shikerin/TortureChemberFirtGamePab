using System;
using Scripts.Controllers.Visitors;
using Scripts.Domain.Visitors;

namespace Scripts.Infrastructure.Factories.Controllers.Visitors
{
    public class VisitorInventoryPresenterFactory
    {
        public VisitorInventoryPresenter Create(VisitorInventory visitorInventory)
        {
            if (visitorInventory == null)
                throw new ArgumentNullException(nameof(visitorInventory));

            return new VisitorInventoryPresenter(visitorInventory);
        }
    }
}