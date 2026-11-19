using System;
using Controllers.Visitors;
using Domain.Visitors;
using Infrastructure.Factories.Controllers.Visitors;
using Presentation.Views.Visitors.Inventorys;
using PresentationInterfaces.Views.Visitors;

namespace Infrastructure.Factories.Views.Visitors
{
    public class VisitorInventoryViewFactory
    {
        private readonly VisitorInventoryPresenterFactory _visitorInventoryPresenterFactory;

        public VisitorInventoryViewFactory(VisitorInventoryPresenterFactory visitorInventoryPresenterFactory)
        {
            _visitorInventoryPresenterFactory =
                visitorInventoryPresenterFactory ??
                throw new ArgumentNullException(nameof(visitorInventoryPresenterFactory));
        }

        public IVisitorInventoryView Create(
            VisitorInventoryView visitorInventoryView, VisitorInventory visitorInventory)
        {
            if (visitorInventoryView == null)
                throw new ArgumentNullException(nameof(visitorInventoryView));
            if (visitorInventory == null)
                throw new ArgumentNullException(nameof(visitorInventory));

            VisitorInventoryPresenter visitorInventoryPresenter =
                _visitorInventoryPresenterFactory.Create(visitorInventory);

            visitorInventoryView.Construct(visitorInventoryPresenter);

            return visitorInventoryView;
        }
    }
}