using System.Collections.Generic;
using Scripts.Presentation.Containers.UI.Images;
using Scripts.Presentation.Views.Visitors;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.PresentationInterfaces.Views.Visitors
{
    public interface IVisitorView
    {
        Vector3 Position { get; }
        NavMeshAgent NavMeshAgent { get; }
        VisitorImageUIContainer VisitorImageUIContainer { get; }
        IReadOnlyList<MeshSkinView> MeshSkins { get; }

        void Move();
        void StopMove();
        void SetPosition(Vector3 position);
        void SetDestination(Vector3 destination);
        void SeatDown(Vector3 position, Quaternion look);
        void Destroy();
    }
}