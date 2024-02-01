using System.Threading;
using Cysharp.Threading.Tasks;
using Sources.Controllers.Taverns;
using Sources.DomainInterfaces.Items;
using Sources.Presentation.Views.Taverns.PickUpPoints.Foods;
using Sources.PresentationInterfaces.Views.Interactions.Givable;
using Sources.PresentationInterfaces.Views.Taverns.PickUpPoints;
using UnityEngine;

namespace Sources.Presentation.Views.Taverns.PickUpPoints
{
    public class TavernFudPickUpPointView<TItem> : 
        PresentableView<TavernFudPickUpPointPresenter>,
        ITavernFudPickUpPointView,
        IGivable where TItem : IItem
    {
        [field: SerializeField] public PickUpPointUIImages PickUpPointUIImages { get; private set; }
        [field: SerializeField] public float FillingRate { get; private set; } = 0.1f;
        
        public async UniTask<IItem> GiveItemAsync(CancellationToken cancellationToken) => 
            await Presenter.GiveItemAsync<TItem>(cancellationToken);
    }
}