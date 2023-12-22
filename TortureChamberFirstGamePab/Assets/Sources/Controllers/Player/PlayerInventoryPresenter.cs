using System;
using MyProject.Sources.Controllers.Common;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Domain.Items;
using Sources.Domain.Players;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factories.Views.Items.Common;
using Sources.PresentationInterfaces.UI;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Exceptions;
using UnityEngine;

namespace MyProject.Sources.Controllers
{
    public class  PlayerInventoryPresenter : PresenterBase
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

        public void AddItem(IItem item)
        {
            try
            {
                //TODO как мне сконтачить мой ийтем и айтем вью
                if (item == null)
                    return;

                _playerInventory.Add(item);
                Debug.Log(_playerInventory.Items.Count);
                Debug.Log(item.GetType().Name);
                IItemView itemView = _itemViewFactory.Create(item);
                item.SetItemView(itemView);

                SetInventoryViewPosition();
            }
            catch (InventoryFullException exception)
            {
                _textUI.SetText(exception.Message);
            }
        }

        public IItem Get(IItem item)
        {
            try
            {
                IItem targetItem = null;
                
                for (int i = 0; i < _playerInventory.Items.Count; i++)
                {
                    if (_playerInventory.Items[i].GetType() == item.GetType())
                    {
                        targetItem = _playerInventory.Items[i];
                        targetItem.ItemView.Destroy();
                        _playerInventoryView.PlayerInventorySlots[i].Image.SetSprite(null);
                        _playerInventory.RemoveItem(targetItem);
                    }
                }
                return targetItem;
            }
            catch (NullItemException exception)
            {
                return null;
            }
        }

        private void SetInventoryViewPosition()
        {
            for (int i = 0; i < _playerInventory.Items.Count; i++)
            {
                IItem item = _playerInventory.Items[i];
                    
                if (_playerInventory.Items[i] == item)
                {
                    item.ItemView.SetPosition(_playerInventoryView.PlayerInventorySlots[i].transform);
                    item.ItemView.SetParent(_playerInventoryView.PlayerInventorySlots[i].transform);
                    _playerInventoryView.PlayerInventorySlots[i].Image.SetSprite(item.Icon);
                }
            }
        }

        public void AddItemView()
        {
        }
    }
}