using Sources.Utils.ObservablePropertyes;
using Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces;
using Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces.Generic;
using UnityEngine;

namespace Sources.Domain.Players
{
    public class PlayerWallet
    {
        private ObservableProperty<int> _coins;

        public PlayerWallet()
        {
            _coins = new ObservableProperty<int>();
        }

        public IObservableProperty Coins => _coins;

        public void Add(int quantity)
        {
            _coins.Value += quantity;
            Debug.Log(_coins.Value);
        }

        public void Remove(int quantity)
        {
            _coins.Value -= quantity;
            Debug.Log(_coins.Value);
        }
    }
}