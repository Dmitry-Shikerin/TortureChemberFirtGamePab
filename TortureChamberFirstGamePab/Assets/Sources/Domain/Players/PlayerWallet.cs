using System;
using Sources.DomainInterfaces.UI.AudioSourcesActivators;
using Sources.Infrastructure.Services.LoadServices.DataAccess.PlayerData;
using Sources.Utils.ObservablePropertyes;
using Sources.Utils.ObservablePropertyes.ObservablePropertyInterfaces.Generic;

namespace Sources.Domain.Players
{
    public class PlayerWallet : IAudioSourceActivator
    {
        private readonly ObservableProperty<int> _coins;
        private readonly ObservableProperty<int> _score;

        public PlayerWallet()
             : this(default, default)
        {
        }

        public PlayerWallet(PlayerWalletData data) : this(data.Coins, data.Score)
        {
        }

        private PlayerWallet(int coins, int score)
        {
            _coins = new ObservableProperty<int>(coins);
            _score = new ObservableProperty<int>(score);
        }

        public IObservableProperty<int> Coins => _coins;
        public IObservableProperty<int> Score => _score;

        public event Action AudioSourceActivated;

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