using System;
using JetBrains.Annotations;
using Sources.Controllers.Taverns;
using Sources.Infrastructure.Factorys.Domains.Items;
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
            IImageUI imageUI)
        {
            if (tavernFudPickUpPointView == null) 
                throw new ArgumentNullException(nameof(tavernFudPickUpPointView));
            if (imageUI == null) 
                throw new ArgumentNullException(nameof(imageUI));
            
            return new TavernFudPickUpPointPresenter(
                tavernFudPickUpPointView, _itemsFactory, imageUI);
        }
    }
}