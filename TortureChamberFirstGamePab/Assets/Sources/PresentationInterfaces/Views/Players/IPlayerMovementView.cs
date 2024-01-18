using UnityEngine;

namespace MyProject.Sources.PresentationInterfaces.Views
{
    public interface IPlayerMovementView
    {
        Vector3 Position { get; }
        //TODO плохая идея потом заменить
        Transform Transform { get; }
        
        public void Move(Vector3 direction);
        public void Rotate(Quaternion look, float speed);
    }
}