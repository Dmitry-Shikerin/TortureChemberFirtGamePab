using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace MyProject.Sources.Infrastructure.Services
{
    public class OverlapService
    {
        private readonly LinecastService _linecastService;
        private Collider[] _colliders  = new Collider[32];

        public OverlapService(LinecastService linecastService)
        {
            _linecastService = linecastService ??
                               throw new ArgumentNullException(nameof(linecastService));
        }

        public IReadOnlyList<T> OverlapSphere<T>(Vector3 position, float radius,
            int searchLayerMask, int obstacleLayerMask) 
            where T : MonoBehaviour
        {
            int collidersCount = Overlap(position, radius, searchLayerMask);

            if (collidersCount == 0)
                return new List<T>();

            return Filter<T>(position, obstacleLayerMask, collidersCount);
        }

        private IReadOnlyList<T> Filter<T>(Vector3 position, int obstacleLayerMask,
            int collidersCount) where T : MonoBehaviour
        {
            List<T> components = new List<T>();

            for (int i = 0; i < collidersCount; i++)
            {
                Collider collider = _colliders[i];

                if (collider.TryGetComponent(out T component) == false)
                    continue;

                Vector3 colliderPosition = collider.transform.position;

                if (_linecastService.Linecast(position, colliderPosition, obstacleLayerMask))
                    continue;
                
                components.Add(component);
            }

            return components;
        }

        private int Overlap(Vector3 position, float radius,int searchLayerMask) => 
            Physics.OverlapSphereNonAlloc(position, radius, _colliders, searchLayerMask);
    }
}