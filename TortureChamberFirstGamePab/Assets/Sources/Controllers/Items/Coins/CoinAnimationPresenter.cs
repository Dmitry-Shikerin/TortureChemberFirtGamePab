using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MyProject.Sources.Controllers.Common;
using MyProject.Sources.Presentation.Views;
using Sources.Domain.Items.Coins;
using Sources.PresentationInterfaces.Views.Items.Coins;
using UnityEngine;

namespace Sources.Controllers.Items.Coins
{
    public class CoinAnimationPresenter : PresenterBase
    {
        private const float TargetDistance = 1f;
        
        private readonly ICoinAnimationView _coinAnimationView;
        private readonly CoinAnimation _coinAnimation;

        public CoinAnimationPresenter(ICoinAnimationView coinAnimationView, CoinAnimation coinAnimation)
        {
            _coinAnimationView = coinAnimationView ?? throw new ArgumentNullException(nameof(coinAnimationView));
            _coinAnimation = coinAnimation ?? throw new ArgumentNullException(nameof(coinAnimation));
        }
        

        private CancellationTokenSource _cancellationTokenSource;

        private float _currentTime;
        
        public override void Enable() => 
            CollectAsync();

        public override void Disable() => 
            _cancellationTokenSource.Cancel();

        public void SetCoinAmount(int amount) => 
            _coinAnimation.SetCoinAmount(amount);

        public void SetPlayerWalletView(IPlayerWalletView playerWalletView) => 
            _coinAnimation.SetPlayerWalletView(playerWalletView);

        public void SetCanMove() => 
            _coinAnimation.SetCanMove();

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
                Console.WriteLine(e);
                throw;
            }
        }

        private async UniTask AddCoinsAsync()
        {
            _coinAnimation.PlayerWalletView.Add(_coinAnimation.CoinAmount);
            await UniTask.Yield(_cancellationTokenSource.Token);
        }

        private async UniTask RotateCoinAsync()
        {
            while (_coinAnimation.CanMove == false)
            {
                _coinAnimationView.Rotate();
                await UniTask.Yield(_cancellationTokenSource.Token);
            }
        }

        private async UniTask MoveToPlayerAsync()
        {
            while (Vector3.Distance(_coinAnimationView.Position, 
                       _coinAnimation.PlayerWalletView.Position) > TargetDistance)
            {
                _currentTime += Time.deltaTime;

                _coinAnimationView.SetTransformPosition(Vector3.MoveTowards(_coinAnimationView.Position,
                    new Vector3(_coinAnimation.PlayerWalletView.Position.x,
                        _coinAnimation.PlayerWalletView.Position.y +
                        _coinAnimationView.AnimationCurve.Evaluate(_currentTime),
                        _coinAnimation.PlayerWalletView.Position.z), 
                    _coinAnimationView.MovementSpeed * Time.deltaTime));

                await UniTask.Yield(_cancellationTokenSource.Token);
            }
        }
    }
}