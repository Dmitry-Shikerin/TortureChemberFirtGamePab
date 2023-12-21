using UnityEngine;

namespace MyProject.Sources.Domain.PlayerVision
{
    public class PlayerVision
    {
        //TODO можно хранить в сервисе но сосдавать новый экземпляр сервиса
        public Collider[] Colliders { get; private set; } = new Collider[32];
    }
}