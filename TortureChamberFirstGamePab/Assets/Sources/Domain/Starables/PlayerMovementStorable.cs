using System;
using MyProject.Sources.Presentation.Views;
using Newtonsoft.Json;
using Sources.Domain.Data;
using Sources.Domain.Players;
using Sources.Domain.Players.PlayerMovements;
using Sources.Extensions.Domain;
using Sources.Infrastructure.Factories.Views.Players;
using Sources.Infrastructure.Services.Providers;
using Sources.InfrastructureInterfaces.Repositories;
using Sources.Presentation.Views.Player;

namespace Sources.Domain.Starables
{
    public class PlayerMovementStorable : IStorable
    {
        private readonly PlayerInventory _playerInventory;

        public PlayerMovementStorable(PlayerMovement playerMovement, PlayerInventory playerInventory)
        {
            _playerInventory = playerInventory;
            PlayerMovement = playerMovement;
        }

        [JsonIgnore]
        public PlayerMovement PlayerMovement { get; private set; }
        //TODO исправить этот костыль
        [JsonIgnore]
        public PlayerMovementView PlayerMovementView { get; private set; }
        
        public Vector3Data Position { get; set; }
        
        public void Load(IViewFactoryProvider provider)
        {
            var viewFactory = provider.Get<PlayerMovementViewFactory>() ??
                              throw new InvalidOperationException(nameof(PlayerMovementViewFactory));

            PlayerMovement = new PlayerMovement();
            PlayerMovement.Position = Position.Vector3DataToVector3();

            //TODO потом раскоментировать
            //TODO в зависимость требует модель инвентаря
            //TODO смогу ли я исправить это Zenjectom?
            // PlayerMovementView = viewFactory.Create(PlayerMovement, _playerInventory);
        }

        public void Save()
        {
            Position = PlayerMovement.Position.Vector3ToVector3Data();
        }
    }
}