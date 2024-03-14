using System;
using System.Collections.Generic;
using Sources.Domain.Exceptions.Inventorys;
using Sources.DomainInterfaces.Items;
using Sources.DomainInterfaces.UI.AudioSourcesActivators;

namespace Sources.Domain.Players
{
    public class PlayerInventory : IFourthAudioSourceActivator
    {
        private readonly List<IItem> _items = new ();
        public int MaxCapacity { get; set; }
        public int InventoryCapacity { get; set; }
        public bool CanGet { get; private set; } = true;
        public IReadOnlyList<IItem> Items => _items;

        public event Action FirstAudioSourceActivated;
        public event Action SecondAudioSourceActivated;
        public event Action ThirdAudioSourceActivated;
        public event Action FourthAudioSourceActivated;

        public bool IsActive { get; private set; }

        public void SetGiveAbility()
        {
            CanGet = true;
        }

        public void LockGiveAbility()
        {
            CanGet = false;
        }

        public void StartGiveItem()
        {
            IsActive = true;
            ThirdAudioSourceActivated?.Invoke();
        }

        public void StopGiveItem()
        {
            IsActive = false;
            FourthAudioSourceActivated?.Invoke();
        }

        public void Add(IItem item)
        {
            if (_items.Count >= InventoryCapacity)
                throw new InventoryFullException("Инвентарь заполнен", nameof(PlayerInventory));

            _items.Add(item);

            FirstAudioSourceActivated?.Invoke();
        }

        public void RemoveItem(IItem item)
        {
            if (_items.Contains(item) == false)
                throw new NullItemException("В инвентаре нет необходимого предмета", nameof(PlayerInventory));

            _items.Remove(item);

            SecondAudioSourceActivated?.Invoke();
        }

        public void IncreaseCapacity()
        {
            InventoryCapacity++;
        }
    }
}