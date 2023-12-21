using System;
using System.Collections.Generic;
using Sources.DomainInterfaces.Items;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Exceptions;
using Sources.Utils.Repositoryes;

namespace Sources.Domain.Players
{
    public class PlayerInventory
    {
        public const int MaxCapacity = 3;
        
        private List<IItem> _items = new List<IItem>(MaxCapacity);
        
        public PlayerInventory()
        {
        }

        public IReadOnlyList<IItem> Items => _items;

        public void Add(IItem item)
        {
            if (_items.Count == MaxCapacity)
                throw new InventoryFullException("Инвентарь заполнен", nameof(PlayerInventory));
            
            _items.Add(item);
        }

        public void Remove(IItem item)
        {
            
        }
    }
}