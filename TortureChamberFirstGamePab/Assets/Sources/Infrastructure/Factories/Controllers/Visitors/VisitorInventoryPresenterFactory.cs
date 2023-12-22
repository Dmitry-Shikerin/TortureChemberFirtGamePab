using Sources.Controllers;
using Sources.Domain.Visitors;
using Sources.PresentationInterfaces.Views;

namespace Sources.Infrastructure.Factorys.Controllers
{
    public class VisitorInventoryPresenterFactory
    {
        public VisitorInventoryPresenter Create(IVisitorInventoryView visitorInventoryView,
            VisitorInventory visitorInventory)
        {
            return new VisitorInventoryPresenter
            (
                visitorInventoryView,
                visitorInventory
            );
        }
    }
}