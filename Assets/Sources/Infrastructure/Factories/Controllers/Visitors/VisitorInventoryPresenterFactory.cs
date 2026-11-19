using System;
using Controllers.Visitors;
using Domain.Visitors;

namespace Infrastructure.Factories.Controllers.Visitors
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