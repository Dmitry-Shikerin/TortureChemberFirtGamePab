using System;
using JetBrains.Annotations;
using MyProject.Sources.Controllers.Common;
using Sources.Domain.Visitors;
using Sources.DomainInterfaces.Items;
using Sources.PresentationInterfaces.Views;

namespace Sources.Controllers
{
    public class VisitorInventoryPresenter : PresenterBase
    {
        private readonly IVisitorInventoryView _visitorInventoryView;
        private readonly VisitorInventory _visitorInventory;

        public VisitorInventoryPresenter
        (
            IVisitorInventoryView visitorInventoryView,
            VisitorInventory visitorInventory
        )
        {
            _visitorInventoryView = visitorInventoryView ??
                                    throw new ArgumentNullException(nameof(visitorInventoryView));
            _visitorInventory = visitorInventory ??
                                throw new ArgumentNullException(nameof(visitorInventory));
        }

        public IItem GetTargetItem()
        {
            return _visitorInventory.TargetItem;
        }

        public void Add(IItem item)
        {
            _visitorInventory.SetItem(item);
        }

        public IItem GetItem()
        {
            return _visitorInventory.Item;
        }
    }
}