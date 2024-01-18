using System;
using Sources.Controllers.Taverns;
using Sources.Domain.Items.ItemConfigs;
using Sources.Infrastructure.Factories.Domains.Items;
using Sources.Presentation.Views.Taverns.PickUpPoints.Foods;
using Sources.PresentationInterfaces.Views.Taverns.PickUpPoints;

namespace Sources.Infrastructure.Factories.Controllers.Taverns.TavernPickUpPoints
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
            ITavernFudPickUpPointView tavernFudPickUpPointView,
            PickUpPointUIImages pickUpPointUIImages, ItemConfig itemConfig)
        {
            if (tavernFudPickUpPointView == null) 
                throw new ArgumentNullException(nameof(tavernFudPickUpPointView));
            
            if (pickUpPointUIImages == null) 
                throw new ArgumentNullException(nameof(pickUpPointUIImages));
            
            return new TavernFudPickUpPointPresenter(
                tavernFudPickUpPointView, _itemsFactory, pickUpPointUIImages, itemConfig);
        }
    }
}