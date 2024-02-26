﻿using Sources.InfrastructureInterfaces.Services;
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

        //TODO Работает
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