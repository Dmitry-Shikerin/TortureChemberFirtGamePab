using Scripts.PresentationInterfaces.Views.Players;
using UnityEngine;

namespace Scripts.PresentationInterfaces.Views.Items.Coins
{
    public interface ICoinView : IView
    {
        float MovementSpeed { get; }
        Vector3 Position { get; }
        public AnimationCurve AnimationCurve { get; }

        void SetCoinAmount(int amount);
        void SetCanMove();
        void SetPlayerWalletView(IPlayerWalletView playerWalletView);
        void Rotate();
        void SetPosition(Vector3 position);
    }
}