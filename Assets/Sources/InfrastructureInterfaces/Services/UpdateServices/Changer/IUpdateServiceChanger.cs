using System;

namespace InfrastructureInterfaces.Services.UpdateServices.Changer
{
    public interface IUpdateServiceChanger
    {
        event Action<float> ChangedUpdate;
        event Action<float> ChangedLateUpdate;
    }
}