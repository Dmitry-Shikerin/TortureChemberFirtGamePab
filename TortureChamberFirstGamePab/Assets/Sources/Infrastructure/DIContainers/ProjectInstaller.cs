using Sources.Domain.Players.Data;
using Sources.Infrastructure.Factories.Controllers.UI;
using Sources.Infrastructure.Factories.Scenes;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Infrastructure.Services.SceneServices;
using Zenject;

namespace Sources.Infrastructure.DIContainers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IDataService<Player>>().To<PlayerDataService>().AsSingle();
        }
    }
}