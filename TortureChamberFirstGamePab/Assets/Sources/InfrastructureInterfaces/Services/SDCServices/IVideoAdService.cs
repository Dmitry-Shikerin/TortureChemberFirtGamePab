﻿using System;

namespace Sources.InfrastructureInterfaces.Services.SDCServices
{
    public interface IVideoAdService
    {
        void ShowVideo(Action onCloseCallback);
    }
}