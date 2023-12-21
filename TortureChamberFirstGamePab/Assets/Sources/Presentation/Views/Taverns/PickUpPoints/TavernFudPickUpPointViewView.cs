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
    public class TavernFudPickUpPointViewView<TItem> : 
        PresentableView<TavernFudPickUpPointPresenter>,
        ITavernFudPickUpPointView,
        ITakeble
    where TItem : IItem
    {
        public UniTask<IItem> TakeItemAsync(CancellationToken cancellationToken)
        {
            return Presenter.Take<TItem>(cancellationToken);
        }

        // public IItemView TakeItemView() => 
        //     Presenter.TakeItemView();
    }
}