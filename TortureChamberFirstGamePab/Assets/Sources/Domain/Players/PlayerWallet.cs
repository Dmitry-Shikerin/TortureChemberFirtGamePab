using System;
using Sources.DomainInterfaces.UI.AudioSourcesActivators;
using Sources.Infrastructure.Services.LoadServices.DataAccess;
using Sources.Infrastructure.Services.LoadServices.DataAccess.PlayerData;
using Sources.Utils.ObservablePropertyes;
using Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces.Generic;
using UnityEngine;

namespace Sources.Domain.Players
{
    public class PlayerWallet : IAudioSourceActivator
    {
        private ObservableProperty<int> _coins;

        public event Action AudioSourceActivated;

        public PlayerWallet() : this(default(int))
        {
        }

        public PlayerWallet(PlayerWalletData data) : this(data.Coins)
        {
        }

        private PlayerWallet(int coins)
        {
            _coins = new ObservableProperty<int>(coins);
        }

        public IObservableProperty<int> Coins => _coins;


        public void Add(int quantity)
        {
            _coins.Value += quantity;
            AudioSourceActivated?.Invoke();
        }

        public void Remove(int quantity)
        {
            if (_coins.Value - quantity < 0)
                throw new InvalidOperationException("в кошельке недостаточно денег");
            
            _coins.Value -= quantity;
            Debug.Log(_coins.Value);
        }
    }
}