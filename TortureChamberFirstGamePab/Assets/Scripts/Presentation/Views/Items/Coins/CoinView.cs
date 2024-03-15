using Scripts.Controllers.Items.Coins;
using Scripts.Presentation.Views.ObjectPolls;
using Scripts.PresentationInterfaces.Views.Items.Coins;
using Scripts.PresentationInterfaces.Views.Players;
using UnityEngine;

namespace Scripts.Presentation.Views.Items.Coins
{
    public class CoinView : PresentableView<CoinPresenter>, ICoinView
    {
        [SerializeField] private float _rotationSpeed;
        [field: SerializeField] public AnimationCurve AnimationCurve { get; private set; }
        [field: SerializeField] public float MovementSpeed { get; private set; }

        public Vector3 Position => transform.position;

        public override void Destroy()
        {
            if (TryGetComponent(out PoolableObject poolableObject) == false)
            {
                Destroy(gameObject);

                return;
            }

            poolableObject.ReturnTooPool();
            Hide();
        }

        public void SetCoinAmount(int amount) =>
            Presenter.SetCoinAmount(amount);

        public void SetCanMove() =>
            Presenter.SetCanMove();

        public void SetPosition(Vector3 position) =>
            transform.position = position;

        public void SetPlayerWalletView(IPlayerWalletView playerWalletView) =>
            Presenter.SetPlayerWalletView(playerWalletView);

        public void Rotate() =>
            transform.Rotate(0, _rotationSpeed, 0);
    }
}