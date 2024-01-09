using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MyProject.Sources.Controllers.Common;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Domain.Players;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factories.Views.Items.Common;
using Sources.Presentation.UI;
using Sources.Presentation.Views.Items;
using Sources.Presentation.Views.Taverns;
using Sources.PresentationInterfaces.UI;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Exceptions;
using UnityEngine;

namespace Sources.Controllers.Player
{
    public class PlayerInventoryPresenter : PresenterBase
    {
        private readonly IPlayerInventoryView _playerInventoryView;
        private readonly ITextUI _textUI;
        private readonly PlayerInventory _playerInventory;
        private readonly ItemViewFactory _itemViewFactory;

        private CancellationTokenSource _cancellationTokenSource;

        public PlayerInventoryPresenter(IPlayerInventoryView playerInventoryView,
            ITextUI textUI, PlayerInventory playerInventory,
            ItemViewFactory itemViewFactory)
        {
            _playerInventoryView = playerInventoryView ??
                                   throw new ArgumentNullException(nameof(playerInventoryView));
            _textUI = textUI ?? throw new ArgumentNullException(nameof(textUI));
            _playerInventory = playerInventory ??
                               throw new ArgumentNullException(nameof(playerInventory));
            _itemViewFactory = itemViewFactory ?? throw new ArgumentNullException(nameof(itemViewFactory));
        }

        public int MaxCapacity => _playerInventory.MaxCapacity;

        public bool TryGet()
        {
            return _playerInventory.CanGet;
        }

        public async void AddItem(IGiveble giveble)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            IItem item = await giveble.GiveItemAsync(_cancellationTokenSource.Token);
            Add(item);
        }

        private void Add(IItem item)
        {
            try
            {
                if (item == null)
                    return;

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

        public async void GetItem(PresentationInterfaces.Views.Interactions.Get.ITakeble takeble)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            
            IItem targetItem = takeble.TargetItem;
            
            if (targetItem == null)
                return;
            
            if (takeble.Item != null)
                return;
            
            if(_playerInventory.CanGet == false)
                return;
            
            IItem item = await Get(targetItem, _cancellationTokenSource.Token);
            takeble.TakeItem(item);
        }
        
        public async UniTask<IItem> Get(IItem item, CancellationToken cancellationToken)
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
                        await backGroundImage.FillMoveTowardsAsync(_playerInventoryView.FillingRate, cancellationToken);
                        _playerInventoryView.PlayerInventorySlots[i].Image.SetSprite(null);
                        _playerInventoryView.PlayerInventorySlots[i].Image.HideImage();
                        backGroundImage.SetFillAmount(1);
                        IItem targetItem = _playerInventory.Items[i];
                        targetItem.ItemView.Destroy();
                        _playerInventory.RemoveItem(targetItem);

                        UpdateViewPosition();
                        RemoveImage();
                        _playerInventory.SetGiveAbility();
                        
                        return targetItem;
                    }
                }

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

        public void Cancel()
        {
            _cancellationTokenSource.Cancel();
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