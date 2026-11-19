using System;
using Controllers.Taverns;
using Domain.Items.ItemConfigs;
using Domain.Taverns;
using DomainInterfaces.Items;
using Infrastructure.Factories.Controllers.Taverns.TavernPickUpPoints;
using Infrastructure.Factories.Views.UI;
using Presentation.Containers.UI.Images;
using Presentation.Views.Taverns.PickUpPoints;
using PresentationInterfaces.Views.Taverns.PickUpPoints;

namespace Infrastructure.Factories.Views.Taverns.PickUpPoints
{
    public class TavernFoodPickUpPointViewFactory
    {
        private readonly ImageUIFactory _imageUIFactory;
        private readonly TavernPickUpPointPresenterFactory _tavernPickUpPointPresenterFactory;

        public TavernFoodPickUpPointViewFactory(
            TavernPickUpPointPresenterFactory tavernPickUpPointPresenterFactory,
            ImageUIFactory imageUIFactory)
        {
            _tavernPickUpPointPresenterFactory =
                tavernPickUpPointPresenterFactory ??
                throw new ArgumentNullException(nameof(tavernPickUpPointPresenterFactory));
            _imageUIFactory = imageUIFactory ?? throw new ArgumentNullException(nameof(imageUIFactory));
        }

        public ITavernFudPickUpPointView Create<T>(
            FoodPickUpPoint foodPickUpPoint,
            TavernFudPickUpPointView<T> pickUpPointView,
            ItemConfig itemConfig)
            where T : IItem
        {
            if (foodPickUpPoint == null)
                throw new ArgumentNullException(nameof(foodPickUpPoint));

            if (pickUpPointView == null)
                throw new ArgumentNullException(nameof(pickUpPointView));

            if (itemConfig == null)
                throw new ArgumentNullException(nameof(itemConfig));

            PickUpPointUIImages pickUpPointUIImages = pickUpPointView.PickUpPointUIImages;

            _imageUIFactory.Create(pickUpPointUIImages.Image);
            _imageUIFactory.Create(pickUpPointUIImages.BackgroundImage);

            TavernFudPickUpPointPresenter tavernPickUpPointPresenterFactory =
                _tavernPickUpPointPresenterFactory.Create(
                    foodPickUpPoint, pickUpPointView, pickUpPointUIImages, itemConfig);

            pickUpPointView.Construct(tavernPickUpPointPresenterFactory);

            return pickUpPointView;
        }
    }
}