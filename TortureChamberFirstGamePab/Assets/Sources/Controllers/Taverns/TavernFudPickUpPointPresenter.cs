using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MyProject.Sources.Controllers.Common;
using Sources.Domain.Items.ItemConfigs;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factorys.Domains.Items;
using Sources.Presentation.UI.PickUpPointUIs;
using Sources.PresentationInterfaces.Views.Taverns.PickUpPoints;

namespace Sources.Controllers.Taverns
{
    public class TavernFudPickUpPointPresenter : PresenterBase
    {
        private readonly ITavernFudPickUpPointView _tavernFudPickUpPointView;
        private readonly ItemsFactory _itemsFactory;
        private readonly PickUpPointUIImages _pickUpPointUIImages;
        private readonly ItemConfig _itemConfig;

        public TavernFudPickUpPointPresenter(ITavernFudPickUpPointView tavernFudPickUpPointView, 
            ItemsFactory itemsFactory, PickUpPointUIImages pickUpPointUIImages, ItemConfig itemConfig)
        {
            _tavernFudPickUpPointView = tavernFudPickUpPointView ?? 
                                    throw new ArgumentNullException(nameof(tavernFudPickUpPointView));
            _itemsFactory = itemsFactory ?? 
                            throw new ArgumentNullException(nameof(itemsFactory));
            _pickUpPointUIImages = pickUpPointUIImages ? pickUpPointUIImages : 
                throw new ArgumentNullException(nameof(pickUpPointUIImages));
            _itemConfig = itemConfig ? itemConfig : 
                throw new ArgumentNullException(nameof(itemConfig));
            
            _pickUpPointUIImages.Image.SetSprite(_itemConfig.Icon);
        }
        
        public async UniTask<IItem> GiveItemAsync<TItem>(CancellationToken cancellationToken) where TItem : IItem
        {
            try
            {
                await _pickUpPointUIImages.BackgroundImage.FillMoveTowardsAsync(_tavernFudPickUpPointView.FillingRate, cancellationToken);
                _pickUpPointUIImages.BackgroundImage.SetFillAmount(1);
                
                return _itemsFactory.Create<TItem>();
            }
            catch (OperationCanceledException)
            {
                _pickUpPointUIImages.BackgroundImage.SetFillAmount(1);
                
                return default;
            }
        }
    }
}