using System;
using System.Collections.Generic;
using Scripts.Domain.Exceptions.Inventorys;
using Scripts.DomainInterfaces.Items;
using Scripts.DomainInterfaces.UI.AudioSourcesActivators;

namespace Scripts.Domain.Players
{
    public class PlayerInventory : IFourthAudioSourceActivator
    {
        private readonly List<IItem> _items = new ();

        public event Action FirstAudioSourceActivated;
        public event Action SecondAudioSourceActivated;
        public event Action ThirdAudioSourceActivated;
        public event Action FourthAudioSourceActivated;

        public int InventoryCapacity { get; set; }
        public bool CanGet { get; private set; } = true;
        public IReadOnlyList<IItem> Items => _items;

        public bool IsActive { get; private set; }

        public void SetGiveAbility() =>
            CanGet = true;

        public void LockGiveAbility() =>
            CanGet = false;

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

        public void IncreaseCapacity() =>
            InventoryCapacity++;
    }
}