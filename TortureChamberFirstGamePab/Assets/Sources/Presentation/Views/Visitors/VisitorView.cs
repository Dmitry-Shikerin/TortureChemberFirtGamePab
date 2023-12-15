using System;
using Sources.Controllers;
using Sources.PresentationInterfaces.Views;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Presentation.Views
{
    public class VisitorView : PresentableView<VisitorPresenter>, IVisitorView
    {
        //TODO потом удалить
        [SerializeField] private SeatPoint _seatPoint;
        
        public NavMeshAgent NavMeshAgent { get; private set; }

        public Vector3 Position => transform.position;

        public void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>() ?? 
                            throw new NullReferenceException(nameof(NavMeshAgent));
        }

        //TODO здесь ли запускатьстейт машину?
        public void Start()
        {
            Presenter?.Start();
        }

        public void Update()
        {
            Presenter?.Update();
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
