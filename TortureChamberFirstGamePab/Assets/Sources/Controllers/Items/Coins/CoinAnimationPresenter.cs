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
        private readonly ICoinAnimationView _coinAnimationView;
        private readonly CoinAnimation _coinAnimation;

        public CoinAnimationPresenter(ICoinAnimationView coinAnimationView, CoinAnimation coinAnimation)
        {
            _coinAnimationView = coinAnimationView ?? throw new ArgumentNullException(nameof(coinAnimationView));
            _coinAnimation = coinAnimation ?? throw new ArgumentNullException(nameof(coinAnimation));
        }
        

        private CancellationTokenSource _cancellationTokenSource;

        private float _currentTime;
        private float _totalTime;
        
        public override void Enable()
        {
            _totalTime = _coinAnimationView.AnimationCurve.
                keys[_coinAnimationView.AnimationCurve.keys.Length - 1].time;
            
            Collect();
        }
        public override void Disable()
        {
            
        }

        public void SetPlayerWalletView(PlayerWalletView playerWalletView)
        {
            _coinAnimation.SetPlayerWalletView(playerWalletView);
        }

        //TODO попробовать сделать через UniRx
        private async void Collect()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                await RotateCoinAsync();
                await MoveToPlayer();
                await AddCoins();
                _coinAnimationView.Destroy();
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        //TODO заменить магическое число
        private async UniTask AddCoins()
        {
            _coinAnimation.PlayerWalletView.Add(10);
            await UniTask.Yield();
        }

        private async UniTask RotateCoinAsync()
        {
            while (_coinAnimation.CanMove == false)
            {
                _coinAnimationView.Rotate();
                await UniTask.Yield();
            }
        }

        //TODO заменить магическое числло
        private async UniTask MoveToPlayer()
        {
            while (Vector3.Distance(_coinAnimationView.Position, new Vector3(
                       _coinAnimation.PlayerWalletView.transform.position.x,
                       _coinAnimation.PlayerWalletView.transform.position.y + _coinAnimationView.OffsetYFinishPoint,
                       _coinAnimation.PlayerWalletView.transform.position.z)) > 0.04f)
            {
                //TODO возможно переместить в модель
                _currentTime += Time.deltaTime;

                _coinAnimationView.SetTransformPosition(Vector3.MoveTowards(_coinAnimationView.Position,
                    new Vector3(_coinAnimation.PlayerWalletView.transform.position.x,
                        _coinAnimation.PlayerWalletView.transform.position.y +
                        _coinAnimationView.AnimationCurve.Evaluate(_currentTime),
                        _coinAnimation.PlayerWalletView.transform.position.z), 
                    _coinAnimationView.MovementSpeed * Time.deltaTime));

                await UniTask.Yield();
            }
        }

        public void SetCanMove(bool canMove)
        {
            _coinAnimation.SetCanMove(canMove);
        }
    }
}