using System;

namespace InfrastructureInterfaces.Services.SDCServices
{
    public interface IVideoAdService
    {
        void ShowVideo(Action onCloseCallback);
    }
}