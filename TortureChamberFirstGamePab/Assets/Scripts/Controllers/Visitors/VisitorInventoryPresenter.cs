using System;
using Scripts.Domain.Visitors;
using Scripts.DomainInterfaces.Items;

namespace Scripts.Controllers.Visitors
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