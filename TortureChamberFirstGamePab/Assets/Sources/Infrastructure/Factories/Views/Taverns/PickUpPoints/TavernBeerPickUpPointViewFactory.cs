using System;
using JetBrains.Annotations;
using Sources.Controllers.Taverns;
using Sources.Domain.Items;
using Sources.Infrastructure.Factories.Controllers.Taverns.TavernPickUpPoints;
using Sources.Presentation.UI;
using Sources.Presentation.Views.Taverns.Foods;
using Sources.PresentationInterfaces.Views.Taverns.PickUpPoints;
using UnityEngine.UIElements;

namespace Sources.Infrastructure.Factories.Views.Taverns.PickUpPoints
{
    public class TavernBeerPickUpPointViewFactory
    {
        private readonly TavernPickUpPointPresenterFactory _tavernPickUpPointPresenterFactory;

        public TavernBeerPickUpPointViewFactory(
            TavernPickUpPointPresenterFactory tavernPickUpPointPresenterFactory)
        {
            _tavernPickUpPointPresenterFactory = 
                tavernPickUpPointPresenterFactory ?? 
                throw new ArgumentNullException(nameof(tavernPickUpPointPresenterFactory));
        }

        public ITavernFudPickUpPointView Create(BeerPickUpPointView beerPickUpPointView,
            ImageUI imageUI)
        {
            TavernFudPickUpPointPresenter tavernPickUpPointPresenterFactory =
                _tavernPickUpPointPresenterFactory.Create(beerPickUpPointView,
                    imageUI);
            
            beerPickUpPointView.Construct(tavernPickUpPointPresenterFactory);

            return beerPickUpPointView;
        }
    }
}