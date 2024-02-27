using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MyProject.Sources.Presentation.Views;
using Sources.Domain.Constants;
using Sources.Domain.Items.Coins;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.PresentationInterfaces.Views.Items.Coins;
using UnityEngine;

namespace Sources.Controllers.Items.Coins
{
    public class CoinPresenter : PresenterBase
    {
        private readonly ICoinView _coinView;
        private readonly Coin _coin;
        private readonly IPauseService _pauseService;

        public CoinPresenter
        (
            ICoinView coinView,
            Coin coin,
            IPauseService pauseService
        )
        {
            _coinView = coinView ?? throw new ArgumentNullException(nameof(coinView));
            _coin = coin ?? throw new ArgumentNullException(nameof(coin));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }


        private CancellationTokenSource _cancellationTokenSource;

        private float _currentTime;

        public override void Enable()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            
            CollectAsync(_cancellationTokenSource.Token);
        }

        public override void Disable() =>
            _cancellationTokenSource.Cancel();

        public void SetCoinAmount(int amount) =>
            _coin.SetCoinAmount(amount);

        public void SetPlayerWalletView(IPlayerWalletView playerWalletView) =>
            _coin.SetPlayerWalletView(playerWalletView);

        public void SetCanMove() =>
            _coin.SetCanMove();

        private async void CollectAsync(CancellationToken cancellationToken)
        {
            try
            {
                await RotateCoinAsync(cancellationToken);
                await MoveToPlayerAsync(cancellationToken);
                await AddCoinsAsync(cancellationToken);
                
                _coinView.Destroy();
            }
            catch (OperationCanceledException e)
            {
            }
        }

        private async UniTask AddCoinsAsync(CancellationToken cancellationToken)
        {
            _coin.PlayerWalletView.Add(_coin.CoinAmount);
            
            await UniTask.Yield(cancellationToken);
            // await _pauseService.Yield(cancellationToken);
        }

        private async UniTask RotateCoinAsync(CancellationToken cancellationToken)
        {
            while (_coin.CanMove == false)
            {
                _coinView.Rotate();
                
                await UniTask.Yield(cancellationToken);
                // await _pauseService.Yield(cancellationToken);
            }
        }

        private async UniTask MoveToPlayerAsync(CancellationToken cancellationToken)
        {
            while (Vector3.Distance(_coinView.Position,
                       _coin.PlayerWalletView.Position) > Constant.Coin.TargetDistance)
            {
                _currentTime += Time.deltaTime;

                _coinView.SetPosition(Vector3.MoveTowards(_coinView.Position,
                    new Vector3(_coin.PlayerWalletView.Position.x,
                        _coin.PlayerWalletView.Position.y +
                        _coinView.AnimationCurve.Evaluate(_currentTime),
                        _coin.PlayerWalletView.Position.z),
                    _coinView.MovementSpeed * Time.deltaTime));

                await UniTask.Yield(cancellationToken);
                // await _pauseService.Yield(cancellationToken);
            }
        }
    }
}