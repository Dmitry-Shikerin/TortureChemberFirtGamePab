﻿using UnityEngine;
using UnityEngine.AI;

namespace Sources.PresentationInterfaces.Views
{
    public interface IVisitorView
    {
        Vector3 Position { get; }
        NavMeshAgent NavMeshAgent { get; }

        void Move();
        void StopMove();
        void SetPosition(Vector3 position);
        void SetDestination(Vector3 destination);
        void SeatDown(Vector3 position, Quaternion look);
        void Destroy();
    }
}