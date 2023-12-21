using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using MyProject.Sources.Controllers.Common;
using Sources.Domain.Items;
using Sources.Domain.Items.ItemConfigs;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factorys.Domains.Items;
using Sources.PresentationInterfaces.UI;
using Sources.PresentationInterfaces.Views;
using Sources.PresentationInterfaces.Views.Taverns.PickUpPoints;
using UnityEngine;

namespace Sources.Controllers.Taverns
{
    public class TavernFudPickUpPointPresenter : PresenterBase
    {
        private readonly ITavernFudPickUpPointView _tavernFudPickUpPointView;
        private readonly ItemsFactory _itemsFactory;
        private readonly IImageUI _imageUI;

        public TavernFudPickUpPointPresenter(ITavernFudPickUpPointView tavernFudPickUpPointView, 
            ItemsFactory itemsFactory, IImageUI imageUI)
        {
            _tavernFudPickUpPointView = tavernFudPickUpPointView ?? 
                                    throw new ArgumentNullException(nameof(tavernFudPickUpPointView));
            _itemsFactory = itemsFactory ?? 
                            throw new ArgumentNullException(nameof(itemsFactory));
            _imageUI = imageUI ?? throw new ArgumentNullException(nameof(imageUI));
        }
        
        public async UniTask<IItem> Take<TItem>(CancellationToken cancellationToken) where TItem : IItem
        {
            try
            {
                _imageUI.SetFillAmount(1);

                while (_imageUI.FillAmount > 0.01)
                {
                    float fill = Mathf.MoveTowards(
                        _imageUI.FillAmount, 0, 0.1f * Time.deltaTime);
                    _imageUI.SetFillAmount(fill);

                    await UniTask.Yield(cancellationToken);
                }

                _imageUI.SetFillAmount(0);

                return _itemsFactory.Create<TItem>();
            }
            catch (OperationCanceledException exception)
            {
                _imageUI.SetFillAmount(0);
                return default;
            }
        }
        
        
        
        
        // public TItem Take<TItem>() where TItem : IItem
        // {
        //     return _itemsFactory.Create<TItem>();
        // }
    }
}