using System;
using Scripts.PresentationInterfaces.Animations;
using UnityEngine;

namespace Scripts.Presentation.Animations
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimation : MonoBehaviour, IPlayerAnimation
    {
        private readonly int _speed = Animator.StringToHash(nameof(_speed));

        private Animator _animator;

        private void Awake() =>
            _animator = GetComponent<Animator>() ??
                        throw new NullReferenceException(nameof(_animator));

        public void PlayMovementAnimation(float speed) =>
            _animator.SetFloat(_speed, speed);
    }
}