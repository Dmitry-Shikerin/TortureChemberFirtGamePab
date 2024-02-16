using Sources.App.Bootstrap;
using Sources.Domain.Datas.Players;
using Sources.Domain.Datas.Taverns;
using Sources.Infrastructure.Factories.Controllers.UI;
using Sources.Infrastructure.Factories.Prefabs;
using Sources.Infrastructure.Factories.Scenes;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Infrastructure.Services.PauseServices;
using Sources.Infrastructure.Services.SceneServices;
using Sources.Infrastructure.Services.YandexSDCServices;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.InfrastructureInterfaces.Services.SDCServices;
using UnityEngine;
using Zenject;

namespace Sources.Infrastructure.DIContainers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IDataService<Player>>().To<PlayerDataService>().AsSingle();
            Container.Bind<IDataService<Tavern>>().To<TavernDataService>().AsSingle();
            Container.Bind<IDataService<PlayerUpgrade>>().To<PlayerUpgradeDataService>().AsSingle();

            Container.BindInterfacesAndSelfTo<PauseService>().AsSingle();
            Container.Bind<IPrefabFactory>().To<PrefabFactory>().AsSingle();
            
            Container.Bind<IInitializeService>().To<SDKInitializeService>().AsSingle();
            Container.Bind<IFocusService>().To<FocusService>().AsSingle();
        }
    }
}