using DomainInterfaces.Data;
using Newtonsoft.Json;

namespace Domain.DataAccess.PlayerData
{
    public class WalletData : IDataModel
    {
        [JsonProperty("coins")]
        public int Coins { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }
    }
}