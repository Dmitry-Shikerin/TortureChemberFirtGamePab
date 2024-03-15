using Newtonsoft.Json;
using Scripts.DomainInterfaces.Data;

namespace Scripts.Domain.DataAccess.PlayerData
{
    public class WalletData : IDataModel
    {
        [JsonProperty("coins")]
        public int Coins { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }
    }
}