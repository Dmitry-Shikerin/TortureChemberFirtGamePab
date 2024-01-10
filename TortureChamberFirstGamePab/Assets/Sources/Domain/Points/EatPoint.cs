using UnityEngine;

namespace Sources.Domain
{
    public class EatPoint
    {
        public bool IsClear { get; private set; } = true;

        public void Clean() => 
            IsClear = true;

        public void SetDirty() => 
            IsClear = false;
    }
}