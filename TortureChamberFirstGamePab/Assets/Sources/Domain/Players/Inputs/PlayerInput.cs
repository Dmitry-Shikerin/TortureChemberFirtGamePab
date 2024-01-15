using UnityEngine;

namespace Sources.Domain.Players.Inputs
{
    public struct PlayerInput
    {
        public PlayerInput(Vector2 direction)
        {
            Direction = direction;
        }

        private Vector2 Direction { get; set; }
        //TODO добавить булки для ротации
    }
}