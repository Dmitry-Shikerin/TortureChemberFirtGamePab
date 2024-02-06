using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MyProject.Sources.Presentation.Views;
using Sources.Domain.Items.Coins;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.PresentationInterfaces.Views.Items.Coins;
using UnityEngine;

namespace Sources.Controllers.Items.Coins
{
    public class CoinPresenter : PresenterBase
    {
        private const float TargetDistance = 1f;

        private readonly ICoinAnimationView _coinAnimationView;
        private readonly Coin _coin;
        private readonly IPauseService _pauseService;

        public CoinPresenter
        (
            ICoinAnimationView coinAnimationView,
            Coin coin,
            IPauseService pauseService
        )
        {
            _coinAnimationView = coinAnimationView ?? throw new ArgumentNullException(nameof(coinAnimationView));
            _coin = coin ?? throw new ArgumentNullException(nameof(coin));
            _pauseService = pauseService ?? throw new ArgumentNullException(nameof(pauseService));
        }


        private CancellationTokenSource _cancellationTokenSource;

        private float _currentTime;

        public override void Enable() =>
            CollectAsync();

        public override void Disable() =>
            _cancellationTokenSource.Cancel();

        public void SetCoinAmount(int amount) =>
            _coin.SetCoinAmount(amount);

        public void SetPlayerWalletView(IPlayerWalletView playerWalletView) =>
            _coin.SetPlayerWalletView(playerWalletView);

        public void SetCanMove() =>
            _coin.SetCanMove();

        private async void CollectAsync()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                await RotateCoinAsync();
                await MoveToPlayerAsync();
                await AddCoinsAsync();
                _coinAnimationView.Destroy();
            }
            catch (OperationCanceledException e)
            {
                Debug.Log(e.Message);
            }
        }

        private async UniTask AddCoinsAsync()
        {
            _coin.PlayerWalletView.Add(_coin.CoinAmount);
            await UniTask.Yield(_cancellationTokenSource.Token);
        }

        private async UniTask RotateCoinAsync()
        {
            while (_coin.CanMove == false)
            {
                _coinAnimationView.Rotate();
                await UniTask.Yield(_cancellationTokenSource.Token);
                await _pauseService.Yield(_cancellationTokenSource.Token);
            }
        }

        private async UniTask MoveToPlayerAsync()
        {
            while (Vector3.Distance(_coinAnimationView.Position,
                       _coin.PlayerWalletView.Position) > TargetDistance)
            {
                _currentTime += Time.deltaTime;

                _coinAnimationView.SetTransformPosition(Vector3.MoveTowards(_coinAnimationView.Position,
                    new Vector3(_coin.PlayerWalletView.Position.x,
                        _coin.PlayerWalletView.Position.y +
                        _coinAnimationView.AnimationCurve.Evaluate(_currentTime),
                        _coin.PlayerWalletView.Position.z),
                    _coinAnimationView.MovementSpeed * Time.deltaTime));

                await UniTask.Yield(_cancellationTokenSource.Token);
                await _pauseService.Yield(_cancellationTokenSource.Token);
            }
        }
    }
}