using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class CoinAnimationView : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _offsetYFinishPoint;

    private bool _canMove;
    
    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
    
    private float _currentTime;
    private float _totalTime;

    private void Start()
    {
        _totalTime = _animationCurve.keys[_animationCurve.keys.Length - 1].time;
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
        
        try
        {
            await RotateCoinAsync(_cancellationTokenSource.Token);
            await MoveToPlayer();
        }
        catch (OperationCanceledException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async UniTask RotateCoinAsync(CancellationToken cancellationToken)
    {
        while (_canMove == false)
        {
            transform.Rotate(0, _rotationSpeed, 0);

            await UniTask.Yield(cancellationToken);
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