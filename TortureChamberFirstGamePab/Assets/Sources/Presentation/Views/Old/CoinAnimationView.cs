using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sources.Presentation.Views.Old
{
    public class CoinAnimationView : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private AnimationCurve _animationCurve;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _offsetYFinishPoint;

        private bool _canMove;

        private CancellationTokenSource _cancellationTokenSource;

        private float _currentTime;
        private float _totalTime;

        private void Start()
        {
            _totalTime = _animationCurve.keys[_animationCurve.keys.Length - 1].time;
            _canMove = false;
        }

        private void OnEnable()
        {
            Collect();
        }

        private void OnDisable()
        {
            Debug.Log("Монетка выключилась");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _canMove = true;
                _cancellationTokenSource.Cancel();
            }
        }

        private async void Collect()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            //
            // try
            // {
            if (gameObject != null)
            {
                await RotateCoinAsync();
                await MoveToPlayer();
            }
            // catch (OperationCanceledException e)
            // {
            //     Console.WriteLine(e);
            //     throw;
            // }
        }

        //TODO вылетают какието непонятные ошибки
        private async UniTask RotateCoinAsync()
        {
            while (_canMove == false)
            {
                // if (gameObject != null)
                transform.Rotate(0, _rotationSpeed, 0);
                await UniTask.Yield();
            }
        }

        private async UniTask MoveToPlayer()
        {
            while (Vector3.Distance(transform.position, new Vector3(_playerTransform.position.x,
                       _playerTransform.position.y + _offsetYFinishPoint,
                       _playerTransform.position.z)) > 0.04f)
            {
                _currentTime += Time.deltaTime;

                transform.position = Vector3.MoveTowards(transform.position,
                    new Vector3(_playerTransform.position.x,
                        _playerTransform.position.y +
                        _animationCurve.Evaluate(_currentTime),
                        _playerTransform.position.z), _movementSpeed * Time.deltaTime);

                await UniTask.Yield();
            }
        }
    }
}