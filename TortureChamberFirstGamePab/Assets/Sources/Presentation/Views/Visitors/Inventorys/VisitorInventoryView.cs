using Sources.Controllers;
using Sources.DomainInterfaces.Items;
using Sources.PresentationInterfaces.Views;
using Sources.PresentationInterfaces.Views.Interactions.Get;
using UnityEngine;

namespace Sources.Presentation.Views.Visitors.Inventorys
{
    public class VisitorInventoryView : PresentableView<VisitorInventoryPresenter>,IVisitorInventoryView, IGettable
    {
        public IItem GetTargetItem()
        {
            return Presenter.GetTargetItem();
        }

        public void Add(IItem item)
        {
            Presenter.Add(item);
        }
    }
}