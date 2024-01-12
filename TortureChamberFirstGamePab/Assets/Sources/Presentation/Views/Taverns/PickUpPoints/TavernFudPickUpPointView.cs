using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MyProject.Sources.Presentation.Views;
using Sources.Controllers.Taverns;
using Sources.DomainInterfaces.Items;
using Sources.PresentationInterfaces.Views;
using Sources.PresentationInterfaces.Views.Interactions.Givable;
using Sources.PresentationInterfaces.Views.Taverns.PickUpPoints;
using UnityEngine;

namespace Sources.Presentation.Views.Taverns
{
    public class TavernFudPickUpPointView<TItem> : 
        PresentableView<TavernFudPickUpPointPresenter>,
        ITavernFudPickUpPointView,
        IGivable
    where TItem : IItem
    {
        [field: SerializeField] public float FillingRate { get; private set; } = 0.1f;
        
        //TODO какие методы нужно называвть с приставкий асинк? все у которых есть приставка асинк.
        public async UniTask<IItem> GiveItemAsync(CancellationToken cancellationToken)
        {
            return await Presenter.GiveItemAsync<TItem>(cancellationToken);
        }
    }
}