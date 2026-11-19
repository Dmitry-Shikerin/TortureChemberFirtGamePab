using System;
using System.Threading;
using Domain.Constants;
using Domain.Taverns;
using Domain.Visitors;
using Infrastructure.Factories.Views.Items.Common;
using Infrastructure.StateMachines.FiniteStateMachines.States;
using InfrastructureInterfaces.Spawners;
using Presentation.Containers.UI.Images;
using PresentationInterfaces.Views.Items;
using PresentationInterfaces.Views.Items.Coins;
using PresentationInterfaces.Views.Items.Garbages;
using UnityEngine;
using Utils.Extensions.ShuffleExtensions;

namespace Controllers.Visitors.States
{
    public class VisitorEatFoodState : FiniteState
    {
        private readonly ISpawner<ICoinView> _coinSpawner;
        private readonly ISpawner<IGarbageView> _garbageSpawner;
        private readonly ItemViewFactory _itemViewFactory;
        private readonly TavernMood _tavernMood;
        private readonly Visitor _visitor;
        private readonly VisitorImageUIContainer _visitorImageUIContainer;
        private readonly VisitorInventory _visitorInventory;

        private CancellationTokenSource _cancellationTokenSource;

        public VisitorEatFoodState(
            Visitor visitor,
            VisitorInventory visitorInventory,
            VisitorImageUIContainer visitorImageUIContainer,
            ItemViewFactory itemViewFactory,
            TavernMood tavernMood,
            ISpawner<IGarbageView> garbageSpawner,
            ISpawner<ICoinView> coinSpawner)
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
            _cancellationTokenSource = new CancellationTokenSource();

            _visitorImageUIContainer.OrderImage.SetSprite(_visitorImageUIContainer.EatSprite);
            var itemView = _itemViewFactory.Create(_visitorInventory.Item);
            itemView.SetTransformPosition(_visitor.SeatPointView.EatPointView.transform);

            Eat(itemView, _cancellationTokenSource.Token);
        }

        public override void Exit()
        {
            _visitor.SeatPointView.UnOccupy();

            if (ShuffleExtension.GetRandomChance(
                    GarbageRandomizerConstant.PositiveRange,
                    GarbageRandomizerConstant.MaximumRange))
            {
                IGarbageView garbageView = _garbageSpawner.Spawn();
                garbageView.SetPosition(_visitor.SeatPointView.EatPointView.Position);
                garbageView.SetEatPointView(_visitor.SeatPointView.EatPointView);
                _visitor.SeatPointView.EatPointView.SetDirty();
            }

            ICoinView coinView = _coinSpawner.Spawn();

            Vector3 coinPosition = new Vector3(
                _visitor.SeatPointView.EatPointView.Position.x,
                _visitor.SeatPointView.EatPointView.Position.y + CoinConstant.OffsetY,
                _visitor.SeatPointView.EatPointView.Position.z);

            coinView.SetPosition(coinPosition);
            coinView.SetCoinAmount(_visitorInventory.Item.Price);

            _cancellationTokenSource.Cancel();
        }

        private async void Eat(IItemView itemView, CancellationToken cancellationToken)
        {
            try
            {
                await _visitorImageUIContainer.BackGroundImage.FillMoveTowardsAsync(
                    VisitorConstant.EatFillingRate, cancellationToken);

                _visitorImageUIContainer.BackGroundImage.SetFillAmount(FillingAmountConstant.Maximum);
                itemView.Destroy();
                _tavernMood.AddTavernMood();
                _visitor.SetUnSeat();
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}