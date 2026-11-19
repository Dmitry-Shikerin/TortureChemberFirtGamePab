using Domain.DataAccess.Containers.Players;
using Domain.DataAccess.Containers.Settings;
using Domain.DataAccess.Containers.Taverns;
using Infrastructure.Factories.Controllers.UI.AudioSources;
using Infrastructure.Factories.Prefabs;
using Infrastructure.Factories.Views.UI.AudioSources.BackgroundMusics;
using Infrastructure.Services.LoadServices.Components;
using Infrastructure.Services.PauseServices;
using Infrastructure.Services.VolumeServices;
using Infrastructure.Services.YandexSDCServices;
using InfrastructureInterfaces.Factories.Prefabs;
using InfrastructureInterfaces.Services.LoadServices.Components;
using InfrastructureInterfaces.Services.SDCServices;
using InfrastructureInterfaces.Services.VolumeServices;
using Zenject;

namespace Infrastructure.DIContainers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IDataService<Player>>().To<PlayerDataService>().AsSingle();
            Container.Bind<IDataService<Tavern>>().To<TavernDataService>().AsSingle();
            Container.Bind<IDataService<PlayerUpgrade>>().To<PlayerUpgradeDataService>().AsSingle();
            Container.Bind<IDataService<Setting>>().To<SettingDataService>().AsSingle();

            Container.Bind<Setting>().AsSingle();

            Container.Bind<IStickyService>().To<StickyService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PauseService>().AsSingle();
            Container.Bind<IVolumeService>().To<VolumeService>().AsSingle();
            Container.Bind<IPrefabFactory>().To<PrefabFactory>().AsSingle();

            Container.Bind<IInitializeService>().To<SDKInitializeService>().AsSingle();
            Container.Bind<IFocusService>().To<FocusService>().AsSingle();

            Container.Bind<BackgroundMusicPresenterFactory>().AsSingle();
            Container.Bind<BackgroundMusicViewFactory>().AsSingle();
        }
    }
}