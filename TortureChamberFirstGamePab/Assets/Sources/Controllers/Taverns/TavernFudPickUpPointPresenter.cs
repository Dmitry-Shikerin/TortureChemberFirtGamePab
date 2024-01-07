using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using MyProject.Sources.Controllers.Common;
using Sources.Domain.Items.ItemConfigs;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factorys.Domains.Items;
using Sources.Presentation.UI.PickUpPointUIs;
using Sources.PresentationInterfaces.UI;
using Sources.PresentationInterfaces.Views.Taverns.PickUpPoints;

namespace Sources.Controllers.Taverns
{
    public class TavernFudPickUpPointPresenter : PresenterBase
    {
        private readonly ITavernFudPickUpPointView _tavernFudPickUpPointView;
        private readonly ItemsFactory _itemsFactory;
        private readonly PickUpPointUI _pickUpPointUI;
        private readonly ItemConfig _itemConfig;

        //TODO сделать модель для таверны, зачем?
        public TavernFudPickUpPointPresenter(ITavernFudPickUpPointView tavernFudPickUpPointView, 
            ItemsFactory itemsFactory, PickUpPointUI pickUpPointUI, ItemConfig itemConfig)
        {
            _tavernFudPickUpPointView = tavernFudPickUpPointView ?? 
                                    throw new ArgumentNullException(nameof(tavernFudPickUpPointView));
            _itemsFactory = itemsFactory ?? 
                            throw new ArgumentNullException(nameof(itemsFactory));
            _pickUpPointUI = pickUpPointUI ? pickUpPointUI : 
                throw new ArgumentNullException(nameof(pickUpPointUI));
            _itemConfig = itemConfig ? itemConfig : 
                throw new ArgumentNullException(nameof(itemConfig));
            
            _pickUpPointUI.Image.SetSprite(_itemConfig.Icon);
        }
        
        //TODO посмотреть инфу про онВалидейт
        public async UniTask<IItem> TakeItemAsync<TItem>(CancellationToken cancellationToken) where TItem : IItem
        {
            try
            {
                await _pickUpPointUI.BackgroundImage.FillMoveTowardsAsync(_tavernFudPickUpPointView.FillingRate, cancellationToken);

                return _itemsFactory.Create<TItem>();
            }
            catch (OperationCanceledException)
            {
                _pickUpPointUI.BackgroundImage.SetFillAmount(1);
                
                return default;
            }
        }
    }
}