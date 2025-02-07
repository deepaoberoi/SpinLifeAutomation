namespace Spinlife.Config
{
    public static class SpinlifeStagingConfig
    {
        public static string BetteUrl { get; } = "https://bette.spinlife.biz/admin/";
        public static string username = "webqa";
		public static string password = "Rev0luti0ns";
		public static string managemycookieurl = $"https://{username}:{password}@staging.spinlife.biz/managemycookies.cfm?gfs=1&action=set&cookie_id=51&new_value=on";

        public static string Product = "SpinLife Classic PR-458 3-Position";

        public static string CreditCardNo = "4111111111111111";
        public static string BetteUserName = "deepa.oberoi@numotion.com";
        public static string BettePassword = "1990@Diya";
    }
}