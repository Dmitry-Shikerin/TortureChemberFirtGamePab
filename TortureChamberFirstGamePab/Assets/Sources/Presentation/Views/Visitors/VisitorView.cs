using System;
using Sources.Controllers;
using Sources.PresentationInterfaces.Views;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Presentation.Views
{
    public class VisitorView : PresentableView<VisitorPresenter>, IVisitorView
    {
        public NavMeshAgent NavMeshAgent { get; private set; }

        public Vector3 Position => transform.position;

        public void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>() ?? 
                            throw new NullReferenceException(nameof(NavMeshAgent));
        }

        public void Update()
        {
            Presenter?.Update();
        }

        protected override void OnConstructed()
        {
            Presenter?.Start();
        }

        public void SetDestination(Vector3 destination)
        {
            NavMeshAgent.destination = destination;
        }

        public void SeatDown(Vector3 position, Quaternion look)
        {
            transform.position = position;
            transform.rotation = look;
        }
    }
}
