using System;
using Sources.Domain.Items.ItemConfigs;
using Sources.Domain.Taverns;
using Sources.Infrastructure.Factories.Views.UI.AudioSources;
using Sources.Presentation.Containers.GamePoints;

namespace Sources.Infrastructure.Factories.Views.Taverns.PickUpPoints
{
    public class FoodPickUpPointsViewFactory
    {
        private readonly AudioSourceUIFactory _audioSourceUIFactory;
        private readonly TavernFoodPickUpPointViewFactory _tavernFoodPickUpPointViewFactory;

        public FoodPickUpPointsViewFactory(
            TavernFoodPickUpPointViewFactory tavernFoodPickUpPointViewFactory,
            AudioSourceUIFactory audioSourceUIFactory)
        {
            _tavernFoodPickUpPointViewFactory = tavernFoodPickUpPointViewFactory ??
                                                throw new ArgumentNullException(
                                                    nameof(tavernFoodPickUpPointViewFactory));
            _audioSourceUIFactory = audioSourceUIFactory ??
                                    throw new ArgumentNullException(nameof(audioSourceUIFactory));
        }

        public void Create(
            FoodPickUpPointContainer foodContainer,
            ItemConfigContainer itemConfigContainer,
            RootGamePoints rootGamePoints)
        {
            _tavernFoodPickUpPointViewFactory.Create(foodContainer.Beer,
                rootGamePoints.BeerPickUpPointView,
                itemConfigContainer.Beer);
            _tavernFoodPickUpPointViewFactory.Create(foodContainer.Bread,
                rootGamePoints.BreadPickUpPointView,
                itemConfigContainer.Bread);
            _tavernFoodPickUpPointViewFactory.Create(foodContainer.Meat,
                rootGamePoints.MeatPickUpPointView,
                itemConfigContainer.Meat);
            _tavernFoodPickUpPointViewFactory.Create(foodContainer.Soup,
                rootGamePoints.SoupPickUpPointView,
                itemConfigContainer.Soup);
            _tavernFoodPickUpPointViewFactory.Create(foodContainer.Wine,
                rootGamePoints.WinePickUpPointView,
                itemConfigContainer.Wine);

            _audioSourceUIFactory.Create(foodContainer.Beer,
                rootGamePoints.UpgradePointsInteractionAudioSourceContainer.Beer);
            _audioSourceUIFactory.Create(foodContainer.Bread,
                rootGamePoints.UpgradePointsInteractionAudioSourceContainer.Bread);
            _audioSourceUIFactory.Create(foodContainer.Meat,
                rootGamePoints.UpgradePointsInteractionAudioSourceContainer.Meat);
            _audioSourceUIFactory.Create(foodContainer.Soup,
                rootGamePoints.UpgradePointsInteractionAudioSourceContainer.Soup);
            _audioSourceUIFactory.Create(foodContainer.Wine,
                rootGamePoints.UpgradePointsInteractionAudioSourceContainer.Wine);
        }
    }
}