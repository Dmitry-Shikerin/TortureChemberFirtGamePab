using InfrastructureInterfaces.Services;
using Presentation.Containers.HUDs;
using Presentation.Containers.UI;
using UnityEngine;

namespace Infrastructure.Services
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
            if (Application.isMobilePlatform)
            {
                _joystickContainer.Movement.IsDynamicJoystick = false;
                _joystickContainer.Rotate.IsDynamicJoystick = false;

                return;
            }

            _joystickContainer.Movement.IsDynamicJoystick = true;
            _joystickContainer.Rotate.IsDynamicJoystick = true;
        }
    }
}