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

        public PlayerWallet() : this(default(int), default(int))
        {
        }

        public PlayerWallet(PlayerWalletData data) : this(data.Coins, data.Score)
        {
        }

        private PlayerWallet(int coins, int score)
        {
            _coins = new ObservableProperty<int>(coins);
            Score = score;
        }

        public IObservableProperty<int> Coins => _coins;
        public int Score { get; private set; }

        public void Add(int quantity)
        {
            _coins.Value += quantity;
            Score += quantity;
            
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