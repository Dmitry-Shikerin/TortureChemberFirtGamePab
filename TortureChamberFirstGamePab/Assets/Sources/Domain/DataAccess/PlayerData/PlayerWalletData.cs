using Newtonsoft.Json;

namespace Sources.Infrastructure.Services.LoadServices.DataAccess.PlayerData
{
    public class PlayerWalletData
    {
        [JsonProperty("coins")]
        public int Coins { get; set; }
        
        [JsonProperty("score")]
        public int Score { get; set; }
    }
}