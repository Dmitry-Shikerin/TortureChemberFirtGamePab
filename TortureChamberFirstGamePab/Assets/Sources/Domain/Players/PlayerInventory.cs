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
        public int InventoryCapacity { get; private set; } = 1;
        
        private List<IItem> _items = new List<IItem>();
        
        public PlayerInventory()
        {
        }

        public int MaxCapacity { get; } = 3;
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
            if (_items.Count >= InventoryCapacity)
                throw new InventoryFullException("Инвентарь заполнен", nameof(PlayerInventory));
            
            _items.Add(item);
        }

        public void AddInventoryCapacity()
        {
            if(InventoryCapacity >= MaxCapacity)
                throw new InventoryFullException(
                    "Достигнут максимальный лимит увеличения инвентаря", nameof(PlayerInventory));
                
            InventoryCapacity++;
            Debug.Log(InventoryCapacity);
        }

        public void RemoveItem(IItem item)
        {
            if (_items.Contains(item) == false)
                throw new NullItemException("В инвентаре нет необходимого предмета", nameof(PlayerInventory));
            
            _items.Remove(item);
        }
    }
}