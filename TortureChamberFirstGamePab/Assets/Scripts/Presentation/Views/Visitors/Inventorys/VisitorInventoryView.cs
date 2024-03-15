using Scripts.Controllers.Visitors;
using Scripts.DomainInterfaces.Items;
using Scripts.PresentationInterfaces.Views.Interactions.Get;
using Scripts.PresentationInterfaces.Views.Visitors;

namespace Scripts.Presentation.Views.Visitors.Inventorys
{
    public class VisitorInventoryView : PresentableView<VisitorInventoryPresenter>, IVisitorInventoryView, ITakeble
    {
        public IItem TargetItem => Presenter.TargetItem;
        public IItem Item => Presenter.Item;

        public void TakeItem(IItem item) =>
            Presenter.TakeItem(item);
    }
}