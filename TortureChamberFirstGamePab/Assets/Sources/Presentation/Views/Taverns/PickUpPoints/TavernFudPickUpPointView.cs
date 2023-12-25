using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MyProject.Sources.Presentation.Views;
using Sources.Controllers.Taverns;
using Sources.DomainInterfaces.Items;
using Sources.PresentationInterfaces.Views;
using Sources.PresentationInterfaces.Views.Taverns.PickUpPoints;
using UnityEngine;

namespace Sources.Presentation.Views.Taverns
{
    public class TavernFudPickUpPointView<TItem> : 
        PresentableView<TavernFudPickUpPointPresenter>,
        ITavernFudPickUpPointView,
        ITakeble
    where TItem : IItem
    {
        [field: SerializeField] public float FillingRate { get; private set; } = 0.1f;
        
        public async UniTask<IItem> TakeItem(CancellationToken cancellationToken)
        {
            return await Presenter.TakeItemAsync<TItem>(cancellationToken);
        }
    }
}