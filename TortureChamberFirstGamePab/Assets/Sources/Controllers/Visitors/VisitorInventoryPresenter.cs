﻿using System;
using Sources.Domain.Visitors;
using Sources.DomainInterfaces.Items;
using Sources.PresentationInterfaces.Views.Visitors;

namespace Sources.Controllers.Visitors
{
    public class VisitorInventoryPresenter : PresenterBase
    {
        private readonly VisitorInventory _visitorInventory;
        private readonly IVisitorInventoryView _visitorInventoryView;

        public VisitorInventoryPresenter(
            IVisitorInventoryView visitorInventoryView,
            VisitorInventory visitorInventory)
        {
            _visitorInventoryView = visitorInventoryView ??
                                    throw new ArgumentNullException(nameof(visitorInventoryView));
            _visitorInventory = visitorInventory ??
                                throw new ArgumentNullException(nameof(visitorInventory));
        }

        public IItem TargetItem => _visitorInventory.TargetItem;
        public IItem Item => _visitorInventory.Item;

        public void TakeItem(IItem item)
        {
            _visitorInventory.TakeItem(item);
        }
    }
}