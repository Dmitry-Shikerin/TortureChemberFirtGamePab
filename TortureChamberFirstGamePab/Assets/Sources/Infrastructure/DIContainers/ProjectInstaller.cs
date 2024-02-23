using Sources.Domain.DataAccess.Containers.Settings;
using Sources.Domain.Datas.Players;
using Sources.Domain.Datas.Taverns;
using Sources.Infrastructure.Factories.Prefabs;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Infrastructure.Services.PauseServices;
using Sources.Infrastructure.Services.VolumeServices;
using Sources.Infrastructure.Services.YandexSDCServices;
using Sources.Infrastructure.Services.YandexSDCServices.WebGlServices;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.SDCServices;
using Sources.InfrastructureInterfaces.Services.SDCServices.WebGlServices;
using Sources.InfrastructureInterfaces.Services.VolumeServices;
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
            Container.Bind<IDataService<Setting>>().To<SettingDataService>().AsSingle();
            
            Container.Bind<Setting>().AsSingle();

            Container.BindInterfacesAndSelfTo<PauseService>().AsSingle();
            Container.Bind<IVolumeService>().To<VolumeService>().AsSingle();
            Container.Bind<IPrefabFactory>().To<PrefabFactory>().AsSingle();
            
            Container.Bind<IInitializeService>().To<SDKInitializeService>().AsSingle();
            Container.Bind<IFocusService>().To<FocusService>().AsSingle();
        }
    }
}