using Sources.App.Bootstrap;
using Sources.Domain.DataAccess.Containers.Settings;
using Sources.Domain.Datas.Players;
using Sources.Domain.Datas.Taverns;
using Sources.Domain.Settings;
using Sources.Infrastructure.Factories.Controllers.UI;
using Sources.Infrastructure.Factories.Prefabs;
using Sources.Infrastructure.Factories.Scenes;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Infrastructure.Services.PauseServices;
using Sources.Infrastructure.Services.Providers.Settings;
using Sources.Infrastructure.Services.SceneServices;
using Sources.Infrastructure.Services.VolumeServices;
using Sources.Infrastructure.Services.YandexSDCServices;
using Sources.Infrastructure.Services.YandexSDCServices.WebGlServices;
using Sources.InfrastructureInterfaces.Factories.Prefabs;
using Sources.InfrastructureInterfaces.Services.LoadServices.Components;
using Sources.InfrastructureInterfaces.Services.PauseServices;
using Sources.InfrastructureInterfaces.Services.SDCServices;
using Sources.InfrastructureInterfaces.Services.SDCServices.WebGlServices;
using Sources.InfrastructureInterfaces.Services.VolumeServices;
using UnityEngine;
using Zenject;

namespace Sources.Infrastructure.DIContainers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IWebGlService>().To<WebGlService>().AsSingle();
            
            Container.Bind<IDataService<Player>>().To<PlayerDataService>().AsSingle();
            Container.Bind<IDataService<Tavern>>().To<TavernDataService>().AsSingle();
            Container.Bind<IDataService<PlayerUpgrade>>().To<PlayerUpgradeDataService>().AsSingle();
            Container.Bind<IDataService<Setting>>().To<SettingDataService>().AsSingle();

            
            Setting setting = new Setting(new Volume());
            
            IDataService<Setting> settingDataService = Container.Resolve<IDataService<Setting>>();

            if (settingDataService.CanLoad)
                setting = settingDataService.Load();

            //TODO какието варнинги изза этого инстанса
            Container.Bind<Setting>().FromInstance(setting).AsSingle();

            Container.BindInterfacesAndSelfTo<PauseService>().AsSingle();
            Container.Bind<IVolumeService>().To<VolumeService>().AsSingle();
            Container.Bind<IPrefabFactory>().To<PrefabFactory>().AsSingle();
            
            Container.Bind<IInitializeService>().To<SDKInitializeService>().AsSingle();
            Container.Bind<IFocusService>().To<FocusService>().AsSingle();
        }
    }
}