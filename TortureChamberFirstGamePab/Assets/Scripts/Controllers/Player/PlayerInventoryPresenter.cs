using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Scripts.Domain.Constants;
using Scripts.Domain.Exceptions.Inventorys;
using Scripts.Domain.Players;
using Scripts.DomainInterfaces.Items;
using Scripts.DomainInterfaces.Upgrades;
using Scripts.Infrastructure.Factories.Views.Items.Common;
using Scripts.Presentation.UI;
using Scripts.Presentation.Views.Items.Common;
using Scripts.Presentation.Views.Player.Inventory;
using Scripts.PresentationInterfaces.UI;
using Scripts.PresentationInterfaces.Views.Interactions.Get;
using Scripts.PresentationInterfaces.Views.Interactions.Givable;
using Scripts.PresentationInterfaces.Views.Players;
using UnityEngine;

namespace Scripts.Controllers.Player
{
    public class PlayerInventoryPresenter : PresenterBase
    {
        private readonly ItemViewFactory _itemViewFactory;
        private readonly PlayerInventory _playerInventory;
        private readonly IPlayerInventoryView _playerInventoryView;
        private readonly ITextUI _textUI;
        private readonly IUpgradable _upgradable;

        private CancellationTokenSource _cancellationTokenSource;

        public PlayerInventoryPresenter(
            IPlayerInventoryView playerInventoryView,
            ITextUI textUI,
            PlayerInventory playerInventory,
            ItemViewFactory itemViewFactory,
            IUpgradable upgradable)
        {
            _playerInventoryView = playerInventoryView ??
                                   throw new ArgumentNullException(nameof(playerInventoryView));
            _textUI = textUI ?? throw new ArgumentNullException(nameof(textUI));
            _playerInventory = playerInventory ??
                               throw new ArgumentNullException(nameof(playerInventory));
            _itemViewFactory = itemViewFactory ?? throw new ArgumentNullException(nameof(itemViewFactory));
            _upgradable = upgradable ?? throw new ArgumentNullException(nameof(upgradable));
        }

        public override void Enable()
        {
            HideSlots();

            for (var i = 0; i < _upgradable.CurrentLevelUpgrade.GetValue + 1; i++)
                UpdateAvailableSlot(i);

            _upgradable.CurrentLevelUpgrade.Changed += ShowAvailableSlot;

            _playerInventory.InventoryCapacity = (int)_upgradable.CurrentAmountUpgrade;
        }

        public override void Disable() =>
            _upgradable.CurrentLevelUpgrade.Changed -= ShowAvailableSlot;

        public async void TakeItemAsync(IGivable givable)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            var item = await givable.GiveItemAsync(_cancellationTokenSource.Token);
            Take(item);
        }

        public async void GiveItemAsync(ITakeble takeble)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            var targetItem = takeble.TargetItem;

            if (targetItem == null)
                return;

            if (takeble.Item != null)
                return;

            if (_playerInventory.CanGet == false)
                return;

            var item = await GiveAsync(targetItem, takeble, _cancellationTokenSource.Token);
            takeble.TakeItem(item);
        }

        public void Cancel() =>
            _cancellationTokenSource.Cancel();

        private void Take(IItem item)
        {
            try
            {
                if (item == null)
                    return;

                if (_playerInventory.Items.Count >= _playerInventory.InventoryCapacity)
                {
                    RemoveItem(
                        _playerInventoryView.PlayerInventorySlots[InventoryConstant.FirstItemIndex]
                            .BackgroundImage, InventoryConstant.FirstItemIndex);
                }

                _playerInventory.Add(item);
                var itemView = _itemViewFactory.Create(item);
                item.SetItemView(itemView);

                SetInventoryViewPosition(item);
            }
            catch (InventoryFullException exception)
            {
                _textUI.SetText(exception.Message);
            }
        }

        private async UniTask<IItem> GiveAsync(IItem item, ITakeble takeble, CancellationToken cancellationToken)
        {
            IImageUI backGroundImage = null;

            try
            {
                _playerInventory.LockGiveAbility();
                _playerInventory.StartGiveItem();

                for (var i = 0; i < _playerInventory.Items.Count; i++)
                {
                    if (_playerInventory.Items[i].GetType() == item.GetType())
                    {
                        backGroundImage = _playerInventoryView.PlayerInventorySlots[i].BackgroundImage;

                        await backGroundImage.FillMoveTowardsAsync(
                            _playerInventoryView.FillingRate,
                            cancellationToken,
                            () =>
                            {
                                if (takeble.TargetItem == null)
                                    Cancel();
                            });

                        _playerInventory.StopGiveItem();

                        return RemoveItem(backGroundImage, i);
                    }
                }

                _playerInventory.StopGiveItem();
                _playerInventory.SetGiveAbility();

                return null;
            }
            catch (NullItemException)
            {
                _playerInventory.StopGiveItem();
                _playerInventory.SetGiveAbility();

                return null;
            }
            catch (OperationCanceledException)
            {
                _playerInventory.StopGiveItem();
                _playerInventory.SetGiveAbility();
                backGroundImage?.SetFillAmount(1);

                return null;
            }
        }

        private IItem RemoveItem(IImageUI backgroundImage, int index)
        {
            backgroundImage.SetFillAmount(FillingAmountConstant.Maximum);
            _playerInventoryView.PlayerInventorySlots[index].Image.HideImage();
            IItem targetItem = _playerInventory.Items[index];
            targetItem.ItemView.Destroy();
            _playerInventory.RemoveItem(targetItem);

            UpdateViewPosition();
            RemoveImage();
            _playerInventory.SetGiveAbility();

            return targetItem;
        }

        private void ShowAvailableSlot()
        {
            _playerInventory.IncreaseCapacity();

            for (var i = 0; i < _playerInventory.InventoryCapacity; i++)
                UpdateAvailableSlot(i);
        }

        private void UpdateAvailableSlot(int index) =>
            _playerInventoryView.PlayerInventorySlots[index].BackgroundImage.ShowImage();

        private void UpdateViewPosition()
        {
            for (var i = 0; i < _playerInventory.Items.Count; i++)
            {
                IItem item = _playerInventory.Items[i];
                Transform slotTransform = _playerInventoryView.PlayerInventorySlots[i].transform;
                ImageUI imageUI = _playerInventoryView.PlayerInventorySlots[i].Image;

                item.ItemView.SetTransformPosition(slotTransform);
                item.ItemView.SetParent(slotTransform);
                imageUI.ShowImage();
                imageUI.SetSprite(item.Icon);
            }
        }

        private void RemoveImage()
        {
            foreach (PlayerInventorySlotView slot in _playerInventoryView.PlayerInventorySlots)
                if (slot.GetComponentInChildren<FoodView>() == null)
                    slot.Image.HideImage();
        }

        private void HideSlots()
        {
            var indexFirstSlot = 0;

            for (var i = 0; i < _playerInventoryView.PlayerInventorySlots.Count; i++)
            {
                PlayerInventorySlotView slot = _playerInventoryView.PlayerInventorySlots[i];

                if (i == indexFirstSlot)
                {
                    slot.Image.HideImage();

                    continue;
                }

                slot.BackgroundImage.HideImage();
                slot.Image.HideImage();
            }
        }

        private void SetInventoryViewPosition(IItem targetItem)
        {
            for (var i = 0; i < _playerInventory.Items.Count; i++)
            {
                IItem item = _playerInventory.Items[i];
                Transform slotTransform = _playerInventoryView.PlayerInventorySlots[i].transform;
                ImageUI imageUI = _playerInventoryView.PlayerInventorySlots[i].Image;

                if (item == targetItem)
                {
                    item.ItemView.SetTransformPosition(slotTransform);
                    item.ItemView.SetParent(slotTransform);
                    imageUI.ShowImage();
                    imageUI.SetSprite(item.Icon);
                }
            }
        }
    }
}