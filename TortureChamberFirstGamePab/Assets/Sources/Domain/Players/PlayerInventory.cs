﻿using System.Collections.Generic;
using Sources.Domain.Exceptions.Inventorys;
using Sources.DomainInterfaces.Items;
using Sources.Infrastructure.Services.LoadServices.DataAccess;
using Sources.Infrastructure.Services.LoadServices.DataAccess.PlayerData;

namespace Sources.Domain.Players
{
    public class PlayerInventory
    {
        private List<IItem> _items = new List<IItem>();

        public PlayerInventory(PlayerInventoryData playerInventoryData)
        {
        }
        public PlayerInventory()
        {
        }
        
        public int MaxCapacity { get; set; }
        public int InventoryCapacity { get; set; }
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
        
        public void RemoveItem(IItem item)
        {
            if (_items.Contains(item) == false)
                throw new NullItemException("В инвентаре нет необходимого предмета", nameof(PlayerInventory));
            
            _items.Remove(item);
        }

        public void IncreaseCapacity()
        {
            InventoryCapacity++;
        }
    }
}