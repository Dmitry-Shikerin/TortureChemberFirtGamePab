using System.Collections.Generic;
using Scripts.Controllers.Player;
using Scripts.Presentation.Triggers.Inventories;
using Scripts.PresentationInterfaces.Views.Interactions.Get;
using Scripts.PresentationInterfaces.Views.Interactions.Givable;
using Scripts.PresentationInterfaces.Views.Players;
using UnityEngine;

namespace Scripts.Presentation.Views.Player.Inventory
{
    public class PlayerInventoryView : PresentableView<PlayerInventoryPresenter>, IPlayerInventoryView
    {
        private const int MaxInventoryCapacity = 3;

        private GiveTrigger _giveTrigger;
        private List<PlayerInventorySlotView> _playerInventorySlots;
        private TakeTrigger _takeTrigger;

        [field: SerializeField] public PlayerInventorySlotView FirstSlotView { get; private set; }
        [field: SerializeField] public PlayerInventorySlotView SecondSlotView { get; private set; }
        [field: SerializeField] public PlayerInventorySlotView ThirdSlotView { get; private set; }
        [field: SerializeField] public float FillingRate { get; private set; } = 0.2f;

        public IReadOnlyList<PlayerInventorySlotView> PlayerInventorySlots => _playerInventorySlots;

        private void Awake()
        {
            _playerInventorySlots = new List<PlayerInventorySlotView>(MaxInventoryCapacity);
            _playerInventorySlots.Add(FirstSlotView);
            _playerInventorySlots.Add(SecondSlotView);
            _playerInventorySlots.Add(ThirdSlotView);

            _takeTrigger = GetComponentInChildren<TakeTrigger>();
            _giveTrigger = GetComponentInChildren<GiveTrigger>();
        }

        protected override void OnAfterEnable()
        {
            _takeTrigger.Entered += OnTakebleEnter;
            _takeTrigger.Exited += OnTakebleExit;

            _giveTrigger.Entered += OnGivableEnter;
            _giveTrigger.Exited += OnGivebleExit;
        }

        protected override void OnAfterDisable()
        {
            _takeTrigger.Entered -= OnTakebleEnter;
            _takeTrigger.Exited -= OnTakebleExit;

            _giveTrigger.Entered -= OnGivableEnter;
            _giveTrigger.Entered -= OnGivebleExit;
        }

        private void OnGivableEnter(IGivable givable) =>
            Presenter.TakeItemAsync(givable);

        private void OnGivebleExit(IGivable givable) =>
            Presenter.Cancel();

        private void OnTakebleEnter(ITakeble takeble) =>
            Presenter.GiveItemAsync(takeble);

        private void OnTakebleExit(ITakeble takeble) =>
            Presenter.Cancel();
    }
}