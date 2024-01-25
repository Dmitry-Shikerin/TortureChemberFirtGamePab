using System;
using Sources.Controllers.Taverns;
using Sources.Domain.Items.ItemConfigs;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factories.Controllers.Taverns.TavernPickUpPoints;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Presentation.Views.Taverns.PickUpPoints;
using Sources.Presentation.Views.Taverns.PickUpPoints.Foods;
using Sources.PresentationInterfaces.Views.Taverns.PickUpPoints;

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

        public ITavernFudPickUpPointView Create<T>
        (
            TavernFudPickUpPointView<T> pickUpPointView,
            PickUpPointUIImages pickUpPointUIImages, 
            ImageUIFactory imageUIFactory, 
            ItemConfig itemConfig
            ) where T : IItem
        {
            if (pickUpPointView == null) 
                throw new ArgumentNullException(nameof(pickUpPointView));
            if (pickUpPointUIImages == null) 
                throw new ArgumentNullException(nameof(pickUpPointUIImages));
            if (imageUIFactory == null) 
                throw new ArgumentNullException(nameof(imageUIFactory));

            imageUIFactory.Create(pickUpPointUIImages.Image);
            imageUIFactory.Create(pickUpPointUIImages.BackgroundImage);
            
            TavernFudPickUpPointPresenter tavernPickUpPointPresenterFactory =
                _tavernPickUpPointPresenterFactory.Create(pickUpPointView,
                    pickUpPointUIImages, itemConfig);
            
            pickUpPointView.Construct(tavernPickUpPointPresenterFactory);

            return pickUpPointView;
        }
    }
}