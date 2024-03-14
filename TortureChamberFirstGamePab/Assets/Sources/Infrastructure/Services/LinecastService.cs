using UnityEngine;

namespace Sources.Infrastructure.Services
{
    public class LinecastService
    {
        public bool Linecast(Vector3 position, Vector3 colliderPosition, int obstacleLayerMask)
        {
            return Physics.Linecast(position, colliderPosition, obstacleLayerMask);
        }
    }
}