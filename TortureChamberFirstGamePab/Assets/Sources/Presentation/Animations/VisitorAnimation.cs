using Sources.PresentationInterfaces.Animations;
using UnityEngine;

namespace Sources.Presentation.Animations
{
    [RequireComponent(typeof(Animator))]
    public class VisitorAnimation : MonoBehaviour, IVisitorAnimation
    {
        private readonly int _isIdle = Animator.StringToHash(nameof(_isIdle));
        private readonly int _isSeatedIdle = Animator.StringToHash(nameof(_isSeatedIdle));
        private readonly int _isStandUp = Animator.StringToHash(nameof(_isStandUp));
        private readonly int _isWalk = Animator.StringToHash(nameof(_isWalk));

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayIdle()
        {
            StopPlayWalk();
            StopPlaySeatIdle();
            StopPlayStandUp();

            if (_animator.GetBool(_isIdle))
                return;

            _animator.SetBool(_isIdle, true);
        }

        public void PlayWalk()
        {
            StopPlayIdle();
            StopPlaySeatIdle();
            StopPlayStandUp();

            if (_animator.GetBool(_isWalk))
                return;

            _animator.SetBool(_isWalk, true);
        }

        public void PlaySeatIdle()
        {
            StopPlayIdle();
            StopPlayWalk();
            StopPlayStandUp();

            if (_animator.GetBool(_isSeatedIdle))
                return;

            _animator.SetBool(_isSeatedIdle, true);
        }

        public void PlayStandUp()
        {
            StopPlayIdle();
            StopPlayWalk();
            StopPlaySeatIdle();

            if (_animator.GetBool(_isStandUp))
                return;

            _animator.SetBool(_isStandUp, true);
        }

        private void StopPlayIdle()
        {
            if (_animator.GetBool(_isIdle) == false)
                return;

            _animator.SetBool(_isIdle, false);
        }

        private void StopPlayWalk()
        {
            if (_animator.GetBool(_isWalk) == false)
                return;

            _animator.SetBool(_isWalk, false);
        }

        private void StopPlaySeatIdle()
        {
            if (_animator.GetBool(_isSeatedIdle) == false)
                return;

            _animator.SetBool(_isSeatedIdle, false);
        }

        private void StopPlayStandUp()
        {
            if (_animator.GetBool(_isStandUp) == false)
                return;

            _animator.SetBool(_isStandUp, false);
        }
    }
}