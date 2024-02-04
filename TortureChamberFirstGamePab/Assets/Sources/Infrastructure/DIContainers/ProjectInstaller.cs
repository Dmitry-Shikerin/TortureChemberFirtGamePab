using Sources.App.Bootstrap;
using Sources.Domain.Players.Data;
using Sources.Infrastructure.Factories.Controllers.UI;
using Sources.Infrastructure.Factories.Scenes;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Infrastructure.Services.SceneServices;
using Sources.Infrastructure.Services.YandexSDCServices;
using UnityEngine;
using Zenject;

namespace Sources.Infrastructure.DIContainers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IDataService<Player>>().To<PlayerDataService>().AsSingle();

            Container.Bind<SDKInitializeService>().AsSingle();
            Container.Bind<FocusService>().AsSingle();
        }
    }
}