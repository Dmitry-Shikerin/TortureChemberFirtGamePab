using System;
using JetBrains.Annotations;
using MyProject.Sources.Controllers.Common;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Domain.Players;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Factories.Views.Items;
using Sources.Infrastructure.Factories.Views.Items.Common;
using Sources.Infrastructure.Factorys.Domains.Items;
using Sources.PresentationInterfaces.UI;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Exceptions;
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

        public void AddItem(IItem item)
        {
            try
            {
                //TODO как мне сконтачить мой ийтем и айтем вью
                // if (item == null)
                //     return;

                _playerInventory.Add(item);

                Debug.Log(_playerInventory.Items.Count);
                IItemView itemView = _itemViewFactory.Create(item);
                itemView.SetParent(_playerInventoryView.FirstSlot);
            }
            catch (InventoryFullException exception)
            {
                _textUI.SetText(exception.Message);
            }
        }

        public void RemoveItem()
        {
            try
            {
            }
            catch (Exception e)
            {
            }
        }

        public void SetInventoryViewPosition()
        {
            // if(_playerInventory.Items.Count == 1)
        }

        public void AddItemView()
        {
        }
    }
}