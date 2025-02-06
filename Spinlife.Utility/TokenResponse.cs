using Newtonsoft.Json;

namespace AlerStallings.Utility
{
    class TokenResponse
    {
        [JsonProperty("id_token")]
        public string AccessToken { get; set; }
    }
}
