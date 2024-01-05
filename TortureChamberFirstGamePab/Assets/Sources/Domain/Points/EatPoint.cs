using UnityEngine;

namespace Sources.Domain
{
    public class EatPoint
    {
        public bool IsClear { get; private set; }

        public void SetIsClear(bool isClear)
        {
            Debug.Log($"Чистота места {isClear}");
            IsClear = isClear;
        }
    }
}