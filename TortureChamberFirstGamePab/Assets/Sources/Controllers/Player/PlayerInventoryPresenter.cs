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
using Sources.PresentationInterfaces.UI;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Exceptions;
using Unity.VisualScripting;
using UnityEngine;

namespace MyProject.Sources.Controllers
{
    public class PlayerInventoryPresenter : PresenterBase
    {
        private readonly IPlayerInventoryView _playerInventoryView;
        private readonly ITextUI _textUI;
        private readonly PlayerInventory _playerInventory;
        private readonly ItemViewFactory _itemViewFactory;


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

        public bool TryGet()
        {
            return _playerInventory.CanGet;
        }

        public void AddItem(IItem item)
        {
            try
            {
                if (item == null)
                    return;

                _playerInventory.Add(item);
                Debug.Log(_playerInventory.Items.Count);
                Debug.Log(item.GetType().Name);
                IItemView itemView = _itemViewFactory.Create(item);
                item.SetItemView(itemView);

                SetInventoryViewPosition(item);
            }
            catch (InventoryFullException exception)
            {
                _textUI.SetText(exception.Message);
            }
        }

        //TODO посмотреть плагины AI
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

        private void UpdateViewPosition()
        {
            for (int i = 0; i < _playerInventory.Items.Count; i++)
            {
                //TODO исправить здесь логику
                //TODO плохая логика
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
                //TODO плохая логика
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
                //TODO исправить здесь логику
                //TODO плохая логика
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