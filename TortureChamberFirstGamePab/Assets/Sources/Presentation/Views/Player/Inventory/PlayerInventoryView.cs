using System;
using System.Collections.Generic;
using System.Threading;
using MyProject.Sources.Controllers;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.DomainInterfaces.Items;
using Sources.Presentation.Views.Player.Inventory;
using Sources.Presentation.Views.Taverns;
using Sources.PresentationInterfaces.Views.Interactions.Get;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sources.Presentation.Views.Player
{
    public class PlayerInventoryView : PresentableView<PlayerInventoryPresenter>, IPlayerInventoryView
    {
        [SerializeField] private PlayerInventorySlotView _firstSlotView;
        [SerializeField] private PlayerInventorySlotView _secondSlotView;
        [SerializeField] private PlayerInventorySlotView _thirdSlotView;

        private CancellationTokenSource _cancellationTokenSource;
        private List<PlayerInventorySlotView> _playerInventorySlots;
        
        public IReadOnlyList<PlayerInventorySlotView> PlayerInventorySlots => _playerInventorySlots;

        private void Awake()
        {
            _playerInventorySlots = new List<PlayerInventorySlotView>(3);
            _playerInventorySlots.Add(_firstSlotView);
            _playerInventorySlots.Add(_secondSlotView);
            _playerInventorySlots.Add(_thirdSlotView);
        }

        private async void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<ITakeble>(out ITakeble takeble))
            {
                _cancellationTokenSource = new CancellationTokenSource();

                IItem item = await takeble.TakeItem(_cancellationTokenSource.Token);
                Presenter.AddItem(item);
            }
            
            if (other.gameObject.TryGetComponent<IGettable>(out IGettable gettable))
            {
                _cancellationTokenSource = new CancellationTokenSource();

                // IItem item = await takeble.TakeItem(_cancellationTokenSource.Token);
                // Presenter.AddItem(item);
                IItem targetItem = gettable.GetTargetItem();
                
                if(gettable.GetTargetItem() == null)
                    return;

                IItem item = Presenter.Get(targetItem);
                gettable.Add(item);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent<ITakeble>(out ITakeble takeble))
            {
                _cancellationTokenSource.Cancel();
            }
        }
    }
}