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
            PickUpPointUI pickUpPointUI, ImageUIFactory imageUIFactory, ItemConfig itemConfig) where T : IItem
        {
            if (beerPickUpPointView == null) 
                throw new ArgumentNullException(nameof(beerPickUpPointView));
            if (pickUpPointUI == null) 
                throw new ArgumentNullException(nameof(pickUpPointUI));
            if (imageUIFactory == null) 
                throw new ArgumentNullException(nameof(imageUIFactory));

            imageUIFactory.Create(pickUpPointUI.Image);
            imageUIFactory.Create(pickUpPointUI.BackgroundImage);
            
            TavernFudPickUpPointPresenter tavernPickUpPointPresenterFactory =
                _tavernPickUpPointPresenterFactory.Create(beerPickUpPointView,
                    pickUpPointUI, itemConfig);
            
            beerPickUpPointView.Construct(tavernPickUpPointPresenterFactory);

            return beerPickUpPointView;
        }
    }
}