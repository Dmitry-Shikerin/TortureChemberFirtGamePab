using System;
using Sources.PresentationInterfaces.Animations;
using UnityEngine;

namespace Sources.Presentation.Animations
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimation : MonoBehaviour, IPlayerAnimation
    {
        private readonly int Speed = Animator.StringToHash(nameof(Speed));

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>() ??
                        throw new NullReferenceException(nameof(_animator));
        }

        public void PlayMovementAnimation(float speed)
        {
            _animator.SetFloat(Speed, speed);
        }
    }
}