using System;
using Domain.Visitors;
using DomainInterfaces.Items;

namespace Controllers.Visitors
{
    public class VisitorInventoryPresenter : PresenterBase
    {
        private readonly VisitorInventory _visitorInventory;

        public VisitorInventoryPresenter(VisitorInventory visitorInventory)
        {
            _visitorInventory = visitorInventory ??
                                throw new ArgumentNullException(nameof(visitorInventory));
        }

        public IItem TargetItem => _visitorInventory.TargetItem;
        public IItem Item => _visitorInventory.Item;

        public void TakeItem(IItem item) =>
            _visitorInventory.TakeItem(item);
    }
}