using System;
using JetBrains.Annotations;
using Sources.Domain.Items;
using Sources.Domain.Visitors;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.StateMachines.States;
using Sources.Presentation.UI;
using Sources.PresentationInterfaces.Animations;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Repositoryes;
using UnityEngine;

namespace Sources.Controllers.Visitors.States
{
    public class VisitorWaitingForOrderState : FiniteState
    {
        private readonly ItemRepository<IItem> _itemRepository;

        //TODO хочется убрать отсюда этот класс
        //TODO сделать интерфей
        private readonly VisitorImageUIView _visitorImageUIView;

        public VisitorWaitingForOrderState(IVisitorView visitorView, Visitor visitor,
            IVisitorAnimation visitorAnimation , CollectionRepository collectionRepository,
            VisitorImageUIView visitorImageUIView, ItemRepository<IItem> itemRepository)
        {
            _itemRepository = itemRepository ?? 
                              throw new ArgumentNullException(nameof(itemRepository));
            _visitorImageUIView = visitorImageUIView ? visitorImageUIView : 
                throw new ArgumentNullException(nameof(visitorImageUIView));
        }
        
        public override void Enter()
        {
            Debug.Log("Посетитель в состоянии ожидания заказа");
            // Beer beer = _itemRepository.Get<Beer>();
            //
            // _visitorImageUIView.OrderImage.SetSprite(beer.Icon);
            // _visitorImageUIView.OrderImage.Show();
            // _visitorImageUIView.BackGroundImage.Show();
        }

        public override void Exit()
        {
        }
    }
}