using Newtonsoft.Json;
using Sources.DomainInterfaces.Data;

namespace Sources.Infrastructure.Services.LoadServices.DataAccess.PlayerData
{
    public class PlayerWalletData : IDataModel
    {
        [JsonProperty("coins")] public int Coins { get; set; }

        [JsonProperty("score")] public int Score { get; set; }
    }
}