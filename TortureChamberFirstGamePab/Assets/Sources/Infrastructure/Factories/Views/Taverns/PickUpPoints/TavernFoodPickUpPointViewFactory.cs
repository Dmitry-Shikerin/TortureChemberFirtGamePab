using System;
using Sources.Domain.Items.ItemConfigs;
using Sources.Domain.Taverns;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factories.Controllers.Taverns.TavernPickUpPoints;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Presentation.Views.Taverns.PickUpPoints;
using Sources.PresentationInterfaces.Views.Taverns.PickUpPoints;

namespace Sources.Infrastructure.Factories.Views.Taverns.PickUpPoints
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

            var pickUpPointUIImages = pickUpPointView.PickUpPointUIImages;

            _imageUIFactory.Create(pickUpPointUIImages.Image);
            _imageUIFactory.Create(pickUpPointUIImages.BackgroundImage);

            var tavernPickUpPointPresenterFactory =
                _tavernPickUpPointPresenterFactory.Create(foodPickUpPoint,
                    pickUpPointView,
                    pickUpPointUIImages,
                    itemConfig);

            pickUpPointView.Construct(tavernPickUpPointPresenterFactory);

            return pickUpPointView;
        }
    }
}