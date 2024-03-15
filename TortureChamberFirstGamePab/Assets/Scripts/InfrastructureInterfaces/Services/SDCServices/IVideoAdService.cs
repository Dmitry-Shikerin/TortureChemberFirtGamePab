using System;

namespace Scripts.InfrastructureInterfaces.Services.SDCServices
{
    public interface IVideoAdService
    {
        void ShowVideo(Action onCloseCallback);
    }
}