using System;
using Sources.Domain.Taverns;
using Sources.Domain.Visitors;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factories.Controllers.Visitors;
using Sources.Infrastructure.Factories.Prefabs;
using Sources.Infrastructure.Factories.Views.Items.Common;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Factories.Views.Visitors;
using Sources.Presentation.Views.Visitors;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Repositoryes;

namespace Sources.Infrastructure.Builders
{
    public class VisitorBuilder
    {
        private readonly VisitorPresenterFactory _visitorPresenterFactory;
        private readonly TavernMood _tavernMood;
        private readonly VisitorCounter _visitorCounter;

        public VisitorBuilder(
            VisitorPresenterFactory visitorPresenterFactory,
            TavernMood tavernMood, VisitorCounter visitorCounter)
        {
            _visitorPresenterFactory = visitorPresenterFactory ?? throw new ArgumentNullException(nameof(visitorPresenterFactory));
            _tavernMood = tavernMood ?? throw new ArgumentNullException(nameof(tavernMood));
            _visitorCounter = visitorCounter ?? throw new ArgumentNullException(nameof(visitorCounter));
        }
        
        public IVisitorView Create(VisitorView visitorView)
        {
            //TODO возможно переместить это во вью фектори

            VisitorViewFactory visitorViewFactory = new VisitorViewFactory(
                _visitorPresenterFactory);
            visitorViewFactory.Create(visitorView, _tavernMood, _visitorCounter);

            return visitorView;
        }
    }
}