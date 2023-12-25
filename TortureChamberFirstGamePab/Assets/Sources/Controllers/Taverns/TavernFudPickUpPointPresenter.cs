using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MyProject.Sources.Controllers.Common;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factorys.Domains.Items;
using Sources.PresentationInterfaces.UI;
using Sources.PresentationInterfaces.Views.Taverns.PickUpPoints;

namespace Sources.Controllers.Taverns
{
    public class TavernFudPickUpPointPresenter : PresenterBase
    {
        private readonly ITavernFudPickUpPointView _tavernFudPickUpPointView;
        private readonly ItemsFactory _itemsFactory;
        private readonly IImageUI _imageUI;

        //TODO сделать модель для таверны, зачем?
        public TavernFudPickUpPointPresenter(ITavernFudPickUpPointView tavernFudPickUpPointView, 
            ItemsFactory itemsFactory, IImageUI imageUI)
        {
            _tavernFudPickUpPointView = tavernFudPickUpPointView ?? 
                                    throw new ArgumentNullException(nameof(tavernFudPickUpPointView));
            _itemsFactory = itemsFactory ?? 
                            throw new ArgumentNullException(nameof(itemsFactory));
            _imageUI = imageUI ?? throw new ArgumentNullException(nameof(imageUI));
        }
        
        //TODO посмотреть инфу про онВалидейт
        public async UniTask<IItem> TakeItemAsync<TItem>(CancellationToken cancellationToken) where TItem : IItem
        {
            try
            {
                await _imageUI.FillMoveTowardsAsync(_tavernFudPickUpPointView.FillingRate, cancellationToken);

                return _itemsFactory.Create<TItem>();
            }
            catch (OperationCanceledException exception)
            {
                _imageUI.SetFillAmount(0);
                
                return default;
            }
        }
    }
}