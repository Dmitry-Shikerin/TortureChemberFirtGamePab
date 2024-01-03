using System;
using JetBrains.Annotations;
using Sources.Controllers.Taverns;
using Sources.Domain.Items.ItemConfigs;
using Sources.Infrastructure.Factorys.Domains.Items;
using Sources.Presentation.UI.PickUpPointUIs;
using Sources.PresentationInterfaces.UI;
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
            PickUpPointUI pickUpPointUI, ItemConfig itemConfig)
        {
            if (tavernFudPickUpPointView == null) 
                throw new ArgumentNullException(nameof(tavernFudPickUpPointView));
            if (pickUpPointUI == null) 
                throw new ArgumentNullException(nameof(pickUpPointUI));
            
            return new TavernFudPickUpPointPresenter(
                tavernFudPickUpPointView, _itemsFactory, pickUpPointUI, itemConfig);
        }
    }
}