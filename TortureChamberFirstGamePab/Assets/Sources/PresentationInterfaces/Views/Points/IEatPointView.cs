using UnityEngine;

namespace Sources.PresentationInterfaces.Views.Points
{
    public interface IEatPointView
    {
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
        public bool IsClear { get; }

        void Clean();
        void SetDirty();
    }
}