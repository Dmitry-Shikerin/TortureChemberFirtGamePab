using System;
using Sources.DomainInterfaces.UI.AudioSourcesActivators;
using Sources.Infrastructure.Services.LoadServices.DataAccess.PlayerData;
using Sources.Utils.ObservablePropertyes;
using Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces.Generic;
using UnityEngine;

namespace Sources.Domain.Players
{
    public class PlayerWallet : IAudioSourceActivator
    {
        private ObservableProperty<int> _coins;
        private ObservableProperty<int> _score;

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
            // Score = score;
            _score = new ObservableProperty<int>(score);
        }

        public IObservableProperty<int> Coins => _coins;
        public IObservableProperty<int> Score => _score;
        // public int Score { get; private set; }

        public void Add(int quantity)
        {
            if(quantity <= 0)
                return;
            
            _coins.Value += quantity;
            _score.Value += quantity;
            
            AudioSourceActivated?.Invoke();
        }

        public void Remove(int quantity)
        {
            if (_coins.Value - quantity < 0)
                throw new InvalidOperationException("в кошельке недостаточно денег");
            
            _coins.Value -= quantity;
        }
    }
}