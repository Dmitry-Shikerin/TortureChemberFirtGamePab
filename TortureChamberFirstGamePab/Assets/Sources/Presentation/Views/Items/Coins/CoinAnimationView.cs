using MyProject.Sources.Presentation.Views;
using Sources.Controllers.Items.Coins;
using Sources.Presentation.Views.ObjectPolls;
using Sources.PresentationInterfaces.Views.Items.Coins;
using UnityEngine;

namespace Sources.Presentation.Views.Items.Coins
{
    public class CoinAnimationView : PresentableView<CoinAnimationPresenter>, ICoinAnimationView
    {
        [SerializeField] private float _rotationSpeed;
        
        [field: SerializeField] public AnimationCurve AnimationCurve { get; private set; }
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float OffsetYFinishPoint { get; private set; }

        private float _totalTime;
        
        public Vector3 Position => transform.position;
        
        private void Start()
        {
            _totalTime = AnimationCurve.keys[AnimationCurve.keys.Length - 1].time;

        }

        public void Destroy()
        {
            if (TryGetComponent(out PoolableObject poolableObject) == false)
            {
                Destroy(gameObject);

                return;
            }

            poolableObject.ReturnTooPool();
            Hide();
        }

        public void SetCoinAmount(int amount)
        {
            Presenter.SetCoinAmount(amount);
        }

        public void SetCanMove(bool canMove)
        {
            Presenter.SetCanMove(canMove);
        }
        
        public void SetTransformPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetPlayerWalletView(IPlayerWalletView playerWalletView)
        {
            Presenter.SetPlayerWalletView(playerWalletView);
        }

        public void Rotate()
        {
            transform.Rotate(0, _rotationSpeed, 0);
        }
    }
}