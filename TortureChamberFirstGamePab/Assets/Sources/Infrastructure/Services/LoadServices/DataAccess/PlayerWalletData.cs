using Newtonsoft.Json;

namespace Sources.Infrastructure.Services.LoadServices.DataAccess
{
    public class PlayerWalletData
    {
        [JsonProperty("coins")]
        public int Coins { get; set; }
    }
}