using Controllers.Visitors;
using DomainInterfaces.Items;
using PresentationInterfaces.Views.Interactions.Get;
using PresentationInterfaces.Views.Visitors;

namespace Presentation.Views.Visitors.Inventorys
{
    public class VisitorInventoryView : PresentableView<VisitorInventoryPresenter>, IVisitorInventoryView, ITakeble
    {
        public IItem TargetItem => Presenter.TargetItem;
        public IItem Item => Presenter.Item;

        public void TakeItem(IItem item) =>
            Presenter.TakeItem(item);
    }
}