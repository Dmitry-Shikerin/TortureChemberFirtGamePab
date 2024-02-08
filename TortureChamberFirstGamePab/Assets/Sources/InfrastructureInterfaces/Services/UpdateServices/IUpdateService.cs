using System;
using Sources.InfrastructureInterfaces.Services.UpdateServices;

namespace Sources.InfrastructureInterfaces.Services
{
    public interface IUpdateService : IUpdatable, IFixedUpdatable, ILateUpdatable
    {
    }
}