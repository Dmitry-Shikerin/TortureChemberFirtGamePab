using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Domain.Constants;
using Domain.Items.ItemConfigs;
using Domain.Taverns;
using DomainInterfaces.Items;
using Infrastructure.Factories.Domains.Items;
using Presentation.Containers.UI.Images;
using PresentationInterfaces.Views.Taverns.PickUpPoints;

namespace Controllers.Taverns
{
    public class TavernFudPickUpPointPresenter : PresenterBase
    {
        private readonly FoodPickUpPoint _foodPickUpPoint;
        private readonly ItemConfig _itemConfig;
        private readonly ItemsFactory _itemsFactory;
        private readonly PickUpPointUIImages _pickUpPointUIImages;
        private readonly ITavernFudPickUpPointView _tavernFudPickUpPointView;

        public TavernFudPickUpPointPresenter(
            FoodPickUpPoint foodPickUpPoint,
            ITavernFudPickUpPointView tavernFudPickUpPointView,
            ItemsFactory itemsFactory,
            PickUpPointUIImages pickUpPointUIImages,
            ItemConfig itemConfig)
        {
            _foodPickUpPoint = foodPickUpPoint ?? throw new ArgumentNullException(nameof(foodPickUpPoint));
            _tavernFudPickUpPointView = tavernFudPickUpPointView ??
                                        throw new ArgumentNullException(nameof(tavernFudPickUpPointView));
            _itemsFactory = itemsFactory ??
                            throw new ArgumentNullException(nameof(itemsFactory));
            _pickUpPointUIImages = pickUpPointUIImages
                ? pickUpPointUIImages
                : throw new ArgumentNullException(nameof(pickUpPointUIImages));
            _itemConfig = itemConfig ? itemConfig : throw new ArgumentNullException(nameof(itemConfig));
        }

        public override void Enable() =>
            _pickUpPointUIImages.Image.SetSprite(_itemConfig.Icon);

        public async UniTask<IItem> GiveItemAsync<TItem>(CancellationToken cancellationToken)
            where TItem : IItem
        {
            try
            {
                _foodPickUpPoint.StartAudioSource();

                await _pickUpPointUIImages.BackgroundImage.FillMoveTowardsAsync(
                    _tavernFudPickUpPointView.FillingRate, cancellationToken);

                _pickUpPointUIImages.BackgroundImage.SetFillAmount(FillingAmountConstant.Maximum);

                _foodPickUpPoint.StopAudioSource();

                return _itemsFactory.Create<TItem>();
            }
            catch (OperationCanceledException)
            {
                _pickUpPointUIImages.BackgroundImage.SetFillAmount(FillingAmountConstant.Maximum);

                _foodPickUpPoint.StopAudioSource();

                return default;
            }
        }
    }
}