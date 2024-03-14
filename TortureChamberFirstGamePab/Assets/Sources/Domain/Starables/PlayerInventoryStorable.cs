using Newtonsoft.Json;
using Sources.Domain.Players;
using Sources.InfrastructureInterfaces.Repositories;
using Sources.InfrastructureInterfaces.Services.Providers;

namespace Sources.Domain.Starables
{
    public class PlayerInventoryStorable : IStorable
    {
        public PlayerInventoryStorable(PlayerInventory playerInventory)
        {
            PlayerInventory = playerInventory;
        }

        [JsonIgnore] public PlayerInventory PlayerInventory { get; private set; }

        public void Load(IViewFactoryProvider provider)
        {
            PlayerInventory = new PlayerInventory();
        }

        public void Save()
        {
        }
    }
}