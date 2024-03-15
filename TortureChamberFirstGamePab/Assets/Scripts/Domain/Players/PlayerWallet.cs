using System;
using Scripts.Domain.DataAccess.PlayerData;
using Scripts.DomainInterfaces.UI.AudioSourcesActivators;
using Scripts.Utils.ObservableProperties;
using Scripts.Utils.ObservableProperties.ObservablePropertyInterfaces.Generic;

namespace Scripts.Domain.Players
{
    public class PlayerWallet : IAudioSourceActivator
    {
        private readonly ObservableProperty<int> _coins;
        private readonly ObservableProperty<int> _score;

        public PlayerWallet()
            : this(default, default)
        {
        }

        public PlayerWallet(WalletData data)
            : this(data.Coins, data.Score)
        {
        }

        private PlayerWallet(int coins, int score)
        {
            _coins = new ObservableProperty<int>(coins);
            _score = new ObservableProperty<int>(score);
        }

        public event Action AudioSourceActivated;

        public IObservableProperty<int> Coins => _coins;
        public IObservableProperty<int> Score => _score;

        public void Add(int quantity)
        {
            if (quantity <= 0)
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