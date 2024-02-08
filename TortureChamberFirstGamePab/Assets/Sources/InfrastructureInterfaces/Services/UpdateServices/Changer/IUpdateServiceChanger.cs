using System;

namespace Sources.InfrastructureInterfaces.Services.UpdateServices.Changer
{
    public interface IUpdateServiceChanger
    {
        event Action<float> ChangedUpdate;
        event Action<float> ChangedFixedUpdate;
        event Action<float> ChangedLateUpdate;
    }
}