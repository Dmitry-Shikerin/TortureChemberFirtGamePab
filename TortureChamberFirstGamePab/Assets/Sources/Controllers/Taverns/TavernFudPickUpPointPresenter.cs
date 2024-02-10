using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Domain.Constants;
using Sources.Domain.Items.ItemConfigs;
using Sources.Domain.Taverns;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factories.Domains.Items;
using Sources.Presentation.Views.Taverns.PickUpPoints.Foods;
using Sources.PresentationInterfaces.Views.Taverns.PickUpPoints;

namespace Sources.Controllers.Taverns
{
    public class TavernFudPickUpPointPresenter : PresenterBase
    {
        private readonly FoodPickUpPoint _foodPickUpPoint;
        private readonly ITavernFudPickUpPointView _tavernFudPickUpPointView;
        private readonly ItemsFactory _itemsFactory;
        private readonly PickUpPointUIImages _pickUpPointUIImages;
        private readonly ItemConfig _itemConfig;

        public TavernFudPickUpPointPresenter
        (
            FoodPickUpPoint foodPickUpPoint,
            ITavernFudPickUpPointView tavernFudPickUpPointView,
            ItemsFactory itemsFactory,
            PickUpPointUIImages pickUpPointUIImages,
            ItemConfig itemConfig
        )
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

        public async UniTask<IItem> GiveItemAsync<TItem>(CancellationToken cancellationToken) where TItem : IItem
        {
            try
            {
                _foodPickUpPoint.StartAudioSource();
                
                await _pickUpPointUIImages.BackgroundImage.FillMoveTowardsAsync(
                    _tavernFudPickUpPointView.FillingRate, cancellationToken);
                
                _pickUpPointUIImages.BackgroundImage.SetFillAmount(Constant.FillingAmount.Maximum);

                _foodPickUpPoint.StopAudioSource();
                
                return _itemsFactory.Create<TItem>();
            }
            catch (OperationCanceledException)
            {
                _pickUpPointUIImages.BackgroundImage.SetFillAmount(Constant.FillingAmount.Maximum);

                _foodPickUpPoint.StopAudioSource();
                
                return default;
            }
        }
    }
}