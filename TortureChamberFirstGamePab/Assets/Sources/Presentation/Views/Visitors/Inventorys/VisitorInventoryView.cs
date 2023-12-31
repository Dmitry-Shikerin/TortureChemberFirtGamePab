﻿using Sources.Controllers;
using Sources.DomainInterfaces.Items;
using Sources.PresentationInterfaces.Views;
using Sources.PresentationInterfaces.Views.Interactions.Get;
using UnityEngine;

namespace Sources.Presentation.Views.Visitors.Inventorys
{
    public class VisitorInventoryView : PresentableView<VisitorInventoryPresenter>,IVisitorInventoryView, ITakeble
    {
        public IItem TargetItem => Presenter.TargetItem;
        public IItem Item => Presenter.Item;

        public void TakeItem(IItem item) => 
            Presenter.TakeItem(item);
    }
}