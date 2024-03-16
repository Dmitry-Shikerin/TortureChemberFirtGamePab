﻿using Scripts.DomainInterfaces.Items;

namespace Scripts.Domain.Visitors
{
    public class VisitorInventory
    {
        public IItem Item { get; private set; }
        public IItem TargetItem { get; private set; }

        public void TakeItem(IItem item)
        {
            Item = item;
        }

        public void SetTargetItem(IItem targetItem)
        {
            TargetItem = targetItem;
        }
    }
}