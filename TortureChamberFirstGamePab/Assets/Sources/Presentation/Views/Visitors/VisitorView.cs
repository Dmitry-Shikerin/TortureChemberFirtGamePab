using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Controllers.Visitors;
using Sources.Presentation.Animations;
using Sources.Presentation.Views.ObjectPolls;
using Sources.Presentation.Views.Visitors.Inventorys;
using Sources.PresentationInterfaces.Views;
using Sources.PresentationInterfaces.Views.Visitors;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Presentation.Views.Visitors
{
    public class VisitorView : PresentableView<VisitorPresenter>, IVisitorView
    {
        [field: SerializeField] public VisitorAnimation Animation { get; private set; }
        [field: SerializeField] public VisitorImageUIContainer VisitorImageUIContainer { get; private set; }
        [field: SerializeField] public VisitorInventoryView Inventory { get; private set; }

        private List<MeshSkinView> _meshSkins;

        public IReadOnlyList<MeshSkinView> MeshSkins => _meshSkins;
        public NavMeshAgent NavMeshAgent { get; private set; }
        

        public Vector3 Position => transform.position;

        public void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>() ??
                           throw new NullReferenceException(nameof(NavMeshAgent));

            _meshSkins = GetComponentsInChildren<MeshSkinView>(true).ToList();
            
            Debug.Log(_meshSkins.Count);
        }

        public override void Destroy()
        {
            if (TryGetComponent(out PoolableObject poolableObject) == false)
            {
                Destroy(gameObject);
                DestroyPresenter();

                return;
            }

            poolableObject.ReturnTooPool();
            
            Hide();
            DestroyPresenter();
        }
        
        public void SetPosition(Vector3 position) => 
            transform.position = position;

        public void StopMove()
        {
            if(NavMeshAgent == null)
                return;
            
            NavMeshAgent.isStopped = true;
        }

        public void Move() => 
            NavMeshAgent.isStopped = false;

        public void SetDestination(Vector3 destination)
        {
            if(NavMeshAgent == null)
                return;
            
            NavMeshAgent.destination = destination;
        }

        public void SeatDown(Vector3 position, Quaternion look)
        {
            transform.position = position;
            transform.rotation = look;
        }
    }
}