using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Scripts.Domain.Constants;
using Scripts.Domain.Items.Coins;
using Scripts.PresentationInterfaces.Views.Items.Coins;
using Scripts.PresentationInterfaces.Views.Players;
using UnityEngine;

namespace Scripts.Controllers.Items.Coins
{
    public class CoinPresenter : PresenterBase
    {
        private readonly Coin _coin;
        private readonly ICoinView _coinView;
        private CancellationTokenSource _cancellationTokenSource;

        private float _currentTime;

        public CoinPresenter(
            ICoinView coinView,
            Coin coin)
        {
            _coinView = coinView ?? throw new ArgumentNullException(nameof(coinView));
            _coin = coin ?? throw new ArgumentNullException(nameof(coin));
        }

        public override void Enable()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            CollectAsync(_cancellationTokenSource.Token);
        }

        public override void Disable()
        {
            _cancellationTokenSource.Cancel();
        }

        public void SetCoinAmount(int amount)
        {
            _coin.SetCoinAmount(amount);
        }

        public void SetPlayerWalletView(IPlayerWalletView playerWalletView)
        {
            _coin.SetPlayerWalletView(playerWalletView);
        }

        public void SetCanMove()
        {
            _coin.SetCanMove();
        }

        private async void CollectAsync(CancellationToken cancellationToken)
        {
            try
            {
                await RotateCoinAsync(cancellationToken);
                await MoveToPlayerAsync(cancellationToken);
                await AddCoinsAsync(cancellationToken);

                _coinView.Destroy();
            }
            catch (OperationCanceledException)
            {
            }
        }

        private async UniTask AddCoinsAsync(CancellationToken cancellationToken)
        {
            _coin.PlayerWalletView.Add(_coin.CoinAmount);

            await UniTask.Yield(cancellationToken);
        }

        private async UniTask RotateCoinAsync(CancellationToken cancellationToken)
        {
            while (_coin.CanMove == false)
            {
                _coinView.Rotate();

                await UniTask.Yield(cancellationToken);
            }
        }

        private async UniTask MoveToPlayerAsync(CancellationToken cancellationToken)
        {
            var positionX = _coin.PlayerWalletView.Position.x;
            var positionY = _coin.PlayerWalletView.Position.y + _coinView.AnimationCurve.Evaluate(_currentTime);
            var positionZ = _coin.PlayerWalletView.Position.z;
            var delta = _coinView.MovementSpeed * Time.deltaTime;

            while (Vector3.Distance(
                       _coinView.Position,
                       _coin.PlayerWalletView.Position) > CoinConstant.TargetDistance)
            {
                _currentTime += Time.deltaTime;

                _coinView.SetPosition(Vector3.MoveTowards(
                    _coinView.Position,
                    new Vector3(positionX, positionY, positionZ),
                    delta));

                await UniTask.Yield(cancellationToken);
            }
        }
    }
}