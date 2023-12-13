using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreateGrass : MonoBehaviour
{
    [SerializeField] private GameObject _grassPrefab;
    [SerializeField] private int _grassSize = 20;
    void Start()
    {
        //TODO ужасное условие!!! переделать
        for (int i = -_grassSize; i <= 20; i++)
        {
            for (int j = -_grassSize; j <= _grassSize; j++)
            {
                Vector3 position = new Vector3(j/4.0f, 0, i/4.0f);
                GameObject grass = Instantiate(_grassPrefab, position, Quaternion.identity);
                grass.transform.parent = gameObject.transform;
                grass.transform.localScale = new Vector3(1, Random.Range(0.8f, 1.2f), 1);
            }
        }
    }

    void Update()
    {
        
    }
}
