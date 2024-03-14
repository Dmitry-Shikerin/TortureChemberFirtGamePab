using System;
using System.Collections.Generic;
using Sources.Domain.Constants;
using UnityEngine;

namespace Sources.Infrastructure.Services
{
    public class OverlapService
    {
        private readonly Collider[] _colliders = new Collider[Constant.Overlap.MaxCollidersValue];
        private readonly LinecastService _linecastService;

        public OverlapService(LinecastService linecastService)
        {
            _linecastService = linecastService ??
                               throw new ArgumentNullException(nameof(linecastService));
        }

        public IReadOnlyList<T> OverlapSphere<T>(
            Vector3 position,
            float radius,
            int searchLayerMask,
            int obstacleLayerMask)
            where T : MonoBehaviour
        {
            var collidersCount = Overlap(position, radius, searchLayerMask);

            if (collidersCount == 0)
                return new List<T>();

            return Filter<T>(position, obstacleLayerMask, collidersCount);
        }

        private IReadOnlyList<T> Filter<T>(
            Vector3 position,
            int obstacleLayerMask,
            int collidersCount)
            where T : MonoBehaviour
        {
            var components = new List<T>();

            for (var i = 0; i < collidersCount; i++)
            {
                var collider = _colliders[i];

                if (collider.TryGetComponent(out T component) == false)
                    continue;

                var colliderPosition = collider.transform.position;

                if (_linecastService.Linecast(position, colliderPosition, obstacleLayerMask))
                    continue;

                components.Add(component);
            }

            return components;
        }

        private int Overlap(Vector3 position, float radius, int searchLayerMask)
        {
            return Physics.OverlapSphereNonAlloc(position, radius, _colliders, searchLayerMask);
        }
    }
}