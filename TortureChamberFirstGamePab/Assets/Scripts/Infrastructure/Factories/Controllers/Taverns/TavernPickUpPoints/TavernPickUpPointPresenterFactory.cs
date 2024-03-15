using System;
using Scripts.Controllers.Taverns;
using Scripts.Domain.Items.ItemConfigs;
using Scripts.Domain.Taverns;
using Scripts.Infrastructure.Factories.Domains.Items;
using Scripts.Presentation.Containers.UI.Images;
using Scripts.PresentationInterfaces.Views.Taverns.PickUpPoints;

namespace Scripts.Infrastructure.Factories.Controllers.Taverns.TavernPickUpPoints
{
    public class TavernPickUpPointPresenterFactory
    {
        private readonly ItemsFactory _itemsFactory;

        public TavernPickUpPointPresenterFactory(ItemsFactory itemsFactory)
        {
            _itemsFactory = itemsFactory ??
                            throw new ArgumentNullException(nameof(itemsFactory));
        }

        public TavernFudPickUpPointPresenter Create(
            FoodPickUpPoint foodPickUpPoint,
            ITavernFudPickUpPointView tavernFudPickUpPointView,
            PickUpPointUIImages pickUpPointUIImages,
            ItemConfig itemConfig)
        {
            if (foodPickUpPoint == null)
                throw new ArgumentNullException(nameof(foodPickUpPoint));

            if (tavernFudPickUpPointView == null)
                throw new ArgumentNullException(nameof(tavernFudPickUpPointView));

            if (pickUpPointUIImages == null)
                throw new ArgumentNullException(nameof(pickUpPointUIImages));

            if (itemConfig == null)
                throw new ArgumentNullException(nameof(itemConfig));

            return new TavernFudPickUpPointPresenter(
                foodPickUpPoint,
                tavernFudPickUpPointView,
                _itemsFactory,
                pickUpPointUIImages,
                itemConfig);
        }
    }
}