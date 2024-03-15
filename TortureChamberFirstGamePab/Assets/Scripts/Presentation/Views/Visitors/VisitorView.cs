using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Controllers.Visitors;
using Scripts.Presentation.Animations;
using Scripts.Presentation.Containers.UI.Images;
using Scripts.Presentation.Views.ObjectPolls;
using Scripts.Presentation.Views.Visitors.Inventorys;
using Scripts.PresentationInterfaces.Views.Visitors;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Presentation.Views.Visitors
{
    public class VisitorView : PresentableView<VisitorPresenter>, IVisitorView
    {
        private List<MeshSkinView> _meshSkins;

        [field: SerializeField] public VisitorAnimation Animation { get; private set; }
        [field: SerializeField] public VisitorInventoryView Inventory { get; private set; }
        [field: SerializeField] public VisitorImageUIContainer VisitorImageUIContainer { get; private set; }

        public IReadOnlyList<MeshSkinView> MeshSkins => _meshSkins;
        public NavMeshAgent NavMeshAgent { get; private set; }
        public Vector3 Position => transform.position;

        public void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>() ??
                           throw new NullReferenceException(nameof(NavMeshAgent));

            _meshSkins = GetComponentsInChildren<MeshSkinView>(true).ToList();
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
            if (NavMeshAgent == null)
                return;

            NavMeshAgent.isStopped = true;
        }

        public void Move() =>
            NavMeshAgent.isStopped = false;

        public void SetDestination(Vector3 destination)
        {
            if (NavMeshAgent == null)
                return;

            NavMeshAgent.destination = destination;
        }

        public void SeatDown(Vector3 position, Quaternion look)
        {
            Transform gameObjectTransform = transform;

            gameObjectTransform.position = position;
            gameObjectTransform.rotation = look;
        }
    }
}