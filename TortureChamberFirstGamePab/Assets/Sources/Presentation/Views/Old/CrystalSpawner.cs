using System.Collections;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.ObjectPools;
using UnityEngine;

namespace Sources.Presentation.Views.Old
{
    public class CrystalSpawner : MonoBehaviour
    { 
        // [SerializeField] private CrystalView _prefab;
        // [SerializeField] private float _spawnInterval = 1f;
        // [SerializeField] private float _spawnradius = 10f;
        // [SerializeField] private float _maxCrystals = 4;
        //
        // private IObjectPool _objectPool;
        // private CrystalViewFactory _factory;
        //
        // private Coroutine _coroutine;
        // private WaitForSeconds _waitForSeconds;
        //
        // private int _objectsCount;
        //
        // private void Awake()
        // {
        //     _objectPool = new ObjectPool<CrystalView>();
        //     _factory = new CrystalViewFactory(_prefab, _objectPool);
        //
        //     _waitForSeconds = new WaitForSeconds(_spawnInterval);
        // }
        //
        // private void Update()
        // {
        //     if(TrySpawn())
        //         _coroutine = StartCoroutine(CrystalSpawn());
        //
        // }
        //
        // private bool TrySpawn()
        // {
        //     _objectsCount = gameObject.GetComponentsInChildren<CrystalView>().Length;
        //     
        //     if (_objectsCount < _maxCrystals)
        //     {
        //         if (_coroutine != null)
        //             return false;
        //
        //         return true;
        //     }
        //
        //     return false;
        // }
        //
        // private IEnumerator CrystalSpawn()
        // {
        //     while (_objectsCount < _maxCrystals)
        //     {
        //         yield return _waitForSeconds;
        //         Spawn();
        //         yield return _waitForSeconds;
        //     }
        //
        //     yield return StopCoroutine();
        // }
        //
        // private IEnumerator StopCoroutine()
        // {
        //     if (_coroutine != null)
        //     {
        //         StopCoroutine(_coroutine);
        //         _coroutine = null;
        //     }
        //
        //     yield return null;
        // }
        //
        // private void Spawn()
        // {
        //     CrystalView crystal = _objectPool.Get<CrystalView>() ?? _factory.Create();
        //
        //     crystal.SetPosition(GetSpawnPosition());
        //     crystal.SetParent(transform);
        //     crystal.SetAvailable();
        //     crystal.Show();
        // }
        //
        // private Vector3 GetSpawnPosition()
        // {
        //     int minAngle = 1;
        //     int maxAngle = 360;
        //
        //     float angle = Random.Range(minAngle, maxAngle);
        //
        //     return transform.position + Quaternion.Euler(Vector3.up * angle) *
        //         Vector3.forward * _spawnradius;
        // }

    }
}