using System.Threading;
using Cysharp.Threading.Tasks;
using Scripts.Controllers.Taverns;
using Scripts.DomainInterfaces.Items;
using Scripts.Presentation.Containers.UI.Images;
using Scripts.PresentationInterfaces.Views.Interactions.Givable;
using Scripts.PresentationInterfaces.Views.Taverns.PickUpPoints;
using UnityEngine;

namespace Scripts.Presentation.Views.Taverns.PickUpPoints
{
    public class TavernFudPickUpPointView<TItem>
        : PresentableView<TavernFudPickUpPointPresenter>,
        ITavernFudPickUpPointView, IGivable
        where TItem : IItem
    {
        [field: SerializeField] public PickUpPointUIImages PickUpPointUIImages { get; private set; }
        [field: SerializeField] public float FillingRate { get; private set; } = 0.1f;

        public async UniTask<IItem> GiveItemAsync(CancellationToken cancellationToken) =>
            await Presenter.GiveItemAsync<TItem>(cancellationToken);
    }
}