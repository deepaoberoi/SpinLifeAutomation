using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AlerStallings.Utility
{
    class AccessTokenProvider
    {
        private static TestSettings _settings;
        private static HttpClient _client;

        public AccessTokenProvider()
        {
            _settings = new TestSettings();
            _client = new HttpClient();
        }

        public async Task AuthenticateRequestAsUserAsync(HttpRequestMessage req)
        {
            var tokenReq = new HttpRequestMessage(HttpMethod.Post, _settings.UserAuthentication.TokenUrl)
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["grant_type"] = "password",
                    ["username"] = _settings.UserAuthentication.Username,
                    ["password"] = _settings.UserAuthentication.Password,
                    ["client_id"] = _settings.UserAuthentication.ClientId,
                    ["client_secret"] = _settings.UserAuthentication.ClientSecret,
                    ["scope"] = "openid profile"
                })
            };

            var res = await _client.SendAsync(tokenReq);

            string json = await res.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(json);

            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
        }
    }
}