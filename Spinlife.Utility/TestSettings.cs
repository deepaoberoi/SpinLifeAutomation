
namespace Spinlife.Utility
{
    public class TestSettings
    {
        public string Authority { get; set; }
        public UserAuthenticationSettings UserAuthentication { get; set; }

        public TestSettings()
        {

            Authority = "https://login.microsoftonline.com/dd0ccbfb-931f-40a2-aaa3-a21530629401/v2.0";
            UserAuthentication = new UserAuthenticationSettings();
        }
    }
}
