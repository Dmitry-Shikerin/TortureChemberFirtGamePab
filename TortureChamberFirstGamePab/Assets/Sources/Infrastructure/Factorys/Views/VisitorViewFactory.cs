using System;
using JetBrains.Annotations;
using Sources.Controllers;
using Sources.Domain.Visitors;
using Sources.Infrastructure.Factorys.Controllers;
using Sources.Presentation.Animations;
using Sources.Presentation.Views;
using Sources.PresentationInterfaces.Views;

namespace Sources.Infrastructure.Factorys.Views
{
    public class VisitorViewFactory
    {
        private readonly VisitorPresenterFactory _visitorPresenterFactory;

        public VisitorViewFactory(VisitorPresenterFactory visitorPresenterFactory)
        {
            _visitorPresenterFactory = visitorPresenterFactory ?? 
                                       throw new ArgumentNullException(nameof(visitorPresenterFactory));
        }

        public IVisitorView Create(VisitorView visitorView, 
            VisitorAnimation visitorAnimation, Visitor visitor)
        {
            VisitorPresenter visitorPresenter = _visitorPresenterFactory.Create(
                visitorView, visitorAnimation,visitor);
            
            visitorView.Construct(visitorPresenter);

            return visitorView;
        }
    }
}
