using System;
using System.Collections.Generic;
using Sources.DomainInterfaces.Items;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Exceptions;
using Sources.Utils.Repositoryes;
using UnityEngine;

namespace Sources.Domain.Players
{
    public class PlayerInventory
    {
        public const int MaxCapacity = 3;

        private int _inventoryCapacity = 1;
        
        private List<IItem> _items = new List<IItem>();
        
        public PlayerInventory()
        {
        }

        public bool CanGet { get; private set; } = true;

        public IReadOnlyList<IItem> Items => _items;

        public void SetGiveAbility()
        {
            CanGet = true;
        }

        public void LockGiveAbility()
        {
            CanGet = false;
        }
        
        public void Add(IItem item)
        {
            if (_items.Count >= _inventoryCapacity)
                throw new InventoryFullException("Инвентарь заполнен", nameof(PlayerInventory));
            
            _items.Add(item);
        }

        public void AddInventoryCapacity()
        {
            if(_inventoryCapacity >= MaxCapacity)
                throw new InventoryFullException(
                    "Достигнут максимальный лимит увеличения инвентаря", nameof(PlayerInventory));
                
            _inventoryCapacity++;
            Debug.Log(_inventoryCapacity);
        }

        public void RemoveItem(IItem item)
        {
            if (_items.Contains(item) == false)
                throw new NullItemException("В инвентаре нет необходимого предмета", nameof(PlayerInventory));
            
            _items.Remove(item);
        }
    }
}