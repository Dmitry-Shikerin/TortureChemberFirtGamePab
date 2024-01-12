using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sources.DomainInterfaces.Items;
using Sources.DomainInterfaces.Upgrades;
using Sources.PresentationInterfaces.Views;
using Sources.Utils.Exceptions;
using Sources.Utils.ObservablePropertyes;
using Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces;
using Sources.Utils.Repositoryes;
using UnityEngine;

namespace Sources.Domain.Players
{
    public class PlayerInventory
    {
        private readonly IUpgradeble _upgradeble;
        private List<IItem> _items = new List<IItem>();
        
        public PlayerInventory(IUpgradeble upgradeble)
        {
            _upgradeble = upgradeble ?? throw new ArgumentNullException(nameof(upgradeble));

        }

        public int MaxCapacity => (int)_upgradeble.MaximumUpgradeAmount;
        public int InventoryCapacity => (int)_upgradeble.CurrentAmountUpgrade;
        public IObservableProperty CurrentLevelUpgrade => _upgradeble.CurrentLevelUpgrade;
        public bool CanGet { get; private set; } = true;
        public IReadOnlyList<IItem> Items => _items;

        public void SetGiveAbility()
        {
            Debug.Log("UnLock inventory");
            CanGet = true;
        }

        public void LockGiveAbility()
        {
            Debug.Log("Lock inventory");
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
    }
}