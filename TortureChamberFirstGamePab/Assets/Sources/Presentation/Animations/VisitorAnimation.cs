using Sources.PresentationInterfaces.Animations;
using UnityEngine;

namespace Sources.Presentation.Animations
{
    [RequireComponent(typeof(Animator))]
    public class VisitorAnimation : MonoBehaviour, IVisitorAnimation
    {
        private readonly int IsIdle = Animator.StringToHash(nameof(IsIdle));
        private readonly int IsWalk = Animator.StringToHash(nameof(IsWalk));
        private readonly int IsSeatedIdle = Animator.StringToHash(nameof(IsSeatedIdle));
        private readonly int IsStandUp = Animator.StringToHash(nameof(IsStandUp));
        
        private Animator _animator;

        private void Awake() => 
            _animator = GetComponent<Animator>();

        public void PlayIdle()
        {
            StopPlayWalk();
            StopPlaySeatIdle();
            StopPlayStandUp();
            
            _animator.SetBool(IsIdle, true);
        }

        private void StopPlayIdle()
        {
            if(_animator.GetBool(IsIdle) == false)
                return;
            
            _animator.SetBool(IsIdle, false);
        }

        public void PlayWalk()
        {
            StopPlayIdle();
            StopPlaySeatIdle();
            StopPlayStandUp();
            
            _animator.SetBool(IsWalk, true);
        }

        private void StopPlayWalk()
        {
            if(_animator.GetBool(IsWalk) == false)
                return;
            
            _animator.SetBool(IsWalk, false);
        }

        public void PlaySeatIdle()
        {
            StopPlayIdle();
            StopPlayWalk();
            StopPlayStandUp();
            
            _animator.SetBool(IsSeatedIdle, true);
        }

        private void StopPlaySeatIdle()
        {
            if(_animator.GetBool(IsSeatedIdle) == false)
                return;
            
            _animator.SetBool(IsSeatedIdle, false);
        }

        public void PlayStandUp()
        {
            StopPlayIdle();
            StopPlayWalk();
            StopPlaySeatIdle();
            
            _animator.SetBool(IsStandUp, true);
        }

        private void StopPlayStandUp()
        {
            if(_animator.GetBool(IsStandUp) == false)
                return;
            
            _animator.SetBool(IsStandUp, false);
        }
    }
}