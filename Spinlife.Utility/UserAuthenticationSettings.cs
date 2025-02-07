
using Spinlife.Support;

namespace Spinlife.Utility
{
    public class UserAuthenticationSettings
    {
        public string TokenUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public UserAuthenticationSettings()
        {
            TokenUrl = JsonHelper.GetDataByEnvironment("TokenUrl");
            ClientId = JsonHelper.GetDataByEnvironment("ClientId");
            ClientSecret = JsonHelper.GetDataByEnvironment("ClientSecret");
            Username = JsonHelper.GetDataByEnvironment("AdminUsername");
            Password = JsonHelper.GetDataByEnvironment("AdminPassword");
        }
    }
}
