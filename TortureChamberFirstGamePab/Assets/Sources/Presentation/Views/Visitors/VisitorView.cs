using System;
using Sources.Controllers;
using Sources.DomainInterfaces.Items;
using Sources.Presentation.Views.ObjectPolls;
using Sources.PresentationInterfaces.Views;
using Sources.PresentationInterfaces.Views.Interactions.Get;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Presentation.Views.Visitors
{
    public class VisitorView : PresentableView<VisitorPresenter>, IVisitorView
    {
        public NavMeshAgent NavMeshAgent { get; private set; }

        public Vector3 Position => transform.position;

        public void Awake() =>
            NavMeshAgent = GetComponent<NavMeshAgent>() ??
                           throw new NullReferenceException(nameof(NavMeshAgent));

        public void Destroy()
        {
            if (TryGetComponent(out PoolableObject poolableObject) == false)
            {
                Destroy(gameObject);

                return;
            }

            poolableObject.ReturnTooPool();
            Hide();
        }
        
        public void Update() =>
            Presenter?.Update();

        protected override void OnConstructed() =>
            Presenter?.Start();

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        //TODO работает не так как я ожидал
        public void StopMove()
        {
            NavMeshAgent.isStopped = true;
            // NavMeshAgent.Stop();
        }

        public void Move()
        {
            NavMeshAgent.isStopped = false;
        }
        
        public void SetDestination(Vector3 destination) =>
            NavMeshAgent.destination = destination;

        public void SeatDown(Vector3 position, Quaternion look)
        {
            transform.position = position;
            transform.rotation = look;
        }
    }
}
