using Sources.Domain.Players.Inputs;

namespace Sources.Utils.Extensions.MovementExtensions
{
    public static partial class PlayerMovementExtension
    {
        //TODO сделал экстеншном
        //TODO почистить ноутбук
        public static bool IsIdle(this PlayerInput playerInput) => 
            playerInput.Direction is { x: 0.0f, y: 0.0f };
    }
}