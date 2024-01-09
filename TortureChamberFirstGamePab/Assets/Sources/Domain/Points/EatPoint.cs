using UnityEngine;

namespace Sources.Domain
{
    public class EatPoint
    {
        public bool IsClear { get; private set; } = true;

        public void Clean() => 
            IsClear = true;

        //TODO название с гет?
        public void GetDirty() => 
            IsClear = false;
    }
}