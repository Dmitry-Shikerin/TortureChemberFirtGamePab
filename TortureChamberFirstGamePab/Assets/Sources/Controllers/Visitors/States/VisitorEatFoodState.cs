using System;
using System.Threading;
using JetBrains.Annotations;
using Sources.Domain.Taverns;
using Sources.Domain.Visitors;
using Sources.Infrastructure.Factories.Views.Items.Common;
using Sources.Infrastructure.StateMachines.States;
using Sources.Presentation.UI;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.UI;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Repositoryes;
using UnityEngine;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorEatFoodState : FiniteState
    {
        private readonly IVisitorView _visitorView;
        private readonly Visitor _visitor;
        private readonly IVisitorAnimation _visitorAnimation;
        private readonly CollectionRepository _collectionRepository;
        private readonly VisitorInventory _visitorInventory;
        private readonly ItemViewFactory _itemViewFactory;
        private readonly TavernMood _tavernMood;
        private readonly VisitorImageUI _visitorImageUI;

        public VisitorEatFoodState(IVisitorView visitorView, Visitor visitor,
            IVisitorAnimation visitorAnimation, CollectionRepository collectionRepository,
            VisitorInventory visitorInventory, VisitorImageUI visitorImageUI,
            ItemViewFactory itemViewFactory, TavernMood tavernMood)
        {
            _visitorView = visitorView ?? throw new ArgumentNullException(nameof(visitorView));
            _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
            _visitorAnimation = visitorAnimation ??
                                throw new ArgumentNullException(nameof(visitorAnimation));
            _collectionRepository = collectionRepository ??
                                    throw new ArgumentNullException(nameof(collectionRepository));
            _visitorInventory = visitorInventory ?? throw new ArgumentNullException(nameof(visitorInventory));
            _itemViewFactory = itemViewFactory ?? throw new ArgumentNullException(nameof(itemViewFactory));
            _tavernMood = tavernMood ?? throw new ArgumentNullException(nameof(tavernMood));
            _visitorImageUI = visitorImageUI ? visitorImageUI : throw new ArgumentNullException(nameof(visitorImageUI));
        }

        public override void Enter()
        {
            Debug.Log("Посетитель в состоянии поедания еды");
            IItemView itemView = _itemViewFactory.Create(_visitorInventory.Item);
            itemView.SetPosition(_visitor.SeatPoint.EatPoint.transform);
            Debug.Log(itemView);
            Eat(itemView);
        }

        public override void Exit()
        {
        }

        private async void Eat(IItemView itemView)
        {
            //TODO убрать магические числа
            await _visitorImageUI.BackGroundImage.FillMoveTowardsAsync(0.2f, new CancellationTokenSource().Token);
            _visitorImageUI.BackGroundImage.SetFillAmount(1);
            _visitor.SetCanSeat(false);
            itemView.Destroy();
            _tavernMood.AddTavernMood();
        }
    }
}