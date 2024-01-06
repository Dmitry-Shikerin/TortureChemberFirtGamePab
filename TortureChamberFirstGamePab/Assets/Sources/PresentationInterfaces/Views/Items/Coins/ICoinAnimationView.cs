using MyProject.Sources.Presentation.Views;
using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Items.Coins
{
    public interface ICoinAnimationView
    {
        public float OffsetYFinishPoint { get; }
        float MovementSpeed { get; }
        Vector3 Position { get; }
        public AnimationCurve AnimationCurve { get; }

        public void Destroy();
        void SetPlayerWalletView(PlayerWalletView playerWalletView);
        void Rotate();
        void SetTransformPosition(Vector3 position);

    }
}