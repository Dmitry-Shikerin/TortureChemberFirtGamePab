using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Sources.Domain.Constants;
using Sources.Domain.Exceptions.Inventorys;
using Sources.Domain.Players;
using Sources.DomainInterfaces.Items;
using Sources.DomainInterfaces.Upgrades;
using Sources.Infrastructure.Factories.Views.Items.Common;
using Sources.Presentation.UI;
using Sources.Presentation.Views.Items;
using Sources.Presentation.Views.Items.Common;
using Sources.Presentation.Views.Player.Inventory;
using Sources.PresentationInterfaces.UI;
using Sources.PresentationInterfaces.Views;
using Sources.PresentationInterfaces.Views.Interactions.Get;
using Sources.PresentationInterfaces.Views.Interactions.Givable;
using Sources.PresentationInterfaces.Views.Players;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Sources.Controllers.Player
{
    public class PlayerInventoryPresenter : PresenterBase
    {
        private readonly PlayerInventorySlotsImages _playerInventorySlotsImages;
        private readonly IPlayerInventoryView _playerInventoryView;
        private readonly ITextUI _textUI;
        private readonly PlayerInventory _playerInventory;
        private readonly ItemViewFactory _itemViewFactory;
        private readonly IUpgradeble _upgradeble;

        private CancellationTokenSource _cancellationTokenSource;

        public PlayerInventoryPresenter
        (
            PlayerInventorySlotsImages playerInventorySlotsImages,
            IPlayerInventoryView playerInventoryView,
            ITextUI textUI,
            PlayerInventory playerInventory,
            ItemViewFactory itemViewFactory,
            IUpgradeble upgradeble
        )
        {
            _playerInventorySlotsImages = playerInventorySlotsImages ? playerInventorySlotsImages : 
                throw new ArgumentNullException(nameof(playerInventorySlotsImages));
            _playerInventoryView = playerInventoryView ??
                                   throw new ArgumentNullException(nameof(playerInventoryView));
            _textUI = textUI ?? throw new ArgumentNullException(nameof(textUI));
            _playerInventory = playerInventory ??
                               throw new ArgumentNullException(nameof(playerInventory));
            _itemViewFactory = itemViewFactory ?? throw new ArgumentNullException(nameof(itemViewFactory));
            //TODO сделать проверки
            _upgradeble = upgradeble;
        }

        public int MaxCapacity => _playerInventory.MaxCapacity;

        public override void Enable()
        {
            HideSlots();
            _upgradeble.CurrentLevelUpgrade.Changed += ShowAvailableSlot;
            //TODO сделать обновление по подписке
            //tODO ИСПРАВИТЬ
            for (int i = 0; i < _upgradeble.CurrentLevelUpgrade.GetValue; i++)
            {
                // Debug.Log(i);
                UpdateAvailableSlot(i);
            }
            Debug.Log(_upgradeble.CurrentLevelUpgrade.GetValue);
            _playerInventory.InventoryCapacity = (int)_upgradeble.CurrentAmountUpgrade;
            _playerInventory.MaxCapacity = (int)_upgradeble.MaximumUpgradeAmount;
        }

        public override void Disable()
        {
            _upgradeble.CurrentLevelUpgrade.Changed -= ShowAvailableSlot;
        }

        private void ShowAvailableSlot()
        {
            _playerInventory.IncreaseCapacity();
            
            int index = _playerInventory.InventoryCapacity - 1;
            _playerInventoryView.PlayerInventorySlots[index].BackgroundImage.ShowImage();
            _playerInventoryView.PlayerInventorySlots[index].Image.ShowImage();
        }
        
        private void UpdateAvailableSlot(int index)
        {
            // int index = _playerInventory.InventoryCapacity - 1;
            //TODO подправить
            _playerInventoryView.PlayerInventorySlots[index + 1].BackgroundImage.ShowImage();
            _playerInventoryView.PlayerInventorySlots[index + 1].Image.ShowImage();
        }

        public async void TakeItemAsync(IGivable givable)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            IItem item = await givable.GiveItemAsync(_cancellationTokenSource.Token);
            Take(item);
        }

        private void Take(IItem item)
        {
            try
            {
                if (item == null)
                    return;

                if (_playerInventory.Items.Count >= _playerInventory.InventoryCapacity)
                    RemoveItem(_playerInventoryView.PlayerInventorySlots[Constant.Inventory.FirstItemIndex]
                        .BackgroundImage, Constant.Inventory.FirstItemIndex);

                _playerInventory.Add(item);
                IItemView itemView = _itemViewFactory.Create(item);
                item.SetItemView(itemView);

                SetInventoryViewPosition(item);
            }
            catch (InventoryFullException exception)
            {
                _textUI.SetText(exception.Message);
            }
        }

        public async void GiveItemAsync(ITakeble takeble)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            IItem targetItem = takeble.TargetItem;

            if (targetItem == null)
                return;

            if (takeble.Item != null)
                return;

            if (_playerInventory.CanGet == false)
                return;

            IItem item = await GiveAsync(targetItem, _cancellationTokenSource.Token);
            takeble.TakeItem(item);
        }

        public void Cancel() =>
            _cancellationTokenSource.Cancel();

        private async UniTask<IItem> GiveAsync(IItem item, CancellationToken cancellationToken)
        {
            IImageUI backGroundImage = null;

            try
            {
                _playerInventory.LockGiveAbility();

                for (int i = 0; i < _playerInventory.Items.Count; i++)
                {
                    if (_playerInventory.Items[i].GetType() == item.GetType())
                    {
                        backGroundImage = _playerInventoryView.PlayerInventorySlots[i].BackgroundImage;
                        await backGroundImage.FillMoveTowardsAsync(
                            _playerInventoryView.FillingRate, cancellationToken);

                        return RemoveItem(backGroundImage, i);
                    }
                }

                _playerInventory.SetGiveAbility();
                return null;
            }
            catch (NullItemException)
            {
                _playerInventory.SetGiveAbility();
                return null;
            }
            catch (OperationCanceledException)
            {
                _playerInventory.SetGiveAbility();
                backGroundImage?.SetFillAmount(1);
                return null;
            }
        }

        private IItem RemoveItem(IImageUI backgroundImage, int index)
        {
            backgroundImage.SetFillAmount(Constant.FillingAmount.Maximum);
            _playerInventoryView.PlayerInventorySlots[index].Image.SetSprite(null);
            _playerInventoryView.PlayerInventorySlots[index].Image.HideImage();
            // backgroundImage.SetFillAmount(Constant.FillingAmount.Minimum);
            IItem targetItem = _playerInventory.Items[index];
            targetItem.ItemView.Destroy();
            _playerInventory.RemoveItem(targetItem);

            UpdateViewPosition();
            RemoveImage();
            _playerInventory.SetGiveAbility();

            return targetItem;
        }

        private void UpdateViewPosition()
        {
            for (int i = 0; i < _playerInventory.Items.Count; i++)
            {
                IItem item = _playerInventory.Items[i];
                Transform slotTransform = _playerInventoryView.PlayerInventorySlots[i].transform;
                ImageUI imageUI = _playerInventoryView.PlayerInventorySlots[i].Image;

                item.ItemView.SetPosition(slotTransform);
                item.ItemView.SetParent(slotTransform);
                imageUI.ShowImage();
                imageUI.SetSprite(item.Icon);
            }
        }

        private void RemoveImage()
        {
            for (int i = 0; i < _playerInventoryView.PlayerInventorySlots.Count; i++)
            {
                if (_playerInventoryView.PlayerInventorySlots[i]
                        .GetComponentInChildren<FoodView>() == null)
                {
                    _playerInventoryView.PlayerInventorySlots[i].Image.HideImage();
                    _playerInventoryView.PlayerInventorySlots[i].Image.SetSprite(null);
                }
            }
        }

        private void HideSlots()
        {
            _playerInventoryView.PlayerInventorySlots[Constant.Inventory.SecondItemIndex].
                BackgroundImage.HideImage();
            _playerInventoryView.PlayerInventorySlots[Constant.Inventory.SecondItemIndex].
                Image.HideImage();
            _playerInventoryView.PlayerInventorySlots[Constant.Inventory.ThirdItemIndex].
                BackgroundImage.HideImage();
            _playerInventoryView.PlayerInventorySlots[Constant.Inventory.ThirdItemIndex].
                Image.HideImage();
        }

        private void SetInventoryViewPosition(IItem targetItem)
        {
            for (int i = 0; i < _playerInventory.Items.Count; i++)
            {
                IItem item = _playerInventory.Items[i];
                Transform slotTransform = _playerInventoryView.PlayerInventorySlots[i].transform;
                ImageUI imageUI = _playerInventoryView.PlayerInventorySlots[i].Image;

                if (item == targetItem)
                {
                    item.ItemView.SetPosition(slotTransform);
                    item.ItemView.SetParent(slotTransform);
                    imageUI.ShowImage();
                    imageUI.SetSprite(item.Icon);
                }
            }
        }
    }
}