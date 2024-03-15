using Scripts.Domain.DataAccess.Containers.Players;
using Scripts.Domain.DataAccess.Containers.Settings;
using Scripts.Domain.DataAccess.Containers.Taverns;
using Scripts.Infrastructure.Factories.Controllers.UI.AudioSources;
using Scripts.Infrastructure.Factories.Prefabs;
using Scripts.Infrastructure.Factories.Views.UI.AudioSources.BackgroundMusics;
using Scripts.Infrastructure.Services.LoadServices.Components;
using Scripts.Infrastructure.Services.PauseServices;
using Scripts.Infrastructure.Services.VolumeServices;
using Scripts.Infrastructure.Services.YandexSDCServices;
using Scripts.InfrastructureInterfaces.Factories.Prefabs;
using Scripts.InfrastructureInterfaces.Services.LoadServices.Components;
using Scripts.InfrastructureInterfaces.Services.SDCServices;
using Scripts.InfrastructureInterfaces.Services.VolumeServices;
using Zenject;

namespace Scripts.Infrastructure.DIContainers
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