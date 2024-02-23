using Sources.InfrastructureInterfaces.StateMachines.SceneStateMachines;
using Sources.Presentation.Containers.UI;
using Sources.Presentation.Voids;
using UnityEngine;

namespace Sources.Infrastructure.Services
{
    public class MobilePlatformService : IMobilePlatformService
    {
        private readonly JoysticksContainer _joystickContainer;

        public MobilePlatformService(HUD hud)
        {
            _joystickContainer = hud.JoysticksContainer;
        }

        public void Enter(object payload = null)
        {
            //TODO сделать такую проверку
            // Application.isMobilePlatform

            // if (Application.isMobilePlatform)
            // {
            //     _joystickContainer.Movement.
            // }
        }
    }

    public interface IMobilePlatformService : IEnterable
    {
    }
}