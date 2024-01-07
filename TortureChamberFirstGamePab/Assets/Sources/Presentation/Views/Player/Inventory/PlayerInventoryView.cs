using System.Collections.Generic;
using MyProject.Sources.Controllers;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Controllers.Player;
using Sources.DomainInterfaces.Items;
using Sources.Presentation.Views.Player.Inventory;
using Sources.Presentation.Views.Player.Triggers;
using Sources.Presentation.Views.Taverns;
using Sources.PresentationInterfaces.Views.Interactions.Get;
using UnityEngine;

namespace Sources.Presentation.Views.Player
{
    public class PlayerInventoryView : PresentableView<PlayerInventoryPresenter>, IPlayerInventoryView
    {
        [field : SerializeField] public PlayerInventorySlotView FirstSlotView { get; private set; }
        [field : SerializeField] public PlayerInventorySlotView SecondSlotView { get; private set; }
        [field : SerializeField] public PlayerInventorySlotView ThirdSlotView { get; private set; }

        [field: SerializeField] public float FillingRate { get; private set; } = 0.2f;
        
        private GettableTrigger _gettableTrigger;
        private TakebleTrigger _takebleTrigger;
        private List<PlayerInventorySlotView> _playerInventorySlots;

        public bool TryGet()
        {
            return Presenter.TryGet();
        }

        public IReadOnlyList<PlayerInventorySlotView> PlayerInventorySlots => _playerInventorySlots;

        private void Awake()
        {
            _playerInventorySlots = new List<PlayerInventorySlotView>(3);
            _playerInventorySlots.Add(FirstSlotView);
            _playerInventorySlots.Add(SecondSlotView);
            _playerInventorySlots.Add(ThirdSlotView);

            _gettableTrigger = GetComponentInChildren<GettableTrigger>();
            _takebleTrigger = GetComponentInChildren<TakebleTrigger>();
        }

        protected override void OnAfterEnable()
        {
            _gettableTrigger.Entered += OnGettableEnter;
            _gettableTrigger.Exited += OnGettableExit;

            _takebleTrigger.Entered += OnTakebleEnter;
            _takebleTrigger.Exited += OnTakebleExit;
        }

        protected override void OnAfterDisable()
        {
            _gettableTrigger.Entered -= OnGettableEnter;
            _gettableTrigger.Exited -= OnGettableExit;
            
            _takebleTrigger.Entered -= OnTakebleEnter;
            _takebleTrigger.Entered -= OnTakebleExit;
        }

        private void OnTakebleEnter(ITakeble takeble)
        {
            Presenter.AddItem(takeble);
        }

        private void OnTakebleExit(ITakeble takeble)
        {
            Presenter.Cancel();
        }

        //TODO вроде работает
        private void OnGettableEnter(IGettable gettable)
        {
            Presenter.GetItem(gettable);
        }

        private void OnGettableExit(IGettable gettable)
        {
            Presenter.Cancel();
        }

        //TODO сделать булку для посетителя

        // public void AddItem(IItem item) => 
        //     Presenter.Add(item);
    }
}