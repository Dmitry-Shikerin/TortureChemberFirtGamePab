using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Sources.Infrastructure.Services
{
    public class FindService
    {
        //TODO правильнро ли тут все
        public async UniTask<T> FindObjectOfType<T>() where T : Object
        {
            await UniTask.Yield();
            return Object.FindObjectOfType<T>();
        }
    }
}