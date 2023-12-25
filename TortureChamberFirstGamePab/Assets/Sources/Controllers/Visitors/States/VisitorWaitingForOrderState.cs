using System;
using JetBrains.Annotations;
using Sources.Domain.Items;
using Sources.Domain.Visitors;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.StateMachines.States;
using Sources.PresentationInterfaces.Views.Visitors;
using Sources.Utils.Repositoryes;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorWaitingForOrderState : FiniteState
    {
        private readonly VisitorInventory _visitorInventory;
        private readonly ItemRepository<IItem> _itemRepository;
        private readonly IVisitorImageUI _visitorImageUI;

        public VisitorWaitingForOrderState(VisitorInventory visitorInventory,
            IVisitorImageUI visitorImageUI,
            ItemRepository<IItem> itemRepository)
        {
            _visitorInventory = visitorInventory ??
                                throw new ArgumentNullException(nameof(visitorInventory));
            _itemRepository = itemRepository ??
                              throw new ArgumentNullException(nameof(itemRepository));
            _visitorImageUI = visitorImageUI ??
                              throw new ArgumentNullException(nameof(visitorImageUI));
        }

        public override void Enter()
        {
            // Debug.Log("Посетитель в состоянии ожидания заказа");
            Beer beer = _itemRepository.Get<Beer>();

            _visitorImageUI.OrderImage.SetSprite(beer.Icon);
            _visitorImageUI.OrderImage.Show();
            _visitorImageUI.BackGroundImage.Show();

            _visitorInventory.SetTargetItem(beer);
        }

        public override void Exit()
        {
            _visitorInventory.SetTargetItem(null);
        }
    }
}