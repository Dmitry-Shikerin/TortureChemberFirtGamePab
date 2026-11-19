using System.Threading;
using Controllers.Taverns;
using Cysharp.Threading.Tasks;
using DomainInterfaces.Items;
using Presentation.Containers.UI.Images;
using PresentationInterfaces.Views.Interactions.Givable;
using PresentationInterfaces.Views.Taverns.PickUpPoints;
using UnityEngine;

namespace Presentation.Views.Taverns.PickUpPoints
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