using System;
using JetBrains.Annotations;
using Sources.Controllers.Taverns;
using Sources.Domain.Items;
using Sources.Domain.Items.ItemConfigs;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factories.Controllers.Taverns.TavernPickUpPoints;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Presentation.UI;
using Sources.Presentation.UI.PickUpPointUIs;
using Sources.Presentation.Views.Taverns;
using Sources.Presentation.Views.Taverns.Foods;
using Sources.PresentationInterfaces.Views.Taverns.PickUpPoints;
using UnityEngine.UIElements;

namespace Sources.Infrastructure.Factories.Views.Taverns.PickUpPoints
{
    public class TavernFoodPickUpPointViewFactory
    {
        private readonly TavernPickUpPointPresenterFactory _tavernPickUpPointPresenterFactory;

        public TavernFoodPickUpPointViewFactory(
            TavernPickUpPointPresenterFactory tavernPickUpPointPresenterFactory)
        {
            _tavernPickUpPointPresenterFactory = 
                tavernPickUpPointPresenterFactory ?? 
                throw new ArgumentNullException(nameof(tavernPickUpPointPresenterFactory));
        }

        public ITavernFudPickUpPointView Create<T>(TavernFudPickUpPointView<T> beerPickUpPointView,
            PickUpPointUIImages pickUpPointUIImages, ImageUIFactory imageUIFactory, ItemConfig itemConfig) where T : IItem
        {
            if (beerPickUpPointView == null) 
                throw new ArgumentNullException(nameof(beerPickUpPointView));
            if (pickUpPointUIImages == null) 
                throw new ArgumentNullException(nameof(pickUpPointUIImages));
            if (imageUIFactory == null) 
                throw new ArgumentNullException(nameof(imageUIFactory));

            imageUIFactory.Create(pickUpPointUIImages.Image);
            imageUIFactory.Create(pickUpPointUIImages.BackgroundImage);
            
            TavernFudPickUpPointPresenter tavernPickUpPointPresenterFactory =
                _tavernPickUpPointPresenterFactory.Create(beerPickUpPointView,
                    pickUpPointUIImages, itemConfig);
            
            beerPickUpPointView.Construct(tavernPickUpPointPresenterFactory);

            return beerPickUpPointView;
        }
    }
}