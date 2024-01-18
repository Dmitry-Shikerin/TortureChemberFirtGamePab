﻿using System;
using Newtonsoft.Json;
using Sources.Domain.Players;
using Sources.Infrastructure.Factories.Views.Players;
using Sources.Infrastructure.Services.Providers;
using Sources.InfrastructureInterfaces.Repositories;

namespace Sources.Domain.Starables
{
    public class PlayerInventoryStorable : IStorable
    {

        public PlayerInventoryStorable(PlayerInventory playerInventory)
        {
            PlayerInventory = playerInventory;
        }
        [JsonIgnore]
        public PlayerInventory PlayerInventory { get; private set; }

        public void Load(IViewFactoryProvider provider)
        {
            var viewFactory = provider.Get<PlayerInventoryViewFactory>() ??
                              throw new InvalidOperationException(nameof(PlayerInventoryViewFactory));

            PlayerInventory = new PlayerInventory();

            //TODO потом раскоментировать
            //TODO в зависимость требует модель инвентаря
            //TODO смогу ли я исправить это Zenjectom?
            //TODO где взять ссылки на эти зависимости?
            
            // viewFactory.Create();
        }

        public void Save()
        {
            
        }
    }
}