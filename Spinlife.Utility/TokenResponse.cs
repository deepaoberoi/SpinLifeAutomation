using Newtonsoft.Json;

namespace Spinlife.Utility
{
    class TokenResponse
    {
        [JsonProperty("id_token")]
        public string AccessToken { get; set; }
    }
}
