using System;
using System.Threading;
using Sources.Domain.Constants;
using Sources.Domain.Taverns;
using Sources.Domain.Visitors;
using Sources.Infrastructure.Factories.Views.Items.Common;
using Sources.Infrastructure.Spawners;
using Sources.Infrastructure.StateMachines.FiniteStateMachines.States;
using Sources.InfrastructureInterfaces.Spawners;
using Sources.Presentation.Views.Visitors;
using Sources.PresentationInterfaces.Views;
using Sources.PresentationInterfaces.Views.Items.Coins;
using Sources.PresentationInterfaces.Views.Items.Garbages;
using UnityEngine;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorEatFoodState : FiniteState
    {
        private readonly Visitor _visitor;
        private readonly VisitorInventory _visitorInventory;
        private readonly ItemViewFactory _itemViewFactory;
        private readonly TavernMood _tavernMood;
        private readonly ISpawner<IGarbageView> _garbageSpawner;
        private readonly ISpawner<ICoinView> _coinSpawner;
        private readonly VisitorImageUIContainer _visitorImageUIContainer;

        public VisitorEatFoodState
        (
            Visitor visitor,
            VisitorInventory visitorInventory,
            VisitorImageUIContainer visitorImageUIContainer,
            ItemViewFactory itemViewFactory,
            TavernMood tavernMood,
            ISpawner<IGarbageView> garbageSpawner,
            ISpawner<ICoinView> coinSpawner
        )
        {
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorInventory = visitorInventory ?? throw new ArgumentNullException(nameof(visitorInventory));
            _itemViewFactory = itemViewFactory ?? throw new ArgumentNullException(nameof(itemViewFactory));
            _tavernMood = tavernMood ?? throw new ArgumentNullException(nameof(tavernMood));
            _garbageSpawner = garbageSpawner ?? throw new ArgumentNullException(nameof(garbageSpawner));
            _coinSpawner = coinSpawner ?? throw new ArgumentNullException(nameof(coinSpawner));
            _visitorImageUIContainer = visitorImageUIContainer
                ? visitorImageUIContainer
                : throw new ArgumentNullException(nameof(visitorImageUIContainer));
        }

        public override void Enter()
        {
            IItemView itemView = _itemViewFactory.Create(_visitorInventory.Item);
            itemView.SetPosition(_visitor.SeatPointView.EatPointView.transform);
            Eat(itemView);
        }

        public override void Exit()
        {
            _visitor.SeatPointView.UnOccupy();
            _visitor.FinishEating();
            IGarbageView garbageView = _garbageSpawner.Spawn();
            garbageView.SetPosition(_visitor.SeatPointView.EatPointView.Position);
            garbageView.SetEatPointView(_visitor.SeatPointView.EatPointView);
            _visitor.SeatPointView.EatPointView.GetDirty();
            ICoinView coinView = _coinSpawner.Spawn();
            coinView.SetTransformPosition(_visitor.SeatPointView.EatPointView.Position);
            coinView.SetCoinAmount(_visitorInventory.Item.Price);
        }

        private async void Eat(IItemView itemView)
        {
            await _visitorImageUIContainer.BackGroundImage.FillMoveTowardsAsync(
                Constant.Visitors.EatFillingRate, new CancellationTokenSource().Token);
            _visitorImageUIContainer.BackGroundImage.SetFillAmount(Constant.FillingAmount.Maximum);
            _visitor.SetUnSeat();
            itemView.Destroy();
            _tavernMood.AddTavernMood();
        }
    }
}