using System.Collections.Generic;
using Newtonsoft.Json;
using Sources.Domain.Players;
using Sources.DomainInterfaces.Items;

namespace Sources.Infrastructure.Services.LoadServices.DataAccess
{
    public class PlayerInventoryData
    {
        [JsonProperty("items")]
        public PlayerInventoryItemData[] Items { get; set; }
    }
}